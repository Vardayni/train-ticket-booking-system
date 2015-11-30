namespace TrainTicketBookingSystem.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Utilities.Generators;

    internal sealed class Configuration : DbMigrationsConfiguration<TrainTicketsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TrainTicketsDbContext context)
        {
            var paris = new City { Name = "Paris", Id = Guid.Parse("280BB2E9-782A-42C9-875A-7DC0178452A6") };
            var brussels = new City { Name = "Brussels", Id = Guid.Parse("280BB2E9-782A-42C9-875A-7DC0178452A7") };
            var berlin = new City { Name = "Berlin", Id = Guid.Parse("280BB2E9-782A-42C9-875A-7DC0178452A8") };
            var amsterdam = new City { Name = "Amsterdam", Id = Guid.Parse("280BB2E9-782A-42C9-875A-7DC0178452A9") };
            var london = new City { Name = "London", Id = Guid.Parse("280BB2E9-782A-42C9-875A-7DC0178452AA") };

            var cities = new List<City>()
            {
                paris, brussels, berlin, amsterdam, london
            };

            if (context.Cities.Count() == 0)
            {
                context.Cities.AddOrUpdate(cities.ToArray());

                // TrainRoutes
                var trainRoutes = new List<TrainRoute>()
                {
                    new TrainRoute { Departure = paris, Arrival = brussels, Price = 20.0M },
                    new TrainRoute { Departure = paris, Arrival = amsterdam, Price = 30.0M },
                    new TrainRoute { Departure = paris, Arrival = berlin, Price = 50.0M },
                    new TrainRoute { Departure = paris, Arrival = london, Price = 40.0M },

                    new TrainRoute { Departure = brussels, Arrival = amsterdam, Price = 20.0M },
                    new TrainRoute { Departure = brussels, Arrival = berlin, Price = 40.0M },
                    new TrainRoute { Departure = brussels, Arrival = london, Price = 30.0M },
                    new TrainRoute { Departure = brussels, Arrival = paris, Price = 30.0M },

                    new TrainRoute { Departure = amsterdam, Arrival = london, Price = 30.0M },
                    new TrainRoute { Departure = amsterdam, Arrival = berlin, Price = 40.0M },
                    new TrainRoute { Departure = amsterdam, Arrival = paris, Price = 30.0M },
                    new TrainRoute { Departure = amsterdam, Arrival = brussels, Price = 20.0M },

                    new TrainRoute { Departure = london, Arrival = berlin, Price = 60.0M },
                    new TrainRoute { Departure = london, Arrival = paris, Price = 40.0M },
                    new TrainRoute { Departure = london, Arrival = amsterdam, Price = 30.0M },
                    new TrainRoute { Departure = london, Arrival = brussels, Price = 30.0M },

                    new TrainRoute { Departure = berlin, Arrival = london, Price = 60.0M },
                    new TrainRoute { Departure = berlin, Arrival = paris, Price = 50.0M },
                    new TrainRoute { Departure = berlin, Arrival = amsterdam, Price = 40.0M },
                    new TrainRoute { Departure = berlin, Arrival = brussels, Price = 40.0M },
               };

                context.TrainRoutes.AddOrUpdate(trainRoutes.ToArray());

                // TRAINS
                var dates = DateTimeGenerator.GetDaysInRange(DateTime.Today, DateTime.Today.AddDays(30));
                var trains = new List<Train>();

                foreach (DateTime date in dates)
                {
                    for (int i = 0; i < 24; i += 2)
                    {
                        foreach (var route in trainRoutes)
                        {
                            trains.Add(TrainGenerator.GenerateTrain(route, date.AddHours(i)));
                        }
                    }
                }

                context.Trains.AddRange(trains);
            }

            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;
            context.SaveChanges();
        }
    }
}

