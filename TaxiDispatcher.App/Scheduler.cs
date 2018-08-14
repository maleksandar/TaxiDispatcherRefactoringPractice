using System;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        private const int AcceptableDistance = 15;

        private readonly IRideRepository _rideRepository;
        private readonly ITaxiRepository _taxiRepository;

        public Scheduler(IRideRepository rideRepository, ITaxiRepository taxiRepository)
        {
            if (rideRepository == null) throw new ArgumentNullException(nameof(rideRepository));
            if (taxiRepository == null) throw new ArgumentNullException(nameof(taxiRepository));

            _rideRepository = rideRepository;
            _taxiRepository = taxiRepository;
        }

        public void OrderRide(RideRequest rideRequest)
        {
            Console.WriteLine($"Ordering ride from {rideRequest.FromLocation} to {rideRequest.ToLocation}...");

            if (rideRequest == null) throw new ArgumentNullException(nameof(rideRequest));

            var closestVechicle = _taxiRepository.VechicleClosestTo(rideRequest.FromLocation, AcceptableDistance);
            var ride = new Ride(rideRequest, closestVechicle);
            
            Console.WriteLine($"Ride ordered, price: {ride.Price}");
            
            AcceptRide(ride);
        }
        
        public void PrintStatsForDriver(int driverId)
        {
            if (driverId <= 0) throw new ArgumentOutOfRangeException(nameof(driverId));

            var totalEarnings = 0;
            foreach (var ride in _rideRepository.GetDriversRidingList(driverId))
            {
                totalEarnings += ride.Price;
                Console.WriteLine($"Price: {ride.Price}");
            }

            Console.WriteLine($"Driver with ID = {driverId} earned today:");
            Console.WriteLine($"Total: {totalEarnings}");
        }

        private void AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));

            _rideRepository.SaveRide(ride);

            var acceptedTaxi = _taxiRepository.GetById(ride.Taxi.Id);
            acceptedTaxi.Location = ride.RideRequest.ToLocation;
            
            Console.WriteLine($"Ride accepted, waiting for driver: {acceptedTaxi.Name}");
            Console.WriteLine("");
        }
    }
}
