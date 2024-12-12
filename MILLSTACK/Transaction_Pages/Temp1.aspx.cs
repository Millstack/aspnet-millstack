using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Pages_Temp1 : System.Web.UI.Page
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
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        Dictionary<string, object> parameters;
        string sql = string.Empty;

        try
        {
            parameters = new Dictionary<string, object> { /*{ "@Approval_Stage_ID", ViewState["Approval_Stage_ID"] },*/ };
            ds = executeClass.Get_DataSet_From_StoredProcedure("USO_GET_DDs_Customer_Creation", parameters);
            if (ds != null && ds.Tables.Count > 0)
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
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }


}