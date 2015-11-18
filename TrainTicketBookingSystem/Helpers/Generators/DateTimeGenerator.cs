using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainTicketBookingSystem.Helpers.Generators
{
    public class DateTimeGenerator
    {
        public static IList<DateTime> GetDaysInRange(DateTime start, DateTime end)
        {
            return Enumerable.Range(0, 1 + end.Subtract(start).Days)
                             .Select(offset => start.AddDays(offset))
                             .ToArray();
        }
    }
}