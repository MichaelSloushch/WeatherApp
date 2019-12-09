using Autofac;
using WeatherApp.BLL.Interfaces;
using WeatherApp.BLL.Services;

namespace WeatherApp.UI.Helpers
{
    /// <summary>
    /// Resolve all dependies
    /// </summary>
    public static class AutofacResolver
    {
        public static IContainer Container { get; set; }

        public static void ResolveDependies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
            AutofacResolver.Container = builder.Build();
        }
    }
}
