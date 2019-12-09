using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using WeatherApp.BLL.Interfaces;
using WeatherApp.BLL.Services;
using WeatherApp.UI.Helpers;

namespace WeatherApp.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
   
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacResolver.ResolveDependies();
        }
    }
}
