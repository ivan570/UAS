using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace UAS_MSU.SubAdmin
{
    public partial class AssignStudent : System.Web.UI.Page
    {
        private static readonly Random random = Constant.random;
        log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.SubAdmin.AssignStudent));
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
                ShowData();
            }
        }
        protected void changeCourse()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "SELECT Course_Id, Course_Name FROM Course, Department dt " +
                " WHERE Course.Department_Id = dt.Department_Id " +
                " AND LOWER(dt.Hod_Username) = '" + Session["subadmin"] + "' order by Course_Name";

            log.Info("changeCourse query => " + query);

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
        }
        protected void changeClass()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "select Class_Id, CONCAT(Semister, ' ', Year) as year_semister from " +
                            " Class where Course_Id = '" + selectDropDownListCourse.SelectedValue + "' order by Year,Semister";

            log.Info("changeClass query => " + query);

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
            log.Info("finishing the changeSubject method");
            
        }
        protected void changeSubject()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "select * from Subject where Elective_Type = 'true' and Class_Id = '" + selectDropDownListClass.SelectedValue + "' order by Subject_Name";

            log.Info("changeSubject query => " + query);

            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            selectDropDownListSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString();
            selectDropDownListSubject.DataValueField = ds.Tables[0].Columns["Subject_Id"].ToString();

            selectDropDownListSubject.DataSource = ds.Tables[0];
            selectDropDownListSubject.DataBind();

            con.Close();
            // ShowData();
            changeStudent();
        }
        protected void changeStudent()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String query = "select Student_Id, Student_Name from Student " +
                             " where Class_Id='" + selectDropDownListClass.SelectedValue + "' " +
                             " and Student_Id not in " +
                             " (select Student_Id from Student_Subject " +
                             " where Subject_Id='"+selectDropDownListSubject.SelectedValue+"') order by Student_Name";

            log.Info("changeStudent query => " + query);

            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DDL.DataTextField = ds.Tables[0].Columns["Student_Name"].ToString();
            DDL.DataValueField = ds.Tables[0].Columns["Student_Id"].ToString();

            DDL.DataSource = ds.Tables[0];
            DDL.DataBind();

            con.Close();
            ShowData();
        }
        protected void studentGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)studentGrid.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("Student_Subject_Id");
            con.Open();
            String query = "delete from Student_Subject where Student_Subject_Id = '" + lbldeleteid.Text + "'";

            log.Info("rowDelete query => " + query);

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            changeStudent();
        }
        protected void ShowData()
        {
            DataTable dt = new DataTable();
            con.Open();
            String query = "Select ss.Student_Subject_Id, st.Student_Id, st.Student_Name,st.Prn, st.Email from" +
                " Student st, Student_Subject ss where ss.Student_Id = st.Student_Id and ss.Subject_Id = '"+selectDropDownListSubject.SelectedValue+"' order by Student_Name";

            SqlDataAdapter adapt = new SqlDataAdapter(query, con);

            log.Info("showData query => " + query);

            adapt.Fill(dt);

            studentGrid.DataSource = dt;
            studentGrid.DataBind();
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";

            con.Close();
        }
        protected void studentGrid_Sorting(object sender, GridViewSortEventArgs e)
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
                studentGrid.DataSource = dtrslt;
                studentGrid.DataBind();


            }

        }
        protected void selectDropDownListCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeClass();
        }

        protected void selectDropDownListClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeSubject();
        }
        protected void selectDropDownListSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeStudent();
            ShowData();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ConStr = "";
            string ext = Path.GetExtension(fileupload.FileName).ToLower();
            string path = "D:\\data\\" + fileupload.FileName;
            fileupload.SaveAs(path);

            if (ext.Trim() == ".xls")
            {
                ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                    + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (ext.Trim() == ".xlsx")
            {
                ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                    + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            string query = "SELECT * FROM [Sheet1$]";

            OleDbConnection conn = new OleDbConnection(ConStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            OleDbCommand cmd = new OleDbCommand(query, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    List<String> outclass = new List<string>();
                    List<String> alreadyAssign = new List<string>();
                    int colCount = reader.FieldCount;
                    while (reader.Read())
                    {
                        String s1 = "select count(*) from Student where Prn = '" + reader.GetValue(0).ToString() +
                            "' and Class_Id = '" + selectDropDownListClass.SelectedValue + "' " +
                            " union all select count(*) from Student_Subject, student" +
                            " where Student.Student_Id = Student_Subject.Student_Id and Prn = '" + reader.GetValue(0).ToString() +
                            "' and Subject_Id = '" + selectDropDownListSubject.SelectedValue + "';";
                        SqlCommand cmd1 = new SqlCommand(s1, con);


                        int inClass = -1, assignSubject = -1;

                        using (SqlDataReader sqlReader = cmd1.ExecuteReader())
                        {
                            sqlReader.Read();
                            inClass = Convert.ToInt32(sqlReader.GetValue(0).ToString());

                            sqlReader.Read();
                            assignSubject = Convert.ToInt32(sqlReader.GetValue(0).ToString());
                        }

                        log.Info("in class " + inClass + " assignSubject " + assignSubject);
                        log.Info("sql query " + s1);

                        if (inClass < 0)
                        {
                            outclass.Add(reader.GetValue(0).ToString());
                            log.Info("inclass or assignSubject prn " + reader.GetValue(0).ToString());
                        }
                        else if (assignSubject > 0)
                        {
                            alreadyAssign.Add(reader.GetValue(0).ToString());
                            log.Info("inclass or assignSubject prn " + reader.GetValue(0).ToString());
                        }
                        else
                        {
                            String sql = "insert into Student_Subject (Student_Id, Subject_Id) values " +
                                      " ((select Student_Id from Student where Prn = '" + reader.GetValue(0).ToString() + "')," +
                                      " '" + selectDropDownListSubject.SelectedValue + "')";


                            SqlCommand sqlcommand = new SqlCommand(sql, con);
                            sqlcommand.ExecuteScalar();
                        }

                    }
                    Constant.alert(this, "alreadyAssign " + alreadyAssign + " outclass " + outclass);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            conn.Close();
            changeStudent();
        }

        protected void bt_Assign_click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            String subject = selectDropDownListSubject.SelectedValue;

            Boolean f = false;
            try
            {
                String query = "insert into Student_Subject (Student_Id, Subject_Id) values ('" + DDL.SelectedValue + "','" + selectDropDownListSubject.SelectedValue + "')";
                
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

            }
            catch (Exception eException)
            {
                Response.Write("select ==> " + eException + "<br />");
            }
            con.Close();
            changeStudent();

        }
    }
}