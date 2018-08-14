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
            Taxis = taxis;
        }

        public Taxi VechicleClosestTo(int location, int acceptableDistance)
        {
            if (acceptableDistance <= 0) throw new ArgumentOutOfRangeException(nameof(acceptableDistance));

            var minimalDistance = Taxis.Min(taxi => taxi.DistanceFrom(location));
            if (minimalDistance > acceptableDistance)
            {
                throw new NoAvailableVehicleException();
            }

            return Taxis.First(taxi => taxi.DistanceFrom(location) == minimalDistance);
        }

        public Taxi GetById(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            
            return Taxis.First(taxi => taxi.Id == id);
        }
    }
}