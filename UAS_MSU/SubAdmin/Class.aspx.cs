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
    public partial class Class : System.Web.UI.Page
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
                changeCourse();
            }
        }

        protected void ShowData()
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select Class_Id, Year, Semister from Class where Course_Id = '" + selectDropDownListCourse.SelectedValue + "' order by Year, Semister", con);
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                classGrid.DataSource = dt;
                classGrid.DataBind();
                ViewState["dirState"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            else
            {
                classGrid.DataSource = null;
                classGrid.DataBind();

            }
            con.Close();
        }
        protected void classGrid_Sorting(object sender, GridViewSortEventArgs e)
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
                classGrid.DataSource = dtrslt;
                classGrid.DataBind();


            }

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
            ShowData();
        }
        protected void selectDropDownListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }
        protected void bt_add_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String classYear = textBox_class_year.Text;
            String classSemister = textBox_class_semister.Text;
            Boolean f = false;
            try
            {
                String query = "select count(*) from Class where lower(Year) = @year and " +
                    " lower(Semister) = @semister and Course_Id='" + selectDropDownListCourse.SelectedValue + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@year", classYear.ToLower());
                cmd.Parameters.AddWithValue("@semister", classSemister.ToLower());
                cmd.ExecuteNonQuery();
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (temp > 0)
                {
                    string message = "Class data is already available with us";
                    string script = String.Format("alert('{0}');", message);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
                    f = true;
                }
            }
            catch (Exception eException)
            {
                Response.Write("select ==> " + eException + "<br />");
            }
            if (!f)
            {
                String query = "insert into Class (Year, Semister, Course_Id) values (@year, @semister, @course)";
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@year", classYear);
                    cmd.Parameters.AddWithValue("@semister", classSemister);
                    cmd.Parameters.AddWithValue("@course", selectDropDownListCourse.SelectedValue);
                    cmd.ExecuteNonQuery();

                    string message = "Class added successfully";
                    string script = String.Format("alert('{0}');", message);
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);

                    if (con.State == ConnectionState.Open)
                        con.Close();

                    ShowData();
                }
                catch (Exception eException)
                {
                    Response.Write("insert  ==> " + eException + "<br /> <br /> query ==> " + query);
                }
            }
            if (con.State == ConnectionState.Open)
                con.Close();


            textBox_class_semister.Text = "";
            textBox_class_year.Text = "";
        }
        protected void classGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)classGrid.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("class_id");
            con.Open();
            String query = "delete from Class where Class_Id='" + lbldeleteid.Text + "';";
            Response.Write("<script> console.log(\"Delete query =>" + (query) + "\") </script> ");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }
        protected void classGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            classGrid.EditIndex = e.NewEditIndex;
            ShowData();
        }
        protected void classGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            Label id = classGrid.Rows[e.RowIndex].FindControl("class_id") as Label;
            TextBox year = classGrid.Rows[e.RowIndex].FindControl("year") as TextBox;
            TextBox semister = classGrid.Rows[e.RowIndex].FindControl("semister") as TextBox;
            String curr = id.Text;
            con.Open();
            String query = "Update Class set Year='" + year.Text + "', Semister='" + semister.Text + "' where Class_Id='" + curr + "';";
            Response.Write("<script> console.log(\"" + (query) + "\") </script> ");
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            classGrid.EditIndex = -1;
            ShowData();
        }
        protected void classGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            classGrid.EditIndex = -1;
            ShowData();
        }
    }
}