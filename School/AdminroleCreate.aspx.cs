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
    public partial class AdminroleCreate : System.Web.UI.Page
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

            }
        }
        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            RolesManagement objRolesManagement = new RolesManagement();
            int RID = IsStandardSchool() ? Convert.ToInt32(drpTeacherRole.SelectedValue) : 1;/// BY default teaching role for After School

            if (chkmanageclass.Checked == true || chkmanagestudent.Checked == true || chkmanagesupport.Checked == true || chkmanageteacher.Checked == true || chkviewaccount.Checked == true || chkManageNonTeacher.Checked == true || chkManageHoliday.Checked == true)
            {
                List<ISUserRole> obj = DB.ISUserRoles.Where(p => p.RoleName == txtRollNo.Text && p.RoleType == RID && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();
                if (obj.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Role Already Exists!!!", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    ISUserRole _Roles = objRolesManagement.CreateorUpdateRole(0, Authentication.SchoolID, txtRollNo.Text, RID, chkmanageteacher.Checked, chkmanageclass.Checked, chkmanagesupport.Checked, chkmanagestudent.Checked, chkviewaccount.Checked, chkManageNonTeacher.Checked, chkManageHoliday.Checked, chkActive.Checked);
                    EmailManage(_Roles);
                    LogManagement.AddLog("Role Added Successfully " + "Name : " + txtRollNo.Text + " Document Category : UserRole", "UserRole");
                    AlertMessageManagement.ServerMessage(Page, "Role Saved Successfully!!!", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    Response.Redirect("Adminrole.aspx");
                }
                Clear();
            }
            else
            {
                AlertMessageManagement.ServerMessage(Page, "Please Select atleast one Role!!!", (int)AlertMessageManagement.MESSAGETYPE.warning);
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
                                    New Role : " + _Role.RoleName + @" Created By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Created Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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
                                    New Role : " + _Role.RoleName + @" Created By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Created Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Create Admin Role Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Create Admin Role Notification", SuperwisorBody);

        }
        private void Clear()
        {
            txtRollNo.Text = "";

            if(IsStandardSchool())
                drpTeacherRole.SelectedValue = "0";

            chkmanageclass.Checked = false;
            chkmanagestudent.Checked = false;
            chkmanageteacher.Checked = false;
            chkmanagesupport.Checked = false;
            chkviewaccount.Checked = false;
            chkManageHoliday.Checked = false;
            chkManageNonTeacher.Checked = false;
            chkActive.Checked = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminrole.aspx", false);
        }
    }
}