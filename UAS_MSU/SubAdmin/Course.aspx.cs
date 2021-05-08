using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace UAS_MSU.SubAdmin
{
    public partial class Course : System.Web.UI.Page
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
                ShowData();
            }
        }
        protected void ShowData()
        {
            DataTable dt = new DataTable();

            con.Open();
            String query = "SELECT Course_Id, Course_Name FROM Course, Department dt WHERE Course.Department_Id = dt.Department_Id " +
                "AND LOWER(dt.Hod_Username) = '" + Session["subadmin"] + "'";
            SqlDataAdapter adapt = new SqlDataAdapter(query, con);
            adapt.Fill(dt);
            courseGrid.DataSource = dt;
            courseGrid.DataBind();
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";

            if (con.State == ConnectionState.Open)
                con.Close();
        }
        protected void courseGrid_Sorting(object sender, GridViewSortEventArgs e)
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
                courseGrid.DataSource = dtrslt;
                courseGrid.DataBind();


            }

        }
        protected void bt_add_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String courseName = textBox_course_name.Text;
            Boolean f = false;
            String query = "SELECT COUNT(*) FROM Course, Department dt WHERE dt.Department_Id = Course.Department_Id " +
                "AND LOWER(Course_Name) = '" + courseName.ToLower() + "' AND dt.Hod_Username = '" + Session["subadmin"] + "'";
            try
            {
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (temp > 0)
                {
                    string message = "Course data is already available with us";
                    string script = String.Format("alert('{0}');", message);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
                    f = true;
                }
            }
            catch (Exception eException)
            {
                Response.Write("< br /> query " + query + " < br />");
                Response.Write("select ==> " + eException + "<br />");
            }
            if (!f)
            {
                String query0 = "insert into Course (Course_Name, Department_Id) values" +
                       " (@cname, (SELECT Department_Id FROM Department WHERE Hod_Username = @name))";

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand(query0, con);
                    cmd.Parameters.AddWithValue("@cname", courseName);
                    cmd.Parameters.AddWithValue("@name", Session["subadmin"]);
                    cmd.ExecuteNonQuery();

                    string message = "Course added successfully";
                    string script = String.Format("alert('{0}');", message);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);

                    if (con.State == ConnectionState.Open)
                        con.Close();

                    ShowData();
                }
                catch (Exception eException)
                {
                    Response.Write("insert  ==> " + eException + "<br /> <br /> query ==> " + query0);
                }
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            textBox_course_name.Text = "";
        }
        protected void courseGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)courseGrid.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("course_id");
            con.Open();
            String query = "delete from Course where Course_Id='" + lbldeleteid.Text + "';";
            Response.Write("<script> console.log(\"Delete query =>" + (query) + "\") </script> ");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }
        protected void courseGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            courseGrid.EditIndex = e.NewEditIndex;
            ShowData();
        }

        protected void courseGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            Label name = courseGrid.Rows[e.RowIndex].FindControl("course_id") as Label;
            TextBox city = courseGrid.Rows[e.RowIndex].FindControl("course_name") as TextBox;
            String curr = name.Text;
            con.Open();
            String query = "Update Course set Course_Name='" + city.Text + "' where Course_Id='" + curr + "';";
            Response.Write("<script> console.log(\"" + (query) + "\") </script> ");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            courseGrid.EditIndex = -1;
            ShowData();
        }
        protected void courseGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            courseGrid.EditIndex = -1;
            ShowData();
        }
    }
}