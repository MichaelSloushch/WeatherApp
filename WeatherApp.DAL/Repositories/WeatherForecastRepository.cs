using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Interfaces;

namespace WeatherApp.DAL.Repositories
{
    public class WeatherForecastRepository: WeatherForeCastAbstractRepository
    {
        public override string AddItem(WeatherForecast item)
        {
        
        
          ExecuteSqlStatement($"EXEC {Consts.AddWeatherForecastStoredProcName} {item.CityId},{item.WeatherText},{item.CelsiusTemperature},'{item.DateTime}'");
            var result = GetWeather(item.CityId)?.CityId;
            return result;
        }

        public override bool RemoveItem(string id)
        {
            var filters = new DynamicParameters();
            filters.Add("@CityId", id);
   
            var result =  ExecuteSqlStatement($"EXEC {Consts.RemoveWeatherForecastStoredProcName} @CityId = {id};");
            return result;
        }

        public override WeatherForecast GetWeather(string cityId)
        {
            var filters = new DynamicParameters();
            filters.Add("@CityId", cityId);
            var result = CallDbStoredProcedure(Consts.GetWeatherStoredProcName, filters).FirstOrDefault();
            return result;
        }
    }
}
