using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.isLogin)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                loginInfo();
            }




        }
        public string GetUserType()
        {
            if (Authentication.ISSchoolLogin() == true)
            {
                return "1";
            }
            else if (Authentication.ISParentLogin() == true)
            {
                return "3";
            }
            else if (Authentication.ISTeacherLogin() == true)
            {
                return "2";
            }
            else
            {
                return "";
            }
        }
        public void loginInfo()
        {

        }

        protected void lnkSignout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            if (Authentication.ISSchoolLogin() == true)
            {
                int ID = Authentication.LogginSchool.ID;
                ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ID);

                objSchool.Password = EncryptionHelper.Encrypt(txtAdminpassword.Text);

                objSchool.ModifyBy = Authentication.LogginSchool.ID;
                objSchool.ModifyDateTime = DateTime.Now;
                DB.SaveChanges();

                Clear();
                LogManagement.AddLog("SchoolAdmin Password Updated Successfully " + "Name : " + objSchool.Name + " Document Category : School", "Profile");
                AlertMessageManagement.ServerMessage(Page, "SchoolAdmin Password Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
        }

        private void Clear()
        {
            txtAdmincpassword.Text = "";
            txtAdminpassword.Text = "";
        }
    }
}
