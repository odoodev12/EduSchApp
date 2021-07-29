using NReco.PdfGenerator;
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
    public partial class Payment : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {
                if (Session["FROMDATE"] != null)
                {
                    DateTime myDt = Convert.ToDateTime(Session["FROMDATE"]);
                    txtDateFrom.Text = myDt.ToString("yyyy-MM-dd");
                }
                if (Session["TODATE"] != null)
                {
                    DateTime myDt = Convert.ToDateTime(Session["TODATE"]);
                    txtDateTo.Text = myDt.ToString("yyyy-MM-dd");
                }
                BindList(txtDateFrom.Text, txtDateTo.Text, "0", "", "0");
                BindDropdown();
            }
        }

        private void BindDropdown()
        {
            drpStatus.DataSource = DB.ISTrasectionStatus.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpStatus.DataTextField = "Name";
            drpStatus.DataValueField = "ID";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Transaction Status", Value = "0" });
        }

        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageViewAccountFlag == true)
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
        private void BindList(string FromDate, string ToDate, string Status, string OrderBy, string SortBy)
        {
            PaymentManagement objPaymentManagement = new PaymentManagement();
            List<MISSchoolInvoice> objList = objPaymentManagement.InvoicesList(Authentication.SchoolID, FromDate, ToDate, Status, OrderBy, SortBy);
            if (objList.Count > 0)
            {
                lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Visible = true;
            }
            ListView1.DataSource = objList.ToList();
            ListView1.DataBind();
        }
        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)ListView1.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(txtDateFrom.Text, txtDateTo.Text, drpStatus.SelectedValue, rbtnAsending.Checked == true ? "ASC" : "DESC", ddlSortBy.SelectedValue);
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (txtDateFrom.Text != "")
            {
                Session["FROMDATE"] = txtDateFrom.Text;
            }
            if (txtDateTo.Text != "")
            {
                Session["TODATE"] = txtDateTo.Text;
            }
            BindList(txtDateFrom.Text, txtDateTo.Text, drpStatus.SelectedValue, rbtnAsending.Checked == true ? "ASC" : "DESC", ddlSortBy.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            rbtnDescending.Checked = true;
            drpStatus.SelectedValue = "0";
            ddlSortBy.SelectedValue = "Date";
            Session["FROMDATE"] = null;
            Session["TODATE"] = null;
            BindList("", "", "0", "", "Date");
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnView")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                string body = string.Empty;
                string tblbody = string.Empty;


                ISSchoolInvoice ObjInvoice = DB.ISSchoolInvoices.SingleOrDefault(p => p.ID == ID);
                
                if (ObjInvoice != null)
                {
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjInvoice.SchoolID && p.Deleted == true);

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/invoice.html")))
                    {
                        body += reader.ReadToEnd();
                    }
                    decimal TaxAmount = Convert.ToDecimal((ObjInvoice.TaxRate * ObjInvoice.TransactionAmount) / 100);
                    body = body.Replace("{InvoiceNumber}", ObjInvoice.InvoiceNo);
                    body = body.Replace("{CreatedDate}", ObjInvoice.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("{Due}", ObjInvoice.DateTo.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("{AddressSchool1}", ObjSchool.BillingAddress);
                    body = body.Replace("{AddressSchool2}", ObjSchool.BillingAddress2);
                    body = body.Replace("{TransactionType}", ObjInvoice.ISTrasectionType.Name);
                    body = body.Replace("{Desc}", ObjInvoice.TransactionDesc);
                    body = body.Replace("{Amount}", ObjInvoice.TransactionAmount.ToString());
                    body = body.Replace("{TaxRate}", ObjInvoice.TaxRate.ToString());
                    body = body.Replace("{TaxAmount}", Convert.ToString(TaxAmount));
                    body = body.Replace("{TotalAmount}", Convert.ToString(TaxAmount + ObjInvoice.TransactionAmount));


                    var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.
                    pdfDoc.Orientation = PageOrientation.Portrait;
                    pdfDoc.Size = PageSize.A4;   //8.27 in × 11.02 in //Page Size
                    PageMargins pageMargins = new PageMargins();     //Margins in mm
                    pageMargins.Bottom = 05;
                    pageMargins.Left = 05;
                    pageMargins.Right = 05;
                    pageMargins.Top = 05;
                    pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                    string content = body;
                    //pdfDoc.PageFooterHtml = "<div style='float:right;'></div>";
                    var pdfBytes = pdfDoc.GeneratePdf(content);

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SchoolInvoice.pdf");//Use inline in place of attachment If You wish to open PDF on Browser.
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.BinaryWrite(pdfBytes);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "Invoice Can not Generated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
        }
    }
}