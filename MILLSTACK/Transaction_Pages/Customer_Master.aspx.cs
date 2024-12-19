using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Pages_Customer_Master : System.Web.UI.Page
{
    #region [ Global Declaration ]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Bind_Dropdown();

                if (Page.RouteData.Values["Customer_ID"] is string encrypted_ID && !string.IsNullOrWhiteSpace(encrypted_ID))
                {
                    ViewState["OPERATION"] = "UPDATE";
                    string Decrypted_ID = EncryptionHelper.Decrypt_UrlSafe(this.Page, HttpUtility.UrlDecode(encrypted_ID));
                    if (Int64.TryParse(Decrypted_ID, out Int64 Customer_ID))
                    {
                        AutoFill_UserRecord(Customer_ID);
                        ViewState["Customer_ID"] = Customer_ID;
                    }
                }
                else
                {
                    ViewState["OPERATION"] = "INSERT";
                    Page_Heading.Text = $"Customer Creation";
                    Btn_Submit.Text = $"Save";
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }



    //-------------------------- Dropdown Bind --------------------------
    private void Bind_Dropdown()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters;

        try
        {
            parameters = new Dictionary<string, object> { /*{ "@Approval_Stage_ID", ViewState["Approval_Stage_ID"] },*/ };
            ds = executeClass.Get_DataSet_From_StoredProcedure("USO_GET_DDs_Customer_Creation", parameters);
            if(ds != null && ds.Tables.Count > 0)
            {
                // gender
                dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0) executeClass.Bind_Dropdown_With_DT(DD_Gender, dt, "GenderName", "Gender_ID", parameters, multiple: false);

                // customer type
                dt = ds.Tables[1];
                if (dt != null && dt.Rows.Count > 0) executeClass.Bind_Dropdown_With_DT(DD_Customer_Type, dt, "CustomerName", "CustomerType_ID", parameters, multiple: false);

                // assembly
                dt = ds.Tables[2];
                if (dt != null && dt.Rows.Count > 0) executeClass.Bind_Dropdown_With_DT(DD_Assembly, dt, "AssemblyName", "Assembly_ID", parameters, multiple: false);
            }
            else
            {
                DD_Gender.Items.Clear();
                DD_Customer_Type.Items.Clear();
                DD_Assembly.Items.Clear();
                DD_Ward.Items.Clear();
                DD_Sector.Items.Clear();
                DD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }




    //-------------------------- Dropdown Event --------------------------
    protected void DD_Assembly_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters;

        try
        {
            if(DD_Assembly.SelectedIndex > 0)
            {
                parameters = new Dictionary<string, object>
                {
                    { "@DD_Assembly", DD_Assembly.SelectedValue },
                    { "@DD_Ward", DBNull.Value },
                    { "@DD_Sector", DBNull.Value },
                };
                ds = executeClass.Get_DataSet_From_StoredProcedure("USP_GET_DDE_CustomerCreation", parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    // ward
                    dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0) executeClass.Bind_Dropdown_With_DT(DD_Ward, dt, "WardName", "Ward_ID", parameters, multiple: false);
                }
            }
            else
            {
                DD_Ward.Items.Clear();
                DD_Sector.Items.Clear();
                DD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }

    protected void DD_Ward_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters;

        try
        {
            if (DD_Ward.SelectedIndex > 0)
            {
                parameters = new Dictionary<string, object>
                {
                    { "@DD_Assembly", DBNull.Value },
                    { "@DD_Ward", DD_Ward.SelectedValue },
                    { "@DD_Sector", DBNull.Value },
                };
                ds = executeClass.Get_DataSet_From_StoredProcedure("USP_GET_DDE_CustomerCreation", parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    // sector
                    dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0) executeClass.Bind_Dropdown_With_DT(DD_Sector, dt, "SectorName", "Sector_ID", parameters, multiple: false);
                }
            }
            else
            {
                DD_Sector.Items.Clear();
                DD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }

    protected void DD_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters;

        try
        {
            if (DD_Sector.SelectedIndex > 0)
            {
                parameters = new Dictionary<string, object>
                {
                    { "@DD_Assembly", DBNull.Value },
                    { "@DD_Ward", DBNull.Value },
                    { "@DD_Sector", DD_Sector.SelectedValue },
                };
                ds = executeClass.Get_DataSet_From_StoredProcedure("USP_GET_DDE_CustomerCreation", parameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    // society
                    dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0) executeClass.Bind_Dropdown_With_DT(DD_Society, dt, "SocietyName", "Society_ID", parameters, multiple: false);
                }
            }
            else
            {
                DD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }




    //-------------------------- Auto-Fill Data --------------------------
    private void AutoFill_UserRecord(Int64 Customer_ID)
    {
        DataSet ds = new DataSet();
        string sql = string.Empty;
        Dictionary<string, object> parameters = new Dictionary<string, object>();

        try
        {
            parameters = new Dictionary<string, object>
            {
                { "@Customer_ID", Customer_ID },
            };

            ds = executeClass.Get_DataSet_From_StoredProcedure("USP_Get_Customer_By_ID", parameters);
            if (ds != null && ds.Tables.Count > 0)
            {
                // customer details
                DataTable customer_DT = ds.Tables[0];
                if (customer_DT != null && customer_DT.Rows.Count > 0)
                {
                    Txt_Customer_Name.Text = customer_DT.Rows[0]["Customer_Name"].ToString();
                    Txt_Customer_Mobile.Text = customer_DT.Rows[0]["Customer_MobileNo"].ToString();
                    Txt_List_No.Text = customer_DT.Rows[0]["List_No"].ToString();
                    Txt_Serial_No.Text = customer_DT.Rows[0]["Serial_No"].ToString();
                    Txt_WRN_No.Text = customer_DT.Rows[0]["WRN_No"].ToString();
                    Txt_Voting_Booth.Text = customer_DT.Rows[0]["Voting_Booth"].ToString();
                    Txt_Voting_Room.Text = customer_DT.Rows[0]["Voting_Room"].ToString();

                    Txt_Data_Entry_Mode.Text = customer_DT.Rows[0]["Data_Entry_Mode"].ToString();

                    DD_Gender.SelectedValue = customer_DT.Rows[0]["Gender_ID"].ToString();
                    DD_Customer_Type.SelectedValue = customer_DT.Rows[0]["CustomerType_ID"].ToString();

                    // customer paid boolean
                    bluetooth.Checked = true; /*Convert.ToBoolean(customer_DT.Rows[0]["Gender_ID"]);*/

                    // customer location details
                    DD_Assembly.SelectedValue = customer_DT.Rows[0]["Assembly_ID"].ToString();
                    DD_Assembly_SelectedIndexChanged(null, null);

                    DD_Ward.SelectedValue = customer_DT.Rows[0]["Ward_ID"].ToString();
                    DD_Ward_SelectedIndexChanged(null, null);

                    DD_Sector.SelectedValue = customer_DT.Rows[0]["Sector_ID"].ToString();
                    DD_Sector_SelectedIndexChanged(null, null);

                    DD_Society.SelectedValue = customer_DT.Rows[0]["Society_ID"].ToString();
                }

                ViewState["Customer_DT"] = customer_DT;
                ViewState["OPERATION"] = "UPDATE";
                Page_Heading.Text = $"Customer Update";
                Btn_Submit.Text = $"Update";
            }
            else
            {
                ViewState["OPERATION"] = "INSERT";
                Page_Heading.Text = $"Customer Creation";
                Btn_Submit.Text = $"Save";
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }



    //-------------------------- Save / Update Event --------------------------
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("Customer_Master_Update_Route", null));
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            // user inputs
            string Customer_Name = Txt_Customer_Name.Text.Trim();
            string Customer_MobileNo = Txt_Customer_Mobile.Text.Trim();
            string List_No = Txt_List_No.Text.Trim();
            string Serial_No = Txt_Serial_No.Text.Trim();
            string Wrn_No = Txt_WRN_No.Text.Trim();
            string Voting_Booth = Txt_Voting_Booth.Text.Trim();
            string Voting_Room = Txt_Voting_Room.Text.Trim();
            string Data_Entry_Mode = Txt_Data_Entry_Mode.Text.Trim();

            string Customer_Done = bluetooth.Value;

            string Customer_Gender = DD_Gender.SelectedValue;
            string Customer_Type = DD_Customer_Type.SelectedValue;

            string Assembly = DD_Assembly.SelectedValue;
            string Ward = DD_Ward.SelectedValue;
            string Sector = DD_Sector.SelectedValue;
            string Society = DD_Society.SelectedValue;

            string OperationStatus = string.IsNullOrEmpty(ViewState["OPERATION"]?.ToString()) ? string.Empty : ViewState["OPERATION"].ToString();

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Operation", OperationStatus },
                { "@Customer_ID", ViewState["OPERATION"].ToString() == "INSERT" ? (object)DBNull.Value : ViewState["Customer_ID"] },
                { "@Customer_Name", Customer_Name },
                { "@Customer_MobileNo", Customer_MobileNo },
                { "@List_No", List_No },
                { "@Serial_No", Serial_No },
                { "@Wrn_No", Wrn_No },
                { "@Voting_Booth", Voting_Booth },
                { "@Voting_Room", Voting_Room },
                { "@Data_Entry_Mode", Data_Entry_Mode },
                { "@Customer_Done", Customer_Done },
                { "@Customer_Gender", Customer_Gender },
                { "@Customer_Type", Customer_Type },
                { "@Assembly", Assembly },
                { "@Ward", Ward },
                { "@Sector", Sector },
                { "@Society", Society },
                { "@SavedBy", Session["User_ID"].ToString() },
            };

            // drop and create new customer table with additional required columns
            // IsApproved
            // IsVerified
            // IsActive
            // Customer_Done ***
            // etc

            executeClass.ExecuteStoredProcedure(this.Page, "USP_IU_Customer", parameters);

            string iconType, mssg, redirect;

            if (ViewState["OPERATION"].ToString() == "INSERT")
            {
                iconType = $@"success";
                mssg = $@"Customer : <b>{Customer_Name}</b> created susccesfully !";
            }
            else
            {
                iconType = $@"info";
                mssg = $@"Customer : <b>{Customer_Name}</b> updated susccesfully !";
            }

            SweetAlert.GetSweet(this.Page, iconType, $"", mssg, GetRouteUrl("Customer_Master_Update_Route", null));
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }






    
}