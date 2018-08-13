using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public static class Scheduler
    {
        private static TaxiCompany naxi = new TaxiCompany {Name = "Naxi", Price = 10};
        private static TaxiCompany alfa = new TaxiCompany {Name = "Alfa", Price = 15};
        private static TaxiCompany gold = new TaxiCompany {Name = "Gold", Price = 13};

        private static Taxi taxi1 = new Taxi { Id = 1, Name = "Predrag", TaxiCompany = naxi, Location = 1};
        private static Taxi taxi2 = new Taxi { Id = 2, Name = "Nenad", TaxiCompany = naxi, Location = 4 };
        private static Taxi taxi3 = new Taxi { Id = 3, Name = "Dragan", TaxiCompany = alfa, Location = 6 };
        private static Taxi taxi4 = new Taxi { Id = 4, Name = "Goran", TaxiCompany = gold, Location = 7 };

        private static List<Taxi> availableTaxis = new List<Taxi> {taxi1, taxi2, taxi3, taxi4};

        public static Ride OrderRide(RideRequest rideRequest)
        {
            var minimalDistance = availableTaxis.Min(x => Math.Abs(x.Location - rideRequest.FromLocation));
            var bestVehicle = availableTaxis.Find(taxi => Math.Abs(taxi.Location - rideRequest.FromLocation) == minimalDistance);

            if (minimalDistance > 15)
            {
                throw new Exception("There are no available taxi vehicles!");
            }


            Ride ride = new Ride();
            ride.Taxi = bestVehicle;
            ride.RideRequest = rideRequest;
            
            Console.WriteLine($"Ride ordered, price: {ride.Price}");
            return ride;
        }

        public static void AcceptRide(Ride ride)
        {
            InMemoryRideDataBase.SaveRide(ride);

            var acceptedTaxi = availableTaxis.Find(taxi => taxi.Id == ride.Taxi.Id);
            acceptedTaxi.Location = ride.RideRequest.ToLocation;
            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Taxi.Name);
        }

        public static List<Ride> GetRideList(int driver_id)
        {
            List<Ride> rides = new List<Ride>();
            List<int> ids = InMemoryRideDataBase.GetRide_Ids();
            foreach (int id in ids)
            {
                Ride ride = InMemoryRideDataBase.GetRide(id);
                if (ride.Taxi.Id == driver_id)
                    rides.Add(ride);
            }

            return rides;
        }
    }
}
