using WeatherApp.DAL.Entities;

namespace WeatherApp.DAL.Interfaces
{
    public abstract class WeatherForeCastAbstractRepository: AbstractRepository<WeatherForecast>
    {
        public abstract WeatherForecast GetWeather(string cityId);
    }
}
