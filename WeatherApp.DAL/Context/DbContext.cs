using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WeatherApp.DAL.Interfaces;

namespace WeatherApp.DAL.Context
{
    public class DbContext : IDbContext,IDisposable
    {
        private string _connectionString {  get; }
        private IDbConnection _connection { get; }

        public DbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            _connection = new SqlConnection(_connectionString);
        }

        public IDbConnection GetConnection()
        {
           return _connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
