using SchoolApp.ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web
{
    public partial class GetProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetThisProduct1_Click(object sender, EventArgs e)
        {
            SendProductEmail();
        }
        protected void btnGetThisProduct2_Click(object sender, EventArgs e)
        {
            SendProductEmail();
        }
        protected void btnGetThisProduct3_Click(object sender, EventArgs e)
        {
            SendProductEmail();
        }
        protected void btnGetThisProduct4_Click(object sender, EventArgs e)
        {
            SendProductEmail();
        }

        private void SendProductEmail()
        {
            string body = string.Empty;
            string email = string.Empty; // enter email here
            string AdminBody = String.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                EmailManagement emailManagement = new EmailManagement();
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", body);
                emailManagement.SendEmail(email, "Thestmanager Product Registration", AdminBody);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Please Enter Official Email');", true);

            }

        }

    }
}