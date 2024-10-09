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

public partial class Master_Pages_CommonMaster : System.Web.UI.Page
{
    #region [GLobal Declaration]

    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();

    Boolean Is_Main_column_1_needed, Is_Main_column_2_needed, Is_Main_column_3_needed,
            Is_Foreign_column_1_needed, Is_Foreign_column_2_needed, Is_Foreign_column_3_needed = false;

    Boolean Is_Foreign_Dropdown_2_Is_Dependant_On_1, Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;

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
                //SweetAlert.GetSweet(this.Page, "success", $"Sweet alert test", $"getting sweet alert", GetRouteUrl("CommonMaster_Route", new { Page = "Role" }));

                if (Page.RouteData.Values["Page"] != null)
                {
                    ViewState["Operation"] = "INSERT";

                    Decide_Page();
                    Decide_Input_Fields();
                    Bind_Dropdown();
                    Bind_Grid();
                }
            }
            else
            {
                // Call on every postback to handle the search
                //Bind_Grid(); 
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
        try
        {
            if (Page.RouteData.Values["Page"].ToString().Trim() == "Society")
            {
                Page_Heading.Text = "Society Master";
                Main_Heading_1.Text = "Society Details";
                Main_Heading_2.Text = "Society Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Society ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "Tbl_M_Society";
                Primary_Key_Column = "Society_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Society";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Society Name";
                Main_Column_1_Name = "SocietyName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Society (Marathi)";
                Is_Main_column_2_needed = false;
                Main_Column_2_Text.Text = "Society Name (Marathi)";
                Main_Column_2_Name = "SocietyNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Society Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Society Code";
                Main_Column_3_Name = "SocietyCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "Sector";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "Tbl_M_Sector";
                Foreign_Table_1_Key_Text.Text = "Sector Name";
                Foreign_Table_1_Column_Name = "SectorName";
                Foreign_Table_1_Key_Column = "Sector_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Sector")
            {
                Page_Heading.Text = "Sector Master";
                Main_Heading_1.Text = "Sector Details";
                Main_Heading_2.Text = "Sector Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Sector ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "Tbl_M_Sector";
                Primary_Key_Column = "Sector_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Sector / Area";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Sector / Area Name";
                Main_Column_1_Name = "SectorName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Sector / Area (Marathi)";
                Is_Main_column_2_needed = false;
                Main_Column_2_Text.Text = "Sector / Area Name (Marathi)";
                Main_Column_2_Name = "SectorNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Sector / Area Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Sector / Area Code";
                Main_Column_3_Name = "SectorCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "Ward";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "Tbl_M_Ward";
                Foreign_Table_1_Key_Text.Text = "Ward Name";
                Foreign_Table_1_Column_Name = "WardName";
                Foreign_Table_1_Key_Column = "Ward_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Ward")
            {
                Page_Heading.Text = "Ward Master";
                Main_Heading_1.Text = "Ward Details";
                Main_Heading_2.Text = "Ward Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Ward ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "Tbl_M_Ward";
                Primary_Key_Column = "Ward_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Ward";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Ward Name";
                Main_Column_1_Name = "WardName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Ward (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "Ward Name (Marathi)";
                Main_Column_2_Name = "WardNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Ward No";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Ward No";
                Main_Column_3_Name = "WardCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "Assembly";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "Tbl_M_Assembly";
                Foreign_Table_1_Key_Text.Text = "Assembly Name";
                Foreign_Table_1_Column_Name = "AssemblyName";
                Foreign_Table_1_Key_Column = "Assembly_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Assembly")
            {
                Page_Heading.Text = "Assembly Master";
                Main_Heading_1.Text = "Assembly Details";
                Main_Heading_2.Text = "Assembly Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Assembly ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "Tbl_M_Assembly";
                Primary_Key_Column = "Assembly_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Assembly";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Assembly Name";
                Main_Column_1_Name = "AssemblyName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Assembly (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "Assembly Name (Marathi)";
                Main_Column_2_Name = "AssemblyNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Assembly Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Assembly Code";
                Main_Column_3_Name = "AssemblyCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "District";
                Is_Foreign_column_2_needed = true;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_District";
                Foreign_Table_2_Key_Text.Text = "District Name";
                Foreign_Table_2_Column_Name = "DistrictName";
                Foreign_Table_2_Key_Column = "District_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "CustomerType")
            {
                Page_Heading.Text = "Customer Type Master";
                Main_Heading_1.Text = "Customer Type Details";
                Main_Heading_2.Text = "Customer Type Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Customer Type ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "Tbl_M_CustomerType";
                Primary_Key_Column = "CustomerType_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Customer Type";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Customer Type";
                Main_Column_1_Name = "CustomerName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Customer Type (Marathi)";
                Is_Main_column_2_needed = false;
                Main_Column_2_Text.Text = "Customer Type (Marathi)";
                Main_Column_2_Name = "CustomerNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Customer Type Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Customer Type Code";
                Main_Column_3_Name = "CustomerCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = false;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Level")
            {
                Page_Heading.Text = "Level Master";
                Main_Heading_1.Text = "Level Details";
                Main_Heading_2.Text = "Level Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Level ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_Level";
                Primary_Key_Column = "Level_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Level";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Level Type";
                Main_Column_1_Name = "LevelType";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Level (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "Level Type (Marathi)";
                Main_Column_2_Name = "LevelTypeMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Level Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Level Code";
                Main_Column_3_Name = "LevelCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = false;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Gender")
            {
                Page_Heading.Text = "Gender Master";
                Main_Heading_1.Text = "Gender Details";
                Main_Heading_2.Text = "Gender Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Gender ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_Gender";
                Primary_Key_Column = "Gender_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Gender";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Gender Name";
                Main_Column_1_Name = "GenderName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Gender (Marathi)";
                Is_Main_column_2_needed = false;
                Main_Column_2_Text.Text = "Gender (Marathi)";
                Main_Column_2_Name = "GenderNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Gender Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Gender Code";
                Main_Column_3_Name = "GenderCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = false;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Role")
            {
                Page_Heading.Text = "Role Master";
                Main_Heading_1.Text = "Role Details";
                Main_Heading_2.Text = "Role Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Role ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_RoleMaster";
                Primary_Key_Column = "Role_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Role";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Role Name";
                Main_Column_1_Name = "RoleName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Role (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "Role (Marathi)";
                Main_Column_2_Name = "RoleNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Role Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Role Code";
                Main_Column_3_Name = "RoleCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = false;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "UserRole")
            {
                Page_Heading.Text = "UserRole Master";
                Main_Heading_1.Text = "UserRole Details";
                Main_Heading_2.Text = "UserRole Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "UserRole ID";
                Grid_Search.Columns[1].Visible = false;
                Main_Table_Name = "M_Division";
                Primary_Key_Column = "Division_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Division";
                Is_Main_column_1_needed = false;
                Main_Column_1_Text.Text = "Division Name";
                Main_Column_1_Name = "DivisionName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Division (Marathi)";
                Is_Main_column_2_needed = false;
                Main_Column_2_Text.Text = "Division (Marathi)";
                Main_Column_2_Name = "DivisionNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Division Code";
                Is_Main_column_3_needed = false;
                Main_Column_3_Text.Text = "Division Code";
                Main_Column_3_Name = "DivisionCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "User";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "M_UserMaster";
                Foreign_Table_1_Key_Text.Text = "User Name";
                Foreign_Table_1_Column_Name = "UserName";
                Foreign_Table_1_Key_Column = "User_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Role";
                Is_Foreign_column_2_needed = true;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_RoleMaster";
                Foreign_Table_2_Key_Text.Text = "Role Name";
                Foreign_Table_2_Column_Name = "RoleName";
                Foreign_Table_2_Key_Column = "Role_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Country")
            {
                Page_Heading.Text = "Country Master";
                Main_Heading_1.Text = "Country Details";
                Main_Heading_2.Text = "Country Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Country ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_Country";
                Primary_Key_Column = "Country_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Country";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Country Name";
                Main_Column_1_Name = "CountryName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Country (Marathi)";
                Is_Main_column_2_needed = false;
                Main_Column_2_Text.Text = "Country (Marathi)";
                Main_Column_2_Name = "CountryNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Country Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Country Code";
                Main_Column_3_Name = "CountryCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "Country";
                Is_Foreign_column_1_needed = false;
                Foreign_Table_1_Name = "M_Country";
                Foreign_Table_1_Key_Text.Text = "Country Name";
                Foreign_Table_1_Column_Name = "CountryName";
                Foreign_Table_1_Key_Column = "Country_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "State")
            {
                Page_Heading.Text = "State Master";
                Main_Heading_1.Text = "State Details";
                Main_Heading_2.Text = "State Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "State ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_State";
                Primary_Key_Column = "State_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "State";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "State Name";
                Main_Column_1_Name = "StateName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "State (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "State (Marathi)";
                Main_Column_2_Name = "StateNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "State Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "State Code";
                Main_Column_3_Name = "StateCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "Country";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "M_Country";
                Foreign_Table_1_Key_Text.Text = "Country Name";
                Foreign_Table_1_Column_Name = "CountryName";
                Foreign_Table_1_Key_Column = "Country_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Division")
            {
                Page_Heading.Text = "Division Master";
                Main_Heading_1.Text = "Division Details";
                Main_Heading_2.Text = "Division Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

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

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = false;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = false;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "District")
            {
                Page_Heading.Text = "District Master";
                Main_Heading_1.Text = "District Details";
                Main_Heading_2.Text = "District Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "District ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_District";
                Primary_Key_Column = "District_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "District";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "District Name";
                Main_Column_1_Name = "DistrictName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "District (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "District (Marathi)";
                Main_Column_2_Name = "DistrictNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "District Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "District Code";
                Main_Column_3_Name = "DistrictCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = true;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = true;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "Division";
                Is_Foreign_column_3_needed = false;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = false;
                Foreign_Table_3_Name = "M_Division";
                Foreign_Table_3_Key_Text.Text = "Division Name";
                Foreign_Table_3_Column_Name = "DivisionName";
                Foreign_Table_3_Key_Column = "Division_ID";
            }
            else if (Page.RouteData.Values["Page"].ToString().Trim() == "Taluka")
            {
                Page_Heading.Text = "Taluka Master";
                Main_Heading_1.Text = "Taluka Details";
                Main_Heading_2.Text = "Taluka Records";

                // Serial Number
                Grid_Search.Columns[0].HeaderText = "Ser.No.";
                Grid_Search.Columns[0].Visible = true;

                // Main Table
                Grid_Search.Columns[1].HeaderText = "Taluka ID";
                Grid_Search.Columns[1].Visible = true;
                Main_Table_Name = "M_Taluka";
                Primary_Key_Column = "Taluka_ID";

                // Main Table Column 1
                Grid_Search.Columns[2].HeaderText = "Taluka";
                Is_Main_column_1_needed = true;
                Main_Column_1_Text.Text = "Taluka Name";
                Main_Column_1_Name = "TalukaName";

                // Main Table Column 2
                Grid_Search.Columns[3].HeaderText = "Taluka (Marathi)";
                Is_Main_column_2_needed = true;
                Main_Column_2_Text.Text = "Taluka (Marathi)";
                Main_Column_2_Name = "TalukaNameMr";

                // Main Table Column 3
                Grid_Search.Columns[4].HeaderText = "Taluka Code";
                Is_Main_column_3_needed = true;
                Main_Column_3_Text.Text = "Taluka Code";
                Main_Column_3_Name = "TalukaCode";

                // Foreign Table 1
                Grid_Search.Columns[5].HeaderText = "State";
                Is_Foreign_column_1_needed = true;
                Foreign_Table_1_Name = "M_State";
                Foreign_Table_1_Key_Text.Text = "State Name";
                Foreign_Table_1_Column_Name = "StateName";
                Foreign_Table_1_Key_Column = "State_ID";

                // Foreign Table 2
                Grid_Search.Columns[6].HeaderText = "Division";
                Is_Foreign_column_2_needed = true;
                Is_Foreign_Dropdown_2_Is_Dependant_On_1 = true;
                Foreign_Table_2_Name = "M_Division";
                Foreign_Table_2_Key_Text.Text = "Division Name";
                Foreign_Table_2_Column_Name = "DivisionName";
                Foreign_Table_2_Key_Column = "Division_ID";

                // Foreign Table 3
                Grid_Search.Columns[7].HeaderText = "District";
                Is_Foreign_column_3_needed = true;
                Is_Foreign_Dropdown_3_Is_Dependant_On_2 = true;
                Foreign_Table_3_Name = "M_District";
                Foreign_Table_3_Key_Text.Text = "District Name";
                Foreign_Table_3_Column_Name = "DistrictName";
                Foreign_Table_3_Key_Column = "District_ID";
            }
            else
            {
                // no page found, hiding the parent div for no UI and notification to user
                Div_Control.Visible = false;
                Div_Grid.Visible = false;
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
            ViewState["Is_Foreign_Dropdown_2_Is_Dependant_On_1"] = Is_Foreign_Dropdown_2_Is_Dependant_On_1;

            ViewState["Foreign_Table_3_Name"] = Foreign_Table_3_Name;
            ViewState["Foreign_Table_3_Key_Column"] = Foreign_Table_3_Key_Column;
            ViewState["Foreign_Table_3_Column_Name"] = Foreign_Table_3_Column_Name;
            ViewState["Is_Foreign_Dropdown_3_Is_Dependant_On_2"] = Is_Foreign_Dropdown_3_Is_Dependant_On_2;
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }


    private void Decide_Input_Fields()
    {
        try
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
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }



    //================================ Dropdown Bind ========================================

    private void Bind_Dropdown()
    {
        try
        {
            if (Is_Foreign_column_1_needed)
            {
                sqlQuery = $@"Select {Foreign_Table_1_Key_Column} as ID, {Foreign_Table_1_Column_Name} as Value 
                      From {Foreign_Table_1_Name} 
                      Where IsDeleted IS NULL 
                      Order By {Foreign_Table_1_Column_Name}";

                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(DD_Foriegn_Column_1, sqlQuery, "Value", "ID", parameters);
            }

            if (Is_Foreign_column_2_needed)
            {
                if (Is_Foreign_Dropdown_2_Is_Dependant_On_1)
                {
                    sqlQuery = $@"Select {Foreign_Table_2_Key_Column} as ID, {Foreign_Table_2_Column_Name} as Value 
                              From {Foreign_Table_2_Name} 
                              Where {Foreign_Table_1_Key_Column} = {DD_Foriegn_Column_1.SelectedValue} AND IsDeleted IS NULL 
                              Order By {Foreign_Table_2_Column_Name}";
                }
                else
                {
                    sqlQuery = $@"Select {Foreign_Table_2_Key_Column} as ID, {Foreign_Table_2_Column_Name} as Value 
                                From {Foreign_Table_2_Name} 
                                Where IsDeleted IS NULL 
                                Order By {Foreign_Table_2_Column_Name}";
                }

                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(DD_Foriegn_Column_2, sqlQuery, "Value", "ID", parameters);
            }

            if (Is_Foreign_column_3_needed)
            {
                if (Is_Foreign_Dropdown_3_Is_Dependant_On_2)
                {
                    sqlQuery = $@"Select {Foreign_Table_3_Key_Column} as ID, {Foreign_Table_3_Column_Name} as Value 
                          From {Foreign_Table_3_Name} 
                          Where {Foreign_Table_2_Key_Column} = {DD_Foriegn_Column_2.SelectedValue} AND IsDeleted IS NULL 
                          Order By {Foreign_Table_3_Column_Name}";
                }
                else
                {
                    sqlQuery = $@"Select {Foreign_Table_3_Key_Column} as ID, {Foreign_Table_3_Column_Name} as Value 
                          From {Foreign_Table_3_Name} 
                          Where IsDeleted IS NULL 
                          Order By {Foreign_Table_3_Column_Name}";
                }

                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(DD_Foriegn_Column_3, sqlQuery, "Value", "ID", parameters);
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }




    //================================ Dropdown Event ========================================
    protected void DD_Foriegn_Column_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Is_Foreign_Dropdown_2_Is_Dependant_On_1 = (bool)ViewState["Is_Foreign_Dropdown_2_Is_Dependant_On_1"];

            if (Is_Foreign_Dropdown_2_Is_Dependant_On_1)
            {
                if (DD_Foriegn_Column_1.SelectedIndex > 0)
                {
                    sqlQuery = $@"Select {Foreign_Table_2_Key_Column} as ID, {Foreign_Table_2_Column_Name} as Value 
                        From {Foreign_Table_2_Name} 
                        Where {Foreign_Table_1_Key_Column} = {DD_Foriegn_Column_1.SelectedValue} AND IsDeleted IS NULL 
                        Order By {Foreign_Table_2_Column_Name}";

                    parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                    executeClass.Bind_Dropdown_Generic(DD_Foriegn_Column_2, sqlQuery, "Value", "ID", parameters);
                }
                else
                {
                    DD_Foriegn_Column_2.ClearSelection();
                    DD_Foriegn_Column_2.Items.Clear();
                }
            }

            Bind_Grid();
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    protected void DD_Foriegn_Column_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Is_Foreign_Dropdown_3_Is_Dependant_On_2 = (bool)ViewState["Is_Foreign_Dropdown_3_Is_Dependant_On_2"];

            if (Is_Foreign_Dropdown_3_Is_Dependant_On_2)
            {
                if (DD_Foriegn_Column_2.SelectedIndex > 0)
                {
                    sqlQuery = $@"Select {Foreign_Table_3_Key_Column} as ID, {Foreign_Table_3_Column_Name} as Value 
                      From {Foreign_Table_3_Name} 
                      Where {Foreign_Table_2_Key_Column} = {DD_Foriegn_Column_2.SelectedValue} AND IsDeleted IS NULL 
                      Order By {Foreign_Table_3_Column_Name}";

                    parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                    executeClass.Bind_Dropdown_Generic(DD_Foriegn_Column_3, sqlQuery, "Value", "ID", parameters);
                }
                else
                {
                    DD_Foriegn_Column_3.ClearSelection();
                    DD_Foriegn_Column_3.Items.Clear();
                }
            }

            Bind_Grid();
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    protected void DD_Foriegn_Column_3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid();
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }





    //================================ Grid Bind ========================================

    private void Bind_Grid()
    {
        DataTable dt = new DataTable();
        string sql = string.Empty;
        Dictionary<string, object> parameters = new Dictionary<string, object>();

        try
        {
            // fetching hierarichal / designation level of logged-in user
            parameters = new Dictionary<string, object> { { "@User_ID", Session["User_ID"] }, };
            dt = executeClass.Get_DataTable_From_StoredProcedure(this.Page, "USP_Get_User_Hierarchy_Level", parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                parameters = new Dictionary<string, object>
                {
                    // mandatory work area parameters
                    { "@Election_Class_IDs", Session["Election_Class_ID"].ToString() },
                    { "@Election_Sub_Class_IDs", Session["Election_Sub_Class_ID"].ToString() },
                    { "@Division_IDs", Session["Division_ID"].ToString() },
                    { "@District_IDs", Session["District_ID"].ToString() },
                    { "@Taluka_IDs", Session["Taluka_ID"].ToString() },
                    { "@LevelType", dt.Rows[0]["LevelType"].ToString() },

                    // dropdown search filter parameters
                    { "@Society_ID", ddlSociety.SelectedIndex > 0 ? (object)ddlSociety.SelectedValue : DBNull.Value },
                    { "@SocietyCode", ddlsCode.SelectedIndex > 0 ? (object)ddlsCode.SelectedValue : DBNull.Value },
                    { "@Election_Class_ID", ddlClass.SelectedIndex > 0 ? (object)ddlClass.SelectedValue : DBNull.Value },
                    { "@Election_Sub_Class_ID", ddlSubClass.SelectedIndex > 0 ? (object)ddlSubClass.SelectedValue : DBNull.Value }
                };

                dt = executeClass.Get_DataTable_From_StoredProcedure(this.Page, "USP_Get_GridView_SocietyMaster", parameters);
                if (dt != null && dt.Rows.Count > 0)
                {

                }
            }


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

            //sqlQuery = $@"
            //        SELECT 
            //            {Main_Table_Name}.{Primary_Key_Column} AS ID, 
            //            {Main_Table_Name}.{Main_Column_1_Name} AS Main_Column_1, 
            //            {Main_Table_Name}.{Main_Column_2_Name} AS Main_Column_2, 
            //            {Main_Table_Name}.{Main_Column_3_Name} AS Main_Column_3, 
            //            {(Is_Foreign_column_1_needed ? $"{Foreign_Table_1_Name}.{Foreign_Table_1_Column_Name} AS Foreign_Column_1, " : "NULL AS Foreign_Column_1, ")} 
            //            {(Is_Foreign_column_2_needed ? $"{Foreign_Table_2_Name}.{Foreign_Table_2_Column_Name} AS Foreign_Column_2, " : "NULL AS Foreign_Column_2, ")} 
            //            {(Is_Foreign_column_3_needed ? $"{Foreign_Table_3_Name}.{Foreign_Table_3_Column_Name} AS Foreign_Column_3, " : "NULL AS Foreign_Column_3, ")} 
            //            NULL AS DummyColumn 
            //        FROM {Main_Table_Name} 
            //        {(Is_Foreign_column_1_needed ? $@"INNER JOIN {Foreign_Table_1_Name} ON {Foreign_Table_1_Name}.{Foreign_Table_1_Key_Column} = {Main_Table_Name}.{Foreign_Table_1_Key_Column} " : "")} 
            //        {(Is_Foreign_column_2_needed ? $@"INNER JOIN {Foreign_Table_2_Name} ON {Foreign_Table_2_Name}.{Foreign_Table_2_Key_Column} = {Main_Table_Name}.{Foreign_Table_2_Key_Column} " : "")} 
            //        {(Is_Foreign_column_3_needed ? $@"INNER JOIN {Foreign_Table_3_Name} ON {Foreign_Table_3_Name}.{Foreign_Table_3_Key_Column} = {Main_Table_Name}.{Foreign_Table_3_Key_Column} " : "")} 
            //        WHERE {Main_Table_Name}.IsDeleted IS NULL
            //        {(DD_Foriegn_Column_1.SelectedIndex > 0 ? $@" AND {Foreign_Table_1_Name}.{Foreign_Table_1_Key_Column} = {DD_Foriegn_Column_1.SelectedValue}" : "")}
            //        {(DD_Foriegn_Column_2.SelectedIndex > 0 ? $@" AND {Foreign_Table_2_Name}.{Foreign_Table_2_Key_Column} = {DD_Foriegn_Column_2.SelectedValue}" : "")}
            //        {(DD_Foriegn_Column_3.SelectedIndex > 0 ? $@" AND {Foreign_Table_3_Name}.{Foreign_Table_3_Key_Column} = {DD_Foriegn_Column_3.SelectedValue}" : "")}
            //        ORDER BY {Main_Table_Name}.{Main_Column_1_Name}";

            ////{ (Input_Search.Text.Trim().Length > 0 ? $@" AND {Main_Table_Name}.{Main_Column_1_Name} LIKE '%{Input_Search.Text.Trim()}%'" : "")}

            //Dictionary<string, object> parameters = new Dictionary<string, object>()
            //{
            //    //{ "@Bank_ID", DD_Bank_Master.SelectedValue },
            //};

            //DataTable dt = executeClass.Get_Datatable(sqlQuery);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    Grid_Search.DataSource = dt;
            //    Grid_Search.DataBind();
            //    ViewState["Grid_Common_DT"] = dt;
            //}
            //else
            //{
            //    Grid_Search.DataSource = null;
            //    Grid_Search.DataBind();
            //    ViewState["Grid_Common_DT"] = null;
            //}
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

            DD_Foriegn_Column_1_SelectedIndexChanged(null, null);

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

            DD_Foriegn_Column_2_SelectedIndexChanged(null, null);

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

            DD_Foriegn_Column_3_SelectedIndexChanged(null, null);

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

            if (executeClass.Check_To_Allow_Delete(this.Page, Primary_Key_Column, Primary_ID) == false)
            {
                string sa_Body = $@"The record's primary key <b>{Primary_ID}</b> is in use in some other table, hence cannot delete this record. Please check";
                SweetAlert.GetSweet(this.Page, "warning", $"Cannot delete this record !!", $"{sa_Body}");
                return;
            }

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
        string redirectURL = GetRouteUrl("CommonMaster_Route", new { Page = Page.RouteData.Values["Page"].ToString().Trim() });
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

                    // clearing user inputs
                    DD_Foriegn_Column_1.ClearSelection();
                    DD_Foriegn_Column_2.ClearSelection();
                    DD_Foriegn_Column_3.ClearSelection();
                    Input_Main_Column_1.Text = string.Empty;
                    Input_Main_Column_2.Text = string.Empty;
                    Input_Main_Column_3.Text = string.Empty;
                    Btn_Submit.Text = "Save";
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