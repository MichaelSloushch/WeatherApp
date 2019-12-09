using System.Collections.Generic;
using WeatherApp.DAL.Entities;
using WeatherApp.DAL.Interfaces;

namespace WeatherApp.DAL.Abstract
{
    public abstract class CityAbstractRepository: AbstractRepository<FavoriteCities>
    {
        public abstract IEnumerable<FavoriteCities> GetList(string filter);
    }
}
