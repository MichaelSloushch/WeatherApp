using System.Collections.Generic;
using WeatherApp.BLL.DTOModels;
using WeatherApp.BLL.DTOModels.Input;
using WeatherApp.BLL.DTOModels.View;

namespace WeatherApp.BLL.Interfaces
{
    public interface IWeatherService
    {
        IEnumerable<FavoriteCityDtoViewModel> GetFavoriteCities(string filter);
        IEnumerable<CityDTOViewModel> GetCities(string filter);
        string AddToFavoriteCity(FavoriteCityDtoInputModel city);

        bool DeleteFromFaforite(string cityId);

        WeatherForeCastDtoViewModel GetWeaherForecast(string cityId);



    }
}
