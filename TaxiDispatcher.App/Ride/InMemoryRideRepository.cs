using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public class InMemoryRideRepository : IRideRepository
    {
        private static readonly List<Ride> Rides = new List<Ride>();

        public void SaveRide(Ride ride)
        {
            if (ride == null) throw new ArgumentNullException(nameof(ride));
            
            Rides.Add(ride);
        }

        public IEnumerable<Ride> GetDriversRidingList(int driveriId)
        {
            if (driveriId <= 0) throw new ArgumentOutOfRangeException(nameof(driveriId));
            
            return Rides.Where(ride => ride.Taxi.Id == driveriId);
        }

        public int GetTotalEarningsForDriver(int driverId)
        {
            return GetDriversRidingList(driverId).Sum(ride => ride.Price);
        }
    }
}
