using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class NewMessageaspx : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    //BindDropdown();
                }

                int SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                lblSchoolNamewithColor.Text = " @ " + DB.ISSchools.FirstOrDefault(r => r.ID == SchoolID).Name;

                Clear();
            }
        }
        //protected void BindDropdown()
        //{
        //    int ID = Convert.ToInt32(Request.QueryString["ID"]);
        //    ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
        //    drpReceiver.DataSource = DB.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.ID==obj.SchoolID).ToList();
        //    drpReceiver.DataTextField = "Name";
        //    drpReceiver.DataValueField = "ID";
        //    drpReceiver.DataBind();
        //    drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
        //}

        protected override void OnInitComplete(EventArgs e)
        {
            FileUpload();
        }

        private void FileUpload()
        {
            try
            {
                if (Session["UploadFilePath"] != null)
                {
                    string uploadedFilePath = Convert.ToString(Session["UploadFilePath"]);
                    string filename = System.IO.Path.GetFileName(uploadedFilePath);
                    fpUploadAttachment.SaveAs(Server.MapPath("~/Upload/" + filename));
                    lblFileName.Visible = true;
                    lblFileName.Text = $"File Uploaded: {filename}";
                }
                else
                {
                    lblFileName.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private int GetStudentId()
        {
            int studentId = 0;
            if (Request.QueryString["SchoolID"] != null && Request.QueryString["Name"] != null)
            {
                int schoolId = Convert.ToInt32(Request.QueryString["SchoolID"]);
                string StudentName = Convert.ToString(Request.QueryString["Name"]);

                studentId = DB.ISStudents.FirstOrDefault(r => r.StudentName == StudentName && r.SchoolID == schoolId).ID;


            }
            return studentId;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = Authentication.LogginParent;
                string FileName = "";
                int StudentID = GetStudentId(); // Convert.ToInt32(Request.QueryString["ID"]);
                string StudentName = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID).StudentName;
                string ClassName = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID).ISClass.Name;
                string ParentName = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID).ParantName1;
                if (fpUploadAttachment.HasFile == true)
                {
                    string filename = System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName);
                    fpUploadAttachment.SaveAs(Server.MapPath("../Upload/" + filename));
                    FileName = Server.MapPath("../Upload/" + System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName));
                }

                if (drpReceiverGroup.SelectedValue == "1")
                {
                    EmailManagement objEmailManagement = new EmailManagement();
                    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> {1}.<br /><br/>Thanks, <br/> StudentName : {2}<br>Parent Name : {3}<br>Class : {4}<br>", "Admin", txtDescription.Text, StudentName, ParentName, ClassName);
                    if (FileName != "")
                    {
                        objEmailManagement.SendEmails(hdnfldEmailIds.Value, txtsubject.Text, Message, FileName);
                    }
                    else
                    {
                        objEmailManagement.SendEmail(hdnfldEmailIds.Value, txtsubject.Text, Message);
                    }
                }
                if (drpReceiverGroup.SelectedValue == "2")
                {
                    EmailManagement objEmailManagement = new EmailManagement();
                    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br>{1}.<br /><br/>Thanks, <br/> StudentName : {2}<br>Parent Name : {3}<br>Class : {4}<br>", "Sir", txtDescription.Text, StudentName, ParentName, ClassName);
                    if (FileName != "")
                    {
                        objEmailManagement.SendEmails(hdnfldEmailIds.Value, txtsubject.Text, Message, FileName);
                    }
                    else
                    {
                        objEmailManagement.SendEmail(hdnfldEmailIds.Value, txtsubject.Text, Message);
                    }
                }
                Clear();
                
                AlertMessageManagement.ServerMessage(Page, "Message Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                Response.Redirect("Message.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void Clear()
        {
            txtDescription.Text = "";
            txtsubject.Text = "";
            hdnfldEmailIds.Value = string.Empty;
        }

        protected void drpReceiverGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnfldEmailIds.Value = string.Empty;


            if (drpReceiverGroup.SelectedValue == "0")
            {
                hdnfldEmailIds.Value = string.Empty;
            }
            if (drpReceiverGroup.SelectedValue == "1")
            {
                if (Request.QueryString["Name"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                    string studentname = Request.QueryString["Name"];

                    ISStudent Obj = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == ID && p.StudentName == studentname && p.Active == true);
                    var shids = DB.ISStudents.Where(p => p.StudentName == Obj.StudentName && p.Deleted == true).Select(q => q.SchoolID).Distinct().ToList();

                    List<ISSchool> ObjSchool = DB.ISSchools.Where(p => p.ID == ID && p.Deleted == true).ToList();

                    hdnfldEmailIds.Value = string.Join(";", ObjSchool.Select(r => r.AdminEmail).ToList());
                    //drpReceiver.DataSource = ObjSchool;
                    //drpReceiver.DataTextField = "Name";
                    //drpReceiver.DataValueField = "ID";
                    //drpReceiver.DataBind();
                    //drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                    //int ID = Authentication.SchoolID;
                    //SchoolManagement objSchoolManagement = new SchoolManagement();
                    //drpReceiver.DataSource = objSchoolManagement.SchoolList().Where(p => p.ID == ID).ToList();
                    //drpReceiver.DataValueField = "ID";
                    //drpReceiver.DataTextField = "Name";
                    //drpReceiver.DataBind();
                }
            }
            if (drpReceiverGroup.SelectedValue == "2")
            {
                if (Request.QueryString["Name"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                    string studentname = Request.QueryString["Name"];

                    TeacherManagement objTeacherManagement = new TeacherManagement();
                    ISStudent ObjSt = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == ID && p.StudentName == studentname && p.Active == true);

                    var teacherList = objTeacherManagement.TeacherList(ObjSt.SchoolID.Value, "", ObjSt.ClassID.Value, "", "", "", 0, "1");
                    hdnfldEmailIds.Value = string.Join(";", teacherList.Select(r => r.Email).ToList());
                }
            }
            //start Code
            int SID = Authentication.SchoolID;
            if (drpReceiverGroup.SelectedValue == "3")
            {
                var classList = DB.ISClasses.Where(p => p.SchoolID == SID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
                hdnfldEmailIds.Value = string.Join(";", classList.SelectMany(r => r.ISTeacherClassAssignments).ToList().Select(r => r.ISTeacher.Email).Distinct().ToList());
            }
            else
            {
                var classList = DB.ISClasses.Where(p => !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true && p.SchoolID == SID).ToList();
                hdnfldEmailIds.Value = string.Join(";", classList.SelectMany(r => r.ISTeacherClassAssignments).ToList().Select(r => r.ISTeacher.Email).Distinct().ToList());
            }
            //End code
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Message.aspx", true);
        }
    }
}