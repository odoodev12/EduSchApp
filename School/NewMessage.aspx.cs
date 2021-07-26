using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class NewMessage : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {

                drpReceiverGroup.Items.Add(new ListItem { Text = "Teacher", Value = "2" });
                drpReceiverGroup.Items.Add(new ListItem { Text = "Parent", Value = "3" });
                if (IsStandardSchool())
                {
                    drpReceiverGroup.Items.Add(new ListItem { Text = "NonTeacher", Value = "4" });
                }

                drpReceiverGroup.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiverGroup.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
                
                //BindClassDropdown();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
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

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }

        private void BindClassDropdown()
        {
            int SID = Authentication.LogginSchool.ID;
            if (drpReceiverGroup.SelectedValue == "4")
            {
                drpClass.DataSource = DB.ISClasses.Where(p => p.SchoolID == SID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
                drpClass.DataTextField = "Name";
                drpClass.DataValueField = "ID";
                drpClass.DataBind();
                drpClass.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpClass.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            else
            {
                drpClass.DataSource = DB.ISClasses.Where(p => !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true && p.SchoolID == SID).ToList();
                drpClass.DataTextField = "Name";
                drpClass.DataValueField = "ID";
                drpClass.DataBind();
                drpClass.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpClass.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
        }



        protected void btnSend_Click(object sender, EventArgs e)
        {
            string FileName = "";
            if (fpUploadAttachment.HasFile == true)
            {
                string filename = System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName);
                fpUploadAttachment.SaveAs(Server.MapPath("~/Upload/" + filename));
                FileName = Server.MapPath("../Upload/" + System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName));
            }
            else
            {
                if (Session["UploadFilePath"] != null)
                {
                    string uploadedFilePath = Convert.ToString(Session["UploadFilePath"]);
                    FileName = Server.MapPath("../Upload/" + System.IO.Path.GetFileName(uploadedFilePath));
                }
            }

            if (drpReceiverGroup.SelectedValue == "2")
            {
                int ReceiverID = Convert.ToInt32(drpReceiver.SelectedValue);
                if (drpReceiver.SelectedValue == "-1")
                {
                    TeacherManagement objTeacherManagement = new TeacherManagement();
                    if (drpClass.SelectedValue != "0" && drpClass.SelectedValue != "-1")
                    {
                        int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                        List<MISTeacher> obj = objTeacherManagement.TeacherList(Authentication.SchoolID, "", ClassID, "", "", "", 0, "0");
                        foreach (var item in obj)
                        {
                            try
                            {
                                string Email = item.Email;
                                EmailManagement objEmailManagement = new EmailManagement();
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                    }
                    else
                    {
                        List<MISTeacher> obj = objTeacherManagement.TeacherList(Authentication.SchoolID, "", 0, "", "", "", 0, "0");
                        foreach (var item in obj)
                        {
                            try
                            {
                                string Email = item.Email;
                                EmailManagement objEmailManagement = new EmailManagement();
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID).Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br/><br/> &nbsp; {1}.<br /><br/>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }
            }
            if (drpReceiverGroup.SelectedValue == "3")
            {
                int ReceiverID = Convert.ToInt32(drpReceiver.SelectedValue);
                if (drpReceiver.SelectedValue == "-1")
                {
                    ParentManagement objParentManagement = new ParentManagement();
                    if (drpClass.SelectedValue != "0" && drpClass.SelectedValue != "-1")
                    {
                        int IDs = Convert.ToInt32(drpClass.SelectedValue);

                        List<MISStudent> List = new List<MISStudent>();
                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                        if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            List = objParentManagement.ParentList(Authentication.SchoolID);
                            int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                            if (ClassID < 1)
                            {
                                ClassID = 0;
                            }
                            else
                            {
                                ISClass ObjCls = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                                if (ObjCls.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                                {
                                    ClassManagement objClassManagement = new ClassManagement();
                                    List = objClassManagement.StudentListByOfficeClass(Authentication.SchoolID);//objParentManagement.ParentList(Authentication.LogginTeacher.SchoolID).Where(p => p.ClassID == ClasssID).ToList();
                                }
                                else
                                {
                                    List = List.Where(p => p.ClassID == ClassID).ToList();
                                }
                            }
                        }
                        else
                        {
                            string dts = DateTime.Now.ToString("dd/MM/yyyy");
                            List = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                            int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                            if (ClassID < 1)
                            {
                                ClassID = 0;
                            }
                            else
                            {
                                ISClass ObjClS = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                                List = List.Where(p => p.ClassID == ClassID).ToList();
                            }
                        }
                        foreach (var item in List)
                        {
                            try
                            {
                                string Email = item.ParantEmail1;
                                EmailManagement objEmailManagement = new EmailManagement();
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parent Of {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                    }
                    else
                    {
                        List<MISStudent> List = new List<MISStudent>();
                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                        if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            List = objParentManagement.ParentList(Authentication.SchoolID);
                        }
                        else
                        {
                            string dts = DateTime.Now.ToString("dd/MM/yyyy");
                            List = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                        }

                        foreach (var item in List)
                        {
                            try
                            {
                                string Email = item.ParantEmail1;
                                EmailManagement objEmailManagement = new EmailManagement();
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parent Of {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        string Email = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiverID).ParantEmail1;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parent Of {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }
            }
            if (drpReceiverGroup.SelectedValue == "4")
            {
                int ReceiverID = Convert.ToInt32(drpReceiver.SelectedValue);
                if (drpReceiver.SelectedValue == "-1")
                {
                    TeacherManagement objTeacherManagement = new TeacherManagement();
                    if (drpClass.SelectedValue != "0" && drpClass.SelectedValue != "-1")
                    {
                        int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                        List<MISTeacher> obj = objTeacherManagement.NonTeacherList(Authentication.SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                        if (ClassID != 0)
                        {
                            obj = obj.Where(p => p.ISTeacherClassAssignments.Any(m => m.ClassID == ClassID)).ToList();
                        }
                        foreach (var item in obj)
                        {
                            try
                            {
                                string Email = item.Email;
                                EmailManagement objEmailManagement = new EmailManagement();
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                    }
                    else
                    {
                        List<MISTeacher> obj = objTeacherManagement.NonTeacherList(Authentication.SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                        foreach (var item in obj)
                        {
                            try
                            {
                                string Email = item.Email;
                                EmailManagement objEmailManagement = new EmailManagement();
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                                objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID).Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", drpReceiver.SelectedItem.Text, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }
            }
            if (drpReceiverGroup.SelectedValue == "-1")
            {
                //Teachers
                TeacherManagement objTeacherManagement = new TeacherManagement();
                List<MISTeacher> obj = objTeacherManagement.TeacherList(Authentication.SchoolID, "", 0, "", "", "", 0, "0");
                foreach (var item in obj)
                {
                    try
                    {
                        string Email = item.Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", item.Name, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }

                //Student's Parents
                ParentManagement objParentManagement = new ParentManagement();
                List<MISStudent> List = new List<MISStudent>();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    List = objParentManagement.ParentList(Authentication.SchoolID);
                }
                else
                {
                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                    List = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                }
                foreach (var item in List)
                {
                    try
                    {
                        string Email = item.ParantEmail1;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parent Of {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", item.StudentName, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }

                //Non-Teachers
                List<MISTeacher> objs = objTeacherManagement.NonTeacherList(Authentication.SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                foreach (var item in objs)
                {
                    try
                    {
                        string Email = item.Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", item.Name, txtDescription.Text, Authentication.LogginSchool.Name, Authentication.LogginSchool.PhoneNumber, Authentication.LogginSchool.AdminEmail);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }
            }
            Clear();
            LogManagement.AddLog("Message Sent Successfully" + "Subject : " + txtSubject.Text + " Document Category : Message", "Message");
            AlertMessageManagement.ServerMessage(Page, "Message Sent Successfully!!!", (int)AlertMessageManagement.MESSAGETYPE.Success);
            Response.Redirect("Home.aspx");
        }
        private void Clear()
        {
            txtDescription.Text = "";
            txtSubject.Text = "";
            drpReceiver.SelectedValue = "0";
            drpReceiverGroup.SelectedValue = "0";

            drpClass.Enabled = true;
            drpReceiver.Enabled = true;
            drpClass.CssClass = "form-control";
            drpReceiver.CssClass = "form-control";
            drpReceiver.Items.Clear();
            drpClass.Items.Clear();
        }
        protected void drpReceiverGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReceiverGroup.SelectedValue != "-1")
            {
                drpClass.Enabled = true;
                drpReceiver.Enabled = true;
                BindClassDropdown();
                BindDropDowndata();
            }
            else
            {
                drpClass.Enabled = false;
                drpClass.CssClass = "form-control";
                drpReceiver.Enabled = false;
                drpReceiver.CssClass = "form-control";
                drpClass.Items.Clear();
                drpReceiver.Items.Clear();
            }
        }

        public void BindDropDowndata()
        {
            if (Convert.ToInt32(drpReceiverGroup.SelectedValue) == (int)EnumsManagement.MESSAGEUSERTYPE.Teacher)
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();

                int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                if (ClassID < 1)
                {
                    ClassID = 0;
                }

                drpReceiver.DataSource = objTeacherManagement.TeacherList(Authentication.SchoolID, "", ClassID, "", "", "", 0, "0");
                drpReceiver.DataValueField = "ID";
                drpReceiver.DataTextField = "Name";
                drpReceiver.DataBind();
                drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            if (Convert.ToInt32(drpReceiverGroup.SelectedValue) == (int)EnumsManagement.MESSAGEUSERTYPE.Parent)
            {
                StudentManagement objStudentManagement = new StudentManagement();

                ParentManagement objParentManagement = new ParentManagement();
                List<MISStudent> List = new List<MISStudent>();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    List = objParentManagement.ParentList(Authentication.SchoolID);
                    int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                    if (ClassID < 1)
                    {
                        ClassID = 0;
                    }
                    else
                    {
                        ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        if (ObjCl.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            ClassManagement objClassManagement = new ClassManagement();
                            List = objClassManagement.StudentListByOfficeClass(Authentication.SchoolID);//objParentManagement.ParentList(Authentication.LogginTeacher.SchoolID).Where(p => p.ClassID == ClasssID).ToList();
                        }
                        else
                        {
                            List = List.Where(p => p.ClassID == ClassID).ToList();
                        }
                    }
                }
                else
                {
                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                    List = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                    int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                    if (ClassID < 1)
                    {
                        ClassID = 0;
                    }
                    else
                    {
                        ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        List = List.Where(p => p.ClassID == ClassID).ToList();
                    }
                }
                drpReceiver.DataSource = List;
                drpReceiver.DataValueField = "ID";
                drpReceiver.DataTextField = "StudentName";
                drpReceiver.DataBind();
                drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            if (Convert.ToInt32(drpReceiverGroup.SelectedValue) == (int)EnumsManagement.MESSAGEUSERTYPE.NonTeacher)
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();
                int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                if (ClassID < 1)
                {
                    ClassID = 0;
                }
                List<MISTeacher> ObjNonTeacher = objTeacherManagement.NonTeacherList(Authentication.SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                if (ClassID != 0)
                {
                    ObjNonTeacher = ObjNonTeacher.Where(p => p.ISTeacherClassAssignments.Any(m => m.ClassID == ClassID)).ToList();
                }
                drpReceiver.DataSource = ObjNonTeacher.ToList();
                drpReceiver.DataValueField = "ID";
                drpReceiver.DataTextField = "Name";
                drpReceiver.DataBind();
                drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
        }
        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDowndata();
        }
    }
}

