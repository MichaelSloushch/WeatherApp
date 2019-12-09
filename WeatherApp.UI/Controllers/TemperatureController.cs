using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherApp.UI.Controllers.Base;
using WeatherApp.UI.Models.View;

namespace WeatherApp.UI.Controllers
{
    /// <summary>
    /// Gets requests from the page to work with temparature
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class TemperatureController : BaseApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string cityId)
        {
            try
            {
                var temperature = WeatherService.GetWeaherForecast(cityId);
                var result = new WeatherforecastViewModel
                {
                    Temperature = temperature.CelsiusTemperature,
                    WeatherText = temperature.WeatherText
                };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                var message = $"Failed to get temperature";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"{message}, error: {e.Message}");
            }
        }
    }
}
