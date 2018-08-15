using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.App
{
    public class NonExistingVechicle : Exception
    {
        public NonExistingVechicle(int id) : base($"Vechicle with id {id} doesn't exist!") { }
    }
}
