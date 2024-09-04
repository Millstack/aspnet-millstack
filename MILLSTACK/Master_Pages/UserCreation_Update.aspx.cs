using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_UserCreation_Update : System.Web.UI.Page
{
    #region [ Global Declaration ]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Dropdown();
        }
    }


    //-----------------------------] DropDown Bind [-----------------------------

    private void Bind_Dropdown()
    {
        string sql = string.Empty;

        try
        {
            // user full name
            sql = $@"Select CONCAT(User_ID, ' - ', FirstName, ' ', LastName) as UserFullName, User_ID From Tbl_M_UserMaster Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_User_ID_FullName, sql, "UserFullName", "User_ID", parameters);

            // username
            sql = $@"Select UserName, User_ID From Tbl_M_UserMaster Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_UserName, sql, "UserName", "User_ID", parameters);

            // designation
            sql = $@"Select d.DesignationName, d.Designation_ID
                    From Tbl_M_UserMaster as um
                    Inner Join M_Designation as d on d.Designation_ID = um.Designation_ID
                    Where um.IsDeleted IS NULL AND d.IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_Designation, sql, "DesignationName", "Designation_ID", parameters);
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }







    //-----------------------------] Search Button Event [-----------------------------

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("UserMaster_Route", null));
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {

    }
}