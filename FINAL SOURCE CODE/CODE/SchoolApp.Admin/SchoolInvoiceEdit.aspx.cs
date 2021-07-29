using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SchoolInvoiceEdit : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
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
                    ltrtitle.Text = "School APP : SchoolInvoiceEdit";
                    if (Request.QueryString["ID"] != null)
                    {
                        OperationManagement.ID = Convert.ToInt32(Request.QueryString["ID"]);
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.Edit;
                        ISSchoolInvoice obj = DB.ISSchoolInvoices.SingleOrDefault(p => p.ID == OperationManagement.ID);
                        if (obj != null)
                        {
                            lblSchoolName.Text = obj.ISSchool.Name;
                            lblInvoiceNo.Text = obj.InvoiceNo;
                            if (obj.DateFrom != null)
                            {
                                lblInvFromDate.Text = obj.DateFrom.Value.ToString("dd/MM/yyyy");
                            }
                            if (obj.DateTo != null)
                            {
                                lblInvToDate.Text = obj.DateTo.Value.ToString("dd/MM/yyyy");
                            }
                            lblTranDesc.Text = obj.TransactionDesc;
                            lblAmount.Text = obj.TransactionAmount.ToString();
                            if (obj.TaxRate != null)
                            {
                                lblRate.Text = obj.TaxRate.ToString();
                            }
                            lblTransactionType.Text = obj.ISTrasectionType.Name;
                            drpStatus.SelectedValue = obj.StatusID.ToString();
                        }
                    }
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
            drpStatus.DataSource = DB.ISTrasectionStatus.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpStatus.DataTextField = "Name";
            drpStatus.DataValueField = "ID";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Transaction Status", Value = "0" });
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (OperationManagement.ID != 0 && OperationManagement.Opration == (int)OperationManagement.OPERRATION.Edit)
                {
                    IList<ISSchoolInvoice> obj2 = DB.ISSchoolInvoices.Where(a => a.ID != OperationManagement.ID && a.InvoiceNo == lblInvoiceNo.Text && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        AlertMessageManagement.ServerMessage(Page, "This Invoice No already Exist", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISSchoolInvoice obj = DB.ISSchoolInvoices.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                        if (obj != null)
                        {
                            SetField(ref obj);
                            obj.ModifyBy = Authentication.LoggedInUser.ID;
                            obj.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                            Clear();
                            AlertMessageManagement.ServerMessage(Page, "School Invoice Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            OperationManagement.ID = 0;
                            OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                            Response.Redirect("SchoolInvoiceList.aspx",false);
                        }
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
                lblSchoolName.Text = "";
                lblInvoiceNo.Text = "";
                lblInvFromDate.Text = "";
                lblInvToDate.Text = "";
                lblTranDesc.Text = "";
                lblRate.Text = "";
                lblAmount.Text = "";
                lblTransactionType.Text = "";
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
                int SID = Convert.ToInt32(drpStatus.SelectedValue);
                if (obj.StatusID != SID)
                {
                    obj.StatusUpdateBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                    obj.StatusUpdateDate = DateTime.Now;
                }
                obj.StatusID = SID;
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
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }
    }
}