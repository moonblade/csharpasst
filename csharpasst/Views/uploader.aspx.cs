using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using csharpasst.Helpers;
using csharpasst.Models;
using System.Net;

namespace csharpasst.Views
{
    public partial class uploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalVariables.loggedInUser == null)
            {
                Response.Redirect(GlobalVariables.starting);
            }
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("..\\files") + "\\" + fn;
                File f = new File(fn, GlobalVariables.loggedInUser.id, File1.PostedFile.ContentLength);
                f.id = Database.Instance.insert(f);
                if(f.id>0)
                {

                Perm p = new Perm(f.id, GlobalVariables.loggedInUser.id);
                Database.Instance.insert(p);
                try
                {
                    File1.PostedFile.SaveAs(SaveLocation);
                    Response.Write("The file has been uploaded.");
                }
                catch (Exception ex)

                 {
                    Response.Write("Error: " + ex.Message);
                    //Note: Exception.Message returns detailed message that describes the current exception. 
                    //For security reasons, we do not recommend you return Exception.Message to end users in 
                    //production environments. It would be better just to put a generic error message. 
                }
                }
                else
                {
                    Response.Write("Exceeds Quota");
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }

            List<File> lf = Database.Instance.getFiles(GlobalVariables.loggedInUser);
            List<string> ls = new List<string>();
            foreach (File f in lf)
            {
                ls.Add(f.filename);
            }
            DataGrid.DataSource = ls;
            DataGrid.DataBind();
        }
        public void Repeater_btn(Object Sender, RepeaterCommandEventArgs e)
        {
            Response.Redirect("userlist.aspx?filename=" + e.CommandName);
        }
    }
}