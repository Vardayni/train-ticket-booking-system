using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainTicketBookingSystem.Helpers
{
    public static class AppConstants
    {
        public static decimal BUSINESS_CLASS_MULTIPLIER = 1.5M;

        public static int TRAIN_CAPACITY_BUSINESS = 10;

        public static int TRAIN_CAPACITY_ECONOMIC = 20;

        public static Dictionary<string, int> HOURS =
            new Dictionary<string, int> {
                { "00:00", 0 },
                { "01:00", 1 },
                { "02:00", 2 },
                { "03:00", 3 },
                { "04:00", 4 },
                { "05:00", 5 },
                { "06:00", 6 },
                { "07:00", 7 },
                { "08:00", 8 },
                { "09:00", 9 },
                { "10:00", 10 },
                { "11:00", 11 },
                { "12:00", 12 },
                { "13:00", 13 },
                { "14:00", 14 },
                { "15:00", 15 },
                { "16:00", 16 },
                { "17:00", 17 },
                { "18:00", 18 },
                { "19:00", 19 },
                { "20:00", 20 },
                { "21:00", 21 },
                { "22:00", 22 },
                { "23:00", 23 }
            };
    }
}