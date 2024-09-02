using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_RoleAndResponsibility : System.Web.UI.Page
{
    #region [ Global Declaration ]
    ExecuteClass executeClass = new ExecuteClass();
    MasterClass masterClass = new MasterClass();
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Bind_Dropdown();
        BindTreeView();
    }


    //-------------------------- Dropdown Bind --------------------------
    private void Bind_Dropdown()
    {
        string sql = string.Empty;

        try
        {
            // binding level types
            sql = $@"Select Role_ID, RoleName, RoleNameMr, RoleCode, IsDeleted
                    From M_RoleMaster
                    Where IsDeleted IS NULL	";

            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_User_Roles, sql, "RoleName", "Role_ID", parameters);
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }




    //-------------------------- Dropdown Event --------------------------

    protected void DD_User_Roles_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedRole = DD_User_Roles.SelectedValue;

        ClearCheckboxes(TreeView1.Nodes);

        string sql = $@" ";
        var parameters = new Dictionary<string, object>
        {
            { "@UserId",  Session["UserID"] },
        };
        DataTable dt = executeClass.Get_Datatable(sql, parameters);
        foreach (DataRow row in dt.Rows)
        {
            string menu_Form_ID = row["MenuForm_ID"].ToString();
            string user_Role_ID = row["UserRole_ID"].ToString();

            TreeNode node = FindNodeByValue(TreeView1.Nodes, menu_Form_ID);

            if (node != null && user_Role_ID == selectedRole)
            {
                node.Checked = true;
            }
        }
    }

    private TreeNode FindNodeByValue(TreeNodeCollection nodes, string menu_Form_ID)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Value == menu_Form_ID)
            {
                return node;
            }

            TreeNode foundNode = FindNodeByValue(node.ChildNodes, menu_Form_ID);

            if (foundNode != null)
            {
                return foundNode;
            }
        }
        return null;
    }

    private void ClearCheckboxes(TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            node.Checked = false;
            ClearCheckboxes(node.ChildNodes);
        }
    }



}