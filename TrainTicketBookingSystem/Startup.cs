using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainTicketBookingSystem.Startup))]
namespace TrainTicketBookingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
