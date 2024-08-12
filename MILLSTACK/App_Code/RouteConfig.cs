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

            routes.MapPageRoute(
                "HomePageRoute", // Route name
                "HomePage", // URL pattern
                "~/View/HomePage/HomePage.aspx" // Physical file
            );

            routes.MapPageRoute(
                "ProductRoute",
                "Product/{productId}",
                "~/View/Product/Details.aspx"
            );

            //string homePageUrl = GetRouteUrl("HomePageRoute", null);
            //string productUrl = GetRouteUrl("ProductRoute", new { productId = 42 });

            routes.MapPageRoute(
               "CommonMaster_Route",
               "CommonMaster/{Page}",
               "~/Master_Pages/CommonMasterTwo.aspx"
           );
        }
    }
}