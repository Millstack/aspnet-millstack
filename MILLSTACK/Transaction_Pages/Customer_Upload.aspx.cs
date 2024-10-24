using CommonClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Owin.BuilderProperties;
using System.Data.Entity.Hierarchy;
using System.IdentityModel.Protocols.WSTrust;

public partial class Transaction_Pages_Customer_Upload : System.Web.UI.Page
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
                // deciding expected excel column names
                Dictionary<string, Type> table_Columns = new Dictionary<string, Type>()
                {
                    { "List_No", typeof(Int64) },
                    { "Serial_No", typeof(Int64) },
                    { "Customer_Name", typeof(string) },
                    { "Customer_MobileNo", typeof(string) },
                    { "Gender_ID", typeof(string) },
                    { "WRN_No", typeof(Int64) },
                    { "CustomerType_ID", typeof(string) },
                    { "Voting_Booth", typeof(Int64) },
                    { "Voting_Room", typeof(Int64) },
                    { "Ward_ID", typeof(Int64) },
                    { "Sector_ID", typeof(Int64) },
                    { "Society_ID", typeof(Int64) },
                };

                ViewState["Table_Columns"] = string.Empty;
                ViewState["Table_Columns"] = table_Columns;
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"An error occured: {ex.Message}");
        }
    }



    //-------------------------------] Upload Event [-------------------------------

    protected void Btn_Upload_Click(object sender, EventArgs e)
    {
        try
        {
            if (File_Upload.HasFile)
            {
                string FileExtension = System.IO.Path.GetExtension(File_Upload.FileName);

                if (FileExtension == ".xlsx" || FileExtension == ".xls")
                {
                    string unique_File_Name = $@"{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_fff")}_{File_Upload.FileName}{System.IO.Path.GetExtension(File_Upload.PostedFile.FileName)}";

                    string virtualPath = Server.MapPath("/Excel_Upload/Customer");
                    if (!Directory.Exists(virtualPath)) Directory.CreateDirectory(virtualPath);

                    string fullPath = System.IO.Path.Combine(virtualPath, unique_File_Name);
                    File_Upload.PostedFile.SaveAs(fullPath); // Save the file to the server

                    using (ExcelPackage package = new ExcelPackage(new FileInfo(fullPath)))
                    {
                        // Licence for Non-Commercial applications
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        ExcelWorksheet worksheet = null;

                        string sheetName = Txt_Sheet_Name.Text.Trim();

                        if (!string.IsNullOrEmpty(sheetName))
                        {
                            worksheet = package.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == sheetName);
                        }

                        if (worksheet != null)
                        {
                            // Access data from the worksheet
                            int rowCount = worksheet.Dimension.Rows;
                            int colCount = worksheet.Dimension.Columns;

                            DataTable dt = new DataTable();

                            bool columnsValid = true;
                            StringBuilder errorMessages = new StringBuilder();

                            // getting expected columns
                            //var tableColumns = CNDSClass.Scheme_Master;
                            var tableColumns = ViewState["Table_Columns"] as Dictionary<string, Type>;

                            // loop tp check if the column is expected
                            for (int col = 1; col <= colCount; col++)
                            {
                                string columnName = worksheet.Cells[1, col].Text.Trim();

                                // Check if the column is expected
                                if (tableColumns.ContainsKey(columnName))
                                {
                                    dt.Columns.Add(columnName);
                                }
                                else
                                {
                                    errorMessages.Append($"Unexpected column '{columnName}' found in Excel. ");
                                    columnsValid = false;
                                }
                            }

                            // If all columns are valid, proceed
                            if (columnsValid)
                            {
                                // Adding an "Error" column to store error messages for each row
                                //dt.Columns.Add("Error", typeof(string));

                                try
                                {
                                    for (int row = 2; row <= rowCount; row++)
                                    {
                                        DataRow dataRow = dt.NewRow();

                                        for (int col = 1; col <= colCount; col++)
                                        {
                                            string cellValue = worksheet.Cells[row, col].Text.Trim();
                                            string columnName = dt.Columns[col - 1].ColumnName;

                                            // Check if the cell value is empty or has whitespace
                                            if (string.IsNullOrWhiteSpace(cellValue))
                                            {
                                                // Assign default or DBNull.Value based on the column type
                                                var columnType = tableColumns[columnName];

                                                if (columnType == typeof(decimal) || columnType == typeof(int) || columnType == typeof(Int64))
                                                {
                                                    dataRow[col - 1] = 0; // Default value for numeric columns
                                                }
                                                else if (columnType == typeof(DateTime))
                                                {
                                                    dataRow[col - 1] = DBNull.Value; // Default value for DateTime
                                                }
                                                else
                                                {
                                                    dataRow[col - 1] = DBNull.Value; // Default for other types
                                                }
                                            }
                                            else
                                            {
                                                // Convert data if the cell is not empty
                                                var columnType = tableColumns[columnName];

                                                if (columnType == typeof(decimal) || columnType == typeof(int) || columnType == typeof(Int64))
                                                {
                                                    dataRow[col - 1] = masterClass.Get_Safe_Decimal(cellValue);
                                                }
                                                else if (columnType == typeof(DateTime))
                                                {
                                                    dataRow[col - 1] = masterClass.Get_Safe_Date(cellValue);
                                                }
                                                else
                                                {
                                                    // Convert other data types
                                                    object converted_Value = Convert.ChangeType(cellValue, columnType);
                                                    dataRow[col - 1] = converted_Value;
                                                }
                                            }
                                        }

                                        dt.Rows.Add(dataRow);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
                                }

                                Bind_GridView(dt);
                            }
                            else
                            {
                                SweetAlert.GetSweet(this.Page, "info", "Invalid Excel Format!", $"Excel columns were not matched, please check");
                            }
                        }
                        else
                        {
                            SweetAlert.GetSweet(this.Page, "info", "Invalid Worksheet Name!", $"The worksheet name {sheetName}was not found in the excel file. <br/> Please check the excel file properly.");
                        }
                    }
                }
                else
                {
                    SweetAlert.GetSweet(this.Page, "warning", "", $"The Excel format <b>{FileExtension}</b> is not supported <br/> please check the allowed formats: <b>.xlsx</b> & <b>.xls</b>");
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    public void Bind_GridView(DataTable Customer_DT)
    {
        try
        {
            // creating new empty datatable for displaying errors
            DataTable dtErrors = new DataTable();
            dtErrors.Columns.Add("Customer_Name", typeof(string));
            dtErrors.Columns.Add("WRN_No", typeof(string));
            dtErrors.Columns.Add("Customer_MobileNo", typeof(string));
            dtErrors.Columns.Add("ErrorReason", typeof(string));

            // hash set for storing unique customer numbers only
            HashSet<string> customerNumbers = new HashSet<string>();

            // hash set for storing unique customer mobile number only
            HashSet<string> customerMobileNumbers = new HashSet<string>();

            // hash set for storing unique customer WRN No only
            HashSet<string> customer_WRN_No = new HashSet<string>();

            DataTable dt = ViewState["Customer_DT"] as DataTable ?? masterClass.Get_DataTable_From_Dictionary(ViewState["Table_Columns"] as Dictionary<string, Type>);

            foreach (DataRow row in Customer_DT.Rows)
            {
                //int rowIndex = dt.Rows.IndexOf(row);
                //dt.Rows.RemoveAt(rowIndex);

                // fetching the row data
                string List_No = row["List_No"].ToString();
                string Serial_No = row["Serial_No"].ToString();
                string customerName = row["Customer_Name"].ToString();
                string customerMobileNo = row["Customer_MobileNo"].ToString();
                string customerGender = row["Gender_ID"].ToString();
                string customerNumber = row["WRN_No"].ToString();
                string wardNumber = row["Ward_ID"].ToString();
                string sectorOrArea = row["Sector_ID"].ToString();
                string society = row["Society_ID"].ToString();

                string Voting_Booth = row["Voting_Booth"].ToString();
                string Voting_Room = row["Voting_Room"].ToString();

                // color columns
                string Customer_Type = row["CustomerType_ID"].ToString();

                // NOTE: yellow --> neutral, black --> against and orange --> kattar supporter

                // deciding the color selected
                //string customerType = string.Empty;
                //if (yellow == "1") customerType = "Yellow";
                //if (black == "1") customerType = "Black";
                //if (orange == "1") customerType = "Orange";

                bool hasError = false;
                string errorReason = string.Empty;

                // checking if all columns in the row have data inside DataTable
                //if (row.ItemArray.All(field => !string.IsNullOrEmpty(field.ToString()))) // condition to check all column data in DataTale
                if (row.ItemArray.All(field => !string.IsNullOrEmpty(field.ToString())))
                {
                    // checking for duplicate customer numbers
                    if (customerNumbers.Contains(customerNumber))
                    {
                        hasError = true;
                        errorReason = "Duplicate Customer No";
                    }
                    else
                    {
                        // adding unque customer no into HashSet
                        customerNumbers.Add(customerNumber);
                    }

                    // checking for duplicate mobile number
                    if (customerMobileNumbers.Contains(customerMobileNo))
                    {
                        hasError = true;
                        errorReason = string.IsNullOrEmpty(errorReason) ? "Duplicate Mobile Number" : $"{errorReason}, Duplicate Mobile Number";
                    }
                    else
                    {
                        // Add unique mobile number into HashSet
                        customerMobileNumbers.Add(customerMobileNo);
                    }

                    // checking for invalid mobile no and all digits only
                    if (customerMobileNo.Length != 10 || !customerMobileNo.All(char.IsDigit))
                    {
                        hasError = true;
                        errorReason = string.IsNullOrEmpty(errorReason) ? "Invalid Mobile Number" : $"{errorReason}, Invalid Mobile Number";
                    }

                    if (hasError) AddRowToErrorDataTable(dtErrors, customerNumber, customerName, customerMobileNo, errorReason);
                }
                else
                {
                    // if not all columns has data
                    errorReason = string.IsNullOrEmpty(errorReason) ? "Missing column data" : $"{errorReason}, Missing column data";

                    AddRowToErrorDataTable(dtErrors, customerNumber, customerName, customerMobileNo, errorReason);
                }

                //AddRowToItemDataTable(dt, customerName, customerMobileNo, customerGender, customerNumber, wardNumber, society, sectorOrArea, Customer_Type, "EXCEL");

                // still adding all records to original grdiview for the user to check it
                DataRow row_1 = dt.NewRow();

                if (!dt.Columns.Contains("Data_Entry_Mode")) dt.Columns.Add("Data_Entry_Mode", typeof(string));

                row_1["List_No"] = List_No;
                row_1["Serial_No"] = Serial_No;
                row_1["Customer_Name"] = customerName;
                row_1["Customer_MobileNo"] = customerMobileNo;
                row_1["Gender_ID"] = customerGender;
                row_1["WRN_No"] = customerNumber;
                row_1["Voting_Booth"] = Voting_Booth;
                row_1["Voting_Room"] = Voting_Room;
                row_1["Ward_ID"] = wardNumber;
                row_1["Sector_ID"] = sectorOrArea;
                row_1["Society_ID"] = society;
                row_1["CustomerType_ID"] = Customer_Type;
                row_1["Data_Entry_Mode"] = "EXCEL";
                dt.Rows.Add(row_1);
            }

            if (dt.Rows.Count > 0)
            {
                ExcelUploadDiv.Visible = false;
                CustomerDiv.Visible = true;

                //masterClass.Bind_GridView_Dynamic(GridCustomer, dt, ViewState, "Customer_DT");

                Grid_Customer.DataSource = dt;
                Grid_Customer.DataBind();

                ViewState["Customer_DT"] = dt;

                Txt_Sheet_Name.Text = string.Empty;
            }
            else
            {
                CustomerDiv.Visible = false;

                ViewState["Customer_DT"] = null;

                Txt_Sheet_Name.Text = string.Empty;
            }

            // populating error grdivew if there are errors
            if (dtErrors.Rows.Count > 0)
            {
                SubmitCancelButtonDiv.Visible = true;
                btnSubmit.Visible = false;

                //masterClass.Bind_GridView_Dynamic(GridErrors, dtErrors, ViewState, "ErrorRecords");

                ErrorGridDiv.Visible = true;

                ViewState["ErrorRecords"] = dtErrors;

                GridErrors.DataSource = dtErrors;
                GridErrors.DataBind();

                SweetAlert.GetSweet(this.Page, "warning", "Excel Errors!", $"Excel Errors Detected, From The Uploaded Excel File. Please Check The Data Carefully");
            }
            else
            {
                SubmitCancelButtonDiv.Visible = true;
                btnSubmit.Visible = true;

                ErrorGridDiv.Visible = false;

                ViewState["ErrorRecords"] = null;

                GridErrors.DataSource = null;
                GridErrors.DataBind();
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "error", "", $"{ex.Message}");
        }
    }

    private void AddRowToErrorDataTable(DataTable dtErrors, string customerNumber, string customerName, string customerMobileNo, string errorReason)
    {
        DataRow errorRow = dtErrors.NewRow();
        errorRow["Customer_Name"] = customerName;
        errorRow["Customer_No"] = customerNumber;
        errorRow["Customer_Mobile"] = customerMobileNo;
        errorRow["ErrorReason"] = errorReason;
        dtErrors.Rows.Add(errorRow);
    }




    //-------------------------------] GridView Event [-------------------------------

    protected void GridCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sql = string.Empty;
            Dictionary<string, object> parameters;

            TableCell customerTypeCell = e.Row.Cells[9];
            string customerType = DataBinder.Eval(e.Row.DataItem, "CustomerType_ID").ToString();
            DropDownList DD_Customer_Type = (DropDownList)e.Row.FindControl("DD_Customer_Type");

            string customerGender = DataBinder.Eval(e.Row.DataItem, "Gender_ID").ToString();
            DropDownList DD_Gender = (DropDownList)e.Row.FindControl("DD_Gender");

            if (DD_Customer_Type != null)
            {
                // customer type
                sql = $@"Select CustomerType_ID, CustomerName, CustomerCode, IsDeleted
                         From Tbl_M_CustomerType as ct
                         Where IsDeleted IS NULL";
                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(DD_Customer_Type, sql, "CustomerName", "CustomerCode", parameters);

                ListItem selectedItem = DD_Customer_Type.Items.FindByText(customerType);
                //ListItem selectedItem = DD_Customer_Type.Items.FindByValue(customerType);
                if (selectedItem != null) DD_Customer_Type.SelectedIndex = DD_Customer_Type.Items.IndexOf(selectedItem);
            }

            if (DD_Gender != null)
            {
                // gender
                sql = $@"Select Gender_ID, GenderName, GenderNameMr, GenderCode, IsDeleted 
                        From M_Gender 
                        Where IsDeleted IS NULL";
                parameters = new Dictionary<string, object> { /*{ "@Bank_ID", DD_Bank_Master.SelectedValue },*/ };
                executeClass.Bind_Dropdown_Generic(DD_Gender, sql, "GenderName", "Gender_ID", parameters);

                ListItem selected_Gender = DD_Gender.Items.FindByText(customerGender);
                //ListItem selected_Gender = DD_Gender.Items.FindByValue(customerGender);
                if (selected_Gender != null) DD_Gender.SelectedIndex = DD_Gender.Items.IndexOf(selected_Gender);
            }


            HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("DivWrapper");
            if (div != null)
            {
                string borderColor = GetColor(customerType);
                div.Attributes["style"] = $"border: 2px solid {borderColor};";
            }

            //// code for setting BG color of the cell
            //string selected_CustomerType = DD_Customer_Type.SelectedItem.Text;
            //string backgroundColor = GetColor(selected_CustomerType);
            //string textColor = backgroundColor == "black" ? "white" : "black";

            //customerTypeCell.Attributes["style"] = $"background-color: {backgroundColor}; color: {textColor};";
            //customerTypeCell.Text = customerType;

        }
    }

    protected void Grid_Customer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void DD_Customer_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get the DropDownList that triggered the event
        DropDownList DD_Customer_Type = (DropDownList)sender;

        // Find the GridViewRow that contains this dropdown
        GridViewRow row = (GridViewRow)DD_Customer_Type.NamingContainer;

        // Get the selected customer type from the dropdown
        string selectedCustomerType = DD_Customer_Type.SelectedItem.Text;

        // Find the div control in the same row
        HtmlGenericControl div = (HtmlGenericControl)row.FindControl("DivWrapper");

        if (div != null)
        {
            // Get the corresponding color based on the selected customer type
            string borderColor = GetColor(selectedCustomerType);

            // Update the div's border color
            div.Attributes["style"] = $"border: 2px solid {borderColor};";
        }
    }

    protected string GetColor(object color)
    {
        if (color == null)
        {
            return "white";
        }

        string customerType = color.ToString();

        switch (customerType)
        {
            case "Yellow":
                return "yellow";
            case "Black":
                return "black";
            case "Orange":
                return "orange";
            default:
                return "white";
        }
    }




    //-------------------------------] Submit Button Event [-------------------------------
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetRouteUrl("Customer_Upload_Route", null));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Grid_Customer.Rows.Count > 0)
        {
            try
            {
                ViewState["OPERATION"] = "INSERT";
                string OperationStatus = string.IsNullOrEmpty(ViewState["OPERATION"]?.ToString()) ? string.Empty : ViewState["OPERATION"].ToString();

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@Operation", OperationStatus },
                    { "@User_ID", ViewState["OPERATION"].ToString() == "INSERT" ? (object)DBNull.Value : Session["User_ID"] },

                    { "@List_No", XXXXXXXXXX },
                    { "@Serial_No", XXXXXXXXXX },
                    { "@Customer_Name", XXXXXXXXXX },
                    { "@Customer_MobileNo", XXXXXXXXXX },
                    { "@Gender_ID", XXXXXXXXXX },
                    { "@WRN_No", XXXXXXXXXX },
                    { "@CustomerType_ID", XXXXXXXXXX },
                    { "@Voting_Booth", XXXXXXXXXX },
                    { "@Voting_Room", XXXXXXXXXX },

                    { "@Ward_ID", XXXXXXXXXX },
                    { "@Sector_ID", XXXXXXXXXX },
                    { "@Society_ID", XXXXXXXXXX },
                    { "@Data_Entry_Mode", XXXXXXXXXX },

                    { "@SavedBy", Session["User_ID"] },
                };
            }
            catch (Exception ex)
            {
                SweetAlert.GetSweet(this.Page, "error", $"", $"{ex.Message}");
            }
        }
        else
        {
            SweetAlert.GetSweet(this.Page, "warning", "No Customer Found!", $"Kindly Upload Some Customers To Proceed Further");
        }
    }









}