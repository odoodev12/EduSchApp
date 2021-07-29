using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web.Teacher
{
    public partial class Message : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (!IsPostBack)
            {
                drpReceiverGroup.Items.Add(new ListItem { Text = "School", Value = "1" }); 
                drpReceiverGroup.Items.Add(new ListItem { Text = "Teacher", Value = "2" });
                drpReceiverGroup.Items.Add(new ListItem { Text = "Parent", Value = "3" });
                if (IsStandardSchool())
                {
                    drpReceiverGroup.Items.Add(new ListItem { Text = "NonTeacher", Value = "4" });
                }

                drpReceiverGroup.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });

                BindDropdown();
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
        private void BindDropdown()
        {
            int TID = Authentication.LogginTeacher.ID;
            TeacherManagement objTeacherManagement = new TeacherManagement();
            drpClass.DataSource = objTeacherManagement.TeacherClassAssignment(TID).OrderByDescending(r => (!r.ClassName.Contains("(Office)") && !r.ClassName.Contains("(After School)") && !r.ClassName.Contains("(Club)"))).ThenBy(r => r.ClassName).ToList();
            drpClass.DataTextField = "ClassName";
            drpClass.DataValueField = "ClassID";
            drpClass.DataBind();
            drpClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
            drpClass.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);
            string FileName = "";
            if (fpUploadAttachment.HasFile == true)
            {
                string filename = System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName);
                fpUploadAttachment.SaveAs(Server.MapPath("../Upload/" + filename));
                FileName = Server.MapPath("../Upload/" + System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName));
            }

            if (drpReceiverGroup.SelectedValue == "1")
            {
                int ReceiverID = Convert.ToInt32(drpReceiver.SelectedValue);
                if (drpReceiver.SelectedValue == "-1")
                {
                    SchoolManagement objSchoolManagement = new SchoolManagement();
                    List<MISSchool> obj = objSchoolManagement.SchoolList().Where(p => p.ID == Authentication.SchoolID).ToList();
                    foreach (var item in obj)
                    {
                        try
                        {
                            string Email = item.AdminEmail;
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br/>Thanks, <br/> {2}<br>School : {3}<br>", drpReceiver.SelectedItem.Text, txtDesc.Text, Authentication.LogginTeacher.Name, ObjSchool.Name);
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
                    try
                    {
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> &nbsp; {1}.<br /><br/>Thanks, <br/> {2}<br>School : {3}<br>", drpReceiver.SelectedItem.Text, txtDesc.Text, Authentication.LogginTeacher.Name, ObjSchool.Name);
                        objEmailManagement.SendEmails(ObjSchool.AdminEmail, txtSubject.Text, Message, FileName);
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

                    List<MISStudent> obStudent = new List<MISStudent>();
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        obStudent = objParentManagement.ParentList(Authentication.SchoolID);
                    }
                    else
                    {
                        string dts = DateTime.Now.ToString("dd/MM/yyyy");
                        obStudent = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                    }
                    //List<MISStudent> obStudent = objParentManagement.ParentList(Authentication.LogginTeacher.SchoolID);
                    if (drpClass.SelectedValue != "0" && drpClass.SelectedValue != "-1")
                    {
                        int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                        ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        if (ObjCl.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            ClassManagement objClassManagement = new ClassManagement();
                            obStudent = objClassManagement.StudentListByOfficeClass(Authentication.SchoolID);
                        }
                        else
                        {
                            obStudent = obStudent.Where(p => p.ClassID == ClassID).ToList();
                        }
                    }
                    foreach (var item in obStudent)
                    {
                        try
                        {
                            string Email = item.ParantEmail1;
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> &nbsp; {1}.<br /><br/>Thanks, <br/> {2}<br>School : {3}<br>", drpReceiver.SelectedItem.Text, txtDesc.Text, Authentication.LogginTeacher.Name, ObjSchool.Name);
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
                    try
                    {
                        string Email = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiverID).ParantEmail1;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> &nbsp; {1}.<br /><br/>Thanks, <br/> {2}<br>School : {3}<br>", drpReceiver.SelectedItem.Text, txtDesc.Text, Authentication.LogginTeacher.Name, ObjSchool.Name);
                        objEmailManagement.SendEmails(Email, txtSubject.Text, Message, FileName);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                }
            }
            Clear();
            AlertMessageManagement.ServerMessage(Page, "Message Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

            System.Threading.Thread.Sleep(1000);
            Response.Redirect("Home.aspx");
        }

        private void Clear()
        {
            txtDesc.Text = "";
            txtSubject.Text = "";
            drpReceiver.SelectedValue = "0";
            drpClass.SelectedValue = "0";
            drpReceiverGroup.SelectedValue = "0";
            drpClass.Enabled = true;
            drpReceiver.Items.Clear();
        }
        protected void drpReceiverGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDowndata();
        }

        private void BindDropDowndata()
        {
            drpClass.Enabled = true;
            if (drpReceiverGroup.SelectedValue == "0")
            {
                drpClass.Enabled = true;
                drpReceiver.Items.Clear();
            }
            if (drpReceiverGroup.SelectedValue == "1")
            {
                drpClass.Enabled = false;
                drpClass.CssClass = "form-control";
                int ID = Authentication.LogginTeacher.SchoolID;
                SchoolManagement objSchoolManagement = new SchoolManagement();
                drpReceiver.DataSource = objSchoolManagement.SchoolList().Where(p => p.ID == ID).ToList();
                drpReceiver.DataValueField = "ID";
                drpReceiver.DataTextField = "Name";
                drpReceiver.DataBind();
                drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            if (drpReceiverGroup.SelectedValue == "3")
            {
                drpClass.Enabled = true;
                drpClass.CssClass = "form-control";
                StudentManagement objStudentManagement = new StudentManagement();

                ParentManagement objParentManagement = new ParentManagement();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                List<MISStudent> objList = new List<MISStudent>();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objParentManagement.ParentList(Authentication.SchoolID);
                }
                else
                {
                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                    objList = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                }
                drpReceiver.DataSource = objList.OrderBy(r => r.SchoolName).ToList();
                drpReceiver.DataValueField = "ID";
                drpReceiver.DataTextField = "StudentName";
                drpReceiver.DataBind();
                drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            //start Code
            int SID = Authentication.SchoolID;
            if (drpReceiverGroup.SelectedValue == "4")
            {
                //drpClass.DataSource = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Authentication.LogginTeacher.ID && p.Active == true).ToList()
                //                       join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.TypeID == 1 && p.Active == true && p.SchoolID == Authentication.SchoolID && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                //                       select new ISClass
                //                       {
                //                           ID = item2.ID,
                //                           Name = item2.Name
                //                       }).ToList();
                drpClass.DataSource = DB.ISClasses.Where(p => p.SchoolID == SID && p.TypeID == (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
                drpClass.DataTextField = "Name";
                drpClass.DataValueField = "ID";
                drpClass.DataBind();
                drpClass.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpClass.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            if (drpReceiverGroup.SelectedValue == "5")
            {


                //drpClass.DataSource = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Authentication.LogginTeacher.ID && p.Active == true).ToList()
                //                       join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                //                       select new ISClass
                //                       {
                //                           ID = item2.ID,
                //                           Name = item2.Name
                //                       }).ToList();
                drpClass.DataSource =  DB.ISClasses.Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true && p.SchoolID == SID).ToList();
                drpClass.DataTextField = "Name";
                drpClass.DataValueField = "ID";
                drpClass.DataBind();
                drpClass.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpClass.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
            }
            //End code
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            int ClassID = Convert.ToInt32(drpClass.SelectedValue);
            if (ClassID < 1)
            {
                ClassID = 0;
            }
            if (ClassID != 0 && drpReceiverGroup.SelectedValue == "3")
            {
                ParentManagement objParentManagement = new ParentManagement();
                int ClasssID = Convert.ToInt32(drpClass.SelectedValue);
                ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == ClasssID);
                if (ObjCl.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                {
                    ClassManagement objClassManagement = new ClassManagement();
                    drpReceiver.DataSource = objClassManagement.StudentListByOfficeClass(Authentication.SchoolID);//objParentManagement.ParentList(Authentication.LogginTeacher.SchoolID).Where(p => p.ClassID == ClasssID).ToList();
                    drpReceiver.DataValueField = "ID";
                    drpReceiver.DataTextField = "StudentName";
                    drpReceiver.DataBind();
                    drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                    drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
                }
                else
                {
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                    List<MISStudent> objList = new List<MISStudent>();
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        objList = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.ClassID == ClasssID).ToList();
                    }
                    else
                    {
                        string dts = DateTime.Now.ToString("dd/MM/yyyy");
                        objList = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.ClassID == ClasssID && (p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts)).ToList();
                    }
                    drpReceiver.DataSource = objList;//objParentManagement.ParentList(Authentication.LogginTeacher.SchoolID).Where(p => p.ClassID == ClasssID).ToList();
                    drpReceiver.DataValueField = "ID";
                    drpReceiver.DataTextField = "StudentName";
                    drpReceiver.DataBind();
                    drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                    drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });
                }


            }
            else if (ClassID != 0 && drpReceiverGroup.SelectedValue == "4")
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();

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
            else
            {
                ParentManagement objParentManagement = new ParentManagement();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                List<MISStudent> objList = new List<MISStudent>();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objParentManagement.ParentList(Authentication.SchoolID);
                }
                else
                {
                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                    objList = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                }
                drpReceiver.DataSource = objList;
                //drpReceiver.DataSource = objParentManagement.ParentList(Authentication.LogginTeacher.SchoolID);
                drpReceiver.DataValueField = "ID";
                drpReceiver.DataTextField = "StudentName";
                drpReceiver.DataBind();
                drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                drpReceiver.Items.Insert(1, new ListItem { Text = "ALL", Value = "-1" });

            }

        }
    }
}