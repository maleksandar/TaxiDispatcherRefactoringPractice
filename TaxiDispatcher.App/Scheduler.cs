using System;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        private const int AcceptableDistance = 15;

        private readonly IRideRepository _rideRepository;
        private readonly ITaxiRepository _taxiRepository;
        public delegate void Logger(string message);

        private readonly Logger _log;

        public Scheduler(IRideRepository rideRepository, ITaxiRepository taxiRepository, Logger loger)
        {
            _rideRepository = rideRepository ?? throw new ArgumentNullException(nameof(rideRepository));
            _taxiRepository = taxiRepository ?? throw new ArgumentNullException(nameof(taxiRepository));
            _log = loger ?? throw new ArgumentNullException(nameof(loger));
        }

        public void OrderRide(RideRequest rideRequest)
        {
            _log($"Ordering ride from {rideRequest.FromLocation} to {rideRequest.ToLocation}...");

            if (rideRequest == null) throw new ArgumentNullException(nameof(rideRequest));

            var closestVechicle = _taxiRepository.VechicleClosestTo(rideRequest.FromLocation, AcceptableDistance);
            var ride = new Ride(rideRequest, closestVechicle);
            
            _log($"Ride ordered, price: {ride.Price}");
            
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

            _log($"Driver with ID = {driverId} earned today:");
            _log($"Total: {totalEarnings}");
        }

        private void AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));

            _rideRepository.SaveRide(ride);

            var acceptedTaxi = _taxiRepository.GetById(ride.Taxi.Id);
            acceptedTaxi.Location = ride.RideRequest.ToLocation;
            
            _log($"Ride accepted, waiting for driver: {acceptedTaxi.Name}");
            _log("");
        }
    }
}
