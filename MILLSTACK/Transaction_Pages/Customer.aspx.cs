using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transaction_Pages_Customer : System.Web.UI.Page
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
                //Bind_CheckBoxList();

                if (Page.RouteData.Values["User_ID"] is string encrypted_ID && !string.IsNullOrWhiteSpace(encrypted_ID))
                {
                    ViewState["OPERATION"] = "UPDATE";
                    string Decrypted_ID = EncryptionHelper.Decrypt_UrlSafe(this.Page, HttpUtility.UrlDecode(encrypted_ID));
                    if (Int64.TryParse(Decrypted_ID, out Int64 User_ID))
                    {
                        AutoFill_UserRecord(User_ID);
                    }
                }
                else
                {
                    ViewState["OPERATION"] = "INSERT";
                    Page_Heading.Text = $"User Creation";
                    Btn_Submit.Text = $"Save";
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
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
            executeClass.Bind_Dropdown_Generic(DD_Gender, sql, "GenderName", "Gender_ID", parameters, multiple: false);

            // role
            sql = $@"Select Role_ID, RoleName, RoleNameMr, RoleCode, IsDeleted
                    From M_RoleMaster
                    Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(MCDD_Role, sql, "RoleName", "Role_ID", parameters, multiple: true);

            // designation
            sql = $@"SELECT d.Designation_ID, CONCAT(d.DesignationName, '_' ,l.LevelType) AS DesignationName, l.LevelCode
                     FROM M_Designation AS d 
                     INNER JOIN M_Level AS l ON l.Level_ID = d.Level_ID
                     WHERE d.IsDeleted IS NULL
                     ORDER BY d.DesignationName";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_Hierarchy, sql, "DesignationName", "Designation_ID", parameters, multiple: false);

            // MCDD assembly
            sql = $@"SELECT Assembly_ID, State_ID, District_ID, AssemblyName, AssemblyNameMr, AssemblyCode, IsDeleted 
                    FROM Tbl_M_Assembly
                    WHERE IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(MCDD_Assembly, sql, "AssemblyName", "Assembly_ID", parameters, multiple: true);
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
                // splitting level type
                string selected_Designation = DD_Hierarchy.SelectedItem.Text.Split(new[] { "_" }, StringSplitOptions.None)[1]
                                                .Split(new[] { " Level" }, StringSplitOptions.None)[0]
                                                .Trim();

                MCDD_Ward.Items.Clear();
                MCDD_Sector.Items.Clear();
                MCDD_Society.Items.Clear();

                Div_Assembly.Visible = false;
                Div_Ward.Visible = false;
                Div_Sector.Visible = false;
                Div_Society.Visible = false;

                MCDD_Assembly.AutoPostBack = false;
                MCDD_Ward.AutoPostBack = false;
                MCDD_Sector.AutoPostBack = false;
                RFV_Assembly.Enabled = false;

                RFV_Ward.Enabled = false;
                RFV_Sector.Enabled = false;
                RFV_Soceity.Enabled = false;

                if (selected_Designation == "Assembly")
                {
                    Div_Assembly.Visible = true;
                    RFV_Assembly.Enabled = true;
                }
                else if (selected_Designation == "Ward")
                {
                    Div_Assembly.Visible = true;
                    Div_Ward.Visible = true;

                    MCDD_Assembly.AutoPostBack = true;

                    RFV_Assembly.Enabled = true;
                    RFV_Ward.Enabled = true;
                }
                else if (selected_Designation == "Sector")
                {
                    Div_Assembly.Visible = true;
                    Div_Ward.Visible = true;
                    Div_Sector.Visible = true;

                    MCDD_Assembly.AutoPostBack = true;
                    MCDD_Ward.AutoPostBack = true;

                    RFV_Assembly.Enabled = true;
                    RFV_Ward.Enabled = true;
                    RFV_Sector.Enabled = true;
                }
                else if (selected_Designation == "Society")
                {
                    Div_Assembly.Visible = true;
                    Div_Ward.Visible = true;
                    Div_Sector.Visible = true;
                    Div_Society.Visible = true;

                    MCDD_Assembly.AutoPostBack = true;
                    MCDD_Ward.AutoPostBack = true;
                    MCDD_Sector.AutoPostBack = true;

                    RFV_Assembly.Enabled = true;
                    RFV_Ward.Enabled = true;
                    RFV_Sector.Enabled = true;
                    RFV_Soceity.Enabled = true;
                }


                // checking for the level access type
                //if (selected_Designation == "Division")
                //{
                //    CheckBoxList_Division.AutoPostBack = false;
                //}
                //else if (selected_Designation == "District")
                //{
                //    //Div_Division.Visible = true;
                //    //Div_District.Visible = true;

                //    CheckBoxList_Division.AutoPostBack = true;
                //    CheckBoxList_District.AutoPostBack = false;
                //}
                //else if (selected_Designation == "Taluka")
                //{
                //    //Div_Division.Visible = true;
                //    //Div_District.Visible = true;
                //    //Div_Taluka.Visible = true;

                //    CheckBoxList_Division.AutoPostBack = true;
                //    CheckBoxList_District.AutoPostBack = true;
                //}
                //else
                //{
                //    // if code comes here, means its logical error
                //    Div_Division.Visible = false;
                //    Div_District.Visible = false;
                //    Div_Taluka.Visible = false;

                //    CheckBoxList_Division.AutoPostBack = false;
                //    CheckBoxList_District.AutoPostBack = false;
                //}
            }
            else
            {
                //Div_Division.Visible = false;
                //Div_District.Visible = false;
                //Div_Taluka.Visible = false;

                //CheckBoxList_Division.ClearSelection();
                //CheckBoxList_District.ClearSelection();
                //CheckBoxList_Taluka.ClearSelection();

                //CheckBoxList_District.Items.Clear();
                //CheckBoxList_Taluka.Items.Clear();

                //CheckBoxList_Division.AutoPostBack = false;
                //CheckBoxList_District.AutoPostBack = false;

                Div_Assembly.Visible = false;
                Div_Ward.Visible = false;
                Div_Sector.Visible = false;
                Div_Society.Visible = false;

                MCDD_Assembly.AutoPostBack = false;
                MCDD_Ward.AutoPostBack = false;
                MCDD_Sector.AutoPostBack = false;

                RFV_Assembly.Enabled = false;
                RFV_Ward.Enabled = false;
                RFV_Sector.Enabled = false;
                RFV_Soceity.Enabled = false;

                MCDD_Ward.Items.Clear();
                MCDD_Sector.Items.Clear();
                MCDD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }


    protected void MCDD_Assembly_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sql = string.Empty;

            var selectedItems = MCDD_Assembly.Items.Cast<ListItem>().Where(i => i.Selected).ToList();
            if (selectedItems.Count > 0)
            {
                string assembly_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Assembly);

                // MCDD assembly
                sql = $@"Select Ward_ID, Assembly_ID, WardName, WardNameMr, WardCode, IsDeleted
                     From Tbl_M_Ward
                     Where IsDeleted IS NULL
                     AND Assembly_ID IN ({string.Join(",", assembly_IDs.Split(',').Select(x => $"'{x}'"))})";
                parameters = new Dictionary<string, object> { /*{ "@Assembly_ID", assembly_IDs },*/ };
                executeClass.Bind_Dropdown_Generic(MCDD_Ward, sql, "WardName", "Ward_ID", parameters, multiple: true);

                Div_Ward.Visible = true;
            }
            else
            {
                MCDD_Ward.Items.Clear();
                MCDD_Sector.Items.Clear();
                MCDD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }

    protected void MCDD_Ward_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sql = string.Empty;

            var selectedItems = MCDD_Ward.Items.Cast<ListItem>().Where(i => i.Selected).ToList();
            if (selectedItems.Count > 0)
            {
                string ward_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Ward);

                sql = $@"Select Sector_ID, Ward_ID, SectorName, SectorNameMr, SectorCode, IsDeleted
                    From Tbl_M_Sector
                    Where IsDeleted IS NULL
                    AND Ward_ID IN ({string.Join(",", ward_IDs.Split(',').Select(x => $"'{x}'"))})";
                parameters = new Dictionary<string, object> { /*{ "@Ward_ID", MCDD_Ward.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(MCDD_Sector, sql, "SectorName", "Sector_ID", parameters, multiple: true);

                Div_Sector.Visible = true;
            }
            else
            {
                MCDD_Sector.Items.Clear();
                MCDD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }

    protected void MCDD_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sql = string.Empty;

            var selectedItems = MCDD_Sector.Items.Cast<ListItem>().Where(i => i.Selected).ToList();
            if (selectedItems.Count > 0)
            {
                string sector_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Ward);

                sql = $@"Select Society_ID, Sector_ID, SocietyName, SocietyNameMr,  SocietyCode, IsDeleted
                    From Tbl_M_Society
                    Where IsDeleted IS NULL
                   AND Sector_ID IN ({string.Join(",", sector_IDs.Split(',').Select(x => $"'{x}'"))})";
                parameters = new Dictionary<string, object> { /*{ "@Sector_ID", MCDD_Sector.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(MCDD_Society, sql, "SocietyName", "Society_ID", parameters, multiple: true);

                Div_Society.Visible = true;
            }
            else
            {
                MCDD_Society.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }

    protected void DD_Hierarchy_SelectedIndexChanged_OLD_CHecboxListMethod(object sender, EventArgs e)
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





    //-------------------------- Auto-Fill Data --------------------------
    private void AutoFill_UserRecord(Int64 User_ID)
    {
        DataSet ds = new DataSet();
        string sql = string.Empty;
        Dictionary<string, object> parameters = new Dictionary<string, object>();

        try
        {
            parameters = new Dictionary<string, object>
            {
                { "@User_ID", User_ID },
            };

            ds = executeClass.Get_DataSet_From_StoredProcedure("USP_Get_UserMaster_By_ID", parameters);
            if (ds != null && ds.Tables.Count > 0)
            {
                // user details
                DataTable user_DT = ds.Tables[0];

                Txt_First_Name.Text = user_DT.Rows[0]["FirstName"].ToString();
                Txt_Middle_Name.Text = user_DT.Rows[0]["MiddleName"].ToString();
                Txt_Laste_Name.Text = user_DT.Rows[0]["LastName"].ToString();
                Txt_Phone_Number.Text = user_DT.Rows[0]["UserPhoneNo"].ToString();
                Txt_Email.Text = user_DT.Rows[0]["UserEmail"].ToString();
                TA_Address.Text = user_DT.Rows[0]["UserAddress"].ToString();
                Txt_User_Name.Text = user_DT.Rows[0]["UserName"].ToString();

                DD_Gender.SelectedValue = user_DT.Rows[0]["Gender_ID"].ToString();
                DD_Hierarchy.SelectedValue = user_DT.Rows[0]["Designation_ID"].ToString();
                DD_Status.SelectedValue = (Convert.ToBoolean(user_DT.Rows[0]["IsActive"]) ? "1" : "0");

                DD_Hierarchy_SelectedIndexChanged(null, null);


                // hiding the password and confirm password
                Div_Password.Visible = false;
                RFV_Password.Enabled = false;

                Div_ConfirmPassword.Visible = false;
                RFV_ConfirmPassword.Enabled = false;

                // for roles
                if (ds.Tables.Count > 1)
                {
                    DataTable role_DT = ds.Tables[1];
                    masterClass.Select_Item_In_DropDown_With_DT(this.Page, MCDD_Role, role_DT, "Role_ID", multiple: true);
                }

                // for workarea :: assembly
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataTable Assembly_ID_DT = ds.Tables[2];
                    masterClass.Select_Item_In_DropDown_With_DT(this.Page, MCDD_Assembly, Assembly_ID_DT, "Assembly_ID", multiple: true);
                    MCDD_Assembly_SelectedIndexChanged(null, null);
                }

                // for workarea :: ward
                if (ds.Tables[3].Rows.Count > 0)
                {
                    DataTable Ward_ID_DT = ds.Tables[3];
                    masterClass.Select_Item_In_DropDown_With_DT(this.Page, MCDD_Ward, Ward_ID_DT, "Ward_ID", multiple: true);
                    MCDD_Ward_SelectedIndexChanged(null, null);
                }

                // for workarea :: sector
                if (ds.Tables[4].Rows.Count > 0)
                {
                    DataTable Sector_ID_DT = ds.Tables[4];
                    masterClass.Select_Item_In_DropDown_With_DT(this.Page, MCDD_Sector, Sector_ID_DT, "Sector_ID", multiple: true);
                    MCDD_Sector_SelectedIndexChanged(null, null);
                }

                // for workarea :: society
                if (ds.Tables[5].Rows.Count > 0)
                {
                    DataTable Society_ID_DT = ds.Tables[5];
                    masterClass.Select_Item_In_DropDown_With_DT(this.Page, MCDD_Society, Society_ID_DT, "Society_ID", multiple: true);
                }

                ViewState["UserMaster_DT"] = user_DT;
                ViewState["OPERATION"] = "UPDATE";
                Page_Heading.Text = $"User Creation Update";
                Btn_Submit.Text = $"Update";
            }
            else
            {
                ViewState["OPERATION"] = "INSERT";
                Page_Heading.Text = $"User Creation";
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
        Response.Redirect(GetRouteUrl("UserMaster_Route", null));
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            // user inputs
            string first_Name = Txt_First_Name.Text.Trim();
            string middle_Name = Txt_Middle_Name.Text.Trim();
            string last_Name = Txt_Laste_Name.Text.Trim();
            string gender = DD_Gender.SelectedValue.Trim();
            string phone_No = Txt_Phone_Number.Text.Trim();
            string email = Txt_Email.Text.Trim();
            string address = TA_Address.Text.Trim();
            string user_Name = Txt_User_Name.Text.Trim();

            string password = Txt_Password.Text.Trim();
            string confirm_Password = Txt_Confirm_Password.Text.Trim();

            if (ViewState["OPERATION"] != null && ViewState["OPERATION"].ToString().Trim() == "INSERT")
            {
                // password confirmation on server side
                if (password != confirm_Password)
                {
                    Txt_Confirm_Password.Focus();
                    SweetAlert.GetSweet(this.Page, "warning", "", $"Password did not <b>Match</b>, kindly check !!");
                    return;
                }
            }

            // converting hashed password and salt
            string salt = HashHelper.GenerateSalt();
            string hashed_Password = HashHelper.Hash(password, salt);


            // work area allocation
            string hierarchy = DD_Hierarchy.SelectedValue;

            string status = DD_Status.SelectedValue.Trim();

            // Checbox List
            //string division_IDs = masterClass.Get_CheckboxList_Checked_Values(CheckBoxList_Division);
            //string district_IDs = masterClass.Get_CheckboxList_Checked_Values(CheckBoxList_District);
            //string taluka_IDs = masterClass.Get_CheckboxList_Checked_Values(CheckBoxList_Taluka);

            // Multi-check Dropdown
            string assembly_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Assembly);
            string ward_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Ward);
            string sector_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Sector);
            string society_IDs = masterClass.Get_Selected_Items_From_DropDown(MCDD_Society);

            string User_ID = Session["User_ID"].ToString();

            string OperationStatus = string.IsNullOrEmpty(ViewState["OPERATION"]?.ToString()) ? string.Empty : ViewState["OPERATION"].ToString();

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Operation", OperationStatus },
                { "@User_ID", ViewState["OPERATION"].ToString() == "INSERT" ? (object)DBNull.Value : User_ID },
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
                { "@Assembly_ID", assembly_IDs },
                { "@Ward_ID", ward_IDs },
                { "@Sector_ID", sector_IDs },
                { "@Society_ID", society_IDs },
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

            string iconType, mssg, redirect;

            if (ViewState["OPERATION"].ToString() == "INSERT")
            {
                iconType = $@"success";
                mssg = $@"User with UserName : <b>{user_Name}</b> created susccesfully !";
            }
            else
            {
                iconType = $@"info";
                mssg = $@"User with UserName : <b>{user_Name}</b> updated susccesfully !";
            }

            SweetAlert.GetSweet(this.Page, iconType, $"", mssg, GetRouteUrl("UserMaster_Route", null));
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
        }
    }





}