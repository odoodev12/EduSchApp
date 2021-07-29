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
    public partial class SupportlogView : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    BindList();
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
        private void BindList()
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            SupportManagement objSupportManagement = new SupportManagement();
            MISSupport objMSupport = objSupportManagement.GetSupport(ID);
            //ISSupport obj = DB.ISSupports.SingleOrDefault(p => p.ID==ID);

            if (objMSupport != null)
            {
                litTicketNo.Text = objMSupport.TicketNo;
                litSubject.Text = objMSupport.Request;
                litCreatedBy.Text = objMSupport.CreatedByName;
                litDate.Text = objMSupport.SDate;
                litTime.Text = objMSupport.STime;
                litStatus.Text = objMSupport.Status;
                litDetail.Text = "<ul class='ulClass'>";
                foreach (ISTicketMessage item in DB.ISTicketMessages.Where(p => p.SupportID == objMSupport.ID).OrderByDescending(p => p.CreatedDatetime))
                {
                    ISSchool objSchool = new ISSchool();
                    ISTeacher ObjTeacher = new ISTeacher();
                    if (item.UserTypeID == 1)
                    {
                        objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == item.SenderID);
                    }
                    else
                    {
                        ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == item.SenderID);
                    }
                    litDetail.Text += "<li style ='width:100%' > " +
                        "<div class='msj macro'>" +
                        "<div class='avatar'><img class='img-circle' style='width:100%;' src='' /></div>" +
                            "<div class='text text-l'>" +
                                "<p><font size='3'>" + item.Message + "</font></p>" +
                                "<p><small>" + (item.UserTypeID == 1 ? objSchool.AdminFirstName + " " + objSchool.AdminLastName + "(" + objSchool.Name + ")" : item.UserTypeID == 2 ? ObjTeacher.Name + "(" + ObjTeacher.ISSchool.Name + ")" : "Support") + "</small></p>" +
                                "<p><small> <label><a href='" + (item.UserTypeID != 3 ? (item.SelectFile != null ? "http://school.gsdemosystem.com/" + item.SelectFile : "#") : "http://school.gsdemosystem.com/" + (item.SelectFile != null ? item.SelectFile : "#")) + "' download=''>Attachment</a></label></small></p>" +
                                "<p><small>" + (item.CreatedDatetime != null ? item.CreatedDatetime.Value.ToString("dd/MM/yyyy hh:mm tt") : "") + "</small></p>" +
                            "</div>" +
                        "</div>" +
                    "</li>";

                }

                litDetail.Text += "</ul>";
            }

            //ListView1.DataSource = obj.ToList();
            //ListView1.DataBind();
        }
        protected void lnkAttachment_Click(object sender, EventArgs e)
        {

        }
        protected void lnkSend_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                ISTicketMessage objTicketMessage = new ISTicketMessage();
                objTicketMessage.SupportID = ID;
                objTicketMessage.SenderID = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.LogginSchool.ID;
                objTicketMessage.Message = txtComment.Text;
                if (fupload.HasFile == true)
                {
                    string filename = System.IO.Path.GetFileName(fupload.PostedFile.FileName);
                    fupload.SaveAs(Server.MapPath("../Upload/" + filename));
                    objTicketMessage.SelectFile = "../Upload/" + filename;
                }
                objTicketMessage.CreatedDatetime = DateTime.Now;
                objTicketMessage.UserTypeID = Authentication.ISTeacherLogin() ? 2 : 1;
                DB.ISTicketMessages.Add(objTicketMessage);
                DB.SaveChanges();

                ISSupport _Support = DB.ISSupports.SingleOrDefault(p => p.ID == ID);
                UpdateEmailManage(_Support, Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.Name : Authentication.LogginSchool.Name);
                Clear();
                BindList();
                //AlertMessageManagement.MESSAGETYPE.Success
                LogManagement.AddLog("Support Sent Successfully " + "Support Subject : " + objTicketMessage.ISSupport.Request + " Document Category : SupportLogView", "Support");
                AlertMessageManagement.ServerMessage(Page, "Support Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void UpdateEmailManage(ISSupport _Support, string UpdatedBy)
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
                                    Support Ticket, " + _Support.TicketNo + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Update Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated by : " + UpdatedBy + @"
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

                _EmailManagement.SendEmail(item.Email, "Support Update Notification", SuperwisorBody);
            }
            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Support Ticket, " + _Support.TicketNo + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Update Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated by : " + UpdatedBy + @"
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Support Update Notification", AdminBody);

        }
        protected void Clear()
        {
            txtComment.Text = "";
        }
    }
}