using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrainTicketBookingSystem.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("SQLAZURECONNSTR", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<TrainTicket> TrainTickets { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<TrainRoute> TrainRoutes { get; set; }

        public DbSet<Train> Trains { get; set; }
    }
}