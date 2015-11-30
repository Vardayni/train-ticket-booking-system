using System.Web.Mvc;
using TrainTicketBookingSystem.Utilities.Generators;

namespace TrainTicketBookingSystem.Filters
{
    /// <summary>
    /// Adds a banner metadata to the ViewBag.
    /// </summary>
    public class HasBannerAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var banner = BannerGenerator.GetBanner();

            filterContext.Controller.ViewBag.BannerRedirectUrl = banner.RedirectUrl;
            filterContext.Controller.ViewBag.BannerImage = banner.ImageClass;
            filterContext.Controller.ViewBag.BannerHeading = banner.Heading;
            filterContext.Controller.ViewBag.BannerContent = banner.Content;

            base.OnResultExecuting(filterContext);
        }
    }
}