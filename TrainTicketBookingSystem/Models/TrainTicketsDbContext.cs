using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrainTicketBookingSystem.Models
{
    public class TrainTicketsDbContext : IdentityDbContext<ApplicationUser>
    {
        public TrainTicketsDbContext()
            : base("MSSQL", throwIfV1Schema: false)
        {
        }

        public static TrainTicketsDbContext Create()
        {
            return new TrainTicketsDbContext();
        }

        // Entities
        public DbSet<TrainTicket> TrainTickets { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<TrainRoute> TrainRoutes { get; set; }

        public DbSet<Train> Trains { get; set; }
    }
}