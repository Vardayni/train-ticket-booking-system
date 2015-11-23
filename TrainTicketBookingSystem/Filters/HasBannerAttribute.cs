using System.Web.Mvc;

namespace TrainTicketBookingSystem.Filters
{
    /// <summary>
    /// Adds a banner metadata to the ViewBag.
    /// TODO: Choose a random city to advertise for each request.
    /// </summary>
    public class HasBannerAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.BannerImage = "amsterdam";
            filterContext.Controller.ViewBag.BannerHeading = "Visit Amsterdam";
            filterContext.Controller.ViewBag.BannerContent =
                @"From its humble beginnings as a 13th-century fishing village on a river 
                bed to its current role as a major hub for business, tourism and culture,
                Amsterdam has had a strong tradition as a centre of culture and commerce.";

            base.OnResultExecuting(filterContext);
        }
    }
}