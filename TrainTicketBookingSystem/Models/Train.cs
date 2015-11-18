using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTicketBookingSystem.Models
{
    public class Train
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        virtual public TrainRoute Route { get; set; }

        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Business Class Passengers Count")]
        [Range(0, 10)]
        [NotMapped]
        public int BusinessClassPassengersCount { get; } = 0;

        [Display(Name = "Business Class Passengers Count")]
        [Range(0, 20)]
        [NotMapped]
        public int EconomicClassPassengersCount { get; } = 0;
    }
}