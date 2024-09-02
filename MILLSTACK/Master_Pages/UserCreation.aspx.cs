using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_UserCreation : System.Web.UI.Page
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
                Bind_CheckBoxList();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }


    //-------------------------- Bind Dropdown --------------------------
    private void Bind_Dropdown()
    {
        string sql = string.Empty;

        try
        {
            // gender
            sql = $@"Select Gender_ID, GenderName, GenderNameMr, GenderCode, IsDeleted 
                     From M_Gender 
                     Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_Gender, sql, "GenderName", "Gender_ID", parameters);

            // role
            sql = $@"Select Role_ID, RoleName, RoleNameMr, RoleCode, IsDeleted
                    From M_RoleMaster
                    Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(MCDD_Role, sql, "RoleName", "Role_ID", parameters, true);

            // designation
            sql = $@"SELECT d.Designation_ID, CONCAT(d.DesignationName, '_' ,l.LevelType) AS DesignationName, l.LevelCode
                     FROM M_Designation AS d 
                     INNER JOIN M_Level AS l ON l.Level_ID = d.Level_ID
                     WHERE d.IsDeleted IS NULL
                     ORDER BY d.DesignationName";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_Hierarchy, sql, "DesignationName", "Designation_ID", parameters);
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }


    //-------------------------- Dropdown Event --------------------------

    protected void DD_Hierarchy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DD_Hierarchy.SelectedIndex > 0)
            {
                Div_Division.Visible = true;

                // splitting level type
                string selected_Designation = DD_Hierarchy.SelectedItem.Text.Split(new[] { "_" }, StringSplitOptions.None)[1]
                                                .Split(new[] { " Level" }, StringSplitOptions.None)[0]
                                                .Trim();

                // checking for the level access type
                if (selected_Designation == "Division")
                {
                    CheckBoxList_Division.AutoPostBack = false;
                }
                else if (selected_Designation == "District")
                {
                    //Div_Division.Visible = true;
                    //Div_District.Visible = true;

                    CheckBoxList_Division.AutoPostBack = true;
                    CheckBoxList_District.AutoPostBack = false;
                }
                else if (selected_Designation == "Taluka")
                {
                    //Div_Division.Visible = true;
                    //Div_District.Visible = true;
                    //Div_Taluka.Visible = true;

                    CheckBoxList_Division.AutoPostBack = true;
                    CheckBoxList_District.AutoPostBack = true;
                }
                else
                {
                    // if code comes here, means its logical error
                    Div_Division.Visible = false;
                    Div_District.Visible = false;
                    Div_Taluka.Visible = false;

                    CheckBoxList_Division.AutoPostBack = false;
                    CheckBoxList_District.AutoPostBack = false;
                }
            }
            else
            {
                Div_Division.Visible = false;
                Div_District.Visible = false;
                Div_Taluka.Visible = false;

                CheckBoxList_Division.ClearSelection();
                CheckBoxList_District.ClearSelection();
                CheckBoxList_Taluka.ClearSelection();

                CheckBoxList_District.Items.Clear();
                CheckBoxList_Taluka.Items.Clear();

                CheckBoxList_Division.AutoPostBack = false;
                CheckBoxList_District.AutoPostBack = false;
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }




    //-------------------------- Check Box List Bind --------------------------

    private void Bind_CheckBoxList()
    {
        string sql = string.Empty;

        try
        {
            // division
            sql = $@"Select Division_ID, State_ID, DivisionName, DivisionNameMr, DivisionCode, IsDeleted, SavedBy
                    From M_Division as div 
                    Where IsDeleted IS NULL
                    Order By DivisionName";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_CheckBoxList_Generic(CheckBoxList_Division, sql, "DivisionName", "Division_ID", parameters);
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }



    //-------------------------- Check All Event --------------------------

    protected void Check_All_Division_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBoxList_Division.Items.Cast<ListItem>().ToList().ForEach(item => item.Selected = Check_All_Division.Checked);

            string selected_Designation = DD_Hierarchy.SelectedItem.Text.Split(new[] { "_" }, StringSplitOptions.None)[1]
                                          .Split(new[] { " Level" }, StringSplitOptions.None)[0]
                                          .Trim();

            if (selected_Designation == "District" || selected_Designation == "Taluka")
            {
                CheckBoxList_Division_SelectedIndexChanged(null, null);
                CheckBoxList_District_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    protected void Check_All_District_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBoxList_District.Items.Cast<ListItem>().ToList().ForEach(item => item.Selected = Check_All_District.Checked);

            string selected_Designation = DD_Hierarchy.SelectedItem.Text.Split(new[] { "_" }, StringSplitOptions.None)[1]
                                          .Split(new[] { " Level" }, StringSplitOptions.None)[0]
                                          .Trim();

            if (selected_Designation == "Taluka")
            {
                CheckBoxList_District_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    protected void Check_All_Taluka_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBoxList_Taluka.Items.Cast<ListItem>().ToList().ForEach(item => item.Selected = Check_All_Taluka.Checked);
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }






    //-------------------------- Check Box List Event --------------------------

    protected void CheckBoxList_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sql = string.Empty;

            var selected_Divisions = CheckBoxList_Division.Items.Cast<ListItem>()
                               .Where(item => item.Selected)
                               .Select(item => item.Value.Trim())
                               .ToList();

            if (selected_Divisions.Any())
            {
                string division_IDs = string.Join(", ", selected_Divisions);

                sql = $@"Select District_ID, State_ID, Division_ID, DistrictName, DistrictNameMr, DistrictCode, SavedBy, IsDeleted
                         From M_District Where Division_ID IN ({division_IDs})";
                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_CheckBoxList_Generic(CheckBoxList_District, sql, "DistrictName", "District_ID", parameters);

                Div_District.Visible = true;
            }
            else
            {
                Div_District.Visible = false;
                CheckBoxList_District.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    protected void CheckBoxList_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sql = string.Empty;

            var selected_Districts = CheckBoxList_District.Items.Cast<ListItem>()
                                    .Where(item => item.Selected)
                                    .Select(item => item.Value.Trim())
                                    .ToList();

            if (selected_Districts.Any())
            {
                string district_IDs = string.Join(", ", selected_Districts);

                sql = $@"Select Taluka_ID, State_ID, Division_ID, District_ID, TalukaName, TalukaNameMr, TalukaCode, SavedBy, IsDeleted
                         From M_Taluka Where District_ID IN ({district_IDs})";
                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_CheckBoxList_Generic(CheckBoxList_Taluka, sql, "TalukaName", "Taluka_ID", parameters);

                Div_Taluka.Visible = true;
            }
            else
            {
                Div_Taluka.Visible = false;
                CheckBoxList_Taluka.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }






    //-------------------------- Save / Update Event --------------------------
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("UserCreation_Route", null));
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        // user inputs
        string first_Name = Txt_First_Name.Text.Trim();
        string middle_Name = Txt_Middle_Name.Text.Trim();
        string last_Name = Txt_Laste_Name.Text.Trim();
        string gender = DD_Gender.SelectedValue.Trim();
        string phone_No = Txt_Phone_Number.Text.Trim();
        string email = Txt_Email.Text.Trim();
        string address = TA_Address.Value.Trim();
        string user_Name = Txt_User_Name.Text.Trim();

        string password = Txt_Password.Text.Trim();
        string confirm_Password = Txt_Confirm_Password.Text.Trim();

        // password confirmation on server side
        if (password != confirm_Password)
        {
            Txt_Confirm_Password.Focus();
            SweetAlert.GetSweet(this.Page, "warning", "", $"Password did not <b>Match</b>, kindly check !!");
            return;
        }

        // converting hashed password and salt
        string salt = HashHelper.GenerateSalt();
        string hashed_Password = HashHelper.Hash(password, salt);


        // work area allocation
        string hierarchy = DD_Hierarchy.SelectedValue;

        string status = DD_Status.SelectedValue.Trim();

        string division_IDs = masterClass.Get_CheckboxList_Checked_Values(CheckBoxList_Division);
        string district_IDs = masterClass.Get_CheckboxList_Checked_Values(CheckBoxList_District);
        string taluka_IDs = masterClass.Get_CheckboxList_Checked_Values(CheckBoxList_Taluka);

        Session["User_ID"] = 1;
        string User_ID = Session["User_ID"].ToString();

        ViewState["Operation"] = "INSERT";

        string OperationStatus = string.IsNullOrEmpty(ViewState["Operation"]?.ToString()) ? string.Empty : ViewState["Operation"].ToString();

        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@Operation", OperationStatus },
            { "@User_ID", DBNull.Value },
            { "@Designation_ID", hierarchy },
            { "@FirstName", first_Name },
            { "@MiddleName", middle_Name },
            { "@LastName", last_Name },
            { "@Gender", gender },
            { "@UserPhoneNo", phone_No },
            { "@UserEmail", email },
            { "@UserAddress", address },
            { "@UserName", user_Name },
            { "@UserPassword", hashed_Password },
            { "@Salt", salt },
            { "@Division_ID", division_IDs },
            { "@District_ID", district_IDs },
            { "@Taluka_ID", taluka_IDs },
            { "@IsActive", status },
            { "@SavedBy", User_ID },
        };

        // Table-Value Parameter (TVP)
        DataTable Role_ID_DT = new DataTable();
        Role_ID_DT.Columns.Add("Role_ID", typeof(Int64));
        Role_ID_DT.Columns.Add("SavedBy", typeof(string));

        string role_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Role);
        List<Int64> selectedRoleIDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Role).Split(',').Select(Int64.Parse).ToList();
        foreach (Int64 roleID in selectedRoleIDs)
        {
            Role_ID_DT.Rows.Add(roleID, User_ID);
        }

        executeClass.ExecuteStoredProcedure(this.Page, "USP_User_Creation", parameters, Role_ID_DT);


    }












}