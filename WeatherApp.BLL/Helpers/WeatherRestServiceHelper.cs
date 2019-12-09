using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using WeatherApp.BLL.RestApiModels;

namespace WeatherApp.BLL.Helpers
{
    /// <summary>
    /// Wrapper to create request and process result
    /// </summary>
    /// <typeparam name="T">Requester model to map</typeparam>
    public class WeatherRestServiceHelper<T>
    {
        private HttpClient _client;

        private string _baseUrl = "http://dataservice.accuweather.com/";

        private static readonly string _apiKey = ConfigurationManager.AppSettings["WeaherServiceApiKey"];

        private WebApiResponce<T> HandleResult(HttpResponseMessage response)
        {
            var result = new WebApiResponce<T> { Status = response.StatusCode };

            if (response.IsSuccessStatusCode)
            {
                result.Response = response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                result.Error = response.Content.ReadAsAsync<string>().Result;
                result.Error = result.Error;
            }

            return result;
        }
        private WebApiResponce<T> HandleException(Exception ex)
        {
            var result = new WebApiResponce<T> { Status = HttpStatusCode.InternalServerError };

            var errorMessage = ex.Message;
            var traceException = ex.InnerException;

            if (traceException != null)
            {
                while (traceException.InnerException != null)
                {
                    errorMessage += "\n\n" + traceException.Message;
                    traceException = traceException.InnerException;
                }

                errorMessage += "\n\n" + traceException.Message;
            }

            result.Error = errorMessage;

            return result;
        }
    
    
        public WeatherRestServiceHelper()
        {
            _client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        
        public WebApiResponce<T> GetWeather(string cityId)
        {
            var apiAddress = $"currentconditions/v1/{cityId}?apikey={_apiKey}";

            try
            {
                HttpResponseMessage response = _client.GetAsync(apiAddress).Result;

                return HandleResult(response);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {
                _client.Dispose();
            }
        }

        public WebApiResponce<T> GetCities(string filter)
        {
            var apiAddress = $"locations/v1/cities/autocomplete?q={filter}&apikey={_apiKey}";

            try
            {
                HttpResponseMessage response = _client.GetAsync(apiAddress).Result;

                return HandleResult(response);

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {
                _client.Dispose();
            }
        }

    }
}
