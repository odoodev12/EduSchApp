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
    public partial class LateStudentReport : System.Web.UI.Page
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
                Session["Generator148"] = r.ToString();
                Binddropdown();
                //bindData("", "", "", "", "0");
            }
        }
        public void Binddropdown()
        {
            drpClassName.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            drpClassName.DataValueField = "ID";
            drpClassName.DataTextField = "Name";
            drpClassName.DataBind();
            drpClassName.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }
        public void bindData(string DateFrom, string DateTo, string StudentName, string StudentNo, string ClassName)
        {
            Session["LateReportlist"] = null;
            List<MISPickup> ObjList = (from item in DB.ISPickups.Where(p => p.ISStudent.SchoolID == Authentication.SchoolID && p.PickStatus == "Picked(Late)").ToList()
                                       select new MISPickup
                                       {
                                           ID = item.ID,
                                           StudentID = item.StudentID,
                                           StudentName = item.ISStudent.StudentName,
                                           StNo = item.ISStudent.StudentNo,
                                           TeacherID = item.TeacherID,
                                           TeacherName = item.ISTeacher.Name,
                                           PickStatus = item.PickStatus,
                                           Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                           Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                           PickerID = item.PickerID == null ? 0 : item.PickerID,
                                           PickDate = item.PickDate == null ? null : item.PickDate,
                                           PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                           ClassID = item.ClassID,
                                           ClassName = item.ClassID != null ? DB.ISClasses.SingleOrDefault(p => p.ID == item.ClassID).Name : "",
                                       }).ToList();
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
            if (StudentName != "")
            {
                ObjList = ObjList.Where(p => p.StudentName.ToLower().Contains(StudentName.ToLower())).ToList();
            }
            if (StudentNo != "")
            {
                ObjList = ObjList.Where(p => p.StNo.ToLower() == StudentNo.ToLower()).ToList();
            }
            if (ClassName != "0")
            {
                int ID = Convert.ToInt32(ClassName);
                ObjList = ObjList.Where(p => p.ClassID == ID).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList.OrderByDescending(p => p.PickDate).ToList();
            lstTable.DataBind();
            Session["LateReportlist"] = ObjList;
        }


        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text, txtStName.Text, txtStNo.Text, drpClassName.SelectedValue);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text, txtStName.Text, txtStNo.Text, drpClassName.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtStName.Text = "";
            txtStNo.Text = "";
            drpClassName.SelectedValue = "0";
            bindData("", "", "", "", "0");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetStudentss(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<ISStudent> ObjList = DB.ISStudents.Where(p => p.StudentName.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();

            List<string> StNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                StNames.Add(ObjList[i].StudentName);
            }
            return StNames;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/LateStudentReportlist" + Session["Generator148"].ToString() + ".xlsx");
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
                var lists = (List<MISPickup>)Session["LateReportlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("LateStudentReportlist" + Session["Generator148"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Student Number");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Class Name");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickup Status");
                CreateCell(Hederrow, "Teacher Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Pick_Date);
                    CreateCell(row, item.StNo);
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickStatus);
                    CreateCell(row, item.TeacherName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;
                if (isFileDownLoadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=LateStudentReportlist" + Session["Generator148"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/LateStudentReportlist" + Session["Generator148"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}