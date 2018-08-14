using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxiDispatcher.App
{
    public class TaxiRegister : ITaxiRepository
    {
        private static readonly IEnumerable<Taxi> Taxis = new[]
        {
            new Taxi {Id = 1, Name = "Predrag", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 1},
            new Taxi {Id = 2, Name = "Nenad", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 4},
            new Taxi {Id = 3, Name = "Dragan", TaxiCompany = TaxiCompanyRegister.Alfa, Location = 6},
            new Taxi {Id = 4, Name = "Goran", TaxiCompany = TaxiCompanyRegister.Gold, Location = 7}
        };

        public Taxi ClosestToLocation(int location, int acceptableDistance)
        {
            var minimalDistance = Taxis.Min(taxi => Math.Abs(taxi.Location - location));
            if (minimalDistance > acceptableDistance)
            {
                throw new NoAvailableVehiclesException("There are no available taxi vehicles!");
            }

            return Taxis.First(taxi => taxi.DistanceFrom(location) == minimalDistance);

        }

        public Taxi GetById(int id)
        {
            return Taxis.First(taxi => taxi.Id == id);
        }
    }
}