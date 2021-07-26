using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web.School
{
    public partial class HolidayList : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!Page.IsPostBack)
            {
                bindData("", "", "", "1");
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
        public void bindData(string Name, string Datefrom, string Dateto, string Status)
        {
            List<MISHoliday> objList = new List<MISHoliday>();
            objList = (from Obj in DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                       select new MISHoliday
                       {
                           ID = Obj.ID,
                           Name = Obj.Name,
                           FromDate = Obj.DateFrom.Value.ToString("dd/MM/yyyy"),
                           ToDate = Obj.DateTo.Value.ToString("dd/MM/yyyy"),
                           DateFrom = Obj.DateFrom,
                           DateTo = Obj.DateTo,
                           //HolidayDate = Obj.HolidayDate,
                           //Date = Obj.HolidayDate.Value.ToString("dd/MM/yyyy"),
                           Active = Obj.Active,
                           ActiveStatus = Obj.Active == true ? "Active" : "InActive"
                       }).ToList();
            if (Name != "")
            {
                objList = objList.Where(p => p.Name.Trim().ToLower().Contains(Name.Trim().ToLower())).ToList();
            }
            if (Datefrom != "")
            {
                DateTime dt = Convert.ToDateTime(txtFromDate.Text);
                objList = objList.Where(p => p.DateFrom >= dt).ToList();
            }
            if (Dateto != "")
            {
                DateTime dt = Convert.ToDateTime(txtToDate.Text);
                objList = objList.Where(p => p.DateTo <= dt).ToList();
            }
            if (Status != "0")
            {
                if (Status == "1")
                {
                    objList = objList.Where(p => p.Active == true).ToList();
                }
                else
                {
                    objList = objList.Where(p => p.Active == false).ToList();
                }
            }
            lstHoliday.DataSource = objList.OrderByDescending(p => p.DateFrom).ToList();
            lstHoliday.DataBind();
            Session["Objectlist"] = objList.ToList();
        }

        protected void lstHoliday_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                if (obj != null)
                {
                    if (obj.DateFrom.Value.Date < DateTime.Now.Date && obj.DateTo.Value.Date < DateTime.Now.Date)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Holiday was gone So not allow to Update Holiday", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        Response.Redirect("HolidayAdd.aspx?ID=" + ID);
                    }
                }
            }
            if (e.CommandName == "btnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);

                ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                if (obj.DateFrom <= DateTime.Now || obj.DateTo <= DateTime.Now)
                {
                    AlertMessageManagement.ServerMessage(Page, "Holiday was gone So not allow to Delete Holiday", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    if (obj != null)
                    {
                        obj.Active = false;
                        obj.Deleted = false;
                        obj.DeletedBy = Authentication.LogginSchool != null ? Authentication.LogginSchool.ID : Authentication.LogginTeacher.ID;
                        obj.DeletedDateTime = DateTime.Now;
                        DB.SaveChanges();
                        DeleteEmailManage(obj);
                        bindData(txtHolidayName.Text, txtFromDate.Text, txtToDate.Text, drpClassStatus.SelectedValue);
                        AlertMessageManagement.ServerMessage(Page, "Holiday Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        LogManagement.AddLog("Holiday Deleted Successfully " + "Name : " + obj.Name + " Document Category : HolidayList", "Holiday");
                    }
                }
            }
        }

        protected void lstHoliday_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstHoliday.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtHolidayName.Text, txtFromDate.Text, txtToDate.Text, drpClassStatus.SelectedValue);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtHolidayName.Text, txtFromDate.Text, txtToDate.Text, drpClassStatus.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtHolidayName.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            drpClassStatus.SelectedValue = "1";
            bindData("", "", "", "1");
        }
        public void DeleteEmailManage(ISHoliday _Holiday)
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
                                    Holiday, " + _Holiday.Name + @", has been deleted by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deleted Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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
                                    Holiday, " + _Holiday.Name + @", has been deleted by : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Deleted Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
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

                _EmailManagement.SendEmail(_School.AdminEmail, "Delete Holiday Notification", AdminBody);
                _EmailManagement.SendEmail(_School.SupervisorEmail, "Delete Holiday Notification", SuperwisorBody);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }
}