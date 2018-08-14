using System;
using System.Collections.Generic;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    internal static class Program
    {
        private static readonly IEnumerable<RideRequest> RideRequests = new[]
        {
            new RideRequest (5, 0, Area.City, new DateTime(2018, 1, 1, 23, 0, 0)),
            new RideRequest (0, 12, Area.InterCity, new DateTime(2018, 1, 1, 9, 0, 0)),
            new RideRequest (5, 0, Area.City, new DateTime(2018, 1, 1, 11, 0, 0)),
            new RideRequest (35, 12, Area.City, new DateTime(2018, 1, 1, 11, 0, 0))
        };

        private static void Main()
        {
            var scheduler = new Scheduler(new InMemoryRideRepository(), new InMemoryTaxiRepository(), Console.WriteLine);

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
