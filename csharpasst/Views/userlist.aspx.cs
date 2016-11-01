using csharpasst.Helpers;
using csharpasst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csharpasst.Views
{
    public partial class userlist : System.Web.UI.Page
    {
        string filename;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalVariables.loggedInUser == null)
            {
                Response.Redirect(GlobalVariables.starting);
            }
            filename = Request.QueryString["filename"];
            List<string> ls = Database.Instance.getAllowedUsers(filename);
            if (!Database.Instance.isAllowed(GlobalVariables.loggedInUser.id, filename))
                Response.Redirect("uploader.aspx");
            allowedList.DataSource = ls;
            allowedList.DataBind();
            List<string> lso = Database.Instance.getDeniedUsers(filename);
            deniedList.DataSource = lso;
            deniedList.DataBind();
        }
        protected void denyUser(object sender, RepeaterCommandEventArgs e)
        {
            Database.Instance.denyUser(e.CommandName, filename);
            Response.Redirect(Request.RawUrl);
        }
        protected void allowUser(object sender, RepeaterCommandEventArgs e)
        {
            Database.Instance.allowUser(e.CommandName,filename);
            Response.Redirect(Request.RawUrl);
        }
        protected void download(object sender, EventArgs e)
        {
            string strURL = "/files/" + filename;
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            byte[] data = req.DownloadData(Server.MapPath(strURL));
            response.BinaryWrite(data);
            response.End();
        }
    }
}