using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace CommonClassLibrary
{
    /// <summary>
    /// This Class is used to communicate with the Database
    /// </summary>
    public class ExecuteClass
    {
        #region "Global Declaration"
        private SqlConnection con = new SqlConnection();
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter ad = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        #endregion

        public ExecuteClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public bool Check_To_Allow_Delete(Page page, string foreignKeyName, string foreignKeyValue)
        {
            string sql = string.Empty;
            bool canDelete = true;

            try
            {
               // getting all tables that reference the foreign key
                sql = @"SELECT OBJECT_NAME(FK.parent_object_id) AS ReferringTable
                         FROM sys.foreign_keys AS FK
                         INNER JOIN sys.foreign_key_columns AS FKC
                         ON FKC.constraint_object_id = FK.OBJECT_ID
                         WHERE COL_NAME(FK.referenced_object_id, FKC.referenced_column_id) = @ForeignKeyName
                         ORDER BY OBJECT_NAME(FK.parent_object_id)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@ForeignKeyName", foreignKeyName }
                };

                DataTable Referring_Tables_DT = Get_Datatable(sql, parameters);

                if (Referring_Tables_DT != null && Referring_Tables_DT.Rows.Count > 0)
                {
                    foreach (DataRow row in Referring_Tables_DT.Rows)
                    {
                        string tableName = row["ReferringTable"].ToString();

                        sql = $"SELECT TOP 1 * FROM {tableName} WHERE IsDeleted IS NULL AND {foreignKeyName} = @ForeignKeyValue";

                        Dictionary<string, object> checkParameters = new Dictionary<string, object>
                        {
                            { "@ForeignKeyValue", foreignKeyValue }
                        };

                        DataTable result = Get_Datatable(sql, checkParameters);

                        if (result != null && result.Rows.Count > 0)
                        {
                            // if any records are found, the delete is not allowed
                            canDelete = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(page, "error", "", $"{ex.Message}");
            }

            return canDelete;
        }


        /// <summary>
        /// Get DataTable
        /// </summary>
        /// <param name="SQL Query"></param>
        /// <param name="Dictionary Parameters"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_Datatable(string SQL_Query, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                connection.Open();

                try
                {
                    using (SqlCommand cmd = new SqlCommand(SQL_Query, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    //connection.Close();
                }
            }

            return dt.Rows.Count > 0 ? dt : null;
        }


        /// <summary>
        /// Get DataTable From Stored Procedure
        /// </summary>
        /// <param name="String Procedure Name"></param>
        /// <param name="Dictionary Object"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_DataTable_From_StoredProcedure(Page page, string storedProcedureName, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        command.Transaction = transaction;

                        try
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dt);
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            //throw;
                            SweetAlert.GetSweet(page, "error", $"", $"{ex.Message}");
                        }
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// Get DataSet From Stored Procedure
        /// </summary>
        /// <param name="String Procedure Name"></param>
        /// <param name="Dictionary Object"></param>
        /// <returns>DataTable</returns>
        public DataSet Get_DataSet_From_StoredProcedure(Page page, string storedProcedureName, Dictionary<string, object> parameters = null)
        {
            DataSet ds = new DataSet();

            using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        command.Transaction = transaction;

                        try
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(ds);
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            SweetAlert.GetSweet(page, "error", $"", $"{ex.Message}");
                        }
                    }
                }
            }

            return ds;
        }


        /// <summary>
        /// Get Flipped DataTable
        /// </summary>
        /// <param name="DataTable"></param>
        /// <returns>DataTable</returns>
        public DataTable Flip_DataTable(DataTable dt)
        {
            DataTable table = new DataTable();

            // Add columns to the new DataTable
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                table.Columns.Add(Convert.ToString(i));
            }

            // Add rows to the new DataTable
            foreach (DataColumn column in dt.Columns)
            {
                DataRow newRow = table.NewRow();
                newRow[0] = column.ColumnName;

                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    newRow[rowIndex++] = row[column];
                }

                table.Rows.Add(newRow);
            }

            return table;
        }


        /// <summary>
        /// Get Next Primary Key From The Given Table 
        /// </summary>
        /// <param name="Table Name"></param>
        /// <param name="Primary Key Name"></param>
        /// <param name="SqlCommand Object"></param>
        /// <returns>Primary Key</returns>
        public string Get_Next_RefID(string TableName, string ColumnName)
        {
            try
            {
                string sql_Query = $@"SELECT ISNULL(MAX(CAST({ColumnName} AS INT)), 0) + 1 AS Next_ID FROM {TableName}";

                var parameters = new Dictionary<string, object>
                {
                    //{ "@ColumnName", ColumnName },
                    //{ "@TableName", TableName },
                };

                dt = Get_Datatable(sql_Query, parameters);
            }
            catch (Exception ex)
            {

            }

            return dt.Rows.Count > 0 ? dt.Rows[0]["Next_ID"].ToString() : "1";
        }


        /// <summary>
        /// Bind dropdown generic method 
        /// </summary>
        /// <param name="ListControl object"></param>
        /// <param name="String sql query"></param>
        /// <param name="String textField"></param>
        /// <param name="String ValueField"></param>
        /// <param name="Dictionary object"></param>
        /// <param name="Boolean optional"></param>
        /// <returns>Void</returns>
        public void Bind_Dropdown_Generic(ListControl dropdownID, string sql, string textField, string valueFiled, Dictionary<string, object> parameters = null, bool multiple = false)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        SqlDataAdapter ad = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (multiple)
                            {
                                dropdownID.DataSource = dt;
                                dropdownID.DataTextField = textField;
                                dropdownID.DataValueField = valueFiled;
                                dropdownID.DataBind();
                            }
                            else
                            {
                                dropdownID.DataSource = dt;
                                dropdownID.DataTextField = textField;
                                dropdownID.DataValueField = valueFiled;
                                dropdownID.DataBind();
                                dropdownID.Items.Insert(0, new ListItem(" ", ""));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // error
                }
                finally
                {
                    //connection.Close();
                }
            }
        }


        /// <summary>
        /// Bind dropdown Using Datatable
        /// </summary>
        /// <param name="Page object"></param>
        /// <param name="ListControl object"></param>
        /// <param name="Datatable Object"></param>
        /// <param name="String textField"></param>
        /// <param name="String ValueField"></param>
        /// <param name="Dictionary object"></param>
        /// <param name="Boolean optional"></param>
        /// <returns>Void</returns>
        public void Bind_Dropdown_With_DT(Page page, ListControl dropdownID, DataTable dt, string textField, string valueFiled, Dictionary<string, object> parameters = null, bool multiple = false)
        {
            try
            {
                if (multiple)
                {
                    dropdownID.DataSource = dt;
                    dropdownID.DataTextField = textField;
                    dropdownID.DataValueField = valueFiled;
                    dropdownID.DataBind();
                }
                else
                {
                    dropdownID.DataSource = dt;
                    dropdownID.DataTextField = textField;
                    dropdownID.DataValueField = valueFiled;
                    dropdownID.DataBind();
                    dropdownID.Items.Insert(0, new ListItem(" ", ""));
                }
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(page, "error", "", $"{ex.Message}");
            }
        }


        /// <summary>
        /// Bind CheckBoxList generic method 
        /// </summary>
        /// <param name="CheckBoxList object"></param>
        /// <param name="String sql query"></param>
        /// <param name="String textField"></param>
        /// <param name="String ValueField"></param>
        /// <param name="Dictionary object"></param>
        /// <returns>Void</returns>
        public void Bind_CheckBoxList_Generic(CheckBoxList checkBoxListID, string sql, string textField, string valueField, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }

                        SqlDataAdapter ad = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        ad.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            checkBoxListID.DataSource = dt;
                            checkBoxListID.DataTextField = textField;
                            checkBoxListID.DataValueField = valueField;
                            checkBoxListID.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the error (e.g., log it)
                }
            }
        }


        /// <summary>
        /// Executing SQL Command 
        /// </summary>
        /// <param name="sqlQuery String"></param>
        /// <param name="SQL Command Object"></param>
        /// <param name="SQL Parameter Object"></param>
        /// <param name="SQL Transaction Object"></param>
        /// <returns>Void</returns>
        public void ExecuteCommand(string sql, SqlCommand command, List<SqlParameter> parameters = null, SqlTransaction transaction = null)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = sql;

            if (transaction != null) command.Transaction = transaction;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }

            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }


        /// <summary>
        /// Executing SQL Command 
        /// </summary>
        /// <param name="Page Control Onject"></param>
        /// <param name="Stored Procedure Name"></param>
        /// <param name="Dictionary Object"></param>
        /// <returns>Void</returns>
        public void ExecuteStoredProcedure(Page page, string storedProcedureName, Dictionary<string, object> parameters, DataTable TVP_Role_ID_DT = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }

                    // Add the TVP parameter if it exists
                    if (TVP_Role_ID_DT != null)
                    {
                        SqlParameter tvpParam = command.Parameters.AddWithValue("@RoleIDTVP", TVP_Role_ID_DT);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.RoleIDTableType";
                    }

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        command.Transaction = transaction;
                        try
                        {
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            SweetAlert.GetSweet(page, "info", "", $"{ex.Message}");
                        }
                    }
                }
            }
        }


    }
}
