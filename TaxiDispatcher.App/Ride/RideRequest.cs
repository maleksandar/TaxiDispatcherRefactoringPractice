using System;

namespace TaxiDispatcher.App
{
    public class RideRequest
    {
        public RideRequest(int fromLocation, int toLocation, Area area, DateTime time)
        {
            FromLocation = fromLocation;
            ToLocation = toLocation;
            Area = area;
            Time = time;
        }

        public int FromLocation { get; }

        public int ToLocation { get; }

        public Area Area { get; }

        public DateTime Time { get; }
    }
}