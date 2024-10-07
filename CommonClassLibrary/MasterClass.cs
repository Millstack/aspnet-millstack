using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CommonClassLibrary
{
    /// <summary>
    /// This Class is used to access the master / generic methods for faster development
    /// </summary>
    public class MasterClass
    {
        #region "Global Declaration"
        private ExecuteClass execls = new ExecuteClass();
        #endregion

        public MasterClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        /// <summary>
        /// Generates a unique key
        /// </summary>
        /// <returns>string</returns>
        public static string Get_Unique_Key()
        {
            int maxSize = 8;
            // int minSize = 5;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString().ToUpper();
        }


        /// <summary>
        /// Select item in a dropdown 
        /// </summary>
        /// <param name="ListControl object"></param>
        /// <param name="dropdown Value"></param>
        /// <param name="Boolean check optional"></param>
        /// <returns>Void</returns>
        public void Select_Item_In_DropDown(ListControl dropdownID, string value, bool multiple = false)
        {
            try
            {
                dropdownID.ClearSelection();

                if (multiple)
                {
                    // multi check dropdown (asp:ListBox)
                    string[] values = value.Split(',');
                    foreach (string val in values)
                    {
                        ListItem item = dropdownID.Items.FindByValue(val.Trim());
                        if (item != null) item.Selected = true;
                    }
                }
                else
                {
                    // dropdown list (asp:DropDownList)
                    foreach (ListItem item in dropdownID.Items)
                    {
                        if (item.Value == value)
                        {
                            item.Selected = true;
                            break; // Exit the loop once the item is found and selected
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Select_Item_In_DropDown_With_DT(Page page, ListControl dropdownID, DataTable dt, string ColumnName, bool multiple = false)
        {
            try
            {
                dropdownID.ClearSelection();

                if (multiple)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string ID = row[ColumnName].ToString().Trim();
                        ListItem item = dropdownID.Items.FindByValue(ID);
                        if (item != null) item.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(page, "error", $"", $"{ex.Message}");
            }
        }



        /// <summary>
        /// Get item from a dropdown OR Items from multi-check dropdown
        /// </summary>
        /// <param name="ListControl object"></param>
        /// <param name="dropdown Value"></param>
        /// <param name="Boolean check optional"></param>
        /// <returns>String Value</returns>
        public string Get_Selected_Items_From_DropDown(ListControl dropdownID)
        {
            try
            {
                // returning only selected items in comma seperated format
                return string.Join(",", dropdownID.Items.Cast<ListItem>().Where(item => item.Selected).Select(item => item.Value));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// Get checked item from CheckBox List
        /// </summary>
        /// <param name="CheckBoxList object"></param>
        /// <returns>String Value</returns>
        public string Get_CheckboxList_Checked_Values(CheckBoxList checkBoxList)
        {
            try
            {
                // returning only checked items in a comma seperated string format
                return string.Join(",", checkBoxList.Items.Cast<ListItem>().Where(item => item.Selected).Select(item => item.Value));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }














        /// <summary>
        /// Get all states
        /// </summary>
        /// <param name="Country_IDID optional"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_State(string Country_ID = null)
        {
            string sql = $@"SELECT State_ID, ISNULL(StateNameMr,StateName) as StateName 
                            FROM Tbl_M_State 
                            Where IsDeleted IS NULL 
                            {(string.IsNullOrEmpty(Country_ID) ? "" : " AND Country_ID = @Country_ID")} 
                            Order By StateName";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Country_ID))
            {
                parameters.Add("@Country_ID", Country_ID);
            }

            return execls.Get_Datatable(sql, parameters);
        }


        /// <summary>
        /// Get all divisions using state ID
        /// </summary>
        /// <param name="State ID"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_Division(string State_ID = null)
        {
            var sql = $@"SELECT Division_ID, ISNULL(DivisionNameMr, DivisionName) as DivisionName 
                         FROM Tbl_M_Division 
                         WHERE IsDeleted IS NULL 
                         {(string.IsNullOrEmpty(State_ID) ? "" : " AND State_ID = @State_ID")} 
                         ORDER BY DivisionName";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(State_ID))
            {
                parameters.Add("@State_ID", State_ID);
            }

            return execls.Get_Datatable(sql, parameters);
        }


        /// <summary>
        /// Get all districts using division ID
        /// </summary>
        /// <param name="Division ID"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_District(string Division_ID = null)
        {
            var sql = $@"SELECT District_ID, ISNULL(DistrictNameMr, DistrictName) as DistrictName  
                         FROM Tbl_M_District 
                         WHERE IsDeleted IS NULL 
                         {(string.IsNullOrEmpty(Division_ID) ? "" : " AND Division_ID = @Division_ID")} 
                         ORDER BY DistrictName";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Division_ID))
            {
                parameters.Add("@Division_ID", Division_ID);
            }

            return execls.Get_Datatable(sql, parameters);
        }


        /// <summary>
        /// Get all taluka using district ID
        /// </summary>
        /// <param name="District ID"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_Taluka(string District_ID = null)
        {
            var sql = $@"SELECT Taluka_ID, ISNULL(TalukaNameMr, TalukaName) as TalukaName 
                         FROM Tbl_M_Taluka 
                         WHERE IsDeleted IS NULL 
                         {(string.IsNullOrEmpty(District_ID) ? "" : " AND District_ID = @District_ID")} 
                         ORDER BY TalukaName";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(District_ID))
            {
                parameters.Add("@District_ID", District_ID);
            }

            return execls.Get_Datatable(sql, parameters);
        }


        /// <summary>
        /// Get all city using taluka ID
        /// </summary>
        /// <param name="Taluka ID"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_City(string Taluka_ID = null)
        {
            var sql = $@"SELECT City_ID, ISNULL(CityNameMr, CityName) as CityName 
                         FROM Tbl_M_City 
                         WHERE IsDeleted IS NULL 
                         {(string.IsNullOrEmpty(Taluka_ID) ? "" : " AND Taluka_ID = @Taluka_ID")} 
                         ORDER BY CityName";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(Taluka_ID))
            {
                parameters.Add("@Taluka_ID", Taluka_ID);
            }

            return execls.Get_Datatable(sql, parameters);
        }


        /// <summary>
        /// Get all village using city ID
        /// </summary>
        /// <param name="City ID"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_Village(string City_ID = null)
        {
            var sql = $@"SELECT Village_ID, ISNULL(VillageNameMr, VillageName) as VillageName 
                         FROM Tbl_M_Village 
                         WHERE IsDeleted IS NULL 
                         {(string.IsNullOrEmpty(City_ID) ? "" : " AND City_ID = @City_ID")} 
                         ORDER BY VillageName";

            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(City_ID))
            {
                parameters.Add("@City_ID", City_ID);
            }

            return execls.Get_Datatable(sql, parameters);
        }












        /// <summary>
        /// Get safe value like decimal, int, date
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="valueType"></param>
        /// <returns>Respective Value</returns>
        public object Get_Safe_Value(object value, Type columnType)
        {
            if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                return DBNull.Value;

            if (columnType == typeof(decimal))
                return Get_Safe_Decimal(value);
            if (columnType == typeof(long)) // BigInt in SQL
                return Convert.ToInt64(value);
            if (columnType == typeof(int))
                return Convert.ToInt32(value);
            if (columnType == typeof(DateTime))
                return Get_Safe_Date(value);
            if (columnType == typeof(string))
                return value.ToString();
            if (columnType == typeof(bool))
                return Convert.ToBoolean(value);

            // Add more types as needed
            return value.ToString(); // Default to string if no matching type is found
        }


        /// <summary>
        /// Get safe decimal value
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>Decimal</returns>
        public decimal Get_Safe_Decimal(object value)
        {
            if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString())) return 0;
            decimal result;
            if (decimal.TryParse(value.ToString(), out result)) return result;
            return 0;
        }


        /// <summary>
        /// Get safe date value format
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>Date Format</returns>
        public object Get_Safe_Date(object value)
        {
            DateTime parsedDate;
            if (value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString())) return DBNull.Value;
            bool isDateValid = DateTime.TryParseExact(value.ToString(), "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out parsedDate);
            if (isDateValid && parsedDate >= new DateTime(1753, 1, 1) && parsedDate <= new DateTime(9999, 12, 31)) return parsedDate;
            return DBNull.Value;
        }


        /// <summary>
        /// Get DataTable with data types using Dictionary
        /// </summary>
        /// <param name="Dictionary Object"></param>
        /// <returns>DataTable</returns>
        public DataTable Get_DataTable_From_Dictionary(Dictionary<string, Type> table_Columns)
        {
            DataTable dt = new DataTable();

            // Iterate over the dictionary and add columns with the specified types
            foreach (var column in table_Columns)
            {
                dt.Columns.Add(column.Key, column.Value);
            }

            return dt;

            // In .NET Framework, typeof() returns the correct Type object for a given data type :
            // typeof(Int64) maps to System.Int64 (alias: long)
            // typeof(Int32) maps to System.Int32 (alias: int)
            // typeof(DateTime) maps to System.DateTime
            // typeof(Decimal) maps to System.Decimal
            // typeof(String) maps to System.String
        }



        /// <summary>
        /// Bind GridView dynamically with DataTanle's columns
        /// </summary>
        /// <param name="GridView object"></param>
        /// <param name="DataTable object"></param>
        /// <param name="ViewState Object"></param>
        /// <param name="ViewState Key"></param>
        /// <returns>DataTable</returns>
        public void Bind_GridView_Dynamic(GridView gridView, DataTable dt, StateBag viewState, string viewStateKey)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    // Turn ON column auto generation
                    gridView.AutoGenerateColumns = true;

                    gridView.DataSource = dt;
                    gridView.DataBind();

                    // Save DataTable in ViewState
                    viewState[viewStateKey] = dt;

                    // Clear existing columns
                    gridView.Columns.Clear();

                    // Dynamically creating BoundFields or Columns from the data source
                    foreach (DataColumn col in dt.Columns)
                    {
                        BoundField boundField = new BoundField
                        {
                            DataField = col.ColumnName,
                            HeaderText = col.ColumnName
                        };
                        gridView.Columns.Add(boundField);
                    }

                    // Turn OFF column auto generation
                    gridView.AutoGenerateColumns = false;
                }
                else
                {
                    gridView.DataSource = null;
                    gridView.DataBind();

                    // Clear ViewState if there's no data
                    viewState[viewStateKey] = null;
                }
            }
            catch(Exception ex)
            {
                //SweetAlert.GetSweet();
            }
        }

    }
}
