
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using WeatherApp.DAL.Context;

namespace WeatherApp.DAL.Interfaces
{
    public abstract class AbstractRepository<T> where T : class,new()
    {

        public abstract string AddItem(T item);
        public abstract bool RemoveItem(string id);

        protected virtual bool ExecuteSqlStatement(string sql)
        {

            using (var dbContext = new DbContext())
            {
               
                var connection = dbContext.GetConnection();
                connection.Open();
                try
                {
                    var rowsEffected = connection.Execute(sql);
                    var result = rowsEffected == 1;
                    return result;
                }
                catch (Exception ex)
                {
                    //todo log
                }
                finally
                {
                    connection.Close();
                }
                return false;
            }

         
        }

        protected virtual IEnumerable<T> CallDbStoredProcedure(string spName, DynamicParameters paramList = null)
        {

            using (var dbContext = new DbContext())
            {
                IEnumerable<T> result = new List<T>();
                var connection = dbContext.GetConnection();
                connection.Open();
                try
                {
                    result = connection.Query<T>(spName, paramList, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    //todo log
                }
                finally
                {
                    connection.Close();
                }
                return result;
            }
        }

      }
}
