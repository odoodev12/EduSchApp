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
    public partial class AdminroleView : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {

                if (OperationManagement.GetOperation("ID") != "")
                {
                    int CID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                    ISUserRole obj = DB.ISUserRoles.Single(p => p.ID == CID && p.Deleted == true && p.SchoolID == Authentication.SchoolID);
                    bool isEditable = true;
                    if (obj != null)
                    {
                        isEditable = !((obj.RoleName == "Admin" && (obj.RoleType == 1 || obj.RoleType == 2)) ||
                                                                (obj.RoleName == "Standard" && (obj.RoleType == 1 || obj.RoleType == 2)));

                        txtRoleName.Text = obj.RoleName;
                        litName.Text = obj.RoleName;
                        litType.Text = obj.RoleType == 1 ? "Teaching Role" : obj.RoleType == 2 ? "Non Teaching Role" : "";
                        chkteacher.Checked = obj.ManageTeacherFlag;
                        chkClass.Checked = obj.ManageClassFlag;
                        chksuppor.Checked = obj.ManageSupportFlag;
                        chkstudent.Checked = obj.ManageStudentFlag;
                        chkviewaccount.Checked = obj.ManageViewAccountFlag;
                        chkHoliday.Checked = Convert.ToBoolean(obj.ManageHolidayFlag);
                        chkNonTeacher.Checked = Convert.ToBoolean(obj.ManageNonTeacherFlag);
                        chkActive.Checked = Convert.ToBoolean(obj.Active);

                        
                    }
                    //IsAllControlEnabled(isEditable);
                }
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            if (OperationManagement.GetOperation("ID") != "")
            {
                int CID = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                ISUserRole obj = DB.ISUserRoles.Single(p => p.ID == CID && p.Deleted == true && p.SchoolID == Authentication.SchoolID);
                bool isEditable = true;
                if (obj != null)
                {
                    isEditable = !((obj.RoleName == "Admin" && (obj.RoleType == 1 || obj.RoleType == 2)) ||
                                                            (obj.RoleName == "Standard" && (obj.RoleType == 1 || obj.RoleType == 2)));

                    txtRoleName.Text = obj.RoleName;
                    litName.Text = obj.RoleName;
                    litType.Text = obj.RoleType == 1 ? "Teaching Role" : obj.RoleType == 2 ? "Non Teaching Role" : "";
                    chkteacher.Checked = obj.ManageTeacherFlag;
                    chkClass.Checked = obj.ManageClassFlag;
                    
                    chksuppor.Checked = obj.ManageSupportFlag;
                    chkstudent.Checked = obj.ManageStudentFlag;
                    chkviewaccount.Checked = obj.ManageViewAccountFlag;
                    chkHoliday.Checked = Convert.ToBoolean(obj.ManageHolidayFlag);
                    chkNonTeacher.Checked = Convert.ToBoolean(obj.ManageNonTeacherFlag);
                    chkActive.Checked = Convert.ToBoolean(obj.Active);
                }
                IsAllControlEnabled(isEditable);
                chkClass.InputAttributes.Remove("disabled");
            }
        }

        private void IsAllControlEnabled(bool isEditable)
        {
            btnUpdate.Visible = isEditable;
            litName.Visible = !isEditable;
            txtRoleName.Visible = isEditable;
            chkteacher.Enabled = isEditable;
            chkClass.Enabled = isEditable;
            chksuppor.Enabled = isEditable;
            chkstudent.Enabled = isEditable;
            chkviewaccount.Enabled = isEditable;
            chkHoliday.Enabled = isEditable;
            chkNonTeacher.Enabled = isEditable;
            chkActive.Enabled = isEditable;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            RolesManagement objRolesManagement = new RolesManagement();
            if (OperationManagement.GetOperation("ID") != "")
            {
                int ID = Convert.ToInt32(OperationManagement.GetOperation("ID"));

                int TypeID = litType.Text == "Teaching Role" ? 1 : 2;
                List<ISUserRole> obj = DB.ISUserRoles.Where(p => p.RoleName == txtRoleName.Text && p.RoleType == TypeID && p.SchoolID == Authentication.SchoolID && p.ID != ID && p.Deleted == true).ToList();
                if (obj.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Role Already Exists!!!", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    if ((txtRoleName.Text == "Standard" || txtRoleName.Text == "Admin") && chkActive.Checked == false)
                    {
                        AlertMessageManagement.ServerMessage(Page, "You can not made this role InActive!!!", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        chkActive.Checked = true;
                    }
                    else
                    {
                        if (chkClass.Checked == true || chkstudent.Checked == true || chksuppor.Checked == true || chkteacher.Checked == true || chkviewaccount.Checked == true || chkNonTeacher.Checked == true || chkHoliday.Checked == true)
                        {
                            int IDS = Convert.ToInt32(OperationManagement.GetOperation("ID"));
                            ISUserRole _Roles = DB.ISUserRoles.SingleOrDefault(p => p.ID == IDS);
                            
                            ISUserRole _Role = objRolesManagement.CreateorUpdateRole(IDS, Authentication.SchoolID, txtRoleName.Text, TypeID, chkteacher.Checked, chkClass.Checked, chksuppor.Checked, chkstudent.Checked, chkviewaccount.Checked, chkNonTeacher.Checked, chkHoliday.Checked, chkActive.Checked);
                            if (_Roles.Active == false && chkActive.Checked == true)
                            {
                                ActiveEmailManage(_Role);
                            }
                            else if (_Roles.Active == true && chkActive.Checked == false)
                            {
                                InActiveEmailManage(_Role);
                            }
                            EmailManage(_Role);
                            AlertMessageManagement.ServerMessage(Page, "Role Updated Successfully!!!", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            LogManagement.AddLog("Role Updated Successfully " + "Name : " + txtRoleName.Text + " Document Category : UserRole", "UserRole");
                            Response.Redirect("Adminrole.aspx");
                        }
                        else
                        {
                            AlertMessageManagement.ServerMessage(Page, "Please keep atleast one Role selected!!!", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                }
            }
        }
        public void EmailManage(ISUserRole _Role)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Role, " + _Role.RoleName + @" Updated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Role record is updated with the current information.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + LoggedINName + @"
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
                                    Role, " + _Role.RoleName + @" Updated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Role record is updated with the current information.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + LoggedINName + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Update Admin Role Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Update Admin Role Notification", SuperwisorBody);

        }
        public void ActiveEmailManage(ISUserRole _Role)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Role, " + _Role.RoleName + @" Activated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Activation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + LoggedINName + @"
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
                                    Role, " + _Role.RoleName + @" Activated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Activation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + LoggedINName + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Activation Admin Role Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Activation Admin Role Notification", SuperwisorBody);

        }
        public void InActiveEmailManage(ISUserRole _Role)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Role, " + _Role.RoleName + @" Deactivated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deactivation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + LoggedINName + @"
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
                                    Role, " + _Role.RoleName + @" Deactivated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deactivation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For any enquiries, please contact " + LoggedINName + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Deactivation Admin Role Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Deactivation Admin Role Notification", SuperwisorBody);

        }

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }
    }
}