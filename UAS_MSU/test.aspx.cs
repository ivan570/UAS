using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;

namespace UAS_MSU
{
    public partial class test : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivankshu\source\repos\UAS_MSU\UAS_MSU\App_Data\Database.mdf;Integrated Security = True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String query = "select Course_Id, Course_Name from Course";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DDL.DataTextField = ds.Tables[0].Columns["Course_Name"].ToString();
                DDL.DataValueField = ds.Tables[0].Columns["Course_Id"].ToString();

                DDL.DataSource = ds.Tables[0];
                DDL.DataBind();
            }
        }
    }
}