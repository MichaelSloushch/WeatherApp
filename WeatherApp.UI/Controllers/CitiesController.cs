using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherApp.UI.Controllers.Base;
using WeatherApp.UI.Models.View;

namespace WeatherApp.UI.Controllers
{
    /// <summary>
    /// Gets requests from the page to work with cities from the api search
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CitiesController : BaseApiController
    {


        [HttpGet]
        public HttpResponseMessage GetFiltered(string filter)
        {
            try
            {
                var cities = WeatherService.GetCities(filter);
                var result= cities.Select(item => new CitiesViewModel() {Id = item.Id, Name = item.Name}).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                var message = $"Failed to get cities.";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"{message}, error: {e.Message}");
            }
        }


    }
}
