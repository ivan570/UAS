using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;



namespace UAS_MSU
{
	public partial class Test11 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string con = ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString;
			SqlConnection conn = new SqlConnection(con);
			conn.Open();
			string str = "select Faculty_id, Faculty_Name from Faculty";
			SqlCommand cmd = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			GridView1.DataSource = dt;
			GridView1.DataBind();
		}

		protected void LinkButton_Click(Object sender, CommandEventArgs e)
		{
			if (e.CommandArgument != null)
			{
				Response.Redirect("test.aspx?IdPassed=" + e.CommandArgument.ToString());
			}
		}
	}
}