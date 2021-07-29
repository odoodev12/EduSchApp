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
    public partial class ReAssignedTeacherReport : System.Web.UI.Page
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
                Session["Generator162"] = r.ToString();
                //bindData("", "", "", "", "");
            }
        }
        public void bindData(string DateFrom, string DateTo, string TeacherName, string ClassFrom, string ClassTo)
        {
            try
            {
                Session["TeachReassignedReportlist"] = null;
                List<MISTeacherReassignHistory> ObjList = (from item in DB.ISTeacherReassignHistories.Where(p => p.SchoolID == Authentication.SchoolID).ToList()
                                                           join item2 in DB.ISTeachers.Where(p => p.SchoolID == Authentication.SchoolID).ToList() on item.TeacherID equals item2.ID
                                                           select new MISTeacherReassignHistory
                                                           {
                                                               ID = item.ID,
                                                               TeacherID = item.TeacherID,
                                                               TeacherName = item2.Name,
                                                               FromClass = item.FromClass,
                                                               ToClass = item.ToClass,
                                                               ClassFrom = item.FromClass != null ? DB.ISClasses.SingleOrDefault(p => p.ID == item.FromClass).Name : "",
                                                               ClassTo = item.ToClass != null ? DB.ISClasses.SingleOrDefault(p => p.ID == item.ToClass).Name : "",
                                                               Date = item.Date,
                                                               DateStr = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : "",
                                                               ReAssignedBy = (item.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.School) ? DB.ISSchools.SingleOrDefault(p => p.ID == item.CreatedBy).Name : (item.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.Teacher) ? DB.ISTeachers.SingleOrDefault(p => p.ID == item.CreatedBy).Name : "",
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
                if (TeacherName != "")
                {
                    ObjList = ObjList.Where(p => p.TeacherName.ToLower().Contains(TeacherName.ToLower())).ToList();
                }
                if (ClassFrom != "")
                {
                    ObjList = ObjList.Where(p => p.ClassFrom.ToLower().Contains(ClassFrom.ToLower())).ToList();
                }
                if (ClassTo != "")
                {
                    ObjList = ObjList.Where(p => p.ClassTo.ToLower().Contains(ClassTo.ToLower())).ToList();
                }
                linkSendEmail.Visible = ObjList.Count > 0;
                lstTable.DataSource = ObjList.OrderByDescending(p => p.Date).ToList();
                lstTable.DataBind();
                Session["TeachReassignedReportlist"] = ObjList.OrderByDescending(p => p.Date).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }


        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text, txtStName.Text, txtClassFrom.Text, txtClassTo.Text);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text, txtStName.Text, txtClassFrom.Text, txtClassTo.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtStName.Text = "";
            txtClassFrom.Text = "";
            txtClassTo.Text = "";
            bindData("", "", "", "", "");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetReTeachers(string prefixText)
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetClassFroms(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ClassManagement objClassManagements = new ClassManagement();
            List<ISClass> ObjList = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && !p.Name.Contains("After School Ex") && p.Name.ToLower().Contains(prefixText.ToLower()) && p.Active == true && p.Deleted == true).ToList();

            List<string> ClassNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                ClassNames.Add(ObjList[i].Name);
            }
            return ClassNames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetClassTos(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ClassManagement objClassManagements = new ClassManagement();
            List<ISClass> ObjList = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && !p.Name.Contains("After School Ex") && p.Name.ToLower().Contains(prefixText.ToLower()) && p.Active == true && p.Deleted == true).ToList();

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
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherReAssignReportlist" + Session["Generator162"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isFileDownlaodRequired = true)
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
                var lists = (List<MISTeacherReassignHistory>)Session["TeachReassignedReportlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherReAssignReportlist" + Session["Generator162"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Teacher Name");
                CreateCell(Hederrow, "From Class");
                CreateCell(Hederrow, "To Class");
                CreateCell(Hederrow, "Updated By");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.DateStr);
                    CreateCell(row, item.TeacherName);
                    CreateCell(row, item.ClassFrom);
                    CreateCell(row, item.ClassTo);
                    CreateCell(row, item.ReAssignedBy);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownlaodRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TeacherReAssignReportlist" + Session["Generator162"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherReAssignReportlist" + Session["Generator162"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}