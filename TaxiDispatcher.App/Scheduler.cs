﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public static class Scheduler
    {
        private const int AcceptableDistance = 15;

        public static void OrderRide(RideRequest rideRequest)
        {
            var closestVechicle = ClosestVechicle(rideRequest);
            var ride = new Ride(rideRequest, closestVechicle);
            Console.WriteLine($"Ride ordered, price: {ride.Price}");
            
            AcceptRide(ride);
        }

        private static Taxi ClosestVechicle(RideRequest rideRequest)
        {
            var minimalDistance = TaxiRegister.AvailableTaxis.Min(x => Math.Abs(x.Location - rideRequest.FromLocation));
            if (minimalDistance > AcceptableDistance)
            {
                throw new Exception("There are no available taxi vehicles!");
            }

            return TaxiRegister.AvailableTaxis.Find(taxi => Math.Abs(taxi.Location - rideRequest.FromLocation) == minimalDistance);
        }

        private static void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);

            var acceptedTaxi = TaxiRegister.AvailableTaxis.Find(taxi => taxi.Id == ride.Taxi.Id);
            acceptedTaxi.Location = ride.ToLocation;
            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Taxi.Name);
        }

        public static IEnumerable<Ride> GetRideList(int driveriId)
        {
            return InMemoryRideDataBase.Rides.Where(ride => ride.Taxi.Id == driveriId);
        }

        public static void PrintStatsForDriver(int driverId)
        {
            Console.WriteLine($"Driver with ID = {driverId} earned today:");
            var total = 0;
            foreach (var r in GetRideList(driverId))
            {
                total += r.Price;
                Console.WriteLine("Price: " + r.Price);
            }
            Console.WriteLine("Total: " + total);

            Console.ReadLine();
        }
    }
}
