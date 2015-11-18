using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TrainTicketBookingSystem.Helpers;
using TrainTicketBookingSystem.Models;
using TrainTicketBookingSystem.ViewModels;
using TrainTicketBookingSystem.ViewModels.Train;

namespace TrainTicketBookingSystem.Controllers
{
    public class TicketController : Controller
    {
        private ApplicationDbContext db;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public TicketController()
        {
            db = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET /tickets
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());

            var tickets = db.TrainTickets
                            .Where(t => t.UserId.ToString() == currentUser.Id)
                            .ToList();

            return View(tickets);
        }

        // GET /ticket/purchase/{id}
        [HttpGet]
        public ActionResult Purchase(Guid id)
        {
            var train = db.Trains.Find(id);
            var viewModel = new AvailableTrainViewModel()
            {
                Id = train.Id,
                Route = train.Route,
                // query here
                BusinessClassPassengersCount = (db.TrainTickets
                                                    .Where(t => t.TrainId == train.Id)
                                                    .Where(t => t.IsConfirmed)
                                                    .Where(t => t.IsBusinessClass)
                                                    .Select(t => t.PassengersCount)
                                                    .DefaultIfEmpty()
                                                    .Sum()),
                EconomicClassPassengersCount = (db.TrainTickets
                                                    .Where(t => t.TrainId == train.Id)
                                                    .Where(t => t.IsConfirmed)
                                                    .Where(t => !t.IsBusinessClass)
                                                    .Select(t => t.PassengersCount)
                                                    .DefaultIfEmpty()
                                                    .Sum()),
                DepartureTime = train.DepartureTime
            };

            return View(viewModel);
        }

        // POST /ticket/purchase
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(PurchaseTicketViewModel ticket)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            var train = db.Trains.Find(ticket.Id);

            if (train == null)
            {
                ViewBag.Error = "No such train found. Try again with different criteria.";
                return View();
            }

            var viewModel = new AvailableTrainViewModel()
            {
                Id = train.Id,
                Route = train.Route,
                // query here
                BusinessClassPassengersCount = (db.TrainTickets
                                        .Where(t => t.TrainId == train.Id)
                                        .Where(t => t.IsConfirmed)
                                        .Where(t => t.IsBusinessClass)
                                        .Select(t => t.PassengersCount)
                                        .DefaultIfEmpty()
                                        .Sum()),
                EconomicClassPassengersCount = (db.TrainTickets
                                        .Where(t => t.TrainId == train.Id)
                                        .Where(t => t.IsConfirmed)
                                        .Where(t => !t.IsBusinessClass)
                                        .Select(t => t.PassengersCount)
                                        .DefaultIfEmpty()
                                        .Sum()),
                DepartureTime = train.DepartureTime
            };

            // extract method
            string errorMessage = ValidateAvailableSeats(ticket, viewModel);

            if (!"".Equals(errorMessage))
            {
                ViewBag.Error = errorMessage;
                return View(viewModel);
            }

            var generatedTicket = new TrainTicket()
            {
                UserId = new Guid(currentUser.Id),
                TrainId = train.Id,
                Departure = train.Route.Departure,
                Arrival = train.Route.Arrival,
                DepartureTime = train.DepartureTime,
                PurchasedOn = DateTime.Now,
                IsBusinessClass = ticket.TravellerClass == "business" ? true : false,
                PassengersCount = ticket.PassengersCount,
                Price = train.Route.Price * ticket.PassengersCount,
                IsConfirmed = false
            };

            generatedTicket.Price *= generatedTicket.IsBusinessClass ? 1.5m : 1.0m;
            db.TrainTickets.Add(generatedTicket);
            db.SaveChanges();

            // should be uncommented in production
            return SendPurchaseConfirmationEmail(generatedTicket.Id);

