using CommonClassLibrary;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_CommonMasterTwo : System.Web.UI.Page
{
    #region [GLobal Declaration]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClas = new MasterClass();

    String sqlQuery = string.Empty;

    static string
        Main_Table_Name = "", Primary_Key_Column = "", Main_Column_1_Name = "", Main_Column_2_Name = "", Main_Column_3_Name = "",
        Foreign_Table_Name = "", Foreign_Key_Column = "", Foreign_Column_Name = "";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                if (Request.QueryString.Count != 0)
                {
                    Decide_Page();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    //================================ Page Type ========================================

    private void Decide_Page()
    {
        if (Request.QueryString["P"].ToString().Trim() == "Division")
        {
            // current table details
            Main_Table_Name = "Tbl_M_Division";
            Primary_Key_Column = "DivisionID";

            Main_Column_1_Text.Text = "Division";
            Main_Column_1_Name = "Divisions";

            Main_Column_2_Text.Text = "Division (Marathi)";
            Main_Column_2_Name = "Divisions";

            Main_Column_3_Text.Text = "Division Code";
            Main_Column_3_Name = "DivisionCode";

            // foriegn table details
            Foreign_Table_Name = "Tbl_M_State";
            Foreign_Column_Name = "StateName";

            Foreign_Key_Text.Text = "State";
            Foreign_Key_Column = "State_ID";

            // gridview column names
            Grid_Common.Columns[0].HeaderText = "Division ID";
            Grid_Common.Columns[1].HeaderText = "Division";
            Grid_Common.Columns[2].HeaderText = "Division (Marathi)";
            Grid_Common.Columns[3].HeaderText = "Division Code";
            Grid_Common.Columns[4].HeaderText = "State";

            // to hode 2nd input column
            Grid_Common.Columns[2].Visible = true;
        }
    }

}