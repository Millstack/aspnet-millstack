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
                Dictionary<string, Type> Customer_Columns = new Dictionary<string, Type>()
                {
                    { "SchemeName", typeof(string) },
                };

                ViewState["Customer_Columns"] = string.Empty;
                ViewState["Customer_Columns"] = Customer_Columns;
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
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                        ExcelWorksheet worksheet = null;

                        string sheetName = SheetName.Text.Trim();

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

                            // Assuming the first row contains column headers
                            for (int col = 1; col <= colCount; col++)
                            {
                                dt.Columns.Add(worksheet.Cells[1, col].Text);
                            }

                            // Starting from the second row to skip headers
                            for (int row = 2; row <= rowCount; row++)
                            {
                                DataRow dataRow = dt.NewRow();

                                for (int col = 1; col <= colCount; col++)
                                {
                                    dataRow[col - 1] = worksheet.Cells[row, col].Text;
                                }

                                dt.Rows.Add(dataRow);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                // Checking column names present in excel sheet or not
                                if (dt.Columns[0].ColumnName.Trim() == "CustomerName" && dt.Columns[1].ColumnName.Trim() == "MobileNo" &&
                                dt.Columns[2].ColumnName.Trim() == "Gender" && dt.Columns[3].ColumnName.Trim() == "CustomerNo" && dt.Columns[4].ColumnName.Trim() == "WardNo"
                                && dt.Columns[5].ColumnName.Trim() == "Society" && dt.Columns[6].ColumnName.Trim() == "SectorArea")
                                {

                                    // Method 1: delete data from Temp Table
                                    // Method 2: inserting data into temp table if needed

                                    InsertToGridView(dt);

                                    // Method 3: To display the inserted data from Temp or Main Table
                                }
                                else
                                {
                                    SweetAlert.GetSweet(this.Page, "warning", "Wrong Excel Columns!", $"The Excel Format Has Not Matched, <br/> Please Download The Correct Excel File Format");
                                }
                            }
                        }
                        else
                        {
                            SweetAlert.GetSweet(this.Page, "warning", "Invalid Worksheet Name!", $"The worksheet name: <strong>{sheetName}</strong> is not present in excel file. Please check excel file properly");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SweetAlert.GetSweet(this.Page, "warning", "Oops!", $"{ex.Message}");
        }
    }

    public void InsertToGridView(DataTable dtCustomer)
    {
        DataTable dt = ViewState["CustomerData"] as DataTable ?? createItemDatatable();

        // creating new empty datatable for displaying errors
        DataTable dtErrors = createErrorDatatable();

        // hash set for storing unique customer numbers only
        HashSet<string> customerNumbers = new HashSet<string>();

        // hash set for storing unique customer mobile no only
        HashSet<string> customerMobileNumbers = new HashSet<string>();

        foreach (DataRow row in dtCustomer.Rows)
        {
            //int rowIndex = dt.Rows.IndexOf(row);
            //dt.Rows.RemoveAt(rowIndex);

            // fetching the row data
            string customerName = row["CustomerName"].ToString();
            string customerMobileNo = row["MobileNo"].ToString();
            string customerGender = row["Gender"].ToString();
            string customerNumber = row["CustomerNo"].ToString();
            string wardNumber = row["WardNo"].ToString();
            string society = row["Society"].ToString();
            string sectorOrArea = row["SectorArea"].ToString();

            // color columns
            string yellow = row["Yellow"].ToString();
            string black = row["Black"].ToString();
            string orange = row["Orange"].ToString();

            // deciding the color selected
            string customerType = string.Empty;
            if (yellow == "1") customerType = "Yellow";
            if (black == "1") customerType = "Black";
            if (orange == "1") customerType = "Orange";

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

                if (hasError)
                {
                    AddRowToErrorDataTable(dtErrors, customerNumber, customerName, customerMobileNo, errorReason);
                }
            }
            else
            {
                // if not all columns has data
                errorReason = string.IsNullOrEmpty(errorReason) ? "Missing column data" : $"{errorReason}, Missing column data";

                AddRowToErrorDataTable(dtErrors, customerNumber, customerName, customerMobileNo, errorReason);
            }

            // still adding all records to original grdiview for the user to check it himself or herself
            AddRowToItemDataTable(dt, customerName, customerMobileNo, customerGender, customerNumber, wardNumber, society, sectorOrArea, customerType, "EXCEL");
        }

        if (dt.Rows.Count > 0)
        {
            ExcelUploadDiv.Visible = false;
            CustomerDiv.Visible = true;

            GridCustomer.DataSource = dt;
            GridCustomer.DataBind();

            ViewState["CustomerData"] = dt;

            SheetName.Text = string.Empty;
        }
        else
        {
            CustomerDiv.Visible = false;

            ViewState["CustomerData"] = null;

            SheetName.Text = string.Empty;
        }

        // populating error grdivew if there are errors
        if (dtErrors.Rows.Count > 0)
        {
            SubmitCancelButtonDiv.Visible = true;
            btnSubmit.Visible = false;

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


    // customer records
    private DataTable createItemDatatable()
    {
        DataTable dt = new DataTable();


        // customer name
        DataColumn CustomerName = new DataColumn("CustomerName", typeof(string));
        dt.Columns.Add(CustomerName);

        // customer phone no
        DataColumn PhoneNo = new DataColumn("MobileNo", typeof(string));
        dt.Columns.Add(PhoneNo);

        // customer gender
        DataColumn Gender = new DataColumn("Gender", typeof(string));
        dt.Columns.Add(Gender);

        // customer number
        DataColumn CustomerNumber = new DataColumn("CustomerNo", typeof(string));
        dt.Columns.Add(CustomerNumber);

        // customer ward no
        DataColumn WardNo = new DataColumn("WardNo", typeof(string));
        dt.Columns.Add(WardNo);

        // customer society
        DataColumn Society = new DataColumn("Society", typeof(string));
        dt.Columns.Add(Society);

        // customer sector / area
        DataColumn SectorOrArea = new DataColumn("SectorArea", typeof(string));
        dt.Columns.Add(SectorOrArea);

        // data entry mode
        DataColumn DataEntryMode = new DataColumn("DataEntryMode", typeof(string));
        dt.Columns.Add(DataEntryMode);

        dt.Columns.Add("CustomerType", typeof(string));

        return dt;
    }

    private void AddRowToItemDataTable(DataTable dt, string customerName, string customerMobileNo, string customerGender, string customerNumber, string wardNumber, string society, string sectorOrArea, string customerType, string enteryMode)
    {
        DataRow row = dt.NewRow();

        row["CustomerName"] = customerName;
        row["MobileNo"] = customerMobileNo;
        row["Gender"] = customerGender;
        row["CustomerNo"] = customerNumber;
        row["WardNo"] = wardNumber;
        row["Society"] = society;
        row["SectorArea"] = sectorOrArea;
        row["CustomerType"] = customerType;
        row["DataEntryMode"] = enteryMode;

        dt.Rows.Add(row);
    }


    // error records
    private DataTable createErrorDatatable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CustomerNo");
        dt.Columns.Add("CustomerName");
        dt.Columns.Add("MobileNo");
        dt.Columns.Add("ErrorReason");
        return dt;
    }

    private void AddRowToErrorDataTable(DataTable dtErrors, string customerNumber, string customerName, string customerMobileNo, string errorReason)
    {
        DataRow errorRow = dtErrors.NewRow();
        errorRow["CustomerNo"] = customerNumber;
        errorRow["CustomerName"] = customerName;
        errorRow["MobileNo"] = customerMobileNo;
        errorRow["ErrorReason"] = errorReason;
        dtErrors.Rows.Add(errorRow);
    }





    //--------------==============( Gridview OnRow Databound Event )==============--------------

    protected void GridCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // index based fetching the CustomerType column
            TableCell customerTypeCell = e.Row.Cells[8];

            // getting the cell value i.e. mentioned color
            string customerType = DataBinder.Eval(e.Row.DataItem, "CustomerType").ToString();

            string backgroundColor = GetColor(customerType);
            string textColor = "black";

            if (backgroundColor == "black")
            {
                textColor = "white";
            }

            customerTypeCell.Attributes["style"] = $"background-color: {backgroundColor}; color: {textColor};";
            customerTypeCell.Text = customerType;
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







    //=========================={ Submit Button Click Event }==========================
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UploadCustomer.aspx");
    }

    protected async void btnSubmit_Click(object sender, EventArgs e)
    {
        if (GridCustomer.Rows.Count > 0)
        {
            using (SqlConnection con = new SqlConnection(ConnectionClass.connection_String_Local))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(IsolationLevel.Serializable);

                try
                {
                    // checking for existing database records
                    DataTable dtErrors = await CheckForExistingCustomerRecords(con, transaction);

                    if (dtErrors.Rows.Count == 0)
                    {
                        // insert header
                        await InsertCustomerRecords(con, transaction);


                        if (transaction.Connection != null)
                        {
                            transaction.Commit();

                            SweetAlert.GetSweet(this.Page, "success", "Customers Uploaded!", $"The Customers List Uploaded Successfully", "~/View/Customer/UploadCustomer.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    SweetAlert.GetSweet(this.Page, "warning", "Oops!", $"{ex.Message}");
                }
                finally
                {
                    con.Close();
                    transaction.Dispose();
                }
            }
        }
        else
        {
            SweetAlert.GetSweet(this.Page, "warning", "No Customer Found!", $"Kindly Upload Some Customers To Proceed Further");
        }
    }





    private async Task<DataTable> CheckForExistingCustomerRecords(SqlConnection con, SqlTransaction transaction)
    {
        DataTable custDT = ViewState["CustomerData"] as DataTable;

        // creating error records datatable
        DataTable dtErrors = new DataTable();
        dtErrors.Columns.Add("CustomerNo");
        dtErrors.Columns.Add("CustomerName");
        dtErrors.Columns.Add("MobileNo");
        dtErrors.Columns.Add("ErrorReason");

        if (custDT.Rows.Count > 0)
        {
            foreach (DataRow row in custDT.Rows)
            {
                string customerName = row["CustomerName"].ToString();
                string customerNo = row["CustomerNo"].ToString();
                string mobileNo = row["MobileNo"].ToString();

                string sql = "SELECT CustomerNo FROM CustomerMaster WHERE CustomerNo = @CustomerNo";
                SqlCommand cmd = new SqlCommand(sql, con, transaction);
                cmd.Parameters.AddWithValue("@CustomerNo", customerNo);

                object result = await cmd.ExecuteScalarAsync();
                if (result != null)
                {
                    DataRow errorRow = dtErrors.NewRow();
                    errorRow["CustomerName"] = customerName;
                    errorRow["CustomerNo"] = customerNo;
                    errorRow["MobileNo"] = mobileNo;
                    errorRow["ErrorReason"] = $"Customer number {customerNo} already exists in the database, please check";
                    dtErrors.Rows.Add(errorRow);
                }
            }
        }

        if (dtErrors.Rows.Count > 0)
        {
            SubmitCancelButtonDiv.Visible = true;
            btnSubmit.Visible = true;

            ErrorGridDiv.Visible = true;

            GridErrors.DataSource = dtErrors;
            GridErrors.DataBind();

            SweetAlert.GetSweet(this.Page, "info", "Existing Customers!", $"There are existing <b>customer numbers</b> in the database, please check the error records");
        }

        return dtErrors;
    }

    private async Task InsertCustomerRecords(SqlConnection con, SqlTransaction transaction)
    {
        string userID = Session["UserId"].ToString();

        DataTable custDT = ViewState["CustomerData"] as DataTable;

        if (custDT.Rows.Count > 0)
        {
            foreach (DataRow row in custDT.Rows)
            {
                // fetching next refID
                //int customer_RefID = await GetNewCustomerRefID(con, transaction);

                // insertion
                string sql = $@"Insert Into CustomerMaster 
                                (CustomerName, MobileNo, Gender, CustomerNo, WardNo, Society, SectorArea, CustomerType, SavedBy) 
                                Values 
                                (@CustomerName, @MobileNo, @Gender, @CustomerNo, @WardNo, @Society, @SectorArea, @CustomerType, @SavedBy)";

                SqlCommand cmd = new SqlCommand(sql, con, transaction);
                cmd.Parameters.AddWithValue("@CustomerName", row["CustomerName"]);
                cmd.Parameters.AddWithValue("@MobileNo", row["MobileNo"]);
                cmd.Parameters.AddWithValue("@Gender", row["Gender"]);
                cmd.Parameters.AddWithValue("@CustomerNo", row["CustomerNo"]);
                cmd.Parameters.AddWithValue("@WardNo", row["WardNo"]);
                cmd.Parameters.AddWithValue("@Society", row["Society"]);
                cmd.Parameters.AddWithValue("@SectorArea", row["SectorArea"]);
                cmd.Parameters.AddWithValue("@CustomerType", row["CustomerType"]);
                cmd.Parameters.AddWithValue("@SavedBy", userID);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }



}