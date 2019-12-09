using System.Web.Http;
using Autofac;
using WeatherApp.BLL.Interfaces;
using WeatherApp.UI.Helpers;

namespace WeatherApp.UI.Controllers.Base
{
    /// <summary>
    /// Set resolved service
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class BaseApiController:ApiController
    {
        protected readonly IWeatherService WeatherService = AutofacResolver.Container.Resolve<IWeatherService>();
    }
}
