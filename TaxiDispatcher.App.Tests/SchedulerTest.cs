using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TaxiDispatcher.App.Tests
{
    [TestClass]
    public class SchedulerTest
    {
        private Scheduler scheduler;
        private int requestToLocation;
        private int requestFromLocation;
        private Taxi taxi;
        private Mock<IRideRepository> rideRepositoryMock;
        private Mock<ITaxiRepository> taxiRepositoryMock;
        private RideRequest rideRequest;
        private Mock<ILogger> loggerMock;

        [TestInitialize]
        public void TesInitialize()
        {
            rideRepositoryMock = new Mock<IRideRepository>();
            taxiRepositoryMock = new Mock<ITaxiRepository>();
            loggerMock = new Mock<ILogger>();
            loggerMock.Setup(logger => logger.Log(It.IsAny<string>())).Verifiable();
            taxi = new Taxi { Id = 1, Location = 0, Name = "Boban", TaxiCompany = new TaxiCompany { Price = 1 } };
            taxiRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(taxi);
            scheduler = new Scheduler(rideRepositoryMock.Object, taxiRepositoryMock.Object, loggerMock.Object);
            requestFromLocation = 0;
            requestToLocation = 1;
            rideRequest = new RideRequest(requestFromLocation, requestToLocation, Area.City, new DateTime(2018, 1, 1, 12, 0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrderRide_WhenRequestIsNull_ArgumentNullExceptionIsThrown()
        {
            // Act
            scheduler.OrderRide(null);
        }

        [TestMethod]
        public void OrderRide_WhenTaxiIsAvailable_TaxiLocationIsChangedToRequestToLocation()
        {
            // Arange
            taxiRepositoryMock.Setup(x => x.VechicleClosestTo(It.IsAny<int>(), It.IsAny<int>())).Returns(taxi);

            // Act
            scheduler.OrderRide(rideRequest);

            // Assert
            Assert.AreEqual(requestToLocation, taxi.Location);
        }

        [TestMethod]
        public void OrderRide_WhenTaxiIsAvailable_RideSaveIsCalled()
        {
            // Arange
            taxiRepositoryMock.Setup(x => x.VechicleClosestTo(It.IsAny<int>(), It.IsAny<int>())).Returns(taxi);

            // Act
            scheduler.OrderRide(rideRequest);

            // Assert
            rideRepositoryMock.Verify(x => x.SaveRide(It.IsAny<Ride>()));
        }


        [TestMethod]
        [ExpectedException(typeof(NoAvailableVehicleException))]
        public void OrderRide_WhenNoTaxiIsAvailable_NoAvailableVehicleExceptionIsThrown()
        {
            // Arange
            var scheduler = new Scheduler(rideRepositoryMock.Object, new InMemoryTaxiRepository(new List<Taxi>()), loggerMock.Object);
            
            // Act
            scheduler.OrderRide(rideRequest);
        }

        [TestMethod]
        public void PrintStatsForDriver_WhenRidesAreNotEmpty_CorrectMessagesAreLogged()
        {
            // Arange
            var rides = new[] { new Ride(rideRequest, taxi) };
            rideRepositoryMock.Setup(x => x.GetDriversRidingList(It.IsAny<int>())).Returns(rides);
            var scheduler = new Scheduler(rideRepositoryMock.Object, new InMemoryTaxiRepository(new List<Taxi>()), loggerMock.Object);

            // Act
            scheduler.PrintStatsForDriver(taxi.Id);

            // Assert
            loggerMock.Verify(logger => logger.Log("Price: 1"));
            loggerMock.Verify(logger => logger.Log("Driver with ID = 1 earned today:"));
            loggerMock.Verify(logger => logger.Log("Total: 1"));
        }

        [TestMethod]
        public void PrintStatsForDriver_WhenRidesAreEmpty_CorrectMessagesAreLogged()
        {
            // Arange
            var rides = new List<Ride> { };
            rideRepositoryMock.Setup(x => x.GetDriversRidingList(It.IsAny<int>())).Returns(rides);
            var scheduler = new Scheduler(rideRepositoryMock.Object, new InMemoryTaxiRepository(new List<Taxi>()), loggerMock.Object);

            // Act
            scheduler.PrintStatsForDriver(taxi.Id);

            // Assert
            loggerMock.Verify(logger => logger.Log("Driver with ID = 1 earned today:"));
            loggerMock.Verify(logger => logger.Log("Total: 0"));
        }
    }
}
