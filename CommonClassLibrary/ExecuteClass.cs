using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

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
                string sql_Query = $@"SELECT ISNULL(MAX(CAST(@ColumnName AS INT)), 0) + 1 AS Next_ID FROM @TableName";

                var parameters = new Dictionary<string, object>
                {
                    { "@ColumnName", ColumnName },
                    { "@TableName", TableName },
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




    }
}
