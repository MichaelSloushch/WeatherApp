
using System;

namespace WeatherApp.BLL.DTOModels
{
    public class WeatherForeCastDtoViewModel
    {
        public string CityId { get; set; }
        public string WeatherText { get; set; }
        public float CelsiusTemperature { get; set; }
        public DateTime DateTime { get; set; }
    }
}
