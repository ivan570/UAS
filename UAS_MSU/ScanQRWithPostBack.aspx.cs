using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UAS_MSU
{
	public partial class ScanQRWithPostBack : System.Web.UI.Page, IPostBackEventHandler
	{
		log4net.ILog log = Constant.GetLog(typeof(UAS_MSU.ScanQRWithPostBack));
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public void RaisePostBackEvent(string eventArgument)
		{
			log.Info("RaisePostBackEvent " + eventArgument);
			if (eventArgument.Equals("Ivankshu"))
			Response.Redirect("~/ivankshu");
		}

	}
}