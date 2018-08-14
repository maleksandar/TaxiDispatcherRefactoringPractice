﻿using System;
using System.Collections.Generic;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    internal static class Program
    {
        
        private static readonly IEnumerable<RideRequest> RideRequests = new[]
        {
            new RideRequest { FromLocation = 5, ToLocation = 0, Area = Area.City, Time = new DateTime(2018, 1, 1, 23, 0, 0) },
            new RideRequest { FromLocation = 0, ToLocation = 12, Area = Area.InterCity, Time = new DateTime(2018, 1, 1, 9, 0, 0) },
            new RideRequest { FromLocation = 5, ToLocation = 0, Area = Area.City, Time = new DateTime(2018, 1, 1, 11, 0, 0) },
            new RideRequest { FromLocation = 35, ToLocation = 12, Area = Area.City, Time = new DateTime(2018, 1, 1, 11, 0, 0) }
        };
        
        private static void Main(string[] args)
        {

            var scheduler = new Scheduler(new InMemoryRideDataBase(), new TaxiRegister());
            foreach (var request in RideRequests)
            {
                try
                {
                    Console.WriteLine($"Ordering ride from {request.FromLocation} to {request.ToLocation}...");
                    scheduler.OrderRide(request);
                    Console.WriteLine("");
                }
                catch (NoAvailableVehiclesException e)
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
