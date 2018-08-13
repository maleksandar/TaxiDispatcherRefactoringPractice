using System;

namespace TaxiDispatcher.App
{
    public class NoAvailableVehiclesException : Exception
    {
        public NoAvailableVehiclesException(string message): base(message) { }
    }
}