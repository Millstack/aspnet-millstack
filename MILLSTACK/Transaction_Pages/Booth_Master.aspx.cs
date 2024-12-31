using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Pages_Booth_Master : System.Web.UI.Page
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
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        Dictionary<string, object> parameters;
        string sql = string.Empty;

        try
        {
            parameters = new Dictionary<string, object> { { "@User_ID", Session["User_ID"] }, };
            dt = executeClass.Get_DataTable_From_StoredProcedure("USP_Get_User_Hierarchy_Level", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                parameters = new Dictionary<string, object>
                {
                    // top dynamic desired record count
                    { "@Record_Count", DBNull.Value },

                    // work area parameters
                    { "@Assembly_IDs", Session["Assembly_ID"].ToString() },
                    { "@Ward_IDs", Session["Ward_ID"].ToString() },
                    { "@Sector_IDs", Session["Sector_ID"].ToString() },
                    { "@Level_ID", dt.Rows[0]["Level_ID"].ToString() },

                    // dropdown search parameters
                    //{ "@List_No", DD_List_No.SelectedIndex > 0 ? (object)DD_List_No.SelectedValue : DBNull.Value },
                    //{ "@Serial_No", DD_Serial_No.SelectedIndex > 0 ? (object)DD_Serial_No.SelectedValue : DBNull.Value },
                    //{ "@Customer_ID", DD_Customer_Name.SelectedIndex > 0 ? (object)DD_Customer_Name.SelectedValue : DBNull.Value },
                    //{ "@WRN_No", DD_WRN_No.SelectedIndex > 0 ? (object)DD_WRN_No.SelectedValue : DBNull.Value },
                    //{ "@Ward_ID", DD_Ward.SelectedIndex > 0 ? (object)DD_Ward.SelectedValue : DBNull.Value },
                    //{ "@Sector_ID", DD_Sector.SelectedIndex > 0 ? (object)DD_Sector.SelectedValue : DBNull.Value },
                };

                ds = executeClass.Get_DataSet_From_StoredProcedure("USP_GET_DD_CustomerMasterUpdate", parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    // list no, serial no, customer name, wrn no
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        executeClass.Bind_Dropdown_With_DT(DD_List_No, dt, "List_No", "List_No");
                        executeClass.Bind_Dropdown_With_DT(DD_Serial_No, dt, "Serial_No", "Serial_No");
                        executeClass.Bind_Dropdown_With_DT(DD_Customer_Name, dt, "Customer_Name", "Customer_ID");
                        executeClass.Bind_Dropdown_With_DT(DD_WRN_No, dt, "WRN_No", "WRN_No");
                        //executeClass.Bind_Dropdown_With_DT(DD_Ward, dt, "WardName", "Ward_ID");
                        //executeClass.Bind_Dropdown_With_DT(DD_Sector, dt, "SectorName", "Sector_ID");
                    }
                    else
                    {
                        DD_List_No.Items.Clear();
                        DD_Serial_No.Items.Clear();
                        DD_Customer_Name.Items.Clear();
                        DD_WRN_No.Items.Clear();
                    }

                    // voting booth & voting room
                    dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        executeClass.Bind_Dropdown_With_DT(DD_Voting_Booth, dt, "Voting_Booth", "Voting_Booth_ID");
                        executeClass.Bind_Dropdown_With_DT(DD_Voting_Room, dt, "Voting_Room", "Voting_Room_ID");
                    }
                    else
                    {
                        DD_Voting_Booth.Items.Clear();
                        DD_Voting_Room.Items.Clear();
                    }
                }
                else
                {
                    DD_List_No.Items.Clear();
                    DD_Serial_No.Items.Clear();
                    DD_Customer_Name.Items.Clear();
                    DD_WRN_No.Items.Clear();
                    DD_Voting_Booth.Items.Clear();
                    DD_Voting_Room.Items.Clear();
                }
            }
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
        Dictionary<string, object> parameters;
        string sql = string.Empty;

        try
        {
            parameters = new Dictionary<string, object> { { "@User_ID", Session["User_ID"] }, };
            dt = executeClass.Get_DataTable_From_StoredProcedure("USP_Get_User_Hierarchy_Level", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                parameters = new Dictionary<string, object>
                {
                    // top dynamic desired record count
                    { "@Record_Count", DBNull.Value },

                    // work area parameters
                    { "@Assembly_IDs", Session["Assembly_ID"].ToString() },
                    { "@Ward_IDs", Session["Ward_ID"].ToString() },
                    { "@Sector_IDs", Session["Sector_ID"].ToString() },
                    { "@Level_ID", dt.Rows[0]["Level_ID"].ToString() },

                    // dropdown search parameters
                    { "@List_No", DD_List_No.SelectedIndex > 0 ? (object)DD_List_No.SelectedValue : DBNull.Value },
                    { "@Serial_No", DD_Serial_No.SelectedIndex > 0 ? (object)DD_Serial_No.SelectedValue : DBNull.Value },
                    { "@Customer_ID", DD_Customer_Name.SelectedIndex > 0 ? (object)DD_Customer_Name.SelectedValue : DBNull.Value },
                    { "@WRN_No", DD_WRN_No.SelectedIndex > 0 ? (object)DD_WRN_No.SelectedValue : DBNull.Value },
                    { "@DD_Voting_Booth", DD_Voting_Booth.SelectedIndex > 0 ? (object)DD_Voting_Booth.SelectedValue : DBNull.Value },
                    { "@DD_Voting_Room", DD_Voting_Room.SelectedIndex > 0 ? (object)DD_Voting_Room.SelectedValue : DBNull.Value },
                };

                dt = executeClass.Get_DataTable_From_StoredProcedure("USP_GET_GRID_Customer_Booth", parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Grid_Search.DataSource = dt;
                    Grid_Search.DataBind();
                    ViewState["Customer_DT"] = dt;
                }
                else
                {
                    Grid_Search.DataSource = null;
                    Grid_Search.DataBind();
                    ViewState["Customer_DT"] = null;
                }
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

            string Customer_ID = Grid_Search.SelectedDataKey["Customer_ID"].ToString();
            ViewState["Customer_ID"] = Customer_ID;

            //string SerNo = Grid_Search.SelectedRow.Cells[0].Text;
            //string ID = Grid_Search.SelectedRow.Cells[1].Text;
            //Input_Main_Column_1.Text = Grid_Search.SelectedRow.Cells[2].Text;
            //Input_Main_Column_2.Text = Grid_Search.SelectedRow.Cells[3].Text;
            //Input_Main_Column_3.Text = Grid_Search.SelectedRow.Cells[4].Text;

            // redirecting to user creation page with encrypted ID in the url
            string encrypted_ID = EncryptionHelper.Encrypt_UrlSafe(Customer_ID);

            string script = $"openModal('Transaction_Pages/Modal/Booth_Master_Modal.aspx?ID={HttpUtility.UrlEncode(encrypted_ID)}', '40%', '90%');";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OpenModalScript", script, true);

            //Response.Redirect(GetRouteUrl("Booth_Details_Modal_Route", new { Customer_ID = HttpUtility.UrlEncode(encrypted_ID) }), false);
            //Context.ApplicationInstance.CompleteRequest();
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

    protected void Btn_New_Record_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("Customer_Master_Route", new { User_ID = "" }));
    }

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("Booth_Master_Route", null));
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        Bind_Grid();
    }




}