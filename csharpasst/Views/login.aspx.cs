using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using csharpasst.Models;
using csharpasst.Helpers;

namespace csharpasst.Views
{
    public partial class login : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
             
        //}
        protected void gotoRegister(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Database d = Database.Instance;
            Owner loggedInUser = d.login(txtEmail.Text, GlobalVariables.md5(txtPWD.Text));
            if (loggedInUser!=null)
            {
                GlobalVariables.loggedInUser = loggedInUser;
                Response.Redirect("uploader.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Email and Password')</script>");
            }
        }
    }
}