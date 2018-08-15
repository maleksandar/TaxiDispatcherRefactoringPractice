using System.Collections.Generic;

namespace TaxiDispatcher.App
{
    public interface IRideRepository
    {
        void SaveRide(Ride ride);
        
        IEnumerable<Ride> GetDriversRidingList(int driveriId);

        int GetTotalEarningsForDriver(int driverId);
    }
}