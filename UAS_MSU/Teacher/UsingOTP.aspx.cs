using System;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using QRCoder;
using System.Drawing;
using System.IO;

namespace UAS_MSU.Teacher
{
	public partial class UsingOTP : System.Web.UI.Page, IPostBackEventHandler
	{

		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Teacher.UsingOTP));
		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				Control control = this.Master.FindControl("navbar");
				control.Visible = false;

				if (Session["subject_id"] == null || Session["class_id"] == null || Session["attendance_id"] == null)
				{
					Response.Redirect("~/Teacher/TakeAttendance");
				}
				if (Session["duration"] == null)
					Session["duration"] = "0100";
			}
		}
		protected void bt_otp_Click(object sender, EventArgs e)
		{
			timerLabel.InnerHtml = "30";
			div_otp.Visible = true;
			Random random = Constant.random;

			String number = random.Next(10, 99).ToString() + Session["attendance_id"].ToString();
			Session["number"] = number;
			st_otp.InnerText = number;

			String query = "insert into OTPt(OTP, Date, class_id, Attendance_id, duration, validity_in_sec) values " +
				"('" + number + "', SYSDATETIMEOFFSET(), '" + Session["class_id"].ToString() + "', '" +
				Session["attendance_id"].ToString() + "', '" + Session["duration"].ToString() + "', 30);";

			log.Info("query in bt_otp_click " + query);

			try
			{
				if (con.State == ConnectionState.Closed)
					con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.ExecuteNonQuery();
				if (con.State == ConnectionState.Open)
					con.Close();

			}
			catch (Exception ex)
			{
				log.Info("Exception in otp_click " + ex);
			}
		}

		public void RaisePostBackEvent(string eventArgument)
		{
			timerLabel.InnerHtml = "0";
			div_otp.Visible = false;

			String query = "delete from otpt where otp = '" + Session["number"] + "'";
			log.Info("RaisePostBackEvent query => " + query);

			if (con.State == ConnectionState.Closed)
				con.Open();
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			if (con.State == ConnectionState.Open)
				con.Close();
			Session["number"] = null;

			Response.Redirect("~/Teacher/TakeAttendance");
		}

		protected void BACK_BTN_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Teacher/TakeAttendance");
		}

		protected void bt_qr_click(object sender, EventArgs e)
		{
			string code = Constant.RandomString(3) + Session["attendance_id"].ToString();

			String query = "insert into OTPt(OTP, Date, class_id, Attendance_id, duration, validity_in_sec) values " +
				"('" + code + "', SYSDATETIMEOFFSET(), '" + Session["class_id"].ToString() + "', '" +
				Session["attendance_id"].ToString() + "', '" + Session["duration"].ToString() + "', null);";

			log.Info("query in bt_qr_click " + query);

			try
			{
				if (con.State == ConnectionState.Closed)
					con.Open();
				SqlCommand cmd = new SqlCommand(query, con);
				cmd.ExecuteNonQuery();
				if (con.State == ConnectionState.Open)
					con.Close();
			}
			catch (Exception ex)
			{
				log.Info("Exception in qr_click " + ex);
			}

			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
			System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
			imgBarCode.Height = 550;
			imgBarCode.Width = 550;

			using (Bitmap bitMap = qrCode.GetGraphic(20))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
					byte[] byteImage = ms.ToArray();
					imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
				}
				PlaceHolder_Qr.Controls.Add(imgBarCode);
			}
		}

	}
}