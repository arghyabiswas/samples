using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SQLInjectionSample
{
    public partial class Login : System.Web.UI.Page
    {
        private string userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Logout();
            }
        }

        private void Logout()
        {
            FormsAuthentication.SignOut();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (IsValidUser())
            {
                FormsAuthentication.RedirectFromLoginPage(this.userName, false);
            }
            else
            {
                lblMessage.Text = "Invalid Username or Password";
            }
        }

        
        private bool IsValidUser()
        {
            string commandText = $"SELECT * FROM Users WHERE Username = '{txtUsername.Text}' AND Password = '{txtPassword.Text}'";

            // After Fixes
            //string commandText = $"SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

            bool isValid = false;
            using (SqlConnection oCnn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString))
            {
                oCnn.Open();
                using (SqlCommand oCmd = new SqlCommand())
                {
                    oCmd.Connection = oCnn;
                    oCmd.CommandText = commandText;

                    /* // After Fixes
                    oCmd.Parameters.Add("Username", SqlDbType.VarChar, 63);
                    oCmd.Parameters["Username"].Value = txtUsername.Text;

                    oCmd.Parameters.Add("Password", SqlDbType.VarChar, 127);
                    oCmd.Parameters["Password"].Value = txtPassword.Text;
                    */
                    oCmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            userName = Convert.ToString(oReader["Name"]);
                            isValid = true;
                        }
                    }
                }
            }

            return isValid;
        }

    }
}
