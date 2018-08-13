using System;
using System.Collections.Generic;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    class Program
    {
        static void Main(string[] args)
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
                    Ride ride = Scheduler.OrderRide(request);
                    Scheduler.AcceptRide(ride);
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

            Console.WriteLine("Driver with ID = 2 earned today:");
            int total = 0;
            foreach (Ride r in Scheduler.GetRideList(2))
            {
                total += r.Price;
                Console.WriteLine("Price: " + r.Price);
            }
            Console.WriteLine("Total: " + total);

            Console.ReadLine();
        }
    }
}
