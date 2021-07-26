using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using System.Threading;
using System.IO;

namespace SchoolApp.Web.School
{
    public partial class HolidayAdd : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
                {
                    Response.Redirect(Authentication.SchoolAuthorizePage());
                }
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == ID);
                    if (obj != null)
                    {
                        DateTime fromdates = DateTime.Parse(Convert.ToDateTime(obj.DateFrom).ToShortDateString());
                        DateTime todates = DateTime.Parse(Convert.ToDateTime(obj.DateTo).ToShortDateString());
                        if (fromdates.Date < DateTime.Now.Date && todates.Date < DateTime.Now.Date)
                        {
                            Response.Redirect(Authentication.UnAuthorizePage());
                        }
                    }
                }
                if (!IsPostBack)
                {
                    txtFromDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                    txtToDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                    if (Request.QueryString["ID"] != null)
                    {

                        PanelActive.Visible = true;
                        OperationManagement.ID = Convert.ToInt32(Request.QueryString["ID"]);
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.Edit;
                        ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == OperationManagement.ID);
                        if (obj != null)
                        {
                            txtHolidayNames.Text = obj.Name;
                            txtFromDate.Text = obj.DateFrom.Value.ToString("yyyy-MM-dd");
                            txtToDate.Text = obj.DateTo.Value.ToString("yyyy-MM-dd");
                            if (obj.Active == true)
                            {
                                chkActive.Checked = true;
                            }
                            else
                            {
                                chkActive.Checked = false;
                            }
                            DateTime fromdate = DateTime.Parse(Convert.ToDateTime(txtFromDate.Text).ToShortDateString());
                            if (fromdate.Date < DateTime.Now.Date)
                            {
                                txtFromDate.Enabled = false;
                                txtFromDate.CssClass = "form-control";
                            }
                            else
                            {
                                txtFromDate.Enabled = true;
                                txtFromDate.CssClass = "form-control";
                            }
                            DateTime todate = DateTime.Parse(Convert.ToDateTime(txtToDate.Text).ToShortDateString());
                            if (todate.Date < DateTime.Now.Date)
                            {
                                txtToDate.Enabled = false;
                                txtToDate.CssClass = "form-control";
                            }
                            else
                            {
                                txtToDate.Enabled = true;
                                txtToDate.CssClass = "form-control";
                            }
                        }
                    }
                    else
                    {
                        PanelActive.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageHolidayFlag == true)
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmailManagement emobj = new EmailManagement();
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    DateTime fromdate = DateTime.Parse(Convert.ToDateTime(txtFromDate.Text).ToShortDateString());
                    DateTime todate = DateTime.Parse(Convert.ToDateTime(txtToDate.Text).ToShortDateString());
                    List<ISHoliday> ObjList = DB.ISHolidays.Where(p => p.Name == txtHolidayNames.Text && p.Active == true && p.Deleted == true && p.ID != OperationManagement.ID && p.DateFrom == fromdate && p.DateTo == todate).ToList();
                    if (fromdate > todate)
                    {
                        AlertMessageManagement.ServerMessage(Page, "From Date can not be greater then To Date", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (ObjList.Count > 0)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Holiday with Same Name and Date Exists", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                        if (obj != null)
                        {
                            if (obj.Active == true && chkActive.Checked == false)
                            {
                                InActiveEmailManage(obj);
                            }
                            else if(obj.Active == false && chkActive.Checked == true)
                            {
                                ActiveEmailManage(obj);
                            }
                            SetField(ref obj);
                            obj.Active = chkActive.Checked == true ? true : false;
                            obj.ModifyBy = Authentication.LogginSchool.ID;
                            obj.ModifyDateTime = DateTime.Now;
                            
                            DB.SaveChanges();
                            UpdateEmailManage(obj);
                            Clear();
                            AlertMessageManagement.ServerMessage(Page, "Holiday Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            LogManagement.AddLog("Holiday Updated Successfully " + "Name : " + obj.Name + " Document Category : HolidayAdd", "Holiday");
                            OperationManagement.ID = 0;
                            OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                            Response.Redirect("HolidayList.aspx", false);
                        }
                    }
                }
                else
                {
                    DateTime fromdate = DateTime.Parse(Convert.ToDateTime(txtFromDate.Text).ToShortDateString());
                    DateTime todate = DateTime.Parse(Convert.ToDateTime(txtToDate.Text).ToShortDateString());
                    var HolidayName = txtHolidayNames.Text;
                    List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.Name == HolidayName && p.Active == true && p.Deleted == true && p.DateFrom == fromdate && p.DateTo == todate).ToList();
                    if (fromdate > todate)
                    {
                        AlertMessageManagement.ServerMessage(Page, "From Date can not be greater then To Date", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (ObjHoliday.Count > 0)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Holiday with Same Name and Date Exists", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISHoliday obj = new ISHoliday();
                        SetField(ref obj);
                        obj.Active = true;
                        obj.Deleted = true;
                        obj.CreatedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                        obj.CreatedDateTime = DateTime.Now;
                        DB.ISHolidays.Add(obj);
                        DB.SaveChanges();
                        CreateEmailManage(obj);
                        Clear();

                        AlertMessageManagement.ServerMessage(Page, "Holiday Added Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        LogManagement.AddLog("Holiday Created Successfully " + "Name : " + obj.Name + " Document Category : HolidayAdd", "Holiday");
                        Response.Redirect("HolidayList.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            } //Save button
        }
        private void SetField(ref ISHoliday obj)
        {
            try
            {
                obj.Name = txtHolidayNames.Text;
                obj.SchoolID = Authentication.SchoolID;
                string dates1 = txtFromDate.Text;
                DateTime dt2 = DateTime.Now;
                if (dates1.Contains("/"))
                {
                    string[] arrDate = dates1.Split('/');
                    dt2 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                }
                else
                {
                    dt2 = Convert.ToDateTime(dates1);
                }
                obj.DateFrom = dt2;

                string dates2 = txtToDate.Text;
                DateTime dt3 = DateTime.Now;
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    dt3 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                }
                else
                {
                    dt3 = Convert.ToDateTime(dates2);
                }
                obj.DateTo = dt3;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void Clear()
        {
            try
            {
                txtHolidayNames.Text = "";
                txtFromDate.Text = "";
                txtToDate.Text = "";
                chkActive.Checked = false;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        public void CreateEmailManage(ISHoliday _Holiday)
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
                                    New Holiday, " + _Holiday.Name + @", Created By : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Holiday Date From : " + _Holiday.DateFrom.Value.ToString("dd/MM/yyyy") + @"
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

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New Holiday, " + _Holiday.Name + @", Created By : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Holiday Date From : " + _Holiday.DateFrom.Value.ToString("dd/MM/yyyy") + @"
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Create Holiday Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Create Holiday Notification", SuperwisorBody);
        }
        public void UpdateEmailManage(ISHoliday _Holiday)
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
                                    New Holiday, " + _Holiday.Name + @", Created By : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Holiday Date From : " + _Holiday.DateFrom.Value.ToString("dd/MM/yyyy") + @"
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

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New Holiday, " + _Holiday.Name + @", Created By : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Holiday Date From : " + _Holiday.DateFrom.Value.ToString("dd/MM/yyyy") + @"
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Create Holiday Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Create Holiday Notification", SuperwisorBody);
        }
        public void ActiveEmailManage(ISHoliday _Holiday)
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
                                    Holiday, " + _Holiday.Name + @", has been made actived  by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Activation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Holiday, " + _Holiday.Name + @", has been made actived  by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Activation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Activate Holiday Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Activate Holiday Notification", SuperwisorBody);
        }
        public void InActiveEmailManage(ISHoliday _Holiday)
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
                                    Holiday, " + _Holiday.Name + @", has been made deactivated  by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deactivation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Holiday, " + _Holiday.Name + @", has been made deactivated  by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deactivation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Deactivate Holiday Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Deactivate Holiday Notification", SuperwisorBody);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HolidayList.aspx");
        }
    }
}