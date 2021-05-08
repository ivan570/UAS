using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU.Teacher
{
	public partial class TakeAttendance : System.Web.UI.Page
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		SqlDataAdapter adapt;
		DataTable dt;
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Teacher.TakeAttendance));

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				current_date.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
				if (Session["teacher"] == null)
				{
					Response.Redirect("~/Login");
				}
				changeCourse();
				ShowData();
			}
		}
		protected void ShowData()
		{
			dt = new DataTable();

			con.Open();
			String tableName = "StudentAttendance_";
			SqlCommand cmd1 = new SqlCommand("select Department_Id " +
				"from Course,Class where Course.Course_Id=Class.Course_Id " +
				"and Class.Class_Id='" + selectDropDownListClass.SelectedValue + "'", con);
			using (SqlDataReader sqlReader = cmd1.ExecuteReader())
			{
				try
				{
					sqlReader.Read();
					tableName += sqlReader.GetValue(0).ToString();
				}
				catch (Exception ex)
				{
					log.Error("Error while getting data");
					log.Error(ex.Message);
					log.Error(ex);
				}
			}

			log.Info("StudentAttendance table " + tableName);

			String query = "SELECT attendance_id,  "
					+ "       attendance.date,  "
					+ "       attendance.Duration,  "
					+ "       (SELECT Count(*)  "
					+ "        FROM  " + tableName + " AS sta,  "
					+ "               attendance AS att,  "
					+ "               teacher_subject ts,  "
					+ "               teacher  "
					+ "        WHERE  sta.attendance_id = att.attendance_id  "
					+ "               AND att.teacher_subject_id = ts.teacher_subject_id  "
					+ "               AND ts.teacher_id = teacher.teacher_id  "
					+ "               AND teacher.email = '" + Session["teacher"].ToString() + "'  "
					+ "               AND ts.subject_id = '" + selectDropDownListSubject.SelectedValue.ToString() + "'  "
					+ "               AND att.attendance_id = attendance.attendance_id) AS Present,  "
					+ "       ( CASE  "
					+ "           WHEN (SELECT sub2.elective_type  "
					+ "                 FROM   subject sub2  "
					+ "                 WHERE  sub2.subject_id = '" + selectDropDownListSubject.SelectedValue.ToString() + "') = 'true' THEN (SELECT Count(*)  "
					+ "                                                             FROM  "
					+ "           student_subject  "
					+ "                                                             WHERE  "
					+ "           student_subject.subject_id = '" + selectDropDownListSubject.SelectedValue.ToString() + "' and STUDENT_SUBJECT.Student_Id is not null  )  "
					+ "           ELSE (SELECT Count(*)  "
					+ "                 FROM   student  "
					+ "                 WHERE  "
					+ "           student.class_id = '" + selectDropDownListClass.SelectedValue.ToString() + "')  "
					+ "         END )                                                  AS Total  "
					+ "FROM   attendance,  " + "       teacher,  "
					+ "       teacher_subject  " + "WHERE  "
					+ "teacher_subject.subject_id = '" + selectDropDownListSubject.SelectedValue.ToString() + "'  "
					+ "AND attendance.teacher_subject_id = teacher_subject.teacher_subject_id  "
					+ "AND teacher.email = '" + Session["teacher"].ToString() + "'  "
					+ "AND teacher.teacher_id = teacher_subject.teacher_id  "
					+ "ORDER  BY Attendance.date DESC   ";



			log.Info("ShowData query tack attendance " + query);
			adapt = new SqlDataAdapter(query, con);
			try
			{
				adapt.Fill(dt);
			}
			catch (Exception ex)
			{
				log.Info("error while lll ");
				log.Error(ex);
			}
			if (dt.Rows.Count > 0)
			{
				attendanceGrid.DataSource = dt;
				attendanceGrid.DataBind();

				ViewState["dirState"] = dt;
				ViewState["sortdr"] = "Asc";
			}
			else
			{
				attendanceGrid.DataSource = null;
				attendanceGrid.DataBind();
			}
			con.Close();
		}


		protected void changeCourse()
		{
			if (con.State == ConnectionState.Closed)
				con.Open();
			String query = "SELECT DISTINCT Course.Course_Id, Course.Course_Name FROM Teacher ,Teacher_Subject, Subject, Class, Course WHERE Teacher.Teacher_Id = Teacher_Subject.Teacher_Id AND Subject.Subject_Id = Teacher_Subject.Subject_Id AND Class.Class_Id = Subject.Class_Id AND Course.Course_Id = Class.Course_Id AND Teacher.Email = '" + Session["teacher"] + "' order by Course_Name;";

			log.Info(query + "changeCourse");

			SqlCommand com = new SqlCommand(query, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds);
			selectDropDownListCourse.DataTextField = ds.Tables[0].Columns["Course_Name"].ToString();
			selectDropDownListCourse.DataValueField = ds.Tables[0].Columns["Course_Id"].ToString();

			selectDropDownListCourse.DataSource = ds.Tables[0];
			selectDropDownListCourse.DataBind();

			con.Close();
			changeClass();

		}
		protected void changeClass()
		{
			if (con.State == ConnectionState.Closed)
				con.Open();
			String query = "SELECT DISTINCT Class.Class_Id,Year,Semister, CONCAT(Semister, ' ', Year) as Year_Semester FROM Teacher ,Teacher_Subject,Subject, Class, Course WHERE Teacher.Teacher_Id = Teacher_Subject.Teacher_Id AND Subject.Subject_Id = Teacher_Subject.Subject_Id AND Class.Class_Id = Subject.Class_Id AND Class.Course_Id = Course.Course_Id AND Course.Course_Id = '" + selectDropDownListCourse.SelectedValue + "' AND Teacher.Email = '" + Session["teacher"] + "' order by Year,Semister;";

			log.Info(query + "changeClass");

			SqlCommand com = new SqlCommand(query, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds);
			selectDropDownListClass.DataTextField = ds.Tables[0].Columns["Year_Semester"].ToString();
			selectDropDownListClass.DataValueField = ds.Tables[0].Columns["Class_Id"].ToString();

			selectDropDownListClass.DataSource = ds.Tables[0];
			selectDropDownListClass.DataBind();

			con.Close();
			changeSubject();
		}

		protected void bt_take_attendance_Click(object sender, EventArgs e)
		{
			Session["subject_id"] = selectDropDownListSubject.SelectedValue;
			Session["class_id"] = selectDropDownListClass.SelectedValue;

			if (con.State == ConnectionState.Closed)
				con.Open();

			String time = duration_hour.Value + ":" + duration_min.Value;

			log.Info("Time insert " + time);

			String queryForInsert = "insert into Attendance(Teacher_Subject_Id, Date, Duration) output inserted.Attendance_Id values  "
				+ "((select Teacher_Subject_id from Teacher_Subject  "
				+ "where Teacher_Id = (select Teacher_Id from Teacher where Email='" + Session["teacher"].ToString() + "') "
				+ "and Subject_Id = '" + selectDropDownListSubject.SelectedValue.ToString() + "'), SYSDATETIMEOFFSET(), "
				+ "CONVERT( TIME, '" + time.ToString() + "'));";

			log.Info("queryForInsert in Attendance table " + queryForInsert);
			SqlCommand cmd = new SqlCommand(queryForInsert, con);
			String attendanceID = "";
			try
			{
				attendanceID = cmd.ExecuteScalar().ToString();
			}
			catch (Exception ex)
			{
				log.Error("Exception while inserting Attendance ");
				log.Error(ex.Message);
				log.Error(ex);
			}

			Session["attendance_id"] = attendanceID;
			log.Info("inserted attendanceID " + attendanceID);

			if (con.State == ConnectionState.Open)
				con.Close();

			if (rad_manualy.Checked == true)
			{
				Session["status"] = "manualy";
				Response.Redirect("~/Teacher/ManualAttendance");
			}
			else if (rad_usingOTP.Checked == true)
			{
				Session["status"] = "OTP";
				Response.Redirect("~/Teacher/UsingOTP");
			}
		}
		protected void selectDropDownListCourse_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeClass();
		}
		protected void selectDropDownListSubject_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowData();
		}
		protected void selectDropDownListClass_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeSubject();
		}
		protected void changeSubject()
		{
			if (con.State == ConnectionState.Closed)
				con.Open();
			String query = "SELECT Subject.Subject_Id, Subject.Subject_Name FROM Teacher ,Teacher_Subject,Subject, Class WHERE Teacher.Teacher_Id = Teacher_Subject.Teacher_Id AND Subject.Subject_Id = Teacher_Subject.Subject_Id AND Class.Class_Id = Subject.Class_Id AND Class.Class_Id = '" + selectDropDownListClass.SelectedValue + "' AND Teacher.Email = '" + Session["teacher"] + "' order by Subject_Name;";

			log.Info(query + "changeSubject");

			SqlCommand com = new SqlCommand(query, con);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds);
			selectDropDownListSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString();
			selectDropDownListSubject.DataValueField = ds.Tables[0].Columns["Subject_Id"].ToString();

			selectDropDownListSubject.DataSource = ds.Tables[0];
			selectDropDownListSubject.DataBind();

			con.Close();
			ShowData();
		}

		protected void attendanceGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)attendanceGrid.Rows[e.RowIndex];
			Label lbldeleteid = (Label)row.FindControl("Attendance_Id");
			con.Open();
			String query = "delete from Attendance where Attendance_Id='" + lbldeleteid.Text + "';";
			Response.Write("<script> console.log(\"Delete query =>" + (query) + "\") </script> ");
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			con.Close();
			ShowData();
		}
		protected void OnEditClick(Object sender, CommandEventArgs e)
		{

			Session["attendance_id"] = e.CommandArgument.ToString();
			Session["subject_id"] = selectDropDownListSubject.SelectedValue;
			Session["class_id"] = selectDropDownListClass.SelectedValue;
			Session["status"] = "Edit";

			log.Info("Session[\"attendance_id\"] " + Session["attendance_id"]);

			log.Info("Session[\"subject_id\"] " + Session["subject_id"]);
			log.Info("Session[\"class_id\"] " + Session["class_id"]);
			Response.Redirect("~/Teacher/ManualAttendance");

		}
		protected void attendanceGrid_Sorting(object sender, GridViewSortEventArgs e)
		{
			log.Info("before sorting");
			DataTable dtrslt = (DataTable)ViewState["dirState"];
			if (dtrslt.Rows.Count > 0)
			{
				if (Convert.ToString(ViewState["sortdr"]) == "Asc")
				{
					dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
					ViewState["sortdr"] = "Desc";
				}
				else
				{
					dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
					ViewState["sortdr"] = "Asc";
				}
				attendanceGrid.DataSource = dtrslt;
				attendanceGrid.DataBind();
			}

			log.Info("after sorting");

		}
		protected void OnDownloadClick(object sender, CommandEventArgs e)
		{
			String attendance_id = e.CommandArgument.ToString();
			if (con.State == ConnectionState.Closed)
				con.Open();

			String tableName = "StudentAttendance_";
			SqlCommand cmd1 = new SqlCommand("select Department_Id " +
				"from Course,Class where Course.Course_Id=Class.Course_Id " +
				"and Class.Class_Id='" + selectDropDownListClass.SelectedValue + "'", con);
			using (SqlDataReader sqlReader = cmd1.ExecuteReader())
			{
				try
				{
					sqlReader.Read();
					tableName += sqlReader.GetValue(0).ToString();
				}
				catch (Exception ex)
				{
					log.Error("Error while getting data");
					log.Error(ex.Message);
					log.Error(ex);
				}
			}
			if (con.State == ConnectionState.Open)
				con.Close();

			String query = "select att.Date, att.Duration, Subject.Subject_Name, Student.Student_Name, "
				+ "Student.Email, student.Prn from " + tableName + " sta, Attendance att, "
				+ "Teacher_Subject ts, Subject, Student "
				+ "where sta.Attendance_Id = att.Attendance_Id and att.Teacher_Subject_Id = "
				+ "ts.Teacher_Subject_Id and "
				+ "sta.Student_Id = Student.Student_Id and ts.Subject_Id = Subject.Subject_Id "
				+ "and att.Attendance_Id = '" + attendance_id + "'"; ;

			log.Info("Query for downloading excel for attendance_id " + attendance_id + ":: " + query);

			String excelSheetName = "attendance_" + attendance_id;
			String fileName = "reports.xlsx";
			Constant.CreateExcel(this, query, fileName, excelSheetName);
		}
	}
}