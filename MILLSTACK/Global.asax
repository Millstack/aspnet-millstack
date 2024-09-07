<%@ Application Language="C#" %>
<%@ Import Namespace="MILLSTACK" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

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
