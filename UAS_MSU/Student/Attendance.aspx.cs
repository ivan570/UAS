using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU.Student
{
	public partial class Attendance : System.Web.UI.Page
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);

		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Teacher.ManualAttendance));

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["student"] == null)
			{
				Response.Redirect("~/Login");
			}
		}

		protected void bt_attendace_Click(object sender, EventArgs e)
		{
			String tableName = "StudentAttendance_";
			String otp_txt = textBox_otp.Text.Trim();

			if (con.State == ConnectionState.Closed)
				con.Open();

			String queryForGettingDepartmentID = "select Course.Department_Id from"
				+ " Course, Class, Student st where  "
				+ " st.Class_Id = Class.Class_Id and Class.Course_Id = Course.Course_Id  "
				+ " and st.Email = '" + Session["student"].ToString() + "' ";

			SqlCommand cmdDepartmentID = new SqlCommand(queryForGettingDepartmentID, con);
			String DepartmentID = "";
			try
			{
				DepartmentID = cmdDepartmentID.ExecuteScalar().ToString();
			}
			catch (Exception ex)
			{
				log.Error("Exception while Attendance ");
				log.Error(ex.Message);
				log.Error(ex);
			}
			tableName += DepartmentID;
			
			log.Fatal("table name in attendance " + tableName);
			
			if (con.State == ConnectionState.Open)
				con.Close();

			String query = "if  EXISTS (select Attendance_id from OTPt where OTP = '" + otp_txt + "'  "
				+ "and DATEDIFF(MILLISECOND, Date, SYSDATETIME()) < (select validity_in_sec * 1000 from OTPt where OTP='" + otp_txt + "')) "
				+ "begin "
				+ "insert into " + tableName + " (Attendance_Id, Student_Id) output '1' as status "
				+ "values ((select Attendance_id from OTPt where OTP = '" + otp_txt + "'), (select Student_id from Student where "
				+ "email='" + Session["student"].ToString() + "')) "
				+ "end "
				+ "else  "
				+ "begin "
				+ "select '2' as status "
				+ "end ";

			log.Info("query in bt_attendace_Click " + query);

			if (con.State == ConnectionState.Closed)
				con.Open();

			SqlCommand cmd = new SqlCommand(query, con);
			String status = "";
			try
			{
				status = cmd.ExecuteScalar().ToString();
			}
			catch (Exception ex)
			{
				log.Info("Exception while Attendance " + ex);
				Constant.alert(this, "May be you already mark your attendance");
			}

			if (con.State == ConnectionState.Open)
				con.Close();

			if (status.Trim().Equals("1"))
			{
				Constant.alert(this, "Your Attendance is marked");
			}
			else if (status.Trim().Equals("2"))
			{
				Constant.alert(this, "OTP is expired");
			}
			textBox_otp.Text = "";
		}

		protected void scan_qr_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Student/ScanQR_Code");
		}
	}
}