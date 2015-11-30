using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainTicketBookingSystem.Models;
using TrainTicketBookingSystem.Utilities.Constants;

namespace TrainTicketBookingSystem.ViewModels.Train
{
    public class AvailableTrainViewModel
    {
        public static int BusinessClassCapacity
        { get; } = AppConstants.TRAIN_CAPACITY_BUSINESS;

        public static int EconomicClassCapacity
        { get; } = AppConstants.TRAIN_CAPACITY_ECONOMIC;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        { get; set; } = Guid.NewGuid();

        virtual public TrainRoute Route
        { get; set; }

        [Display(Name = "Departure Time")]
        public DateTime DepartureTime
        { get; set; }

        [Display(Name = "Business")]
        [Range(0, AppConstants.TRAIN_CAPACITY_BUSINESS)]
        public int BusinessClassPassengersCount
        { get; set; } = 0;

        [Display(Name = "Economic")]
        [Range(0, AppConstants.TRAIN_CAPACITY_ECONOMIC)]
        public int EconomicClassPassengersCount
        { get; set; } = 0;
    }
}