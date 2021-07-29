using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SchoolAdd : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        ISSchool obj;
        protected void Page_Load(object sender, EventArgs e)
        {
            Wizard1.PreRender += new EventHandler(Wizard1_PreRender);
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
                        if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }

                if (!IsPostBack)
                {
                    obj = new ISSchool();

                    BindDropdown();
                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : SchoolAdd";
                    CalendarExtender1.StartDate = DateTime.Now;
                    CalendarExtender2.StartDate = DateTime.Now;
                    if (Request.QueryString["ID"] != null)
                    {
                        OperationManagement.ID = Convert.ToInt32(Request.QueryString["ID"]);
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.Edit;
                        ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.ID == OperationManagement.ID);
                        Session["SCHOOL"] = obj;
                        if (obj != null)
                        {
                            txtSchoolWebsite.Text = obj.Website;
                            txtSchoolNo.Text = obj.Number;
                            txtSchoolName.Text = obj.Name;
                            txtAdminFName.Text = obj.AdminFirstName;
                            txtAdminLName.Text = obj.AdminLastName;
                            txtAdminEmail.Text = obj.AdminEmail;
                            txtPassword.Text = EncryptionHelper.Decrypt(obj.Password);
                            //txtPassword.Attributes["type"] = "password";
                            txtPassword.Attributes.Add("value", EncryptionHelper.Decrypt(obj.Password));
                            txtConfirmPass.Text = EncryptionHelper.Decrypt(obj.Password);
                            txtConfirmPass.Attributes.Add("value", EncryptionHelper.Decrypt(obj.Password));
                            txtSupFName.Text = obj.SupervisorFirstname;
                            txtSupLName.Text = obj.SupervisorLastname;
                            txtSupEmail.Text = obj.SupervisorEmail;
                            if (obj.OpningTime != null)
                            {
                                txtSchoolOpeningTime.Text = obj.OpningTime.Value.ToString("HH:MM");
                            }
                            if (obj.ClosingTime != null)
                            {
                                txtSchoolClosingTime.Text = obj.ClosingTime.Value.ToString("HH:MM");
                            }
                            drpLateAfterClose.SelectedValue = obj.LateMinAfterClosing;
                            drpChargeAfterClose.SelectedValue = obj.ChargeMinutesAfterClosing;
                            drpReportMinsAfterClose.SelectedValue = obj.ReportableMinutesAfterClosing;

                            txtPhone.Text = obj.PhoneNumber;
                            drpSchoolType.SelectedValue = obj.TypeID.ToString();
                            drpSchoolManagerType.SelectedValue = obj.AccountManagerId.ToString();
                            txtAddress1.Text = obj.Address1;
                            txtAddress2.Text = obj.Address2;
                            txtTown.Text = obj.Town;
                            txtPostCode.Text = obj.PostCode;
                            drpCountry.SelectedValue = obj.CountryID.ToString();

                            if (obj.PaymentSystems == true)
                            {
                                chkPayment_Systems.Checked = true;
                            }
                            else
                            {
                                chkPayment_Systems.Checked = false;
                            }
                            if (obj.CustSigned == true)
                            {
                                ChkCustSigned.Checked = true;
                            }
                            else
                            {
                                ChkCustSigned.Checked = false;
                            }
                            rblist.SelectedValue = obj.SetupTrainingStatus == true ? "1" : "0";
                            if (obj.SetupTrainingDate != null)
                            {
                                DateTime dt = obj.SetupTrainingDate.Value.Date;

                                string dates = dt.ToString("dd/MM/yyyy");
                                if (dates.Contains("-"))
                                {
                                    string[] arrDate = dates.Split('-');
                                    txtIntTrainingDate.Text = arrDate[0].ToString() + "/" + arrDate[1].ToString() + "/" + arrDate[2].ToString();
                                }
                                else
                                {
                                    txtIntTrainingDate.Text = dates;
                                }
                                if (dt < DateTime.Now)
                                {
                                    txtIntTrainingDate.Enabled = false;
                                }
                                else
                                {
                                    txtIntTrainingDate.Enabled = true;
                                }
                            }
                            if (obj.BillingAddress == obj.Address1 && obj.BillingAddress2 == obj.Address2 && obj.BillingPostCode == obj.PostCode && obj.BillingTown == obj.Town)
                            {
                                chkBilling.Checked = true;
                                txtBillingAddress1.ReadOnly = true;
                                txtBillingAddress2.ReadOnly = true;
                                txtBillingPostCode.ReadOnly = true;
                                txtBillingTown.ReadOnly = true;
                                drpBillingCountry.Enabled = false;
                                txtBillingAddress1.Text = obj.BillingAddress;
                                txtBillingAddress2.Text = obj.BillingAddress2;
                                txtBillingPostCode.Text = obj.BillingPostCode;
                                txtBillingTown.Text = obj.BillingTown;
                                drpBillingCountry.SelectedValue = obj.BillingCountryID.ToString();
                            }
                            else
                            {
                                chkBilling.Checked = false;
                                txtBillingAddress1.Text = obj.BillingAddress;
                                txtBillingAddress2.Text = obj.BillingAddress2;
                                txtBillingPostCode.Text = obj.BillingPostCode;
                                txtBillingTown.Text = obj.BillingTown;
                                drpBillingCountry.SelectedValue = obj.BillingCountryID != null ? obj.BillingCountryID.ToString() : "0";
                            }
                            if (obj.ActivationDate != null)
                            {
                                DateTime dt = obj.ActivationDate.Value.Date;
                                string dates = dt.ToString("dd/MM/yyyy");
                                if (dates.Contains("-"))
                                {
                                    string[] arrDate = dates.Split('-');
                                    txtActivateDate.Text = arrDate[0].ToString() + "/" + arrDate[1].ToString() + "/" + arrDate[2].ToString();
                                }
                                else
                                {
                                    txtActivateDate.Text = dates;
                                }
                                if (dt < DateTime.Now)
                                {
                                    txtActivateDate.Enabled = false;
                                }
                                else
                                {
                                    txtActivateDate.Enabled = true;
                                }
                            }
                            if (obj.isAttendanceModule == true)
                            {
                                chkAttendanceModule.Checked = true;
                            }
                            else
                            {
                                chkAttendanceModule.Checked = false;
                            }
                            if (obj.NotificationAttendance == true)
                            {
                                ChkConfirmAttendance.Checked = true;
                            }
                            else
                            {
                                ChkConfirmAttendance.Checked = false;
                            }
                            if (obj.isNotificationPickup == true)
                            {
                                chkConfirmPickUp.Checked = true;
                            }
                            else
                            {
                                chkConfirmPickUp.Checked = false;
                            }

                            if (obj.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active)
                            {
                                chkActive.Checked = true;
                            }
                            else
                            {
                                chkActive.Checked = false;
                            }
                        }
                    }
                    else
                    {
                        Session["SCHOOL"] = new ISSchool();
                        OperationManagement.ID = 0;
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.Add;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void BindDropdown()
        {
            drpSchoolType.DataSource = DB.ISSchoolTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpSchoolType.DataTextField = "Name";
            drpSchoolType.DataValueField = "ID";
            drpSchoolType.DataBind();
            drpSchoolType.Items.Insert(0, new ListItem { Text = "Select School Type", Value = "0" });

            drpCountry.DataSource = DB.ISCountries.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpCountry.DataTextField = "Name";
            drpCountry.DataValueField = "ID";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem { Text = "Select Country", Value = "0" });

            drpBillingCountry.DataSource = DB.ISCountries.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpBillingCountry.DataTextField = "Name";
            drpBillingCountry.DataValueField = "ID";
            drpBillingCountry.DataBind();
            drpBillingCountry.Items.Insert(0, new ListItem { Text = "Select Country", Value = "0" });


            drpSchoolManagerType.DataSource = DB.ISOrganisationUsers.Where(p => (p.RoleID == 2 || p.RoleID == 4) && p.Deleted == true && p.Active == true).OrderBy(r => r.FirstName).ToList();
            drpSchoolManagerType.DataTextField = "FirstName";
            drpSchoolManagerType.DataValueField = "ID";
            drpSchoolManagerType.DataBind();
            drpSchoolManagerType.Items.Insert(0, new ListItem { Text = "Select School Account Manager", Value = "0" });

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
        private string AutoIncrement()
        {
            string tempId = DB.ISSchools.ToList().Count.ToString();
            int id = Convert.ToInt32(tempId);
            id = id + 1;
            string autoId = "CUST" + String.Format("{0:00000}", id);
            return autoId;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            obj = (ISSchool)Session["SCHOOL"];
            bool isvalidate = true;
            if (Wizard1.ActiveStepIndex == 0)
            {

                obj.Website = txtSchoolWebsite.Text;
                obj.Number = txtSchoolNo.Text;
                obj.Name = txtSchoolName.Text;
                if (fpUploadLogo.HasFile == true)
                {
                    string filename = System.IO.Path.GetFileName(fpUploadLogo.PostedFile.FileName);
                    fpUploadLogo.SaveAs(Server.MapPath("Upload/" + filename));
                    obj.Logo = "Upload/" + filename;
                }
                obj.AdminFirstName = txtAdminFName.Text;
                obj.AdminLastName = txtAdminLName.Text;
                obj.AdminEmail = txtAdminEmail.Text;
                obj.Password = EncryptionHelper.Encrypt(txtPassword.Text);
                obj.SupervisorFirstname = txtSupFName.Text;
                obj.SupervisorLastname = txtSupLName.Text;
                obj.SupervisorEmail = txtSupEmail.Text;
                string OpenTime = txtSchoolOpeningTime.Text;
                if (OpenTime.Contains(":"))
                {
                    string[] arrDate = OpenTime.Split(':');
                    string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
                    obj.OpningTime = DateTime.Parse(DTS);
                }
                string CloseTime = txtSchoolClosingTime.Text;
                if (CloseTime.Contains(":"))
                {
                    string[] arrDate = CloseTime.Split(':');
                    string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
                    obj.ClosingTime = DateTime.Parse(DTS);
                }
                obj.LateMinAfterClosing = drpLateAfterClose.SelectedValue;
                obj.ChargeMinutesAfterClosing = drpChargeAfterClose.SelectedValue;
                obj.ReportableMinutesAfterClosing = drpReportMinsAfterClose.SelectedValue;
                obj.SchoolEndDate = new DateTime(2050, 01, 01);
                obj.PhoneNumber = txtPhone.Text;

                obj.AccountManagerId = Convert.ToInt32(drpSchoolManagerType.SelectedValue);

                if (obj.ID > 0)
                {
                    if (obj.TypeID != Convert.ToInt32(drpSchoolType.SelectedValue))
                    {
                        isvalidate = false;
                        AlertMessageManagement.ServerMessage(Page, "You cannot change school type.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else
                    obj.TypeID = Convert.ToInt32(drpSchoolType.SelectedValue);

                Session["SCHOOL"] = obj;
            }
            if (Wizard1.ActiveStepIndex == 1)
            {
                obj.Address1 = txtAddress1.Text;
                obj.Address2 = txtAddress2.Text;
                obj.Town = txtTown.Text;
                obj.PostCode = txtPostCode.Text;
                obj.CountryID = Convert.ToInt32(drpCountry.SelectedValue);

                if (chkBilling.Checked == true)
                {
                    obj.BillingAddress = txtAddress1.Text;
                    obj.BillingAddress2 = txtAddress2.Text;
                    obj.BillingTown = txtTown.Text;
                    obj.BillingPostCode = txtPostCode.Text;
                    obj.BillingCountryID = Convert.ToInt32(drpCountry.SelectedValue);
                }
                else
                {
                    obj.BillingAddress = txtBillingAddress1.Text;
                    obj.BillingAddress2 = txtBillingAddress2.Text;
                    obj.BillingTown = txtBillingTown.Text;
                    obj.BillingPostCode = txtBillingPostCode.Text;
                    obj.BillingCountryID = Convert.ToInt32(drpBillingCountry.SelectedValue);
                }

                Session["SCHOOL"] = obj;

            }
            //if (Wizard1.ActiveStepIndex == 2)
            //{

            //    if (FpUploadClasses.HasFile == true)
            //    {
            //        string filename = System.IO.Path.GetFileName(FpUploadClasses.PostedFile.FileName);
            //        FpUploadClasses.SaveAs(Server.MapPath("Upload/" + filename));
            //        obj.Classfile = "Upload/" + filename;
            //    }
            //    if (FpUploadTeachers.HasFile == true)
            //    {
            //        string filename = System.IO.Path.GetFileName(FpUploadTeachers.PostedFile.FileName);
            //        FpUploadTeachers.SaveAs(Server.MapPath("Upload/" + filename));
            //        obj.Teacherfile = "Upload/" + filename;
            //    }
            //    if (FpUploadStudents.HasFile == true)
            //    {
            //        string filename = System.IO.Path.GetFileName(FpUploadStudents.PostedFile.FileName);
            //        FpUploadStudents.SaveAs(Server.MapPath("Upload/" + filename));
            //        obj.Studentfile = "Upload/" + filename;
            //    }
            //    Session["SCHOOL"] = obj;
            //}

            if (Wizard1.ActiveStepIndex == 2)
            {
                DateTime Traindate = new DateTime();
                DateTime Actdate = new DateTime();
                if (txtIntTrainingDate.Text != "")
                {
                    Traindate = DateTime.ParseExact(txtIntTrainingDate.Text, "dd/MM/yyyy", null);
                }
                if (txtActivateDate.Text != "")
                {
                    Actdate = DateTime.ParseExact(txtActivateDate.Text, "dd/MM/yyyy", null);
                }
                if (rblist.SelectedValue == "0")
                {
                    AlertMessageManagement.ServerMessage(Page, "Must have to Select Initial Training to Yes", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (txtActivateDate.Text != "" && txtIntTrainingDate.Text != "" && Traindate > Actdate)
                {
                    AlertMessageManagement.ServerMessage(Page, "Initial Training Date CANNOT be Greater than Activation Date", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    obj.PaymentSystems = chkPayment_Systems.Checked == true ? true : false;
                    obj.CustSigned = ChkCustSigned.Checked == true ? true : false;
                    obj.SetupTrainingStatus = rblist.SelectedValue == "1" ? true : false;
                    if (txtIntTrainingDate.Text != "")
                    {
                        Traindate = DateTime.ParseExact(txtIntTrainingDate.Text, "dd/MM/yyyy", null);
                        obj.SetupTrainingDate = Traindate; //DateTime.Parse(txtIntTrainingDate.Text);
                    }
                    else
                    {
                        obj.SetupTrainingDate = null;
                    }
                    if (txtActivateDate.Text != "")
                    {
                        Actdate = DateTime.ParseExact(txtActivateDate.Text, "dd/MM/yyyy", null);
                        obj.ActivationDate = Actdate; //DateTime.Parse(txtActivateDate.Text);
                        if (Actdate.Date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            obj.AccountStatusID = (int)EnumsManagement.ACCOUNTSTATUS.Active;
                        }
                        else
                        {
                            obj.AccountStatusID = (int?)EnumsManagement.ACCOUNTSTATUS.InActive;
                        }
                    }
                    else
                    {
                        obj.ActivationDate = null;
                        obj.AccountStatusID = (int)EnumsManagement.ACCOUNTSTATUS.InProcess;
                    }
                    obj.isAttendanceModule = chkAttendanceModule.Checked == true ? true : false;
                    obj.NotificationAttendance = ChkConfirmAttendance.Checked == true ? true : false;
                    obj.isNotificationPickup = chkConfirmPickUp.Checked == true ? true : false;
                    if ((obj.Active == false || obj.Active == (bool?)null) && chkActive.Checked == true)
                    {
                        obj.ActivatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                        //obj.LastActivationDate = DateTime.Now;
                        obj.ActivationDate = DateTime.Now;
                    }
                    obj.AccountStatusID = obj.Active == true ? (int?)EnumsManagement.ACCOUNTSTATUS.Active : (int?)EnumsManagement.ACCOUNTSTATUS.InActive;
                    //obj.Active = chkActive.Checked == true ? true : false;
                    Session["SCHOOL"] = obj;

                    try
                    {
                        if (OperationManagement.ID != 0 && OperationManagement.Opration == (int)OperationManagement.OPERRATION.Edit)
                        {
                            IList<ISSchool> obj2 = DB.ISSchools.Where(a => a.ID != OperationManagement.ID && (a.Number == txtSchoolNo.Text || a.AdminEmail == txtAdminEmail.Text) && a.Active == true && a.Deleted == true).ToList();
                            if (obj2.Count > 0)
                            {
                                AlertMessageManagement.ServerMessage(Page, "This School Number Or Admin Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (Convert.ToInt32(drpSchoolType.SelectedValue) == 1 && !chkAttendanceModule.Checked)
                            {
                                AlertMessageManagement.ServerMessage(Page, "This School must have enable Activate Attendance Module.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {
                                ISSchool obj1 = DB.ISSchools.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                                if (obj != null)
                                {
                                    obj1.CustomerNumber = obj.CustomerNumber;
                                    obj1.Name = obj.Name;
                                    obj1.Number = obj.Number;
                                    obj1.TypeID = obj.TypeID;
                                    obj1.Address1 = obj.Address1;
                                    obj1.Address2 = obj.Address2;
                                    obj1.Town = obj.Town;
                                    obj1.CountryID = obj.CountryID;
                                    obj1.Logo = obj.Logo;
                                    obj1.AdminFirstName = obj.AdminFirstName;
                                    obj1.AdminLastName = obj.AdminLastName;
                                    obj1.AdminEmail = obj.AdminEmail;
                                    obj1.Password = obj.Password;
                                    obj1.PhoneNumber = obj.PhoneNumber;
                                    obj1.Website = obj.Website;
                                    obj1.SupervisorFirstname = obj.SupervisorFirstname;
                                    obj1.SupervisorLastname = obj.SupervisorLastname;
                                    obj1.SupervisorEmail = obj.SupervisorEmail;
                                    obj1.OpningTime = obj.OpningTime;
                                    obj1.ClosingTime = obj.ClosingTime;
                                    obj1.LateMinAfterClosing = obj.LateMinAfterClosing;
                                    obj1.ChargeMinutesAfterClosing = obj.ChargeMinutesAfterClosing;
                                    obj1.ReportableMinutesAfterClosing = obj.ReportableMinutesAfterClosing;
                                    obj1.AccountManagerId = obj.AccountManagerId;
                                    obj1.SetupTrainingStatus = obj.SetupTrainingStatus;
                                    obj1.SetupTrainingDate = obj.SetupTrainingDate;
                                    obj1.ActivationDate = obj.ActivationDate;
                                    obj1.AccountStatusID = obj.AccountStatusID;
                                    obj1.SchoolEndDate = obj.SchoolEndDate;
                                    obj1.isAttendanceModule = obj.isAttendanceModule;
                                    obj1.isNotificationPickup = obj.isNotificationPickup;
                                    obj1.NotificationAttendance = obj.NotificationAttendance;
                                    obj1.AttendanceModule = obj.AttendanceModule;
                                    obj1.PostCode = obj.PostCode;
                                    obj1.BillingAddress = obj.BillingAddress;
                                    obj1.BillingAddress2 = obj.BillingAddress2;
                                    obj1.BillingTown = obj.BillingTown;
                                    obj1.BillingPostCode = obj.BillingPostCode;
                                    obj1.BillingCountryID = obj.BillingCountryID;
                                    obj1.Classfile = obj.Classfile;
                                    obj1.Teacherfile = obj.Teacherfile;
                                    obj1.Studentfile = obj.Studentfile;
                                    obj1.Reportable = obj.Reportable;
                                    obj1.PaymentSystems = obj.PaymentSystems;
                                    obj1.CustSigned = obj.CustSigned;
                                    //obj1.Active = obj.Active;
                                    obj1.ModifyBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                                    obj1.ModifyDateTime = DateTime.Now;
                                    obj1.LastUpdatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                                    DB.SaveChanges();

                                    EmailManage(OperationManagement.ID);
                                    Clear();
                                    AlertMessageManagement.ServerMessage(Page, "School Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                    OperationManagement.ID = 0;
                                    OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                                    Response.Redirect("SchoolList.aspx", false);
                                }
                            }
                        }
                        else
                        {
                            IList<ISSchool> obj2 = DB.ISSchools.Where(a => (a.Number == txtSchoolNo.Text || a.AdminEmail == txtAdminEmail.Text) && a.Active == true && a.Deleted == true).ToList();
                            if (obj2.Count > 0)
                            {
                                AlertMessageManagement.ServerMessage(Page, "This School Number Or Admin Email already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (Convert.ToInt32(drpSchoolType.SelectedValue) == 1 && !chkAttendanceModule.Checked)
                            {
                                AlertMessageManagement.ServerMessage(Page, "This School must have enable Activate Attendance Module.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {
                                int createdBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;

                                obj.Active = true;
                                obj.CustomerNumber = AutoIncrement();
                                obj.Deleted = true;
                                obj.CreatedBy = createdBy;
                                obj.CreatedByName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                                obj.CreatedDateTime = DateTime.Now;
                                DB.ISSchools.Add(obj);
                                DB.SaveChanges();


                                ISUserRole StandardRole = new ISUserRole();
                                StandardRole.SchoolID = obj.ID;
                                StandardRole.RoleName = "Standard";
                                StandardRole.RoleType = (int)EnumsManagement.ROLETYPE.TEACHING;
                                StandardRole.ManageTeacherFlag = false;
                                StandardRole.ManageStudentFlag = false;
                                StandardRole.ManageClassFlag = false;
                                StandardRole.ManageSupportFlag = false;
                                StandardRole.ManageViewAccountFlag = false;
                                StandardRole.Active = true;
                                StandardRole.Deleted = true;
                                StandardRole.CreatedBy = createdBy;
                                StandardRole.CreatedDateTime = DateTime.Now;
                                DB.ISUserRoles.Add(StandardRole);
                                DB.SaveChanges();

                                StandardRole = new ISUserRole();
                                StandardRole.SchoolID = obj.ID;
                                StandardRole.RoleName = "Admin";
                                StandardRole.RoleType = (int)EnumsManagement.ROLETYPE.TEACHING;
                                StandardRole.ManageTeacherFlag = true;
                                StandardRole.ManageStudentFlag = true;
                                StandardRole.ManageClassFlag = true;
                                StandardRole.ManageSupportFlag = true;
                                StandardRole.ManageViewAccountFlag = true;
                                StandardRole.ManageHolidayFlag = true;
                                StandardRole.ManageNonTeacherFlag = true;
                                StandardRole.Active = true;
                                StandardRole.Deleted = true;
                                StandardRole.CreatedBy = createdBy;
                                StandardRole.CreatedDateTime = DateTime.Now;
                                DB.ISUserRoles.Add(StandardRole);
                                DB.SaveChanges();

                                if (drpSchoolType.SelectedValue == "2")
                                {
                                    StandardRole = new ISUserRole();
                                    StandardRole.SchoolID = obj.ID;
                                    StandardRole.RoleName = "Admin";
                                    StandardRole.RoleType = (int)EnumsManagement.ROLETYPE.NONTEACHING;
                                    StandardRole.ManageTeacherFlag = true;
                                    StandardRole.ManageStudentFlag = true;
                                    StandardRole.ManageClassFlag = true;
                                    StandardRole.ManageSupportFlag = true;
                                    StandardRole.ManageViewAccountFlag = true;
                                    StandardRole.ManageHolidayFlag = true;
                                    StandardRole.ManageNonTeacherFlag = true;
                                    StandardRole.Active = true;
                                    StandardRole.Deleted = true;
                                    StandardRole.CreatedBy = createdBy;
                                    StandardRole.CreatedDateTime = DateTime.Now;
                                    DB.ISUserRoles.Add(StandardRole);
                                    DB.SaveChanges();

                                    StandardRole = new ISUserRole();
                                    StandardRole.SchoolID = obj.ID;
                                    StandardRole.RoleName = "Standard";
                                    StandardRole.RoleType = (int)EnumsManagement.ROLETYPE.NONTEACHING;
                                    StandardRole.ManageTeacherFlag = false;
                                    StandardRole.ManageStudentFlag = false;
                                    StandardRole.ManageClassFlag = false;
                                    StandardRole.ManageSupportFlag = false;
                                    StandardRole.ManageViewAccountFlag = false;
                                    StandardRole.Active = true;
                                    StandardRole.Deleted = true;
                                    StandardRole.CreatedBy = createdBy;
                                    StandardRole.CreatedDateTime = DateTime.Now;
                                    DB.ISUserRoles.Add(StandardRole);
                                    DB.SaveChanges();


                                    ISClass ObjClass = new ISClass();
                                    ObjClass.Name = "Office Class(Office)";
                                    ObjClass.TypeID = 5;
                                    ObjClass.SchoolID = obj.ID;
                                    ObjClass.Active = true;
                                    ObjClass.Deleted = true;
                                    ObjClass.CreatedBy = obj.ID;
                                    ObjClass.CreatedDateTime = DateTime.Now;
                                    DB.ISClasses.Add(ObjClass);
                                    DB.SaveChanges();

                                    ObjClass = new ISClass();
                                    ObjClass.Name = "Club Class(Club)";
                                    ObjClass.TypeID = 6;
                                    ObjClass.SchoolID = obj.ID;
                                    ObjClass.Active = true;
                                    ObjClass.Deleted = true;
                                    ObjClass.CreatedBy = obj.ID;
                                    ObjClass.CreatedDateTime = DateTime.Now;
                                    DB.ISClasses.Add(ObjClass);
                                    DB.SaveChanges();
                                }
                                //if (drpSchoolType.SelectedValue == "1")
                                //{
                                //    ISClass ObjClass = new ISClass();
                                //    ObjClass.Name = "Local Class";
                                //    ObjClass.TypeID = 1;
                                //    ObjClass.SchoolID = obj.ID;
                                //    ObjClass.Active = true;
                                //    ObjClass.Deleted = true;
                                //    ObjClass.CreatedBy = obj.ID;
                                //    ObjClass.CreatedDateTime = DateTime.Now;
                                //    DB.ISClasses.Add(ObjClass);
                                //    DB.SaveChanges();

                                //    ISClass ObjClasses = new ISClass();
                                //    ObjClasses.Name = "Outside Class";
                                //    ObjClasses.TypeID = 1;
                                //    ObjClasses.SchoolID = obj.ID;
                                //    ObjClasses.Active = true;
                                //    ObjClasses.Deleted = true;
                                //    ObjClasses.CreatedBy = obj.ID;
                                //    ObjClasses.CreatedDateTime = DateTime.Now;
                                //    DB.ISClasses.Add(ObjClasses);
                                //    DB.SaveChanges();
                                //}
                                Clear();
                                AlertMessageManagement.ServerMessage(Page, "School Added Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                Response.Redirect("SchoolList.aspx", false);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        //ErrorLogManagement.AddLog(Page, ex);
                    }
                }
            }
            if (Wizard1.ActiveStepIndex < 2 && isvalidate)
            {
                Wizard1.ActiveStepIndex = Wizard1.ActiveStepIndex + 1;
            }

        }
        public void EmailManage(int SchoolID)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == SchoolID);
            string LoggedINName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
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

            _EmailManagement.SendEmail(_School.AdminEmail, "Edit School Profile (Edited By Organisation)", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Edit School Profile (Edited By Organisation)", SuperwisorBody);

        }
        private void Clear()
        {
            try
            {
                txtSchoolWebsite.Text = "";
                txtSchoolNo.Text = "";
                txtSchoolName.Text = "";
                txtAdminFName.Text = "";
                txtAdminLName.Text = "";
                txtAdminEmail.Text = "";
                txtPassword.Text = "";
                txtSupFName.Text = "";
                txtSupLName.Text = "";
                txtSupEmail.Text = "";
                txtSchoolOpeningTime.Text = "";
                txtSchoolClosingTime.Text = "";
                drpLateAfterClose.SelectedValue = "0";
                drpReportMinsAfterClose.SelectedValue = "0";
                drpChargeAfterClose.SelectedValue = "0";
                //txtLateMins.Text = "";
                //txtChargeMins.Text = "";
                //txtReporatbleMins.Text = "";
                //txtEndDate.Text = "";
                txtPhone.Text = "";
                drpSchoolType.SelectedValue = "0";
                drpSchoolManagerType.SelectedValue = "0";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtTown.Text = "";
                txtPostCode.Text = "";
                drpCountry.SelectedValue = "";
                txtBillingAddress1.Text = "";
                txtBillingAddress2.Text = "";
                txtBillingPostCode.Text = "";
                txtBillingTown.Text = "";
                drpBillingCountry.SelectedValue = "0";
                chkPayment_Systems.Checked = false;
                ChkCustSigned.Checked = false;
                rblist.SelectedValue = "0";
                txtIntTrainingDate.Text = "";
                txtActivateDate.Text = "";
                chkAttendanceModule.Checked = false;
                ChkConfirmAttendance.Checked = false;
                chkConfirmPickUp.Checked = false;
                chkBilling.Checked = false;
                chkActive.Checked = true;
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }

        //private void SetField(ref ISSchool obj)
        //{
        //    try
        //    {
        //        obj.Website = txtSchoolWebsite.Text;
        //        obj.Number = txtSchoolNo.Text;
        //        obj.Name = txtSchoolName.Text;
        //        if (fpUploadLogo.HasFile == true)
        //        {
        //            string filename = System.IO.Path.GetFileName(fpUploadLogo.PostedFile.FileName);
        //            fpUploadLogo.SaveAs(Server.MapPath("Upload/" + filename));
        //            obj.Logo = "Upload/" + filename;
        //        }
        //        obj.AdminFirstName = txtAdminFName.Text;
        //        obj.AdminLastName = txtAdminLName.Text;
        //        obj.AdminEmail = txtAdminEmail.Text;
        //        obj.Password = EncryptionHelper.Encrypt(txtPassword.Text);
        //        obj.SupervisorFirstname = txtSupFName.Text;
        //        obj.SupervisorLastname = txtSupLName.Text;
        //        obj.SupervisorEmail = txtSupEmail.Text;
        //        string OpenTime = txtSchoolOpeningTime.Text;
        //        if (OpenTime.Contains(":"))
        //        {
        //            string[] arrDate = OpenTime.Split(':');
        //            string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
        //            obj.OpningTime = DateTime.Parse(DTS);
        //        }
        //        string CloseTime = txtSchoolClosingTime.Text;
        //        if (CloseTime.Contains(":"))
        //        {
        //            string[] arrDate = CloseTime.Split(':');
        //            string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
        //            obj.ClosingTime = DateTime.Parse(DTS);
        //        }
        //        obj.LateMinAfterClosing = drpLateAfterClose.SelectedValue;//txtLateMins.Text;
        //        obj.ChargeMinutesAfterClosing = drpChargeAfterClose.SelectedValue; //txtChargeMins.Text;
        //        obj.ReportableMinutesAfterClosing = drpReportMinsAfterClose.SelectedValue; //txtReporatbleMins.Text;
        //        DateTime date = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", null);
        //        obj.SchoolEndDate = date; //DateTime.Parse(txtEndDate.Text);
        //        obj.PhoneNumber = txtPhone.Text;
        //        obj.TypeID = Convert.ToInt32(drpSchoolType.SelectedValue);
        //        obj.Address1 = txtAddress1.Text;
        //        obj.Address2 = txtAddress2.Text;
        //        obj.Town = txtTown.Text;
        //        obj.PostCode = txtPostCode.Text;
        //        obj.CountryID = Convert.ToInt32(drpCountry.SelectedValue);
        //        if (chkBilling.Checked == true)
        //        {
        //            obj.BillingAddress = txtAddress1.Text;
        //            obj.BillingAddress2 = txtAddress2.Text;
        //            obj.BillingTown = txtTown.Text;
        //            obj.BillingPostCode = txtPostCode.Text;
        //            obj.BillingCountryID = Convert.ToInt32(drpCountry.SelectedValue);
        //        }
        //        else
        //        {
        //            obj.BillingAddress = null;
        //            obj.BillingAddress2 = null;
        //            obj.BillingTown = null;
        //            obj.BillingPostCode = null;
        //            obj.BillingCountryID = null;
        //        }
        //        obj.PaymentSystems = chkPayment_Systems.Checked == true ? true : false;
        //        obj.CustSigned = ChkCustSigned.Checked == true ? true : false;
        //        obj.SetupTrainingStatus = rblist.SelectedValue == "1" ? true : false;
        //        DateTime Traindate = DateTime.ParseExact(txtIntTrainingDate.Text, "dd/MM/yyyy", null);
        //        obj.SetupTrainingDate = Traindate; //DateTime.Parse(txtIntTrainingDate.Text);
        //        DateTime Actdate = DateTime.ParseExact(txtActivateDate.Text, "dd/MM/yyyy", null);
        //        obj.ActivationDate = Actdate; //DateTime.Parse(txtActivateDate.Text);
        //        obj.isAttendanceModule = chkAttendanceModule.Checked == true ? true : false;
        //        obj.NotificationAttendance = ChkConfirmAttendance.Checked == true ? true : false;
        //        obj.isNotificationPickup = chkConfirmPickUp.Checked == true ? true : false;
        //        if (FpUploadClasses.HasFile == true)
        //        {
        //            string filename = System.IO.Path.GetFileName(FpUploadClasses.PostedFile.FileName);
        //            FpUploadClasses.SaveAs(Server.MapPath("Upload/" + filename));
        //            obj.Classfile = "Upload/" + filename;
        //        }
        //        if (FpUploadTeachers.HasFile == true)
        //        {
        //            string filename = System.IO.Path.GetFileName(FpUploadTeachers.PostedFile.FileName);
        //            FpUploadTeachers.SaveAs(Server.MapPath("Upload/" + filename));
        //            obj.Teacherfile = "Upload/" + filename;
        //        }
        //        if (FpUploadStudents.HasFile == true)
        //        {
        //            string filename = System.IO.Path.GetFileName(FpUploadStudents.PostedFile.FileName);
        //            FpUploadStudents.SaveAs(Server.MapPath("Upload/" + filename));
        //            obj.Studentfile = "Upload/" + filename;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //ErrorLogManagement.AddLog(Page, ex);
        //    }
        //}

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (Wizard1.ActiveStepIndex > 0)
                {
                    Wizard1.ActiveStepIndex = Wizard1.ActiveStepIndex - 1;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }
        protected void CheckBoxRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = chkPayment_Systems.Checked;
        }

        protected void ChkRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ChkCustSigned.Checked;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (OperationManagement.Opration == (int)OperationManagement.OPERRATION.Edit && Convert.ToInt32(drpSchoolType.SelectedValue) == 2)
            {
                args.IsValid = false;// AlertMessageManagement.ServerMessage(Page, "Attendance module you can not changed it.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
                args.IsValid = true;
            //args.IsValid = chkAttendanceModule.Checked;
        }

        protected void CustomChkConfirmAttendance_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ChkConfirmAttendance.Checked;
        }
        protected void CustomChkConfirmPickUp_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = chkConfirmPickUp.Checked;
        }
        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (!IsValid)
            {
                e.Cancel = true;
            }
        }

        protected void Wizard1_PreRender(object sender, EventArgs e)
        {
            Repeater SideBarList = Wizard1.FindControl("HeaderContainer").FindControl("SideBarList") as Repeater;
            SideBarList.DataSource = Wizard1.WizardSteps;
            SideBarList.DataBind();

            if (chkAttendanceModule.Enabled && Convert.ToInt32(drpSchoolType.SelectedValue) == 2)
            {
                chkAttendanceModule.Enabled = false;
                chkAttendanceModule.ToolTip = "Standard school should not required to make attandance complusary.";
                divAttend.Disabled = true;
            }
            else
            {
                chkAttendanceModule.Enabled = true;
                chkAttendanceModule.ToolTip = "";
                divAttend.Disabled = false;
            }


        }

        protected string GetClassForWizardStep(object wizardStep)
        {

            WizardStep step = wizardStep as WizardStep;

            if (step == null)
            {
                return "";
            }
            int stepIndex = Wizard1.WizardSteps.IndexOf(step);

            if (stepIndex < Wizard1.ActiveStepIndex)
            {
                return "prevStep";
            }
            else if (stepIndex > Wizard1.ActiveStepIndex)
            {
                return "nextStep";
            }
            else
            {
                return "currentStep";
            }
        }

        protected void chkBilling_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBilling.Checked == true)
            {
                txtBillingAddress1.ReadOnly = true;
                txtBillingAddress2.ReadOnly = true;
                txtBillingPostCode.ReadOnly = true;
                txtBillingTown.ReadOnly = true;
                drpBillingCountry.Enabled = false;
                txtBillingAddress1.Text = txtAddress1.Text;
                txtBillingAddress2.Text = txtAddress2.Text;
                txtBillingPostCode.Text = txtPostCode.Text;
                txtBillingTown.Text = txtTown.Text;
                drpBillingCountry.SelectedValue = drpCountry.SelectedValue;
            }
            else
            {
                txtBillingAddress1.ReadOnly = false;
                txtBillingAddress2.ReadOnly = false;
                txtBillingPostCode.ReadOnly = false;
                txtBillingTown.ReadOnly = false;
                drpBillingCountry.Enabled = true;
                txtBillingAddress1.Text = "";
                txtBillingAddress2.Text = "";
                txtBillingPostCode.Text = "";
                txtBillingTown.Text = "";
                drpBillingCountry.SelectedValue = "0";
            }
        }

        protected void chkAttendanceModule_CheckedChanged(object sender, EventArgs e)
        {
            //if (OperationManagement.Opration == (int)OperationManagement.OPERRATION.Edit && Convert.ToInt32(drpSchoolType.SelectedValue) == 2)
            //{
            //    AlertMessageManagement.ServerMessage(Page, "Attendance module you can not changed it.", (int)AlertMessageManagement.MESSAGETYPE.warning);                
            //}
        }




        //public static List<string> GetCountryList()
        //{
        //    List<string> cultureList = new List<string>();
        //    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
        //    foreach (CultureInfo culture in cultures)
        //    {
        //        RegionInfo region = new RegionInfo(culture.LCID);
        //        if (!(cultureList.Contains(region.EnglishName)))
        //            cultureList.Add(region.EnglishName);
        //    }
        //    return cultureList;
        //}

        //foreach(string country in GetCountryList())
        //{
        //    comboBox1.Items.Add(country);
        //}
    }

}