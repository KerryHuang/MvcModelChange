using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sample.Web.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string RepositoryType
        {
            get
            {
                // Get Repository Assembly Name
                return WebConfigurationManager.AppSettings["RepositoryType"].ToString().Trim();
            }
        }

        public static string ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            }
        }

        public static string EFConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["NorthwindEntities"].ConnectionString;
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
