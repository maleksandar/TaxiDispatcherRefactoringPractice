using System;

namespace TaxiDispatcher.App
{
    public class NonExistingVechicle : Exception
    {
        public NonExistingVechicle(int id) : base($"Vechicle with id {id} doesn't exist!") { }
    }
}
