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

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
{
    #region [GLobal Declaration]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Literal_Navbar_UserName.Text = Session["UserName"].ToString();
                Literal_Navbar_UserRole.Text = Session["UserRole"].ToString();

                Clear_All_ViewStates();

                if (Session["Menu_Form_Structure"] == null) Create_Sidebar_Menus();
                else LiteralMenu.Text = Session["Menu_Form_Structure"].ToString().Trim();
            }
        }
        else
        {
            //main.Visible = false; // hiding child page content
            //Response.Redirect("~/Account/Login.aspx");
            //Response.Redirect(GetRouteUrl("UserCreation_Route", null));
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
        Response.Redirect("~/View/HomePage/HomePage.aspx");
    }

    protected void LogoutBtn_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1000);

        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();

        Response.Redirect("~/Account/Login.aspx");
    }




    //------------============( Navbar Redirect )============------------

    private void Create_Sidebar_Menus()
    {
        try
        {
            StringBuilder menuBuilder = new StringBuilder();
            string userId = Session["UserId"].ToString();
            string menu_query;

            DataTable Main_Menu_DT = new DataTable();
            DataTable Child_Menu_DT = new DataTable();

            menu_query = $@"
                    SELECT distinct 
	                    MF.ID, MF.MenuName, MF.MenuOrder, MF.MenuURL, MF.MenuIcon
                    FROM M_MenuForm AS MF
                    INNER JOIN Map_UserRole_MenuForm AS MAP ON MAP.MenuForm_ID = MF.ID
                    INNER JOIN UserRoles AS UR ON UR.UserID = MAP.UserRole_ID
                    WHERE 
	                    MF.IsDeleted IS NULL AND MAP.IsDeleted IS NULL
	                    AND MF.Parent_ID = 0
	                    AND UR.UserID = '{userId}'
                    ORDER BY MF.MenuOrder";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            Main_Menu_DT = executeClass.Get_Datatable(menu_query, parameters);
            if (Main_Menu_DT.Rows.Count > 0)
            {
                foreach (DataRow mainRow in Main_Menu_DT.Rows)
                {
                    string mainMenuName = mainRow["MenuName"].ToString().Trim();
                    string mainMenuId = mainRow["ID"].ToString().Trim();
                    string mainMenuNavigateUrl = mainRow["MenuURL"].ToString().Trim();
                    string mainMenuIcon = mainRow["MenuIcon"].ToString().Trim();

                    menuBuilder.AppendLine($"<li class='border border-dark'>");
                    menuBuilder.AppendLine($"<div class='iocn-link py-1' style='cursor: pointer;' >");
                    menuBuilder.AppendLine($"<a href='{mainMenuNavigateUrl}'>");
                    menuBuilder.AppendLine($"<img src='{mainMenuIcon}' class='white-svg p-3' />");
                    menuBuilder.AppendLine($"<span class='link_name ps-2'>{mainMenuName}</span>");
                    menuBuilder.AppendLine("</a>");
                    menuBuilder.AppendLine("<img src='/assests/box-icons/bx-chevron-down.svg' class='white-svg arrow px-3 py-3' style='cursor: pointer;' />");
                    menuBuilder.AppendLine("</div>");


                    menu_query = $@"
                             SELECT distinct 
	                             MF.ID, MF.MenuName, MF.MenuOrder, MF.MenuURL, MF.MenuIcon
                             FROM M_MenuForm AS MF
                             INNER JOIN Map_UserRole_MenuForm AS MAP ON MAP.MenuForm_ID = MF.ID
                             INNER JOIN UserRoles AS UR ON UR.UserID = MAP.UserRole_ID
                             WHERE 
	                             MF.IsDeleted IS NULL AND MAP.IsDeleted IS NULL
	                             AND MF.Parent_ID = '{mainMenuId}'
	                             AND UR.UserID = '{userId}'
                             ORDER BY MF.MenuOrder";
                    parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                    Main_Menu_DT = executeClass.Get_Datatable(menu_query, parameters);
                    if (Child_Menu_DT.Rows.Count > 0)
                    {
                        menuBuilder.AppendLine("<ul class='sub-menu ps-2' >");
                        menuBuilder.AppendLine($"<li class='fw-lighter fs-6 px-2 border-bottom border-dark-subtle bg-gradient mb-2'>");
                        menuBuilder.AppendLine($"<a class='link_name' href='{mainMenuNavigateUrl}'>{mainMenuName}</a>");
                        menuBuilder.AppendLine($"</li>");

                        foreach (DataRow childRow in Child_Menu_DT.Rows)
                        {
                            string childMenuName = childRow["MenuName"].ToString().Trim();
                            string childMenuId = childRow["ID"].ToString().Trim();
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
