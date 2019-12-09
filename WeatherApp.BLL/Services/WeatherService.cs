using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.BLL.DTOModels;
using WeatherApp.BLL.DTOModels.Input;
using WeatherApp.BLL.DTOModels.View;
using WeatherApp.BLL.Helpers;
using WeatherApp.BLL.Interfaces;
using WeatherApp.BLL.RestApiModels;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Repositories;

namespace WeatherApp.BLL.Services
{
    /// <summary>
    /// service contains methods to work with cities and weatherforecast
    /// </summary>
    /// <seealso cref="WeatherApp.BLL.Interfaces.IWeatherService" />
    public class WeatherService : IWeatherService
    {
        //repository to work with favorite cities
        private readonly FavoriteCitiesRepository _favoriteCityRepository;
        //repository to work weather forecasts
        private readonly WeatherForecastRepository _weatherForecastRepository;

        private FavoriteCityDtoViewModel RemapFavoriteCityView(FavoriteCities city)
        {
            return new FavoriteCityDtoViewModel()
            {
                Id = city.Id,
                Name = city.Name
            };
        }

        private CityDTOViewModel RemapCityView(CitiesModel city)
        {
            if (city == null) return null;
            return new CityDTOViewModel()
            {
                Id = city.Key,
                Name = city.LocalizedName
            };
        }

        private FavoriteCities RemapFavoriteCityInput(FavoriteCityDtoInputModel city)
        {
            if (city == null) return null;
            return new FavoriteCities()
            {
                Id = city.Id,
                Name = city.Name
            };
        }
        private WeatherForeCastDtoViewModel RemapWeatherForecast(WeatherForecast weatherForecast)
        {
            if (weatherForecast == null) return null;

            return new WeatherForeCastDtoViewModel()
            {

                CityId = weatherForecast.CityId,
                WeatherText = weatherForecast.WeatherText,
                CelsiusTemperature = weatherForecast.CelsiusTemperature,
                DateTime = weatherForecast.DateTime
            };
        }

        private bool IsForecastOutOfDate(DateTime date)
        {
            //we consider the date to be old if more than 12 hours have passed
            int hoursBetweenDates = (DateTime.Now - date).Hours;

            return hoursBetweenDates>12;
        }

        private WeatherForeCastDtoViewModel GetWeaherForecastFromDb(string cityId)
        {
            var weather = _weatherForecastRepository.GetWeather(cityId);
            if (weather == null) return null;

            //check if the saved date is to old
            if (IsForecastOutOfDate(weather.DateTime.ToLocalTime())) return null;

            var result = RemapWeatherForecast(weather);
            return result;
        }

        private WeatherForeCastDtoViewModel GetWeaherForecastFromRestService(string cityId)
        {
            var client = new WeatherRestServiceHelper<IEnumerable<WeatherForecastModel>>();
            var result = client.GetWeather(cityId);
            var weather = result?.Response?.FirstOrDefault();
            if (weather == null || !string.IsNullOrEmpty(result.Error))
            {
                throw new Exception("Failed to get westher from the REST service\n" + result?.Error);
            }
          
            _weatherForecastRepository.AddItem(new WeatherForecast()
            {
                CelsiusTemperature = weather.Temperature.Metric.Value,
                CityId = cityId,
                WeatherText = weather.WeatherText,
                DateTime = weather.LocalObservationDateTime
            });
            return new WeatherForeCastDtoViewModel
            {
                WeatherText = weather.WeatherText,
                CelsiusTemperature = weather.Temperature.Metric.Value

            };

        }

        public WeatherService()
        {
            _favoriteCityRepository=new FavoriteCitiesRepository();
            _weatherForecastRepository =new WeatherForecastRepository();
        }
        public IEnumerable<FavoriteCityDtoViewModel> GetFavoriteCities(string filter)
        {
            var result = new List<FavoriteCityDtoViewModel>();
            try
            {
                var favoriteCities = _favoriteCityRepository.GetList(filter);
                result.AddRange(favoriteCities.Select(RemapFavoriteCityView));
               
            }
            catch (Exception ex)
            {
                //todo log
            }
       
            return result;
        }

        public IEnumerable<CityDTOViewModel> GetCities(string filter)
        {
            var client = new WeatherRestServiceHelper<IEnumerable<CitiesModel>>();
            var requestResult = client.GetCities(filter);
            var cities = requestResult?.Response;
            if (cities == null || !string.IsNullOrEmpty(requestResult.Error))
            {
                throw new Exception("Failed to get westher from the REST service\n" + requestResult?.Error);
            }

            var result = new List<CityDTOViewModel>();
          
            result.AddRange(cities.Select(RemapCityView));
            return result;
        }


        public string AddToFavoriteCity(FavoriteCityDtoInputModel city)
        {
            var result=_favoriteCityRepository.AddItem(RemapFavoriteCityInput(city));
            return result;
        }

        public bool DeleteFromFaforite(string cityId)
        {
            var result = _favoriteCityRepository.RemoveItem(cityId);
            return result;
        }

        public WeatherForeCastDtoViewModel GetWeaherForecast(string cityId)
        {
            // tries to get value from db, otherwise, call api
            return GetWeaherForecastFromDb(cityId) ?? GetWeaherForecastFromRestService(cityId);
        }
    }
}
