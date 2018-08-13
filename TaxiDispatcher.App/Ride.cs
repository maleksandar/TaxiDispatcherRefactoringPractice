using System;

namespace TaxiDispatcher.App
{
    public class Ride
    {
        public int Id { get; set; }
        public RideRequest RideRequest { get; set; }
        public int FromLocation => RideRequest.FromLocation;
        public int ToLocation => RideRequest.ToLocation;

        public Taxi Taxi { get; set; }

        public int Price
        {
            get
            {
                var basePrice = Taxi.TaxiCompany.Price * Math.Abs(RideRequest.FromLocation - RideRequest.ToLocation);
                var areaBonus = RideRequest.Area == Area.InterCity ? 2 : 1;
                var timeOfDayBonus = (RideRequest.Time.Hour < 6 || RideRequest.Time.Hour > 22) ? 2 : 1;
                return basePrice * areaBonus * timeOfDayBonus;
            }
        }
    
    }
}