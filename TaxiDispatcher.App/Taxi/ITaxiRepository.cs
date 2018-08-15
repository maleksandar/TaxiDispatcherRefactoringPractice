namespace TaxiDispatcher.App
{
    public interface ITaxiRepository
    {
        Taxi VechicleClosestTo(int location, int acceptableDistance); 
    }
}