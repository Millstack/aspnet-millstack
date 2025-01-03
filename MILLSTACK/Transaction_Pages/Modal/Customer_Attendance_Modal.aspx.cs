using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Pages_Modal_Customer_Attendance_Modal : System.Web.UI.Page
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
            if (Session["User_ID"] == null)
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect(GetRouteUrl("LoginRoute", null));
            }

            if (!IsPostBack)
            {
                //Bind_Dropdown();

                if (Request.QueryString.Count != 0)
                {
                    string encrypted_ID = Request.QueryString["ID"];

                    if (!string.IsNullOrWhiteSpace(encrypted_ID))
                    {
                        string Decrypted_ID = EncryptionHelper.Decrypt_UrlSafe(this.Page, HttpUtility.UrlDecode(encrypted_ID));
                        if (Int64.TryParse(Decrypted_ID, out Int64 Customer_ID))
                        {
                            AutoFill_UserRecord(Customer_ID);
                            ViewState["Customer_ID"] = Customer_ID;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
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
                    Txt_WRN_No.Text = customer_DT.Rows[0]["WRN_No"].ToString();
                    Txt_List_No.Text = customer_DT.Rows[0]["List_No"].ToString();
                    Txt_Serial_No.Text = customer_DT.Rows[0]["Serial_No"].ToString();
                    Txt_Voting_Booth.Text = customer_DT.Rows[0]["Voting_Booth"].ToString();
                    Txt_Voting_Room.Text = customer_DT.Rows[0]["Voting_Room"].ToString();
                    bluetooth.Checked = Convert.ToBoolean(customer_DT.Rows[0]["IsPresent"]);
                }

                ViewState["Customer_DT"] = customer_DT;
                ViewState["OPERATION"] = "UPDATE";
                Page_Heading.Text = $"Attendance Status";
                Btn_Submit.Text = $"Update";
            }
            else
            {
                ViewState["OPERATION"] = "INSERT";
                Page_Heading.Text = $"Attendance Status";
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
        Response.Redirect(GetRouteUrl("Customer_Attendance_Route", null));
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        // user inputs
        string Customer_Name = Txt_Customer_Name.Text.Trim();
        string List_No = Txt_List_No.Text.Trim();
        string Serial_No = Txt_Serial_No.Text.Trim();
        string Wrn_No = Txt_WRN_No.Text.Trim();
        string Voting_Booth = Txt_Voting_Booth.Text.Trim();
        string Voting_Room = Txt_Voting_Room.Text.Trim();

        int Customer_IsPresent = bluetooth.Checked ? 1 : 0;

        string OperationStatus = string.IsNullOrEmpty(ViewState["OPERATION"]?.ToString()) ? string.Empty : ViewState["OPERATION"].ToString();

        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@Operation", OperationStatus },
            { "@Customer_ID", ViewState["OPERATION"].ToString() == "INSERT" ? (object)DBNull.Value : ViewState["Customer_ID"] },
            { "@Customer_IsPresent", Customer_IsPresent },
            { "@SavedBy", Session["User_ID"].ToString() },
        };

        executeClass.ExecuteStoredProcedure(this.Page, "USP_IU_Customer_Attendance_Status", parameters);

        string iconType, mssg, redirect;

        if (ViewState["OPERATION"].ToString() == "INSERT")
        {
            iconType = $@"success";
            mssg = $@"Attendance status update susccesfully for customer : <b>{Customer_Name}</b>";
        }
        else
        {
            iconType = $@"info";
            mssg = $@"Attendance status update susccesfully for customer : <b>{Customer_Name}</b>";
        }

        //SweetAlert.GetSweet(this.Page, iconType, $"", mssg, GetRouteUrl("Customer_Attendance_Route", null));

        SweetAlert.GetSweet_ModaL(
            this.Page, 
            iconType, $"", mssg, 
            redirectUrl: GetRouteUrl("Customer_Attendance_Route", null),
            closeModal: true,
            reloadPage: true
         );
    }


}