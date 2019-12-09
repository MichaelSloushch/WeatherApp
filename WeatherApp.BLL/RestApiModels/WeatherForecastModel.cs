using System;

namespace WeatherApp.BLL.RestApiModels
{
    public class WeatherForecastModel
    {
        public DateTime LocalObservationDateTime { get; set; }
        public string WeatherText { get; set; }

        public TemperatureModel Temperature { get; set; }
    }
}
