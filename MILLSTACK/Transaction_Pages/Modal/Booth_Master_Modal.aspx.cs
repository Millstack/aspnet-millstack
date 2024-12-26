using CommonClassLibrary;
using System;
using System.Collections.Generic;
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
                            //AutoFill_UserRecord(Customer_ID);
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
}