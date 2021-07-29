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
    public partial class CreatedClassReport : System.Web.UI.Page
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
                Session["Generator144"] = r.ToString();
                Binddropdown();
                //bindData("", "", "","");
            }
        }
        public void Binddropdown()
        {
            drpClassYear.DataSource = CommonOperation.GetYear();
            drpClassYear.DataValueField = "ID";
            drpClassYear.DataTextField = "Year";
            drpClassYear.DataBind();
            drpClassYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });
        }
        public void bindData(string DateFrom, string DateTo, string ClassName, string ClassYear)
        {
            Session["CreatedClassReportlist"] = null;
            List<MISClass> ObjList = objClassManagement.ClassListByFilter(Authentication.SchoolID, ClassYear, 0, "0");
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

            if (ClassName != "")
            {
                ObjList = ObjList.Where(p => p.Name.ToLower().Contains(ClassName.ToLower())).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList.OrderByDescending(p => p.CreatedDateTime).ToList();
            lstTable.DataBind();
            Session["CreatedClassReportlist"] = ObjList.OrderByDescending(p => p.CreatedDateTime).ToList();
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
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text, txtClassName.Text, drpClassYear.SelectedValue);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text, txtClassName.Text, drpClassYear.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtClassName.Text = "";
            drpClassYear.SelectedValue = "";
            bindData("", "", "", "");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetClasses(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<ISClass> ObjList = DB.ISClasses.Where(p => p.Name.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();

            List<string> ClassNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                ClassNames.Add(ObjList[i].Name);
            }
            return ClassNames;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/CreatedClassReportlist" + Session["Generator144"].ToString() + ".xlsx");
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
                var lists = (List<MISClass>)Session["CreatedClassReportlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("CreatedClassReportlist" + Session["Generator144"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Created Date");
                CreateCell(Hederrow, "Class Name");
                CreateCell(Hederrow, "Class Year");
                CreateCell(Hederrow, "Created By");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    CreateCell(row, item.Name);
                    CreateCell(row, item.YearName);
                    CreateCell(row, item.CreateBy);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownLoadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CreatedClassReportlist" + Session["Generator144"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/CreatedClassReportlist" + Session["Generator144"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}