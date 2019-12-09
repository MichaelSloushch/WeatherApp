using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WeatherApp.DAL.Abstract;
using WeatherApp.DAL.Context;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Interfaces;

namespace WeatherApp.DAL.Repositories
{
    public class FavoriteCitiesRepository:CityAbstractRepository
    {
        public override string AddItem(FavoriteCities item)
        {

            var filters = new DynamicParameters();
            filters.Add("@CityKey", item.Id);
            filters.Add("@CityName", item.Name);
            var result=CallDbStoredProcedure(Consts.AddFavoriteCityStoredProcName,  filters).FirstOrDefault()?.Id;
            return result;
        }

        public override bool RemoveItem(string id)
        {
            var result = ExecuteSqlStatement($"EXEC {Consts.RemoveFavoriteCityStoredProcName} @CityKey = {id};");
          return result;
        }

        public override IEnumerable<FavoriteCities> GetList(string filter)
        {

            var filters = new DynamicParameters();
            filters.Add("@CityName", filter);
            var result = CallDbStoredProcedure(Consts.GetFavoriteCitiesStoredProcName, filters);
            return result;
        }
    }
}
