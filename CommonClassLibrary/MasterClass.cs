using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        public void Select_Item_In_DropDown(ListControl dropdownID, string value, bool isMultiple = false)
        {
            try
            {
                dropdownID.ClearSelection();

                if (isMultiple)
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


        /// <summary>
        /// Get item from a dropdown OR Items from multi-check dropdown
        /// </summary>
        /// <param name="ListControl object"></param>
        /// <param name="dropdown Value"></param>
        /// <param name="Boolean check optional"></param>
        /// <returns>String Value</returns>
        public string Get_Selected_Items_From_DropDown(ListControl dropdownID, bool ForSQL = false)
        {
            try
            {
                List<string> selectedValues = new List<string>();
                foreach (ListItem item in dropdownID.Items)
                {
                    if (item.Selected)
                    {
                        if (ForSQL) selectedValues.Add($"'{item.Value}'");
                        else selectedValues.Add(item.Value);
                    }
                }
                return string.Join(",", selectedValues);
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


    }
}
