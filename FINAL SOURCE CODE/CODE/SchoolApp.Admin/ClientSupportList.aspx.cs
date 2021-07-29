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
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SchoolApp.Admin
{
    public partial class ClientSupport : System.Web.UI.Page
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
                        if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 2)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
                if (!Page.IsPostBack)
                {
                    BindPriority();
                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : Client Support List";
                    Session["Objectlist"] = null;
                    OperationManagement.ID = 0;
                    OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                    BindData("", "None", "");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void BindData(string Code, string priority, string createdByName)
        {
            try
            {
                List<MISSupport> objList = (from item in DB.ISSupports.Where(p => p.Deleted == true && p.Active == true).ToList()
                                            select new MISSupport
                                            {
                                                ID = item.ID,
                                                TicketNumber = item.TicketNo,
                                                TicketNo = item.TicketNo,
                                                SDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt"),
                                                STime = item.CreatedDateTime.Value.ToString("hh:mm tt"),
                                                Subject = item.Request,
                                                StatusID = item.StatusID,
                                                Status = item.ISSupportStatu.Name,
                                                CreatedByName = item.CreatedByName,
                                                CreatedDateTime = item.CreatedDateTime,
                                                SupportOfficerID = item.SupportOfficerID != null ? item.SupportOfficerID : 0,
                                                Priorities = item.Priority == 1 ? "Critical" : item.Priority == 2 ? "High" : item.Priority == 3 ? "Medium" : item.Priority == 4 ? "Low" : "None",
                                                SupportOfficer = item.SupportOfficerID != null ? DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == item.SupportOfficerID).FirstName + " " + DB.ISOrganisationUsers.SingleOrDefault(p => p.ID == item.SupportOfficerID).LastName : "",
                                            }).ToList();

                var result = new List<MISSupport>();

                if (GetUserType() == "3")
                {
                    result.AddRange(objList.Where(p => p.SupportOfficerID == Authentication.LoggedInOrgUser.ID).ToList());
                }
                if (priority != "None")
                    result.AddRange(objList.Where(p => p.Priorities == priority).ToList());
                if (createdByName != "")
                    result.AddRange(objList.Where(p => p.CreatedByName.Contains(createdByName)).ToList());

                if (GetUserType() != "3" && priority == "None" && createdByName == "")
                    result = objList;

                //if (Code != "")
                //{
                //    objList = objList.Where(p => p.O == Code).ToList();
                //}

                result = result.Select(r => r).Distinct().ToList();

                GvClientSupportList.DataSource = result.OrderByDescending(p => p.CreatedDateTime).ToList();
                GvClientSupportList.DataBind();
                Session["Objectlist"] = result.ToList();
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        private void BindPriority()
        {
            List<string> priorityList = new List<string> { "Critical", "High", "Medium", "Low" };
            drpPriority.DataSource = priorityList.ToList();
            drpPriority.DataBind();
            drpPriority.Items.Insert(0, new ListItem { Text = "Select Priority", Value = "None" });
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(txtTicketNo.Text, drpPriority.SelectedValue, txtCreatedBy.Text);
                PanelManagement.OpenPanel(ref collapseOne1);
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                PanelManagement.ClosePanel(ref collapseOne1);
                BindData("", "None", "");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        private void Clear()
        {
            try
            {
                txtCreatedBy.Text = txtTicketNo.Text = "";
                drpPriority.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        protected void GvClientSupportList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnView")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("TicketMessage.aspx?ID=" + ID);
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvClientSupportList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvClientSupportList.PageIndex = e.NewPageIndex;
                if (txtTicketNo.Text != "")
                {
                    BindData(txtTicketNo.Text, drpPriority.SelectedValue, txtCreatedBy.Text);
                }
                else
                {
                    BindData("", "None", "");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("Upload") + "/ClientSupportList.xlsx");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
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
                //ErrorLogManagement.AddLog(ex);
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
                var lists = (List<ISSupport>)Session["Objectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ClientSupportList").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Ticket No");
                CreateCell(Hederrow, "Request");
                CreateCell(Hederrow, "CreatedBy");
                CreateCell(Hederrow, "CreatedDateTime");

                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.TicketNo);
                    CreateCell(row, item.Request);
                    CreateCell(row, item.CreatedBy.ToString());
                    CreateCell(row, item.CreatedDateTime.ToString());

                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=ClientSupportList.xlsx");
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
                //ErrorLogManagement.AddLog(ex);
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
    }
}