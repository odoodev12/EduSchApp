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
    public partial class AbsentStudentReport : System.Web.UI.Page
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
                Session["Generator155"] = r.ToString();
                //bindData("", "","","");
            }
        }
        public void bindData(string Date, string StudentName, string ClassName , string StudentNo)
        {
            Session["AbsentStudentReportlist"] = null;
            List<MISStudent> ObjList = (from item in DB.ISAttendances.Where(p => p.ISStudent.SchoolID == Authentication.SchoolID && p.Status == "Absent").ToList()
                                        join item2 in DB.ISStudents.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true).ToList() on item.StudentID equals item2.ID
                                        select new MISStudent
                                        {
                                            ID = item2.ID,
                                            AttStatus = item.Status,
                                            AttDate = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : "",
                                            AttDates = item.Date,
                                            StudentName = item2.StudentName,
                                            StudentNo = item2.StudentNo,
                                            ParantEmail1 = item2.ParantEmail1,
                                            ParantEmail2 = item2.ParantEmail2,
                                            ParantPhone1 = item2.ParantPhone1,
                                            ParantPhone2 = item2.ParantPhone2,
                                            ClassID = item2.ClassID,
                                            ClassName = item2.ClassID != null ? item2.ISClass.Name : "",
                                            CreatedDateTime = item2.CreatedDateTime,
                                            CreateDate = item2.CreatedDateTime != null ? item2.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "",
                                        }).ToList();
            if(Date != "")
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
                ObjList = ObjList.Where(p => p.AttDates.Value.ToString("dd/MM/yyyy") == dt2.ToString("dd/MM/yyyy")).ToList();
            }
            if (StudentName != "")
            {
                ObjList = ObjList.Where(p => p.StudentName.ToLower().ToString() == StudentName.ToLower().ToString()).ToList();
            }
            if (ClassName != "")
            {
                ObjList = ObjList.Where(p => p.ClassName.ToLower().ToString() == ClassName.ToLower().ToString()).ToList();
            }
            if (StudentNo != "")
            {
                ObjList = ObjList.Where(p => p.StudentNo.ToLower().ToString() == StudentNo.ToLower().ToString()).ToList();
            }
            linkSendEmail.Visible = ObjList.Count > 0;
            lstTable.DataSource = ObjList.OrderByDescending(p => p.CreatedDateTime).ToList();
            lstTable.DataBind();
            Session["AbsentStudentReportlist"] = ObjList.OrderByDescending(p => p.CreatedDateTime).ToList();
        }


        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager2");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData(txtDate.Text, txtStName.Text, txtClassname.Text, txtStNo.Text);
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            bindData(txtDate.Text, txtStName.Text, txtClassname.Text, txtStNo.Text);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtStName.Text = "";
            txtClassname.Text = "";
            txtStNo.Text = "";
            txtDate.Text = "";
            bindData("", "","","");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetsStudentss(string prefixText)
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
        public static List<string> GetsClassess(string prefixText)
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
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/AbsentStudentReport" + Session["Generator155"].ToString() + ".xlsx");
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
        private string CreateSpreadsheetWorkbook(string filepath, bool isFileDownload = true)
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
                var lists = (List<MISStudent>)Session["AbsentStudentReportlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("AbsentStudentReport" + Session["Generator155"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Number");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Attendance Status");
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
                    CreateCell(row, item.StudentNo);
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.AttStatus);
                    CreateCell(row, item.ParantEmail1);
                    CreateCell(row, item.ParantPhone1);
                    CreateCell(row, item.ParantEmail2);
                    CreateCell(row, item.ParantPhone2);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                if (isFileDownload)
                {
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition", "attachment; filename=AbsentStudentReport" + Session["Generator155"].ToString() + ".xlsx");
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
                string filePath = CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/AbsentStudentReport" + Session["Generator155"].ToString() + ".xlsx", false);
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/School/NewMessage.aspx");
            }
            catch (Exception ex)
            {

            }
        }
    }
}