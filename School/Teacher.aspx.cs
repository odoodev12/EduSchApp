using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
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
    public partial class Teacher : System.Web.UI.Page
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
                Binddropdown();
                bindData("", 0, "", "", "Date", Convert.ToInt32(ddlClassType.SelectedValue), "1");
                //txtEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                //if(Session["ENDDATE"] != null)
                //{
                //    DateTime myDt = Convert.ToDateTime(Session["ENDDATE"]);
                //    txtEndDate.Text = myDt.ToString("yyyy-MM-dd");
                //}
                if (Authentication.LogginSchool != null)
                {
                    if (Authentication.LogginSchool.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        PanelAfterSchool.Visible = false;
                    }
                    else
                    {
                        PanelAfterSchool.Visible = true;
                    }
                }
                if (Authentication.LogginTeacher != null)
                {
                    ISSchool _school = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);
                    if (_school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        PanelAfterSchool.Visible = false;
                    }
                    else
                    {
                        PanelAfterSchool.Visible = true;
                    }
                }
            }

        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageTeacherFlag == true)
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
        public void Binddropdown()
        {


            ddlClassName.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r=>r.Name).ToList();
            ddlClassName.DataValueField = "ID";
            ddlClassName.DataTextField = "Name";
            ddlClassName.DataBind();
            ddlClassName.Items.Insert(0, new ListItem { Text = "Select Class Name", Value = "0" });


            List<ISClassType> _List = new List<ISClassType>();
            if (DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.AfterSchoolType == "External" && p.Deleted == true).Count() > 0)
            {
                _List = DB.ISClassTypes.Where(p => p.Deleted == true && p.ID != (int)EnumsManagement.CLASSTYPE.AfterSchool).ToList();
            }
            else
            {
                _List = DB.ISClassTypes.Where(p => p.Deleted == true).ToList();
            }
            ddlClassType.DataSource = _List;//objClassManagement.ClassTyleList();
            ddlClassType.DataValueField = "ID";
            ddlClassType.DataTextField = "Name";
            ddlClassType.DataBind();
            ddlClassType.Items.Insert(0, new ListItem { Text = "Select Class Type", Value = "0" });


            ddlClassYear.DataSource = CommonOperation.GetYear();
            ddlClassYear.DataValueField = "ID";
            ddlClassYear.DataTextField = "Year";
            ddlClassYear.DataBind();
            ddlClassYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });

            //ddlClassYear.DataSource = CommonOperation.GetYear();
            //ddlClassYear.DataBind();
            //ddlClassYear.Items.Insert(0, new ListItem { Text = "Select Class", Value = "" });

            ddlClass1.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddlClass1.DataValueField = "ID";
            ddlClass1.DataTextField = "Name";
            ddlClass1.DataBind();
            ddlClass1.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });


            ddlClass2.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddlClass2.DataValueField = "ID";
            ddlClass2.DataTextField = "Name";
            ddlClass2.DataBind();
            ddlClass2.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass3.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddlClass3.DataValueField = "ID";
            ddlClass3.DataTextField = "Name";
            ddlClass3.DataBind();
            ddlClass3.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass4.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddlClass4.DataValueField = "ID";
            ddlClass4.DataTextField = "Name";
            ddlClass4.DataBind();
            ddlClass4.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });



            ddlClass5.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddlClass5.DataValueField = "ID";
            ddlClass5.DataTextField = "Name";
            ddlClass5.DataBind();
            ddlClass5.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl1stClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddl1stClass.DataValueField = "ID";
            ddl1stClass.DataTextField = "Name";
            ddl1stClass.DataBind();
            ddl1stClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl2ndClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddl2ndClass.DataValueField = "ID";
            ddl2ndClass.DataTextField = "Name";
            ddl2ndClass.DataBind();
            ddl2ndClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl3rdClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddl3rdClass.DataValueField = "ID";
            ddl3rdClass.DataTextField = "Name";
            ddl3rdClass.DataBind();
            ddl3rdClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl4thClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddl4thClass.DataValueField = "ID";
            ddl4thClass.DataTextField = "Name";
            ddl4thClass.DataBind();
            ddl4thClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl5thClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true)
                .OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            ddl5thClass.DataValueField = "ID";
            ddl5thClass.DataTextField = "Name";
            ddl5thClass.DataBind();
            ddl5thClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlRole.DataSource = DB.ISUserRoles.Where(p => p.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList();
            ddlRole.DataValueField = "ID";
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem { Text = "Select Role", Value = "0" });
            ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByText("Standard"));
        }

        public void bindData(string Year, int ClassID, string TeacherName, string OrderBy, string SortBy, int classtype, string TeacherStatus)
        {
            var Obj = objTeacherManagement.TeacherList(Authentication.SchoolID, Year, ClassID, TeacherName, OrderBy, SortBy, classtype, TeacherStatus).OrderBy(r=>r.Name).ToList();
            lstTeachers.DataSource = Obj;
            lstTeachers.DataBind();
            //int ID = Convert.ToInt32(HID.Value);
            //litFirstClass.Text = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID).OrderBy(p => p.ID).FirstOrDefault().ISClass.Name;
            //litSecondClass.Text = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID).OrderBy(p => p.ID).LastOrDefault().ISClass.Name;
        }

        protected void ddlSelectClassYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClassYear.SelectedValue != "")
            {
                bindData(ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassName.SelectedValue), txtTeacherName.Text, rbtnAscending.Checked == true ? "ASC" : "DESC", ddlShortBy.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
            }
            else
            {
                bindData(ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassName.SelectedValue), txtTeacherName.Text, rbtnAscending.Checked == true ? "ASC" : "DESC", ddlShortBy.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
            }
        }


        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassName.SelectedValue), txtTeacherName.Text, rbtnAscending.Checked == true ? "ASC" : "DESC", ddlShortBy.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (DB.ISTeachers.Where(a => a.Email.ToLower() == txtEmail.Text.ToLower() && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Email Address is Already Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISTeachers.Where(a => a.PhoneNo == txtPhoneNo.Text && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Phone number is Already Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISTeachers.Where(a => a.TeacherNo == txtIdNo.Text && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Number is Already Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (DB.ISTeachers.Where(a => a.Email == txtEmail.Text && a.Name == txtName.Text && a.Deleted == true).ToList().Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Name is Already Setup with this Email in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    ISTeacher objTeacher = new ISTeacher();
                    objTeacher.SchoolID = Authentication.SchoolID;
                    objTeacher.ClassID = Convert.ToInt32(ddlClass1.SelectedValue);
                    objTeacher.Role = (int)EnumsManagement.ROLETYPE.TEACHING;
                    objTeacher.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
                    objTeacher.TeacherNo = txtIdNo.Text;
                    objTeacher.Title = ddlTeacherTitle.SelectedValue;
                    objTeacher.Name = txtName.Text;
                    objTeacher.PhoneNo = txtPhoneNo.Text;
                    objTeacher.Email = txtEmail.Text;
                    string Passwords = CommonOperation.GenerateNewRandom();
                    objTeacher.Password = EncryptionHelper.Encrypt(Passwords);
                    //if (txtEndDate.Text != "")
                    //{
                    //    objTeacher.EndDate = DateTime.Parse(txtEndDate.Text);
                    //    Session["ENDDATE"] = txtEndDate.Text;
                    //}
                    objTeacher.EndDate = new DateTime(2050, 01, 01);
                    objTeacher.Photo = "Upload/user.jpg";
                    objTeacher.Active = true;
                    objTeacher.Deleted = true;
                    objTeacher.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    objTeacher.CreatedDateTime = DateTime.Now;
                    //if (ChkActive.Checked == false)
                    //{
                    //    objTeacher.ModifyBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                    //    objTeacher.ModifyDateTime = DateTime.Now;
                    //}
                    objTeacher.CreatedByType = Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                    DB.ISTeachers.Add(objTeacher);
                    DB.SaveChanges();

                    //if (ChkActive.Checked == true)
                    //{
                    ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                    objClass1.ClassID = Convert.ToInt32(ddlClass1.SelectedValue);
                    objClass1.TeacherID = objTeacher.ID;
                    objClass1.Active = true;
                    objClass1.Deleted = true;
                    objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass1.CreatedDateTime = DateTime.Now;
                    objClass1.Out = 0;
                    objClass1.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass1);
                    DB.SaveChanges();
                    if (ddlClass2.SelectedValue != "0")
                    {
                        ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                        objClass2.ClassID = Convert.ToInt32(ddlClass2.SelectedValue);
                        objClass2.TeacherID = objTeacher.ID;
                        objClass2.Active = true;
                        objClass2.Deleted = true;
                        objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
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
                        objClass3.TeacherID = objTeacher.ID;
                        objClass3.Active = true;
                        objClass3.Deleted = true;
                        objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
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
                        objClass4.TeacherID = objTeacher.ID;
                        objClass4.Active = true;
                        objClass4.Deleted = true;
                        objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
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
                        objClass5.TeacherID = objTeacher.ID;
                        objClass5.Active = true;
                        objClass5.Deleted = true;
                        objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                        objClass5.CreatedDateTime = DateTime.Now;
                        objClass5.Out = 0;
                        objClass5.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass5);
                        DB.SaveChanges();
                    }
                    LogManagement.AddLog("Teacher Created Successfully " + "Name : " + objTeacher.Name + " Document Category : Teacher", "Teacher");
                    EmailManage(objTeacher);
                    Clear();
                }
                bindData("", 0, "", "", "", Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
                AlertMessageManagement.ServerMessage(Page, "Teacher Created Successfully.", (int)AlertMessageManagement.MESSAGETYPE.Success);
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
            string TeacherBody1 = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            string tblTeacherBody = string.Empty;
            string tblTeacherBody1 = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New Teacher Added By : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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
                                        Please contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> " + LoggedINName + @"<br/> " + _School.Name + @"
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
                                <td>
                                    New Teacher Added By : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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
                                        Please contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> " + LoggedINName + @"<br/> " + _School.Name + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Add Teacher Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Add Teacher Notification", SuperwisorBody);

            tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A new teacher account has been created for you by : " + _School.Name + @"<br/>
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

            _EmailManagement.SendEmail(_Teacher.Email, "New Teacher Account Notification", TeacherBody);

            tblTeacherBody1 = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database record shows that " + _Teacher.Name + @" has no identifiable image.<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for the teacher
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Failure to do so will result you receiving this notification in the future.
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                TeacherBody1 += reader.ReadToEnd();
            }
            TeacherBody1 = TeacherBody1.Replace("{Body}", tblTeacherBody1);

            _EmailManagement.SendEmail(_School.AdminEmail, "No Teacher Image", TeacherBody1);
        }
        private void Clear()
        {
            ddlTeacherTitle.SelectedValue = "0";
            txtIdNo.Text = "";
            txtName.Text = "";
            ddlClass1.SelectedValue = "0";
            ddlClass2.SelectedValue = "0";
            ddlRole.SelectedValue = "0";
            txtEmail.Text = "";
            txtPhoneNo.Text = "";
            ddl1stClass.SelectedValue = "0";
            ddl2ndClass.SelectedValue = "0";
            drpStatus.SelectedValue = "1";
            //ChkActive.Checked = false;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string AssignClass(string ID)
        {
            int IDS = Convert.ToInt32(ID);
            SchoolAppEntities DB = new SchoolAppEntities();
            List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == IDS && p.Deleted == true).OrderBy(p => p.ID).ToList();
            string ClassName1 = objList.Count >= 1 ? objList[0].ISClass.Name : "";
            string ClassName2 = objList.Count >= 2 ? objList[1].ISClass.Name : "";
            string ClassName3 = objList.Count >= 3 ? objList[2].ISClass.Name : "";
            string ClassName4 = objList.Count >= 4 ? objList[3].ISClass.Name : "";
            string ClassName5 = objList.Count >= 5 ? objList[4].ISClass.Name : "";


            string ClassID1 = objList.Count >= 1 ? objList[0].ISClass.ID.ToString() : "";
            string ClassID2 = objList.Count >= 2 ? objList[1].ISClass.ID.ToString() : "";
            string ClassID3 = objList.Count >= 3 ? objList[2].ISClass.ID.ToString() : "";
            string ClassID4 = objList.Count >= 4 ? objList[3].ISClass.ID.ToString() : "";
            string ClassID5 = objList.Count >= 5 ? objList[4].ISClass.ID.ToString() : "";

            //string ClassID1 = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == IDS).OrderBy(p => p.ID).FirstOrDefault().ISClass.Name;
            //string ClassID2 = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == IDS).OrderByDescending(p => p.ID).FirstOrDefault().ISClass.Name;



            return "{\"FirstClassID\" : \"" + ClassID1 + "\" , \"FirstClass\" : \"" + ClassName1 + "\" , \"SecondClassID\" : \"" + ClassID2 + "\",\"SecondClass\" : \"" + ClassName2 + "\",\"ThirdClassID\" : \"" + ClassID3 + "\", \"ThirdClass\" : \"" + ClassName3 + "\", \"FourthClassID\" : \"" + ClassID4 + "\", \"FourthClass\" : \"" + ClassName4 + "\",\"FifthClassID\" : \"" + ClassID5 + "\", \"FifthClass\" : \"" + ClassName5 + "\"}";
        }

        protected void btnAddClassAssign_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(HID.Value);
            ISTeacher ObjTeachers = DB.ISTeachers.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
            List<ISTeacherClassAssignment> objClass = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
            string OldClasses = string.Empty;
            string NewClasses = string.Empty;
            if (objClass.Count > 0)
            {
                if (ddl1stClass.SelectedValue != "0")
                {
                    if (objClass.Count >= 1)
                    {
                        OldClasses += objClass[0].ISClass.Name + ", ";
                        NewClasses += ddl1stClass.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[0].ClassID, Convert.ToInt32(ddl1stClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl2ndClass.SelectedValue != "0")
                {
                    if (objClass.Count > 1)
                    {
                        OldClasses += objClass[1].ISClass.Name + ", ";
                        NewClasses += ddl2ndClass.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[1].ClassID, Convert.ToInt32(ddl2ndClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl3rdClass.SelectedValue != "0")
                {
                    if (objClass.Count > 2)
                    {
                        OldClasses += objClass[2].ISClass.Name + ", ";
                        NewClasses += ddl3rdClass.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[2].ClassID, Convert.ToInt32(ddl3rdClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl4thClass.SelectedValue != "0")
                {
                    if (objClass.Count > 3)
                    {
                        OldClasses += objClass[3].ISClass.Name + ", ";
                        NewClasses += ddl4thClass.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[3].ClassID, Convert.ToInt32(ddl4thClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                if (ddl5thClass.SelectedValue != "0")
                {
                    if (objClass.Count > 4)
                    {
                        OldClasses += objClass[4].ISClass.Name + ", ";
                        NewClasses += ddl5thClass.SelectedItem.Text + ", ";
                        objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[4].ClassID, Convert.ToInt32(ddl5thClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
                    }
                }
                OldClasses = OldClasses.Substring(0, OldClasses.Length - 2);
                NewClasses = NewClasses.Substring(0, NewClasses.Length - 2);
                DB.ISTeacherClassAssignments.RemoveRange(objClass);
                DB.SaveChanges();
            }
            if (ObjTeachers.Active == true)
            {
                ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                objClass1.ClassID = Convert.ToInt32(ddl1stClass.SelectedValue);
                objClass1.TeacherID = ID;
                objClass1.Active = true;
                objClass1.Deleted = true;
                objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                objClass1.CreatedDateTime = DateTime.Now;
                objClass1.Out = 0;
                objClass1.Outbit = false;
                DB.ISTeacherClassAssignments.Add(objClass1);
                DB.SaveChanges();
                if (ddl2ndClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                    objClass2.ClassID = Convert.ToInt32(ddl2ndClass.SelectedValue);
                    objClass2.TeacherID = ID;
                    objClass2.Active = true;
                    objClass2.Deleted = true;
                    objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass2.CreatedDateTime = DateTime.Now;
                    objClass2.Out = 0;
                    objClass2.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass2);
                    DB.SaveChanges();
                    //edited By maharshi @Gatistavam
                    // Class_item_2_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass2.ClassID).OrderBy(p => p.ID).ToList();

                }
                if (ddl3rdClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                    objClass3.ClassID = Convert.ToInt32(ddl3rdClass.SelectedValue);
                    objClass3.TeacherID = ID;
                    objClass3.Active = true;
                    objClass3.Deleted = true;
                    objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass3.CreatedDateTime = DateTime.Now;
                    objClass3.Out = 0;
                    objClass3.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass3);
                    DB.SaveChanges();
                    //edited By maharshi @Gatistavam
                    // Class_item_3_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass3.ClassID).OrderBy(p => p.ID).ToList();

                }
                if (ddl4thClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                    objClass4.ClassID = Convert.ToInt32(ddl4thClass.SelectedValue);
                    objClass4.TeacherID = ID;
                    objClass4.Active = true;
                    objClass4.Deleted = true;
                    objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass4.CreatedDateTime = DateTime.Now;
                    objClass4.Out = 0;
                    objClass4.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass4);
                    DB.SaveChanges();
                    //edited By maharshi @Gatistavam
                    //Class_item_4_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass4.ClassID).OrderBy(p => p.ID).ToList();

                }
                if (ddl5thClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                    objClass5.ClassID = Convert.ToInt32(ddl5thClass.SelectedValue);
                    objClass5.TeacherID = ID;
                    objClass5.Active = true;
                    objClass5.Deleted = true;
                    objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass5.CreatedDateTime = DateTime.Now;
                    objClass5.Out = 0;
                    objClass5.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass5);
                    DB.SaveChanges();
                    //edited By maharshi @Gatistavam
                    //Class_item_5_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass5.ClassID).OrderBy(p => p.ID).ToList();

                }
            }
            else
            {

                ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                objClass1.ClassID = Convert.ToInt32(ddl1stClass.SelectedValue);
                objClass1.TeacherID = ID;
                objClass1.Active = false;
                objClass1.Deleted = false;
                objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                objClass1.CreatedDateTime = DateTime.Now;
                objClass1.Out = 0;
                objClass1.Outbit = false;
                DB.ISTeacherClassAssignments.Add(objClass1);
                DB.SaveChanges();
                if (ddl2ndClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                    objClass2.ClassID = Convert.ToInt32(ddl2ndClass.SelectedValue);
                    objClass2.TeacherID = ID;
                    objClass2.Active = false;
                    objClass2.Deleted = false;
                    objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass2.CreatedDateTime = DateTime.Now;
                    objClass2.Out = 0;
                    objClass2.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass2);
                    DB.SaveChanges();
                }
                if (ddl3rdClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                    objClass3.ClassID = Convert.ToInt32(ddl3rdClass.SelectedValue);
                    objClass3.TeacherID = ID;
                    objClass3.Active = false;
                    objClass3.Deleted = false;
                    objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass3.CreatedDateTime = DateTime.Now;
                    objClass3.Out = 0;
                    objClass3.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass3);
                    DB.SaveChanges();
                }
                if (ddl4thClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                    objClass4.ClassID = Convert.ToInt32(ddl4thClass.SelectedValue);
                    objClass4.TeacherID = ID;
                    objClass4.Active = false;
                    objClass4.Deleted = false;
                    objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass4.CreatedDateTime = DateTime.Now;
                    objClass4.Out = 0;
                    objClass4.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass4);
                    DB.SaveChanges();
                }
                if (ddl5thClass.SelectedValue != "0")
                {
                    ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                    objClass5.ClassID = Convert.ToInt32(ddl5thClass.SelectedValue);
                    objClass5.TeacherID = ID;
                    objClass5.Active = false;
                    objClass5.Deleted = false;
                    objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                    objClass5.CreatedDateTime = DateTime.Now;
                    objClass5.Out = 0;
                    objClass5.Outbit = false;
                    DB.ISTeacherClassAssignments.Add(objClass5);
                    DB.SaveChanges();
                }
            }
            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
            LogManagement.AddLog("Teacher Class Re-Assign Successfully " + "ID : " + ObjTeacher.Name + " Document Category : Teacher", "Teacher");
            AlertMessageManagement.ServerMessage(Page, "Teacher Class Re-Assign Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            ReassignEmailManage(ObjTeacher, OldClasses, NewClasses);
            Clear();
            bindData("", 0, "", "", "", Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
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
        protected void ddlClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClassType.SelectedValue != "")
            {
                if (ddlClassType.SelectedValue != "1")
                {
                    ddlClassYear.Enabled = false;
                    ddlClassYear.Style.Add("color", "#cccccc !important");
                    ddlClassYear.CssClass = "form-control";
                    ddlClassYear.SelectedValue = "";
                }
                else
                {
                    ddlClassYear.Enabled = true;
                    ddlClassYear.Style.Add("color", "#555 !important");
                }
                bindData(ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassName.SelectedValue), txtTeacherName.Text, rbtnAscending.Checked == true ? "ASC" : "DESC", ddlShortBy.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
            }
            else
            {
                bindData(ddlClassYear.SelectedValue, Convert.ToInt32(ddlClassName.SelectedValue), txtTeacherName.Text, rbtnAscending.Checked == true ? "ASC" : "DESC", ddlShortBy.SelectedValue, Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlClassYear.SelectedValue = "";
            ddlClassType.SelectedValue = "0";
            ddlClassName.SelectedValue = "0";
            txtTeacherName.Text = "";
            drpStatus.SelectedValue = "1";
            rbtndescending.Checked = true;
            ddlShortBy.SelectedValue = "Date";
            bindData("", 0, "", "", "Date", Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);
        }

        //protected void drpTeacherRole_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(drpTeacherRole.SelectedValue == "1")
        //    {

        //    }
        //    else if(drpTeacherRole.SelectedValue == "2")
        //    {
        //        ddlRole.DataSource = DB.ISUserRoles.Where(p => p.RoleType == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList();
        //        ddlRole.DataValueField = "ID";
        //        ddlRole.DataTextField = "RoleName";
        //        ddlRole.DataBind();
        //        ddlRole.Items.Insert(0, new ListItem { Text = "Select Role", Value = "0" });
        //    }
        //    else
        //    {
        //        ddlRole.DataSource = null;
        //    }
        //}
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetTeachers(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            TeacherManagement objTeacherManagement = new TeacherManagement();
            List<ISTeacher> ObjList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Name.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();

            List<string> TeacherNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                TeacherNames.Add(ObjList[i].Name);
            }
            return TeacherNames;
        }

        //protected void valDateRange_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    DateTime minDate = DateTime.Now;
        //    DateTime maxDate = DateTime.Parse("9999/12/28");
        //    DateTime dt;

        //    args.IsValid = (DateTime.TryParse(args.Value, out dt)
        //                    && dt <= maxDate
        //                    && dt >= minDate);
        //}
    }
}