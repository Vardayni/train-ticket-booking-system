using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTicketBookingSystem.Models
{
    public class TrainTicket
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } = Guid.NewGuid();

        virtual public Guid UserId { get; set; }

        virtual public Guid TrainId { get; set; }

        virtual public City Departure { get; set; }

		virtual public City Arrival { get; set; }

        [DataType(DataType.Date)]
		public DateTime? DepartureTime { get; set; }

		public bool IsBusinessClass { get; set; }

		[Range(1, 10)]
		public int PassengersCount { get; set; }

        public DateTime PurchasedOn { get; set; } = DateTime.Now;

        [UIHint("Currency")]
        public decimal Price { get; set; }

        public bool IsConfirmed { get; set; }

        public override string ToString()
        {
            IFormatProvider formatProvider = new System.Globalization.CultureInfo("nl-BE");
            string travellerClass = IsBusinessClass ? "business" : "economic";
            return $"{Departure.Name} (DEPT) - {Arrival.Name} (ARR)" 
                    + $" on {DepartureTime.Value.ToString("dd.MM.yyyy hh:mm")}"
                    + $" ({travellerClass} class) for {PassengersCount} people."
                    + $" Final price: {Price.ToString("C", formatProvider)}.";
        }
    }
}