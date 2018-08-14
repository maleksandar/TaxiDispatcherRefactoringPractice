using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public class InMemoryTaxiRepository : ITaxiRepository
    {
        private static readonly IEnumerable<Taxi> Taxis = new[]
        {
            new Taxi {Id = 1, Name = "Predrag", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 1},
            new Taxi {Id = 2, Name = "Nenad", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 4},
            new Taxi {Id = 3, Name = "Dragan", TaxiCompany = TaxiCompanyRegister.Alfa, Location = 6},
            new Taxi {Id = 4, Name = "Goran", TaxiCompany = TaxiCompanyRegister.Gold, Location = 7}
        };

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