using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class TeacherProfile : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        ClassManagement objClassManagement = new ClassManagement();
        TeacherManagement objTeacherManagement = new TeacherManagement();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!Page.IsPostBack)
            {
                if (OperationManagement.GetOperation("ID") != "")
                {
                    BindDropdown();
                    BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
                    //txtEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            if (IsPostBack && fpUploadLogo.PostedFile != null)
            {
                if (fpUploadLogo.PostedFile.FileName.Length > 0)
                {
                    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
                    if (objTeacher != null)
                    {
                        string filename = System.IO.Path.GetFileName(fpUploadLogo.PostedFile.FileName);
                        fpUploadLogo.SaveAs(Server.MapPath("~/Upload/" + filename));
                        objTeacher.Photo = "Upload/" + filename;
                        DB.SaveChanges();
                        ChangeImageEmailManage(objTeacher);
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
        private void BindData(int TeacherID)
        {
            MISTeacher Obj = objTeacherManagement.GetTeacher(TeacherID);
            if (Obj != null)
            {
                litClassName.Text = Obj.ClassName;
                litTeacherName.Text = String.Format("{0} {1}", Obj.Title, Obj.Name);
                litTeacherNo.Text = Obj.TeacherNo;
                litTitle.Text = String.Format("{0} {1}", Obj.Title, Obj.Name);
                litRole.Text = Obj.RoleName;
                litEmail.Text = Obj.Email;
                litPhone.Text = Obj.PhoneNo;
                litEndDate.Text = Obj.Status;

                Image6.ImageUrl = String.Format("{0}{1}", "../", Obj.Photo);
                AID.HRef = String.Format("{0}{1}", "../", Obj.Photo);
                AID.Attributes["data-title"] = Obj.Name;

                ddlTeacherTitle.SelectedValue = Obj.Title;
                txtIdNo.Text = Obj.TeacherNo;
                txtName.Text = Obj.Name;
                List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Active == true && p.Deleted == true).ToList();
                List<ISClass> objClassList = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList();

                ddlClass1.SelectedValue = objList.Count >= 1 ? objList[0].ClassID.ToString() : "0";
                ddlClass2.SelectedValue = objList.Count >= 2 ? objList[1].ClassID.ToString() : "0";
                ddlClass3.SelectedValue = objList.Count >= 3 ? objList[2].ClassID.ToString() : "0";
                ddlClass4.SelectedValue = objList.Count >= 4 ? objList[3].ClassID.ToString() : "0";
                ddlClass5.SelectedValue = objList.Count >= 5 ? objList[4].ClassID.ToString() : "0";

                ddl1st.SelectedValue = objList.Count >= 1 ? objList[0].ClassID.ToString() : "0";
                ddl2nd.SelectedValue = objList.Count >= 2 ? objList[1].ClassID.ToString() : "0";
                ddl3rd.SelectedValue = objList.Count >= 3 ? objList[2].ClassID.ToString() : "0";
                ddl4th.SelectedValue = objList.Count >= 4 ? objList[3].ClassID.ToString() : "0";
                ddl5th.SelectedValue = objList.Count >= 5 ? objList[4].ClassID.ToString() : "0";

                litFirstClass.Text = objList.Count >= 1 ? objClassList.SingleOrDefault(p => p.ID == objList[0].ClassID).Name : "";
                litSecondClass.Text = objList.Count >= 2 ? objClassList.SingleOrDefault(p => p.ID == objList[1].ClassID).Name : "";
                litThirdClass.Text = objList.Count >= 3 ? objClassList.SingleOrDefault(p => p.ID == objList[2].ClassID).Name : "";
                litFourthClass.Text = objList.Count >= 4 ? objClassList.SingleOrDefault(p => p.ID == objList[3].ClassID).Name : "";
                litFifthClass.Text = objList.Count >= 5 ? objClassList.SingleOrDefault(p => p.ID == objList[4].ClassID).Name : "";

                if (ddl1st.SelectedValue == "0") { ddl1st.Visible = false; } else { ddl1st.Visible = true; }
                if (ddl2nd.SelectedValue == "0") { ddl2nd.Visible = false; } else { ddl2nd.Visible = true; }
                if (ddl3rd.SelectedValue == "0") { ddl3rd.Visible = false; } else { ddl3rd.Visible = true; }
                if (ddl4th.SelectedValue == "0") { ddl4th.Visible = false; } else { ddl4th.Visible = true; }
                if (ddl5th.SelectedValue == "0") { ddl5th.Visible = false; } else { ddl5th.Visible = true; }

                ddlRole.SelectedValue = Obj.RoleID.ToString();

                txtEmail.Text = Obj.Email;
                txtPhoneNo.Text = Obj.PhoneNo;
                if (Obj.Active == true)
                {
                    ChkActive.Checked = true;
                }
                else
                {
                    ChkActive.Checked = false;
                }
                //txtEndDate.Text = Obj.EndDate != null ? Obj.EndDate.Value.ToString("yyyy-MM-dd") : "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:showdrop(); ", true);
            }
        }

        private void BindDropdown()
        {
            ddlClass1.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddlClass1.DataValueField = "ID";
            ddlClass1.DataTextField = "Name";
            ddlClass1.DataBind();
            ddlClass1.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass2.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddlClass2.DataValueField = "ID";
            ddlClass2.DataTextField = "Name";
            ddlClass2.DataBind();
            ddlClass2.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass3.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddlClass3.DataValueField = "ID";
            ddlClass3.DataTextField = "Name";
            ddlClass3.DataBind();
            ddlClass3.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass4.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddlClass4.DataValueField = "ID";
            ddlClass4.DataTextField = "Name";
            ddlClass4.DataBind();
            ddlClass4.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });



            ddlClass5.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddlClass5.DataValueField = "ID";
            ddlClass5.DataTextField = "Name";
            ddlClass5.DataBind();
            ddlClass5.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlRole.DataSource = DB.ISUserRoles.Where(p => p.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList();
            ddlRole.DataValueField = "ID";
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem { Text = "Select Role", Value = "0" });

            ddl1st.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddl1st.DataValueField = "ID";
            ddl1st.DataTextField = "Name";
            ddl1st.DataBind();
            ddl1st.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl2nd.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddl2nd.DataValueField = "ID";
            ddl2nd.DataTextField = "Name";
            ddl2nd.DataBind();
            ddl2nd.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl3rd.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddl3rd.DataValueField = "ID";
            ddl3rd.DataTextField = "Name";
            ddl3rd.DataBind();
            ddl3rd.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl4th.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddl4th.DataValueField = "ID";
            ddl4th.DataTextField = "Name";
            ddl4th.DataBind();
            ddl4th.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl5th.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            ddl5th.DataValueField = "ID";
            ddl5th.DataTextField = "Name";
            ddl5th.DataBind();
            ddl5th.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));

                if (DB.ISTeachers.Where(a => a.ID != ID && a.Email == txtEmail.Text && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Email Address is Already Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISTeachers.Where(a => a.ID != ID && a.PhoneNo == txtPhoneNo.Text && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Phone number is Already Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISTeachers.Where(a => a.ID != ID && a.TeacherNo == txtIdNo.Text && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Number is Already Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISTeachers.Where(a => a.ID != ID && a.Email == txtEmail.Text && a.Name == txtName.Text && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Name is Already Setup with this Email in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
                    objTeacher.ClassID = Convert.ToInt32(ddlClass1.SelectedValue);
                    //objTeacher.Role = drpTeacherRole.SelectedValue == "1" ? (int)EnumsManagement.ROLETYPE.TEACHING : drpTeacherRole.SelectedValue == "2" ? (int)EnumsManagement.ROLETYPE.NONTEACHING : 0;
                    objTeacher.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                    objTeacher.TeacherNo = txtIdNo.Text;
                    objTeacher.Title = ddlTeacherTitle.SelectedValue;
                    objTeacher.Name = txtName.Text;
                    objTeacher.PhoneNo = txtPhoneNo.Text;
                    objTeacher.Email = txtEmail.Text;
                    //objTeacher.Password = EncryptionHelper.Encrypt(txtPass.Text);
                    objTeacher.Active = ChkActive.Checked == true ? true : false;

                    objTeacher.ModifyBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objTeacher.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();
                    //Remove Range from Teacher Class Assignment By Teacher ID
                    List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
                    if (objList.Count > 0)
                    {
                        if (ddlClass1.SelectedValue != "0")
                        {
                            if (objList.Count >= 1)
                            {
                                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objList[0].ClassID, Convert.ToInt32(ddlClass1.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                            }
                        }
                        if (ddlClass2.SelectedValue != "0")
                        {
                            if (objList.Count > 1)
                            {
                                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objList[1].ClassID, Convert.ToInt32(ddlClass2.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                            }
                        }
                        if (ddlClass3.SelectedValue != "0")
                        {
                            if (objList.Count > 2)
                            {
                                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objList[2].ClassID, Convert.ToInt32(ddlClass3.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                            }
                        }
                        if (ddlClass4.SelectedValue != "0")
                        {
                            if (objList.Count > 3)
                            {
                                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objList[3].ClassID, Convert.ToInt32(ddlClass4.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                            }
                        }
                        if (ddlClass5.SelectedValue != "0")
                        {
                            if (objList.Count > 4)
                            {
                                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objList[4].ClassID, Convert.ToInt32(ddlClass5.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                            }
                        }
                        DB.ISTeacherClassAssignments.RemoveRange(objList);
                        DB.SaveChanges();
                    }

                    //Add Teacher Class Assignment
                    if (ChkActive.Checked == true)
                    {
                        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                        objClass1.ClassID = Convert.ToInt32(ddlClass1.SelectedValue);
                        objClass1.TeacherID = ID;
                        objClass1.Active = true;
                        objClass1.Deleted = true;
                        objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                        objClass1.CreatedDateTime = DateTime.Now;
                        objClass1.Out = 0;
                        objClass1.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass1);
                        DB.SaveChanges();
                        if (ddlClass2.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                            objClass2.ClassID = Convert.ToInt32(ddlClass2.SelectedValue);
                            objClass2.TeacherID = ID;
                            objClass2.Active = true;
                            objClass2.Deleted = true;
                            objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass2.CreatedDateTime = DateTime.Now;
                            objClass2.Out = 0;
                            objClass2.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass2);
                            DB.SaveChanges();
                        }
                        if (ddlClass3.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                            objClass3.ClassID = Convert.ToInt32(ddlClass3.SelectedValue);
                            objClass3.TeacherID = ID;
                            objClass3.Active = true;
                            objClass3.Deleted = true;
                            objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass3.CreatedDateTime = DateTime.Now;
                            objClass3.Out = 0;
                            objClass3.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass3);
                            DB.SaveChanges();
                        }
                        if (ddlClass4.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                            objClass4.ClassID = Convert.ToInt32(ddlClass4.SelectedValue);
                            objClass4.TeacherID = ID;
                            objClass4.Active = true;
                            objClass4.Deleted = true;
                            objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass4.CreatedDateTime = DateTime.Now;
                            objClass4.Out = 0;
                            objClass4.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass4);
                            DB.SaveChanges();
                        }
                        if (ddlClass5.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                            objClass5.ClassID = Convert.ToInt32(ddlClass5.SelectedValue);
                            objClass5.TeacherID = ID;
                            objClass5.Active = true;
                            objClass5.Deleted = true;
                            objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass5.CreatedDateTime = DateTime.Now;
                            objClass5.Out = 0;
                            objClass5.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass5);
                            DB.SaveChanges();
                        }
                    }
                    else
                    {
                        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                        objClass1.ClassID = Convert.ToInt32(ddlClass1.SelectedValue);
                        objClass1.TeacherID = ID;
                        objClass1.Active = false;
                        objClass1.Deleted = false;
                        objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                        objClass1.CreatedDateTime = DateTime.Now;
                        objClass1.Out = 0;
                        objClass1.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass1);
                        DB.SaveChanges();
                        if (ddlClass2.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                            objClass2.ClassID = Convert.ToInt32(ddlClass2.SelectedValue);
                            objClass2.TeacherID = ID;
                            objClass2.Active = false;
                            objClass2.Deleted = false;
                            objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass2.CreatedDateTime = DateTime.Now;
                            objClass2.Out = 0;
                            objClass2.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass2);
                            DB.SaveChanges();
                        }
                        if (ddlClass3.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                            objClass3.ClassID = Convert.ToInt32(ddlClass3.SelectedValue);
                            objClass3.TeacherID = ID;
                            objClass3.Active = false;
                            objClass3.Deleted = false;
                            objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass3.CreatedDateTime = DateTime.Now;
                            objClass3.Out = 0;
                            objClass3.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass3);
                            DB.SaveChanges();
                        }
                        if (ddlClass4.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                            objClass4.ClassID = Convert.ToInt32(ddlClass4.SelectedValue);
                            objClass4.TeacherID = ID;
                            objClass4.Active = false;
                            objClass4.Deleted = false;
                            objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass4.CreatedDateTime = DateTime.Now;
                            objClass4.Out = 0;
                            objClass4.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass4);
                            DB.SaveChanges();
                        }
                        if (ddlClass5.SelectedValue != "0")
                        {
                            ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                            objClass5.ClassID = Convert.ToInt32(ddlClass5.SelectedValue);
                            objClass5.TeacherID = ID;
                            objClass5.Active = false;
                            objClass5.Deleted = false;
                            objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                            objClass5.CreatedDateTime = DateTime.Now;
                            objClass5.Out = 0;
                            objClass5.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass5);
                            DB.SaveChanges();
                        }
                    }
                    Clear();
                    LogManagement.AddLog("Teacher Updated Successfully " + "Name : " + objTeacher.Name + " Document Category : TeacherProfile", "Teacher");
                    AlertMessageManagement.ServerMessage(Page, "Teacher Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                BindData(ID);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void EmailManage(ISTeacher _Teacher)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            string tblTeacherBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Teacher " + _Teacher.Name + @", has been ammended by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Teacher Name : " + _Teacher.Name + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Teacher Email : " + _Teacher.Email + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact <b>" + LoggedINName + @"</b> for any enquiries.
                                   </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                SuperwisorBody += reader.ReadToEnd();
            }
            SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Teacher " + _Teacher.Name + @", has been ammended by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Teacher Name : " + _Teacher.Name + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Teacher Email : " + _Teacher.Email + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact <b>" + LoggedINName + @"</b> for any enquiries.
                                   </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Edit Non Teacher Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Edit Non Teacher Notification", SuperwisorBody);

            tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    A teacher account has been modified for you by : " + _School.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        UserName : " + _Teacher.Email + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password : " + EncryptionHelper.Decrypt(_Teacher.Password) + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You can see after Login and in your Profile which changes done by School. 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You will be taken to the login page by clicking on this link. <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">Click Here</a> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions of useful FAQs or on how to used the app effctively is attached to this email or can be obtained electronically by clicking on this link.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        We are on hand to help with queries should you have them and you can email us at support@learningstreet.co.uk with a question. We plan to respond to every question within 24 hours. Please note that we will only be able to respond to the email provided to school for registrating this account.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Alternatively, speak to your School Administrator for assistance
                                   </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                TeacherBody += reader.ReadToEnd();
            }
            TeacherBody = TeacherBody.Replace("{Body}", tblTeacherBody);

            _EmailManagement.SendEmail(_Teacher.Email, "Edit Teacher Account Notification", TeacherBody);
        }
        private void Clear()
        {
            ddlTeacherTitle.SelectedValue = "0";
            txtIdNo.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhoneNo.Text = "";
            //txtEndDate.Text = "";
            ddlClass1.SelectedValue = "0";
            ddlClass2.SelectedValue = "0";
            ddlClass3.SelectedValue = "0";
            ddlClass4.SelectedValue = "0";
            ddlClass5.SelectedValue = "0";
            //drpTeacherRole.SelectedValue = "0";
            ddlRole.SelectedValue = "0";
            ChkActive.Checked = false;
            //txtPass.Text = "";
            //txtDefaultPass.Text = "";
            //cbb2.Checked = false;
        }

        protected void btnAssignClass_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
            ISTeacher ObjTeachers = DB.ISTeachers.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
            List<ISTeacherClassAssignment> objClass = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
            string OldClasses = string.Empty;
            string NewClasses = string.Empty;
            if (objClass.Count > 0)
            {
                if (ddl1st.SelectedValue != "0")
                {
                    if (objClass.Count >= 1)
                    {
                        OldClasses += objClass[0].ISClass.Name + ", ";
                        NewClasses += ddl1st.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[0].ClassID, Convert.ToInt32(ddl1st.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl2nd.SelectedValue != "0")
                {
                    if (objClass.Count > 1)
                    {
                        OldClasses += objClass[1].ISClass.Name + ", ";
                        NewClasses += ddl2nd.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[1].ClassID, Convert.ToInt32(ddl2nd.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl3rd.SelectedValue != "0")
                {
                    if (objClass.Count > 2)
                    {
                        OldClasses += objClass[2].ISClass.Name + ", ";
                        NewClasses += ddl3rd.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[2].ClassID, Convert.ToInt32(ddl3rd.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl4th.SelectedValue != "0")
                {
                    if (objClass.Count > 3)
                    {
                        OldClasses += objClass[3].ISClass.Name + ", ";
                        NewClasses += ddl4th.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[3].ClassID, Convert.ToInt32(ddl4th.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl5th.SelectedValue != "0")
                {
                    if (objClass.Count > 4)
                    {
                        OldClasses += objClass[4].ISClass.Name + ", ";
                        NewClasses += ddl5th.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[4].ClassID, Convert.ToInt32(ddl5th.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                OldClasses = OldClasses.Substring(0, OldClasses.Length - 2);
                NewClasses = NewClasses.Substring(0, NewClasses.Length - 2);
                DB.ISTeacherClassAssignments.RemoveRange(objClass);
                DB.SaveChanges();
            }
            if (ObjTeachers.Active == true)
            {
                //Add New data to TeacherClassAssignment Table
                ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                objClass1.ClassID = Convert.ToInt32(ddl1st.SelectedValue);
                objClass1.TeacherID = ID;
                objClass1.Active = true;
                objClass1.Deleted = true;
                objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                objClass1.CreatedDateTime = DateTime.Now;
                objClass1.Out = 0;
                objClass1.Outbit = false;
                DB.ISTeacherClassAssignments.Add(objClass1);
                DB.SaveChanges();
                if (ddl2nd.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                    objClass2.ClassID = Convert.ToInt32(ddl2nd.SelectedValue);
                    objClass2.TeacherID = ID;
                    objClass2.Active = true;
                    objClass2.Deleted = true;
                    objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass2.CreatedDateTime = DateTime.Now;
                    objClass2.Out = 0;
                    objClass2.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass2);
                    DB.SaveChanges();
                }
                if (ddl3rd.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                    objClass3.ClassID = Convert.ToInt32(ddl3rd.SelectedValue);
                    objClass3.TeacherID = ID;
                    objClass3.Active = true;
                    objClass3.Deleted = true;
                    objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass3.CreatedDateTime = DateTime.Now;
                    objClass3.Out = 0;
                    objClass3.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass3);
                    DB.SaveChanges();
                }
                if (ddl4th.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                    objClass4.ClassID = Convert.ToInt32(ddl4th.SelectedValue);
                    objClass4.TeacherID = ID;
                    objClass4.Active = true;
                    objClass4.Deleted = true;
                    objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass4.CreatedDateTime = DateTime.Now;
                    objClass4.Out = 0;
                    objClass4.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass4);
                    DB.SaveChanges();
                }
                if (ddl5th.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                    objClass5.ClassID = Convert.ToInt32(ddl5th.SelectedValue);
                    objClass5.TeacherID = ID;
                    objClass5.Active = true;
                    objClass5.Deleted = true;
                    objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass5.CreatedDateTime = DateTime.Now;
                    objClass5.Out = 0;
                    objClass5.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass5);
                    DB.SaveChanges();
                }

            }
            else
            {
                ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                objClass1.ClassID = Convert.ToInt32(ddl1st.SelectedValue);
                objClass1.TeacherID = ID;
                objClass1.Active = false;
                objClass1.Deleted = false;
                objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                objClass1.CreatedDateTime = DateTime.Now;
                objClass1.Out = 0;
                objClass1.Outbit = false;
                DB.ISTeacherClassAssignments.Add(objClass1);
                DB.SaveChanges();
                if (ddl2nd.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                    objClass2.ClassID = Convert.ToInt32(ddl2nd.SelectedValue);
                    objClass2.TeacherID = ID;
                    objClass2.Active = false;
                    objClass2.Deleted = false;
                    objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass2.CreatedDateTime = DateTime.Now;
                    objClass2.Out = 0;
                    objClass2.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass2);
                    DB.SaveChanges();
                }
                if (ddl3rd.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                    objClass3.ClassID = Convert.ToInt32(ddl3rd.SelectedValue);
                    objClass3.TeacherID = ID;
                    objClass3.Active = false;
                    objClass3.Deleted = false;
                    objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass3.CreatedDateTime = DateTime.Now;
                    objClass3.Out = 0;
                    objClass3.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass3);
                    DB.SaveChanges();
                }
                if (ddl4th.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                    objClass4.ClassID = Convert.ToInt32(ddl4th.SelectedValue);
                    objClass4.TeacherID = ID;
                    objClass4.Active = false;
                    objClass4.Deleted = false;
                    objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass4.CreatedDateTime = DateTime.Now;
                    objClass4.Out = 0;
                    objClass4.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass4);
                    DB.SaveChanges();
                }
                if (ddl5th.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                    objClass5.ClassID = Convert.ToInt32(ddl5th.SelectedValue);
                    objClass5.TeacherID = ID;
                    objClass5.Active = false;
                    objClass5.Deleted = false;
                    objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objClass5.CreatedDateTime = DateTime.Now;
                    objClass5.Out = 0;
                    objClass5.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass5);
                    DB.SaveChanges();
                }
            }
            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
            ReassignEmailManage(ObjTeacher, OldClasses, NewClasses);
            ClearData();
            LogManagement.AddLog("Teacher ReAssign Successfully " + "Name : " + ObjTeacher.Name + " Document Category : TeacherProfile", "Teacher");
            AlertMessageManagement.ServerMessage(Page, "Teacher ReAssign Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            BindData(Convert.ToInt32(OperationManagement.GetOperation("ID")));
        }
        public void ReassignEmailManage(ISTeacher _Teacher, string OldClass, string NewClass)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            string tblTeacherBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Teacher " + _Teacher.Name + @" has been re-assigned to another class by " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Old Class Assigned : " + OldClass + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        New Class Assigned : " + NewClass + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact <b>" + LoggedINName + @"</b> for any enquiries.
                                   </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                SuperwisorBody += reader.ReadToEnd();
            }
            SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Teacher " + _Teacher.Name + @" has been re-assigned to another class by " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Old Class Assigned : " + OldClass + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        New Class Assigned : " + NewClass + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact <b>" + LoggedINName + @"</b> for any enquiries.
                                   </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Re-assign Class Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Re-assign Class Notification", SuperwisorBody);

            tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    Your teacher account has been re-assigned to another class by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Old Class Assigned : " + OldClass + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        New Class Assigned : " + NewClass + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You can be redirected to activate the account and change this password to user-defined the first time you login.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You will be taken to the login page by clicking on this link. <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">Click Here</a> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Alternatively, speak to your School Administrator for assistance
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> <b>" + _School.Name + @"</b>
                                    </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                TeacherBody += reader.ReadToEnd();
            }
            TeacherBody = TeacherBody.Replace("{Body}", tblTeacherBody);

            _EmailManagement.SendEmail(_Teacher.Email, "Re-assign Class Notification", TeacherBody);
        }
        private void ClearData()
        {
            ddl1st.SelectedValue = "0";
            ddl2nd.SelectedValue = "0";
            ddl3rd.SelectedValue = "0";
            ddl4th.SelectedValue = "0";
            ddl5th.SelectedValue = "0";
        }
        //protected void resetpassword_Click(object sender, EventArgs e)
        //{
        //    int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
        //    IList<ISTeacher> obj2 = DB.ISTeachers.Where(a => a.ID != ID && a.Email == txtEmail.Text && a.Active == true && a.Deleted == true).ToList();
        //    if (obj2.Count > 0)
        //    {
        //        AlertMessageManagement.ServerMessage(Page, "This Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
        //    }
        //    else
        //    {
        //        ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);

        //        objTeacher.Password = EncryptionHelper.Encrypt(txtpassword.Text);

        //        objTeacher.ModifyBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
        //        objTeacher.ModifyDateTime = DateTime.Now;
        //        DB.SaveChanges();

        //        EmailManagement objEmailManagement = new EmailManagement();
        //        if (Authentication.LogginSchool != null)
        //        {
        //            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Teacher {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objTeacher.Name, "Password Changed", "Hello Dear Teacher, Your Password has been Changed to " + txtpassword.Text + " So Now onwards you have to use this Credentials for Login to SchoolApp.", Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName, Authentication.LogginSchool.Name);
        //            objEmailManagement.SendEmail(objTeacher.Email, "Password Changed", Message);
        //        }
        //        else
        //        {
        //            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Teacher {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objTeacher.Name, "Password Changed", "Hello Dear Teacher, Your Password has been Changed to " + txtpassword.Text + " So Now onwards you have to use this Credentials for Login to SchoolApp.", Authentication.LogginTeacher.Name, DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID).ISSchool.Name);
        //            objEmailManagement.SendEmail(objTeacher.Email, "Password Changed", Message);
        //        }
        //        // sending email to admin & supervisor by Maharshi @ Gatistavam-softech
        //        EmailManagement emobj = new EmailManagement();
        //        var tcd = DB.ISTeachers.SingleOrDefault(a => a.ID == ID);
        //        emobj.SendEmail(tcd.Email, "  Password changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + tcd.Name + ",<br /> your Password  has been changed please confirm on website <br /> Thnk you");

        //        //Clear();
        //        LogManagement.AddLog("Teacher Password Updated Successfully " + "Name : " + objTeacher.Name + " Document Category : TeacherProfile", "Teacher");
        //        AlertMessageManagement.ServerMessage(Page, "Teacher Password Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
        //    }
        //}

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
            IList<ISTeacher> obj2 = DB.ISTeachers.Where(a => a.ID != ID && a.Email == txtEmail.Text && a.Active == true && a.Deleted == true).ToList();
            if (obj2.Count > 0)
            {
                AlertMessageManagement.ServerMessage(Page, "This Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
                string Pass = CommonOperation.GenerateNewRandom();
                objTeacher.Password = EncryptionHelper.Encrypt(Pass);

                objTeacher.ModifyBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                objTeacher.ModifyDateTime = DateTime.Now;
                DB.SaveChanges();
                objTeacherManagement.ChangePasswordEmailManage(objTeacher);
                //Clear();
                LogManagement.AddLog("Teacher Password Reset Successfully " + "Name : " + objTeacher.Name + " Document Category : TeacherProfile", "Teacher");
                AlertMessageManagement.ServerMessage(Page, "Teacher Password Reset Successfully.", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
        }
        public void ChangeImageEmailManage(ISTeacher _Teacher)
        {
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblTeacherBody = string.Empty;

            tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Notification of Image changed for " + _Teacher.Name + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment By : " + LoggedINName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please contact " + _School.AdminFirstName + " " + _School.AdminLastName + @" with any query
                                   </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                TeacherBody += reader.ReadToEnd();
            }
            TeacherBody = TeacherBody.Replace("{Body}", tblTeacherBody);

            _EmailManagement.SendEmail(_Teacher.Email, "Change of Image Notification", TeacherBody);
        }
    }
}