using System;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketBookingSystem.ViewModels
{
    public class SearchTrainViewModel
    {
        [Display(Name = "Departure")]
        [Required]
        public string Departure { get; set; }

        [Display(Name = "Arrival")]
        [Required]
        public string Arrival { get; set; }

        [Display(Name = "Departure time")]
        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public int DepartureTimeHour { get; set; }
    }
}