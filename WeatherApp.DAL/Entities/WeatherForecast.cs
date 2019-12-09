using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.DAL.Entities
{
    public class WeatherForecast
    {
        public string CityId { get; set; }
        public string WeatherText { get; set; }
        public float CelsiusTemperature { get; set; }
        // we need to compare 
        public DateTime DateTime { get; set; }
    }
}
