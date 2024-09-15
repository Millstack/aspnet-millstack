using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_Login : System.Web.UI.Page
{
    #region [ GLobal Declaration ]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Destroy_Existing_Sessions();
            Clear_All_ViewStates();
        }
    }





    //-----------------------------] Clearing Sessions & ViewState [-----------------------------
    private void Destroy_Existing_Sessions()
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
    }

    private void Clear_All_ViewStates()
    {
        foreach (var key in ViewState.Keys.Cast<string>().ToList())
        {
            ViewState[key] = null;
        }
    }





    //-----------------------------] Login Event [-----------------------------

    protected void btn_Login_Click(object sender, EventArgs e)
    {
        // user input
        string userName = Txt_UserName.Text;
        string password = Txt_Passowrd.Text;

        string sql = $@"Select Distinct um.User_ID, UserName,  Concat(um.FirstName, ' ' ,um.LastName) as User_FullName, Salt, UserPassword
                        From Tbl_M_UserMaster as um 
                        Where um.IsDeleted IS NULL AND UserName = @UserName";
        var parameters = new Dictionary<string, object> { { "@UserName", userName }, };
        DataTable dt = executeClass.Get_Datatable(sql, parameters);
        if(dt != null && dt.Rows.Count > 0)
        {
            // performing hashing with user name fetched salt and enterd password, to compare with has stored in database
            Div_Wrong.Visible = false;

            // hashing user enterd password to check with database hashed password
            string user_Salt = dt.Rows[0]["Salt"].ToString();
            string user_Password_Hash = dt.Rows[0]["UserPassword"].ToString();
            string login_Password_Hash = HashHelper.Hash(password, user_Salt);

            if (user_Password_Hash == login_Password_Hash)
            {
                // assigning sessions
                Session["User_ID"] = dt.Rows[0]["User_ID"].ToString();
                Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                Session["User_FullName"] = dt.Rows[0]["User_FullName"].ToString();

                System.Threading.Thread.Sleep(2000);

                //Response.Redirect(GetRouteUrl("HomePage_Route", null)); // home page
                Response.Redirect(GetRouteUrl("Dashboard_Route", null)); // dashboard
            }
            else
            {
                Div_Wrong.Visible = true;
                wrong.Text = "Password did not match, please check";
            }
        }
        else
        {
            Div_Wrong.Visible = true;
            wrong.Text = "Username does not exists, please check !";
        }
    }




}