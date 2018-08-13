using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public static class Scheduler
    {
        private const int AcceptableDistance = 15;

        public static Ride OrderRide(RideRequest rideRequest)
        {
            var closestVechicle = ClosestVechicle(rideRequest);
            var ride = new Ride(rideRequest, closestVechicle);
            
            Console.WriteLine($"Ride ordered, price: {ride.Price}");
            return ride;
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

        public static void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);

            var acceptedTaxi = TaxiRegister.AvailableTaxis.Find(taxi => taxi.Id == ride.Taxi.Id);
            acceptedTaxi.Location = ride.ToLocation;
            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Taxi.Name);
        }

        public static IEnumerable<Ride> GetRideList(int driver_id)
        {
            var rides = new List<Ride>();
            var ids = InMemoryRideDataBase.GetRide_Ids();
            foreach (var id in ids)
            {
                var ride = InMemoryRideDataBase.GetRide(id);
                if (ride.Taxi.Id == driver_id)
                    rides.Add(ride);
            }

            return rides;
        }
    }
}
