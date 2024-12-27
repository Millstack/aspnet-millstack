using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Pages_Modal_Booth_Master_Modal : System.Web.UI.Page
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

                //if (Page.RouteData.Values["Customer_ID"] is string encrypted_ID && !string.IsNullOrWhiteSpace(encrypted_ID))
                //{
                //    ViewState["OPERATION"] = "UPDATE";
                //    string Decrypted_ID = EncryptionHelper.Decrypt_UrlSafe(this.Page, HttpUtility.UrlDecode(encrypted_ID));
                //    if (Int64.TryParse(Decrypted_ID, out Int64 Customer_ID))
                //    {
                //        //AutoFill_UserRecord(Customer_ID);
                //        ViewState["Customer_ID"] = Customer_ID;
                //    }
                //}
                //else
                //{
                //    ViewState["OPERATION"] = "INSERT";
                //    Page_Heading.Text = $"Booth Details";
                //    //Btn_Submit.Text = $"Save";
                //}
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
                }
            }
            else
            {
                ViewState["OPERATION"] = "INSERT";
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }



    //-------------------------- Close Event --------------------------
    protected void Btn_Close_Click(object sender, EventArgs e)
    {

    }


}