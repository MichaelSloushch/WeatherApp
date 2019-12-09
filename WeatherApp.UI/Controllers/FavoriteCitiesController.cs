using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherApp.BLL.DTOModels.Input;
using WeatherApp.UI.Controllers.Base;
using WeatherApp.UI.Models.Input;
using WeatherApp.UI.Models.View;

namespace WeatherApp.UI.Controllers
{
    /// <summary>
    /// Gets requests from the page to work with Favorites cities
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class FavoriteCitiesController : BaseApiController
    {
        [HttpGet]
        public HttpResponseMessage GetFiltered(string filter)
        {
            try
            {
                if(filter==null) filter=String.Empty;
                var cities = WeatherService.GetFavoriteCities(filter);
                var result = cities.Select(item => new FavoriteCitiesViewModel {Id = item.Id, Name = item.Name}).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                var message = $"Failed to get filtered cities";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"{message}, error: {e.Message}");
            }

        }



        [HttpPost]
        public HttpResponseMessage Add(FavoriteCityInputModel model)
        {
            try
            {
                var inputModel = new FavoriteCityDtoInputModel { Id = model.Id, Name = model.Name };

                return Request.CreateResponse(HttpStatusCode.OK, WeatherService.AddToFavoriteCity(inputModel));
            }
            catch (Exception e)
            {
                var message = $"Failed to get thumbnail record.";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"{message}, error: {e.Message}");
            }

        }

        [HttpPost]
        public HttpResponseMessage Delete(string cityId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, WeatherService.DeleteFromFaforite(cityId));
            }
            catch (Exception e)
            {
                var message = $"Failed to get thumbnail record.";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"{message}, error: {e.Message}");
            }

        }

    }
}
