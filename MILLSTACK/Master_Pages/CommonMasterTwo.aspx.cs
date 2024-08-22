using CommonClassLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
                //SweetAlert.GetSweet(this.Page, "success", $"Sweet alert test", $"getting sweet alert", GetRouteUrl("CommonMaster_Route", new { Page = "Division" }));

                if (Page.RouteData.Values["Page"] != null)
                {
                    Decide_Page();
                    Decide_Input_Fields();
                    Bind_Dropdown();
                    Bind_Grid();

                    ViewState["Operation"] = "INSERT";
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
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

        // retaining the boolean values
        ViewState["Is_Main_column_1_needed"] = Is_Main_column_1_needed;
        ViewState["Is_Main_column_2_needed"] = Is_Main_column_2_needed;
        ViewState["Is_Main_column_3_needed"] = Is_Main_column_3_needed;

        ViewState["Is_Foreign_column_1_needed"] = Is_Foreign_column_1_needed;
        ViewState["Is_Foreign_column_2_needed"] = Is_Foreign_column_2_needed;
        ViewState["Is_Foreign_column_3_needed"] = Is_Foreign_column_3_needed;

        ViewState["Main_Table_Name"] = Main_Table_Name;
        ViewState["Primary_Key_Column"] = Primary_Key_Column;
        ViewState["Main_Column_1_Name"] = Main_Column_1_Name;
        ViewState["Main_Column_2_Name"] = Main_Column_2_Name;
        ViewState["Main_Column_3_Name"] = Main_Column_3_Name;

        ViewState["Foreign_Table_1_Name"] = Foreign_Table_1_Name;
        ViewState["Foreign_Table_1_Key_Column"] = Foreign_Table_1_Key_Column;
        ViewState["Foreign_Table_1_Column_Name"] = Foreign_Table_1_Column_Name;

        ViewState["Foreign_Table_2_Name"] = Foreign_Table_2_Name;
        ViewState["Foreign_Table_2_Key_Column"] = Foreign_Table_2_Key_Column;
        ViewState["Foreign_Table_2_Column_Name"] = Foreign_Table_2_Column_Name;

        ViewState["Foreign_Table_3_Name"] = Foreign_Table_3_Name;
        ViewState["Foreign_Table_3_Key_Column"] = Foreign_Table_3_Key_Column;
        ViewState["Foreign_Table_3_Column_Name"] = Foreign_Table_3_Column_Name;
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
        try
        {
            // re-assigning the boolean and string variabls
            Is_Main_column_1_needed = (bool)ViewState["Is_Main_column_1_needed"];
            Is_Main_column_2_needed = (bool)ViewState["Is_Main_column_2_needed"];
            Is_Main_column_3_needed = (bool)ViewState["Is_Main_column_3_needed"];

            Is_Foreign_column_1_needed = (bool)ViewState["Is_Foreign_column_1_needed"];
            Is_Foreign_column_2_needed = (bool)ViewState["Is_Foreign_column_2_needed"];
            Is_Foreign_column_3_needed = (bool)ViewState["Is_Foreign_column_3_needed"];

            Main_Table_Name = (string)ViewState["Main_Table_Name"];
            Primary_Key_Column = (string)ViewState["Primary_Key_Column"];
            Main_Column_1_Name = (string)ViewState["Main_Column_1_Name"];
            Main_Column_2_Name = (string)ViewState["Main_Column_2_Name"];
            Main_Column_3_Name = (string)ViewState["Main_Column_3_Name"];

            Foreign_Table_1_Name = (string)ViewState["Foreign_Table_1_Name"];
            Foreign_Table_1_Key_Column = (string)ViewState["Foreign_Table_1_Key_Column"];
            Foreign_Table_1_Column_Name = (string)ViewState["Foreign_Table_1_Column_Name"];

            Foreign_Table_2_Name = (string)ViewState["Foreign_Table_2_Name"];
            Foreign_Table_2_Key_Column = (string)ViewState["Foreign_Table_2_Key_Column"];
            Foreign_Table_2_Column_Name = (string)ViewState["Foreign_Table_2_Column_Name"];

            Foreign_Table_3_Name = (string)ViewState["Foreign_Table_3_Name"];
            Foreign_Table_3_Key_Column = (string)ViewState["Foreign_Table_3_Key_Column"];
            Foreign_Table_3_Column_Name = (string)ViewState["Foreign_Table_3_Column_Name"];

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
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }


    protected void Grid_Search_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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

            Btn_Submit.Text = "Update";
            ViewState["Operation"] = "UPDATE";
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
            string Primary_ID = Grid_Search.DataKeys[e.RowIndex]["ID"].ToString();

            sqlQuery = $@"UPDATE {Main_Table_Name} SET IsDeleted = 1 WHERE {Primary_Key_Column} = {Primary_ID}";
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





    //================================ Save / Update Button Event ========================================
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        string redirectURL = GetRouteUrl("CommonMaster_Route", new { Page = "Division" });
        Response.Redirect(redirectURL);
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable, "Transaction_");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                string sqlQuery = string.Empty;

                // re-assigning the boolean and string variabls
                Is_Main_column_1_needed = (bool)ViewState["Is_Main_column_1_needed"];
                Is_Main_column_2_needed = (bool)ViewState["Is_Main_column_2_needed"];
                Is_Main_column_3_needed = (bool)ViewState["Is_Main_column_3_needed"];

                Is_Foreign_column_1_needed = (bool)ViewState["Is_Foreign_column_1_needed"];
                Is_Foreign_column_2_needed = (bool)ViewState["Is_Foreign_column_2_needed"];
                Is_Foreign_column_3_needed = (bool)ViewState["Is_Foreign_column_3_needed"];

                Main_Table_Name = (string)ViewState["Main_Table_Name"];
                Primary_Key_Column = (string)ViewState["Primary_Key_Column"];
                Main_Column_1_Name = (string)ViewState["Main_Column_1_Name"];
                Main_Column_2_Name = (string)ViewState["Main_Column_2_Name"];
                Main_Column_3_Name = (string)ViewState["Main_Column_3_Name"];

                Foreign_Table_1_Name = (string)ViewState["Foreign_Table_1_Name"];
                Foreign_Table_1_Key_Column = (string)ViewState["Foreign_Table_1_Key_Column"];
                Foreign_Table_1_Column_Name = (string)ViewState["Foreign_Table_1_Column_Name"];

                Foreign_Table_2_Name = (string)ViewState["Foreign_Table_2_Name"];
                Foreign_Table_2_Key_Column = (string)ViewState["Foreign_Table_2_Key_Column"];
                Foreign_Table_2_Column_Name = (string)ViewState["Foreign_Table_2_Column_Name"];

                Foreign_Table_3_Name = (string)ViewState["Foreign_Table_3_Name"];
                Foreign_Table_3_Key_Column = (string)ViewState["Foreign_Table_3_Key_Column"];
                Foreign_Table_3_Column_Name = (string)ViewState["Foreign_Table_3_Column_Name"];

                string foriegn_Column_Value_1 = DD_Foriegn_Column_1.SelectedValue ?? string.Empty;
                string foriegn_Column_Value_2 = DD_Foriegn_Column_2.SelectedValue ?? string.Empty;
                string foriegn_Column_Value_3 = DD_Foriegn_Column_3.SelectedValue ?? string.Empty;

                string input_Main_Column_1 = Input_Main_Column_1.Text ?? string.Empty;
                string input_Main_Column_2 = Input_Main_Column_2.Text ?? string.Empty;
                string input_Main_Column_3 = Input_Main_Column_3.Text ?? string.Empty;

                string OperationStatus = string.IsNullOrEmpty(ViewState["Operation"]?.ToString()) ? string.Empty : ViewState["Operation"].ToString();

                if (OperationStatus == "UPDATE" && Btn_Submit.Text == "Update")
                {
                    sqlQuery = $@"
                        UPDATE {Main_Table_Name} SET
                            {(Is_Main_column_1_needed ? $"{Main_Column_1_Name} = '{input_Main_Column_1}' " : "")}
                            {(Is_Main_column_2_needed ? $",{Main_Column_2_Name} = N'{input_Main_Column_2}' " : "")}
                            {(Is_Main_column_3_needed ? $",{Main_Column_3_Name} = '{input_Main_Column_3}' " : "")}
                            {(Is_Foreign_column_1_needed ? $",{Foreign_Table_1_Key_Column} = {foriegn_Column_Value_1} " : "")}
                            {(Is_Foreign_column_2_needed ? $",{Foreign_Table_2_Key_Column} = {foriegn_Column_Value_2} " : "")}
                            {(Is_Foreign_column_3_needed ? $",{Foreign_Table_3_Key_Column} = {foriegn_Column_Value_3} " : "")}
                    WHERE {Primary_Key_Column} = {ViewState["ID"].ToString()}";

                    //sqlQuery = sqlQuery.TrimEnd(' ', ',');

                    //sqlQuery = sqlQuery.Replace(", WHERE", " WHERE");
                    //sqlQuery += $" WHERE {Primary_Key_Column} = {ViewState["ID"].ToString()}";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        //new SqlParameter("@SavedBy", savedBy)
                    };

                    executeClass.ExecuteCommand(sqlQuery, command, parameters);

                    SweetAlert.GetSweet(this.Page, "success", "", $"Record updated !!");
                }
                else if (OperationStatus == "INSERT" && Btn_Submit.Text == "Save")
                {
                    string Primary_Key = executeClass.Get_Next_RefID(Main_Table_Name, Primary_Key_Column);

                    Session["User_ID"] = 1;
                    string User_ID = Session["User_ID"].ToString();

                    sqlQuery = $@"
                        INSERT INTO {Main_Table_Name} (
                            {Primary_Key_Column},
                            {(Is_Main_column_1_needed ? $"{Main_Column_1_Name}, " : "")}
                            {(Is_Main_column_2_needed ? $"{Main_Column_2_Name}, " : "")}
                            {(Is_Main_column_3_needed ? $"{Main_Column_3_Name}, " : "")}
                            {(Is_Foreign_column_1_needed ? $"{Foreign_Table_1_Key_Column}, " : "")}
                            {(Is_Foreign_column_2_needed ? $"{Foreign_Table_2_Key_Column}, " : "")}
                            {(Is_Foreign_column_3_needed ? $"{Foreign_Table_3_Key_Column}, " : "")}
                            SavedBy
                        ) VALUES (
                            {Primary_Key},
                            {(Is_Main_column_1_needed ? $"'{input_Main_Column_1}', " : "")}
                            {(Is_Main_column_2_needed ? $"N'{input_Main_Column_2}', " : "")}
                            {(Is_Main_column_3_needed ? $"'{input_Main_Column_3}', " : "")}
                            {(Is_Foreign_column_1_needed ? $"'{foriegn_Column_Value_1}', " : "")}
                            {(Is_Foreign_column_2_needed ? $"'{foriegn_Column_Value_2}', " : "")}
                            {(Is_Foreign_column_3_needed ? $"'{foriegn_Column_Value_3}', " : "")}
                            {User_ID}
                        )";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        //new SqlParameter("@SavedBy", savedBy)
                    };

                    executeClass.ExecuteCommand(sqlQuery, command, parameters);

                    SweetAlert.GetSweet(this.Page, "success", "", $"Record inserted !!");
                }
                else
                {
                    // logical error
                }

                if (transaction.Connection != null)
                {
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
                transaction.Dispose();
            }
        }

        Bind_Grid();
    }






}