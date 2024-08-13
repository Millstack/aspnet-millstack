using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_Login : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // setting session timeout in minutes
            //Session.Timeout = 30;

            // destroying the sessions
            DestroyingExisitingSessions();
        }
    }

    private void DestroyingExisitingSessions()
    {
        // clearing existing sessions
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
    }


    //------------==================( Login )==================------------

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = LoginEmail.Text;
        string password = LoginPassword.Text;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                AuthenticateUser(con, transaction, email, password);

                if(transaction.Connection != null)
                {
                    //transaction.Commit();

                    // deliberate process delay
                    System.Threading.Thread.Sleep(1000);

                    Response.Redirect("~/View/HomePage/HomePage.aspx");
                }
            }
            catch (Exception ex)
            {
                //getSweetHTML("error", "Oops!", $"{ex.Message}", "");
                SweetAlert.GetSweet(this.Page, "error", "Oops!", $"{ex.Message}");
                transaction.Rollback();
            }
            finally
            {
                con.Close();
                transaction.Dispose();
            }
        }
    }

    private void AuthenticateUser(SqlConnection con, SqlTransaction transaction, string email, string password)
    {
        string sql = $@"SELECT um.*, rm.RoleName
                        FROM UserMaster as um 
                        inner join UserRoles as ur on ur.UserID = um.ID
                        inner join RoleMaster as rm on rm.ID = ur.RoleID
                        WHERE um.UserEmail = @UserEmail AND um.UserPassword = @UserPassword AND um.IsActive = 1";

        SqlCommand cmd = new SqlCommand(sql, con, transaction);
        cmd.Parameters.AddWithValue("@UserEmail", email);
        cmd.Parameters.AddWithValue("@UserPassword", password);
        cmd.ExecuteNonQuery();

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        ad.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            // fetching user details
            string userID = dt.Rows[0]["ID"].ToString();
            string userName = dt.Rows[0]["UserName"].ToString();
            string userRole = dt.Rows[0]["RoleName"].ToString();

            // creating session
            Session["UserID"] = userID;
            Session["UserRole"] = userRole;
            Session["UserName"] = userName;

            LoginFailedLiteral.Text = "";
        }
        else
        {
            LoginFailedLiteral.Text = "User email or password wrong, please check";
        }
    }



}