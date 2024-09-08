using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        if (!IsPostBack)
        {
            Bind_Dropdown();
            BindTreeView();
        }
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

        string sql = $@"Select URFM_ID, UserRole_ID, Menu_ID, IsDeleted
                        From Tbl_MAP_UserRole_MenuForm 
                        Where UserRole_ID IN (Select Distinct UserRole_ID From Tbl_MAP_UserRole Where User_ID = @User_ID AND IsDeleted IS NULL)";
        var parameters = new Dictionary<string, object> { { "@User_ID", Session["User_ID"] }, };
        DataTable dt = executeClass.Get_Datatable(sql, parameters);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                string menu_Form_ID = row["Menu_ID"].ToString();
                string user_Role_ID = row["UserRole_ID"].ToString();
                TreeNode node = FindNodeByValue(TreeView1.Nodes, menu_Form_ID);
                if (node != null && user_Role_ID == selectedRole) node.Checked = true;
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




    //-------------------------- TreeView Bind --------------------------

    private void BindTreeView()
    {
        DataTable menuTable = GetMenuData();
        PopulateTreeView(menuTable, 0, null);
    }

    private DataTable GetMenuData()
    {
        string sql = $@"Select Menu_ID, Parent_ID, MenuName, MenuOrder, MenuURL, MenuIcon From Tbl_M_MenuForm Where IsDeleted IS NULL";
        var parameters = new Dictionary<string, object> { /*{ "@ID", projectID },*/ };
        return executeClass.Get_Datatable(sql, parameters);
    }

    private void PopulateTreeView(DataTable dt, int parentId, TreeNode parentNode)
    {
        //TreeView1.ExpandDepth = 0; //treeview complete collapse
        //TreeView1.ExpandDepth = 1000; // higher values to fully expand, but not recommended

        DataRow[] rows = dt.Select($"Parent_ID = {parentId}", "MenuOrder");

        foreach (DataRow row in rows)
        {
            string menu_Name = row["MenuName"].ToString();
            string menu_ID = row["Menu_ID"].ToString();
            string menu_URL = ResolveUrl(row["MenuURL"].ToString());
            string menu_Icon = row["MenuIcon"].ToString();

            TreeNode newNode = new TreeNode
            {
                Text = menu_Name,
                Value = menu_ID,
                NavigateUrl = menu_URL,
                //ImageUrl = menu_Icon
            };

            if (parentNode == null)
            {
                TreeView1.Nodes.Add(newNode);

                // This prevents the node from being selectable
                newNode.SelectAction = TreeNodeSelectAction.None;

                newNode.Text = $"<span class='text-primary-emphasis fw-semibold bg-opacity-50 bg-body-tertiary border shadow px-3 py-1 ms-1 rounded-2'>{menu_Name}</span>";
            }
            else
            {
                parentNode.ChildNodes.Add(newNode);

                // This prevents the node from being selectable
                //newNode.SelectAction = TreeNodeSelectAction.None;

                //newNode.Text = $"<span class='text-dark fw-lighter ms-1'>{menu_Name}</span>";
                newNode.Text = $"<a href='{menu_URL}' target='_blank' class='text-dark fw-lighter text-decoration-none ms-1'>{menu_Name}</a>";
            }

            PopulateTreeView(dt, Convert.ToInt32(row["Menu_ID"]), newNode);
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        // Handle the event when a tree node is selected
    }






    //-------------------------- Save Button Event --------------------------

    protected void btnEventClick_btnReset(object sender, EventArgs e)
    {
        ClearCheckboxes(TreeView1.Nodes);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        List<SqlParameter> parameters;

        if (DD_User_Roles.SelectedIndex == 0)
        {
            DD_User_Roles.Focus();
            return;
        }

        string user_Role_ID = DD_User_Roles.SelectedValue;
        List<int> selected_MenuForm_IDs = new List<int>();

        // Collect the IDs of all checked checkboxes
        foreach (TreeNode node in TreeView1.CheckedNodes)
        {
            selected_MenuForm_IDs.Add(int.Parse(node.Value));
        }

        using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction = connection.BeginTransaction(IsolationLevel.Serializable, "Transaction_");
            command.Connection = connection;
            command.Transaction = transaction;

            try
            {
                // Delete existing records for the selected UserRole_ID
                string deleteQuery = "DELETE FROM Tbl_MAP_UserRole_MenuForm WHERE UserRole_ID = @UserRole_ID";
                parameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserRole_ID", user_Role_ID)
                };
                executeClass.ExecuteCommand(deleteQuery, command, parameters);


                // Insert new records
                string insertQuery = "INSERT INTO Tbl_MAP_UserRole_MenuForm (UserRole_ID, Menu_ID, SavedBy) VALUES (@UserRole_ID, @Menu_ID, @SavedBy)";
                foreach (int menu_Form_ID in selected_MenuForm_IDs)
                {
                    parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@UserRole_ID", user_Role_ID),
                        new SqlParameter("@Menu_ID", menu_Form_ID), // This is to set dynamically in each iteration
                        new SqlParameter("@SavedBy", Session["User_ID"])
                    };

                    executeClass.ExecuteCommand(insertQuery, command, parameters);
                }

                if (transaction.Connection != null)
                {
                    transaction.Commit();

                    SweetAlert.GetSweet(this.Page, "success", $"", $"<b>{DD_User_Roles.SelectedItem.Text}</b> responsibilities assigned successfully");

                    DD_User_Roles.ClearSelection();
                    DD_User_Roles.Focus();
                    DD_User_Roles_SelectedIndexChanged(null, null);
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
    }


}