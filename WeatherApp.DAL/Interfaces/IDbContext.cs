using System.Data;

namespace WeatherApp.DAL.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
    }
}
