using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UAS_MSU.Teacher
{
	public partial class ManualAttendance : System.Web.UI.Page
	{
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		SqlDataAdapter adapt;
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Teacher.ManualAttendance));
		DataTable dt;
		public bool IsTrue(object value)
		{
			log.Info("value in " + value);
			bool bools = value.Equals("true");
			log.Info("value of bools " + bools);
			return (bools);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				Control control = this.Master.FindControl("navbar");// "masterDiv"= the Id of the div.
				control.Visible = false;//to set the div to be hidden.
				if (Session["subject_id"] == null || Session["class_id"] == null || Session["attendance_id"] == null)
				{
					Response.Redirect("~/Teacher/TakeAttendance");
				}
				log.Info("hi : " + Session["subject_id"] + ", : , " + Session["class_id"] + ", : , " + Session["attendance_id"]);

				if (Session["status"] != null && Session["status"].ToString().ToUpper().Equals("EDIT"))
				{
					showDataForEdit();
				}
				else
				{
					ShowData();
				}
			}
		}

		protected void showDataForEdit()
		{
			dt = new DataTable();
			con.Open();

			String query = "select Elective_Type from Subject where Subject_Id='" + Session["subject_id"] + "'";
			String fquery = "";
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			String temp;
			using (SqlDataReader sqlReader = cmd.ExecuteReader())
			{
				sqlReader.Read();
				temp = (sqlReader.GetValue(0).ToString());
			}
			log.Info("temp value after checking Elective type " + temp);
			String tableName = "StudentAttendance_";
			SqlCommand cmd1 = new SqlCommand("select Department_Id " +
				"from Course,Class where Course.Course_Id=Class.Course_Id " +
				"and Class.Class_Id='" + Session["class_id"] + "'", con);
			using (SqlDataReader sqlReader = cmd1.ExecuteReader())
			{
				sqlReader.Read();
				tableName += sqlReader.GetValue(0).ToString();
			}
			if (temp.Equals("False"))
			{
				fquery += "select distinct Prn,Student.Student_Id,Student_Name, ( "
				+ "	case  "
				+ "	when (select Count(*) from " + tableName + " std where std.Student_Id = Student.Student_Id and std.Attendance_Id='"+ Session["attendance_id"] + "') > 0 "
				+ "	then ('true') "
				+ "	else "
				+ "	('false') "
				+ "	end "
				+ ") isPresent from Student where CLass_Id='" + Session["class_id"].ToString()
				+ "'order by Student_Name ";
			}
			else
			{
				fquery += "select distinct Prn,Student.Student_Id,Student_Name , ( "
				+ "	case  "
				+ "	when (select Count(*) from " + tableName + " std where " +
				"std.Student_Id = Student.Student_Id and std.Attendance_Id='" + Session["attendance_id"] + "') > 0  "
				+ "	then ('true') "
				+ "	else "
				+ "	('false') "
				+ "	end "
				+ ") isPresent from Student, Student_Subject where Subject_Id = '" + Session["subject_id"].ToString() + "' "
				+ "and Student.Student_Id = Student_Subject.Student_Id  "
				+ "order by Student_Name;";
			}

			log.Info("fquery after if-else " + fquery);

			adapt = new SqlDataAdapter(fquery, con);
			adapt.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				takeAttendanceGrid.DataSource = dt;
				takeAttendanceGrid.DataBind();
			}
			else
			{
				takeAttendanceGrid.DataSource = null;
				takeAttendanceGrid.DataBind();
			}
			con.Close();
			//Session["status"] = "conpleteEdit";
		}
		protected void ShowData()
		{
			dt = new DataTable();
			con.Open();

			String query = "select Elective_Type from Subject where Subject_Id='" + Session["subject_id"] + "'";
			String fquery = "";
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			String temp;
			using (SqlDataReader sqlReader = cmd.ExecuteReader())
			{
				sqlReader.Read();
				temp = (sqlReader.GetValue(0).ToString());
			}
			log.Info("temp value after checking Elective type " + temp);
			if (temp.Equals("False"))
			{
				fquery += "select distinct Student_Id, Student_Name, Prn, 'false' isPresent " +
					"from Student where Class_Id='" + Session["class_id"] + "' order by Student_Name";
			}
			else
			{
				fquery += "select distinct Prn,Student.Student_Id,Student_Name, 'false' as isPresent from Student, Student_Subject where Subject_Id = '" + Session["subject_id"] + "' and Student.Student_Id = Student_Subject.Student_Id order by Student_Name";
			}

			log.Info("fquery after if-else " + fquery);

			adapt = new SqlDataAdapter(fquery, con);
			adapt.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				takeAttendanceGrid.DataSource = dt;
				takeAttendanceGrid.DataBind();
			}
			else
			{
				takeAttendanceGrid.DataSource = null;
				takeAttendanceGrid.DataBind();
			}
			con.Close();
		}

		protected void bt_take_attendance_Click(object sender, EventArgs e)
		{
			if (con.State == ConnectionState.Closed)
				con.Open();
			String tableName = "StudentAttendance_";
			SqlCommand cmd = new SqlCommand("select Department_Id " +
				"from Course,Class where Course.Course_Id=Class.Course_Id " +
				"and Class.Class_Id='" + Session["class_id"] + "'", con);
			using (SqlDataReader sqlReader = cmd.ExecuteReader())
			{
				sqlReader.Read();
				tableName += sqlReader.GetValue(0).ToString();
			}
			
			if (Session["status"].ToString().Equals("Edit"))
			{
				
				String q = "delete from "+tableName+" where Attendance_Id='"+ Session["attendance_id"].ToString() + "'";
				SqlCommand cmd2= new SqlCommand(q, con);
				cmd2.ExecuteNonQuery();
			}
			con.Close();

			foreach (GridViewRow gvrow in takeAttendanceGrid.Rows)
			{

				var checkbox = gvrow.FindControl("CheckBox_Present") as CheckBox;
				if (checkbox.Checked)
				{
					var lblPrn = gvrow.FindControl("lbl_Prn") as Label;
					var lblName = gvrow.FindControl("lbl_Name") as Label;
					var lblId = gvrow.FindControl("lbl_id") as Label;
					if (con.State == ConnectionState.Closed)
						con.Open();

					String query = "insert into " + tableName + "(Attendance_Id,Student_Id) values ('" + Session["attendance_id"] + "','" + lblId.Text + "')";
					log.Info("insert query for teacher " + query);
					SqlCommand cmd1 = new SqlCommand(query, con);
					cmd1.ExecuteNonQuery();
					con.Close();
					//consolePrint(lblID.Text, lblName.Text); 


				}
			}
			if (Session["status"].ToString() == "Edit"){
				Session["status"] = "conpleteEdit";
				ScriptManager.RegisterStartupScript(this, this.GetType(),
				"alert",
				"alert('Attendance updated successfully.');window.location ='TakeAttendance';",
			true);
			}
			else
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(),
				"alert",
				"alert('Attendance taken successfully.');window.location ='TakeAttendance';",
			true);
			}
			
			

		}
		protected void consolePrint(String query, String message = "")
		{
			Response.Write("<script> console.log(\" Printing : " + message + " " + query + " \") </script> ");
		}

		protected void bt_reset_Click(object sender, EventArgs e)
		{

			Response.Redirect("~/Teacher/TakeAttendance");
		}


	}
}