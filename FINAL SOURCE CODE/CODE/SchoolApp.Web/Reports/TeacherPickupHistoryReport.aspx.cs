using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace SchoolApp.Web.Reports
{
    public partial class TeacherPickupHistoryReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        TeacherManagement ObjTeacherManagement = new TeacherManagement();
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
                Session["Generator143"] = r.ToString();
                Binddropdown();
                //bindData("", "", "", 0);
            }
        }
        public void Binddropdown()
        {
            drpClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            drpClass.DataValueField = "ID";
            drpClass.DataTextField = "Name";
            drpClass.DataBind();
            drpClass.Items.Insert(0, new ListItem { Text = "Select Class Name", Value = "0" });
        }
        public void bindData(string DateFrom, string DateTo, string TeacherName, int ClassID)
        {
            Session["TeacherPickUpReportlist"] = null;
            List<MISTeacherPickHistory> ObjList = (from item in DB.ISTeachers.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList()
                                                   join item2 in DB.ISPickups.Where(p => p.ISStudent.SchoolID == Authentication.SchoolID).ToList() on item.ID equals item2.TeacherID
                                                   group item2 by new
                                                   {
                                                       item2.TeacherID,
                                                       item2.ClassID,
                                                       PickDates = item2.PickDate.Value.Date,
                                                   } into gcs
                                                   select new MISTeacherPickHistory
                                                   {
                                                       TeacherID = gcs.Key.TeacherID,
                                                       TeacherName = gcs.Key.TeacherID != null ? DB.ISTeachers.SingleOrDefault(p => p.ID == gcs.Key.TeacherID).Name : "",
                                                       ClassID = gcs.Key.ClassID,
                                                       ClassName = gcs.Key.ClassID != null ? DB.ISClasses.SingleOrDefault(p => p.ID == gcs.Key.ClassID).Name : "",
                                                       PickDate = gcs.Key.PickDates,
                                                       PickDateStr = gcs.Key.PickDates != null ? gcs.Key.PickDates.ToString("dd/MM/yyyy"): "",
                                                       NoOfClassStudent = GetStudentByClass(gcs.Key.ClassID),
                                                       NoOfProcessedStudent = GetProcessedStudents(gcs.Key.ClassID, gcs.Key.PickDates),
                                                   }).OrderByDescending(p => p.PickDate).ToList();
            if (DateFrom != "")
            {
                DateTime dt = Convert.ToDateTime(DateFrom);
                ObjList = ObjList.Where(p => p.PickDate.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                DateTime dt = Convert.ToDateTime(DateTo);
                ObjList = ObjList.Where(p => p.PickDate.Value.Date <= dt.Date).ToList();
            }
            if(TeacherName != "")
            {
                ObjList = ObjList.Where(p => p.TeacherName.ToLower().Contains(TeacherName.ToLower())).ToList();
            }
            if (ClassID != 0)
            {
                ObjList = ObjList.Where(p => p.ClassID == ClassID).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList;
            lstTable.DataBind();
            Session["TeacherPickUpReportlist"] = ObjList;
        }

        private int GetProcessedStudents(int? classID, DateTime? PickDate)
        {
            if (classID != null || classID != 0)
            {
                int Students = DB.ISPickups.Where(p => p.ClassID == classID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(PickDate)).Count();
                return Students;
            }
            return 0;
        }

        private int GetStudentByClass(int? classID)
        {
            if (classID != null || classID != 0)
            {
                int Students = DB.ISStudents.Where(p => p.ClassID == classID).Count();
                return Students;
            }
            return 0;
        }

        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager3");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text, txtTeacherName.Text, Convert.ToInt32(drpClass.SelectedValue));
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text, txtTeacherName.Text, Convert.ToInt32(drpClass.SelectedValue));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtTeacherName.Text = "";
            drpClass.SelectedValue = "0";
            bindData("", "", "", 0);
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetTeacher(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            TeacherManagement objTeacherManagement = new TeacherManagement();
            List<ISTeacher> ObjList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Name.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();

            List<string> TeacherNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                TeacherNames.Add(ObjList[i].Name);
            }
            return TeacherNames;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherPickUpHistoryReportList" + Session["Generator143"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isFileDownloadRequired = true)
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
                var lists = (List<MISTeacherPickHistory>)Session["TeacherPickUpReportlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherPickUpHistoryReportList" + Session["Generator143"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "TeacherName");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "ClassName");
                CreateCell(Hederrow, "No Of Student in Class");
                CreateCell(Hederrow, "No Of Student Processed");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.TeacherName);
                    CreateCell(row, item.PickDateStr);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.NoOfClassStudent.ToString());
                    CreateCell(row, item.NoOfProcessedStudent.ToString());
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownloadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TeacherPickUpHistoryReportList" + Session["Generator143"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherPickUpHistoryReportList" + Session["Generator143"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}