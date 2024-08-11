using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for RoleAutorization
/// </summary>
public class RoleAutorization
{
    public RoleAutorization()
    {
        // constructor
    }


    //------------============( Authorization )============------------
    public static void CheckUserRole(Page page, ContentPlaceHolder placeholder, params string[] allowedRoles)
    {
        try
        {
            if (HttpContext.Current.Session["UserRole"] != null)
            {
                string userRole = HttpContext.Current.Session["UserRole"].ToString();
                if (!allowedRoles.Contains(userRole))
                {
                    placeholder.Visible = false;
                    SweetAlert.getSweetHTML(page, "info", "Not Authorized", $"<b>{userRole}</b> Is Not Authorized To Access This Page", "~/View/HomePage/HomePage.aspx");
                }
            }
            else
            {
                SweetAlert.getSweetHTML(page, "info", "Not Authorized", $"Kindly Login To Access This Page", "~/Account/Login.aspx");
            }
        }
        catch(Exception ex)
        {
            SweetAlert.getSweetHTML(page, "info", "Not Authorized", $"Kindly Login To Access This Page", "~/Account/Login.aspx");
        }
    }
}