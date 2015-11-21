using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainTicketBookingSystem.Helpers;
using TrainTicketBookingSystem.Models;
using TrainTicketBookingSystem.ViewModels;
using TrainTicketBookingSystem.ViewModels.Train;

namespace TrainTicketBookingSystem.Controllers
{
    public class TrainController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: trains/ 
        public ActionResult Index()
        {
            InitializeCitiesAndHoursDropDownLists();

            ViewBag.Errors = new List<string>();
            return View();
        }

        // GET: trains/search
        [HttpGet]
        public ActionResult Search(SearchTrainTicketViewModel trainTicket)
        {
            ViewBag.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                DateTime departureTime = DateTime.Parse(trainTicket.DepartureTime)
                                            .AddHours(trainTicket.DepartureTimeHour);

                if (departureTime < DateTime.Now)
                {
                    ModelState.AddModelError("AlreadyDeparted", "The time of departure for your search has already passed.");
                    InitializeCitiesAndHoursDropDownLists();
                    return View(nameof(this.Index), trainTicket);
                }

                var trainRoute = db.TrainRoutes
                                             .Where(r => r.Arrival.Name == trainTicket.Arrival)
                                             .Where(r => r.Departure.Name == trainTicket.Departure)
                                             .SingleOrDefault();

                if (trainRoute == null)
                {
                    ModelState.AddModelError("InvalidRoute", "Departure and arrival cities should be different.");
                    InitializeCitiesAndHoursDropDownLists();
                    return View(nameof(this.Index), trainTicket);
                }

                var trains = db.Trains
                                     .Where(t => t.DepartureTime == departureTime)
                                     .Where(t => t.Route.Arrival.Name == trainRoute.Arrival.Name)
                                     .Where(t => t.Route.Departure.Name == trainRoute.Departure.Name)
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

        private void InitializeCitiesAndHoursDropDownLists()
        {
            var cities = db.Cities.ToList();
            var hours = AppConstants.HOURS;

            ViewBag.Hours = hours;
            ViewBag.Cities = cities;
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