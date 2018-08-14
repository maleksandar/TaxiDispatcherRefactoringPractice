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

            var scheduler = new Scheduler(rideRepositoryMock, taxyRepositoryMock, Console.WriteLine);
            scheduler.OrderRide(null);
        }
    }
}