            // should be commented in production
            //ConfirmTicketPurchase(generatedTicket.Id);
        }

        // GET /ticket/cancel/{id}
        [HttpGet]
        [Authorize]
        public ActionResult Cancel(Guid id)
        {
            var ticketToCancel = db.TrainTickets.Find(id);

            if (ticketToCancel == null)
            {
                throw new NullReferenceException("Cannot find this ticket.");
            }

            db.TrainTickets.Remove(ticketToCancel);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        // GET /tickets/ConfirmTicketPurchase/{id}
        [HttpGet]
        [Authorize]
        public ActionResult ConfirmTicketPurchase(Guid id)
        {
            var ticket = db.TrainTickets.Find(id);

            if (ticket == null)
            {
                return new HttpStatusCodeResult(404);
            }

            if (ticket.IsConfirmed)
            {
                ViewBag.ConfirmationMessage = "This ticket has already been confirmed.";
                return View("PurchaseConfirmed");
            }

            // confirm the ticket and update it
            ticket.IsConfirmed = true;

            db.TrainTickets.Attach(ticket);
            var entry = db.Entry(ticket);
            entry.Property(e => e.IsConfirmed).IsModified = true;
            db.SaveChanges();

            ViewBag.ConfirmationMessage = "You successfully confirmed your purchase.";
            return View("PurchaseConfirmed");
        }

        public ActionResult SendPurchaseConfirmationEmail(Guid ticketId)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var websiteUrl = new UrlHelper(this.ControllerContext.RequestContext);
            var ticket = db.TrainTickets.Find(ticketId);

            if (ticket == null)
            {
                return new HttpStatusCodeResult(404);
            }

            MailAddress from = new MailAddress("admin@trainticketbookingsystem.com");
            MailAddress to = new MailAddress(user.Email);

            string username = ConfigurationManager.AppSettings["sendGridUser"];
            string password = ConfigurationManager.AppSettings["sendGridPassword"]; 

            SmtpClient mail = new SmtpClient()
            {
                Host = "smtp.sendgrid.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password),
                Timeout = 20000
            };

            const string websiteRoot = "http://localhost:50665";
            MailMessage msg = new MailMessage(from, to);
            msg.Subject = "Train ticket purchase confirmation.";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            
            msg.IsBodyHtml = true;
            msg.Body += $"Dear {user.FirstName}, you requested to purchase the following ticket: ";
            msg.Body += $"<p> {ticket.ToString()} </p>";
            msg.Body += $"<a href=\"{websiteRoot}{websiteUrl.Action("ConfirmTicketPurchase")}/{ticket.Id}\" >";
            msg.Body += "Click here to confirm.";
            msg.Body += "</a>";

            mail.Send(msg);

            return View("ConfirmationEmailSent");
        }

        private string ValidateAvailableSeats(PurchaseTicketViewModel ticket, AvailableTrainViewModel train)
        {
            if ("business".Equals(ticket.TravellerClass))
            {
                int difference = (train.BusinessClassPassengersCount + ticket.PassengersCount)
                    - AppConstants.TRAIN_CAPACITY_BUSINESS;

                if (difference > 0)
                {
                    int availableSeats = AppConstants.TRAIN_CAPACITY_BUSINESS - train.BusinessClassPassengersCount;
                    return availableSeats > 0 ? $"There are only { availableSeats } seats available." : "There are no seats available";
                }
            }

            if ("economic".Equals(ticket.TravellerClass))
            {
                int difference = (train.EconomicClassPassengersCount + ticket.PassengersCount)
                    - AppConstants.TRAIN_CAPACITY_ECONOMIC;

                if (difference > 0)
                {
                    int availableSeats = AppConstants.TRAIN_CAPACITY_ECONOMIC - train.EconomicClassPassengersCount;
                    return availableSeats > 0 ? $"There are only { availableSeats } seats available." : "There are no seats available";
                }
            }

            return string.Empty;
        }

        // AJAX
        // GET: /Ticket/IsCancellableWithoutFee/{id}
        [HttpGet]
        public JsonResult IsCancellableWithoutFee(Guid ticketId)
        {
            var ticket = db.TrainTickets.Find(ticketId);

            return Json(ticket.DepartureTime.Value.Subtract(DateTime.Now)
                   >= new TimeSpan(3, 0, 0, 0, 0) || !ticket.IsConfirmed, JsonRequestBehavior.AllowGet);
        }
    }
}
