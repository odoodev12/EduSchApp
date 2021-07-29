using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web
{
    public partial class RegistrationForm : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtSupervisorEmail.Text;
            string AdminBody = String.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                ISRegistration registration = new ISRegistration();
                registration.BillingAddress = txtBillingAddress.Text;
                registration.SupervisorName = txtSupervisorName.Text;
                registration.SupervisorEmail = txtSupervisorEmail.Text;
                registration.SchoolNumber = txtSchoolNumber.Text;
                registration.MobileNumber = txtMobileNumber.Text;
                registration.SchoolAddress = txtSchoolAddress.Text;
                registration.SchoolWebsite = txtSchoolWebsite.Text;
                registration.SchoolOpeningTime = txtOpeningTime.Text;
                registration.SchoolClosingTime = txtClosingTime.Text;
                registration.LastMinutesAfterClosing = txtLastMinutes.Text;
                registration.ChargeMinutesAfterClosing = txtChargeMinutes.Text;
                registration.ReportMinutesAfterClosing = txtReportMinutes.Text;
                registration.IsNotificationAfterAttendance = chbNotificationAfterAttendance.Checked;
                registration.IsNotificationAfterPickup = chbNotificationAfterPickup.Checked;
                registration.FilePath = fileUploadControl.FileName;
                registration.CreatedDateTime = DateTime.Now;
                DB.ISRegistrations.Add(registration);
                DB.SaveChanges();
                string calendarLink = string.Empty; // calendar service link
                string tblAdminBody = @"<table>
                            <tr style='float:center;'><td><h2>Welcome to Thestmanager</h2></td></tr>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + txtSupervisorName.Text + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                   Thank you for signing up to Thestmanager. We are super excited to have you on board and aim to get you set up with the utmost ease.
                                </td>
                             </tr>
                            <tr><br/>
                            <td>
                               Your new school account has been created. As the nominated admin, your account controls the school account.
                                For added security, we use two-factor authentication, hence you would be prompted to set up a password and memorable word.
                            </td>
                             </tr>
                            <tr>
                                <td>
                                    Please click the link below to create your login credentials.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    If the above link does not work, simply copy and paste the link below into your browser,
                                </td>
                            </tr>
                                <tr style='float:left;'>
                                <td>
                                 Kind Regards,<br/>
                                 Thestmanager.
                                </td><br/>
                                </tr></table>";



                EmailManagement emailManagement = new EmailManagement();
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);
                emailManagement.SendEmail(email, " Thestmanager New School Account Created", AdminBody);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('You have registered succesfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Please Enter Official Email');", true);
            }

        }
    }
}