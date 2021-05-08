using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace UAS_MSU.Admin
{
	public partial class Department : System.Web.UI.Page
	{

		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		SqlDataAdapter adapt;
		DataTable dt;
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Admin.Department));

		protected void alert(String message)
		{
			string script = String.Format("alert('{0}');", message);
			this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["admin"] == null)
				{
					Response.Redirect("~/Login");
				}
				if (con.State == ConnectionState.Closed)
					con.Open();
				SqlCommand com = new SqlCommand("select * from faculty order by Faculty_Name", con);
				SqlDataAdapter da = new SqlDataAdapter(com);
				DataSet ds = new DataSet();
				da.Fill(ds);
				selectDropDownList.DataTextField = ds.Tables[0].Columns["faculty_name"].ToString();
				selectDropDownList.DataValueField = ds.Tables[0].Columns["faculty_id"].ToString();

				selectDropDownList.DataSource = ds.Tables[0];
				selectDropDownList.DataBind();

				con.Close();
				ShowData();
			}
		}
		protected void ShowData()
		{
			dt = new DataTable();
			con.Open();
			String query = "Select Department_Id, Department_Name, Hod_Name, Hod_Username, Faculty_Name" +
				" from Department as dt, Faculty as ft where dt.Faculty_Id = ft.Faculty_Id and dt.Faculty_Id='" + selectDropDownList.SelectedValue + "' order by Department_Name";

			log.Info("query for showdata for department " + query);

			adapt = new SqlDataAdapter(query, con);
			adapt.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				departmentGrid.DataSource = dt;
				departmentGrid.DataBind();
				ViewState["dirState"] = dt;
				ViewState["sortdr"] = "Asc";
			}
			else
			{
				departmentGrid.DataSource = null;
				departmentGrid.DataBind();
			}
			con.Close();
		}
		protected void departmentGrid_Sorting(object sender, GridViewSortEventArgs e)
		{
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
				departmentGrid.DataSource = dtrslt;
				departmentGrid.DataBind();

			}

		}
		protected void departmentGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)departmentGrid.Rows[e.RowIndex];
			Label lbldeleteid = (Label)row.FindControl("Department_Id");
			con.Open();
			String query = "delete from Department where Department_Id='" + lbldeleteid.Text + "';";

			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			con.Close();
			ShowData();
		}
		protected void departmentGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
		{
			//NewEditIndex property used to determine the index of the row being edited.  
			departmentGrid.EditIndex = e.NewEditIndex;
			ShowData();
		}
		protected void departmentGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
		{
			//Finding the controls from Gridview for the row which is going to update  
			//int userid = (int)(departmentGrid.DataKeys[e.RowIndex].Value);
			Label id = departmentGrid.Rows[e.RowIndex].FindControl("Department_id") as Label;
			TextBox name = departmentGrid.Rows[e.RowIndex].FindControl("Department_Name") as TextBox;
			TextBox hod_name = departmentGrid.Rows[e.RowIndex].FindControl("Hod_Name") as TextBox;
			TextBox hod_username = departmentGrid.Rows[e.RowIndex].FindControl("Hod_Username") as TextBox;

			String curr = id.Text;

			//*
			con.Open();
			String qtr = "select count(*) from Department where Hod_Username ='" + hod_username.Text + "'";
			SqlCommand scmd = new SqlCommand(qtr, con);
			scmd.ExecuteNonQuery();
			int temp = Convert.ToInt32(scmd.ExecuteScalar().ToString());
			if (temp > 0)
			{
				alert("Email id already registered.");
				con.Close();
				return;
			}
			con.Close();




			con.Open();

			String query = "Update Department set Department_Name = @name, Hod_Name = @hod_name, " +
				" Hod_Username = @hod_username" +
				", Faculty_id = @faculty_id where Department_id = @id";

			SqlCommand cmd = new SqlCommand(query, con);
			cmd.Parameters.AddWithValue("@name", name.Text);
			cmd.Parameters.AddWithValue("@hod_name", hod_name.Text);
			cmd.Parameters.AddWithValue("@hod_username", hod_username.Text);
			cmd.Parameters.AddWithValue("@faculty_id", selectDropDownList.SelectedValue);
			cmd.Parameters.AddWithValue("@id", id.Text);
			cmd.ExecuteNonQuery();

			con.Close();
			String to = hod_username.Text;
			String subject = "Upadate information of Department on UAS_MSU";
			String body = "Your username is " + hod_username.Text + " and Department name is " + name.Text;
			String successMessage = "Mail";
			String errorMessage = "Invalid email id.";
			int temp1 = Constant.sendEmail(this, to, subject, body, successMessage, errorMessage);

			departmentGrid.EditIndex = -1;

			ShowData();
		}
		protected void departmentGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
		{
			departmentGrid.EditIndex = -1;
			ShowData();
		}

		protected void selectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			log.Info("select change in department ");
			ShowData();
		}
		protected void bt_add_Click(object sender, EventArgs e)
		{
			if (con.State == ConnectionState.Closed)
				con.Open();
			String departmentName = textBox_department_name.Text;
			String hodName = textBox_department_hod.Text;
			String username = textBox_hod_username.Text;
			String password = encryption(textBox_hod_password.Text);
			String facultyId = selectDropDownList.SelectedValue;

			bool data = false;
			try
			{
				String qtr = "select count(*) from Department where lower(Department_Name)='" + departmentName.ToLower() + "' and Faculty_Id='" + selectDropDownList.SelectedValue + "'";
				SqlCommand scmd = new SqlCommand(qtr, con);
				scmd.ExecuteNonQuery();
				int temp = Convert.ToInt32(scmd.ExecuteScalar().ToString());
				if (temp > 0)
				{
					string smessage = "Department data is already available with us";
					Constant.alert(this, smessage);
					con.Close();
					data = true;
				}
			}
			catch (Exception) { }
			if (!data)
			{
				String query = "insert into Department (Department_Name, Faculty_Id, " +
					"Hod_Name, Hod_Username, Hod_Password) output inserted.Department_Id " +
					"values (@departmentName, " +
					"@facultyId, @hodName, @username" +
					", @password);";

				SqlCommand cmd = new SqlCommand(query, con);
				cmd.Parameters.AddWithValue("@departmentName", departmentName);
				cmd.Parameters.AddWithValue("@facultyId", facultyId);
				cmd.Parameters.AddWithValue("@hodName", hodName);
				cmd.Parameters.AddWithValue("@username", username);
				cmd.Parameters.AddWithValue("@password", password);

				String max_id = cmd.ExecuteScalar().ToString();

				String tableName = "StudentAttendance_" + max_id.ToString();

				bool f = false;
				try
				{
					//query = "CREATE TABLE " + tableName + " ( "
					//+ "    [Attendance_Id] INT    NOT NULL, "
					//+ "    [Student_Id]    BIGINT NOT NULL, "
					//+ "    PRIMARY KEY CLUSTERED ([Attendance_Id] ASC, [Student_Id] ASC), "
					//+ "    CONSTRAINT [FK_" + tableName + "_ToTable] FOREIGN KEY "
					//+ "    ([Attendance_Id]) REFERENCES [dbo].[Attendance] ([Attendance_Id]) ON DELETE CASCADE, "
					//+ "    CONSTRAINT [FK_" + tableName + "_ToTable_1] FOREIGN KEY "
					//+ "	   ([Student_Id]) REFERENCES [dbo].[Student] ([Student_Id]) "
					//+ ");";

					query = "CREATE TABLE [dbo].[" + tableName + "] (" +
							" [attendance_id] int NOT NULL," +
							" [student_id] bigint NOT NULL," +
							" PRIMARY KEY CLUSTERED([attendance_id] ASC, [student_id] ASC)," +
							" CONSTRAINT[table_name_" + tableName + "_to_StudentId] FOREIGN KEY" +
							" ([student_id]) REFERENCES[dbo].[Student]([student_id]) ON DELETE CASCADE" +
							" );";

					cmd = new SqlCommand(query, con);
					cmd.ExecuteNonQuery();
					Constant.alert(this, "Department add successfully");
					f = true;
				}
				catch (Exception ex)
				{
					log.Error(">> Exception in create table for student attendance table !!! ");
					log.Error(ex.Message);
					log.Error(ex);
				}

				if (!f)
				{
					Constant.alert(this, "Error While Create Department Attendance !!");
				}

				if (con.State == ConnectionState.Open)
					con.Close();
				ShowData();
			}
			textBox_department_hod.Text = "";
			textBox_department_name.Text = "";
			textBox_hod_username.Text = "";
		}

		private String encryption(String str)
		{
			return str;
		}

	}
}