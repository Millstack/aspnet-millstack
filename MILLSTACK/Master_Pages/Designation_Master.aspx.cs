using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_Pages_Designation_Master : System.Web.UI.Page
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
            ViewState["Operation"] = "INSERT";

            Bind_Dropdown();
            BindTreeView();
        }
    }



    //-------------------------- Bind Dropdown --------------------------
    private void Bind_Dropdown()
    {
        string sql = string.Empty;

        try
        {
            // binding level types
            sql = $@"Select Level_ID, LevelType, LevelTypeMr, LevelCode, IsDeleted
                     From M_Level as lvl
                     Where IsDeleted IS NULL";

            parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
            executeClass.Bind_Dropdown_Generic(DD_Level_Type, sql, "LevelType", "Level_ID", parameters);


            // binding existing designation hierarchy
            DD_Parent_Designation.Items.Clear();
            DataTable Designation_DT = Get_Designations(0);
            DD_Parent_Designation.Items.Insert(0, new ListItem("--Select--", "0"));
            Get_Recersive_Designation_Dropdown(Designation_DT, 0, 0);
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    public DataTable Get_Designations(int HeadId)
    {
        string query = @"
        SELECT 
            Designation_ID, 
            ISNULL(Parent_Designation_ID, 0) AS Parent_Designation_ID, 
            DesignationName 
        FROM M_Designation 
        WHERE 1 = 1
            -- AND Designation_ID = 1
            AND IsDeleted IS NULL 
        ORDER BY 
            Parent_Designation_ID, Designation_ID";

        parameters = new Dictionary<string, object> { { "@HeadId", HeadId }, };
        return executeClass.Get_Datatable(query, parameters);
    }

    private void Get_Recersive_Designation_Dropdown(DataTable Designation_DT, int parentID, int level)
    {
        DataView dv = new DataView(Designation_DT);
        dv.RowFilter = string.Format("Parent_Designation_ID = {0}", parentID);

        StringBuilder appender = new StringBuilder();
        for (int j = 0; j < level; j++)
        {
            // Indentation based on the level
            appender.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
        }
        if (level > 0)
        {
            appender.Append("|_");
        }

        foreach (DataRowView row in dv)
        {
            string text = Server.HtmlDecode(appender.ToString() + row["DesignationName"].ToString());
            string value = row["Designation_ID"].ToString();
            DD_Parent_Designation.Items.Add(new ListItem(text, value));

            // Recursively call to add child nodes
            Get_Recersive_Designation_Dropdown(Designation_DT, int.Parse(row["Designation_ID"].ToString()), level + 1);
        }
    }





    //-------------------------- Bind Designation TreeView --------------------------

    private void BindTreeView()
    {
        Hierarchy.Nodes.Clear();
        DataTable Parent_Node_DT = BindDesigHead(0);
        PopulateTreeView(Parent_Node_DT, null);
    }

    private void PopulateTreeView(DataTable Parent_Node_DT, TreeNode parentNode)
    {
        if (Parent_Node_DT != null && Parent_Node_DT.Rows.Count > 0)
        {
            foreach (DataRow dr in Parent_Node_DT.Rows)
            {
                TreeNode newNode = new TreeNode
                {
                    Text = dr["DesignationName"].ToString(),
                    Value = dr["Designation_ID"].ToString()
                };

                if (parentNode == null)
                {
                    Hierarchy.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }

                // Recursively bind child nodes
                DataTable dtChildNodes = BindChildDesigHead(Convert.ToInt32(dr["Designation_ID"]));
                if (dtChildNodes != null && dtChildNodes.Rows.Count > 0)
                {
                    PopulateTreeView(dtChildNodes, newNode);
                }
            }

            Hierarchy.ExpandAll();
        }
    }

    public DataTable BindDesigHead(int? parentId)
    {
        string query = $@"Select Designation_ID, Parent_Designation_ID, Level_ID, DesignationName, DesignationNameMr, DesignationCode, IsDeleted
                          From M_Designation as dsg
                          Where Parent_Designation_ID = @Parent_Designation_ID and IsDeleted IS NULL";
        parameters = new Dictionary<string, object> { { "@Parent_Designation_ID", parentId }, };
        return executeClass.Get_Datatable(query, parameters);
    }

    public DataTable BindChildDesigHead(int parentId)
    {
        return BindDesigHead(parentId);
    }




    //-------------------------- TreeView Node Event --------------------------

    protected void Hierarchy_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Designation_ID"] = "";
            int Designation_ID = Convert.ToInt32(Hierarchy.SelectedNode.Value);
            ViewState["Designation_ID"] = Designation_ID.ToString();

            // designation details by ID
            string query = @"Select Designation_ID, Parent_Designation_ID, Level_ID, DesignationName, DesignationNameMr, DesignationCode, IsDeleted
                            From M_Designation as dsg
                            Where IsDeleted IS NULL	AND Designation_ID = @Designation_ID";
            parameters = new Dictionary<string, object> { { "@Designation_ID", Designation_ID }, };
            DataTable dt = executeClass.Get_Datatable(query, parameters);
            if (dt.Rows.Count > 0)
            {
                Txt_Designation_Name.Text = dt.Rows[0]["DesignationName"].ToString();
                Txt_Designation_Code.Text = dt.Rows[0]["DesignationCode"].ToString();

                string Parent_Designation_ID = dt.Rows[0]["Parent_Designation_ID"].ToString();
                if (DD_Parent_Designation.Items.FindByValue(Parent_Designation_ID) != null)
                {
                    DD_Parent_Designation.SelectedValue = Parent_Designation_ID;
                }

                string Level_ID = dt.Rows[0]["Level_ID"].ToString();
                if (DD_Level_Type.Items.FindByValue(Level_ID) != null)
                {
                    DD_Level_Type.SelectedValue = Level_ID;
                }

                Btn_Submit.Text = "Update";
                ViewState["Operation"] = "UPDATE";
            }
            else
            {
                Btn_Submit.Text = "Save";
                ViewState["Designation_ID"] = "";
                ViewState["Operation"] = "INSERT";
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }




    //-------------------------- Save / Reset / Delete Events --------------------------

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Hierarchy_Master.aspx");
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
                Session["User_ID"] = 1;

                // user inputs
                string parent_Designation = DD_Parent_Designation.SelectedValue;
                string designation_Name = Txt_Designation_Name.Text;
                string designation_Code = Txt_Designation_Code.Text;
                string level = DD_Level_Type.SelectedValue;
                string User_ID = Session["User_ID"].ToString();

                string Designation_ID = executeClass.Get_Next_RefID("M_Designation", "Designation_ID");

                string OperationStatus = string.IsNullOrEmpty(ViewState["Operation"]?.ToString()) ? string.Empty : ViewState["Operation"].ToString();

                if (OperationStatus == "INSERT" && Btn_Submit.Text == "Save")
                {
                    string sql = $@"INSERT INTO M_Designation 
                                    (Designation_ID, Parent_Designation_ID, Level_ID, DesignationName, DesignationCode, SavedBy) 
                                    VALUES 
                                    (@Designation_ID, @Parent_Designation_ID, @Level_ID, @DesignationName, @DesignationCode, @SavedBy)";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@Designation_ID", Designation_ID),
                        new SqlParameter("@Parent_Designation_ID", parent_Designation),
                        new SqlParameter("@Level_ID", level),
                        new SqlParameter("@DesignationName", designation_Name),
                        new SqlParameter("@DesignationCode", designation_Code),
                        new SqlParameter("@SavedBy", User_ID)
                    };

                    executeClass.ExecuteCommand(sql, command, parameters);

                    SweetAlert.GetSweet(this.Page, "success", "", $"Designation: <b>{Txt_Designation_Name.Text}</b> Created");
                }
                else if(OperationStatus == "UPDATE" && Btn_Submit.Text == "Update")
                {
                    Designation_ID = ViewState["Designation_ID"].ToString();

                    string sql = $@"UPDATE M_Designation SET Parent_Designation_ID=@Parent_Designation_ID, Level_ID=@Level_ID, 
                                        DesignationName=@DesignationName, DesignationCode=@DesignationCode
                                    WHERE Designation_ID = @Designation_ID";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@Parent_Designation_ID", parent_Designation),
                        new SqlParameter("@Level_ID", level),
                        new SqlParameter("@DesignationName", designation_Name),
                        new SqlParameter("@DesignationCode", designation_Code),
                        new SqlParameter("@Designation_ID", Designation_ID),
                    };

                    executeClass.ExecuteCommand(sql, command, parameters);

                    SweetAlert.GetSweet(this.Page, "success", "", $"Designation: <b>{Txt_Designation_Name.Text}</b> Updated");
                }
                else
                {
                    // if code flow comes here, means kuch to gadbad hai re baba !!
                }

                if (transaction.Connection != null)
                {
                    transaction.Commit();

                    Bind_Dropdown();
                    BindTreeView();

                    // clearing user inputs
                    DD_Parent_Designation.ClearSelection();
                    DD_Level_Type.ClearSelection();
                    Txt_Designation_Name.Text = string.Empty;
                    Txt_Designation_Code.Text = string.Empty;
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
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
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
                if (ViewState["Designation_ID"] != null && ViewState["Designation_ID"].ToString().Trim() != string.Empty)
                {
                    Int64 Designation_ID = Convert.ToInt32(ViewState["Designation_ID"]);

                    //if (dl.RecordExistanceChk("Tbl_Employee_Other_Details", "Designation_ID", id.ToString(), "Designation_ID"))

                    string sql = $@"UPDATE M_Designation SET IsDeleted = 1 WHERE Designation_ID = @Designation_ID";

                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@Designation_ID", Designation_ID),
                    };

                    executeClass.ExecuteCommand(sql, command, parameters);


                    if (transaction.Connection != null)
                    {
                        transaction.Commit();

                        Bind_Dropdown();
                        BindTreeView();

                        // clearing user inputs
                        DD_Parent_Designation.ClearSelection();
                        DD_Level_Type.ClearSelection();
                        Txt_Designation_Name.Text = string.Empty;
                        Txt_Designation_Code.Text = string.Empty;
                        Btn_Submit.Text = "Save";

                        SweetAlert.GetSweet(this.Page, "success", "", $"Designation: <b>{Txt_Designation_Name.Text}</b> deleted successfully");
                    }
                }
                else
                {
                    SweetAlert.GetSweet(this.Page, "info", "", $"Please select a node to delete");
                }
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
            }
        }
    }




}