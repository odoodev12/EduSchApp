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
    public partial class NonTeacher : System.Web.UI.Page
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
                bindData("", "1", "", "Date");

                //if (Session["EndedDates"] != null)
                //{
                //    DateTime myDt = Convert.ToDateTime(Session["EndedDates"]);
                //    txtEndDate.Text = myDt.ToString("yyyy-MM-dd");
                //}
                //txtEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageNonTeacherFlag == true)
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
            ddlRole.DataSource = DB.ISUserRoles.Where(p => p.RoleType == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList();
            ddlRole.DataValueField = "ID";
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem { Text = "Select Role", Value = "0" });

            ddlClass1.DataSource = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
            ddlClass1.DataValueField = "ID";
            ddlClass1.DataTextField = "Name";
            ddlClass1.DataBind();
            ddlClass1.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });


            ddlClass2.DataSource = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
            ddlClass2.DataValueField = "ID";
            ddlClass2.DataTextField = "Name";
            ddlClass2.DataBind();
            ddlClass2.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddlClass3.DataSource = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
            ddlClass3.DataValueField = "ID";
            ddlClass3.DataTextField = "Name";
            ddlClass3.DataBind();
            ddlClass3.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl1stClass.DataSource = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
            ddl1stClass.DataValueField = "ID";
            ddl1stClass.DataTextField = "Name";
            ddl1stClass.DataBind();
            ddl1stClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            ddl2ndClass.DataSource = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
            ddl2ndClass.DataValueField = "ID";
            ddl2ndClass.DataTextField = "Name";
            ddl2ndClass.DataBind();
            ddl2ndClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });


            ddl3rdClass.DataSource = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true && p.Active == true).ToList();
            ddl3rdClass.DataValueField = "ID";
            ddl3rdClass.DataTextField = "Name";
            ddl3rdClass.DataBind();
            ddl3rdClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }

        public void bindData(string TeacherName, string Status, string OrderBy, string SortBy)
        {
            var Obj = objTeacherManagement.NonTeacherList(Authentication.SchoolID, TeacherName, OrderBy, SortBy);
            if (Status != "0")
            {
                if (Status == "1")
                {
                    Obj = Obj.Where(p => p.Active == true).ToList();
                }
                else
                {
                    Obj = Obj.Where(p => p.Active == false).ToList();
                }
            }
            lstTeachers.DataSource = Obj;
            lstTeachers.DataBind();
            //int ID = Convert.ToInt32(HID.Value);
            //litFirstClass.Text = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID).OrderBy(p => p.ID).FirstOrDefault().ISClass.Name;
            //litSecondClass.Text = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID).OrderBy(p => p.ID).LastOrDefault().ISClass.Name;
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtTeacherName.Text, drpStatus.SelectedValue, rbtnAscending.Checked == true ? "ASC" : "DESC", ddlShortBy.SelectedValue);
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
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
            else if (DB.ISTeachers.Where(a => a.Email == txtEmail.Text && a.Name == txtName.Text && a.SchoolID == Authentication.SchoolID && a.Deleted == true).ToList().Count > 0)
            {
                AlertMessageManagement.ServerMessage(Page, "Not Allowed. Teacher Name is Already Setup with this Email in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                ISTeacher objTeacher = new ISTeacher();
                objTeacher.SchoolID = Authentication.SchoolID;
                objTeacher.ClassID = ddlClass1.SelectedValue != "0" ? Convert.ToInt32(ddlClass1.SelectedValue) : (int?)null;
                objTeacher.Role = (int)EnumsManagement.ROLETYPE.NONTEACHING;
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
                //    Session["EndedDates"] = txtEndDate.Text;
                //}
                objTeacher.EndDate = new DateTime(2050, 01, 01);
                objTeacher.Photo = "Upload/user.jpg";
                objTeacher.Active = true;/*ChkActive.Checked == true ? true : false;*/
                objTeacher.Deleted = true;
                objTeacher.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                objTeacher.CreatedDateTime = DateTime.Now;
                //if(ChkActive.Checked == false)
                //{
                //    objTeacher.ModifyBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                //    objTeacher.ModifyDateTime = DateTime.Now;
                //}
                DB.ISTeachers.Add(objTeacher);
                DB.SaveChanges();
                //if (ChkActive.Checked == true)
                //{
                if (ddlClass1.SelectedValue != "0")
                {
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
                }
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
                    ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                    objClass2.ClassID = Convert.ToInt32(ddlClass3.SelectedValue);
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
                EmailManage(objTeacher);
                Clear();
                LogManagement.AddLog("Non Teacher Created Successfully " + "Name : " + objTeacher.Name + " Document Category : NonTeacher", "NonTeacher");
                AlertMessageManagement.ServerMessage(Page, "Non Teacher Created Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            bindData("", "1", "", "Date");
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
                                    New Non Teacher Added By : " + LoggedINName + @"<br/>
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
                                    New Non Teacher Added By : " + LoggedINName + @"<br/>
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
                                <td style = 'text-align:left;'>
                                    A new non teacher account has been created for you by : " + _School.Name + @"<br/>
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
            ddlClass1.SelectedValue = "0";
            ddlClass2.SelectedValue = "0";
            ddlClass3.SelectedValue = "0";
            ddlTeacherTitle.SelectedValue = "0";
            txtIdNo.Text = "";
            txtName.Text = "";
            ddlRole.SelectedValue = "0";
            txtEmail.Text = "";
            txtPhoneNo.Text = "";
            ddl1stClass.SelectedValue = "0";
            ddl2ndClass.SelectedValue = "0";
            ddl3rdClass.SelectedValue = "0";
            //ChkActive.Checked = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtTeacherName.Text = "";
            drpStatus.SelectedValue = "1";
            rbtndescending.Checked = true;
            ddlShortBy.SelectedValue = "Date";
            bindData("", "1", "", "Date");
        }

        //protected void valDateRanges_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    DateTime minDate = DateTime.Now;
        //    DateTime maxDate = DateTime.Parse("9999/12/28");
        //    DateTime dt;

        //    args.IsValid = (DateTime.TryParse(args.Value, out dt)
        //                    && dt <= maxDate
        //                    && dt >= minDate);
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static string AssignClasses(string ID)
        {
            int IDS = Convert.ToInt32(ID);
            SchoolAppEntities DB = new SchoolAppEntities();
            List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == IDS && p.Deleted == true).OrderBy(p => p.ID).ToList();
            string ClassName1 = objList.Count >= 1 ? objList[0].ISClass.Name : "";
            string ClassName2 = objList.Count >= 2 ? objList[1].ISClass.Name : "";


            string ClassID1 = objList.Count >= 1 ? objList[0].ISClass.ID.ToString() : "";
            string ClassID2 = objList.Count >= 2 ? objList[1].ISClass.ID.ToString() : "";

            return "{\"FirstClassID\" : \"" + ClassID1 + "\" , \"FirstClass\" : \"" + ClassName1 + "\" , \"SecondClassID\" : \"" + ClassID2 + "\",\"SecondClass\" : \"" + ClassName2 + "\"}";

        }

        protected void btnAddClassAssign_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(HID.Value);
            List<ISTeacherClassAssignment> objClass = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
            string OldClasses = string.Empty;
            string NewClasses = string.Empty;
            if (objClass.Count >= 1)
            {
                OldClasses += objClass[0].ISClass.Name + ", ";
            }
            if(objClass.Count > 1)
            {
                OldClasses += objClass[1].ISClass.Name + ", ";
            }
            DB.ISTeacherClassAssignments.RemoveRange(objClass);
            DB.SaveChanges();
            if (ddl1stClass.SelectedValue != "0")
            {
                NewClasses += ddl1stClass.SelectedItem.Text + ", ";
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
            }
            if (ddl2ndClass.SelectedValue != "0")
            {
                NewClasses += ddl2ndClass.SelectedItem.Text + ", ";
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
            }

            if (ddl3rdClass.SelectedValue != "0")
            {
                NewClasses += ddl3rdClass.SelectedItem.Text + ", ";
                ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                objClass2.ClassID = Convert.ToInt32(ddl3rdClass.SelectedValue);
                objClass2.TeacherID = ID;
                objClass2.Active = true;
                objClass2.Deleted = true;
                objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
                objClass2.CreatedDateTime = DateTime.Now;
                objClass2.Out = 0;
                objClass2.Outbit = false;
                DB.ISTeacherClassAssignments.Add(objClass2);
                DB.SaveChanges();
            }
            OldClasses = OldClasses.Substring(0, OldClasses.Length - 2);
            NewClasses = NewClasses.Substring(0, NewClasses.Length - 2);
            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
            ReassignEmailManage(ObjTeacher, OldClasses, NewClasses);
            Clear();
            LogManagement.AddLog("NonTeacher ReAssign Successfully " + "Name : " + ObjTeacher.Name + " Document Category : NonTeacher", "NonTeacher");
            AlertMessageManagement.ServerMessage(Page, "NonTeacher ReAssign Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            bindData("", "1", "", "Date");
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetNonTeachers(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            TeacherManagement objTeacherManagement = new TeacherManagement();
            List<ISTeacher> ObjList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.Name.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();

            List<string> NonTeacherNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                NonTeacherNames.Add(ObjList[i].Name);
            }
            return NonTeacherNames;
        }
    }
}