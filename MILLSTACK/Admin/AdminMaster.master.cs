using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonClassLibrary;
using System.Drawing;
using System.Web.DynamicData;

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
{
    #region [GLobal Declaration]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User_ID"] != null)
        {
            if (!IsPostBack)
            {
                Literal_Navbar_UserName.Text = Session["User_FullName"].ToString();
                //Literal_Navbar_UserRole.Text = Session["UserRole"].ToString();

                Clear_All_ViewStates();

                Session["Menu_Form_Structure"] = null;

                if (Session["Menu_Form_Structure"] == null) Create_Sidebar_Menus();
                else LiteralMenu.Text = Session["Menu_Form_Structure"].ToString().Trim();
            }
        }
        else
        {
            main.Visible = false; // hiding child page content
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(GetRouteUrl("LoginRoute", null));
        }
    }




    //------------==================( Clearing ViewState & Sessions )==================------------
    private void Clear_All_ViewStates()
    {
        foreach (var key in ViewState.Keys.Cast<string>().ToList())
        {
            ViewState[key] = null;
        }
    }





    //------------============( Navbar Redirect )============------------
    protected void HomeBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("HomePage_Route", null));
    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();

        main.Visible = false; // hiding child page content
        Response.Redirect(GetRouteUrl("LoginRoute", null));
    }




    //------------============( Navbar Redirect )============------------

    private void Create_Sidebar_Menus()
    {
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters = new Dictionary<string, object>();

        try
        {
            StringBuilder menuBuilder = new StringBuilder();
            string userId = Session["User_ID"].ToString();

            DataTable Main_Menu_DT = new DataTable();
            DataTable Child_Menu_DT = new DataTable();

            parameters = new Dictionary<string, object>
            {
                { "@User_ID", Session["User_ID"] },
                { "@Parent_ID", 0 },
            };

            Main_Menu_DT = executeClass.Get_DataTable_From_StoredProcedure(this.Page, "USP_GET_Sidebar_Menus", parameters);
            if (Main_Menu_DT != null && Main_Menu_DT.Rows.Count > 0)
            {
                foreach (DataRow mainRow in Main_Menu_DT.Rows)
                {
                    string mainMenuName = mainRow["MenuName"].ToString().Trim();
                    string mainMenuId = mainRow["Menu_ID"].ToString().Trim();
                    string mainMenuNavigateUrl = mainRow["MenuURL"].ToString().Trim();
                    string mainMenuIcon = mainRow["MenuIcon"].ToString().Trim();

                    menuBuilder.AppendLine($"<li class='border border-dark'>");
                    menuBuilder.AppendLine($"<div class='iocn-link py-1' style='cursor: pointer;' >");
                    menuBuilder.AppendLine($"<a href='{mainMenuNavigateUrl}'>");
                    menuBuilder.AppendLine($"<img src='{mainMenuIcon}' class='white-svg p-3' />");
                    //menuBuilder.AppendLine($"<i class='bi bi-person-vcard-fill p-3 white-svg p-3'></i>");
                    menuBuilder.AppendLine($"<span class='link_name ps-2'>{mainMenuName}</span>");
                    menuBuilder.AppendLine("</a>");
                    menuBuilder.AppendLine("<img src='/assets/icons/box-icons/bx-chevron-down.svg' class='white-svg arrow px-3 py-3' style='cursor: pointer;' />");
                    menuBuilder.AppendLine("</div>");


                    parameters = new Dictionary<string, object>
                    {
                        { "@User_ID", Session["User_ID"] },
                        { "@Parent_ID", mainMenuId },
                    };

                    Child_Menu_DT = executeClass.Get_DataTable_From_StoredProcedure(this.Page, "USP_GET_Sidebar_Menus", parameters);
                    if (Child_Menu_DT != null && Child_Menu_DT.Rows.Count > 0)
                    {
                        menuBuilder.AppendLine("<ul class='sub-menu ps-2' >");
                        menuBuilder.AppendLine($"<li class='fw-lighter fs-6 px-2 border-bottom border-dark-subtle bg-gradient mb-2'>");
                        menuBuilder.AppendLine($"<a class='link_name' href='{mainMenuNavigateUrl}'>{mainMenuName}</a>");
                        menuBuilder.AppendLine($"</li>");

                        foreach (DataRow childRow in Child_Menu_DT.Rows)
                        {
                            string childMenuName = childRow["MenuName"].ToString().Trim();
                            string childMenuId = childRow["Menu_ID"].ToString().Trim();
                            string childMenuNavigateUrl = childRow["MenuURL"].ToString().Trim();

                            menuBuilder.AppendLine($"<li id='{childMenuId}' class='my-1 w-100' style='border-left: 3px solid red; border-bottom: 1px solid #313131;'>");
                            menuBuilder.AppendLine($"<button class='fw-lighter btn text-white py-2 px-2 col-12 text-start'>");
                            menuBuilder.AppendLine($"<a href='{childMenuNavigateUrl}'>{childMenuName}</a>");
                            menuBuilder.AppendLine("</button>");
                            menuBuilder.AppendLine("</li>");
                        }

                        menuBuilder.AppendLine("</ul>");
                    }

                    menuBuilder.AppendLine("</li>");
                }
            }

            LiteralMenu.Text = menuBuilder.ToString().Trim();
            Session["Menu_Form_Structure"] = menuBuilder.ToString().Trim();
        }
        catch(Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }





}
