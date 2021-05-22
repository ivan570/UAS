using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UAS_MSU.Admin
{
	public partial class Reports : System.Web.UI.Page
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Student.viewAttendance));

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["admin"] == null)
				{
					Response.Redirect("~/Login");
				}
			}
		}

		private string getQuery(String prn)
		{
			if (prn == null)
				return "";

			String DepartmentName = "";
			String queryfor = "select course.department_id from student stu, class, course where " +
				" stu.class_id = class.class_id and class.Course_Id = Course.Course_Id and stu.Prn='" + prn + "'";

			if (con.State == System.Data.ConnectionState.Closed)
				con.Open();

			SqlCommand cmd1 = new SqlCommand(queryfor, con);
			using (SqlDataReader sqlReader = cmd1.ExecuteReader())
			{
				if (sqlReader.Read())
					DepartmentName += sqlReader.GetValue(0).ToString();
				else
					return null;
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
				+ "                             FROM " + tableName + " stuatt, "
				+ "                                                          Student whenStudent "
				+ "                             WHERE stuatt.attendance_id = att.attendance_id "
				+ "                               AND whenStudent.Student_id = stuatt.Student_id "
				+ "                               AND whenStudent.Prn='" + prn + "') > 0 THEN ('true') "
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
				+ "       AND stu.Prn = '" + prn + "')";

			return query;
		}


		protected void check_Click(object sender, EventArgs e)
		{
			String prn = textBox_prn.Text;
			if (prn == null)
				prn = "";

			String query = getQuery(prn);

			log.Info("show data " + query);

			if (query == null)
				return;

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
			String prn = textBox_prn.Text;
			if (prn == null)
				prn = "";

			String query = getQuery(prn);
			log.Info("exports data " + query);

			if (query == null)
				return;

			Constant.CreateExcel(this, query, "reports.xlsx", "student");
		}

	}
}