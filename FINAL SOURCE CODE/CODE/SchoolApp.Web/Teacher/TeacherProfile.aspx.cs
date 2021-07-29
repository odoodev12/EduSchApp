using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Teacher
{
    public partial class TeacherProfile : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        ClassManagement objClassManagement = new ClassManagement();
        TeacherManagement objTeacherManagement = new TeacherManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (!Page.IsPostBack)
            {

                BindData(Convert.ToInt32(Authentication.LogginTeacher.ID));
                //BindDropdown();

            }
            if (IsPostBack && fpUpload.PostedFile != null)
            {
                if (fpUpload.PostedFile.FileName.Length > 0)
                {
                    int ID = Convert.ToInt32(Authentication.LogginTeacher.ID);
                    ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                    if (objTeacher != null)
                    {
                        string filename = System.IO.Path.GetFileName(fpUpload.PostedFile.FileName);
                        fpUpload.SaveAs(Server.MapPath("~/Upload/" + filename));
                        objTeacher.Photo = "Upload/" + filename;
                        DB.SaveChanges();
                        AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                        // sending email to admin & supervisor by Maharshi @ Gatistavam-softech
                        EmailManagement emobj = new EmailManagement();
                        var tcd = DB.ISTeachers.SingleOrDefault(a =>a.ID == ID);
                        emobj.SendEmail(tcd.Email, "  profile Picture changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + tcd.Name + ",<br /> your Profile Image  has been changed please confirm on website <br /> Thnk you");

                    }
                    BindData(ID);
                }
            }
        }

        private void BindData(int TeacherID)
        {
            MISTeacher Obj = objTeacherManagement.GetTeacher(TeacherID);
            if (Obj != null)
            {
                litClassName.Text = Obj.ClassName;
                litTeacherName.Text = Obj.Title + " " + Obj.Name;
                litTeacherNo.Text = Obj.TeacherNo;
                litTitle.Text = Obj.Title + " " + Obj.Name;
                litRole.Text = Obj.RoleName;
                litEmail.Text = Obj.Email;
                litPhone.Text = Obj.PhoneNo;
                litEndDate.Text = Obj.TeacherEndDate;
                Image6.ImageUrl = String.Format("{0}{1}", "../", Obj.Photo);
                AID.HRef = String.Format("{0}{1}", "../", Obj.Photo);
                AID.Attributes["data-title"] = Obj.Name;
            }
        }

        protected void resetpassword_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(Authentication.LogginTeacher.ID);
                ISTeacher objStudent = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
                if (objStudent != null)
                {
                    objStudent.Password = EncryptionHelper.Encrypt(txtpassword.Text);
                    objStudent.ModifyBy = Authentication.LogginTeacher.ID;
                    objStudent.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();

                    // sending email to admin & supervisor by Maharshi @ Gatistavam-softech
                    var objschoo = DB.ISSchools.SingleOrDefault(x => x.ID == objStudent.SchoolID);
                    EmailManagement emobj = new EmailManagement();
                     emobj.SendEmail(objStudent.Email, " Some profile Details(Password Reset) changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + objStudent.Name + ",<br /> " + "your Profile Details have been changed. please confirm on website <br /> Thnk you");
                    emobj.SendEmail(objschoo.SupervisorEmail, " teacher profile Details(Password Reset) changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + objschoo.SupervisorFirstname + objschoo.SupervisorLastname + " ,<br />" + "a teacher has updated profile details teachername : " + objStudent.Name + " " + "please confirm on website<br/> Thnk you");
                    emobj.SendEmail(objschoo.AdminEmail, "  teacher profile Details(Password Reset) changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + objschoo.AdminFirstName + objschoo.AdminLastName + " ,<br />" + "a teacher has updated profile details teachername : " + objStudent.Name + " " + "please confirm on website<br/> Thnk you");

                    Clear();
                    AlertMessageManagement.ServerMessage(Page, "Password Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        private void Clear()
        {
            txtcpassword.Text = "";
            txtpassword.Text = "";
        }
    }
}