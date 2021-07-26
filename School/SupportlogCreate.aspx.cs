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
    public partial class SupportlogCreate : System.Web.UI.Page
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
                BindData();
                BindDropdown();
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
            drpType.DataSource = DB.ISLogTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpType.DataTextField = "LogTypeName";
            drpType.DataValueField = "ID";
            drpType.DataBind();
            drpType.Items.Insert(0, new ListItem { Text = "Select Log Type", Value = "0" });
        }

        private void BindData()
        {
            litTicketNumber.Text = CommonOperation.GenerateSequenceNumber();
            litCreatedBy.Text = Authentication.ISSchoolLogin() == true ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            litDate.Text = DateTime.Now.ToShortDateString();
            litTime.Text = DateTime.Now.ToShortTimeString();
            litStatus.Text = "UnResolved";
        }

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            ISSupport obj = new ISSupport();
            obj.TicketNo = litTicketNumber.Text;
            obj.Request = txtsubject.Text;
            obj.SchoolID = Authentication.SchoolID;
            obj.StatusID = (int)EnumsManagement.SUPPORTSTATUS.UnResolved;
            obj.LogTypeID = Convert.ToInt32(drpType.SelectedValue);
            obj.Active = true;
            obj.Deleted = true;
            obj.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
            obj.CreatedByName = litCreatedBy.Text;
            obj.CreatedDateTime = DateTime.Now;
            DB.ISSupports.Add(obj);
            DB.SaveChanges();
            
            ISTicketMessage objTm = new ISTicketMessage();
            objTm.SupportID = obj.ID;
            objTm.SenderID = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
            objTm.Message = txtDetail.Text;
            if (fupload.HasFile == true)
            {
                string filename = System.IO.Path.GetFileName(fupload.PostedFile.FileName);
                fupload.SaveAs(Server.MapPath("~/Upload/" + filename));
                objTm.SelectFile = "Upload/" + filename;
            }
            objTm.UserTypeID = Authentication.ISTeacherLogin() == true ? 2 : 1;
            objTm.CreatedDatetime = DateTime.Now;
            DB.ISTicketMessages.Add(objTm);
            DB.SaveChanges();

            CreateEmailManage(obj);
            BindDropdown();
            BindData();
            Clear();
            LogManagement.AddLog("Support Created Successfully " + "Subject : " + txtsubject.Text + " Document Category : SupportLogCreate", "Support");
            AlertMessageManagement.ServerMessage(Page, "Support Created Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            Response.Redirect("Supportlog.aspx");
        }

        public void CreateEmailManage(ISSupport _Support)
        {
            string AdminBody = String.Empty;
            

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            
            string tblAdminBody = string.Empty;
            List<ISOrganisationUser> _SupportOwners = DB.ISOrganisationUsers.Where(p => p.RoleID == (int)EnumsManagement.USERROLE.APPSADMIN && p.RoleID == (int)EnumsManagement.USERROLE.ADMIN && p.RoleID == (int)EnumsManagement.USERROLE.SUPPORT && p.Deleted == true).ToList();
            foreach (var item in _SupportOwners)
            {
                string SuperwisorBody = String.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.FirstName + " " + item.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New Support Created : " + _Support.TicketNo + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Title of Support Ticket : " + _Support.Request + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Raised by School : " + _School.Name + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Raised by : " + LoggedINName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date Support Ticket was raised : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Time Support Ticket was raised : " + DateTime.Now.ToString("hh:mm tt") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        For any enquiries, please contact <b>" + LoggedINName + @"</b>for more information.
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(item.Email, "Create Support  Notification", SuperwisorBody);
            }
            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New Support Created : " + _Support.TicketNo + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Title of Support Ticket : " + _Support.Request + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Raised by School : " + _School.Name + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Raised by : " + LoggedINName + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Date Support Ticket was raised : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Time Support Ticket was raised : " + DateTime.Now.ToString("hh:mm tt") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        For any enquiries, please contact <b>" + LoggedINName + @"</b>for more information.
                                   </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Create Support  Notification", AdminBody);
            
        }

        private void Clear()
        {
            drpType.SelectedValue = "0";
            txtsubject.Text = "";
            txtDetail.Text = "";
            fupload.Dispose();
        }
    }
}