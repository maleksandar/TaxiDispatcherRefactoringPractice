using System;

namespace TaxiDispatcher.App
{
    public class RideRequest
    {
        public int FromLocation { get; set; }

        public int ToLocation { get; set; }

        public Area Area { get; set; }

        public DateTime Time { get; set; }
    }
}