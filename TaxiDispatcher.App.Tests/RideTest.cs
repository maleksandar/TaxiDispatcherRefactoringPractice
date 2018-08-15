using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaxiDispatcher.App.Tests
{
    [TestClass]
    public class RideTest
    {
        private int requestFrom;
        private int requestTo;
        private int companyPrce;
        private Taxi taxi;
        private TaxiCompany taxiCompany;

       [TestInitialize]
        public void TestInitialize()
        {
            requestFrom = 0;
            requestTo = 10;
            companyPrce = 10;
            taxiCompany = new TaxiCompany { Price = 10 };
            taxi = new Taxi { Id = 1, Location = 0, Name = "Gagi", TaxiCompany = taxiCompany };
        }

        [TestMethod]
        public void Price_DuringDay_And_InsideCityArea_ReturnsPriceWithNoBonus()
        {
            var ride = new Ride(new RideRequest(0, 10, Area.City, new DateTime(2012, 1, 1, 12, 0, 0)), taxi);

            Assert.AreEqual(Math.Abs(requestFrom - requestTo) * companyPrce, ride.Price);
        }

        [TestMethod]
        public void Price_DuringNight_And_InsideCityArea_ReturnsPriceWithNoBonus()
        {
            var ride = new Ride(new RideRequest(0, 10, Area.City, new DateTime(2012, 1, 1, 0, 0, 0)), taxi);

            Assert.AreEqual(Math.Abs(requestFrom - requestTo) * companyPrce * Ride.NightTimeBonus, ride.Price);
        }

        [TestMethod]
        public void Price_DuringNight_And_OutsideCityArea_ReturnsPriceWithNoBonus()
        {
            var ride = new Ride(new RideRequest(0, 10, Area.InterCity, new DateTime(2012, 1, 1, 0, 0, 0)), taxi);

            Assert.AreEqual(Math.Abs(requestFrom - requestTo) * companyPrce * Ride.NightTimeBonus * Ride.InterCityBonus, ride.Price);
        }
    }
}
