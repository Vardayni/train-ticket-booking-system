using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainTicketBookingSystem.Models
{
	public class TrainRoute
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Departure")]
        virtual public City Departure { get; set; }

        [Display(Name = "Arrival")]
        virtual public City Arrival { get; set; }

        [UIHint("Currency")]
		public decimal Price { get; set; }
	}
}