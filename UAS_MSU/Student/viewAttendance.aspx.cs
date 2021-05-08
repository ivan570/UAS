using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace UAS_MSU.Student
{
	public partial class viewAttendance : System.Web.UI.Page
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Student.viewAttendance));

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["student"] == null)
				{
					Response.Redirect("~/Login");
				}
				ShowData();
			}
		}

		private void ShowData()
		{
			String DepartmentName = "";
			String queryfor = "select Department_id from Student, Class, Course where Student.Class_Id = Class.Class_Id " +
							" and Class.Course_Id = Course.Course_Id and Student.Email = '" + Session["student"].ToString() + "'";

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
			String query = "SELECT DISTINCT att.attendance_id, "
				+ "                att.date AS date , "
				+ "                att.duration AS time, "
				+ "                (CASE "
				+ "                     WHEN "
				+ "                            (SELECT Count(*) "
				+ "                             FROM " + tableName + " stuatt, Student whenStudent "
				+ "                             WHERE stuatt.attendance_id = att.attendance_id "
				+ "                             and whenStudent.Student_id = stuatt.Student_id  "
				+ "                             and whenStudent.Email='" + Session["student"].ToString() + "' "
				+ "                             ) > 0 THEN ('true') "
				+ "                     ELSE ('false') "
				+ "                 END) AS ispresent, "
				+ " "
				+ "  (SELECT sub.subject_name "
				+ "   FROM subject sub, "
				+ "                teacher_subject ts "
				+ "   WHERE att.teacher_subject_id = ts.teacher_subject_id "
				+ "     AND ts.subject_id = sub.subject_id) AS subject "
				+ "FROM attendance att, "
				+ "     teacher_subject tss, "
				+ "     subject sub, "
				+ "     CLASS, "
				+ "     course, "
				+ "     student stu "
				+ "WHERE att.teacher_subject_id = tss.teacher_subject_id "
				+ "  AND tss.subject_id = sub.subject_id "
				+ "  AND sub.class_id = CLASS.class_id "
				+ "  AND CLASS.course_id = course.course_id "
				+ "  AND course.department_id = "
				+ "    (SELECT co.department_id "
				+ "     FROM course co, "
				+ "          CLASS cl, "
				+ "                student stu "
				+ "     WHERE stu.class_id = cl.class_id "
				+ "       AND cl.course_id = co.course_id "
				+ "       AND stu.email = '" + Session["student"].ToString() + "')";

			log.Info("show data " + query);

			SqlCommand cmd = new SqlCommand(query, con);

			SqlDataAdapter adapt = new SqlDataAdapter(query, con);

			DataTable dt = new DataTable();
			adapt.Fill(dt);

			student_attendance.DataSource = dt;
			student_attendance.DataBind();

			con.Close();
		}

		protected void export_Click(object sender, EventArgs e)
		{
			String DepartmentName = "";
			String queryfor = "select Department_id from Student, Class, Course where Student.Class_Id = Class.Class_Id " +
							" and Class.Course_Id = Course.Course_Id and Student.Email = '" + Session["student"].ToString() + "'";

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
			log.Info("table name " + tableName);
			String query = "SELECT DISTINCT att.attendance_id, "
					+ "                att.date AS date , "
					+ "                att.duration AS time, "
					+ "                (CASE "
					+ "                     WHEN "
					+ "                            (SELECT Count(*) "
					+ "                             FROM " + tableName + " stuatt, Student whenStudent "
					+ "                             WHERE stuatt.attendance_id = att.attendance_id "
					+ "                             and whenStudent.Student_id = stuatt.Student_id  "
					+ "                             and whenStudent.Email='" + Session["student"].ToString() + "' "
					+ "                             ) > 0 THEN ('true') "
					+ "                     ELSE ('false') "
					+ "                 END) AS ispresent, "
					+ " "
					+ "  (SELECT sub.subject_name "
					+ "   FROM subject sub, "
					+ "                teacher_subject ts "
					+ "   WHERE att.teacher_subject_id = ts.teacher_subject_id "
					+ "     AND ts.subject_id = sub.subject_id) AS subject "
					+ "FROM attendance att, "
					+ "     teacher_subject tss, "
					+ "     subject sub, "
					+ "     CLASS, "
					+ "     course, "
					+ "     student stu "
					+ "WHERE att.teacher_subject_id = tss.teacher_subject_id "
					+ "  AND tss.subject_id = sub.subject_id "
					+ "  AND sub.class_id = CLASS.class_id "
					+ "  AND CLASS.course_id = course.course_id "
					+ "  AND course.department_id = "
					+ "    (SELECT co.department_id "
					+ "     FROM course co, "
					+ "          CLASS cl, "
					+ "                student stu "
					+ "     WHERE stu.class_id = cl.class_id "
					+ "       AND cl.course_id = co.course_id "
					+ "       AND stu.email = '" + Session["student"].ToString() + "')";

			log.Info("exports data " + query);

			Constant.CreateExcel(this, query, "reports.xlsx", "student");
		}
	}
}