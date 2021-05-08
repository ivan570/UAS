using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UAS_MSU
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void alert(String message)
        {
            string script = String.Format("alert('{0}');", message);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
        }
        protected void bt_login_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String username = textBox_username.Text;
            String password = textBox_password.Text;

            String query = "Select status from(select Email, Password, '1' as status from student union all" +
                            " Select Email, Password, '2' as status from Teacher union all " +
                            " Select Username as Email, Password, '3' as status from Admin union all " +
                            " Select Hod_Username as Email, Hod_Password as Password, " +
                            " '4' as status from Department ) " +
                            " as dt where lower(dt.Email) = '" + username.ToLower()
                            + "' and dt.Password = '" + password + "'; ";

            SqlCommand cmd = new SqlCommand(query, con);

            int status = -1;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    reader.Read();
                    status = Convert.ToInt32(reader["status"].ToString());
                }
                catch (Exception) { }
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            if (status == -1)
            {
                alert("invalid username and password");
            }
            else if (status == 1)
            {
                Session["student"] = username;
                Response.Redirect("~/Student/Home");
            }
            else if (status == 2)
            {
                Session["teacher"] = username;
                Response.Redirect("~/Teacher/Home");
            }
            else if (status == 3)
            {
                Session["admin"] = username;
                Response.Redirect("~/Admin/Home");
            }
            else if (status == 4)
            {
                Session["subadmin"] = username;
                Response.Redirect("~/SubAdmin/Home");
            }
        }
    }
}