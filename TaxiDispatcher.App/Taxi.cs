namespace TaxiDispatcher.App
{
    public class Taxi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaxiCompany TaxiCompany { get; set; }
        public int Location { get; set; }
    }
}