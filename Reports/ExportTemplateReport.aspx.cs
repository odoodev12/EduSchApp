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
    public partial class ExportTemplateReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        TeacherManagement _TeacherManagement = new TeacherManagement();
        ClassManagement _ClassManagement = new ClassManagement();
        StudentManagement _StudentManagement = new StudentManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!Page.IsPostBack)
            {
                
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            Session["GeneratorExport"] = r.ToString();
            try
            {
                CreateMasterWorkbook(Server.MapPath("~/Upload") + "/ExportTemplateReport" + r.ToString() + ".xlsx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
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
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void CreateMasterWorkbook(string filepath)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                if(drpExport.SelectedValue == "1" || drpExport.SelectedValue == "5")
                {
                    // Class Names
                    List<MISClass> _ClassList = _ClassManagement.ClassListByFilter(Authentication.SchoolID, "", 0, "0");
                    WorksheetPart ClassworksheetParts = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData ClasssheetDatas = new SheetData();
                    ClassworksheetParts.Worksheet = new Worksheet(ClasssheetDatas);

                    Sheet Classsheetss = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(ClassworksheetParts), SheetId = 1, Name = ("Classes").ToString() };
                    sheets.Append(Classsheetss);

                    Hederrow = new Row();
                    ClasssheetDatas.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "ClassName");
                    CreateCell(Hederrow, "Year");
                    CreateCell(Hederrow, "ClassType");
                    CreateCell(Hederrow, "AfterSchoolType");
                    CreateCell(Hederrow, "ExternalOrganisation");
                    CreateCell(Hederrow, "Status");
                    CreateCell(Hederrow, "CreatedBy");
                    CreateCell(Hederrow, "CreatedDate");
                    int ClassTotal = 0;
                    foreach (var item in _ClassList)
                    {
                        Row row = new Row();
                        ClasssheetDatas.Append(row);
                        CreateCell(row, (ClassTotal += 1).ToString());
                        CreateCell(row, item.Name);
                        CreateCell(row, item.YearName);
                        CreateCell(row, item.ClassType);
                        CreateCell(row, item.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool ? item.AfterSchoolType : "");
                        CreateCell(row, item.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool ? item.ExternalOrganisation : "");
                        CreateCell(row, item.Status);
                        CreateCell(row, item.CreateBy);
                        CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    }
                    // Over Class Names
                }
                if (drpExport.SelectedValue == "2" || drpExport.SelectedValue == "5")
                {
                    // Teacher
                    List<MISTeacher> list = _TeacherManagement.TeacherList(Authentication.SchoolID, "", 0, "", "ASC", "", 0, "0");
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("Teachers").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "TeacherNo");
                    CreateCell(Hederrow, "Title");
                    CreateCell(Hederrow, "Name");
                    CreateCell(Hederrow, "Role");
                    CreateCell(Hederrow, "Phone");
                    CreateCell(Hederrow, "Email");
                    CreateCell(Hederrow, "AssignedClasses");
                    CreateCell(Hederrow, "Status");
                    CreateCell(Hederrow, "CreatedBy");
                    CreateCell(Hederrow, "CreatedDate");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.TeacherNo);
                        CreateCell(row, item.Title);
                        CreateCell(row, item.Name);
                        CreateCell(row, item.RoleName);
                        CreateCell(row, item.PhoneNo);
                        CreateCell(row, item.Email);
                        CreateCell(row, item.AssignedClass);
                        CreateCell(row, item.Status);
                        CreateCell(row, item.CreateBy);
                        CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    }
                    // Over Teacher
                }
                if (drpExport.SelectedValue == "3" || drpExport.SelectedValue == "5")
                {
                    // NonTeacher
                    List<MISTeacher> list = _TeacherManagement.NonTeacherList(Authentication.SchoolID, "", "ASC", "");
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("NonTeachers").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "TeacherNo");
                    CreateCell(Hederrow, "Title");
                    CreateCell(Hederrow, "Name");
                    CreateCell(Hederrow, "Role");
                    CreateCell(Hederrow, "Phone");
                    CreateCell(Hederrow, "Email");
                    CreateCell(Hederrow, "AssignedClasses");
                    CreateCell(Hederrow, "Status");
                    CreateCell(Hederrow, "CreatedBy");
                    CreateCell(Hederrow, "CreatedDate");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.TeacherNo);
                        CreateCell(row, item.Title);
                        CreateCell(row, item.Name);
                        CreateCell(row, item.RoleName);
                        CreateCell(row, item.PhoneNo);
                        CreateCell(row, item.Email);
                        CreateCell(row, item.ClassName1 + (!String.IsNullOrEmpty(item.ClassName2) ? ", " + item.ClassName2 : ""));
                        CreateCell(row, item.Status);
                        CreateCell(row, item.CreateBy);
                        CreateCell(row, item.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    }
                    // Over NonTeacher
                }
                if (drpExport.SelectedValue == "4" || drpExport.SelectedValue == "5")
                {
                    // Student
                    List<MISStudent> list = new List<MISStudent>();
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        list = _ClassManagement.StudentFullListByClass(Authentication.SchoolID, 0);
                    }
                    else
                    {
                        List<ISClass> _ClassList = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();
                        foreach(var items in _ClassList)
                        {
                            if (items.Name.Contains("Outside"))
                            {
                                list.AddRange(_ClassManagement.StudentFullListByExtClass(Authentication.SchoolID, items.ID));
                            }
                            else
                            {
                                list.AddRange(_ClassManagement.StudentFullListByClass(Authentication.SchoolID, items.ID));
                            }
                        }
                    }
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("Students").ToString() };
                    sheets.Append(sheet);

                    Hederrow = new Row();
                    sheetData.Append(Hederrow);
                    CreateCell(Hederrow, "Sr.No.");
                    CreateCell(Hederrow, "StudentNo");
                    CreateCell(Hederrow, "Name");
                    CreateCell(Hederrow, "ClassName");
                    CreateCell(Hederrow, "PrimaryParentName");
                    CreateCell(Hederrow, "PrimaryParentEmail");
                    CreateCell(Hederrow, "PrimaryParentPhone");
                    CreateCell(Hederrow, "PrimaryParentRelation");
                    CreateCell(Hederrow, "SecondaryParentName");
                    CreateCell(Hederrow, "SecondaryParentEmail");
                    CreateCell(Hederrow, "SecondaryParentPhone");
                    CreateCell(Hederrow, "SecondaryParentRelation");
                    CreateCell(Hederrow, "EmailAfterConfirmPickUp");
                    CreateCell(Hederrow, "EmailAfterConfirmAttendance");
                    int Total = 0;
                    foreach (var item in list)
                    {
                        Row row = new Row();
                        sheetData.Append(row);
                        CreateCell(row, (Total += 1).ToString());
                        CreateCell(row, item.StudentNo);
                        CreateCell(row, item.StudentName);
                        CreateCell(row, item.ClassName);
                        CreateCell(row, item.ParantName1);
                        CreateCell(row, item.ParantEmail1);
                        CreateCell(row, item.ParantPhone1);
                        CreateCell(row, item.ParantRelation1);
                        CreateCell(row, item.ParantName2);
                        CreateCell(row, item.ParantEmail2);
                        CreateCell(row, item.ParantPhone2);
                        CreateCell(row, item.ParantRelation2);
                        CreateCell(row, item.EmailAfterConfirmPickUp == true ? "Yes" : "No");
                        CreateCell(row, item.EmailAfterConfirmAttendence == true ? "Yes" : "No");
                    }
                    // Over Student
                }
                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=ExportTemplateReport" + Session["GeneratorExport"].ToString() + ".xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            drpExport.SelectedValue = "0";
            Response.Redirect("ExportTemplateReport.aspx", true);
        }
    }
}