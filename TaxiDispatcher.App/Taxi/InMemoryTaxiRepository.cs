using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public class InMemoryTaxiRepository : ITaxiRepository
    {
        private IEnumerable<Taxi> Taxis { get; }

        public InMemoryTaxiRepository(IEnumerable<Taxi> taxis)
        {
            Taxis = taxis ?? throw new ArgumentNullException(nameof(taxis));
        }

        public Taxi VechicleClosestTo(int location, int acceptableDistance)
        {
            if (acceptableDistance <= 0) throw new ArgumentOutOfRangeException(nameof(acceptableDistance));
            if (Taxis.Count() == 0) throw new NoAvailableVehicleException();

            var minimalDistance = Taxis.Min(taxi => taxi.DistanceFrom(location));
            if (minimalDistance > acceptableDistance)
            {
                throw new NoAvailableVehicleException(acceptableDistance);
            }

            return Taxis.First(taxi => taxi.DistanceFrom(location) == minimalDistance);
        }
    }
}