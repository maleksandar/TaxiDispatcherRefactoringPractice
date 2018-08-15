using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaxiDispatcher.App.Tests
{
    [TestClass]
    public class InMemoryTaxiRepositoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(NoAvailableVehicleException))]
        public void VechicleClosestTo_WhenThereAreNoVechicles_NoAvailableVehicleExceptionIsThrown()
        {
            // new Taxi { Id = 1, Location = 1, Name = "Boban", TaxiCompany = new TaxiCompany { Price = 1 } }
            var repo = new InMemoryTaxiRepository(new List<Taxi> { });

            repo.VechicleClosestTo(0, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(NoAvailableVehicleException))]
        public void VechicleClosestTo_WhenThereAreNoVechiclesInAcceptableDistance_NoAvailableVehicleExceptionIsThrown()
        {
            var repo = new InMemoryTaxiRepository(new List<Taxi> { new Taxi { Id = 1, Location = 11, Name = "Boban", TaxiCompany = new TaxiCompany { Price = 1 } } });

            repo.VechicleClosestTo(0, 10);
        }

        [TestMethod]
        public void VechicleClosestTo_WhenThereAreVechiclesInAcceptableDistance_ClosestIsReturned()
        {
            var closestTaxi = new Taxi { Id = 1, Location = 10, Name = "Boban", TaxiCompany = new TaxiCompany { Price = 1 } };
            var distantTaxi = new Taxi { Id = 1, Location = 9, Name = "Boban", TaxiCompany = new TaxiCompany { Price = 1 } };

            var repo = new InMemoryTaxiRepository(new List<Taxi> { closestTaxi, distantTaxi });

            var result = repo.VechicleClosestTo(20, 15);

            Assert.AreEqual(closestTaxi, result);
        }
    }
}
