﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyVanity.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.Register();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AppInitializer.CreateAdminUser();
            AppInitializer.InitializeData();
        }
    }
}