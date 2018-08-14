namespace TaxiDispatcher.App
{
    public interface ITaxiRepository
    {
        Taxi ClosestToLocation(int location, int acceptableDistance);
        Taxi GetById(int id);
    }
}