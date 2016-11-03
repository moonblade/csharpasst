using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using csharpasst.Helpers;
using csharpasst.Models;

namespace csharpasst.Views
{
    public partial class register : System.Web.UI.Page
    {
        protected void Register(object sender, EventArgs e)
        {
            Database d = Database.Instance;
            Owner user = new Owner(txtName.Text,TxtEmail.Text,GlobalVariables.md5(txtPWD.Text));
            bool registered = d.register(user);
            if (registered)
                Response.Redirect("login.aspx");
            else
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('There was an error in the registration request')</script>");

        }
    }
}