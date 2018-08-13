using System.Collections.Generic;
using System.Linq;
using static TaxiDispatcher.App.Scheduler;

namespace TaxiDispatcher.App
{
    public static class InMemoryRideDataBase
    {
        private static readonly List<Ride> Rides = new List<Ride>();

        public static void SaveRide(Ride ride)
        {
            Rides.Add(ride);
        }

        public static IEnumerable<Ride> GetDriversRidingList(int driveriId)
        {
            return Rides.Where(ride => ride.Taxi.Id == driveriId);
        }
    }
}
