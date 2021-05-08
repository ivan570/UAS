using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU.SubAdmin
{
    public partial class TeacherSubject2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);

        protected void consolePrint(String query, String message = "")
        {
            Response.Write("<script> console.log(\" printing " + message + " " + query + " \") </script> ");
        }
        protected void alert(String message)
        {
            string script = String.Format("alert('{0}');", message);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["subadmin"] == null)
                {
                    Response.Redirect("~/Login");
                }
                changeCourse();
                this.SearchTeacher();
                ShowData();
            }
        }

        protected void ShowData()
        {
            DataTable dt = new DataTable();
            con.Open();
            String query = "select ts.Teacher_Subject_Id,ts.Teacher_Id,t.Email,(SELECT top 1 value FROM STRING_SPLIT(Teacher_Name, ';') order by value) +' '+ (SELECT top 1 value FROM STRING_SPLIT(Teacher_Name, ';') order by value DESC) as Teacher_Name ,sub.Subject_Name, Concat(Class.Year,'-',Class.Semister)" +
                " as year_semister,Course.Course_Name from Teacher_Subject ts, subject sub, class, Course, teacher t where ts.Subject_Id=sub.Subject_Id and sub.Class_Id = Class.Class_Id and  Class.Course_Id = Course.Course_Id and t.Teacher_Id = ts.Teacher_Id and sub.Subject_Id='" + selectDropDownListSubject.SelectedValue + "' order by Teacher_Name";

            SqlDataAdapter adapt = new SqlDataAdapter(query, con);

            consolePrint(query, "showdata");

            adapt.Fill(dt);

            teacherGrid.DataSource = dt;
            teacherGrid.DataBind();

            con.Close();
        }
        protected void Search(object sender, EventArgs e)
        {
            this.SearchTeacher();
        }
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            teacher.PageIndex = e.NewPageIndex;
            this.SearchTeacher();
        }
        private void SearchTeacher()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                String sql = "select Teacher_Id, Email, Password, (SELECT top 1 value FROM STRING_SPLIT(Teacher_Name, ';') order by value) +' '+ (SELECT top 1 value FROM STRING_SPLIT(Teacher_Name, ';') order by value DESC) as Teacher_Name from Teacher where Teacher_Id not in (select Teacher_Id from Teacher_Subject where Subject_Id = '" + selectDropDownListSubject.SelectedValue + "')";
                String txt = txtSearch.Text.Trim();
                consolePrint(sql, "SearchTeacher");
                if (!string.IsNullOrEmpty(txt))
                {
                    sql += " and (Teacher_Name like '%" + txt + "%' " +
                           " or Email like '%" + txt + "%')";
                }
                cmd.CommandText = sql;
                cmd.Connection = con;

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    teacher.DataSource = dt;
                    teacher.DataBind();
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }

        protected void OnAssignClick(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            String teacherEmail = gvr.Cells[2].Text;


            if (con.State == ConnectionState.Closed)
                con.Open();
            if (selectDropDownListSubject.SelectedValue == "")
            {
                alert("Please first select subject");
            }
            else
            {
                String query = "insert into Teacher_Subject (Teacher_Id, Subject_Id) " +
                            " values ((select Teacher_Id from Teacher where Email='" + teacherEmail + "'), " +
                            " '" + selectDropDownListSubject.SelectedValue + "')";
                consolePrint(query, "bt_submit_click");

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                alert("Subject Assign to the teacher");
            }
            con.Close();
            //changeSubject();
            txtSearch.Text = "";
            ShowData();
            SearchTeacher();
        }
        protected void teacherGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Response.Write("ivankshu");
            GridViewRow row = (GridViewRow)teacherGrid.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("Teacher_Subject_Id");
            con.Open();
            String query = "delete from Teacher_Subject where Teacher_Subject_Id = '" + lbldeleteid.Text + "'";

            consolePrint(query, "delete");

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            alert("Assigned teacher deleted");
            con.Close();

            ShowData();
            SearchTeacher();
        }

        protected void changeCourse()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "SELECT Course_Id, Course_Name FROM Course, Department dt WHERE Course.Department_Id = dt.Department_Id " +
                " AND LOWER(dt.Hod_Username) = '" + Session["subadmin"] + "' order by Course_Name";

            consolePrint(query, "changeCourse");

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
            ShowData();
            SearchTeacher();
        }
        protected void changeClass()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "select Class_Id, CONCAT(Semister, ' ', Year) as year_semister from " +
                            " Class where Course_Id = '" + selectDropDownListCourse.SelectedValue + "' order by Year,Semister";

            consolePrint(query, "changeClass");

            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            selectDropDownListClass.DataTextField = ds.Tables[0].Columns["year_semister"].ToString();
            selectDropDownListClass.DataValueField = ds.Tables[0].Columns["Class_Id"].ToString();

            selectDropDownListClass.DataSource = ds.Tables[0];
            selectDropDownListClass.DataBind();

            con.Close();
            changeSubject();
            ShowData();
            SearchTeacher();

        }
        protected void selectDropDownListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeClass();
        }
        protected void selectDropDownListSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
            SearchTeacher();
        }
        protected void selectDropDownListClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeSubject();
        }
        protected void changeSubject()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "select Subject_Id, Subject_Name from subject where Class_Id = '" + selectDropDownListClass.SelectedValue + "' "
                            + " and Subject_Id not in"
                            + " (select distinct(Subject_Id) from Teacher_Subject ts, Teacher where Teacher.Teacher_Id = ts.Teacher_Id "
                            + " and Email = '" + Session["subadmin"] + "') order by Subject_Name";

            consolePrint(query, "changeSubject");

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
            SearchTeacher();
        }
    }
}