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
    public partial class EndDatedTeacherReport : System.Web.UI.Page
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
                Session["Generator141"] = r.ToString();
                Binddropdown();
                //bindData("", "", "", 0, "");
            }
        }
        public void Binddropdown()
        {
            drpClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0).Where(p => !p.Name.Contains("After School Ex") && p.Active == true).ToList();
            drpClass.DataValueField = "ID";
            drpClass.DataTextField = "Name";
            drpClass.DataBind();
            drpClass.Items.Insert(0, new ListItem { Text = "Select Class Name", Value = "0" });

            drpClassYear.DataSource = CommonOperation.GetYear();
            drpClassYear.DataValueField = "ID";
            drpClassYear.DataTextField = "Year";
            drpClassYear.DataBind();
            drpClassYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });
        }
        public void bindData(string DateFrom, string DateTo, string TeacherName, int ClassID, string ClassYear)
        {
            Session["EndDateTeacherReportlist"] = null;
            List<MISTeacher> ObjList = ObjTeacherManagement.AllTeacherList(Authentication.SchoolID, ClassYear, ClassID, TeacherName, "", "", 0, "2").Where(p => p.Active == false).ToList();
            if (DateFrom != "")
            {
                DateTime dt = Convert.ToDateTime(DateFrom);
                ObjList = ObjList.Where(p => p.ModifyDateTime.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                DateTime dt = Convert.ToDateTime(DateTo);
                ObjList = ObjList.Where(p => p.ModifyDateTime.Value.Date <= dt.Date).ToList();
            }

            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList;
            lstTable.DataBind();
            Session["EndDateTeacherReportlist"] = ObjList;
        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager5");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtFromDate.Text, txtToDate.Text, txtTeacherNames.Text, Convert.ToInt32(drpClass.SelectedValue), drpClassYear.SelectedValue);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtFromDate.Text, txtToDate.Text, txtTeacherNames.Text, Convert.ToInt32(drpClass.SelectedValue), drpClassYear.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtTeacherNames.Text = "";
            drpClass.SelectedValue = "0";
            drpClassYear.SelectedValue = "";
            bindData("", "", "", 0, "");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetAllTeachers(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            TeacherManagement objTeacherManagement = new TeacherManagement();
            List<ISTeacher> ObjList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Active == false && p.Name.ToLower().Contains(prefixText.ToLower()) && p.SchoolID == Authentication.SchoolID).ToList();

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
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/EndDatedTeacherReportList" + Session["Generator141"].ToString() + ".xlsx");
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
                var lists = (List<MISTeacher>)Session["EndDateTeacherReportlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("EndDatedTeacherReportList" + Session["Generator141"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "EndDate");
                CreateCell(Hederrow, "TeacherName");
                CreateCell(Hederrow, "ClassName");
                CreateCell(Hederrow, "ClassYear");
                CreateCell(Hederrow, "Ended By");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.TeacherEndDate);
                    CreateCell(row, item.Name);
                    CreateCell(row, item.AssignedClass);
                    CreateCell(row, item.AssignedClassYear);
                    CreateCell(row, item.CreateBy);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownloadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=EndDatedTeacherReportList" + Session["Generator141"].ToString() + ".xlsx");
                    Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    ///GvUserList.HeaderStyle.Font.Bold = true;
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/EndDatedTeacherReportList" + Session["Generator141"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}