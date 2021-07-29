using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class OrganisationUserAdd : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Authentication.ISUserLogin())
                {
                    if (!Authentication.ISOrgUserLogin())
                    {
                        Response.Redirect(Authentication.AuthorizePage());
                    }
                    else
                    {
                        if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 2 || Authentication.LoggedInOrgUser.RoleID == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
                if (!IsPostBack)
                {
                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : OrganisationAdd";
                    BindDropdown();
                    if (Request.QueryString["ID"] != null)
                    {
                        OperationManagement.ID = Convert.ToInt32(Request.QueryString["ID"]);
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.Edit;
                        ISOrganisationUser obj = DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == OperationManagement.ID);
                        if (obj != null)
                        {
                            txtFirstName.Text = obj.FirstName;
                            txtLastName.Text = obj.LastName;
                            txtEmail.Text = obj.Email;
                            txtPassword.Text = EncryptionHelper.Decrypt(obj.Password);
                            txtPassword.Attributes.Add("value", EncryptionHelper.Decrypt(obj.Password));
                            txtConfirmPass.Text = EncryptionHelper.Decrypt(obj.Password);
                            txtConfirmPass.Attributes.Add("value", EncryptionHelper.Decrypt(obj.Password));
                            txtCode.Text = obj.OrgCode;
                            txtPostalAddress1.Text = obj.Address1;
                            txtPostalAddress2.Text = obj.Address2;
                            txtTown.Text = obj.Town;
                            drpCountry.SelectedValue = obj.CountryID.ToString();
                            drpRole.SelectedValue = obj.RoleID.ToString();
                            if (obj.Active == true)
                            {
                                chkActive.Checked = true;
                            }
                            else
                            {
                                chkActive.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void BindDropdown()
        {
            try
            {
                drpCountry.DataSource = DB.ISCountries.Where(p => p.Deleted == true && p.Active == true).ToList();
                drpCountry.DataTextField = "Name";
                drpCountry.DataValueField = "ID";
                drpCountry.DataBind();
                drpCountry.Items.Insert(0, new ListItem { Text = "Select Country", Value = "0" });

                drpRole.DataSource = DB.ISRoles.Where(p => p.Deleted == true && p.Active == true).ToList();
                drpRole.DataTextField = "Name";
                drpRole.DataValueField = "ID";
                drpRole.DataBind();
                drpRole.Items.Insert(0, new ListItem { Text = "Select Organisation User Role", Value = "0" });
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationManagement.ID != 0 && OperationManagement.Opration == (int)OperationManagement.OPERRATION.Edit)
                {
                    IList<ISOrganisationUser> obj2 = DB.ISOrganisationUsers.Where(a => a.ID != OperationManagement.ID && a.Email == txtEmail.Text && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        AlertMessageManagement.ServerMessage(Page, "This Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISOrganisationUser obj = DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                        if (obj != null)
                        {
                            SetField(ref obj);
                            obj.ModifyBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                            obj.LastUpdatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                            obj.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                            EditEmailManage(obj);
                            Clear();
                            AlertMessageManagement.ServerMessage(Page, "Organisation Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            OperationManagement.ID = 0;
                            OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                            Response.Redirect("OrganisationUserList.aspx", false);
                        }
                    }
                }
                else
                {
                    IList<ISOrganisationUser> obj2 = DB.ISOrganisationUsers.Where(a => a.Email == txtEmail.Text && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        AlertMessageManagement.ServerMessage(Page, "This Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISOrganisationUser obj = new ISOrganisationUser();
                        SetField(ref obj);
                        if (UploadImage.HasFile == false)
                        {
                            //string filename = System.IO.Path.GetFileName(UploadImage.PostedFile.FileName);
                            //UploadImage.SaveAs(Server.MapPath("Upload/" + filename));
                            obj.Photo = "Upload/user.jpg";
                        }
                        obj.StartDate = DateTime.Now;
                        obj.EndDate = new DateTime(2050, 01, 01);
                        obj.Deleted = true;
                        obj.CreatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                        obj.CreatedByName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                        obj.CreatedDateTime = DateTime.Now;
                        DB.ISOrganisationUsers.Add(obj);
                        DB.SaveChanges();
                        NewEmailManage(obj);
                        Clear();
                        AlertMessageManagement.ServerMessage(Page, "Organisation Added Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        Response.Redirect("OrganisationUserList.aspx", false);
                    }
                }

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
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtCode.Text = "";
                txtPostalAddress1.Text = "";
                txtPostalAddress2.Text = "";
                txtTown.Text = "";
                //txtStartDate.Text = "";
                //txtEndDate.Text = "";
                drpRole.SelectedValue = "0";
                drpCountry.SelectedValue = "0";
                chkActive.Checked = true;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        public void NewEmailManage(ISOrganisationUser _User)
        {
            List<ISAdminLogin> _Login = DB.ISAdminLogins.Where(p => p.Active == true && p.Deleted == true).ToList();
            foreach (var item in _Login)
            {
                string AdminBody = String.Empty;
                string LoggedINName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                string tblAdminBody = string.Empty;

                tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.FullName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New organisation User , " + _User.FirstName + " " + _User.LastName + @" Added By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact " + LoggedINName + @" for any enquiries 
                                    </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(item.Email, "New Organisation Notification", AdminBody);
            }
            List<ISOrganisationUser> _Users = DB.ISOrganisationUsers.Where(p => p.RoleID == (int)EnumsManagement.USERROLE.APPSADMIN && p.Active == true && p.Deleted == true).ToList();
            foreach (var item in _Users)
            {
                string AdminBody = String.Empty;
                string LoggedINName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                string tblAdminBody = string.Empty;

                tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.FirstName + " " + item.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    New organisation User , " + _User.FirstName + " " + _User.LastName + @" Added By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact " + LoggedINName + @" for any enquiries 
                                    </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(item.Email, "New Organisation Notification", AdminBody);
            }

            string AdminBodys = String.Empty;
            string LoggedINNames = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
            string tblAdminBodys = string.Empty;

            tblAdminBodys = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _User.FirstName + " " + _User.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your new account has been created. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                       UserName : " + _User.Email + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       Password : " + EncryptionHelper.Decrypt(_User.Password) + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You will be redirected  to activate the account and if you want to change this password then go to Profile Section.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You will be taken to the login page by clicking on this <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">link</a>.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions of useful FAQs or on how to used the app effctively is attached to this email or can be obtained electronically by clicking on this link. 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact " + LoggedINNames + @" for any enquiries 
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBodys += reader.ReadToEnd();
            }
            AdminBodys = AdminBodys.Replace("{Body}", tblAdminBodys);

            _EmailManagement.SendEmail(_User.Email, "New Organisation User Account Notofication", AdminBodys);
        }
        public void EditEmailManage(ISOrganisationUser _User)
        {
            List<ISAdminLogin> _Login = DB.ISAdminLogins.Where(p => p.Active == true && p.Deleted == true).ToList();
            foreach (var item in _Login)
            {
                string AdminBody = String.Empty;
                string LoggedINName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                string tblAdminBody = string.Empty;
                
                tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.FullName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Organisation User, " + _User.FirstName + " " + _User.LastName + @" Updated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact " + LoggedINName + @" for any enquiries 
                                    </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(item.Email, "Edit Organisation User Notification", AdminBody);
            }
            List<ISOrganisationUser> _Users = DB.ISOrganisationUsers.Where(p => p.RoleID == (int)EnumsManagement.USERROLE.APPSADMIN && p.Active == true && p.Deleted == true).ToList();
            foreach (var item in _Users)
            {
                string AdminBody = String.Empty;
                string LoggedINName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                string tblAdminBody = string.Empty;

                tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + item.FirstName + " " + item.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Organisation User, " + _User.FirstName + " " + _User.LastName + @" Updated By " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact " + LoggedINName + @" for any enquiries 
                                    </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(item.Email, "Edit Organisation User Notification", AdminBody);
            }

            string AdminBodys = String.Empty;
            string LoggedINNames = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
            string tblAdminBodys = string.Empty;

            tblAdminBodys = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _User.FirstName + " " + _User.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Organisation User, " + _User.FirstName + " " + _User.LastName + @" Updated By " + LoggedINNames + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Updated Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please contact " + LoggedINNames + @" for any enquiries 
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBodys += reader.ReadToEnd();
            }
            AdminBodys = AdminBodys.Replace("{Body}", tblAdminBodys);

            _EmailManagement.SendEmail(_User.Email, "Edit Organisation User Notification", AdminBodys);
        }
        private void SetField(ref ISOrganisationUser obj)
        {
            try
            {
                obj.FirstName = txtFirstName.Text;
                obj.LastName = txtLastName.Text;
                obj.Email = txtEmail.Text;
                obj.Password = EncryptionHelper.Encrypt(txtPassword.Text);
                obj.OrgCode = txtCode.Text;
                if (UploadImage.HasFile == true)
                {
                    string filename = System.IO.Path.GetFileName(UploadImage.PostedFile.FileName);
                    UploadImage.SaveAs(Server.MapPath("Upload/" + filename));
                    obj.Photo = "Upload/" + filename;
                }
                obj.Address1 = txtPostalAddress1.Text;
                obj.Address2 = txtPostalAddress2.Text;
                obj.Town = txtTown.Text;
                obj.CountryID = Convert.ToInt32(drpCountry.SelectedValue);
                obj.RoleID = Convert.ToInt32(drpRole.SelectedValue);
                obj.StatusID = chkActive.Checked == true ? "Active" : "InActive";
                if ((obj.Active == false || obj.Active == (bool?)null) && chkActive.Checked == true)
                {
                    obj.ActivationBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                    obj.ActivationDate = DateTime.Now;
                }
                obj.Active = chkActive.Checked == true ? true : false;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                OperationManagement.ID = 0;
                OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                Response.Redirect("OrganisationUserList.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
    }
}