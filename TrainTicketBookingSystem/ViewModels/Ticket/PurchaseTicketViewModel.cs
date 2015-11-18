using System;
using System.ComponentModel.DataAnnotations;

namespace TrainTicketBookingSystem.ViewModels
{
    public class PurchaseTicketViewModel
    {
        public Guid Id { get; set; }

        public int PassengersCount { get; set; }

        public string TravellerClass { get; set; }
    }
}