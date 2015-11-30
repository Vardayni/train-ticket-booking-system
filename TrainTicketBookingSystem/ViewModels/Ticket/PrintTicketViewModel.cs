using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainTicketBookingSystem.ViewModels.Ticket
{
    public class PrintTicketViewModel
    {
        public Guid Id
        { get; set; } = Guid.NewGuid();

        public string CustomerName
        { get; set; }

        public string Departure
        { get; set; }

        public string Arrival
        { get; set; }

        [Display(Name = "Date")]
        public DateTime? DepartureTime
        { get; set; }

        [UIHint("PassengerClass")]
        public bool IsBusinessClass
        { get; set; }

        [Range(1, 10)]
        public int PassengersCount
        { get; set; }

        public DateTime PurchasedOn
        { get; set; } = DateTime.Now;

        [UIHint("Currency")]
        public decimal Price
        { get; set; }
    }
}