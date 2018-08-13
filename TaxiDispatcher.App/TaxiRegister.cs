using System.Collections.Generic;

namespace TaxiDispatcher.App
{
    public static class TaxiRegister
    {
        public static readonly List<Taxi> AvailableTaxis = new List<Taxi>
        {
            new Taxi {Id = 1, Name = "Predrag", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 1},
            new Taxi {Id = 2, Name = "Nenad", TaxiCompany = TaxiCompanyRegister.Naxi, Location = 4},
            new Taxi {Id = 3, Name = "Dragan", TaxiCompany = TaxiCompanyRegister.Alfa, Location = 6},
            new Taxi {Id = 4, Name = "Goran", TaxiCompany = TaxiCompanyRegister.Gold, Location = 7}
        };
    }
}