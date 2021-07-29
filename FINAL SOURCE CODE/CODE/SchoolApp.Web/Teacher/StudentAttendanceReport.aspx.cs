using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace SchoolApp.Web.Teacher
{
    public partial class StudentAttendanceReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        PickupManagement ObjpickupManagement = new PickupManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID).isAttendanceModule == false)
            {
                Response.Redirect(Authentication.UnAuthorizePage());
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    Random generator = new Random();
                    int r = generator.Next(100000, 1000000);
                    Session["Generator4"] = r.ToString();
                    int StudentID = Convert.ToInt32(Request.QueryString["ID"]);
                    BindList(StudentID, "", "", "0", "", "Date");
                    BindDropdown(StudentID);
                }
            }
        }

        private void BindDropdown(int StudentID)
        {
            int ID = Authentication.SchoolID;
            ISStudent student = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID);
            if (student != null)
            {
                TeacherManagement obj = new TeacherManagement();
                drpTeacher.DataSource = obj.TeacherList(ID, "", student.ClassID.Value, "", "", "", 0, "1");//DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Deleted == true && p.Active == true && p.SchoolID == ID).ToList();
                drpTeacher.DataTextField = "Name";
                drpTeacher.DataValueField = "ID";
                drpTeacher.DataBind();
                drpTeacher.Items.Insert(0, new ListItem { Text = "Select Teacher", Value = "0" });
            }
        }

        private void BindList(int StudentID, string FromDate, string ToDate, string TeacherID, string Status, string SortBy)
        {
            Session["AttendenceObjectlist"] = null;
            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Active == true && p.Deleted == true);
            if (_Student != null)
            {
                List<MGetAttendenceData> ObjList = new List<MGetAttendenceData>();
                if (_Student.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    lblInstruction.Visible = true;
                    ObjList = ObjpickupManagement.StudentAttendenceReport(StudentID, FromDate, ToDate, TeacherID, Status, SortBy, rbAsc.Checked == true ? "ASC" : "DESC", StudentID);
                }
                else
                {
                    lblInstruction.Visible = false;
                    ObjList = ObjpickupManagement.StudentAttendenceReport1(StudentID, FromDate, ToDate, TeacherID, Status, SortBy, rbAsc.Checked == true ? "ASC" : "DESC", StudentID);
                }
                lstAttendance.DataSource = ObjList.ToList();
                lstAttendance.DataBind();
                Session["AttendenceObjectlist"] = ObjList.ToList();
                ListView1.DataSource = DB.ISStudents.Where(p => p.ID == StudentID && p.Deleted == true).ToList();
                ListView1.DataBind();
            }
        }
        protected void lstAttendance_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstAttendance.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            if (Request.QueryString["ID"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                BindList(ID, txtFromDate.Text, txtToDate.Text, drpTeacher.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                BindList(ID, txtFromDate.Text, txtToDate.Text, drpTeacher.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherStudentAttendenceReport" + Session["Generator4"].ToString() + ".xlsx");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
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
                //ErrorLogManagement.AddLog(ex);
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

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = (List<MGetAttendenceData>)Session["AttendenceObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherStudentAttendenceReport" + Session["Generator4"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Marked Time");
                CreateCell(Hederrow, "TeacherName");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Date.Value.ToString("dd/MM/yyyy"));
                    CreateCell(row, item.Status);
                    CreateCell(row, item.MarkedTime);
                    CreateCell(row, item.TeacherName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=TeacherStudentAttendenceReport" + Session["Generator4"].ToString() + ".xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                ///GvUserList.HeaderStyle.Font.Bold = true;
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            drpTeacher.SelectedValue = "0";
            drpStatus.SelectedValue = "";
            drpSort.SelectedValue = "Date";
            rbDesc.Checked = true;
            int StudentID = Convert.ToInt32(Request.QueryString["ID"]);
            BindList(StudentID, "", "", "0", "", "Date");
        }

        protected void email_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbooks(Server.MapPath("../Upload") + "/TeacherStudentAttendenceReport" + Session["Generator4"].ToString() + ".xlsx");
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
            }
        }
        private void CreateSpreadsheetWorkbooks(string filepath)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = (List<MGetAttendenceData>)Session["AttendenceObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherStudentAttendenceReport" + Session["Generator4"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Marked Time");
                CreateCell(Hederrow, "TeacherName");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Date.Value.ToString("dd/MM/yyyy"));
                    CreateCell(row, item.Status);
                    CreateCell(row, item.MarkedTime);
                    CreateCell(row, item.TeacherName);
                }

                spreadsheetDocument.Close();
                EmailManagement ObjEmailManagement = new EmailManagement();
                string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: StudentAttendenceReport<br><br>Description &nbsp;: Here we Send you a Mail for Student Attendence Report So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", Authentication.LogginTeacher.Name);
                string FileNames = Server.MapPath("../Upload/TeacherStudentAttendenceReport" + Session["Generator4"].ToString() + ".xlsx");
                ObjEmailManagement.SendEmails(Authentication.LogginTeacher.Email, "StudentAttendenceReport", Message, FileNames);
                AlertMessageManagement.ServerMessage(Page, "Mail Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                //FileInfo fileinfo = new FileInfo(filepath);
                //Response.Clear();
                //Response.Charset = "";
                //Response.AddHeader("Content-Disposition", "attachment; filename=TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx");
                //Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                //Response.ContentType = "application/octet-stream";
                /////GvUserList.HeaderStyle.Font.Bold = true;
                //Response.TransmitFile(fileinfo.FullName);
                ////Response.Flush();
                //System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }
}