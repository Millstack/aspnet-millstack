using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Master_Pages_Dashboard : System.Web.UI.Page
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
                GetDivisionData();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }


    private void GetDivisionData()
    {
        string sql = $@"Select d.DivisionName, 
	                        (Select COUNT(*) From M_District Where Division_ID = d.Division_ID) as DistrictCount
                        From M_Division as d
                        Where d.IsDeleted IS NULL";
        parameters = new Dictionary<string, object> { /*{ "@User_ID", Session["User_ID"] },*/ };
        DataTable dt = executeClass.Get_Datatable(sql, parameters);
        if(dt != null && dt.Rows.Count > 0 )
        {
            //Repeater_Dashboard.DataSource = dt;
            //Repeater_Dashboard.DataBind();
        }
    }


}