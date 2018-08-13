using System;
using System.Collections.Generic;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var requests = new List<RideRequest>
            {
                new RideRequest { FromLocation = 5, ToLocation = 0, Area = Area.City, Time = new DateTime(2018, 1, 1, 23, 0, 0) },
                new RideRequest { FromLocation = 0, ToLocation = 12, Area = Area.InterCity, Time = new DateTime(2018, 1, 1, 9, 0, 0) },
                new RideRequest { FromLocation = 5, ToLocation = 0, Area = Area.City, Time = new DateTime(2018, 1, 1, 11, 0, 0) },
                new RideRequest { FromLocation = 35, ToLocation = 12, Area = Area.City, Time = new DateTime(2018, 1, 1, 11, 0, 0) }
            };

            foreach (var request in requests)
            {
                try
                {
                    Console.WriteLine($"Ordering ride from {request.FromLocation} to {request.ToLocation}...");
                    Scheduler.OrderRide(request);
                    Console.WriteLine("");
                }
                catch (Exception e)
                {
                    if (e.Message == "There are no available taxi vehicles!")
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("");
                    }
                    else
                        throw;
                }
                
            }

            Scheduler.PrintStatsForDriver(2);
        }
    }
}
