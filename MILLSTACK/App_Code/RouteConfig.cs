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


            // Master Pages :: Home

            routes.MapPageRoute(
                "HomePage_Route", // Route name
                "HomePage", // URL pattern
               "~/Master_Pages/HomePage.aspx" // Physical file
            );


            // Master Pages :: Dashboard
            routes.MapPageRoute(
             "Dashboard_Route",
             "Dashboard",
             "~/Master_Pages/Dashboard.aspx"
           );


            // Master Pages :: Example
            routes.MapPageRoute(
                "ProductRoute",
                "Product/{productId}",
                "~/View/Product/Details.aspx"
            );

            //string homePageUrl = GetRouteUrl("HomePageRoute", null);
            //string productUrl = GetRouteUrl("ProductRoute", new { productId = 42 });


            // Master Pages :: Designation / Hierachy
            routes.MapPageRoute(
               "Designation_Route",
               "Designation",
               "~/Master_Pages/Designation_Master.aspx"
           );


            // Master Pages :: User Creation
            routes.MapPageRoute(
              "UserCreation_Route",
              "UserCreation/{User_ID}",
              "~/Master_Pages/UserCreation.aspx",
              true,
              new RouteValueDictionary { { "User_ID", "" } }  // Set default value for User_ID (for insert mode)
            );


            // Master Pages :: User Creation Update
            routes.MapPageRoute(
             "UserMaster_Route",
             "UserMaster",
             "~/Master_Pages/UserCreation_Update.aspx"
           );

            // Master Pages :: Common Master
            routes.MapPageRoute(
               "CommonMaster_Route",
               "CommonMaster/{Page}",
               "~/Master_Pages/CommonMaster.aspx"
           );

            // Master Pages :: Roels & Responsibility
            routes.MapPageRoute(
              "Role_Responsibility_Route",
              "RoleAndResponsibility",
              "~/Master_Pages/RoleAndResponsibility.aspx"
            );



            // Traansction Pages :: Csutomer Upload
            routes.MapPageRoute(
             "Customer_Upload_Route",
             "CustomerUpload",
             "~/Transaction_Pages/Customer_Upload.aspx"
           );

            // Traansction Pages :: Csutomer Insert / Update
            routes.MapPageRoute(
            "Customer_Master_Route",
            "Customer",
            "~/Transaction_Pages/Customer_Master.aspx"
          );


            // Traansction Pages :: Csutomer Master Grid
            routes.MapPageRoute(
                "Customer_Master_Update_Route",
                "CustomerMaster",
                "~/Transaction_Pages/Customer_Master_Update.aspx"
             );


        }
    }
}