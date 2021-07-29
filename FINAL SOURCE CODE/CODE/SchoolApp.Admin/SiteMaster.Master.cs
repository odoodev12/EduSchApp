using SchoolApp.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authentication.ISUserLogin())
            {
                lblUserName.Text = Authentication.LoggedInUser.Email;
                lblUserType.Text = Authentication.LoggedInUser.Email;
            }
            if(Authentication.ISOrgUserLogin())
            {
                lblUserName.Text = Authentication.LoggedInOrgUser.FirstName;
                lblUserType.Text = Authentication.LoggedInOrgUser.ISRole.Name;
            }
            if (!IsPostBack)
            {

            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx", false);
        }

        protected void lnkLogouts_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx", false);
        }
        public string GetUserType()
        {
            if (Authentication.LoggedInOrgUser != null)
            {
                return Authentication.LoggedInOrgUser.RoleID.ToString();
            }
            else
            {
                return "0";
            }
        }
        public bool GetAdmin()
        {
            if (Authentication.ISUserLogin() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void lnkResetMemorableWord_Click(object sender, EventArgs e)
        {

        }
    }
}