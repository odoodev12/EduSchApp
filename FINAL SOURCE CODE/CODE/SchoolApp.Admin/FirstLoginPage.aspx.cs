using Microsoft.SqlServer.Server;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class FirstLoginPage : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string eid = "";
                string gid = "";
                string vid = "";

                GetQueryString(out eid, out gid, out vid);

                if (!string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(vid))
                {
                    if (IsValidUser(eid, gid, vid))
                    {
                        if (!IsPostBack)
                        {
                            
                        }
                    }
                    else
                    {
                        Response.Redirect("~/FirstLoginExipred.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/FirstLoginExipred.aspx");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetQueryString(out string eid, out string gid, out string vid)
        {
            gid = vid = eid = "";
            if (Request.QueryString["eid"] != null)
            {
                eid = EncryptionHelper.Decrypt(Convert.ToString(Request.QueryString["eid"]));
            }

            if (Request.QueryString["gid"] != null)
            {
                gid = EncryptionHelper.Decrypt(Convert.ToString(Request.QueryString["gid"]));
            }

            if (Request.QueryString["vid"] != null)
            {
                vid = EncryptionHelper.Decrypt(Convert.ToString(Request.QueryString["vid"]));
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string eid = "";
                string gid = "";
                string vid = "";
                GetQueryString(out eid, out gid, out vid);
                if (!string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(vid))
                {
                    if (IsValidUser(eid, gid, vid))
                    {
                        if (vid == "Admin")
                        {
                            var isAdmin = DB.ISAdminLogins.FirstOrDefault(r => r.Email == eid && r.IsActivationID == gid && r.Active == true && r.Deleted == true && (r.ISActivated == null || !r.ISActivated.Value));
                            isAdmin.ISActivated = true;
                            isAdmin.IsActivationID = null;
                            //isAdmin.MemorableQueId = Convert.ToInt32(drpMemorableQue.SelectedValue);
                            isAdmin.MemorableQueAnswer = EncryptionHelper.Encrypt(txtMemorableAnswer.Text);
                            isAdmin.Pass = EncryptionHelper.Encrypt(txtPassword.Text);
                            DB.SaveChanges();

                            Authentication.LoggedInUser = isAdmin;
                            Response.Redirect("~/Dashboard.aspx");
                        }
                        else
                        {
                            var isOrgUser = DB.ISOrganisationUsers.FirstOrDefault(r => r.Email == eid && r.IsActivationID == gid && r.Active == true && r.Deleted == true && (r.ISActivated == null || !r.ISActivated.Value));
                            isOrgUser.ISActivated = true;
                            isOrgUser.IsActivationID = null;
                            //isOrgUser.MemorableQueId = Convert.ToInt32(drpMemorableQue.SelectedValue);
                            isOrgUser.MemorableQueAnswer = EncryptionHelper.Encrypt(txtMemorableAnswer.Text);
                            isOrgUser.Password = EncryptionHelper.Encrypt(txtPassword.Text);
                            DB.SaveChanges();

                            Authentication.LoggedInOrgUser = isOrgUser;
                            Response.Redirect("~/Dashboard.aspx");
                        }
                    }
                    else
                    {
                        ///show exipre page here
                        Response.Redirect("~/FirstLoginExipred.aspx");
                    }
                }
                else
                {
                    ///show exipre page here
                    Response.Redirect("~/FirstLoginExipred.aspx");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private bool IsValidUser(string eid, string gid, string vid)
        {
            bool isValid = false;
            
            if (vid == "Admin")
            {
                isValid = DB.ISAdminLogins.FirstOrDefault(r => r.Email == eid && r.IsActivationID == gid && r.Active == true && r.Deleted == true && (r.ISActivated == null || !r.ISActivated.Value)) != null;
            }
            else
            {
                isValid = DB.ISOrganisationUsers.FirstOrDefault(r => r.Email == eid && r.IsActivationID == gid && r.Active == true && r.Deleted == true && (r.ISActivated == null || !r.ISActivated.Value)) != null;
            }

            return isValid; 
        }
    }
}