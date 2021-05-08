using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;

namespace UAS_MSU
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
        static String otp = "0";
        static String who;

        protected void alert(String message)
        {
            string script = String.Format("alert('{0}');", message);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Control control = this.Master.FindControl("navbar");// "masterDiv"= the Id of the div.
                control.Visible = false;//to set the div to be hidden.
            }
        }
        protected void bt_get_otp_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            String username = textBox_username.Text;

            String query = "Select status from(select Email, '1' as status from student union all Select Hod_Username as Email, '2' as status from Department union all Select Email, '3' as status from Teacher ) as dt where lower(dt.Email) = '" + username + "'";

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

            if(status == 1)
            {
                who = "Student";
            }
            else if (status == 2)
            {
                who = "SubAdmin";
            }
            else if(status == 3)
            {
                who = "Teacher";
            }
            

            if (status == -1)
            {
                alert("Invalid Username");
            }
            else if (status == 1 || status == 2 || status == 3 || status == 4)
            {
                Random r = new Random();
                String genRand = r.Next(100000, 999999).ToString();
                otp = genRand;

                String to = textBox_username.Text;
                String subject = "Forgot Password on UAS_MSU";
                String body = "OTP for forgot password is : " + genRand;
                String successMessage = "OTP sent successfully.";
                String errorMessage = "Invalid email id";

                int temp = Constant.sendEmail(this, to, subject, body, successMessage, errorMessage);
                
                if(temp == 1)
                {
                    textBox_username.ReadOnly = true;
                    textBox_otp.Enabled = true;
                    bt_verify.Enabled = true;
                    bt_resend_otp.Enabled = true;
                    bt_get_otp.Enabled = false;
                }
            }

            con.Close();
        }

        protected void bt_reset_Click(object sender, EventArgs e)
        {
            textBox_username.Text = "";
            textBox_otp.Text = "";
            textBox_otp.Enabled = false;
            textBox_set_password.Enabled = false;
            textBox_confirm_password.Enabled = false;
            bt_forgot.Enabled = false;
            bt_verify.Enabled = false;
            bt_resend_otp.Enabled = false;
        }

        protected void bt_verify_Click(object sender, EventArgs e)
        {
            if (textBox_otp.Text == otp)
            {
                alert("OTP verified.");
                textBox_otp.ReadOnly = true;
                textBox_set_password.Enabled = true;
                textBox_confirm_password.Enabled = true;
                bt_forgot.Enabled = true;
                bt_verify.Enabled = false;
                bt_resend_otp.Enabled = false;
            }
            else
            {
                alert("Invalid OTP");
            }
        }
        protected void bt_resend_otp_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            String genRand = r.Next(100000, 999999).ToString();
            otp = genRand;

            String to = textBox_username.Text;
            String subject = "Forgot Password on UAS_MSU";
            String body = "OTP for forgot password is : " + genRand;
            String successMessage = "New OTP sent successfully.";
            String errorMessage = "Invalid email id";

            int temp = Constant.sendEmail(this, to, subject, body, successMessage, errorMessage);

            if (temp == 1)
            {
                
                textBox_username.ReadOnly = true;
                textBox_otp.Enabled = true;
                bt_verify.Enabled = true;
            }
        }

        protected void bt_forgot_Click(object sender, EventArgs e)
        {
            if (who == "Student")
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                String query = "update Student set Password = @password where Email = @email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@password", textBox_set_password.Text);
                cmd.Parameters.AddWithValue("@email", textBox_username.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (who == "SubAdmin")
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                String query = "update Department set Hod_Password = @password where Hod_Username = @email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@password", textBox_set_password.Text);
                cmd.Parameters.AddWithValue("@email", textBox_username.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (who == "Teacher")
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                String query = "update Teacher set Password = @password where Email = @email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@password", textBox_set_password.Text);
                cmd.Parameters.AddWithValue("@email", textBox_username.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert",
            "alert('Successfully Password changed.');window.location ='Login';",
            true);
        }

        protected void bt_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }
    }
}