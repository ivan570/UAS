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
    public partial class WebForm1 : System.Web.UI.Page
    {
		SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FinalConnectionString"].ConnectionString);
		SqlDataAdapter sda;
		DataTable dt;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				refreshdata();
			}
		}
		public void refreshdata()
		{

			
			SqlCommand cmd = new SqlCommand("select Faculty_Id, Faculty_Name from Faculty", con);
			sda = new SqlDataAdapter(cmd);
			dt = new DataTable();
			sda.Fill(dt);
			GridView1.DataSource = dt;
			GridView1.DataBind();
			ViewState["dirState"] = dt;
			ViewState["sortdr"] = "Asc";


		}
		protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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
				GridView1.DataSource = dtrslt;
				GridView1.DataBind();


			}

		}
	}
}