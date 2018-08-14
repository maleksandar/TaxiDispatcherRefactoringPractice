using System;
using System.Linq;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        private const int AcceptableDistance = 15;

        private readonly IRideRepository _rideRepository;
        private readonly ITaxiRepository _taxiRepository;

        public Scheduler(IRideRepository rideRepository, ITaxiRepository taxiRepository)
        {
            _rideRepository = rideRepository;
            _taxiRepository = taxiRepository;
        }

        public void OrderRide(RideRequest rideRequest)
        {
            var closestVechicle = ClosestVechicle(rideRequest);
            var ride = new Ride(rideRequest, closestVechicle);
            Console.WriteLine($"Ride ordered, price: {ride.Price}");
            
            AcceptRide(ride);
        }
        
        public void PrintStatsForDriver(int driverId)
        {
            Console.WriteLine($"Driver with ID = {driverId} earned today:");
            var total = 0;
            foreach (var ride in _rideRepository.GetDriversRidingList(driverId))
            {
                total += ride.Price;
                Console.WriteLine("Price: " + ride.Price);
            }
            Console.WriteLine("Total: " + total);
        }

        private Taxi ClosestVechicle(RideRequest rideRequest)
        {
            return _taxiRepository.ClosestToLocation(rideRequest.FromLocation, AcceptableDistance);
        }

        private void AcceptRide(Ride ride)
        {
            _rideRepository.SaveRide(ride);

            var acceptedTaxi = _taxiRepository.GetById(ride.Taxi.Id);
            acceptedTaxi.Location = ride.ToLocation;
            
            Console.WriteLine("Ride accepted, waiting for driver: " + ride.Taxi.Name);
        }
    }
}
