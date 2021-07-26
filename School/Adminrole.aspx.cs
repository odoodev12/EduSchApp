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
    public partial class Adminrole : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        RolesManagement objRolesManagement = new RolesManagement();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!Page.IsPostBack)
            {
                bindData("", "0", "1");
            }

        }

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }

        public void bindData(string RoleName, string RoleType, string Status)
        {
            List<MISUserRole> ObjList = objRolesManagement.AdminRoleList(Authentication.SchoolID);
            if (RoleName != "")
            {
                ObjList = ObjList.Where(p => p.RoleName.ToLower() == RoleName.ToLower()).ToList();
            }
            if (RoleType != "0")
            {
                int TypeID = Convert.ToInt32(RoleType);
                ObjList = ObjList.Where(p => p.RoleType == TypeID).ToList();
            }
            if (Status != "0")
            {
                if (Status == "1")
                {
                    ObjList = ObjList.Where(p => p.Active == true).ToList();
                }
                else
                {
                    ObjList = ObjList.Where(p => p.Active == false).ToList();
                }
            }
            lstAdminRoles.DataSource = ObjList;
            lstAdminRoles.DataBind();
        }

        protected void lstAdminRoles_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISUserRole ObjRoles = DB.ISUserRoles.SingleOrDefault(p => p.ID == ID);
                if (ObjRoles.RoleName == "Standard" || ObjRoles.RoleName == "Admin")
                {
                    //bindData();
                    AlertMessageManagement.ServerMessage(Page, "This Role Cannot be Deleted", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    objRolesManagement.DeleteRole(ID);
                    ISUserRole ObjRole = DB.ISUserRoles.SingleOrDefault(p => p.RoleName == "Standard" && p.RoleType == ObjRoles.RoleType && p.SchoolID == Authentication.SchoolID);
                    List<ISTeacher> ObJ = DB.ISTeachers.Where(p => p.RoleID == ID && p.Active == true && p.Deleted == true).ToList();
                    foreach (var item in ObJ)
                    {
                        item.RoleID = ObjRole.ID;
                        DB.SaveChanges();
                    }
                    EmailManage(ObjRoles);
                    LogManagement.AddLog("Role Deleted Successfully " + "Name : " + ObjRoles.RoleName + " Document Category : UserRole", "UserRole");
                    bindData(txtRoleName.Text, drpTeacherRole.SelectedValue, drpStatus.SelectedValue);
                    AlertMessageManagement.ServerMessage(Page, "Role Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.info);
                }
            }
        }

        protected void lstAdminRoles_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstAdminRoles.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtRoleName.Text, drpTeacherRole.SelectedValue, drpStatus.SelectedValue);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtRoleName.Text, drpTeacherRole.SelectedValue, drpStatus.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtRoleName.Text = "";
            drpTeacherRole.SelectedValue = "0";
            drpStatus.SelectedValue = "1";
            bindData("", "0", "1");
        }
        public void EmailManage(ISUserRole _Role)
        {
            try
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
                                    Role, " + _Role.RoleName + @" Deleted By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deleted Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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
                                    Role, " + _Role.RoleName + @" Deleted By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deleted Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void lstAdminRoles_DataBound(object sender, EventArgs e)
        {

        }
    }
}