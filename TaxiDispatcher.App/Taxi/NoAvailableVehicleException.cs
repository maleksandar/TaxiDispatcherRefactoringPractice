using System;

namespace TaxiDispatcher.App
{
    public class NoAvailableVehicleException : Exception
    {
        public NoAvailableVehicleException(): base("There are no available taxi vehicles!") { }
    }
}