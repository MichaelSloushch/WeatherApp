using System.Net;

namespace WeatherApp.BLL.RestApiModels
{
    public class WebApiResponce<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Response { get; set; }
        public string Error { get; set; }
    }
}
