using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace UAS_MSU.Admin
{
	public partial class Teacher : System.Web.UI.Page
	{
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.Admin.Teacher));
		private static readonly Random random = new Random();
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				if (Session["admin"] == null)
				{
					Response.Redirect("~/Login");
				}
				ShowData();
			}
		}
		protected void alert(String message)
		{
			string script = String.Format("alert('{0}');", message);
			this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
		}
		protected String getRandomPassword()
		{
			String str = random.Next(10000000, 99999999).ToString();
			return str;
		}
		protected void bt_add_Click(object sender, EventArgs e)
		{
			String fname = textBox_teacher_fname.Text;
			String lname = textBox_teacher_lname.Text;
			String name = fname.Trim() + ";" + lname.Trim();
			String email = textBox_email.Text;
			String password = getRandomPassword();
			String query = "insert into Teacher (Email, Password, Teacher_Name) values ('" + email + "', '" + password + "', '" + name + "');";
			if (con.State == ConnectionState.Closed)
				con.Open();

			String check = "select count(*) from Teacher where Email='" + email + "'";
			bool f = false;
			try
			{
				SqlCommand cmd = new SqlCommand(check, con);
				cmd.ExecuteNonQuery();
				int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
				if (temp > 0)
				{
					alert("Teacher email is registered");
					f = true;
				}
			}
			catch (Exception) { }
			if (!f)
			{
				try
				{
					SqlCommand cmd = new SqlCommand(query, con);
					cmd.ExecuteNonQuery();
					string message = "Data added successfully";
					alert(message);
					if (con.State == ConnectionState.Open)
						con.Close();
					ShowData();
					emailSent(email, password);
				}
				catch (Exception eCatch)
				{
					string message = "Error in add data \n" + query;
					alert(message);
				}
			}
			if (con.State == ConnectionState.Open)
				con.Close();
			textBox_email.Text = "";
			textBox_teacher_fname.Text = "";
			textBox_teacher_lname.Text = "";
		}

		protected void emailSent(String email, String password)
		{
			string from = "harshkachhadiya55@gmail.com";    
			MailMessage message = new MailMessage(from, email);

			string mailbody = "Your Username is : " + email + " and Your Password is : " + password;
			message.Subject = "Teacher : Username and Password from UAS_MSU";
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
				alert("Mail sent");
			}
			catch (Exception) { }
		}

		protected void teacherGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)teacherGrid.Rows[e.RowIndex];
			Label lbldeleteid = (Label)row.FindControl("tid");
			con.Open();
			String query = "delete from Teacher where Teacher_Id='" + lbldeleteid.Text + "';";
			
			log.Info("Delete query => " + query);
			
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			con.Close();
			ShowData();
		}
		protected void teacherGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
		{
			//NewEditIndex property used to determine the index of the row being edited.  
			teacherGrid.EditIndex = e.NewEditIndex;
			ShowData();
		}
		protected void teacherGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
		{
			Label id = teacherGrid.Rows[e.RowIndex].FindControl("tid") as Label;
			TextBox fname = teacherGrid.Rows[e.RowIndex].FindControl("textBox_fname") as TextBox;
			TextBox lname = teacherGrid.Rows[e.RowIndex].FindControl("textBox_lname") as TextBox;
			String curr = id.Text;

			String name = fname.Text + ";" + lname.Text;
			con.Open();
			
			String query = "Update Teacher set Teacher_Name='" + name + "' where Teacher_Id='" + curr + "';";
			alert(query);
			
			log.Info(query);

			log.Info("teacherGrid_RowUpdating" + query);
			SqlCommand cmd = new SqlCommand(query, con);
			cmd.ExecuteNonQuery();
			con.Close();
			teacherGrid.EditIndex = -1;
			ShowData();
		}
		protected void teacherGrid_Sorting(object sender, GridViewSortEventArgs e)
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
				teacherGrid.DataSource = dtrslt;
				teacherGrid.DataBind();


			}

		}
		protected void teacherGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
		{
			teacherGrid.EditIndex = -1;
			ShowData();
		}
		protected void ShowData()
		{
			SqlDataAdapter adapt;
			DataTable dt;
			dt = new DataTable();

			con.Open();
			String query = "select Teacher_Id, Email, (SELECT top 1 value FROM STRING_SPLIT(Teacher_Name, ';') order by value) as fname, " +
				"(SELECT top 1 value FROM STRING_SPLIT(Teacher_Name, ';') order by value DESC) as lname from Teacher order by fname,lname";

			adapt = new SqlDataAdapter(query, con);
			adapt.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				teacherGrid.DataSource = dt;
				teacherGrid.DataBind();
				ViewState["dirState"] = dt;
				ViewState["sortdr"] = "Asc";
			}
			else
			{
				teacherGrid.DataSource = null;
				teacherGrid.DataBind();
			}
			con.Close();
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

			OleDbConnection conn = new OleDbConnection(ConStr);
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			OleDbCommand cmd = new OleDbCommand(query, conn);

			using (OleDbDataReader reader = cmd.ExecuteReader())
			{
				if (con.State == ConnectionState.Closed)
					con.Open();
				try
				{
					int colCount = reader.FieldCount;
					String q = "select count(*) from Teacher where Email='";
					String j = "Already registered email ids : ";

					query = "insert into Teacher (";
					for (int i = 0; i < colCount; ++i)
					{
						query += "" + reader.GetName(i) + ", ";
					}
					query += " Password)";
					query += " values (";
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

						SqlCommand sqlcommand = new SqlCommand(qq, con);
						int temp = Convert.ToInt32(sqlcommand.ExecuteScalar().ToString());
						if (temp == 1)
						{
							j += reader.GetString(k) + " ";

						}
						else
						{
							String sql = query;
							for (int i = 0; i < colCount; ++i)
							{
								sql += " '" + reader.GetString(i) + "', ";
							}
							String random = getRandomPassword();
							sql += " '" + random + "') ";
							SqlCommand sqlcommand1 = new SqlCommand(sql, con);
							sqlcommand1.ExecuteNonQuery();
						
							log.Info("reader.GetString(k) : " + reader.GetString(k) + " random : " + random);
							emailSent(reader.GetString(k), random);
						}
					}
					if (con.State == ConnectionState.Open)
						con.Close();
					alert(j);
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
				if (con.State == ConnectionState.Open)
					con.Close();
			}
			conn.Close();
			ShowData();
		}
	}
}