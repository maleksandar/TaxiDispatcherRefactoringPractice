using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TaxiDispatcher.App.Tests
{
    [TestClass]
    public class SchedulerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrderRide_WhenRequestIsNull_ArgumentNullExceptionIsThrown()
        {
            var rideRepositoryMock = new Mock<IRideRepository>().Object;
            var taxyRepositoryMock = new Mock<ITaxiRepository>().Object;
            var loggerMock = new Mock<ILogger>();

            var scheduler = new Scheduler(rideRepositoryMock, taxyRepositoryMock, loggerMock.Object);
            scheduler.OrderRide(null);
        }

        [TestMethod]
        public void OrderRide_WhenTaxiIsAvailable_TaxiLocationIsChangedToRequestToLocation()
        {
            var rideRepositoryMock = new Mock<IRideRepository>().Object;
            var taxyRepositoryMock = new Mock<ITaxiRepository>();
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(logger => logger.Log(It.IsAny<string>())).Verifiable();
            var taxi = new Taxi { Id = 1, Location = 0, Name = "Boban", TaxiCompany = TaxiCompanyRegister.Naxi };
            taxyRepositoryMock.Setup(x => x.VechicleClosestTo(It.IsAny<int>(), It.IsAny<int>())).Returns(taxi);
            taxyRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(taxi);
            var scheduler = new Scheduler(rideRepositoryMock, taxyRepositoryMock.Object, loggerMock.Object);

            var requestFromLocation = 0;
            var requestToLocation = 1;
            var rideRequest = new RideRequest(requestFromLocation, requestToLocation, Area.City, new DateTime(2018, 1, 1, 12, 0, 0));
            scheduler.OrderRide(rideRequest);

            Assert.AreEqual(requestToLocation, taxi.Location);
        }
    }
}
