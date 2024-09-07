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

            routes.MapPageRoute("LoginRoute", "", "~/Account/Login.aspx");

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
               "Designation_Route",
               "Designation",
               "~/Master_Pages/Designation_Master.aspx"
           );

            routes.MapPageRoute(
              "UserCreation_Route",
              "UserCreation",
              "~/Master_Pages/UserCreation.aspx"
            );

            routes.MapPageRoute(
               "CommonMaster_Route",
               "CommonMaster/{Page}",
               "~/Master_Pages/CommonMaster.aspx"
           );

            routes.MapPageRoute(
              "Role_Responsibility_Route",
              "RoleAndResponsibility",
              "~/Master_Pages/RoleAndResponsibility.aspx"
            );

            routes.MapPageRoute(
             "UserMaster_Route",
             "UserMaster",
             "~/Master_Pages/UserCreation_Update.aspx"
           );
        }
    }
}