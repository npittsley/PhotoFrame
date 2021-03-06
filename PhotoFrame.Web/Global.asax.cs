﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PhotoFrame.Web.Models;
using System.Diagnostics;

namespace PhotoFrame.Web
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
            Database.SetInitializer<ApplicationDbContext>(new PhotoInitializer());
        }
        protected void Application_PostResolveRequestCache()
        {
            //Debugger.Break();
        }
        protected void Application_MapRequestHandler()
        {
            //Debugger.Break();
        }
        protected void Application_PreRequestHandlerExecute()
        {
            //Debugger.Break();
        }
        protected void Application_PostRequestHandlerExecute()
        {
            //Debugger.Break();
        }
    }
}
