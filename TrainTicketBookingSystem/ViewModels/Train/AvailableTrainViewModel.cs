using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainTicketBookingSystem.Models;

namespace TrainTicketBookingSystem.ViewModels.Train
{
    public class AvailableTrainViewModel 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        virtual public TrainRoute Route { get; set; }

        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Business")]
        [Range(0, 10)]
        public int BusinessClassPassengersCount { get; set; } = 0;

        [Display(Name = "Economic")]
        [Range(0, 20)]
        public int EconomicClassPassengersCount { get; set; } = 0;

        public static int BusinessClassCapacity { get; } = 10;

        public static int EconomicClassCapacity { get; } = 20;
    }
}