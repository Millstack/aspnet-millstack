<%@ Application Language="C#" %>
<%@ Import Namespace="MILLSTACK" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    //void Session_Start(object sender, EventArgs e)
    //{
    //    if (Session["User_ID"] != null)
    //    {
    //        VirtualPathData routeData = RouteTable.Routes.GetVirtualPath(null, "Dashboard_Route", null);
    //        if (routeData != null)
    //        {
    //            string virtualPath = routeData.VirtualPath;
    //            Response.Redirect("~/" + virtualPath);
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("~/Account/Login.aspx");
    //    }
    //}

    //void Application_BeginRequest(object sender, EventArgs e)
    //{
    //    // Redirect to login if user is not authenticated
    //    //if (!HttpContext.Current.Request.Url.AbsolutePath.EndsWith("Login.aspx", StringComparison.OrdinalIgnoreCase) &&
    //    //    !HttpContext.Current.User.Identity.IsAuthenticated)
    //    //{
    //    //    HttpContext.Current.Response.Redirect("~/Account/Login.aspx");
    //    //}

    //    // Check if session is available and if the User_ID is null
    //    if (HttpContext.Current.Session != null && Session["User_ID"] == null)
    //    {
    //        // Redirect to login page if session is null
    //        Response.Redirect("~/Account/Login.aspx");
    //    }
    //    else if (HttpContext.Current.Session != null && Session["User_ID"] != null)
    //    {
    //        // Use RouteTable.Routes.GetVirtualPath to get the route URL for Dashboard
    //        VirtualPathData routeData = RouteTable.Routes.GetVirtualPath(null, "Dashboard_Route", null);

    //        if (routeData != null)
    //        {
    //            string virtualPath = routeData.VirtualPath;
    //            Response.Redirect("~/" + virtualPath);
    //        }
    //    }
    //}



    //void Application_BeginRequest(object sender, EventArgs e)
    //{
    //    // Ensure the user is redirected to the login page if they are not authenticated
    //    if (!HttpContext.Current.Request.Url.AbsolutePath.EndsWith("Login.aspx", StringComparison.OrdinalIgnoreCase) &&
    //        !HttpContext.Current.User.Identity.IsAuthenticated)
    //    {
    //        HttpContext.Current.Response.Redirect("~/Login.aspx");
    //    }
    //}

</script>
