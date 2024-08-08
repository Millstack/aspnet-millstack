using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace MILLSTACK
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            // Define custom routes
            routes.MapPageRoute(
                "HomePageRoute",            // Route name
                "HomePage",                 // URL pattern
                "~/View/HomePage/HomePage.aspx"  // Physical file
            );

            // Add more custom routes as needed
        }
    }
}