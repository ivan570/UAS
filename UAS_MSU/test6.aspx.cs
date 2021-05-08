using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace UAS_MSU
{
    public partial class test6 : System.Web.UI.Page
    {
        public static ArrayList Files = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ivankshu\source\repos\UAS_MSU\UAS_MSU\App_Data\Database.mdf;Integrated Security=True");
            string com = "select Course_Id, Course_Name from Course";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataSet myDataSet = new DataSet();
            adpt.Fill(myDataSet, "Course Details");
            DataTable myDataTable = myDataSet.Tables[0];
            DataRow tempRow = null;

            foreach (DataRow tempRow_Variable in myDataTable.Rows)
            {
                tempRow = tempRow_Variable;
                ListBox1.Items.Add(tempRow["Course_Id"].ToString());
            }

        }
    }
}