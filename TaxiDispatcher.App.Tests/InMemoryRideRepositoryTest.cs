using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TaxiDispatcher.App.Tests
{
    [TestClass]
    public class InMemoryRideRepositoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveRide_WhenRideIsNull_ArgumentNullExceptionIsThrown()
        {
            // Arrange
            var repo = new InMemoryRideRepository();

            // Act
            repo.SaveRide(null);
        }

        [TestMethod]
        public void GetDriversRidingList_WhenRidesExist_ReturnsAllRides()
        {
            // Arrange
            var taxiId = 42;
            var repo = new InMemoryRideRepository();
            Taxi taxi = new Taxi { Id = taxiId, Location = 0, Name = "Boban", TaxiCompany = new TaxiCompany() };
            Ride ride = new Ride(new RideRequest(0, 1, Area.City, new DateTime()), taxi);
            Ride ride2 = new Ride(new RideRequest(0, 1, Area.City, new DateTime()), taxi);
            repo.SaveRide(ride);
            repo.SaveRide(ride2);

            // Act
            var result = repo.GetDriversRidingList(taxiId);

            // Assert
            Assert.AreEqual(2 ,result.Count());
            Assert.AreEqual(ride, result.ToList()[0]);
            Assert.AreEqual(ride2, result.ToList()[1]);
        }
    }
}
