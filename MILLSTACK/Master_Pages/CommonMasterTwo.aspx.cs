using CommonClassLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_CommonMasterTwo : System.Web.UI.Page
{
    #region [GLobal Declaration]

    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();

    Boolean Is_Main_column_1_needed, Is_Main_column_2_needed, Is_Main_column_3_needed,
            Is_Foreign_column_1_needed, Is_Foreign_column_2_needed, Is_Foreign_column_3_needed = false;

    string sqlQuery = string.Empty;

    static string
        Main_Table_Name = "", Primary_Key_Column = "", Main_Column_1_Name = "", Main_Column_2_Name = "", Main_Column_3_Name = "",
        Foreign_Table_1_Name = "", Foreign_Table_1_Key_Column = "", Foreign_Table_1_Column_Name = "",
        Foreign_Table_2_Name = "", Foreign_Table_2_Key_Column = "", Foreign_Table_2_Column_Name = "",
        Foreign_Table_3_Name = "", Foreign_Table_3_Key_Column = "", Foreign_Table_3_Column_Name = "";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values["Page"] != null)
                {
                    Decide_Page();
                    Decide_Input_Fields();
                    Bind_Dropdown();
                    Bind_Grid();
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured");
        }
    }


    //================================ Page Type ========================================
    private void Decide_Page()
    {
        Page_Heading.Text = "Division Master";
        Main_Heading_1.Text = "Division Details";
        Main_Heading_2.Text = "Division Records";

        if (Page.RouteData.Values["Page"].ToString().Trim() == "Division")
        {
            // Serial Number
            Grid_Search.Columns[0].HeaderText = "Ser.No.";

            // Main Table
            Grid_Search.Columns[1].HeaderText = "Division ID";
            Grid_Search.Columns[1].Visible = true;
            Main_Table_Name = "M_Division";
            Primary_Key_Column = "Division_ID";

            // Main Table Column 1
            Grid_Search.Columns[2].HeaderText = "Division";
            Is_Main_column_1_needed = true;
            Main_Column_1_Text.Text = "Division Name";
            Main_Column_1_Name = "DivisionName";

            // Main Table Column 2
            Grid_Search.Columns[3].HeaderText = "Division (Marathi)";
            Is_Main_column_2_needed = true;
            Main_Column_2_Text.Text = "Division (Marathi)";
            Main_Column_2_Name = "DivisionNameMr";

            // Main Table Column 3
            Grid_Search.Columns[4].HeaderText = "Division Code";
            Is_Main_column_3_needed = true;
            Main_Column_3_Text.Text = "Division Code";
            Main_Column_3_Name = "DivisionCode";

            // Foreign Table 1
            Grid_Search.Columns[5].HeaderText = "State";
            Is_Foreign_column_1_needed = true;
            Foreign_Table_1_Name = "M_State";
            Foreign_Table_1_Key_Text.Text = "State Name";
            Foreign_Table_1_Column_Name = "StateName";
            Foreign_Table_1_Key_Column = "State_ID";
        }
    }

    private void Decide_Input_Fields()
    {
        if (Is_Main_column_1_needed) // Main Table Column 1
        {
            Div_Main_Table_Column_1.Visible = true;
            RFV_Main_Table_Column_1.Enabled = true;
            Grid_Search.Columns[2].Visible = true;
        }
        else
        {
            Div_Main_Table_Column_1.Visible = false;
            RFV_Main_Table_Column_1.Enabled = false;
            Grid_Search.Columns[2].Visible = false;
        }

        if (Is_Main_column_2_needed) // Main Table Column 2
        {
            Div_Main_Table_Column_2.Visible = true;
            RFV_Main_Table_Column_2.Enabled = true;
            Grid_Search.Columns[3].Visible = true;
        }
        else
        {
            Div_Main_Table_Column_2.Visible = false;
            RFV_Main_Table_Column_2.Enabled = false;
            Grid_Search.Columns[3].Visible = false;
        }

        if (Is_Main_column_3_needed) // Main Table Column 3
        {
            Div_Main_Table_Column_3.Visible = true;
            RFV_Main_Table_Column_3.Enabled = true;
            Grid_Search.Columns[4].Visible = true;
        }
        else
        {
            Div_Main_Table_Column_3.Visible = false;
            RFV_Main_Table_Column_3.Enabled = false;
            Grid_Search.Columns[4].Visible = false;
        }

        if (Is_Foreign_column_1_needed) // Foreign Table 1
        {
            Div_Foriegn_DD_1.Visible = true;
            RFV_DD_Foreign_Column_1.Enabled = true;
            Grid_Search.Columns[5].Visible = true;
        }
        else
        {
            Div_Foriegn_DD_1.Visible = false;
            RFV_DD_Foreign_Column_1.Enabled = false;
            Grid_Search.Columns[5].Visible = false;
        }

        if (Is_Foreign_column_2_needed) // Foreign Table 2
        {
            Div_Foriegn_DD_2.Visible = true;
            RFV_DD_Foreign_Column_2.Enabled = true;
            Grid_Search.Columns[6].Visible = true;
        }
        else
        {
            Div_Foriegn_DD_2.Visible = false;
            RFV_DD_Foreign_Column_2.Enabled = false;
            Grid_Search.Columns[6].Visible = false;
        }

        if (Is_Foreign_column_3_needed) // Foreign Table 3
        {
            Div_Foriegn_DD_3.Visible = true;
            RFV_DD_Foreign_Column_3.Enabled = true;
            Grid_Search.Columns[7].Visible = true;
        }
        else
        {
            Div_Foriegn_DD_3.Visible = false;
            RFV_DD_Foreign_Column_3.Enabled = false;
            Grid_Search.Columns[7].Visible = false;
        }
    }



    //================================ Dropdown Bind ========================================

    private void Bind_Dropdown()
    {
        sqlQuery = $@"Select {Foreign_Table_1_Key_Column} as ID, {Foreign_Table_1_Column_Name} as Value 
                      From {Foreign_Table_1_Name} 
                      Where IsDeleted IS NULL 
                      Order By {Foreign_Table_1_Column_Name}";

        parameters = new Dictionary<string, object>
        {
            //{ "@Bank_ID", DD_Bank_Master.SelectedValue },
        };

        executeClass.Bind_Dropdown_Generic(DD_Foriegn_Column_1, sqlQuery, "Value", "ID", parameters);
    }




    //================================ Dropdown Event ========================================
    protected void DD_Foriegn_Column_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid();
    }

    protected void DD_Foriegn_Column_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid();
    }

    protected void DD_Foriegn_Column_3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Grid();
    }





    //================================ Grid Bind ========================================

    private void Bind_Grid()
    {
        string foriegn_Dropdown_1 = DD_Foriegn_Column_1.SelectedValue ?? "";
        string foriegn_Dropdown_2 = DD_Foriegn_Column_2.SelectedValue ?? "";
        string foriegn_Dropdown_3 = DD_Foriegn_Column_3.SelectedValue ?? "";

        sqlQuery = $@"
                    SELECT 
                        {Main_Table_Name}.{Primary_Key_Column} AS ID, 
                        {Main_Table_Name}.{Main_Column_1_Name} AS Main_Column_1, 
                        {Main_Table_Name}.{Main_Column_2_Name} AS Main_Column_2, 
                        {Main_Table_Name}.{Main_Column_3_Name} AS Main_Column_3, 
                        {(Is_Foreign_column_1_needed ? $"{Foreign_Table_1_Name}.{Foreign_Table_1_Column_Name} AS Foreign_Column_1, " : "NULL AS Foreign_Column_1, ")} 
                        {(Is_Foreign_column_2_needed ? $"{Foreign_Table_2_Name}.{Foreign_Table_2_Column_Name} AS Foreign_Column_2, " : "NULL AS Foreign_Column_2, ")} 
                        {(Is_Foreign_column_3_needed ? $"{Foreign_Table_3_Name}.{Foreign_Table_3_Column_Name} AS Foreign_Column_3, " : "NULL AS Foreign_Column_3, ")} 
                        NULL AS DummyColumn 
                    FROM {Main_Table_Name} 
                    {(Is_Foreign_column_1_needed ? $@"INNER JOIN {Foreign_Table_1_Name} ON {Foreign_Table_1_Name}.{Foreign_Table_1_Key_Column} = {Main_Table_Name}.{Foreign_Table_1_Key_Column} " : "")} 
                    {(Is_Foreign_column_2_needed ? $@"INNER JOIN {Foreign_Table_2_Name} ON {Foreign_Table_2_Name}.{Foreign_Table_2_Key_Column} = {Main_Table_Name}.{Foreign_Table_2_Key_Column} " : "")} 
                    {(Is_Foreign_column_3_needed ? $@"INNER JOIN {Foreign_Table_3_Name} ON {Foreign_Table_3_Name}.{Foreign_Table_3_Key_Column} = {Main_Table_Name}.{Foreign_Table_3_Key_Column} " : "")} 
                    WHERE {Main_Table_Name}.IsDeleted IS NULL
                    {(DD_Foriegn_Column_1.SelectedIndex > 0 ? $@" AND {Foreign_Table_1_Name}.{Foreign_Table_1_Key_Column} = {DD_Foriegn_Column_1.SelectedValue}" : "")}
                    {(DD_Foriegn_Column_2.SelectedIndex > 0 ? $@" AND {Foreign_Table_2_Name}.{Foreign_Table_2_Key_Column} = {DD_Foriegn_Column_2.SelectedValue}" : "")}
                    {(DD_Foriegn_Column_3.SelectedIndex > 0 ? $@" AND {Foreign_Table_3_Name}.{Foreign_Table_3_Key_Column} = {DD_Foriegn_Column_3.SelectedValue}" : "")}
                    ORDER BY {Main_Table_Name}.{Main_Column_1_Name}";

        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            //{ "@Bank_ID", DD_Bank_Master.SelectedValue },
        };

        DataTable dt = executeClass.Get_Datatable(sqlQuery);
        if (dt != null && dt.Rows.Count > 0)
        {
            Grid_Search.DataSource = dt;
            Grid_Search.DataBind();
            ViewState["Grid_Common_DT"] = dt;
        }
        else
        {
            Grid_Search.DataSource = null;
            Grid_Search.DataBind();
            ViewState["Grid_Common_DT"] = null;
        }
    }


    protected void Grid_Search_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;

        string Primary_Key = Grid_Search.SelectedDataKey["ID"].ToString();
        ViewState["ID"] = Primary_Key;

        string SerNo = Grid_Search.SelectedRow.Cells[0].Text;
        string ID = Grid_Search.SelectedRow.Cells[1].Text;
        Input_Main_Column_1.Text = Grid_Search.SelectedRow.Cells[2].Text;
        Input_Main_Column_2.Text = Grid_Search.SelectedRow.Cells[3].Text;
        Input_Main_Column_3.Text = Grid_Search.SelectedRow.Cells[4].Text;

        // Foreign Dropdown 1
        string sqlQuery_Foriegn_Table_1 = $@"
                    SELECT {Foreign_Table_1_Key_Column} AS ID
                    FROM {Main_Table_Name} 
                    WHERE {Main_Table_Name}.{Primary_Key_Column} = {Primary_Key}";
        dt = executeClass.Get_Datatable(sqlQuery_Foriegn_Table_1);
        if (dt != null && dt.Rows.Count > 0)
        {
            DD_Foriegn_Column_1.ClearSelection();
            masterClass.Select_Item_In_DropDown(DD_Foriegn_Column_1, dt.Rows[0]["ID"].ToString());
        }

        // Foreign Dropdown 2
        string sqlQuery_Foriegn_Table_2 = $@"
                    SELECT {Foreign_Table_2_Key_Column} AS ID
                    FROM {Main_Table_Name} 
                    WHERE {Main_Table_Name}.{Primary_Key_Column} = {Primary_Key}";
        dt = executeClass.Get_Datatable(sqlQuery_Foriegn_Table_2);
        if (dt != null && dt.Rows.Count > 0)
        {
            DD_Foriegn_Column_2.ClearSelection();
            masterClass.Select_Item_In_DropDown(DD_Foriegn_Column_2, dt.Rows[0]["ID"].ToString());
        }

        // Foreign Dropdown 3
        string sqlQuery_Foriegn_Table_3 = $@"
                    SELECT {Foreign_Table_3_Key_Column} AS ID
                    FROM {Main_Table_Name} 
                    WHERE {Main_Table_Name}.{Primary_Key_Column} = {Primary_Key}";
        dt = executeClass.Get_Datatable(sqlQuery_Foriegn_Table_3);
        if (dt != null && dt.Rows.Count > 0)
        {
            DD_Foriegn_Column_3.ClearSelection();
            masterClass.Select_Item_In_DropDown(DD_Foriegn_Column_3, dt.Rows[0]["ID"].ToString());
        }
    }

    protected void Grid_Search_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Grid_Search_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }





    //================================ Save / Update Button Event ========================================
    protected void Btn_Back_Click(object sender, EventArgs e)
    {

    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        Btn_Submit.Text = "Save";
    }






}