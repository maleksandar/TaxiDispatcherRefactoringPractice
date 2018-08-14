using System;

namespace TaxiDispatcher.App
{
    public class Ride
    {
        private const int InterCityBonus = 2;
        private const int NightTimeBonus = 2;

        public Ride(RideRequest rideRequest, Taxi taxi)
        {
            if (rideRequest == null) throw new ArgumentNullException(nameof(rideRequest));
            if (taxi == null) throw new ArgumentNullException(nameof(taxi));

            RideRequest = rideRequest;
            Taxi = taxi;
        }

        public RideRequest RideRequest { get; }
        public Taxi Taxi { get; }

        public int Price
        {
            get
            {
                var basePrice = Taxi.TaxiCompany.Price * Math.Abs(RideRequest.FromLocation - RideRequest.ToLocation);
                return basePrice * CalculateAreaBonus * CalculateTimeBonus;
            }
        }

        private int CalculateTimeBonus => (RideRequest.Time.Hour < 6 || RideRequest.Time.Hour > 22) ? NightTimeBonus : 1;

        private int CalculateAreaBonus => RideRequest.Area == Area.InterCity ? InterCityBonus : 1;
    }
}