using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class StudentProfile : System.Web.UI.Page
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

                    BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                    BindDropdown();

                }
            }
            if (IsPostBack && fpUploadLogo.PostedFile != null)
            {
                if (fpUploadLogo.PostedFile.FileName.Length > 0)
                {
                    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
                    if (objStudent != null)
                    {
                        string filename = System.IO.Path.GetFileName(fpUploadLogo.PostedFile.FileName);
                        fpUploadLogo.SaveAs(Server.MapPath("~/Upload/" + filename));
                        objStudent.Photo = "Upload/" + filename;
                        DB.SaveChanges();
                        AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    }
                    BindData(ID);
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

        public int GetSelectedClassId
        {
            get
            {
                int classId = 0;
                int.TryParse(OperationManagement.GetOperation("ClassId"), out classId);
                return classId;
            }
        }

        private void BindDropdown()
        {
            ClassManagement objClassManagement = new ClassManagement();
            ddlClassTo.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => p.Active == true && p.TypeID == (int)EnumsManagement.CLASSTYPE.Standard).ToList();
            ddlClassTo.DataTextField = "Name";
            ddlClassTo.DataValueField = "ID";
            ddlClassTo.DataBind();
            ddlClassTo.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => p.Active == true && p.TypeID == (int)EnumsManagement.CLASSTYPE.Standard).ToList();
            ddlClass.DataTextField = "Name";
            ddlClass.DataValueField = "ID";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }
        public void BindData(int ID)
        {
            Session["PName"] = null;
            Session["SName"] = null;
            ClassManagement objClassManagement = new ClassManagement();
            MISStudent Obj = objClassManagement.GetStudentInfo(ID);
            if (Obj != null)
            {
                //if (Obj.ClassName == "Outside Class")
                //{
                //    lnkReAssign.Visible = false;
                //    btnRemove.Visible = false;
                //    lnkEdit.Visible = false;
                //}
                //litClassName.Text = Obj.ClassName;
                litPPName.Text = Obj.ParantName1;
                litPPEmail.Text = Obj.ParantEmail1;
                litPPPhone.Text = Obj.ParantPhone1;
                litPPRelation.Text = Obj.ParantRelation1;
                litSPName.Text = Obj.ParantName2;
                litSPEmail.Text = Obj.ParantEmail2;
                litSPPhone.Text = Obj.ParantPhone2;
                litSPRelation.Text = Obj.ParantRelation2;
                litClassNm.Text = Obj.ClassName;
                litStudent.Text = Obj.StudentName;
                litIDno.Text = Obj.StudentNo;
                img1.Src = String.Format("{0}{1}", "../", Obj.Photo);
                AID.HRef = String.Format("{0}{1}", "../", Obj.Photo);
                AID.Attributes["data-title"] = Obj.StudentName;
                txtStudentName.Text = Obj.StudentName;
                ddlClass.SelectedValue = Obj.ClassID.ToString();
                txtStudentNo.Text = Obj.StudentNo;
                txtPPName.Text = Obj.ParantName1;
                txtPPEmail.Text = Obj.ParantEmail1;
                txtPPPhone.Text = Obj.ParantPhone1;
                ddlPPRelation.SelectedValue = CommonOperation.GetParentRelation1(Obj.ParantRelation1);
                txtSPName.Text = Obj.ParantName2;
                txtSPEmail.Text = Obj.ParantEmail2;
                txtSPPhone.Text = Obj.ParantPhone2;
                ddlSPRelation.SelectedValue = Obj.ParantRelation2 == "" ? "0" : CommonOperation.GetParentRelation2(Obj.ParantRelation2);
                if (!String.IsNullOrEmpty(Obj.ParantName2) || !String.IsNullOrEmpty(Obj.ParantEmail2))
                {
                    txtSPName.BorderColor = Color.Red;
                    txtSPEmail.BorderColor = Color.Red;
                    txtSPPhone.BorderColor = Color.Red;
                    ddlSPRelation.BorderColor = Color.Red;
                }
                Session["PName"] = Obj.ParantName1 + "(" + Obj.ParantRelation1 + ")";
                if (Obj.ParantName2 != "" && Obj.ParantName2 != null)
                {
                    Session["SName"] = Obj.ParantName2 + "(" + Obj.ParantRelation2 + ")";
                }

                if (Authentication.LogginParent == null)
                {
                    txtPPEmail.Enabled = false;
                    txtSPEmail.Enabled = true;
                    if (!string.IsNullOrEmpty(litSPEmail.Text))
                        txtSPEmail.Enabled = false;
                }

            }
        }

        public bool IsSecondaryEmailBlank()
        {
            return string.IsNullOrEmpty(txtSPEmail.Text);
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            if (objStudent != null)
            {
                string OldClassName = objStudent.ISClass.Name;
                int OldClassID = Convert.ToInt32(objStudent.ClassID);
                int ClassID = Convert.ToInt32(ddlClassTo.SelectedValue);
                if (objStudent.ClassID != ClassID)
                {
                    ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                    ObjReassign.SchoolID = Authentication.SchoolID;
                    ObjReassign.FromClass = objStudent.ClassID;
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
                    ReassignEmailManage(objStudent, OldClassName, OldClassID);
                }
                objStudent.ClassID = Convert.ToInt32(ddlClassTo.SelectedValue);
                objStudent.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                objStudent.ModifyDateTime = DateTime.Now;
                DB.Entry(objStudent).State = EntityState.Modified;
                DB.SaveChanges();
                LogManagement.AddLog("Student Class Assigned Successfully " + "Name : " + objStudent.StudentName + " Document Category : StudentProfile", "Student");
                AlertMessageManagement.ServerMessage(Page, "Student Class Assigned Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
            {
                IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != ID && a.StudentNo == txtStudentNo.Text && a.SchoolID == Authentication.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                if (obj2.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Number Must Be Unique", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == txtStudentName.Text.ToLower() &&
                (a.ParantEmail1.ToLower() == txtPPEmail.Text.ToLower() || a.ParantEmail2.ToLower() == txtPPEmail.Text.ToLower())
                && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Primary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (txtSPEmail.Text.Length > 0 && DB.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == txtStudentName.Text.ToLower()
               && (a.ParantEmail2.ToLower() == txtSPEmail.Text.ToLower() || a.ParantEmail1.ToLower() == txtSPEmail.Text.ToLower())
               && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Secondary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantName1 != txtPPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantName2 != txtSPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                ////else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantRelation1 != ddlPPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantRelation1 != (ddlPPRelation.SelectedValue.ToString() == "0" ? "" : ddlPPRelation.SelectedValue.ToString()) && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                ////else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantRelation2 != ddlSPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantRelation2 != (ddlSPRelation.SelectedValue.ToString() == "0" ? "" : ddlSPRelation.SelectedValue.ToString()) && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantPhone1 != txtPPPhone.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantPhone2 != txtSPPhone.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                else
                {
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                    if (objStudent != null)
                    {
                        if (objStudent.ParantEmail1.ToLower() != txtPPEmail.Text.ToLower())
                        {
                            ///this indicate that student primary parent changed or removed.
                            InsertArchiveParentDetail(objStudent);
                        }

                        ///this indicate that student secondary parent changes or removed. Here there are two possibility
                        if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != txtPPEmail.Text.ToLower())
                        {
                            InsertArchiveParentDetail(objStudent, false);
                        }
                        else if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != txtSPEmail.Text.ToLower() &&
                            objStudent.ParantEmail2.ToLower() != txtPPEmail.Text.ToLower())
                        {
                            InsertArchiveParentDetail(objStudent, false);
                        }

                        objStudent.StudentName = txtStudentName.Text;
                        int ClassID = Convert.ToInt32(ddlClass.SelectedValue);
                        if (objStudent.ClassID != ClassID)
                        {
                            ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                            ObjReassign.SchoolID = Authentication.SchoolID;
                            ObjReassign.FromClass = objStudent.ClassID;
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
                        }
                        objStudent.ClassID = Convert.ToInt32(ddlClass.SelectedValue);
                        objStudent.StudentNo = txtStudentNo.Text;
                        objStudent.ParantName1 = txtPPName.Text;
                        if (objStudent.ParantEmail1 != txtPPEmail.Text)
                        {
                            string Message = "";
                            ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID && p.Deleted == true);
                            if (txtPPEmail.Text != "")
                            {
                                Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your E-Mail has been changed to <br/><br/> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", txtPPName.Text, txtPPEmail.Text, EncryptionHelper.Decrypt(objStudent.ParantPassword1), objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                _EmailManagement.SendEmail(objStudent.ParantEmail1, "Your Account Email Changed", Message);
                            }
                            objStudent.ParantEmail1 = txtPPEmail.Text;
                        }
                        else
                        {
                            objStudent.ParantEmail1 = txtPPEmail.Text;
                        }
                        //objStudent.ParantEmail1 = txtPPEmail.Text;
                        objStudent.ParantPhone1 = txtPPPhone.Text;
                        objStudent.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                        objStudent.ParantName2 = txtSPName.Text;
                        if (objStudent.ParantEmail2 != txtSPEmail.Text)
                        {
                            string Message = "";
                            ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID && p.Deleted == true);
                            if (txtSPEmail.Text != "" && !String.IsNullOrEmpty(objStudent.ParantEmail2))
                            {
                                Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your E-Mail has been changed to <br/><br/> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", txtSPName.Text, txtSPEmail.Text, EncryptionHelper.Decrypt(objStudent.ParantPassword2), objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                _EmailManagement.SendEmail(objStudent.ParantEmail2, "Your Account Email Changed", Message);
                            }
                            objStudent.ParantEmail2 = txtSPEmail.Text;
                        }
                        else
                        {
                            objStudent.ParantEmail2 = txtSPEmail.Text;
                        }
                        //objStudent.ParantEmail2 = txtSPEmail.Text;
                        objStudent.ParantPhone2 = txtSPPhone.Text;
                        objStudent.ParantRelation2 = ddlSPRelation.SelectedValue != "0" ? ddlSPRelation.SelectedItem.Text : "";
                        objStudent.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                        objStudent.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();

                        string Names = Session["PName"] != null ? Convert.ToString(Session["PName"]) : "";
                        ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == Names && p.Active == true && p.Deleted == true);
                        if (objPicker != null)
                        {
                            objPicker.SchoolID = Authentication.SchoolID;
                            objPicker.ParentID = ID;
                            objPicker.StudentID = ID;
                            objPicker.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                            objPicker.Email = txtPPEmail.Text;
                            objPicker.Phone = txtPPPhone.Text;
                            objPicker.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                            objPicker.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                        }
                        if (txtSPName.Text != "" && txtSPEmail.Text != "" && ddlSPRelation.SelectedValue != "0")
                        {
                            string NameSecond = Session["SName"] != null ? Session["SName"].ToString() : "";
                            ISPicker objPickers = DB.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == NameSecond && p.Deleted == true);
                            if (objPickers != null)
                            {
                                objPickers.SchoolID = Authentication.SchoolID;
                                objPickers.ParentID = ID;
                                objPickers.StudentID = ID;
                                objPickers.FirstName = txtSPName.Text + "(" + ddlSPRelation.SelectedItem.Text + ")";
                                objPickers.Email = txtSPEmail.Text;
                                objPickers.Phone = txtSPPhone.Text;
                                objPickers.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                                objPickers.ModifyDateTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                            else
                            {
                                ISPicker objPickers1 = new ISPicker();
                                objPickers1.SchoolID = Authentication.SchoolID;
                                objPickers1.ParentID = ID;
                                objPickers1.StudentID = ID;
                                objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                objPickers1.FirstName = txtSPName.Text + "(" + ddlSPRelation.SelectedItem.Text + ")";
                                objPickers1.Photo = GetExistingPickerImage(txtSPEmail.Text);
                                objPickers1.Email = txtSPEmail.Text;
                                objPickers1.Phone = txtSPPhone.Text;
                                objPickers1.OneOffPickerFlag = false;
                                objPickers1.ActiveStatus = "Active";
                                objPickers1.Active = true;
                                objPickers1.Deleted = true;
                                objPickers1.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                                objPickers1.CreatedDateTime = DateTime.Now;
                                DB.ISPickers.Add(objPickers1);
                                DB.SaveChanges();
                                if (DB.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                {
                                    ISPickerAssignment objAssigns = new ISPickerAssignment();
                                    objAssigns.PickerId = objPickers1.ID;
                                    objAssigns.StudentID = ID;
                                    objAssigns.RemoveChildStatus = 0;
                                    DB.ISPickerAssignments.Add(objAssigns);
                                    DB.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            string NameDelete = Session["SName"] != null ? Session["SName"].ToString() : "";
                            ISPicker objPickers = DB.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && p.SchoolID == Authentication.SchoolID && p.FirstName == NameDelete);
                            if (objPickers != null)
                            {
                                objPickers.Active = false;
                                objPickers.Deleted = false;
                                objPickers.DeletedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                                objPickers.DeletedDateTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                        try
                        {
                            EmailManage(objStudent);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogManagement.AddLog(ex);
                        }

                        LogManagement.AddLog("Student Updated Successfully " + "Name : " + objStudent.StudentName + " Document Category : StudentProfile", "Student");
                        AlertMessageManagement.ServerMessage(Page, "Student Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    }
                }
            }
            else
            {
                IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != ID && a.StudentNo == txtStudentNo.Text && a.SchoolID == Authentication.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                if (obj2.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Number Must Be Unique", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == txtStudentName.Text.ToLower() &&
                (a.ParantEmail1.ToLower() == txtPPEmail.Text.ToLower() || a.ParantEmail2.ToLower() == txtPPEmail.Text.ToLower())
                && a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Primary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (txtSPEmail.Text.Length > 0 && DB.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == txtStudentName.Text.ToLower() &&
                (a.ParantEmail2.ToLower() == txtSPEmail.Text.ToLower() || a.ParantEmail1.ToLower() == txtSPEmail.Text.ToLower()) &&
                a.SchoolID == Authentication.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && a.Active == true && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Name With Secondary Parent Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantName1 != txtPPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantName2 != txtSPName.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Name. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantRelation1 != ddlPPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantRelation2 != ddlSPRelation.SelectedValue && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Parent Relationship. Please confirm the relationship from parent before you proceed", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantPhone1 != txtPPPhone.Text && a.StartDate == (DateTime?)null && a.ParantEmail1 == txtPPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Primary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                //else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantPhone2 != txtSPPhone.Text && a.StartDate == (DateTime?)null && a.ParantEmail2 == txtSPEmail.Text && a.Active == true && a.Deleted == true).ToList().Count > 0)
                //{
                //    AlertMessageManagement.ServerMessage(Page, "Secondary Parent Email has already been setup with a different Phone Number. Accept the Existing Name, Confirm the correct name from parent or Accept this new name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                else
                {
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                    if (objStudent != null)
                    {
                        if (objStudent.ParantEmail1.ToLower() != txtPPEmail.Text.ToLower())
                        {
                            ///this indicate that student primary parent changed or removed.
                            InsertArchiveParentDetail(objStudent);
                        }

                        ///this indicate that student secondary parent changes or removed. Here there are two possibility
                        if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != txtPPEmail.Text.ToLower())
                        {
                            InsertArchiveParentDetail(objStudent, false);
                        }
                        else if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != txtSPEmail.Text.ToLower() &&
                            objStudent.ParantEmail2.ToLower() != txtPPEmail.Text.ToLower())
                        {
                            InsertArchiveParentDetail(objStudent, false);
                        }

                        objStudent.StudentName = txtStudentName.Text;
                        int ClassID = Convert.ToInt32(ddlClass.SelectedValue);
                        if (objStudent.ClassID != ClassID)
                        {
                            ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                            ObjReassign.SchoolID = Authentication.SchoolID;
                            ObjReassign.FromClass = objStudent.ClassID;
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
                        }
                        objStudent.ClassID = Convert.ToInt32(ddlClass.SelectedValue);
                        objStudent.StudentNo = txtStudentNo.Text;
                        objStudent.ParantName1 = txtPPName.Text;
                        if (objStudent.ParantEmail1 != txtPPEmail.Text)
                        {
                            string Message = "";
                            ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID && p.Deleted == true);
                            if (txtPPEmail.Text != "")
                            {
                                Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your E-Mail has been changed to <br/><br/> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", txtPPName.Text, txtPPEmail.Text, EncryptionHelper.Decrypt(objStudent.ParantPassword1), objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                _EmailManagement.SendEmail(objStudent.ParantEmail1, "Your Account Email Changed", Message);
                            }
                            objStudent.ParantEmail1 = txtPPEmail.Text;
                        }
                        else
                        {
                            objStudent.ParantEmail1 = txtPPEmail.Text;
                        }
                        objStudent.ParantPhone1 = txtPPPhone.Text;
                        objStudent.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                        objStudent.ParantRelation1 = ddlPPRelation.SelectedItem.Text;
                        objStudent.ParantName2 = txtSPName.Text;
                        if (objStudent.ParantEmail2 != txtSPEmail.Text)
                        {
                            string Message = "";
                            ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID && p.Deleted == true);
                            if (txtSPEmail.Text != "" && !String.IsNullOrEmpty(objStudent.ParantEmail2))
                            {
                                Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your E-Mail has been changed to <br/><br/> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", txtSPName.Text, txtSPEmail.Text, EncryptionHelper.Decrypt(objStudent.ParantPassword2), objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                _EmailManagement.SendEmail(objStudent.ParantEmail2, "Your Account Email Changed", Message);
                            }
                            objStudent.ParantEmail2 = txtSPEmail.Text;
                        }
                        else
                        {
                            objStudent.ParantEmail2 = txtSPEmail.Text;
                        }
                        objStudent.ParantPhone2 = txtSPPhone.Text;
                        objStudent.ParantRelation2 = ddlSPRelation.SelectedValue != "0" ? ddlSPRelation.SelectedItem.Text : "";
                        objStudent.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                        objStudent.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();

                        string Names = Session["PName"] != null ? Convert.ToString(Session["PName"]) : "";
                        ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == Names && p.Active == true && p.Deleted == true);
                        if (objPicker != null)
                        {
                            objPicker.SchoolID = Authentication.SchoolID;
                            objPicker.ParentID = ID;
                            objPicker.StudentID = ID;
                            objPicker.FirstName = txtPPName.Text + "(" + ddlPPRelation.SelectedItem.Text + ")";
                            objPicker.Email = txtPPEmail.Text;
                            objPicker.Phone = txtPPPhone.Text;
                            objPicker.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                            objPicker.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                        }
                        if (txtSPName.Text != "" && txtSPEmail.Text != "" && ddlSPRelation.SelectedValue != "0")
                        {
                            string NameSecond = Session["SName"] != null ? Session["SName"].ToString() : "";
                            ISPicker objPickers = DB.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == NameSecond && p.Deleted == true);
                            if (objPickers != null)
                            {
                                objPickers.SchoolID = Authentication.SchoolID;
                                objPickers.ParentID = ID;
                                objPickers.StudentID = ID;
                                objPickers.FirstName = txtSPName.Text + "(" + ddlSPRelation.SelectedItem.Text + ")";
                                objPickers.Email = txtSPEmail.Text;
                                objPickers.Phone = txtSPPhone.Text;
                                objPickers.ModifyBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                                objPickers.ModifyDateTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                            else
                            {
                                ISPicker objPickers1 = new ISPicker();
                                objPickers1.SchoolID = Authentication.SchoolID;
                                objPickers1.ParentID = ID;
                                objPickers1.StudentID = ID;
                                objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                objPickers1.FirstName = txtSPName.Text + "(" + ddlSPRelation.SelectedItem.Text + ")";
                                objPickers1.Photo = GetExistingPickerImage(txtSPEmail.Text);
                                objPickers1.Email = txtSPEmail.Text;
                                objPickers1.Phone = txtSPPhone.Text;
                                objPickers1.OneOffPickerFlag = false;
                                objPickers1.ActiveStatus = "Active";
                                objPickers1.Active = true;
                                objPickers1.Deleted = true;
                                objPickers1.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                                objPickers1.CreatedDateTime = DateTime.Now;
                                DB.ISPickers.Add(objPickers1);
                                DB.SaveChanges();
                                if (DB.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                {
                                    ISPickerAssignment objAssigns = new ISPickerAssignment();
                                    objAssigns.PickerId = objPickers1.ID;
                                    objAssigns.StudentID = ID;
                                    objAssigns.RemoveChildStatus = 0;
                                    DB.ISPickerAssignments.Add(objAssigns);
                                    DB.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            string NameDelete = Session["SName"] != null ? Session["SName"].ToString() : "";
                            ISPicker objPickers = DB.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && p.SchoolID == Authentication.SchoolID && p.FirstName == NameDelete);
                            if (objPickers != null)
                            {
                                objPickers.Active = false;
                                objPickers.Deleted = false;
                                objPickers.DeletedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                                objPickers.DeletedDateTime = DateTime.Now;
                                DB.SaveChanges();
                            }
                        }
                        try
                        {
                            EmailManage(objStudent);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogManagement.AddLog(ex);
                        }
                        LogManagement.AddLog("Student Updated Successfully " + "Name : " + objStudent.StudentName + " Document Category : StudentProfile", "Student");
                        AlertMessageManagement.ServerMessage(Page, "Student Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    }
                }
            }
            BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
        }

        private void InsertArchiveParentDetail(ISStudent iSStudent, bool isPrimaryParent = true)
        {
            BckupParent bckupParent = new BckupParent();

            bckupParent.Name = isPrimaryParent ? iSStudent.ParantName1 : iSStudent.ParantName2;
            bckupParent.StudentId = iSStudent.ID;
            bckupParent.ParentId = iSStudent.ID;
            bckupParent.Email = isPrimaryParent ? iSStudent.ParantEmail1 : iSStudent.ParantEmail2;
            bckupParent.Relationship = isPrimaryParent ? iSStudent.ParantRelation1 : iSStudent.ParantRelation2;
            bckupParent.Phone = isPrimaryParent ? iSStudent.ParantPhone1 : iSStudent.ParantPhone2;
            bckupParent.DeletedDate = DateTime.Now;
            bckupParent.WhoDeleted = Authentication.LogginTeacher != null ? "Teacher" : "School";
            DB.BckupParents.Add(bckupParent);
            DB.SaveChanges();
        }
        public string GetExistingPickerImage(string emailId)
        {
            string photo = "Upload/user.jpg";

            ISPicker iSPicker = DB.ISPickers.FirstOrDefault(r => r.Email.ToLower() == emailId.ToLower() && r.Deleted == true && r.Active == true);

            if (iSPicker != null)
                photo = iSPicker.Photo;

            return photo;
        }

        public void EmailManage(ISStudent _Student)
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
                                 Dear " + _Student.ParantName1 + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Changes made to student account " + _Student.StudentName + " by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Student records on the database is being updated with the current information. Please check your records and validate changes that have been made.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Any queries  should be directed to the School Administrator
                                   </td>
                                </tr>
                                </table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                PP1body += reader.ReadToEnd();
            }
            PP1body = PP1body.Replace("{Body}", tblPP1body);
            _EmailManagement.SendEmail(_Student.ParantEmail1, "Update Student Account Notification", PP1body);

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
                                    Changes made to student account " + _Student.StudentName + " by " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Student records on the database is being updated with the current information. Please check your records and validate changes that have been made.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Any queries  should be directed to the School Administrator
                                   </td>
                                </tr>
                                </table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP2Body += reader.ReadToEnd();
                }
                PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                _EmailManagement.SendEmail(_Student.ParantEmail2, "Update Student Account Notification", PP2Body);
            }

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            if (objStudent != null)
            {
                objStudent.Active = false;
                objStudent.Deleted = false;
                objStudent.DeletedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                objStudent.DeletedDateTime = DateTime.Now;
                DB.SaveChanges();
                LogManagement.AddLog("Student Deleted Successfully " + "ID : " + ID + " Document Category : StudentProfile", "Student");
                AlertMessageManagement.ServerMessage(Page, "Student Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            Response.Redirect("Class.aspx", false);
        }

        protected void txtSPName_TextChanged(object sender, EventArgs e)
        {
            if (txtSPName.Text != "")
            {
                Requiredfieldvalidator10.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator3.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator12.Enabled = true;
                txtSPPhone.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhone.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator10.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator3.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator12.Enabled = false;
                    txtSPPhone.BorderColor = Color.LightGray;

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
                Requiredfieldvalidator10.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator3.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator12.Enabled = true;
                txtSPPhone.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhone.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator10.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator3.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator12.Enabled = false;
                    txtSPPhone.BorderColor = Color.LightGray;

                    RequiredFieldValidator11.Enabled = false;
                    ddlSPRelation.BorderColor = Color.LightGray;
                }
            }
            txtSPPhone.Focus();
        }

        protected void txtSPPhone_TextChanged(object sender, EventArgs e)
        {
            if (txtSPPhone.Text != "")
            {
                Requiredfieldvalidator10.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator3.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator12.Enabled = true;
                txtSPPhone.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhone.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator10.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator3.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator12.Enabled = false;
                    txtSPPhone.BorderColor = Color.LightGray;

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
                Requiredfieldvalidator10.Enabled = true;
                txtSPName.BorderColor = Color.Red;

                Requiredfieldvalidator9.Enabled = true;
                RegularExpressionValidator3.Enabled = true;
                txtSPEmail.BorderColor = Color.Red;

                Requiredfieldvalidator12.Enabled = true;
                txtSPPhone.BorderColor = Color.Red;

                RequiredFieldValidator11.Enabled = true;
                ddlSPRelation.BorderColor = Color.Red;
            }
            else
            {
                if (txtSPName.Text == "" && txtSPEmail.Text == "" && txtSPPhone.Text == "" && ddlSPRelation.SelectedValue == "0")
                {
                    Requiredfieldvalidator10.Enabled = false;
                    txtSPName.BorderColor = Color.LightGray;

                    Requiredfieldvalidator9.Enabled = false;
                    RegularExpressionValidator3.Enabled = false;
                    txtSPEmail.BorderColor = Color.LightGray;

                    Requiredfieldvalidator12.Enabled = false;
                    txtSPPhone.BorderColor = Color.LightGray;

                    RequiredFieldValidator11.Enabled = false;
                    ddlSPRelation.BorderColor = Color.LightGray;
                }
            }

            ddlSPRelation.Focus();
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

        protected void CompareDLValues(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (ddlSPRelation.SelectedIndex == ddlPPRelation.SelectedIndex)
            {
                args.IsValid = false;
            }
        }
    }
}