using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class StudentClass : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {
                if (OperationManagement.GetOperation("ID") != "")
                {
                    Session["ClassId"] = OperationManagement.GetOperation("ID");
                    BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), "");
                    BindDropdown();
                    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
                    if (ObjClass != null)
                    {
                        if (ObjClass.Name.Contains("Outside"))
                        {
                            btnAdd.Visible = false;
                        }
                    }
                }
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageStudentFlag == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void BindDropdown()
        {
            ClassManagement objClassManagement = new ClassManagement();
            ddlClassTo.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => p.Active == true && p.TypeID == 1).ToList();
            ddlClassTo.DataTextField = "Name";
            ddlClassTo.DataValueField = "ID";
            ddlClassTo.DataBind();
            ddlClassTo.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }

        public void BindData(int ID, string StudentName)
        {
            ClassManagement objClassManagement = new ClassManagement();
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
            if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
            {
                ISClass objClass = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
                var Obj = objClassManagement.StudentListByClass(Authentication.SchoolID, ID);
                if (StudentName != "")
                {
                    Obj = Obj.Where(p => p.StudentName.Trim().ToLower().Contains(StudentName.Trim().ToLower())).ToList();
                }
                lstClasses.DataSource = Obj.ToList();
                lstClasses.DataBind();
                if (objClass != null)
                {
                    litClassName.Text = objClass.Name + " Students";
                    litClassYear.Text = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == objClass.Year) != null ? DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == objClass.Year).Year : "";
                    litTotalStudent.Text = objClassManagement.getStudentCount(objClass.ID, objClass.SchoolID).ToString();
                }
                if (Obj.Count <= 0)
                {
                    lbl.Text = "There are No Student in " + objClass.Name;
                }
                else
                {
                    lbl.Text = "";
                }
            }
            else
            {
                ISClass objClass = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
                if (objClass.Name.Contains("Outside"))
                {
                    var Obj = objClassManagement.StudentListByExtClass(Authentication.SchoolID, ID);
                    if (StudentName != "")
                    {
                        Obj = Obj.Where(p => p.StudentName.Trim().ToLower().Contains(StudentName.Trim().ToLower())).ToList();
                    }
                    lstClasses.DataSource = Obj.ToList();
                    lstClasses.DataBind();
                    if (objClass != null)
                    {
                        litClassName.Text = objClass.Name + " Students";
                        litClassYear.Text = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == objClass.Year) != null ? DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == objClass.Year).Year : "";
                        litTotalStudent.Text = objClassManagement.getExternalStudentCount(objClass.SchoolID).ToString();
                    }
                    if (Obj.Count <= 0)
                    {
                        lbl.Text = "There are No Student in " + objClass.Name;
                    }
                    else
                    {
                        lbl.Text = "";
                    }
                }
                else
                {
                    var Obj = objClassManagement.StudentListByClass(Authentication.SchoolID, ID);
                    if (StudentName != "")
                    {
                        Obj = Obj.Where(p => p.StudentName.Trim().ToLower().Contains(StudentName.Trim().ToLower())).ToList();
                    }
                    lstClasses.DataSource = Obj.ToList();
                    lstClasses.DataBind();
                    if (objClass != null)
                    {
                        litClassName.Text = objClass.Name + " Students";
                        litClassYear.Text = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == objClass.Year) != null ? DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == objClass.Year).Year : "";
                        litTotalStudent.Text = objClassManagement.getStudentCount(objClass.ID, objClass.SchoolID).ToString();
                    }
                    if (Obj.Count <= 0)
                    {
                        lbl.Text = "There are No Student in " + objClass.Name;
                    }
                    else
                    {
                        lbl.Text = "";
                    }
                }
            }
        }

        protected void lstClasses_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnRemove")
            {
                try
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                    if (obj != null)
                    {
                        obj.Active = false;
                        obj.Deleted = false;
                        obj.DeletedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                        obj.DeletedDateTime = DateTime.Now;
                        DB.SaveChanges();
                        RemoveEmailManage(obj);
                        LogManagement.AddLog("Student Deleted Successfully " + "Name : " + obj.StudentName + " Document Category : StudentClass", "Student");
                        AlertMessageManagement.ServerMessage(Page, "Student Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.info);
                        BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), txtStudentNames.Text);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogManagement.AddLog(ex);
                }
            }
        }

        protected void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(HID.Value);
            ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            if (obj != null)
            {
                string OldClassName = obj.ISClass.Name;
                int OldClassID = Convert.ToInt32(obj.ClassID);
                int ClassID = Convert.ToInt32(ddlClassTo.SelectedValue);
                if (obj.ClassID != ClassID)
                {
                    ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                    ObjReassign.SchoolID = Authentication.SchoolID;
                    ObjReassign.FromClass = obj.ClassID;
                    ObjReassign.ToClass = ClassID;
                    ObjReassign.Date = DateTime.Now;
                    ObjReassign.StduentID = ID;
                    ObjReassign.Active = true;
                    ObjReassign.Deleted = true;
                    ObjReassign.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                    ObjReassign.CreatedByType = Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                    ObjReassign.CreatedDateTime = DateTime.Now;
                    DB.ISStudentReassignHistories.Add(ObjReassign);
                    DB.SaveChanges();
                    ReassignEmailManage(obj, OldClassName, OldClassID);
                }
                obj.ClassID = ClassID; //Convert.ToInt32(ddlClassTo.SelectedValue);
                obj.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                obj.ModifyDateTime = DateTime.Now;
                DB.SaveChanges();

                LogManagement.AddLog("Student Class ReAssigned Successfully " + "Name : " + obj.StudentName + " Document Category : StudentClass", "Student");
                AlertMessageManagement.ServerMessage(Page, "Student Class ReAssigned Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                Clear();
                BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), txtStudentNames.Text);
            }

        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            EmailManagement objEmailManagement = new EmailManagement();
            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
            {
                IList<ISStudent> objStudent = DB.ISStudents.Where(a => a.StudentNo == txtIdNo.Text && a.SchoolID == Authentication.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                if (objStudent.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Number Must Be Unique", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISStudents.Where(a => a.StudentName.ToLower() == txtStudentName.Text.ToLower()
                    && (a.ParantEmail1.ToLower() == txtPPEmai.Text.ToLower() || a.ParantEmail2.ToLower() == txtPPEmai.Text.ToLower())
                    && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Primary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (txtSPEmail.Text.Length > 0 && DB.ISStudents.Where(a => a.StudentName.ToLower() == txtStudentName.Text.ToLower()
                    && (a.ParantEmail2.ToLower() == txtSPEmail.Text.ToLower() || a.ParantEmail1.ToLower() == txtSPEmail.Text.ToLower())
                    && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Secondary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }


                // else if (DB.ISStudents.Where(a => a.ParantName1 != txtPPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Parent Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ParantName2 != txtSPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                /* done by maharshi for allowing 1 perent to add 2,3 or more childs

             //else if (DB.ISStudents.Where(a => a.ParantRelation1 != ddlPPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
            else if (DB.ISStudents.Where(a => a.ParantRelation1 != (ddlPPRelation.SelectedValue.ToString() == "0" ? "" : ddlPPRelation.SelectedValue.ToString()) && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
             {
                 //Maharshi needds Edit here
                 AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
             }
             //  else if (DB.ISStudents.Where(a => a.ParantRelation2 != ddlSPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
             else if (DB.ISStudents.Where(a => a.ParantRelation2 != (ddlSPRelation.SelectedValue.ToString()=="0"?"": ddlSPRelation.SelectedValue.ToString()) && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
             {
                 AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
             }
             else if (DB.ISStudents.Where(a => a.ParantPhone1 != txtPPPhoneNo.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
             {
                 AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
             }
             else if (DB.ISStudents.Where(a => a.ParantPhone2 != txtSPPhoneNo.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
             {
                 AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
             } */
                else
                {
                    ISStudent obj = new ISStudent();
                    obj.StudentName = txtStudentName.Text;
                    obj.ClassID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    obj.StudentNo = txtIdNo.Text;
                    obj.SchoolID = Authentication.SchoolID;
                    obj.Photo = "Upload/user.jpg";
                    obj.ParantName1 = txtPPName.Text;
                    obj.ParantEmail1 = txtPPEmai.Text;
                    obj.ParantPhone1 = txtPPPhoneNo.Text;
                    obj.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                    obj.ParantName2 = txtSPName.Text;
                    obj.ParantEmail2 = txtSPEmail.Text;
                    obj.ParantPhone2 = txtSPPhoneNo.Text;
                    obj.ParantRelation2 = ddlSPRelation.SelectedValue != "0" ? ddlSPRelation.SelectedItem.Text : "";
                    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                    string Message = "";
                    if (txtPPEmai.Text != "")
                    {
                        var isEmailIdPresent = DB.ISStudents.Where(r => r.ParantEmail1.ToLower() == txtPPEmai.Text.Trim().ToLower() && r.Active == true && r.Deleted == true).FirstOrDefault();

                        obj.ParantPassword1 = isEmailIdPresent == null ? EncryptionHelper.Encrypt(ParantPassword1) : isEmailIdPresent.ParantPassword1;
                    }
                    if (txtSPEmail.Text != "")
                    {
                        var isEmailIdPresent = DB.ISStudents.Where(r => r.ParantEmail2.ToLower() == txtSPEmail.Text.Trim().ToLower() && r.Active == true && r.Deleted == true).FirstOrDefault();

                        obj.ParantPassword2 = isEmailIdPresent == null ? EncryptionHelper.Encrypt(ParantPassword2) : isEmailIdPresent.ParantPassword2;
                    }
                    obj.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                    obj.CreatedDateTime = DateTime.Now;
                    obj.ISImageEmail = false;
                    obj.SentMailDate = DateTime.Now;
                    obj.Active = true;
                    obj.Deleted = true;
                    obj.Out = 0;
                    obj.Outbit = false;
                    DB.ISStudents.Add(obj);
                    DB.SaveChanges();

                    ISPicker objPicker = new ISPicker();
                    objPicker.SchoolID = Authentication.SchoolID;
                    objPicker.ParentID = obj.ID;
                    objPicker.StudentID = obj.ID;
                    objPicker.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                    objPicker.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                    objPicker.Photo = GetExistingPickerImage(txtPPEmai.Text);
                    objPicker.Email = txtPPEmai.Text;
                    objPicker.Phone = txtPPPhoneNo.Text;
                    objPicker.OneOffPickerFlag = false;
                    objPicker.ActiveStatus = "Active";
                    objPicker.Active = true;
                    objPicker.Deleted = true;
                    objPicker.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                    objPicker.CreatedDateTime = DateTime.Now;
                    DB.ISPickers.Add(objPicker);
                    DB.SaveChanges();
                    if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                    {
                        ISPickerAssignment objAssign = new ISPickerAssignment();
                        objAssign.PickerId = objPicker.ID;
                        objAssign.StudentID = obj.ID;
                        objAssign.RemoveChildStatus = 0;
                        DB.ISPickerAssignments.Add(objAssign);
                        DB.SaveChanges();
                    }
                    if (txtSPName.Text != "" && txtSPEmail.Text != "" && ddlSPRelation.SelectedValue != "0")
                    {
                        ISPicker objPickers = new ISPicker();
                        objPickers.SchoolID = Authentication.SchoolID;
                        objPickers.ParentID = obj.ID;
                        objPickers.StudentID = obj.ID;
                        objPickers.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                        objPickers.FirstName = txtSPName.Text + "(" + ddlSPRelation.SelectedItem.Text + ")";
                        objPickers.Photo = GetExistingPickerImage(txtSPEmail.Text);
                        objPickers.Email = txtSPEmail.Text;
                        objPickers.Phone = txtSPPhoneNo.Text;
                        objPickers.OneOffPickerFlag = false;
                        objPickers.ActiveStatus = "Active";
                        objPickers.Active = true;
                        objPickers.Deleted = true;
                        objPickers.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                        objPickers.CreatedDateTime = DateTime.Now;
                        DB.ISPickers.Add(objPickers);
                        DB.SaveChanges();
                        if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                        {
                            ISPickerAssignment objAssigns = new ISPickerAssignment();
                            objAssigns.PickerId = objPickers.ID;
                            objAssigns.StudentID = obj.ID;
                            objAssigns.RemoveChildStatus = 0;
                            DB.ISPickerAssignments.Add(objAssigns);
                            DB.SaveChanges();
                        }
                    }
                    try
                    {
                        CommonOperation.EmailManage(obj);
                        //EmailManage(obj);
                        //NoImageEmailManage(obj);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }
                    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    LogManagement.AddLog("Student Saved Successfully " + "Name : " + obj.StudentName + "Document Category : StudentClass", "Student");
                    AlertMessageManagement.ServerMessage(Page, "Student Saved Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    BindData(ID, txtStudentNames.Text);
                }

            }
            else
            {
                IList<ISStudent> objStudent = DB.ISStudents.Where(a => a.StudentNo == txtIdNo.Text && a.SchoolID == Authentication.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                if (objStudent.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Number Must Be Unique", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISStudents.Where(a => a.StudentName.ToLower() == txtStudentName.Text.ToLower() && 
                (a.ParantEmail1.ToLower() == txtPPEmai.Text.ToLower() || a.ParantEmail2.ToLower() == txtPPEmai.Text.ToLower()) && 
                a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool 
                && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Primary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (txtSPEmail.Text.Length > 0 && DB.ISStudents.Where(a => a.StudentName.ToLower() == txtStudentName.Text.ToLower() && 
                (a.ParantEmail2.ToLower() == txtSPEmail.Text.ToLower() || a.ParantEmail1.ToLower() == txtSPEmail.Text.ToLower()) && 
                a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && 
                a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Secondary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                //else if (DB.ISStudents.Where(a => a.ParantName1 != txtPPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ParantName2 != txtSPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ParantRelation1 != ddlPPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ParantRelation2 != ddlSPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ParantPhone1 != txtPPPhoneNo.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmai.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ParantPhone2 != txtSPPhoneNo.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                else
                {
                    ISStudent obj = new ISStudent();
                    obj.StudentName = txtStudentName.Text;
                    obj.ClassID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    obj.StudentNo = txtIdNo.Text;
                    obj.SchoolID = Authentication.SchoolID;
                    obj.Photo = "Upload/user.jpg";
                    obj.ParantName1 = txtPPName.Text;
                    obj.ParantEmail1 = txtPPEmai.Text;
                    obj.ParantPhone1 = txtPPPhoneNo.Text;
                    obj.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                    obj.ParantName2 = txtSPName.Text;
                    obj.ParantEmail2 = txtSPEmail.Text;
                    obj.ParantPhone2 = txtSPPhoneNo.Text;
                    obj.ParantRelation2 = ddlSPRelation.SelectedValue != "0" ? ddlSPRelation.SelectedItem.Text : "";
                    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                    string Message = "";
                    if (txtPPEmai.Text != "")
                    {
                        var isEmailIdPresent = DB.ISStudents.Where(r => r.ParantEmail1.ToLower() == txtPPEmai.Text.Trim().ToLower() && r.Active == true && r.Deleted == true).FirstOrDefault();

                        obj.ParantPassword1 = isEmailIdPresent == null ? EncryptionHelper.Encrypt(ParantPassword1) : isEmailIdPresent.ParantPassword1;
                    }
                    if (txtSPEmail.Text != "")
                    {
                        var isEmailIdPresent = DB.ISStudents.Where(r => r.ParantEmail2.ToLower() == txtSPEmail.Text.Trim().ToLower() && r.Active == true && r.Deleted == true).FirstOrDefault();

                        obj.ParantPassword2 = isEmailIdPresent == null ? EncryptionHelper.Encrypt(ParantPassword2) : isEmailIdPresent.ParantPassword2;
                    }
                    obj.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                    obj.CreatedDateTime = DateTime.Now;
                    obj.Active = true;
                    obj.Deleted = true;
                    obj.Out = 0;
                    obj.Outbit = false;
                    DB.ISStudents.Add(obj);
                    DB.SaveChanges();

                    ISPicker objPicker = new ISPicker();
                    objPicker.SchoolID = Authentication.SchoolID;
                    objPicker.ParentID = obj.ID;
                    objPicker.StudentID = obj.ID;
                    objPicker.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                    objPicker.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                    objPicker.Photo = GetExistingPickerImage(txtPPEmai.Text);
                    objPicker.Email = txtPPEmai.Text;
                    objPicker.Phone = txtPPPhoneNo.Text;
                    objPicker.OneOffPickerFlag = false;
                    objPicker.ActiveStatus = "Active";
                    objPicker.Active = true;
                    objPicker.Deleted = true;
                    objPicker.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                    objPicker.CreatedDateTime = DateTime.Now;
                    DB.ISPickers.Add(objPicker);
                    DB.SaveChanges();
                    if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                    {
                        ISPickerAssignment objAssign = new ISPickerAssignment();
                        objAssign.PickerId = objPicker.ID;
                        objAssign.StudentID = obj.ID;
                        objAssign.RemoveChildStatus = 0;
                        DB.ISPickerAssignments.Add(objAssign);
                        DB.SaveChanges();
                    }
                    if (txtSPName.Text != "" && txtSPEmail.Text != "" && ddlSPRelation.SelectedValue != "0")
                    {
                        ISPicker objPickers = new ISPicker();
                        objPickers.SchoolID = Authentication.SchoolID;
                        objPickers.ParentID = obj.ID;
                        objPickers.StudentID = obj.ID;
                        objPickers.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                        objPickers.FirstName = txtSPName.Text + "(" + ddlSPRelation.SelectedItem.Text + ")";
                        objPickers.Photo = GetExistingPickerImage(txtSPEmail.Text);
                        objPickers.Email = txtSPEmail.Text;
                        objPickers.Phone = txtSPPhoneNo.Text;
                        objPickers.OneOffPickerFlag = false;
                        objPickers.ActiveStatus = "Active";
                        objPickers.Active = true;
                        objPickers.Deleted = true;
                        objPickers.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                        objPickers.CreatedDateTime = DateTime.Now;
                        DB.ISPickers.Add(objPickers);
                        DB.SaveChanges();
                        if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                        {
                            ISPickerAssignment objAssigns = new ISPickerAssignment();
                            objAssigns.PickerId = objPickers.ID;
                            objAssigns.StudentID = obj.ID;
                            objAssigns.RemoveChildStatus = 0;
                            DB.ISPickerAssignments.Add(objAssigns);
                            DB.SaveChanges();
                        }
                    }
                    try
                    {
                        CommonOperation.EmailManage(obj);
                        NoImageEmailManage(obj);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogManagement.AddLog(ex);
                    }

                    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    LogManagement.AddLog("Student Saved Successfully " + "Name : " + obj.StudentName + "Document Category : StudentClass", "Student");
                    AlertMessageManagement.ServerMessage(Page, "Student Saved Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    BindData(ID, txtStudentNames.Text);
                }
            }

            //Clear();
        }

        public string GetExistingPickerImage(string emailId)
        {
            string photo = "Upload/user.jpg";

            ISPicker iSPicker = DB.ISPickers.FirstOrDefault(r => r.Email.ToLower() == emailId.ToLower() && r.Deleted == true && r.Active == true);

            if (iSPicker != null)
                photo = iSPicker.Photo;

            return photo;
        }

        public void NoImageEmailManage(ISStudent _Student)
        {
            string PP1body = String.Empty;
            string PP2Body = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblPP1body = string.Empty;
            string tblPP2Body = string.Empty;
            string tblTeacherBody = string.Empty;
            tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database records show that a child, " + _Student.StudentName + @" has no identifiable image. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for this child.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Failure to do so will result in  you receiving this notification in the future, and the  child pickup process could also become more difficult.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please use the FAQs, videos and guides on your account or contact the School Administrator if you experience any difficulty in carrying out this task.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                PP1body += reader.ReadToEnd();
            }
            PP1body = PP1body.Replace("{Body}", tblPP1body);
            _EmailManagement.SendEmail(_Student.ParantEmail1, "No Child Image", PP1body);

            if (!String.IsNullOrEmpty(_Student.ParantName2) && !String.IsNullOrEmpty(_Student.ParantEmail2))
            {
                tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database records show that a child, " + _Student.StudentName + @" has no identifiable image. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for this child.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Failure to do so will result in  you receiving this notification in the future, and the  child pickup process could also become more difficult.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please use the FAQs, videos and guides on your account or contact the School Administrator if you experience any difficulty in carrying out this task.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP2Body += reader.ReadToEnd();
                }
                PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                _EmailManagement.SendEmail(_Student.ParantEmail2, "No Child Image", PP2Body);
            }
        }
        private void Clear()
        {
            txtStudentName.Text = "";
            txtIdNo.Text = "";
            txtPPName.Text = "";
            txtPPEmai.Text = "";
            txtPPPhoneNo.Text = "";
            ddlPPRelation.SelectedValue = "0";
            txtSPName.Text = "";
            txtSPEmail.Text = "";
            txtSPPhoneNo.Text = "";
            ddlSPRelation.SelectedValue = "0";
            ddlClassTo.SelectedValue = "0";

            Requiredfieldvalidator2.Enabled = false;
            txtSPName.BorderColor = Color.LightGray;
            Requiredfieldvalidator9.Enabled = false;
            RegularExpressionValidator1.Enabled = false;
            txtSPEmail.BorderColor = Color.LightGray;
            Requiredfieldvalidator10.Enabled = false;
            txtSPPhoneNo.BorderColor = Color.LightGray;
            RequiredFieldValidator11.Enabled = false;
            ddlSPRelation.BorderColor = Color.LightGray;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string StudentAssignClass(string ID)
        {
            int IDS = Convert.ToInt32(ID);
            SchoolAppEntities DB = new SchoolAppEntities();

            string ClassID1 = DB.ISStudents.SingleOrDefault(p => p.ID == IDS).ISClass.Name;
            return "{ \"FirstClass\" : \" " + ClassID1 + "\"}";
        }
        public void ReassignEmailManage(ISStudent _Student, string FromClassName, int FromClassID)
        {
            try
            {
                string PP1body = String.Empty;
                string PP2Body = String.Empty;


                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
                string tblPP1body = string.Empty;
                string tblPP2Body = string.Empty;

                tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName1 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student, " + _Student.StudentName + @", class changed from " + FromClassName + @" to " + _Student.ISClass.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP1body += reader.ReadToEnd();
                }
                PP1body = PP1body.Replace("{Body}", tblPP1body);
                _EmailManagement.SendEmail(_Student.ParantEmail1, "Reassign Student Class Notification", PP1body);

                if (!String.IsNullOrEmpty(_Student.ParantName2) && !String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName2 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student, " + _Student.StudentName + @", class changed from " + FromClassName + @" to " + _Student.ISClass.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        PP2Body += reader.ReadToEnd();
                    }
                    PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Reassign Student Class Notification", PP2Body);
                }
                List<ISTeacherClassAssignment> _Assign = DB.ISTeacherClassAssignments.Where(p => p.ClassID == FromClassID && p.ClassID == _Student.ClassID && p.ISTeacher.Deleted == true && p.Deleted == true).ToList();
                foreach (var item in _Assign)
                {
                    string TeacherBody = String.Empty;
                    string tblTeacherBody = string.Empty;
                    tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.ISTeacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student, " + _Student.StudentName + @", class changed from " + FromClassName + @" to " + _Student.ISClass.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        TeacherBody += reader.ReadToEnd();
                    }
                    TeacherBody = TeacherBody.Replace("{Body}", tblTeacherBody);
                    _EmailManagement.SendEmail(item.ISTeacher.Email, "Reassign Student Class Notification", TeacherBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void RemoveEmailManage(ISStudent _Student)
        {
            try
            {
                string PP1body = String.Empty;
                string PP2Body = String.Empty;


                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
                string tblPP1body = string.Empty;
                string tblPP2Body = string.Empty;

                tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName1 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student, " + _Student.StudentName + @", has been removed from " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any queries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP1body += reader.ReadToEnd();
                }
                PP1body = PP1body.Replace("{Body}", tblPP1body);
                _EmailManagement.SendEmail(_Student.ParantEmail1, "Remove Student Notification", PP1body);

                if (!String.IsNullOrEmpty(_Student.ParantName2) && !String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Student.ParantName2 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student, " + _Student.StudentName + @", has been removed from " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any queries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        PP2Body += reader.ReadToEnd();
                    }
                    PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Remove Student Notification", PP2Body);
                }
                List<ISTeacherClassAssignment> _Assign = DB.ISTeacherClassAssignments.Where(p => p.ClassID == _Student.ClassID && p.ISTeacher.Deleted == true && p.Deleted == true).ToList();
                foreach (var item in _Assign)
                {
                    string TeacherBody = String.Empty;
                    string tblTeacherBody = string.Empty;
                    tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.ISTeacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student, " + _Student.StudentName + @", has been removed from " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any queries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        TeacherBody += reader.ReadToEnd();
                    }
                    TeacherBody = TeacherBody.Replace("{Body}", tblTeacherBody);
                    _EmailManagement.SendEmail(item.ISTeacher.Email, "Remove Student Notification", TeacherBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void lstClasses_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstClasses.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), txtStudentNames.Text);
        }

        protected void txtSPName_TextChanged(object sender, EventArgs e)
        {
            if (txtSPName.Text != "")
            {
                Requiredfieldvalidator2.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator10.Enabled = true;
                txtSPPhoneNo.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhoneNo.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator2.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator1.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator10.Enabled = false;
                    txtSPPhoneNo.BorderColor = Color.LightGray;

                    RequiredFieldValidator11.Enabled = false;
                    ddlSPRelation.BorderColor = Color.LightGray;
                }
            }
            txtSPEmail.Focus();
        }

        protected void txtSPEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtSPEmail.Text != "")
            {
                Requiredfieldvalidator2.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator10.Enabled = true;
                txtSPPhoneNo.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhoneNo.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator2.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator1.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator10.Enabled = false;
                    txtSPPhoneNo.BorderColor = Color.LightGray;

                    RequiredFieldValidator11.Enabled = false;
                    ddlSPRelation.BorderColor = Color.LightGray;
                }
            }
            txtSPPhoneNo.Focus();
        }

        protected void txtSPPhoneNo_TextChanged(object sender, EventArgs e)
        {
            if (txtSPPhoneNo.Text != "")
            {
                Requiredfieldvalidator2.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator10.Enabled = true;
                txtSPPhoneNo.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhoneNo.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator2.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator1.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator10.Enabled = false;
                    txtSPPhoneNo.BorderColor = Color.LightGray;

                    RequiredFieldValidator11.Enabled = false;
                    ddlSPRelation.BorderColor = Color.LightGray;
                }
            }
            ddlSPRelation.Focus();
        }

        protected void ddlSPRelation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSPRelation.SelectedValue != "0")
            {
                Requiredfieldvalidator2.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator10.Enabled = true;
                txtSPPhoneNo.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhoneNo.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator2.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator1.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator10.Enabled = false;
                    txtSPPhoneNo.BorderColor = Color.LightGray;

                    RequiredFieldValidator11.Enabled = false;
                    ddlSPRelation.BorderColor = Color.LightGray;
                }
            }
            ddlSPRelation.Focus();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (txtStudentNames.Text != "")
            {
                BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), txtStudentNames.Text);
            }
            else
            {
                BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), "");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtStudentNames.Text = "";
            BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")), "");
        }

        protected void cusCustom_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (e.Value == ddlPPRelation.SelectedValue && e.Value != "Guardian")
                e.IsValid = true;
            else
                e.IsValid = false;
        }
    }

}