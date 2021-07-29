using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Reports
{
    public partial class CreatedAdminRoleReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        RolesManagement rolemanagement = new RolesManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!Page.IsPostBack)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator157"] = r.ToString();
                //bindData("", "");
            }
        }
        public void bindData(string DateFrom, string DateTo)
        {
            Session["CreatedAdminRoleReport"] = null;
            List<MISUserRole> ObjList = (from item in DB.ISUserRoles.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                         select new MISUserRole
                                         {
                                             ID = item.ID,
                                             RoleName = item.RoleName,
                                             strCreateBy = rolemanagement.GetCreatedBy(item.CreatedBy.Value),
                                             CreatedDateTime = item.CreatedDateTime,
                                             strCreatedDate = item.CreatedDateTime != null ? item.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "",
                                             ManageClassFlag = item.ManageClassFlag,
                                             ManageStudentFlag = item.ManageStudentFlag,
                                             ManageSupportFlag = item.ManageSupportFlag,
                                             ManageTeacherFlag = item.ManageTeacherFlag,
                                             ManageViewAccountFlag = item.ManageViewAccountFlag,
                                             ActivatedFunctions = ActivatedFunctions(item.ManageClassFlag, item.ManageStudentFlag, item.ManageSupportFlag, item.ManageTeacherFlag, item.ManageViewAccountFlag)
                                         }).ToList();
            if (DateFrom != "")
            {
                DateTime dt = Convert.ToDateTime(DateFrom);
                ObjList = ObjList.Where(p => p.CreatedDateTime.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                DateTime dt = Convert.ToDateTime(DateTo);
                ObjList = ObjList.Where(p => p.CreatedDateTime.Value.Date <= dt.Date).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList.ToList();
            lstTable.DataBind();
            Session["CreatedAdminRoleReport"] = ObjList;
        }
        public string ActivatedFunctions(bool ManageClass, bool ManageStudent, bool ManageSupport, bool ManageTeacher, bool ManageViewAccount)
        {
            string str = "";
            if (ManageClass == true)
            {
                str += "Manage Class,";

            }
            if (ManageStudent == true)
            {
                str += " Manage Student,";
            }
            if (ManageSupport == true)
            {
                str += " Manage Support,";
            }
            if (ManageTeacher == true)
            {
                str += " Manage Teacher,";
            }
            if (ManageViewAccount == true)
            {
                str += " Manage Account,";
            }
            if (str != "")
            {
                str = str.Remove(str.Length - 1);
            }

            return str;
        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text);
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            bindData("", "");
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/CreatedAdminRoleReport" + Session["Generator157"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isFileDownLoadRequired = true)
        {
            string returnFilePath = "";
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                ///Adding Sheet here
                Row Hederrow;
                var lists = (List<MISUserRole>)Session["CreatedAdminRoleReport"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("CreatedAdminRoleReport" + Session["Generator157"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Admin Role Name");
                CreateCell(Hederrow, "Ceated By");
                CreateCell(Hederrow, "Activated Functions");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.strCreatedDate);
                    CreateCell(row, item.RoleName);
                    CreateCell(row, item.strCreateBy);
                    CreateCell(row, item.ActivatedFunctions);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownLoadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CreatedAdminRoleReport" + Session["Generator157"].ToString() + ".xlsx");
                    Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.TransmitFile(fileinfo.FullName);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

            return returnFilePath;
        }

        protected void linkSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/CreatedAdminRoleReport" + Session["Generator157"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}