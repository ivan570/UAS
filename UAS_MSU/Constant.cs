using log4net;
using System;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.Mail;
using System.Net.Sockets;

namespace UAS_MSU
{
	public class Constant
	{
		public static Random random = new Random();
		public static void alert(System.Web.UI.Page page, String message)
		{
			string script = String.Format("alert('{0}');", message);
			page.Page.ClientScript.RegisterStartupScript(page.Page.GetType(), "msgbox", script, true);
		}

		public static int sendEmail(System.Web.UI.Page page, String to, String subject, String body, String successMessage, String errorMessage)
		{

			string from = "harshkachhadiya55@gmail.com";

			MailMessage message = new MailMessage(from, to);
			message.Subject = subject;
			message.Body = body;
			message.BodyEncoding = System.Text.Encoding.UTF8;
			message.IsBodyHtml = true;

			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("harshkachhadiya55@gmail.com", "Harsh@1213");


			client.EnableSsl = true;
			client.UseDefaultCredentials = false;
			client.Credentials = basicCredential1;

			try
			{
				client.Send(message);
				if (successMessage.Length > 0)
				{
					alert(page, successMessage);
				}
				return 1;
			}
			catch (Exception ex)
			{
				alert(page, errorMessage + " : " + ex.Message);
				return 0;
			}
		}

		public static ILog GetLog(System.Type type)
		{
			return log4net.LogManager.GetLogger(type);
		}

		public static void alertWithRedirect(System.Web.UI.Page page, String message, String redirect)
		{
			ScriptManager.RegisterStartupScript(page, page.GetType(), "alert",
							"alert('" + message + "');window.location ='" + redirect + "';",
							true);
		}

		public static String RandomString(int length = 8)
		{
			StringBuilder str_build = new StringBuilder();
			Random random = new Random();

			char letter;

			for (int i = 0; i < length; i++)
			{
				double flt = random.NextDouble();
				int shift = Convert.ToInt32(Math.Floor(25 * flt));
				letter = Convert.ToChar(shift + 65);
				str_build.Append(letter);
			}

			return str_build.ToString();
		}

		public static void CreateExcel(System.Web.UI.Page page, String query, String fileName, String excelSheetName)
		{
			string constr = ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString;
			using (SqlConnection con = new SqlConnection(constr))
			{
				using (SqlCommand cmd = new SqlCommand(query))
				{
					using (SqlDataAdapter sda = new SqlDataAdapter())
					{
						cmd.Connection = con;
						sda.SelectCommand = cmd;
						using (DataTable dt = new DataTable())
						{
							sda.Fill(dt);
							using (XLWorkbook excelWorkBook = new XLWorkbook())
							{
								excelWorkBook.Worksheets.Add(dt, excelSheetName);

								page.Response.Clear();
								page.Response.Buffer = true;
								page.Response.Charset = "";
								page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
								page.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "");
								using (MemoryStream MyMemoryStream = new MemoryStream())
								{
									excelWorkBook.SaveAs(MyMemoryStream);
									MyMemoryStream.WriteTo(page.Response.OutputStream);
									page.Response.Flush();
									page.Response.End();
								}
							}
						}
					}
				}
			}

		}

	}
}