using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;



namespace UAS_MSU
{
    public partial class test5 : System.Web.UI.Page
    {
        SqlConnection gcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivankshu\source\repos\UAS_MSU\UAS_MSU\App_Data\Database.mdf;Integrated Security=True");
        private static int PageSize = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDummyRow();
                ShowData();
            }
        }

        private void ShowData()
        {
            DataTable dt = new DataTable();

            gcon.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select Department_id, Department_Name, Hod_Name, Faculty_Id from department", gcon);
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gvCustomers.DataSource = dt;
                gvCustomers.DataBind();
            }
            else
            {
                gvCustomers.DataSource = null;
                gvCustomers.DataBind();
            }
            gcon.Close();
        }
        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();
            dummy.Columns.Add("Department_id");
            dummy.Columns.Add("Department_Name");
            dummy.Columns.Add("Hod_Name");
            dummy.Rows.Add();
            gvCustomers.DataSource = dummy;
            gvCustomers.DataBind();
        }

        [WebMethod]
        public static string GetCustomers(string searchTerm, int pageIndex)
        {
            string query = "select Department_id, Department_Name, Hod_Name, Faculty_Id from department";
            SqlCommand cmd = new SqlCommand(query);
            return GetData(cmd, pageIndex).GetXml();
        }

        private static DataSet GetData(SqlCommand cmd, int pageIndex)
        {
            
            using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivankshu\source\repos\UAS_MSU\UAS_MSU\App_Data\Database.mdf;Integrated Security=True"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "Department");
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            }
        }
    }
}