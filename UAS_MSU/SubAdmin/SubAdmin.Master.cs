using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU.SubAdmin
{
    public partial class SubAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Logout(Object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Login");
        }
    }
}