using System;

namespace TaxiDispatcher.App
{
    public class NoAvailableVehicleException : Exception
    {
        public NoAvailableVehicleException(int acceptableDistance): base($"There are no available taxi vehicles within {acceptableDistance} miles!") { }
        public NoAvailableVehicleException() : base("There are no available taxi vehicles!") { }

    }
}