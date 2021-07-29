using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class SchoolInvoiceList : System.Web.UI.Page
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
                        if (Authentication.LoggedInOrgUser.RoleID == 2)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
               
                if (!Page.IsPostBack)
                {
                    BindSchoolFilter();
                    BindTransactionType();
                    BindTranStatus();
                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : School Invoice List";
                    Session["Objectlist"] = null;
                    OperationManagement.ID = 0;
                    OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                    BindData("",0,0,0);
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void BindSchoolFilter()
        {
            List<MISSchoolInvoice> ObjList = (from item in DB.ISSchoolInvoices.Where(p => p.Deleted == true).ToList()
                                              select new MISSchoolInvoice
                                              {
                                                  ID = item.ID,
                                                  InvoiceNumber = item.InvoiceNo,
                                                  strTransectionType = item.ISTrasectionType.Name,
                                                  DateFrom = item.DateFrom,
                                                  DateTo = item.DateTo,
                                                  Transaction_Amount = item.TransactionAmount.ToString(),
                                                  TransactionAmount = item.TransactionAmount,
                                                  TaxRate = item.TaxRate,
                                                  TaxRates = item.TaxRate != null ? item.TaxRate.ToString() : "N/A",
                                                  Description = item.TransactionDesc,
                                                  strStatus = item.ISTrasectionStatu.Name,
                                                  strCreatedDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt"),
                                                  CreatedBy = item.CreatedBy,
                                                  CreatedDateTime = item.CreatedDateTime.Value.Date,
                                                  StatusID = item.StatusID,
                                                  SchoolID = item.SchoolID,
                                                  SchoolName = item.ISSchool.Name,
                                                  Active = item.Active
                                              }).ToList();
            drpSchool.DataSource = ObjList;
            drpSchool.DataTextField = "SchoolName";
            drpSchool.DataValueField = "SchoolID";
            drpSchool.DataBind();
            drpSchool.Items.Insert(0, new ListItem { Text = "Select School", Value = "0" });
        }

        private void BindTransactionType()
        {
            drpTransactionTypeID.DataSource = DB.ISTrasectionTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpTransactionTypeID.DataTextField = "Name";
            drpTransactionTypeID.DataValueField = "ID";
            drpTransactionTypeID.DataBind();
            drpTransactionTypeID.Items.Insert(0, new ListItem { Text = "Select Transaction Type", Value = "0" });
        }

        private void BindTranStatus()
        {   
            drpTranStatus.DataSource = DB.ISTrasectionStatus.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpTranStatus.DataTextField = "Name";
            drpTranStatus.DataValueField = "ID";
            drpTranStatus.DataBind();
            drpTranStatus.Items.Insert(0, new ListItem { Text = "Select Transaction Status", Value = "0" });
        }

        private void BindData(string Code, int schoolId,int transTypeId, int transStatusId)
        {
            try
            {
                List<MISSchoolInvoice> ObjList = (from item in DB.ISSchoolInvoices.Where(p => p.Deleted == true).ToList()
                                                  select new MISSchoolInvoice
                                                  {
                                                      ID = item.ID,
                                                      InvoiceNumber = item.InvoiceNo,
                                                      strTransectionType = item.ISTrasectionType.Name,
                                                      DateFrom = item.DateFrom,
                                                      DateTo = item.DateTo,
                                                      Transaction_Amount = item.TransactionAmount.ToString(),
                                                      TransactionAmount = item.TransactionAmount,
                                                      TaxRate = item.TaxRate,
                                                      TaxRates = item.TaxRate != null ? item.TaxRate.ToString() : "N/A",
                                                      Description = item.TransactionDesc,
                                                      strStatus = item.ISTrasectionStatu.Name,
                                                      TransactionTypeID = item.TransactionTypeID,
                                                      strCreatedDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt"),
                                                      CreatedBy = item.CreatedBy,
                                                      CreatedDateTime = item.CreatedDateTime.Value.Date,
                                                      StatusID = item.StatusID,
                                                      SchoolID = item.SchoolID,
                                                      SchoolName = item.ISSchool.Name,
                                                      Active = item.Active
                                                  }).ToList();

                var result = new List<MISSchoolInvoice>();

                if (Code != "")
                {
                    result.AddRange(ObjList.Where(p => p.InvoiceNumber == Code).ToList());
                }
                if(schoolId!=0)
                    result.AddRange(ObjList.Where(p => p.SchoolID == schoolId).ToList());
                if (transTypeId != 0)
                    result.AddRange(ObjList.Where(p => p.TransactionTypeID == transTypeId).ToList());
                if (transStatusId != 0)
                    result.AddRange(ObjList.Where(p => p.StatusID == transStatusId).ToList());


                if (Code == "" && schoolId == 0 && transTypeId == 0 && transStatusId == 0)
                    result = ObjList;

                result = result.Select(r => r).Distinct().ToList();

                GvSchoolInvoiceList.DataSource = result.OrderByDescending(p => p.CreatedDateTime).ToList();
                GvSchoolInvoiceList.DataBind();
                Session["Objectlist"] = result.ToList();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SchoolInvoiceAdd.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(txtSchoolName.Text,Convert.ToInt32(drpSchool.SelectedValue), Convert.ToInt32(drpTransactionTypeID.SelectedValue), Convert.ToInt32(drpTranStatus.SelectedValue));
                PanelManagement.OpenPanel(ref collapseOne1);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                PanelManagement.ClosePanel(ref collapseOne1);
                BindData("",0,0,0);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        private void Clear()
        {
            try
            {
                txtSchoolName.Text = "";
                drpSchool.SelectedIndex = 0;
                drpTranStatus.SelectedIndex = 0;
                drpTransactionTypeID.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvSchoolInvoiceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnEdit")
                {
                    if (GetUserType() == "2" || GetUserType() == "4" || GetAdmin() == true)
                    {
                        int ID = Convert.ToInt32(e.CommandArgument);
                        Response.Redirect("SchoolInvoiceEdit.aspx?ID=" + ID);
                    }
                }
                if (e.CommandName == "btnDelete")
                {
                    OperationManagement.ID = Convert.ToInt32(e.CommandArgument);
                    ISSchoolInvoice obj = DB.ISSchoolInvoices.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                    if (obj != null)
                    {
                        obj.Active = false;
                        obj.Deleted = false;
                        obj.DeletedBy = Authentication.LoggedInUser.ID;
                        obj.DeletedDateTime = DateTime.Now;
                        DB.SaveChanges();
                        BindData("",0,0,0);
                        AlertMessageManagement.ServerMessage(Page, "School Invoice Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public string GetUserType()
        {
            if (Authentication.LoggedInOrgUser != null)
            {
                return Authentication.LoggedInOrgUser.RoleID.ToString();
            }
            else
            {
                return "0";
            }
        }
        public bool GetAdmin()
        {
            if (Authentication.ISUserLogin() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void GvSchoolInvoiceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvSchoolInvoiceList.PageIndex = e.NewPageIndex;
                if (txtSchoolName.Text != "")
                {
                    BindData(txtSchoolName.Text, Convert.ToInt32(drpSchool.SelectedValue), Convert.ToInt32(drpTransactionTypeID.SelectedValue), Convert.ToInt32(drpTranStatus.SelectedValue));
                }
                else
                {
                    BindData("",0,0,0);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("Upload") + "/SchoolInvoiceList.xlsx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void CreateCell(Row rw, String value)
        {
            try
            {
                Cell newCell = new Cell();
                rw.Append(newCell);
                newCell.CellValue = new CellValue(value);
                newCell.DataType = new EnumValue<CellValues>(CellValues.String);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void CreateSpreadsheetWorkbook(string filepath)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = (List<ISSchoolInvoice>)Session["Objectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("SchoolInvoiceList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "ID");
                CreateCell(Hederrow, "SchoolName");
                CreateCell(Hederrow, "InvoiceNo");
                CreateCell(Hederrow, "TransactionTypeID");
                CreateCell(Hederrow, "TransactionDesc");
                CreateCell(Hederrow, "TransactionAmount");
                CreateCell(Hederrow, "TaxRate");
                CreateCell(Hederrow, "Status");

                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.ISSchool.Name);
                    CreateCell(row, item.InvoiceNo);
                    CreateCell(row, item.ISTrasectionType.Name);
                    CreateCell(row, item.TransactionDesc);
                    CreateCell(row, item.TransactionAmount.ToString());
                    CreateCell(row, item.TaxRate != null ? item.TaxRate.ToString() : "");
                    CreateCell(row, item.ISTrasectionStatu.Name);

                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=OrganisationList.xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                ///GvUserList.HeaderStyle.Font.Bold = true;
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        
    }
}