using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UAS_MSU.SubAdmin
{
    public partial class Subject : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["subadmin"] == null)
                {
                    Response.Redirect("~/Login");
                }
                showNotice.Visible = false;
                showClass.Visible = true;
                changeCourse();
            }
        }
        protected void ShowData()
        {
            if (showClass.Visible == false)
            {
                teacherGrid.DataSource = null;
                teacherGrid.DataBind();
                teacherGrid.Columns[1].Visible = false;
                
                return;
            }
            DataTable dt = new DataTable();
            con.Open();
            String query;
            
                query = "select sub.Subject_Id, sub.Subject_Name, sub.Elective_Type as E_type, CONCAT(Class.year, '-', Class.semister) as Year_Semester , "
                        + " Course_Name as CourseName"
                        + " from Subject sub, Class, Course"
                        + " where sub.Class_Id = Class.Class_Id and Class.Course_Id = Course.Course_Id "
                        + " and Course.Course_Id = '" + selectDropDownListCourse.SelectedValue + "' and Class.Class_Id = '"+selectDropDownListClass.SelectedValue+ "' order by sub.Subject_Name; ";
            
            SqlDataAdapter adapt = new SqlDataAdapter(query, con);
            adapt.Fill(dt);
            teacherGrid.DataSource = dt;
            teacherGrid.DataBind();
            
            con.Close();
        }
        protected void changeCourse()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "SELECT Course_Id, Course_Name FROM Course, Department dt WHERE Course.Department_Id = dt.Department_Id " +
                 "AND LOWER(dt.Hod_Username) = '" + Session["subadmin"] + "' order by Course_Name";
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
            String query = "select Class_Id, CONCAT(Class.Semister, ' ', Year ) as Class_Name from Class where Course_Id = '"
                + selectDropDownListCourse.SelectedValue + "' order by Year,Semister";
            // Response.Write("changeClass" + query + "<br />");
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            selectDropDownListClass.DataTextField = ds.Tables[0].Columns["Class_Name"].ToString();
            selectDropDownListClass.DataValueField = ds.Tables[0].Columns["Class_Id"].ToString();

            selectDropDownListClass.DataSource = ds.Tables[0];
            selectDropDownListClass.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                showNotice.Visible = false;
                showClass.Visible = true;
            }
            else
            {
                showNotice.Visible = true;
                showClass.Visible = false;
            }
            con.Close();
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
            String subject = textBox_subject.Text;
            bool type = checkbox_type.Checked;
            if (con.State == ConnectionState.Closed)
                con.Open();
            bool data = false;
            try
            {
                String qtr = "select count(*) from Subject where " +
                    "lower(Subject_Name)='" + subject.ToLower() + "' " +
                    "and Class_Id='" + selectDropDownListClass.SelectedValue + "'";
                SqlCommand scmd = new SqlCommand(qtr, con);
                scmd.ExecuteNonQuery();
                // Response.Write("qtr " + qtr);
                int temp = Convert.ToInt32(scmd.ExecuteScalar().ToString());
                if (temp > 0)
                {
                    string smessage = "Subject data is already available with us";
                    string sscript = String.Format("alert('{0}');", smessage);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", sscript, true);
                    data = true;
                }
            }
            catch (Exception eException)
            {
                Response.Write(eException);
            }
            if (!data)
            {
                String query = "insert into Subject (Class_Id , Subject_Name, Elective_Type) " +
                "values ('" + selectDropDownListClass.SelectedValue + "', '" + subject + "', '" + type.ToString() + "')";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                    
                string message = "Subject added successfully";
                string script = String.Format("alert('{0}');", message);
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
                if (con.State == ConnectionState.Open)
                    con.Close();
                ShowData();
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            textBox_subject.Text = "";
            checkbox_type.Checked = false;

        }
        protected void teacherGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)teacherGrid.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("Subject_Id");
            con.Open();
            String str = lbldeleteid.Text;
            String query = "delete from Subject where Subject_Id='" + str + "';"; // need
            
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }
    }
}