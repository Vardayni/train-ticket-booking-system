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

        public static int MAX_DAYS_BEFORE_RESERVATION = 13;

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

        public static IList<Banner> BANNERS = new List<Banner>
        {
                new Banner()
                {
                    ImageClass = "amsterdam",
                    Heading = "Visit Amsterdam",
                    Content = @"From its humble beginnings as a 13th-century fishing village on a river 
                                bed to its current role as a major hub for business, tourism and culture,
                                Amsterdam has had a strong tradition as a centre of culture and commerce.",
                    RedirectUrl = new Uri("http://www.iamsterdam.com/en/visiting")
                },
                new Banner()
                {
                    ImageClass = "berlin",
                    Heading = "Visit Berlin",
                    Content = @"Since reunification, Berlin has established itself as an internationally
                                attractive city bustling with creativity and open to the world.
                                The FIFA World Cup Finals in 2006 provided for a markedly improved
                                image of Germany and Berlin.",
                    RedirectUrl = new Uri("http://www.visitberlin.de/en")
                },
                new Banner()
                {
                    ImageClass = "brussels",
                    Heading = "Visit Brussels",
                    Content = @"Things happen in Brussels! There’s always a buzz. 
                                Not just because it’s the capital of 500 million Europeans
                                and political decisions are made here, but also because Brussels
                                offers you an exciting cultural, artistic and social scene.",
                    RedirectUrl = new Uri("http://visitbrussels.be/")
                },
                new Banner()
                {
                    ImageClass = "london",
                    Heading = "Visit London",
                    Content = @"Come and explore all London has to offer 
                                and get ready for the adventure of a lifetime. 
                                The great city of London awaits you.",
                    RedirectUrl = new Uri("http://www.visitlondon.com/")
                },
                new Banner()
                {
                    ImageClass = "paris",
                    Heading = "Visit Paris",
                    Content = @"If you love history, art and good wine,
                                Paris is the place for you.",
                    RedirectUrl = new Uri("http://en.parisinfo.com/")
                }
        };
    }
}