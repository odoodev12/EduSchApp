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

namespace SchoolApp.Web.Reports
{
    public partial class TeacherAttendanceMarkHistoryReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        ClassManagement objClassManagement = new ClassManagement();
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
                Session["Generator158"] = r.ToString();
                //bindData("", "", "", "");
            }
        }
        public void bindData(string DateFrom, string DateTo, string TeacherNo, string TeacherName)
        {
            Session["TeacherAttendanceMarkHistoryReport"] = null;
            List<MISCompleteAttendanceRun> ObjList = (from item in DB.ISCompleteAttendanceRuns.Where(p => p.ISTeacher.SchoolID == Authentication.SchoolID).ToList()
                                                      select new MISCompleteAttendanceRun
                                                      {
                                                          ID = item.ID,
                                                          Date = item.Date,
                                                          A_Date = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : "",
                                                          A_Time = item.Date != null ? item.Date.Value.ToString("HH:mm tt") : "",
                                                          TeacherID = item.TeacherID,
                                                          TeacherName = item.ISTeacher.Name,
                                                          ClassID = item.ClassID,
                                                          ClassName = item.ClassID != null ? DB.ISClasses.SingleOrDefault(p => p.ID == item.ClassID).Name : "",
                                                          TeacherNo = item.ISTeacher.TeacherNo
                                                      }).ToList();
            if (DateFrom != "")
            {
                DateTime dt = Convert.ToDateTime(DateFrom);
                ObjList = ObjList.Where(p => p.Date.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                DateTime dt = Convert.ToDateTime(DateTo);
                ObjList = ObjList.Where(p => p.Date.Value.Date <= dt.Date).ToList();
            }
            if (TeacherNo != "")
            {
                ObjList = ObjList.Where(p => p.TeacherNo.ToLower() == TeacherNo.ToLower()).ToList();
            }
            if (TeacherName != "")
            {
                ObjList = ObjList.Where(p => p.TeacherName.ToLower().Contains(TeacherName.ToLower())).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList.OrderByDescending(p => p.Date).ToList();
            lstTable.DataBind();
            Session["TeacherAttendanceMarkHistoryReport"] = ObjList.OrderByDescending(p => p.Date).ToList();
        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text, txtTeacherNo.Text, txtTeacherName.Text);
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text, txtTeacherNo.Text, txtTeacherName.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtTeacherName.Text = "";
            txtTeacherNo.Text = "";
            bindData("", "", "", "");
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetTeachersFunc(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<ISTeacher> ObjList = DB.ISTeachers.Where(p => p.Name.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();

            List<string> StNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                StNames.Add(ObjList[i].Name);
            }
            return StNames;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherAttendanceMarkHistoryReport" + Session["Generator158"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath,bool isFileDownloadRequired = true)
        {
            string retunrFilePath = "";
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                ///Adding Sheet here
                Row Hederrow;
                var lists = (List<MISCompleteAttendanceRun>)Session["TeacherAttendanceMarkHistoryReport"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherAttendanceMarkHistoryReport" + Session["Generator158"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Class Name");
                CreateCell(Hederrow, "Complete Attendance Time");
                CreateCell(Hederrow, "Activated By");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.A_Date);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.A_Time);
                    CreateCell(row, item.TeacherName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                retunrFilePath = filepath;

                if (isFileDownloadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TeacherAttendanceMarkHistoryReport" + Session["Generator158"].ToString() + ".xlsx");
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

            return retunrFilePath;
        }

        protected void linkSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherAttendanceMarkHistoryReport" + Session["Generator158"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}