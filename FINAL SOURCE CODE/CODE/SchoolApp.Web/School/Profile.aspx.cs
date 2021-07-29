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
    public partial class Profile : System.Web.UI.Page
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
                BindDropdown();
                BindData(Authentication.LogginSchool.ID);
                //if (Authentication.LogginSchool.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                //{
                //    PanelStandard.Visible = false;
                //    PanelStandard1.Visible = false;
                //}
                //else
                //{
                //    PanelStandard.Visible = true;
                //    PanelStandard1.Visible = true;
                //}
            }
            if (IsPostBack && fpUploadLogo.PostedFile != null)
            {
                if (fpUploadLogo.PostedFile.FileName.Length > 0)
                {
                    ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true);
                    if (objSchool != null)
                    {
                        string filename = System.IO.Path.GetFileName(fpUploadLogo.PostedFile.FileName);
                        fpUploadLogo.SaveAs(Server.MapPath("~/Upload/" + filename));
                        objSchool.Logo = "Upload/" + filename;
                        DB.SaveChanges();
                        AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    }
                    BindData(Authentication.LogginSchool.ID);
                }
            }
        }

        private void BindDropdown()
        {
            for (int i = 0; i <= 55; i++)
            {
                drpLateAfterClose.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
            }

            for (int i = 0; i <= 55; i++)
            {
                drpChargeAfterClose.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
            }

            for (int i = 0; i <= 55; i++)
            {
                drpReportMinsAfterClose.Items.Add(new ListItem { Text = i.ToString(), Value = i.ToString() });
            }
        }

        private void BindData(int ID)
        {
            ProfileManagement objProfileManagement = new ProfileManagement();
            MISSchool Obj = objProfileManagement.GetSchool(ID);
            if (Obj != null)
            {
                litCustomerNo.Text = Obj.CustomerNumber;
                litSchoolName1.Text = Obj.Name;
                litSchoolNo.Text = Obj.Number;
                litSchoolType.Text = Obj.TypeID != null ? DB.ISSchoolTypes.SingleOrDefault(p => p.ID == Obj.TypeID).Name : "";
                litSchoolStatus.Text = Obj.AccountStatusID != null ? DB.ISAccountStatus.SingleOrDefault(p => p.ID == Obj.AccountStatusID).Name : "";// Obj.ISAccountStatu.Name;
                litPhoneNo.Text = Obj.PhoneNumber;
                litSchAddress.Text = Obj.Address1 + " " + Obj.Address2;
                litTown.Text = Obj.Town;
                litPostCode.Text = Obj.PostCode;
                litCountry.Text = Obj.CountryID != null ? DB.ISCountries.SingleOrDefault(p => p.ID == Obj.CountryID).Name : "";
                litSchBillingAddress.Text = Obj.BillingAddress + " " + Obj.BillingAddress2;
                litSchBillingTown.Text = Obj.BillingTown;
                litBillingPostCode.Text = Obj.BillingPostCode;
                litSchBillingCountry.Text = Obj.BillingCountryID != null ? DB.ISCountries.SingleOrDefault(p => p.ID == Obj.BillingCountryID).Name : "";
                litWebsite.Text = Obj.Website;
                litSupervisorName.Text = Obj.SupervisorFirstname + " " + Obj.SupervisorLastname;
                litSupervisorEmail.Text = Obj.SupervisorEmail;
                litAdminName.Text = Obj.AdminFirstName + " " + Obj.AdminLastName;
                litAdminEmail.Text = Obj.AdminEmail;
                litOpeningTime.Text = Obj.OpningTime.Value.ToString("hh:mm:ss tt");
                litClosingTime.Text = Obj.ClosingTime.Value.ToString("hh:mm:ss tt");
                litLateMinAftClosing.Text = Obj.LateMinAfterClosing;
                litChargeMinAftClosing.Text = Obj.ChargeMinutesAfterClosing;
                litPortableMinAftClosing.Text = Obj.ReportableMinutesAfterClosing;
                litNotyAftConfirmPickup.Text = Obj.isNotificationPickup == true ? "Yes" : "No";
                litNotyAftConfirmAttedance.Text = Obj.NotificationAttendance == true ? "Yes" : "No";
                //litAttendenceModule.Text = Obj.isAttendanceModule == true ? "Yes" : "No";
                Image1.ImageUrl = "../" + Obj.Logo;
                AID.HRef = "../" + Obj.Logo;
                AID.Attributes["data-title"] = Obj.Name;

                txtOpeningTime.Text = Obj.OpningTime.Value.ToString("HH:MM");
                txtClosingTime.Text = Obj.ClosingTime.Value.ToString("HH:MM");
                drpLateAfterClose.SelectedValue = Obj.LateMinAfterClosing;
                drpChargeAfterClose.SelectedValue = Obj.ChargeMinutesAfterClosing;
                drpReportMinsAfterClose.SelectedValue = Obj.ReportableMinutesAfterClosing;
                drpNotifyAftPickup.SelectedValue = Obj.isNotificationPickup == true ? "1" : "0";
                drpNotifyAftAttendance.SelectedValue = Obj.NotificationAttendance == true ? "1" : "0";
                //drpAttendanceModule.SelectedValue = Obj.isAttendanceModule == true ? "1" : "0";
            }

        }

        protected void btnUPdate_Click(object sender, EventArgs e)
        {
            int Late = Convert.ToInt32(drpLateAfterClose.SelectedValue);
            int Charge = Convert.ToInt32(drpChargeAfterClose.SelectedValue);
            int Report = Convert.ToInt32(drpReportMinsAfterClose.SelectedValue);
            if (Late > Charge)
            {
                AlertMessageManagement.ServerMessage(Page, "Not Allowed. Chargeable Minutes cannot be less than Late Minutes", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if(Late > Report)
            {
                AlertMessageManagement.ServerMessage(Page, "Not Allowed. Reportable Minutes cannot be less than Late Minutes", (int)AlertMessageManagement.MESSAGETYPE.warning);

            }
            else if (Charge > Report)
            {
                AlertMessageManagement.ServerMessage(Page, "Not Allowed. Reportable Minutes cannot be less than Chargeable Minutes", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            //else if (Report < Charge || Report < Late)
            //{
            //    AlertMessageManagement.ServerMessage(Page, "Reportable Minutes can not be Smaller then Reportable or Late Minutes.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            //}
            else
            {
                ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true);
                if (objSchool != null)
                {
                    string OpenTime = txtOpeningTime.Text;
                    if (OpenTime.Contains(":"))
                    {
                        string[] arrDate = OpenTime.Split(':');
                        string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
                        objSchool.OpningTime = DateTime.Parse(DTS);
                    }
                    string CloseTime = txtClosingTime.Text;
                    if (CloseTime.Contains(":"))
                    {
                        string[] arrDate = CloseTime.Split(':');
                        string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
                        objSchool.ClosingTime = DateTime.Parse(DTS);
                    }

                    objSchool.LateMinAfterClosing = drpLateAfterClose.SelectedValue;
                    objSchool.ChargeMinutesAfterClosing = drpChargeAfterClose.SelectedValue;
                    objSchool.ReportableMinutesAfterClosing = drpReportMinsAfterClose.SelectedValue;
                    objSchool.NotificationAttendance = drpNotifyAftAttendance.SelectedValue == "1" ? true : false;
                    objSchool.isNotificationPickup = drpNotifyAftPickup.SelectedValue == "1" ? true : false;
                    //if (Authentication.LogginSchool.TypeID == (int?)EnumsManagement.SCHOOLTYPE.Standard)
                    //{
                    //    objSchool.isAttendanceModule = drpAttendanceModule.SelectedValue == "1" ? true : false;
                    //}
                    DB.SaveChanges();
                    EmailManage();

                    Clear();
                    LogManagement.AddLog("School Profile Updated Successfully " + "Name : " + objSchool.Name + " Document Category : Profile", "Profile");
                    AlertMessageManagement.ServerMessage(Page, "School Profile Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
            }
            BindData(Authentication.LogginSchool.ID);
        }
        public void EmailManage()
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
                                    School Profile has been edited by " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        School records now reflect the current information
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
                                    School Profile has been edited by " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        School records now reflect the current information
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Edit School Profile (Edited By School Admin)", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Edit School Profile (Edited By School Admin)", SuperwisorBody);

        }
        private void Clear()
        {
            txtClosingTime.Text = "";
            txtOpeningTime.Text = "";
            drpLateAfterClose.SelectedValue = "0";
            drpChargeAfterClose.SelectedValue = "0";
            drpReportMinsAfterClose.SelectedValue = "0";
            drpNotifyAftPickup.SelectedValue = "0";
            drpNotifyAftAttendance.SelectedValue = "0";
            //drpAttendanceModule.SelectedValue = "0";
        }
    }
}