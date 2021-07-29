using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Row = DocumentFormat.OpenXml.Spreadsheet.Row;

namespace SchoolApp.Web.Reports
{
    public partial class StudentArchiveReport : System.Web.UI.Page
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
                Session["GeneratorStArchive"] = r.ToString();
                //bindData("", "");
            }
        }
        public void bindData(string StudentName, string ClassName)
        {
            Session["StArchiveReportList"] = null;
            List<MISStudent> ObjList = (from item in DB.ISStudents.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == false && p.Active == false).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            ParantEmail1 = item.ParantEmail1,
                                            ParantEmail2 = item.ParantEmail2,
                                            ParantPhone1 = item.ParantPhone1,
                                            ParantPhone2 = item.ParantPhone2,
                                            ClassID = item.ClassID,
                                            ClassName = item.ClassID != null ? item.ISClass.Name : "",
                                            CreatedDateTime = item.CreatedDateTime,
                                            CreateDate = item.CreatedDateTime != null ? item.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "",
                                        }).ToList();
            if (StudentName != "")
            {
                ObjList = ObjList.Where(p => p.StudentName.ToLower().Contains(StudentName.ToLower())).ToList();
            }
            if (ClassName != "")
            {
                ObjList = ObjList.Where(p => p.ClassName.ToLower().Contains(ClassName.ToLower())).ToList();
            }

            linkSendEmail.Visible = ObjList.Count > 0;

            lstTable.DataSource = ObjList.OrderByDescending(p => p.CreatedDateTime).ToList();
            lstTable.DataBind();
            Session["StArchiveReportList"] = ObjList.OrderByDescending(p => p.CreatedDateTime).ToList();
        }


        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtStName.Text, txtClassname.Text);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtStName.Text, txtClassname.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtStName.Text = "";
            txtClassname.Text = "";
            bindData("", "");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetsStudents(string prefixText)
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetsClasses(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ClassManagement objClassManagements = new ClassManagement();
            List<ISClass> ObjList = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && !p.Name.Contains("After School Ex") && p.Name.ToLower().Contains(prefixText.ToLower()) && p.Active == true && p.Deleted == true).ToList();

            List<string> StNames = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                StNames.Add(ObjList[i].Name);
            }
            return StNames;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string newfileNamePath = "";

            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/StudentWithNoImageReportlist" + Session["GeneratorStArchive"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isdownloadRequired = true)
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
                var lists = (List<MISStudent>)Session["StArchiveReportList"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("StudentWithNoImageReportlist" + Session["GeneratorStArchive"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();                
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Class");
                CreateCell(Hederrow, "Primary Parent Email");
                CreateCell(Hederrow, "Primary Parent Number");
                CreateCell(Hederrow, "Secondary Parent Email");
                CreateCell(Hederrow, "Secondary Parent Number");
                CreateCell(Hederrow, "Created Date");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.ClassName);
                    CreateCell(row, item.ParantEmail1);
                    CreateCell(row, item.ParantPhone1);
                    CreateCell(row, item.ParantEmail2);
                    CreateCell(row, item.ParantPhone2);
                    CreateCell(row, item.CreateDate);
                }

                spreadsheetDocument.Close();

                //if (filepath.Length > 0)
                //{   
                //    Session["NewFilePathUploaded"] = filepath;
                //    Response.Redirect("~/School/NewMessage.aspx");
                //}
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isdownloadRequired)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=StudentArchiveReport" + Session["GeneratorStArchive"].ToString() + ".xlsx");
                    Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.TransmitFile(fileinfo.FullName);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Flush();
                    Response.End();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Response.Flush();
                ErrorLogManagement.AddLog(ex);
            }

            return returnFilePath;
        }

        protected void linkSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/StudentWithNoImageReportlist" + Session["GeneratorStArchive"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch(Exception ex)
            {

            }
        }
    }
}