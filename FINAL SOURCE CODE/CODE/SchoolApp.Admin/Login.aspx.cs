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
    public partial class Login : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnURL = "";
                if (Request.QueryString.AllKeys.Count() > 0)
                {
                    ReturnURL = Request.QueryString["ReturnURL"].ToString();
                }
                String pass = EncryptionHelper.Encrypt(txtPassword.Text);
                string UName = "";
                int UType = 0;

                List<ISAdminLogin> obj = DB.ISAdminLogins.Where(p => p.Email == txtUserName.Text && p.Pass == pass && p.Active == true && p.Deleted == true).ToList();
                if (obj.Count > 0)
                {

                    //Authentication.LoggedInUser = obj[0];
                    if (ReturnURL != "")
                    {
                        Response.Redirect(ReturnURL);
                    }
                    else
                    {
                        ///Here if user is not activated then redirect to first login page.
                        string eid = EncryptionHelper.Encrypt(obj[0].Email);
                        string vid = EncryptionHelper.Encrypt("Admin");

                        if (obj[0].ISActivated == null || obj[0].ISActivated.Value == false)
                        {
                            obj[0].IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                            DB.SaveChanges();
                            
                            string gid = EncryptionHelper.Encrypt(obj[0].IsActivationID);
                            
                            Response.Redirect($"~/FirstLoginPage.aspx?eid={eid}&gid={gid}&vid={vid}");
                        }
                        else
                        {
                            //Authentication.LoggedInUser = obj[0];
                            Session["LoggedInUser"] = obj[0];
                            Session["LoggedInUserType"] = "Admin";
                            Response.Redirect($"~/ConfirmLogin.aspx?eid={eid}&vid={vid}");
                        }
                    }

                }
                else if (DB.ISOrganisationUsers.Where(p => p.Email == txtUserName.Text && p.Password == pass && p.Active == true && p.Deleted == true).Count() > 0)
                {
                    List<ISOrganisationUser> objs = DB.ISOrganisationUsers.Where(p => p.Email == txtUserName.Text && p.Password == pass && p.Active == true && p.Deleted == true).ToList();
                    if (objs[0].StartDate.Value.Date <= DateTime.Now.Date && objs[0].EndDate.Value.Date >= DateTime.Now.Date)
                    {
                        //Authentication.LoggedInOrgUser = objs[0];
                        if (ReturnURL != "")
                        {
                            Response.Redirect(ReturnURL);
                        }
                        else
                        {
                            ///Here if user is not activated then redirect to first login page.
                            string eid = EncryptionHelper.Encrypt(obj[0].Email);
                            string vid = EncryptionHelper.Encrypt("OrgUser");

                            if (obj[0].ISActivated == null || obj[0].ISActivated.Value == false)
                            {
                                obj[0].IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                                DB.SaveChanges();

                                
                                string gid = EncryptionHelper.Encrypt(obj[0].IsActivationID);
                                
                                Response.Redirect($"~/FirstLoginPage.aspx?eid={eid}&gid={gid}&vid={vid}");
                            }
                            else
                            {
                                //Authentication.LoggedInOrgUser = objs[0];
                                Session["LoggedInUser"] = objs[0];
                                Session["LoggedInUserType"] = "OrgUser";
                                Response.Redirect($"~/ConfirmLogin.aspx?eid={eid}&vid={vid}");
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('Invalid UserName and Password')", true);
                }

            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void lnkForgot_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
}