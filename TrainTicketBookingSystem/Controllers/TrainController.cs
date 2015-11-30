using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainTicketBookingSystem.Filters;
using TrainTicketBookingSystem.Models;
using TrainTicketBookingSystem.ViewModels;
using TrainTicketBookingSystem.ViewModels.Train;

namespace TrainTicketBookingSystem.Controllers
{
    public class TrainController : Controller
    {
        private TrainTicketsDbContext db = new TrainTicketsDbContext();

        // GET: trains/ 
        [HttpGet]
        [HasBanner]
        [InitializeSearchTrainsForm]
        public ActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }

        // GET: trains/search
        [HttpGet]
        [HasBanner]
        [InitializeSearchTrainsForm]
        public ActionResult Search(SearchTrainViewModel trainTicket)
        {
            ViewBag.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                DateTime departureTime = 
                    DateTime.Parse(trainTicket.DepartureTime)
                            .AddHours(trainTicket.DepartureTimeHour);

                if (departureTime < DateTime.Now)
                {
                    ModelState.AddModelError("AlreadyDeparted", "The time of departure for your search has already passed.");
                    return View(nameof(this.Index), trainTicket);
                }

                var trainRoute = db.TrainRoutes
                                             .Where(r => r.Arrival.Name == trainTicket.Arrival)
                                             .Where(r => r.Departure.Name == trainTicket.Departure)
                                             .SingleOrDefault();

                if (trainRoute == null)
                {
                    ModelState.AddModelError("InvalidRoute", "Departure and arrival cities should be different.");
                    return View(nameof(this.Index), trainTicket);
                }

                var trains = db.Trains
                                     .Where(t => t.DepartureTime >= departureTime)
                                     .Where(t => t.Route.Arrival.Name == trainRoute.Arrival.Name)
                                     .Where(t => t.Route.Departure.Name == trainRoute.Departure.Name)
                                     .OrderBy(t => t.DepartureTime)
                                     .Take(3)
                                     .Select(tr => new AvailableTrainViewModel()
                                     {
                                         Route = tr.Route,
                                         DepartureTime = tr.DepartureTime,
                                         Id = tr.Id,
                                         BusinessClassPassengersCount = (db.TrainTickets
                                                                              .Where(t => t.TrainId == tr.Id)
                                                                              .Where(t => t.IsConfirmed)
                                                                              .Where(t => t.IsBusinessClass)
                                                                              .Select(t => t.PassengersCount)
                                                                              .DefaultIfEmpty()
                                                                              .Sum()),
                                         EconomicClassPassengersCount = (db.TrainTickets
                                                                              .Where(t => t.TrainId == tr.Id)
                                                                              .Where(t => t.IsConfirmed)
                                                                              .Where(t => !t.IsBusinessClass)
                                                                              .Select(t => t.PassengersCount)
                                                                              .DefaultIfEmpty()
                                                                              .Sum())
                                     })
                                     .ToList();

                return View(trains);
            }

            return View();
        }

        // GET: trains/details/{id}
        [HttpGet]
        public ActionResult Details(Guid? id)
        {
            var train = db.Trains.Where(t => t.Id == id).SingleOrDefault();

            if (train == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var model = new AvailableTrainViewModel
            {
                Route = train.Route,
                DepartureTime = train.DepartureTime,
                BusinessClassPassengersCount = train.BusinessClassPassengersCount,
                EconomicClassPassengersCount = train.EconomicClassPassengersCount
            };

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}