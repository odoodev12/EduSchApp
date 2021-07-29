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
    public partial class RequestForDemo : System.Web.UI.Page
    {

        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindComboBox();
            }
        }


        private void BindComboBox()
        {
            drpRoles.DataSource = GetRoles();
            drpRoles.DataBind();
        }

        private List<string> GetRoles()
        {
            List<string> roles = new List<string>();
            roles.Add("Head of School");
            roles.Add("Deputy Head of School Owner");
            roles.Add("School Admin");
            roles.Add("School IT Personnel");
            roles.Add("Head of Subject");
            roles.Add("Head of School");
            roles.Add("Teacher");
            roles.Add("Other");
            return roles;
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtOfficialEmail.Text;
            string AdminBody = String.Empty;
            if (!string.IsNullOrEmpty(txtOfficialEmail.Text))
            {

                ISRequestDemo requestDemo = new ISRequestDemo();
                requestDemo.FirstName = txtFirstName.Text;
                requestDemo.LastName = txtLastName.Text;
                requestDemo.Role = drpRoles.SelectedValue;
                requestDemo.SchoolName = txtSchoolName.Text;
                requestDemo.OfficialEmail = txtOfficialEmail.Text;
                requestDemo.OfficialPhoneNumber = txtOfficialPhoneNumber.Text;
                requestDemo.MobileNumber = txtMobileNumber.Text;
                requestDemo.CreatedDateTime = DateTime.Now;
                DB.ISRequestDemoes.Add(requestDemo);
                DB.SaveChanges();

                string calendarLink = string.Empty; // calendar service link
                string tblAdminBody = @"<table>
                            <tr style='float:center;'><td><h2>Welcome to Thestmanager</h2></td></tr>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + txtFirstName.Text + ' ' + txtLastName.Text + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Thanks very much for your interest in our software product. We are delighted to give you a demo of our application.
                                    This has been designed to give a good understanding of the features  and how benefitial it can be for your organisation.
                                    Simply choose an available and suitable date on our calendar and we'll send you a demo event confirmation.
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Click link to select demo date/time.<a href=""" + calendarLink + @""" target=""_blank"">Link</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       If you have any questions regarding the demo, please send an email to Demo@Thestmanager.com.
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
                emailManagement.SendEmail(email, "Thestmanager Demo Request", AdminBody);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Demo Request has been created succesfully');", true);
                ClearFields();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal('Please Enter Official Email');", true);

            }

        }

        private void ClearFields()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            drpRoles.SelectedValue = string.Empty;
            txtSchoolName.Text = string.Empty;
            txtOfficialEmail.Text = string.Empty;
            txtOfficialPhoneNumber.Text = string.Empty;
            txtMobileNumber.Text = string.Empty;
        }
    }
}