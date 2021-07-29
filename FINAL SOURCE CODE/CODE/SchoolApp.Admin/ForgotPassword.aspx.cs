using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string loginUrl = WebConfigurationManager.AppSettings["LoginURL"].ToString().Replace("/Login.aspx","");
                List <ISAdminLogin> obj = DB.ISAdminLogins.Where(p => p.Email.ToLower() == txtUserName.Text.ToLower() && p.Active == true && p.Deleted == true).ToList();
                if (obj.Count > 0)
                {
                    string eid = EncryptionHelper.Encrypt(obj[0].Email);
                    string vid = EncryptionHelper.Encrypt("Admin");

                    obj[0].IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                    obj[0].IsResetPassword = true;
                    DB.SaveChanges();

                    string gid = EncryptionHelper.Encrypt(obj[0].IsActivationID);
                    string resetLink = $"{loginUrl}/ResetPassword.aspx?eid={eid}&gid={gid}&vid={vid}";
                    SendEmail(obj[0].FullName, obj[0].Email, resetLink);
                    //Response.Redirect($"~/FirstLoginPage.aspx?eid={eid}&gid={gid}&vid={vid}");
                }
                else if (DB.ISOrganisationUsers.Where(p => p.Email.ToLower() == txtUserName.Text.ToLower() && p.Active == true && p.Deleted == true).Count() > 0)
                {
                    ISOrganisationUser objs = DB.ISOrganisationUsers.FirstOrDefault(p => p.Email.ToLower() == txtUserName.Text.ToLower() && p.Active == true && p.Deleted == true);

                    string eid = EncryptionHelper.Encrypt(objs.Email);
                    string vid = EncryptionHelper.Encrypt("OrgUser");

                    objs.IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                    objs.IsResetPassword = true;
                    DB.SaveChanges();

                    string gid = EncryptionHelper.Encrypt(objs.IsActivationID);
                    string resetLink = $"{loginUrl}/ResetPassword.aspx?eid={eid}&gid={gid}&vid={vid}";

                    SendEmail(objs.FirstName + ' ' + objs.LastName, objs.Email, resetLink);
                    //Response.Redirect($"~/FirstLoginPage.aspx?eid={eid}&gid={gid}&vid={vid}");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void SendEmail(string userName, string emailID, string resetLink)
        {

            string AdminBody = string.Empty;
            string tblAdminBody = string.Empty;

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + userName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Password has been Reset.<br/>
                                </td>

                             </tr>
                            <tr>
                                <td>
                                    Please click on <a href=""" + resetLink + @""" target=""_blank"">Reset Password</a>
                                </td>                                
                            </tr>
                                <tr>
                                    <td>
                                        Password Reset Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"<br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact Admin.
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(emailID, "Reset Password Notification", AdminBody);
        }

    }
}