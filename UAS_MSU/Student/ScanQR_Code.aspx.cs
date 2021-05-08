using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU.Student
{
	public partial class ScanQR_Code : System.Web.UI.Page, IPostBackEventHandler
	{
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Student.ScanQR_Code));
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["student"] == null)
					Response.Redirect("~/login");
			}
		}
		public void RaisePostBackEvent(string eventArgument)
		{
			log.Info("RaisePostBackEvent " + eventArgument);
			String DepartmentName = "";
			String queryfor = "select Department_id from Student, Class, Course where Student.Class_Id = Class.Class_Id " +
							" and Class.Course_Id = Course.Course_Id and Student.Email = '"+ Session["student"].ToString() + "'";

			if (con.State == System.Data.ConnectionState.Closed)
				con.Open();

			SqlCommand cmd1 = new SqlCommand(queryfor, con);
			using (SqlDataReader sqlReader = cmd1.ExecuteReader())
			{
				sqlReader.Read();
				DepartmentName += sqlReader.GetValue(0).ToString();
			}

			if (con.State == System.Data.ConnectionState.Open)
				con.Close();

			String tableName = "StudentAttendance_" + DepartmentName;

			log.Info("queryfor department " + queryfor + " department id " + DepartmentName);

			String query = "if EXISTS (select Attendance_id from OTPt where OTP = '" + eventArgument + "'"
							+ "	and(CONVERT(Date, date)) = (Convert(date, getdate())))"
							+ "	begin"
							+ "	insert into " + tableName + "(Attendance_Id, Student_Id) "
							+ " output '1' as status"
							+ "	values((select Attendance_id from OTPt where OTP = '" + eventArgument + "'), "
							+ "	(select Student_id from Student where"
							+ "	email = '" + Session["student"].ToString() + "')) "
							+ "	end"
							+ "	else"
							+ "	begin"
							+ "	select '2' as status"
							+ "	end";

			log.Info("query in bt_attendace_Click " + query);

			if (con.State == System.Data.ConnectionState.Closed)
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
				Constant.alertWithRedirect(this, "May be you already mark your attendance", "Home");
			}

			if (status.Trim().Equals("1"))
			{
				Constant.alertWithRedirect(this, "Your Attendance is Marked", "Home");
			}
			else if (status.Trim().Equals("2"))
			{
				Constant.alertWithRedirect(this, "QR Code Expired", "Home");
			}

			if (con.State == System.Data.ConnectionState.Open)
				con.Close();

		}
	}
}