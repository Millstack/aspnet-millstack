using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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
            Bind_Grid();
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
            executeClass.Bind_Dropdown_Generic(DD_UserName, sql, "UserName", "UserName", parameters);

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



    //-----------------------------] Grid Bind [-----------------------------
    private void Bind_Grid()
    {
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters = new Dictionary<string, object>();

        try
        {
            parameters = new Dictionary<string, object>
            {
                { "@User_ID", Session["User_ID"] },

                // search filters parameters
                { "@User_ID_Name", DD_User_ID_FullName.SelectedIndex > 0 ? (object)DD_User_ID_FullName.SelectedValue : DBNull.Value },
                { "@UserName", DD_UserName.SelectedIndex > 0 ? (object)DD_UserName.SelectedValue : DBNull.Value },
                { "@Designation_ID", DD_Designation.SelectedIndex > 0 ? (object)DD_Designation.SelectedValue : DBNull.Value },
            };

            dt = executeClass.Get_DataTable_From_StoredProcedure(this.Page, "USP_Get_UserMaster", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                Grid_Search.DataSource = dt;
                Grid_Search.DataBind();

                ViewState["Search_DT"] = dt;
            }
            else
            {
                Grid_Search.DataSource = null;
                Grid_Search.DataBind();

                ViewState["Search_DT"] = null;
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }



    //-----------------------------] Grid Events [-----------------------------

    protected void Grid_Search_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt;

            string User_ID = Grid_Search.SelectedDataKey["User_ID"].ToString();
            ViewState["User_ID"] = User_ID;

            //string SerNo = Grid_Search.SelectedRow.Cells[0].Text;
            //string ID = Grid_Search.SelectedRow.Cells[1].Text;
            //Input_Main_Column_1.Text = Grid_Search.SelectedRow.Cells[2].Text;
            //Input_Main_Column_2.Text = Grid_Search.SelectedRow.Cells[3].Text;
            //Input_Main_Column_3.Text = Grid_Search.SelectedRow.Cells[4].Text;

            // redirecting to user creation page with encrypted ID in the url
            string encrypted_ID = EncryptionHelper.Encrypt_UrlSafe(User_ID);
            Response.Redirect(GetRouteUrl("UserCreation_Route", new { User_ID = HttpUtility.UrlEncode(encrypted_ID) }), false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)

        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }

    protected void Grid_Search_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //string Primary_ID = Grid_Search.DataKeys[e.RowIndex]["ID"].ToString();

            //if (executeClass.Check_To_Allow_Delete(this.Page, Primary_Key_Column, Primary_ID) == false)
            //{
            //    string sa_Body = $@"The record's primary key <b>{Primary_ID}</b> is in use in some other table, hence cannot delete this record. Please check";
            //    SweetAlert.GetSweet(this.Page, "warning", $"Cannot delete this record !!", $"{sa_Body}");
            //    return;
            //}

            //sqlQuery = $@"UPDATE {Main_Table_Name} SET IsDeleted = 1 WHERE {Primary_Key_Column} = {Primary_ID}";
            // excute the query, new logic in execute class needed
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }

    protected void Grid_Search_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Search.PageIndex = e.NewPageIndex;
        Bind_Grid();
    }






    //-----------------------------] Search Button Event [-----------------------------

    protected void Btn_UserCreation_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("UserCreation_Route", new { User_ID = "" }));
    }

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("UserMaster_Route", null));
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Bind_Grid();
    }



    
}