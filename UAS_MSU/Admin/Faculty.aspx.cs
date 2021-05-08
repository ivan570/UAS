using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UAS_MSU.Admin
{
    public partial class Faculty : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
        SqlDataAdapter adapt;
        DataTable dt;
        protected void alert(String message)
        {
            string script = String.Format("alert('{0}');", message);
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "msgbox", script, true);
        }
        protected void consolePrint(String query, String message = "")
        {
            Response.Write("<script> console.log(\" printing " + message + " " + query + " \") </script> ");
        }

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

        protected void facultyGrid_Sorting(object sender, GridViewSortEventArgs e)
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
                facultyGrid.DataSource = dtrslt;
                facultyGrid.DataBind();
            }
        }
        protected void ShowData()
        {
            dt = new DataTable();
            if (con.State == ConnectionState.Closed)
                con.Open();
            
            adapt = new SqlDataAdapter("Select Faculty_Id,Faculty_Name from Faculty order by Faculty_Name", con);
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                facultyGrid.DataSource = dt;
                facultyGrid.DataBind();
                ViewState["dirState"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            else
            {
                facultyGrid.DataSource = null;
                facultyGrid.DataBind();
            }
            con.Close();
        }
        protected void facultyGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)facultyGrid.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lbl_Name");
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "delete from Faculty where Faculty_Id = @facultyId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@facultyId", lbldeleteid.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }
        protected void facultyGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {  
            facultyGrid.EditIndex = e.NewEditIndex;
            ShowData();//harsh
        }
        protected void facultyGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            Label name = facultyGrid.Rows[e.RowIndex].FindControl("lbl_Name") as Label;
            TextBox city = facultyGrid.Rows[e.RowIndex].FindControl("txt_City") as TextBox;
            String curr = name.Text;
            con.Open();
            //updating the record  
            String query = "Update Faculty set Faculty_Name='" + city.Text + "' where Faculty_Id='" + curr + "';";
           
            consolePrint("facultyGrid_RowUpdating", query);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            facultyGrid.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        protected void facultyGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            facultyGrid.EditIndex = -1;
            ShowData();
        }
        protected void bt_add_Click(object sender, EventArgs e)
        {
            
            String facultyName = textBox_faculty_name.Text;

            if (con.State == ConnectionState.Closed)
                con.Open();
            try
            {
                String query = "select count(*) from  Faculty where lower(Faculty_Name) = @fname";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fname", facultyName.ToLower());
                cmd.ExecuteNonQuery();
                int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (temp > 0)
                {
                    alert("Faculty data is already exist.");
                    textBox_faculty_name.Text = "";
                    return;
                }
                con.Close();
            }
            catch (Exception eException)
            {
                consolePrint("Faculty::bt_add_Click::sql", eException.ToString());
            }
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                String query = "insert into Faculty (Faculty_Name) values (@fname)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@fname", facultyName);
                cmd.ExecuteNonQuery();
                con.Close();

                alert("Faculty added successfully.");

            }
            catch (Exception eException)
            {
                consolePrint("Faculty::bt_add_Click::sql", eException.ToString());
                alert("Error while inserting the data.");
            }
            textBox_faculty_name.Text = "";
            if (con.State == ConnectionState.Open)
                con.Close();
            ShowData();
        }
    }
}