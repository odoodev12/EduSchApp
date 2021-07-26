using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SchoolApp.Web
{
    public partial class ResetMemorablePwd : System.Web.UI.Page
    {
        string userType = "";
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {   
            string eid = "";
            string gid = "";
            string vid = "";
            string pwd = "";
            GetQueryString(out eid, out gid, out vid, out pwd);

            if (eid == "" || gid == "" || vid == "" || pwd == "")
            {
                ///return to login page or nav to another error page
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
                gid = EncryptionHelper.Encrypt(Convert.ToString(Request.QueryString["gid"]));
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
        protected override void OnUnload(EventArgs e)
        {
            
        }

        private bool IsValidUser(string eid, string gid, string vid, string pwd)
        {
            bool isValid = false;

            if (vid == EnumsManagement.USERTYPE.SCHOOL.ToString())
            {
                isValid = DB.ISSchools.FirstOrDefault(p => p.AdminEmail.ToLower() == eid.ToLower() && p.Password == pwd && p.IsActivationID == gid && p.Deleted == true && p.Active == true) != null;
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
                        UpdateSchoolRecords(eid);
                    }
                    else if (vid == EnumsManagement.USERTYPE.PARENT.ToString())
                    {
                        UpdateParentRecords(eid, pwd);
                    }
                    else if (vid == EnumsManagement.USERTYPE.TEACHER.ToString())
                    {
                        UpdateTeacherRecords(eid);
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

        private void UpdateSchoolRecords(string emailId)
        {
            try
            {
                ISSchool _School = DB.ISSchools.OrderBy(p => p.ID).FirstOrDefault(p => p.AdminEmail == emailId && p.Deleted == true);  //Session["LoggedInUser"] as ISSchool;

                if (_School != null)
                {
                    _School.ISActivated = true;
                    _School.IsActivationID = null;
                    _School.MemorableQueAnswer = EncryptionHelper.Encrypt(txtMemorableAnswer.Text);                    
                    DB.SaveChanges();

                    Authentication.LogginSchool = _School;
                    Authentication.SchoolID = _School.ID;
                    Authentication.USERTYPE = EnumsManagement.USERTYPE.SCHOOL.ToString();
                    Authentication.SchoolTypeID = Convert.ToInt32(_School.TypeID);
                    Authentication.isLogin = true;
                    Response.Redirect("~/School/Home.aspx", false);
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

        private void UpdateTeacherRecords(string emailId)
        {
            try
            {
                ISTeacher teacher = DB.ISTeachers.OrderBy(p => p.ID).FirstOrDefault(p => p.Email == emailId && p.Deleted == true);  //Session["LoggedInUser"] as ISTeacher;

                if (teacher != null)
                {
                    teacher.ISActivated = true;
                    teacher.IsActivationID = null;
                    teacher.MemorableQueAnswer = EncryptionHelper.Encrypt(txtMemorableAnswer.Text);
                    
                    DB.SaveChanges();

                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == teacher.SchoolID);
                    Authentication.LogginTeacher = teacher;
                    Authentication.SchoolID = teacher.SchoolID;
                    Authentication.USERTYPE = EnumsManagement.USERTYPE.TEACHER.ToString();
                    Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                    Authentication.isLogin = true;
                    Response.Redirect("~/Teacher/Home.aspx");
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

        private void UpdateParentRecords(string emailId, string pwd)
        {
            ParentManagement objParentManagement = new ParentManagement();
            try
            {   
                MISStudent Obj = objParentManagement.ParentLogin(emailId, EncryptionHelper.Decrypt(pwd));

                if (Obj != null)
                {
                    ISStudentConfirmLogin confirmLogin = DB.ISStudentConfirmLogins.FirstOrDefault(r => r.Email.ToLower() == Obj.ParentEmail.ToLower());

                    confirmLogin.ISActivated = true;
                    confirmLogin.IsActivationID = null;
                    confirmLogin.MemorableQueAnswer = EncryptionHelper.Encrypt(txtMemorableAnswer.Text);
                    DB.SaveChanges();

                    
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Obj.SchoolID);
                    Authentication.LogginParent = Obj;
                    Authentication.LoginParentEmail = Obj.ParentEmail;
                    Authentication.LoginParentName = Obj.ParentName;
                    Authentication.LoginParentRelation = Obj.ParentRelation;
                    Authentication.SchoolID = Obj.SchoolID.Value;
                    Authentication.USERTYPE = EnumsManagement.USERTYPE.PARENT.ToString();
                    Authentication.SchoolTypeID = Convert.ToInt32(ObjSchool.TypeID);
                    Authentication.isLogin = true;
                    Response.Redirect("~/Parent/Home.aspx");
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