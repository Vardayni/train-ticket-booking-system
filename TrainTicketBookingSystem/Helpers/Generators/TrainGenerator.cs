using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainTicketBookingSystem.Models;

namespace TrainTicketBookingSystem.Helpers.Generators
{
    public class TrainGenerator
    {
        public static Train GenerateTrain(TrainRoute route, DateTime departureTime)
        {
            return new Train()
            {
                Route = route,
                DepartureTime = departureTime,
            };
        }
    }
}