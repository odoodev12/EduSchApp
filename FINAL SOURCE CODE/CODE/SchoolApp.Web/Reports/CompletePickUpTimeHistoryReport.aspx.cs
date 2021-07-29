﻿using DocumentFormat.OpenXml;
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
    public partial class CompletePickUpTimeHistoryReport : System.Web.UI.Page
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
                Session["Generator156"] = r.ToString();
                //bindData("", "", "");
            }
        }
        public void bindData(string Date, string ClassName, string ActivatedByName)
        {
            Session["CompletePickUpTimeHistoryReport"] = null;
            List<MISCompletePickupRun> ObjList = (from item in DB.ISCompletePickupRuns.Where(p => p.ISTeacher.SchoolID == Authentication.SchoolID).ToList()
                                                      select new MISCompletePickupRun
                                                      {
                                                          ID = item.ID,
                                                          Date = item.Date,
                                                          DateStr = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : "",
                                                          CompletePickTime = item.Date != null ? item.Date.Value.ToString("HH:mm tt") : "",
                                                          TeacherID = item.TeacherID,
                                                          TeacherName = item.ISTeacher.Name,
                                                          ClassID = item.ClassID,
                                                          ClassName = item.ISClass.Name,
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
                ObjList = ObjList.Where(p => p.DateStr == dt2.ToString("dd/MM/yyyy")).ToList();
            }
            if (ClassName != "")
            {
                ObjList = ObjList.Where(p => p.ClassName.ToLower().Contains(ClassName.ToLower())).ToList();
            }
            if (ActivatedByName != "")
            {
                ObjList = ObjList.Where(p => p.TeacherName.ToLower().Contains(ActivatedByName.ToLower())).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;

            lstTable.DataSource = ObjList.OrderByDescending(p => p.Date).ToList();
            lstTable.DataBind();
            Session["CompletePickUpTimeHistoryReport"] = ObjList.OrderByDescending(p => p.Date).ToList();
        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtDate.Text, txtClassName.Text, txtTeacherName.Text);
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtDate.Text, txtClassName.Text, txtTeacherName.Text);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDate.Text = "";
            txtTeacherName.Text = "";
            txtClassName.Text = "";
            bindData("", "", "");
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetTeachersFuncs(string prefixText)
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
        public static List<string> GetClassFuncs(string prefixText)
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
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/CompletePickUpTimeHistoryReport" + Session["Generator156"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isFileDownLoad = true)
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
                var lists = (List<MISCompletePickupRun>)Session["CompletePickUpTimeHistoryReport"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("CompletePickUpTimeHistoryReport" + Session["Generator156"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "DateStr");
                CreateCell(Hederrow, "Class Name");
                CreateCell(Hederrow, "Complete PickUp Time");
                CreateCell(Hederrow, "Activated By");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.DateStr);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.CompletePickTime);
                    CreateCell(row, item.TeacherName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownLoad)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CompletePickUpTimeHistoryReport" + Session["Generator156"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/CompletePickUpTimeHistoryReport" + Session["Generator156"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}