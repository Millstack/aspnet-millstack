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

            //string homePageUrl = GetRouteUrl("HomePageRoute", null);
            //string productUrl = GetRouteUrl("ProductRoute", new { productId = 42 });

            routes.MapPageRoute(
                "Exampe_Route", // Route name
                "Exampe", // URL pattern
               "~/Exampe_Folder/Exampe.aspx" // Physical file
            );


            // Login page route
            routes.MapPageRoute("LoginRoute", "", "~/Account/Login.aspx");




            //---------------------------- Master Pages ---------------------------- 

            // Home
            routes.MapPageRoute("HomePage_Route", "HomePage", "~/Master_Pages/HomePage.aspx");

            // Dashboard
            routes.MapPageRoute("Dashboard_Route", "Dashboard", "~/Master_Pages/Dashboard.aspx");

            // Designation / Hierachy
            routes.MapPageRoute("Designation_Route", "Designation", "~/Master_Pages/Designation_Master.aspx");

            // User Creation
            routes.MapPageRoute("UserCreation_Route", "UserCreation/{User_ID}", "~/Master_Pages/UserCreation.aspx", true, new RouteValueDictionary { { "User_ID", "" } });

            // User Creation Update
            routes.MapPageRoute("UserMaster_Route", "UserMaster", "~/Master_Pages/UserCreation_Update.aspx");

            // Common Master
            routes.MapPageRoute("CommonMaster_Route", "CommonMaster/{Page}", "~/Master_Pages/CommonMaster.aspx");

            // Roles & Responsibility
            routes.MapPageRoute("Role_Responsibility_Route", "RoleAndResponsibility", "~/Master_Pages/RoleAndResponsibility.aspx");




            //---------------------------- Traansction Pages ---------------------------- 

            // Customer Upload
            routes.MapPageRoute("Customer_Upload_Route", "CustomerUpload", "~/Transaction_Pages/Customer_Upload.aspx");

            // Customer Insert / Update
            routes.MapPageRoute("Customer_Master_Route", "Customer/{Customer_ID}", "~/Transaction_Pages/Customer_Master.aspx", true, new RouteValueDictionary { { "Customer_ID", "" } });

            // Customer Master Grid
            routes.MapPageRoute("Customer_Master_Update_Route", "CustomerMaster", "~/Transaction_Pages/Customer_Master_Update.aspx");

            // Booth Master Grid
            routes.MapPageRoute("Booth_Master_Route", "BoothMaster", "~/Transaction_Pages/Booth_Master.aspx");

            // Booth Details Modal
            routes.MapPageRoute("Booth_Details_Modal_Route", "BoothDetails", "~/Transaction_Pages/Modal/Booth_Master_Modal.aspx");









            // Fallback route for unmapped URLs
            routes.MapPageRoute(
                "404_Page_Not_Found_Route", // Route name
                "{*url}",           // Catch-all route
                "~/Master_Pages/404.aspx"        // Redirect to your custom 404 page
            );
        }
    }
}