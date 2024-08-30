using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
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

                }
                else if (selected_Designation == "Taluka")
                {

                }
                else
                {
                    // if code comes here, means its logical error
                }
            }
            else
            {
                Div_Division.Visible = false;
                Div_Division.Visible = false;
                Div_Division.Visible = false;

                CheckBoxList_Division.ClearSelection();
                CheckBoxList_Division.ClearSelection();
                CheckBoxList_Division.ClearSelection();

                //CheckList_Division.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }




    //-------------------------- Check List Event --------------------------

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
        CheckBoxList_Division.Items.Cast<ListItem>().ToList().ForEach(item => item.Selected = Check_All_Division.Checked);

        string selected_Designation = DD_Hierarchy.SelectedItem.Text.Split(new[] { "_" }, StringSplitOptions.None)[1]
                                      .Split(new[] { " Level" }, StringSplitOptions.None)[0]
                                      .Trim();

        if (selected_Designation == "District" || selected_Designation == "Taluka")
        {
            CheckBoxList_Division_SelectedIndexChanged(null, null);
        }
    }



    //-------------------------- Check Box List Event --------------------------

    protected void CheckBoxList_Division_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }






    //-------------------------- Save / Update Event --------------------------
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("UserCreation_Route", null));
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {

    }









    protected void CheckBoxList_Division_SelectedIndexChanged1(object sender, EventArgs e)
    {
        var selected_Divisions = CheckBoxList_Division.Items.Cast<ListItem>()
                                .Where(item => item.Selected)
                                .Select(item => item.Value.Trim())
                                .ToList();

        if (selected_Divisions.Any())
        {

        }
        else
        {
            //Div_District.Visible = false;
            //CheckList_District.Items.Clear();
        }
    }
}