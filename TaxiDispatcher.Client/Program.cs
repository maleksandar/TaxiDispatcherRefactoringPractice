using System;
using System.Collections.Generic;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    internal static class Program
    {
        private static readonly IEnumerable<Taxi> Taxis = new[]
        {
            new Taxi {Id = 1, Name = "Predrag", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 1},
            new Taxi {Id = 2, Name = "Nenad", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 4},
            new Taxi {Id = 3, Name = "Dragan", TaxiCompany = TaxiCompanyRegister.Alfa, Location = 6},
            new Taxi {Id = 4, Name = "Goran", TaxiCompany = TaxiCompanyRegister.Gold, Location = 7}
        };

        private static readonly IEnumerable<RideRequest> RideRequests = new[]
        {
            new RideRequest (5, 0, Area.City, new DateTime(2018, 1, 1, 23, 0, 0)),
            new RideRequest (0, 12, Area.InterCity, new DateTime(2018, 1, 1, 9, 0, 0)),
            new RideRequest (5, 0, Area.City, new DateTime(2018, 1, 1, 11, 0, 0)),
            new RideRequest (35, 12, Area.City, new DateTime(2018, 1, 1, 11, 0, 0))
        };

        private static void Main()
        {
            var scheduler = new Scheduler(new InMemoryRideRepository(), new InMemoryTaxiRepository(Taxis), Console.WriteLine);

            foreach (var request in RideRequests)
            {
                try
                {
                    scheduler.OrderRide(request);
                }
                catch (NoAvailableVehicleException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("");
                }
            }

            scheduler.PrintStatsForDriver(2);
            Console.ReadLine();
        }
    }
}
