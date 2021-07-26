using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web
{
    public partial class Login : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["OP"] != null)
                {
                    lblLogin.Text = Request.QueryString["OP"].ToString();
                }
                else
                {
                    lblLogin.Text = "";
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationManagement.GetOperation("OP") == EnumsManagement.USERTYPE.TEACHER.ToString())
                {
                    TeacherManagement objTeacherManagement = new TeacherManagement();
                    string password = EncryptionHelper.Encrypt(txtPassword.Text);

                    if (!DB.ISTeachers.Any(r => r.Email.ToLower() == txtUserName.Text.ToLower() && r.Active == true && r.Deleted == true))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('Teacher Account email does not exists. Please contact School Admin')", true);
                    }
                    else if (!DB.ISTeachers.Any(r => r.Email.ToLower() == txtUserName.Text.ToLower() && r.Password == password && r.Active == true && r.Deleted == true))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('You have enterted wrong password. Please contact School Admin')", true);
                    }
                    else
                    {
                        ISTeacher ObjTeacher = objTeacherManagement.TeacherLogin(txtUserName.Text, txtPassword.Text);
                        // ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjTeacher.SchoolID);
                        if (ObjTeacher != null)
                        {
                            string eid = EncryptionHelper.Encrypt(ObjTeacher.Email);

                            if (ObjTeacher.ISActivated == null || ObjTeacher.ISActivated.Value == false)
                            {
                                ISTeacher tempTeacher = DB.ISTeachers.FirstOrDefault(r => r.ID == ObjTeacher.ID);

                                tempTeacher.IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                                DB.SaveChanges();
                                string op = EncryptionHelper.Encrypt(EnumsManagement.USERTYPE.TEACHER.ToString());
                                string gid = EncryptionHelper.Encrypt(tempTeacher.IsActivationID);

                                Response.Redirect($"~/FirstLoginPage.aspx?op={op}&pwd={tempTeacher.Password}&eid={eid}&gid={gid}");
                            }
                            else
                            {
                                Session["LoggedInUser"] = ObjTeacher;
                                Session["LoggedInUserType"] = EnumsManagement.USERTYPE.TEACHER.ToString();
                                Response.Redirect($"~/ConfirmLogin.aspx?eid={eid}");

                                //ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjTeacher.SchoolID);
                                //Authentication.LogginTeacher = ObjTeacher;
                                //Authentication.SchoolID = ObjTeacher.SchoolID;
                                //Authentication.USERTYPE = EnumsManagement.USERTYPE.TEACHER.ToString();
                                //Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                                //Authentication.isLogin = true;
                                //Response.Redirect("~/Teacher/Home.aspx");
                            }
                        }
                    }
                }
                if (OperationManagement.GetOperation("OP") == EnumsManagement.USERTYPE.PARENT.ToString())
                {
                    ParentManagement objParentManagement = new ParentManagement();
                    string password = EncryptionHelper.Encrypt(txtPassword.Text);

                    bool isParantEmail1 = DB.ISStudents.Any(r => r.ParantEmail1.ToLower() == txtUserName.Text.ToLower() && r.Active == true && r.Deleted == true);
                    bool isParantEmail2 = DB.ISStudents.Any(r => r.ParantEmail2.ToLower() == txtUserName.Text.ToLower() && r.Active == true && r.Deleted == true);

                    bool isemailIdPwd1 = DB.ISStudents.Any(r => r.ParantEmail1.ToLower() == txtUserName.Text.ToLower() && r.ParantPassword1 == password && r.Active == true && r.Deleted == true);
                    bool isemailIdPwd2 = DB.ISStudents.Any(r => r.ParantEmail2.ToLower() == txtUserName.Text.ToLower() && r.ParantPassword2 == password && r.Active == true && r.Deleted == true);

                    if (!isParantEmail1 && !isParantEmail2)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('Parent Account email does not exists. Please contact School Admin')", true);
                    }
                    else if (!isemailIdPwd1 && !isemailIdPwd2)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('You have enterted wrong password. Please contact School Admin')", true);
                    }
                    else
                    {
                        ISStudent ObjStudent = objParentManagement.ParentLogin(txtUserName.Text, txtPassword.Text);

                        if (ObjStudent != null)
                        {
                            ISStudentConfirmLogin stdLogin = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == txtUserName.Text.ToLower());
                            string eid = EncryptionHelper.Encrypt(txtUserName.Text);

                            if (stdLogin.ISActivated == null || stdLogin.ISActivated.Value == false)
                            {
                                string pwd = EncryptionHelper.Encrypt(txtPassword.Text);
                                stdLogin.IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                                DB.SaveChanges();
                                string op = EncryptionHelper.Encrypt(EnumsManagement.USERTYPE.PARENT.ToString());
                                string gid = EncryptionHelper.Encrypt(stdLogin.IsActivationID);

                                Response.Redirect($"~/FirstLoginPage.aspx?op={op}&pwd={pwd}&eid={eid}&gid={gid}");
                            }
                            else
                            {
                                MISStudent misStudent = objParentManagement.ParentLogin(txtUserName.Text, txtPassword.Text);
                                Session["LoggedInUserMisStudent"] = misStudent;
                                Session["LoggedInUser"] = ObjStudent;                                
                                Session["LoggedInUserType"] = EnumsManagement.USERTYPE.PARENT.ToString();
                                Response.Redirect($"~/ConfirmLogin.aspx?eid={eid}");


                                //ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjStudent.SchoolID);
                                //Authentication.LogginParent = ObjStudent;
                                //Authentication.LoginParentEmail = txtUserName.Text;
                                //Authentication.LoginParentName = objParentManagement.ParentLogin(txtUserName.Text, txtPassword.Text).ParentName;
                                //Authentication.LoginParentRelation = objParentManagement.ParentLogin(txtUserName.Text, txtPassword.Text).ParentRelation;
                                //Authentication.SchoolID = ObjStudent.SchoolID.Value;
                                //Authentication.USERTYPE = EnumsManagement.USERTYPE.PARENT.ToString();
                                //Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                                //Authentication.isLogin = true;
                                //Response.Redirect("~/Parent/Home.aspx");
                            }
                        }
                    }
                }
                if (OperationManagement.GetOperation("OP") == EnumsManagement.USERTYPE.SCHOOL.ToString())
                {
                    SchoolManagement objSchoolManagement = new SchoolManagement();
                    string password = EncryptionHelper.Encrypt(txtPassword.Text);



                    if(DB.ISSchools.Any(r => r.AdminEmail.ToLower() == txtUserName.Text.ToLower() && r.Active == false))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('School Account email has been deactivated by admin. Please contact School Admin')", true);
                    }
                    else if (!DB.ISSchools.Any(r => r.AdminEmail.ToLower() == txtUserName.Text.ToLower() && r.Deleted == true && r.Active == true))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('School Account email does not exists. Please contact School Admin')", true);
                    }
                    else if (!DB.ISSchools.Any(r => r.AdminEmail.ToLower() == txtUserName.Text.ToLower() && r.Password == password && r.Deleted == true && r.Active == true))
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "myscript", "alert('You have entered wrong password. Please contact School Admin.')", true);
                    }
                    else
                    {
                        ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.AdminEmail == txtUserName.Text && p.Password == password && p.Deleted == true && p.Active == true && (p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.InActive || p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.InProcess));
                        if (_School != null)
                        {
                            if (_School.ActivationDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                            {
                                _School.AccountStatusID = (int?)EnumsManagement.ACCOUNTSTATUS.Active;
                                DB.SaveChanges();
                            }
                        }
                        ISSchool ObjISSchool = objSchoolManagement.SchoolLogin(txtUserName.Text, txtPassword.Text);

                        if (ObjISSchool != null)
                        {

                            string eid = EncryptionHelper.Encrypt(ObjISSchool.AdminEmail);

                            if (ObjISSchool.ISActivated == null || ObjISSchool.ISActivated.Value == false)
                            {
                                ISSchool tempSchool = DB.ISSchools.FirstOrDefault(r => r.ID == ObjISSchool.ID);

                                tempSchool.IsActivationID = Guid.NewGuid().ToString().Replace("-", "");
                                DB.SaveChanges();
                                string op = EncryptionHelper.Encrypt(EnumsManagement.USERTYPE.SCHOOL.ToString());
                                string gid = EncryptionHelper.Encrypt(tempSchool.IsActivationID);

                                Response.Redirect($"~/FirstLoginPage.aspx?op={op}&pwd={tempSchool.Password}&eid={eid}&gid={gid}");
                            }
                            else
                            {
                                Session["LoggedInUser"] = ObjISSchool;
                                Session["LoggedInUserType"] = EnumsManagement.USERTYPE.SCHOOL.ToString();
                                Response.Redirect($"~/ConfirmLogin.aspx?eid={eid}");

                                //Authentication.LogginSchool = ObjISSchool;
                                //Authentication.SchoolID = ObjISSchool.ID;
                                //Authentication.USERTYPE = EnumsManagement.USERTYPE.SCHOOL.ToString();
                                //Authentication.SchoolTypeID = Convert.ToInt32(ObjISSchool.TypeID);
                                //Authentication.isLogin = true;
                                //Response.Redirect("~/School/Home.aspx", false);
                            }
                        }                        
                    }
                }



            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void lnkForgot_Click(object sender, EventArgs e)
        {
            if (OperationManagement.GetOperation("OP") != null)
            {
                Response.Redirect("ForgotPassword.aspx?OP=" + OperationManagement.GetOperation("OP"));
            }
            else
            {
                Response.Redirect("ForgotPassword.aspx");
            }
        }
    }
}