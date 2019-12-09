using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DAL
{
    public static class Consts
    {
        public static string GetFavoriteCitiesStoredProcName = "GetFavoriteCities";
        public static string AddFavoriteCityStoredProcName = "AddFavoriteCity";
        public static string RemoveFavoriteCityStoredProcName = "RemoveFavoriteCity";

        
        public static string GetWeatherStoredProcName = "GetWeather";
        public static string AddWeatherForecastStoredProcName = "AddWeatherForecast";
        public static string RemoveWeatherForecastStoredProcName = "RemoveWeatherForecast";
      
    }
}

