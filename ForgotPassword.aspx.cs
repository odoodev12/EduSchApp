using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnForgot_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text != "" && OperationManagement.GetOperation("OP") != null)
            {
                string loginUrl = WebConfigurationManager.AppSettings["LoginURL"].ToString().Replace("/Login.aspx", "");
                string Type = OperationManagement.GetOperation("OP");
                string Email = txtUserName.Text;
                string LoggedINType = string.Empty;
                
                string vid = EncryptionHelper.Encrypt(Type);
                string activationId =  EncryptionHelper.Encrypt(Guid.NewGuid().ToString().Replace("-", ""));
               // string Password = CommonOperation.GenerateNewRandom();//Membership.GeneratePassword(10, 2);
                if (Type.ToLower() == "school")
                {
                    ISSchool _School = DB.ISSchools.OrderBy(p => p.ID).FirstOrDefault(p => p.AdminEmail == Email && p.Deleted == true);
                    if (_School != null)
                    {
                        //_School.Password = EncryptionHelper.Encrypt(Password);
                        _School.IsActivationID = activationId;
                        DB.SaveChanges();
                        LoggedINType = _School.Name;

                        string resetLink = $"{loginUrl}/ResetPassword.aspx?eid={EncryptionHelper.Encrypt(Email)}&vid={vid}&pwd={_School.Password}&gid={activationId}";

                        string AdminBody = String.Empty;

                        string tblAdminBody = string.Empty;

                        tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Password has been Reset. Please reset click on <a href=""" + resetLink + @""" target=""_blank"">Reset Password</a>
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

                        _EmailManagement.SendEmail(Email, "Reset Password Email", AdminBody);
                        Response.Redirect("Login.aspx?OP=School");
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Email Address.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else if (Type.ToLower() == "teacher")
                {
                    ISTeacher _Teacher = DB.ISTeachers.OrderBy(p => p.ID).FirstOrDefault(p => p.Email == Email && p.Deleted == true);
                    if (_Teacher != null)
                    {
                        LoggedINType = _Teacher.Name;
                        //_Teacher.Password = EncryptionHelper.Encrypt(Password);
                        _Teacher.IsActivationID = activationId;
                        DB.SaveChanges();
                        string AdminBody = String.Empty;

                        string resetLink = $"{loginUrl}/ResetPassword.aspx?eid={EncryptionHelper.Encrypt(Email)}&vid={vid}&pwd={_Teacher.Password}&gid={activationId}";

                        string tblAdminBody = string.Empty;

                        tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Password has been Reset. Please reset click on <a href=""" + resetLink + @""" target=""_blank"">Reset Password</a>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password Reset Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"<br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + _Teacher.ISSchool.Name + @".
                                    </td>
                                </tr></table>";

                        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                        {
                            AdminBody += reader.ReadToEnd();
                        }
                        AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                        _EmailManagement.SendEmail(Email, "Reset Password Email", AdminBody);
                        Response.Redirect("Login.aspx?OP=Teacher");
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Email Address.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else if (Type.ToLower() == "parent")
                {
                    ISStudent _Student = DB.ISStudents.OrderBy(p => p.ID).FirstOrDefault(p => p.ParantEmail1 == Email || p.ParantEmail2 == Email && p.Active == true && p.Deleted == true);
                    if (_Student != null)
                    {
                        LoggedINType = _Student.StudentName;

                        //foreach (var stud in DB.ISStudents.Where(p=> p.ParantEmail1 == Email || p.ParantEmail2 == Email && p.Active == true && p.Deleted == true).ToList())
                        {
                            //if (stud.ParantEmail1.ToLower() == Email)
                            //{
                            //    stud.ParantPassword1 = EncryptionHelper.Encrypt(Password);
                            //}
                            //else if (!string.IsNullOrEmpty(stud.ParantEmail2) &&  stud.ParantEmail2.ToLower() == Email)
                            //{
                            //    stud.ParantPassword2 = EncryptionHelper.Encrypt(Password);
                            //}
                            //DB.SaveChanges();
                        }

                        string AdminBody = String.Empty;
                        string pwd = _Student.ParantEmail1 == Email ? _Student.ParantPassword1 : _Student.ParantPassword2;

                        ISStudentConfirmLogin confirmStudent = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == Email.ToLower());
                        confirmStudent.IsActivationID = activationId;
                        DB.SaveChanges();

                        string resetLink = $"{loginUrl}/ResetPassword.aspx?eid={EncryptionHelper.Encrypt(Email)}&vid={vid}&pwd={pwd}&gid={activationId}";

                        string tblAdminBody = string.Empty;

                        tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent of " + LoggedINType + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your Password has been Reset. Please reset click on <a href=""" + resetLink + @""" target=""_blank"">Reset Password</a>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password Reset Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"<br/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + _Student.ISSchool.Name + @".
                                    </td>
                                </tr></table>";

                        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                        {
                            AdminBody += reader.ReadToEnd();
                        }
                        AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                        _EmailManagement.SendEmail(Email, "Reset Password Email", AdminBody);
                        Response.Redirect("Login.aspx?OP=Parent");
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Email Address.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "Please Select atleast one type.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            if (OperationManagement.GetOperation("OP") == EnumsManagement.USERTYPE.TEACHER.ToString())
            {
                Response.Redirect("Login.aspx?OP=Teacher");
            }
            else if (OperationManagement.GetOperation("OP") == EnumsManagement.USERTYPE.PARENT.ToString())
            {
                Response.Redirect("Login.aspx?OP=Parent");
            }
            else if (OperationManagement.GetOperation("OP") == EnumsManagement.USERTYPE.SCHOOL.ToString())
            {
                Response.Redirect("Login.aspx?OP=School");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}