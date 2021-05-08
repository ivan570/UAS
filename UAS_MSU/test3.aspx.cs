using System;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace UAS_MSU
{
    public partial class csv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.SearchCustomers();
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            this.SearchCustomers();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvCustomers.PageIndex = e.NewPageIndex;
            this.SearchCustomers();
        }

        private void SearchCustomers()
        {
            
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivankshu\source\repos\UAS_MSU\UAS_MSU\App_Data\Database.mdf;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "select Department_id, Department_Name, Hod_Name, Faculty_Id from department";
                    
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        sql += " WHERE Department_id LIKE @ContactName + '%'";
                        cmd.Parameters.AddWithValue("@ContactName", txtSearch.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvCustomers.DataSource = dt;
                        gvCustomers.DataBind();
                    }
                }
            }
        }
    }
}