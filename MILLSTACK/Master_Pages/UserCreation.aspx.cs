using CommonClassLibrary;
using System;
using System.Collections.Generic;
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
            sql = $@"Select Gender_ID, GenderName, GenderNameMr, GenderCode, IsDeleted 
                     From M_Gender 
                     Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_Gender, sql, "GenderName", "Gender_ID", parameters);


            sql = $@"Select Role_ID, RoleName, RoleNameMr, RoleCode, IsDeleted
                    From M_RoleMaster
                    Where IsDeleted IS NULL";
            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(MCDD_Role, sql, "RoleName", "Role_ID", parameters, true);


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


}