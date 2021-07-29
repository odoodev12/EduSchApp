using SchoolApp.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class Support : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISUserLogin())
            {
                if (!Authentication.ISOrgUserLogin())
                {
                    Response.Redirect(Authentication.AuthorizePage());
                }
                else
                {
                    if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                    }
                }
            }
        }
    }
}