using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainTicketBookingSystem.Helpers.Generators
{
    public static class BannerGenerator
    {
        private static Random rand = new Random();

        public static Banner GetBanner()
        {
            int min = 0;
            int max = AppConstants.BANNERS.Count;

            return AppConstants.BANNERS[rand.Next(min, max)];
        }
    }
}