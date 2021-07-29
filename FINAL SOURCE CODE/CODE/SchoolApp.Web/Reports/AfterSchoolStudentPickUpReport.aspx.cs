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
    public partial class AfterSchoolStudentPickUpReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
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
                Session["Generator153"] = r.ToString();
                //bindData("", "");
            }
        }
        public void bindData(string Date, string StudentName)
        {
            Session["AfterSchoolStudentPickUpReport"] = null;
            List<MISPickup> ObjList = (from item in DB.ISPickups.Where(p => p.ISStudent.SchoolID == Authentication.SchoolID && p.PickStatus.Contains("After-School")).ToList()
                                       select new MISPickup
                                       {
                                           ID = item.ID,
                                           StudentID = item.StudentID,
                                           StudentName = item.ISStudent.StudentName,
                                           StNo = item.ISStudent.StudentNo,
                                           TeacherName = item.ISTeacher.Name,
                                           PickStatus = item.PickStatus,
                                           Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                           Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                           PickerID = item.PickerID == null ? 0 : item.PickerID,
                                           PickDate = item.PickDate == null ? null : item.PickDate,
                                       }).ToList();
            if (Date != "")
            {
                string dates = Date;
                string Format = "";
                if (dates.Contains("/"))
                {
                    string[] arrDate = dates.Split('/');
                    Format = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format = dates;
                }
                DateTime dt2 = Convert.ToDateTime(Format);
                ObjList = ObjList.Where(p => p.Pick_Date == dt2.ToString("dd/MM/yyyy")).ToList();
            }
            if (StudentName != "")
            {
                ObjList = ObjList.Where(p => p.StudentName.ToLower().Contains(StudentName.ToLower())).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList.OrderByDescending(p => p.PickDate).ToList();
            lstTable.DataBind();
            Session["AfterSchoolStudentPickUpReport"] = ObjList.OrderByDescending(p => p.PickDate).ToList();
        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtDate.Text, txtStName.Text);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtDate.Text, txtStName.Text);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDate.Text = "";
            txtStName.Text = "";
            bindData("", "");
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
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/AfterSchoolStudentPickUpReportlist" + Session["Generator153"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isDownLoadFileRequired =true)
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
                var lists = (List<MISPickup>)Session["AfterSchoolStudentPickUpReport"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("AfterSchoolStudentPickUpReportlist" + Session["Generator153"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Pickup Status");
                CreateCell(Hederrow, "Pickup Time");                
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Pick_Date);
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.PickStatus);
                    CreateCell(row, item.Pick_Time);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isDownLoadFileRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=AfterSchoolStudentPickUpReportlist" + Session["Generator153"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/AfterSchoolStudentPickUpReportlist" + Session["Generator153"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}