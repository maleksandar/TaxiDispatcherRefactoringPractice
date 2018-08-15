using System;

namespace TaxiDispatcher.App
{
    public class Taxi
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public TaxiCompany TaxiCompany { get; set; }
        
        public int Location { get; set; }

        public int DistanceFrom(int location)
        {
            return Math.Abs(Location - location);
        }

        public void DriveTo(int location)
        {
            Location = location;
        }
    }
}