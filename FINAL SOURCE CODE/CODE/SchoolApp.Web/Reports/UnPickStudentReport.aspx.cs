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
    public partial class UnPickStudentReport : System.Web.UI.Page
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
                Session["Generator152"] = r.ToString();
                //bindData();
            }
        }
        public void bindData()
        {
            Session["UnPickStudentReport"] = null;
            DateTime dt = DateTime.Now;
            List<MViewStudentPickUp> ObjList = (from item in DB.getPickUpData(dt).Where(p => p.SchoolID == Authentication.SchoolID && p.PickStatus == null || p.PickStatus == "Not Marked")
                                        select new MViewStudentPickUp
                                        {
                                            StudentName = item.StudentName,
                                            PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                            ParantEmail1 = item.ParantEmail1,
                                            ParantEmail2 = item.ParantEmail2,
                                            ParantPhone1 = item.ParantPhone1,
                                            ParantPhone2 = item.ParantPhone2,
                                        }).ToList();
            lstTable.DataSource = ObjList;
            lstTable.DataBind();
            Session["UnPickStudentReport"] = ObjList;
        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager3");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/UnPickStudentReportList" + Session["Generator152"].ToString() + ".xlsx");
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

                ///Adding Sheet here
                Row Hederrow;
                var lists = (List<MViewStudentPickUp>)Session["UnPickStudentReport"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("UnPickStudentReportList" + Session["Generator152"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "PickStatus");
                CreateCell(Hederrow, "Primary Parent Email");
                CreateCell(Hederrow, "Primary Parent Number");
                CreateCell(Hederrow, "Secondary Parent Email");
                CreateCell(Hederrow, "Secondary Parent Number");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.PickStatus);
                    CreateCell(row, item.ParantEmail1);
                    CreateCell(row, item.ParantPhone1);
                    CreateCell(row, item.ParantEmail2);
                    CreateCell(row, item.ParantPhone2);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=UnPickStudentReportList" + Session["Generator152"].ToString() + ".xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
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