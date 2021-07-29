using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SchoolInvoiceAdd : System.Web.UI.Page
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
                        if (Authentication.LoggedInOrgUser.RoleID == 2 || Authentication.LoggedInOrgUser.RoleID == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
                if (!IsPostBack)
                {   
                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : SchoolInvoiceAdd";
                    BindDropDown();
                }

            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }


        private void BindDropDown()
        {
            drpTransactionTypeID.DataSource = DB.ISTrasectionTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpTransactionTypeID.DataTextField = "Name";
            drpTransactionTypeID.DataValueField = "ID";
            drpTransactionTypeID.DataBind();
            drpTransactionTypeID.Items.Insert(0, new ListItem { Text = "Select Transaction Type", Value = "0" });

            drpStatus.DataSource = DB.ISTrasectionStatus.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpStatus.DataTextField = "Name";
            drpStatus.DataValueField = "ID";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Transaction Status", Value = "0" });

            drpSchool.DataSource = DB.ISSchools.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpSchool.DataTextField = "Name";
            drpSchool.DataValueField = "ID";
            drpSchool.DataBind();
            drpSchool.Items.Insert(0, new ListItem { Text = "Select School", Value = "0" });
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IList<ISSchoolInvoice> obj2 = DB.ISSchoolInvoices.Where(a => a.InvoiceNo == txtInvoiceNo.Text && a.Active == true && a.Deleted == true).ToList();
                if (obj2.Count > 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "This InvoiceNo already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    ISSchoolInvoice obj = new ISSchoolInvoice();
                    SetField(ref obj);
                    obj.Active = true;
                    obj.Deleted = true;
                    obj.CreatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                    obj.CreatedByName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                    obj.CreatedDateTime = DateTime.Now;
                    DB.ISSchoolInvoices.Add(obj);
                    DB.SaveChanges();
                    int SchoolID = Convert.ToInt32(drpSchool.SelectedValue);
                    ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);


                    Clear();
                    AlertMessageManagement.ServerMessage(Page, "School Invoice Added Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    Response.Redirect("SchoolInvoiceList.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        public void EmailManage(int SchoolID, ISSchoolInvoice _Invoice)
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
                                    New Invoice Created : " + _Invoice.InvoiceNo + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Invoice Issue Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Invoice Due Date : " + _Invoice.DateTo.Value.ToString("dd/MM/yyyy") + @"
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
                                    New Invoice Created : " + _Invoice.InvoiceNo + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Invoice Issue Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Invoice Due Date : " + _Invoice.DateTo.Value.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "Create Payment Notification", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "Create Payment Notification", SuperwisorBody);

        }
        private void Clear()
        {
            try
            {
                drpSchool.SelectedValue = "0";
                txtInvoiceNo.Text = "";
                txtDateFrom.Text = "";
                txtDateTo.Text = "";
                txtTransactionDesc.Text = "";
                txtTaxRate.Text = "";
                txtTransactionAmount.Text = "";
                drpTransactionTypeID.SelectedValue = "0";
                drpStatus.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void SetField(ref ISSchoolInvoice obj)
        {
            try
            {
                obj.SchoolID = Convert.ToInt32(drpSchool.SelectedValue);
                obj.InvoiceNo = txtInvoiceNo.Text;
                if (txtDateFrom.Text != "")
                {
                    DateTime DateFrom = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", null);
                    obj.DateFrom = DateFrom;//DateTime.Parse(txtDateFrom.Text);
                }
                if (txtDateTo.Text != "")
                {
                    DateTime DateTo = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", null);
                    obj.DateTo = DateTo; //DateTime.Parse(txtDateTo.Text);
                }
                obj.TransactionTypeID = Convert.ToInt32(drpTransactionTypeID.SelectedValue);
                obj.TransactionDesc = txtTransactionDesc.Text;
                obj.TransactionAmount = Convert.ToInt32(txtTransactionAmount.Text);
                if (txtTaxRate.Text != "")
                {
                    obj.TaxRate = Convert.ToDecimal(txtTaxRate.Text);
                }
                obj.StatusID = Convert.ToInt32(drpStatus.SelectedValue);
                obj.StatusUpdateBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                obj.StatusUpdateDate = DateTime.Now;
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
                Response.Redirect("SchoolInvoiceList.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
    }
}