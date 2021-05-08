using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_add_Click(object sender, EventArgs e)
        {
            String name = textBox_name.Text;
            String email = textBox_email.Text;
            String details = textBox_area.Text;
            emailID(email, name, details);
            textBox_name.Text = "";
            textBox_email.Text = "";
            textBox_area.Text = "";
        }
        protected void alert(String message)
        {
            string script = String.Format("alert('{0}');", message);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
        }
        protected void emailID(String email, String name, String details)
        {
            String from = "harshkachhadiya55@gmail.com";
            String to = "ivankshu@gmail.com";
            MailMessage message = new MailMessage(from, to);

            string mailbody = details;
            message.Subject = "Query from name:: " + name.Trim() + " and email:: " + email;
            message.Body = mailbody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("harshkachhadiya55@gmail.com", "Harsh@134");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
                alert("Mail sent");
            }
            catch (Exception) { }
        }
    }
}