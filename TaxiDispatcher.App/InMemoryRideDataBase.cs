using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public class InMemoryRideDataBase : IRideRepository
    {
        private static readonly List<Ride> Rides = new List<Ride>();

        public void SaveRide(Ride ride)
        {
            Rides.Add(ride);
        }

        public IEnumerable<Ride> GetDriversRidingList(int driveriId)
        {
            return Rides.Where(ride => ride.Taxi.Id == driveriId);
        }
    }
}
