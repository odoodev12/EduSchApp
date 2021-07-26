using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            string eid = "";
            string gid = "";
            string vid = "";
            string pwd = "";
            GetQueryString(out eid, out gid, out vid, out pwd);

            if(eid == "" || gid=="" || vid == "" || pwd == "")
            {
                ///return to login page or nav to another error page
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string eid = "";
            string gid = "";
            string vid = "";
            string pwd = "";
            GetQueryString(out eid, out gid, out vid, out pwd);
            if (!string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(eid) && !string.IsNullOrEmpty(vid) && !string.IsNullOrEmpty(pwd))
            {
                if (IsValidUser(eid, gid, vid, pwd))
                {
                    if (vid == EnumsManagement.USERTYPE.SCHOOL.ToString())
                    {
                        UpdateSchoolRecords(eid, gid);
                    }
                    else if (vid == EnumsManagement.USERTYPE.PARENT.ToString())
                    {
                        UpdateParentRecords(eid, gid, pwd);
                    }
                    else if (vid == EnumsManagement.USERTYPE.TEACHER.ToString())
                    {
                        UpdateTeacherRecords(eid, gid);
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

        private void GetQueryString(out string eid, out string gid, out string vid, out string pwd)
        {
            gid = vid = pwd = eid = "";
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

            if (Request.QueryString["pwd"] != null)
            {
                pwd = Convert.ToString(Request.QueryString["pwd"]);
            }
        }

        private bool IsValidUser(string eid, string gid, string vid, string pwd)
        {
            bool isValid = false;

            if (vid == EnumsManagement.USERTYPE.SCHOOL.ToString())
            {
                isValid = DB.ISSchools.FirstOrDefault(p => p.AdminEmail.ToLower() == eid.ToLower() && p.Password == pwd && p.IsActivationID == gid && p.Deleted == true && p.Active == true &&
                p.AccountStatusID == 2) != null;
            }
            else if (vid == EnumsManagement.USERTYPE.PARENT.ToString())
            {
                ISStudent Obj = DB.ISStudents.OrderBy(p => p.ID).FirstOrDefault(p => ((p.ParantEmail1.ToLower() == eid.ToLower() && p.ParantPassword1 == pwd)
                || (p.ParantEmail2.ToLower() == eid.ToLower() && p.ParantPassword2 == pwd)) && p.Active == true && p.Deleted == true);

                if (Obj != null)
                {
                    isValid = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == eid.ToLower() && r.IsActivationID == gid) != null;
                }
            }
            else if (vid == EnumsManagement.USERTYPE.TEACHER.ToString())
            {
                isValid = DB.ISTeachers.FirstOrDefault(r => r.Email == eid && r.Password == pwd && r.IsActivationID == gid && r.Active == true && r.Deleted == true) != null;
            }

            return isValid;
        }


        private void UpdateSchoolRecords(string eid, string gid)
        {
            try
            {
                ISSchool _School = DB.ISSchools.FirstOrDefault(p => p.AdminEmail.ToLower() == eid.ToLower() && p.IsActivationID == gid && p.Deleted == true && p.Active == true &&
                p.AccountStatusID == 2);

                if (_School != null)
                {
                    _School.ISActivated = true;
                    _School.IsActivationID = null;                    
                    _School.Password = EncryptionHelper.Encrypt(txtPassword.Text);
                    DB.SaveChanges();


                    Session["LoggedInUser"] = _School;
                    Session["LoggedInUserType"] = EnumsManagement.USERTYPE.SCHOOL.ToString();
                    Response.Redirect($"~/ConfirmLogin.aspx?eid={EncryptionHelper.Encrypt(eid)}");

                    //Authentication.LogginSchool = _School;
                    //Authentication.SchoolID = _School.ID;
                    //Authentication.USERTYPE = EnumsManagement.USERTYPE.SCHOOL.ToString();
                    //Authentication.SchoolTypeID = Convert.ToInt32(_School.TypeID);
                    //Authentication.isLogin = true;
                    //Response.Redirect("~/School/Home.aspx", false);
                }
                else
                {
                    ///show invalid page
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateTeacherRecords(string eid, string gid)
        {
            try
            {
                ISTeacher teacher = DB.ISTeachers.FirstOrDefault(p => p.Email.ToLower() == eid.ToLower() && p.IsActivationID == gid && p.Deleted == true && p.Active == true);

                if (teacher != null)
                {
                    teacher.ISActivated = true;
                    teacher.IsActivationID = null;                    
                    teacher.Password = EncryptionHelper.Encrypt(txtPassword.Text);
                    DB.SaveChanges();


                    Session["LoggedInUser"] = teacher;
                    Session["LoggedInUserType"] = EnumsManagement.USERTYPE.TEACHER.ToString();
                    Response.Redirect($"~/ConfirmLogin.aspx?eid={EncryptionHelper.Encrypt(eid)}");

                    //ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == teacher.SchoolID);
                    //Authentication.LogginTeacher = teacher;
                    //Authentication.SchoolID = teacher.SchoolID;
                    //Authentication.USERTYPE = EnumsManagement.USERTYPE.TEACHER.ToString();
                    //Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                    //Authentication.isLogin = true;
                    //Response.Redirect("~/Teacher/Home.aspx");
                }
                else
                {
                    ///show invalid page
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateParentRecords(string eid, string gid, string pwd)
        {
            ParentManagement objParentManagement = new ParentManagement();
            try
            {
                ISStudent Obj = DB.ISStudents.OrderBy(p => p.ID).FirstOrDefault(p => ((p.ParantEmail1.ToLower() == eid.ToLower() && p.ParantPassword1 == pwd)
                || (p.ParantEmail2.ToLower() == eid.ToLower() && p.ParantPassword2 == pwd)) && p.Active == true && p.Deleted == true);

                if (Obj != null)
                {
                    ISStudentConfirmLogin confirmLogin = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == eid.ToLower());

                    confirmLogin.ISActivated = true;
                    confirmLogin.IsActivationID = null;                    
                    DB.SaveChanges();

                    if (Obj.ParantEmail1.ToLower() == eid.ToLower())
                        Obj.ParantPassword1 = EncryptionHelper.Encrypt(txtPassword.Text);
                    else
                        Obj.ParantPassword2 = EncryptionHelper.Encrypt(txtPassword.Text);

                    DB.SaveChanges();


                    MISStudent misStudent = objParentManagement.ParentLogin(eid, txtPassword.Text);
                    Session["LoggedInUserMisStudent"] = misStudent;
                    Session["LoggedInUser"] = Obj;
                    Session["LoggedInUserType"] = EnumsManagement.USERTYPE.PARENT.ToString();
                    Response.Redirect($"~/ConfirmLogin.aspx?eid={EncryptionHelper.Encrypt(eid)}");

                    //MISStudent misStudent = objParentManagement.ParentLogin(eid, txtPassword.Text);
                    //ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Obj.SchoolID);
                    //Authentication.LogginParent = misStudent;
                    //Authentication.LoginParentEmail = eid;
                    //Authentication.LoginParentName = misStudent.ParentName;
                    //Authentication.LoginParentRelation = misStudent.ParentRelation;
                    //Authentication.SchoolID = Obj.SchoolID.Value;
                    //Authentication.USERTYPE = EnumsManagement.USERTYPE.PARENT.ToString();
                    //Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                    //Authentication.isLogin = true;
                    //Response.Redirect("~/Parent/Home.aspx");
                }
                else
                {
                    ///show invalid page
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}