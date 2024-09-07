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

        string sql = $@"Select Menu_ID, Parent_ID, MenuName, MenuOrder, MenuURL, MenuIcon From Tbl_M_MenuForm Where IsDeleted IS NULL";
        var parameters = new Dictionary<string, object> { { "@ID", userName }, };
        DataTable dt = executeClass.Get_Datatable(sql, parameters);
        if(dt != null && dt.Rows.Count > 0)
        {
            // performing hashing with user name fetched salt and enterd password, to compare with has stored in database

        }
        else
        {

        }
    }




}