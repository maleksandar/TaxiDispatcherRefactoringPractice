using System;

namespace TaxiDispatcher.App
{
    public class Ride
    {
        public Ride(RideRequest rideRequest, Taxi taxi)
        {
            FromLocation = rideRequest.FromLocation;
            ToLocation = rideRequest.ToLocation;
            Area = rideRequest.Area;
            OrderTime = rideRequest.Time;

            Taxi = taxi;
        }

        public int ToLocation { get; }

        private int FromLocation { get;  }
        private Area Area { get; }
        private DateTime OrderTime { get; }
        public Taxi Taxi { get; }

        public int Price
        {
            get
            {
                var basePrice = Taxi.TaxiCompany.Price * Math.Abs(FromLocation - ToLocation);
                var areaBonus = Area == Area.InterCity ? 2 : 1;
                var timeOfDayBonus = (OrderTime.Hour < 6 || OrderTime.Hour > 22) ? 2 : 1;
                return basePrice * areaBonus * timeOfDayBonus;
            }
        }
    
    }
}