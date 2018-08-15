using System;

namespace TaxiDispatcher.App
{
    public class Scheduler
    {
        public int AcceptableDistance { get; }

        private readonly IRideRepository _rideRepository;
        private readonly ITaxiRepository _taxiRepository;
        private readonly ILogger _logger;

        public Scheduler(IRideRepository rideRepository, ITaxiRepository taxiRepository, ILogger logger, int acceptableDistance = 15)
        {
            _rideRepository = rideRepository ?? throw new ArgumentNullException(nameof(rideRepository));
            _taxiRepository = taxiRepository ?? throw new ArgumentNullException(nameof(taxiRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            AcceptableDistance = acceptableDistance;
        }

        public void OrderRide(RideRequest rideRequest)
        {
            if (rideRequest == null) throw new ArgumentNullException(nameof(rideRequest));

            _logger.Log($"Ordering ride from {rideRequest.FromLocation} to {rideRequest.ToLocation}...");

            var closestVechicle = _taxiRepository.VechicleClosestTo(rideRequest.FromLocation, AcceptableDistance);
            var ride = new Ride(rideRequest, closestVechicle);

            _logger.Log($"Ride ordered, price: {ride.Price}");
            
            AcceptRide(ride);
        }
        
        public void PrintStatsForDriver(int driverId)
        {
            if (driverId <= 0) throw new ArgumentOutOfRangeException(nameof(driverId));

            foreach (var ride in _rideRepository.GetDriversRidingList(driverId))
            {
                _logger.Log($"Price: {ride.Price}");
            }

            var totalEarnings = _rideRepository.GetTotalEarningsForDriver(driverId);

            _logger.Log($"Driver with ID = {driverId} earned today:");
            _logger.Log($"Total: {totalEarnings}");
        }

        private void AcceptRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));

            _rideRepository.SaveRide(ride);

            ride.Taxi.DriveTo(ride.RideRequest.ToLocation);

            _logger.Log($"Ride accepted, waiting for driver: {ride.Taxi.Name}");
            _logger.Log("");
        }
    }
}
