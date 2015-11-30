using System.Linq;
using System.Web.Mvc;
using TrainTicketBookingSystem.Models;
using TrainTicketBookingSystem.Utilities.Constants;
using Newtonsoft.Json;

namespace TrainTicketBookingSystem.Filters
{
    public class InitializeSearchTrainsFormAttribute : ActionFilterAttribute
    {
        private TrainTicketsDbContext db = TrainTicketsDbContext.Create();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var cities = db.Cities.Distinct().ToList();
            var hours = AppConstants.HOURS;

            filterContext.Controller.ViewBag.Hours = hours;
            filterContext.Controller.ViewBag.Cities = cities;
            filterContext.Controller.ViewBag.CitiesJson = JsonConvert.SerializeObject(
                cities.Select(c => c.Name).ToArray());

            base.OnResultExecuting(filterContext);
        }
    }
}