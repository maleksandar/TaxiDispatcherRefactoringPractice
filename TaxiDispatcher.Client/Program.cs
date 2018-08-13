using System;
using TaxiDispatcher.App;

namespace TaxiDispatcher.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler scheduler = new Scheduler();

            try
            {
                Console.WriteLine("Ordering ride from 5 to 0...");
                Scheduler.Ride ride = scheduler.OrderRide(new RideRequest { FromLocation = 5, ToLocation = 0, Area = Constants.City, Time = new DateTime(2018, 1, 1, 23, 0, 0) } );
                scheduler.AcceptRide(ride);
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

            try
            {
                Console.WriteLine("Ordering ride from 0 to 12...");
                Scheduler.Ride ride = scheduler.OrderRide(new RideRequest { FromLocation = 0, ToLocation = 12, Area = Constants.InterCity, Time = new DateTime(2018, 1, 1, 9, 0, 0) });
                scheduler.AcceptRide(ride);
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

            try
            {
                Console.WriteLine("Ordering ride from 5 to 0...");
                Scheduler.Ride ride = scheduler.OrderRide(new RideRequest { FromLocation = 5, ToLocation = 0, Area = Constants.City, Time = new DateTime(2018, 1, 1, 11, 0, 0)});
                scheduler.AcceptRide(ride);
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

            try
            {
                Console.WriteLine("Ordering ride from 35 to 12...");
                Scheduler.Ride ride = scheduler.OrderRide(new RideRequest { FromLocation = 35, ToLocation = 12, Area = Constants.City, Time = new DateTime(2018, 1, 1, 11, 0, 0)});
                scheduler.AcceptRide(ride);
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

            Console.WriteLine("Driver with ID = 2 earned today:");
            int total = 0;
            foreach (Scheduler.Ride r in scheduler.GetRideList(2))
            {
                total += r.Price;
                Console.WriteLine("Price: " + r.Price);
            }
            Console.WriteLine("Total: " + total);

            Console.ReadLine();
        }
    }
}
