using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace UAS_MSU.SubAdmin
{
	public partial class Student : System.Web.UI.Page
	{
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.SubAdmin.Student));

		SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		Random random = new Random();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["subadmin"] == null)
				{
					Response.Redirect("~/Login");
				}
				changeCourse();
			}
		}

		protected void changeCourse()
		{
			if (sqlCon.State == ConnectionState.Closed)
				sqlCon.Open();
			String query = "SELECT Course_Id, Course_Name FROM Course, Department dt WHERE Course.Department_Id = dt.Department_Id " +
				"AND LOWER(dt.Hod_Username) = '" + Session["subadmin"] + "' order by Course_Name";
			SqlCommand com = new SqlCommand(query, sqlCon);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds);
			selectDropDownListCourse.DataTextField = ds.Tables[0].Columns["Course_Name"].ToString();
			selectDropDownListCourse.DataValueField = ds.Tables[0].Columns["Course_Id"].ToString();

			selectDropDownListCourse.DataSource = ds.Tables[0];
			selectDropDownListCourse.DataBind();

			sqlCon.Close();
			changeClass();
		}
		protected void changeClass()
		{
			if (sqlCon.State == ConnectionState.Closed)
				sqlCon.Open();
			String query = "select Class_Id, CONCAT(Semister, ' ', Year) as Year_Semister " +
							" from Class where Course_Id='" + selectDropDownListCourse.SelectedValue + "' order by Year,semister";
			SqlCommand com = new SqlCommand(query, sqlCon);
			SqlDataAdapter da = new SqlDataAdapter(com);
			DataSet ds = new DataSet();
			da.Fill(ds);
			selectDropDownListClass.DataTextField = ds.Tables[0].Columns["Year_Semister"].ToString();
			selectDropDownListClass.DataValueField = ds.Tables[0].Columns["Class_Id"].ToString();

			selectDropDownListClass.DataSource = ds.Tables[0];
			selectDropDownListClass.DataBind();

			sqlCon.Close();
			ShowData();
		}
		protected void ShowData()
		{
			SqlDataAdapter adapt;
			DataTable dt;
			dt = new DataTable();

			if (sqlCon.State == ConnectionState.Closed)
				sqlCon.Open();

			String query = "select Student_Id, Student_Name, Prn, Email from " +
							" Student where Class_Id = '" + selectDropDownListClass.SelectedValue + "' order by Student_Name";

			adapt = new SqlDataAdapter(query, sqlCon);
			adapt.Fill(dt);

			studentGrid.DataSource = dt;
			studentGrid.DataBind();


			ViewState["dirState"] = dt;
			ViewState["sortdr"] = "Asc";

			sqlCon.Close();
		}
		protected String getRandomPassword()
		{
			String str = random.Next(10000000, 99999999).ToString();
			return str;
		}
		protected void alert(String message)
		{
			string script = String.Format("alert('{0}');", message);
			this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
		}
		protected void studentGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)studentGrid.Rows[e.RowIndex];
			Label lbldeleteid = (Label)row.FindControl("sid");
			sqlCon.Open();
			String query = "delete from Student where Student_Id='" + lbldeleteid.Text + "';";

			SqlCommand cmd = new SqlCommand(query, sqlCon);
			cmd.ExecuteNonQuery();
			sqlCon.Close();
			ShowData();
		}
		protected void studentGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
		{
			studentGrid.EditIndex = e.NewEditIndex;
			ShowData();
		}
		protected void studentGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
		{
			Label id = studentGrid.Rows[e.RowIndex].FindControl("sid") as Label;
			TextBox text_name = studentGrid.Rows[e.RowIndex].FindControl("textBox_name") as TextBox;
			TextBox text_prn = studentGrid.Rows[e.RowIndex].FindControl("textBox_prn") as TextBox;

			String curr = id.Text;
			String name = text_name.Text;
			String prn = text_prn.Text;
			sqlCon.Open();

			String query = "Update Student set Student_Name='" + name + "', Prn='" + prn + "' where Student_Id='" + curr + "';";

			SqlCommand cmd = new SqlCommand(query, sqlCon);
			cmd.ExecuteNonQuery();
			sqlCon.Close();
			studentGrid.EditIndex = -1;
			ShowData();
		}
		protected void studentGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
		{
			studentGrid.EditIndex = -1;
			ShowData();
		}
		protected void studentGrid_Sorting(object sender, GridViewSortEventArgs e)
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
				studentGrid.DataSource = dtrslt;
				studentGrid.DataBind();


			}

		}
		protected void btnUpload_Click(object sender, EventArgs e)
		{
			string ConStr = "";
			string ext = Path.GetExtension(fileupload.FileName).ToLower();
			string path = "D:\\data\\" + fileupload.FileName;
			fileupload.SaveAs(path);

			if (ext.Trim() == ".xls")
			{
				ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
					+ path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
			}
			else if (ext.Trim() == ".xlsx")
			{
				ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
					+ path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
			}
			string query = "SELECT * FROM [Sheet1$]";

			OleDbConnection oleDbCon = new OleDbConnection(ConStr);
			if (oleDbCon.State == ConnectionState.Closed)
			{
				oleDbCon.Open();
			}
			OleDbCommand cmd = new OleDbCommand(query, oleDbCon);

			using (OleDbDataReader reader = cmd.ExecuteReader())
			{
				if (sqlCon.State == ConnectionState.Closed)
					sqlCon.Open();
				try
				{
					int colCount = reader.FieldCount;
					int flag = 0;
					String q = "select count(*) from Student where Email='";
					String j = "Already registered email ids : ";

					query = "insert into Student ( Class_Id, ";
					for (int i = 0; i < colCount; ++i)
					{
						query += "" + reader.GetName(i) + ", ";
					}
					query += " Password)";
					query += " values (" + selectDropDownListClass.SelectedValue + ",";
					while (reader.Read())
					{
						String qq = q;
						int k = 0;
						for (k = 0; k < colCount; ++k)
						{
							if (reader.GetName(k) == "Email")
							{
								qq += "" + reader.GetString(k) + "' ;";
								break;
							}

						}

						SqlCommand sqlcommand = new SqlCommand(qq, sqlCon);
						int temp = Convert.ToInt32(sqlcommand.ExecuteScalar().ToString());
						if (temp == 1)
						{
							j += reader.GetString(k) + " ";
							flag = 1;


						}
						else
						{
							String sql = query;
							for (int i = 0; i < colCount; ++i)
							{
								if (reader.GetName(i).Equals("Prn"))
								{
									sql += " '" + reader.GetValue(i).ToString() + "', ";
								}
								else
								{
									sql += " '" + reader.GetString(i) + "', ";
								}
							}
							String random = getRandomPassword();
							sql += " '" + random + "') ";

							SqlCommand sqlcommand1 = new SqlCommand(sql, sqlCon);
							sqlcommand1.ExecuteNonQuery();

							emailSent(reader.GetString(k), random);
						}
					}
					if (sqlCon.State == ConnectionState.Open)
						sqlCon.Close();
					if (flag == 1)
					{
						alert(j);
					}
					else
					{
						alert("All data inserted.");
					}

				}
				catch (Exception ex)
				{
					log.Error("exception in data insert " + ex);
				}
				if (sqlCon.State == ConnectionState.Open)
					sqlCon.Close();
			}
			oleDbCon.Close();
			ShowData();
		}

		protected void selectDropDownListCourse_SelectedIndexChanged(object sender, EventArgs e)
		{
			changeClass();
		}

		protected void selectDropDownListClass_SelectedIndexChanged(object sender, EventArgs e)
		{
			ShowData();
		}

		protected void bt_add_Click(object sender, EventArgs e)
		{
			String name = textBox_name.Text.Trim();
			String email = textBox_email.Text.Trim();
			String prn = textBox_Prn.Text.Trim();
			String password = getRandomPassword();
			String query = "insert into Student (Email, Password, Student_Name, Prn, Class_ID) " +
				"values ('" + email + "', '" + password + "', '" + name + "', '" + prn + "', '" + selectDropDownListClass.SelectedValue + "');";


			if (sqlCon.State == ConnectionState.Closed)
				sqlCon.Open();

			String check = "select count(*) from Student where Email='" + email + "' or Prn='" + prn + "'";
			bool f = false;
			try
			{
				SqlCommand cmd = new SqlCommand(check, sqlCon);
				cmd.ExecuteNonQuery();
				int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

				if (temp > 0)
				{
					log.Info("check query " + query);
					alert("Student email or prn is registered ");
					f = true;
				}
			}
			catch (Exception) { }
			if (!f)
			{
				try
				{

					SqlCommand cmd = new SqlCommand(query, sqlCon);
					cmd.ExecuteNonQuery();

					log.Info("query in data insert " + query);
					string message = "Data added successfully";
					alert(message);
					if (sqlCon.State == ConnectionState.Open)
						sqlCon.Close();
					ShowData();
					emailSent(email, password);
				}
				catch (Exception eCatch)
				{
					string message = "Error in add data \n" + query;
					alert(message);
					log.Error("Error at 260 " + eCatch);
				}
			}
			if (sqlCon.State == ConnectionState.Open)
				sqlCon.Close();
			textBox_email.Text = "";
			textBox_name.Text = "";
			textBox_Prn.Text = "";
		}
		protected void emailSent(String email, String password)
		{
			string from = "harshkachhadiya55@gmail.com"; //From address    
			MailMessage message = new MailMessage(from, email);

			string mailbody = "Your Username is : " + email + " and Your Password is : " + password;
			message.Subject = "Student : Username and Password from UAS_MSU";
			message.Body = mailbody;
			message.BodyEncoding = System.Text.Encoding.UTF8;
			message.IsBodyHtml = true;
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			System.Net.NetworkCredential basicCredential1 = new
			System.Net.NetworkCredential("harshkachhadiya55@gmail.com", "Harsh@1213");
			client.EnableSsl = true;
			client.UseDefaultCredentials = false;
			client.Credentials = basicCredential1;

			try
			{
				client.Send(message);
			}
			catch (Exception) { }
		}
	}
}