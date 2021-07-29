using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
namespace SchoolApp.Web.Teacher
{
    public partial class StudentPickupReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        PickerManagement objPickerManagement = new PickerManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    Random generator = new Random();
                    int r = generator.Next(100000, 1000000);
                    Session["Generator5"] = r.ToString();
                    int StudentID = Convert.ToInt32(Request.QueryString["ID"]);
                    if (Session["FROMDATE"] != null)
                    {
                        DateTime myDt = Convert.ToDateTime(Session["FROMDATE"]);
                        txtFromDate.Text = myDt.ToString("yyyy-MM-dd");
                    }
                    if (Session["TODATE"] != null)
                    {
                        DateTime myDt = Convert.ToDateTime(Session["TODATE"]);
                        txtToDate.Text = myDt.ToString("yyyy-MM-dd");
                    }
                    BindList(StudentID, txtFromDate.Text, txtToDate.Text, "", "", "Date");
                    BindDropdown();
                }
            }
        }
        private void BindDropdown()
        {
            int StudentID = Convert.ToInt32(Request.QueryString["ID"]);
            PickerManagement objPickerManagement = new PickerManagement();
            List<MISPickerAssignment> objPickerList = new List<MISPickerAssignment>();
            List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(StudentID);
            List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(StudentID);
            foreach (MISPickerAssignment items in objList2)
            {
                if (objList.Where(p => p.ID == items.ID).Count() <= 0)
                {
                    objList.Add(items);
                    objPickerList.Add(items);
                }
            }
            var list = objList.Select(p => new { PickerName = p.PickerName }).Distinct().ToList();
            drpPicker.DataSource = list.OrderBy(r => r.PickerName);//objPickerManagement.AllPickerByStudent(StudentID);
            drpPicker.DataTextField = "PickerName";
            drpPicker.DataValueField = "PickerName";
            drpPicker.DataBind();
            drpPicker.Items.Insert(0, new ListItem { Text = "Select Picker", Value = "" });

            drpStatus.DataSource = DB.ISPickUpStatus.Where(p => p.Active == true).OrderBy(r => r.Status).ToList();
            drpStatus.DataTextField = "Status";
            drpStatus.DataValueField = "Status";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "" });
        }
        private void BindList(int StudentID, string FromDate, string ToDate, string PickerID, string Status, string SortBy)
        {
            Session["PickUpObjectlist"] = null;
            lblInstruction.Visible = (FromDate == "" || ToDate == "");
            ClassManagement classManagement = new ClassManagement();
            PickupManagement objPickupManagement = new PickupManagement();
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            if (FromDate != "")
            {
                Session["FROMDATE"] = FromDate;
            }
            if (ToDate != "")
            {
                Session["TODATE"] = ToDate;
            }
            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Active == true && p.Deleted == true);
            if (_Student != null)
            {
                List<MGetAttendenceData> ObjList = new List<MGetAttendenceData>();
                if (_Student.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    //lblInstruction.Visible = true;
                    objList = objPickupManagement.StudentPickUpReports(StudentID, FromDate, ToDate, PickerID, Status, SortBy, rbAsc.Checked == true ? "ASC" : "DESC", StudentID);
                }
                else
                {
                    //lblInstruction.Visible = false;
                    objList = objPickupManagement.StudentPickUpReportsAfterSchoolOnly(StudentID, FromDate, ToDate, PickerID, Status, SortBy, rbAsc.Checked == true ? "ASC" : "DESC", StudentID);
                    //objList = objPickupManagement.StudentPickUpReportsAfterSchool(StudentID, FromDate, ToDate, PickerID, Status, SortBy, rbAsc.Checked == true ? "ASC" : "DESC", StudentID);
                }
            }


            foreach (var item in objList)
            {

                if (OperationManagement.DefaultPickupStatuslist.Contains(item.PickStatus))
                {
                    item.Pick_Time = "";
                    item.PickerName = "";
                }
                else if (string.Compare(item.PickStatus, "No School", true) == 0)
                    item.PickStatus = "Weekend (School Closed)";

                if (item.PickDate != null)
                {

                    if (item.PickDate.Value.DayOfWeek == DayOfWeek.Saturday || item.PickDate.Value.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (item.PickStatus == "Not Marked")
                        {
                            item.PickStatus = "Weekend (School Closed)";
                            item.Pick_Time = "";
                            item.PickerName = "";
                        }
                    }
                }
            }
            lstPickup.DataSource = objList.ToList();
            //classManagement.GetAverage(null, StudentID);
            lstPickup.DataBind();

            Session["PickUpObjectlist"] = objList.ToList();

            ISStudent iSStudent = DB.ISStudents.FirstOrDefault(p => p.ID == StudentID && p.Deleted == true);
            List<MISStudent> mISStudent = new List<MISStudent>();
            mISStudent.Add(new MISStudent());
            mISStudent[0].StudentName = iSStudent.StudentName;
            mISStudent[0].Photo = iSStudent.Photo;
            mISStudent[0].ID = iSStudent.ID;
            mISStudent[0].StudentPickUpAverage = classManagement.GetAverage(null, StudentID);

            ListView1.DataSource = mISStudent;
            ListView1.DataBind();
        }
        protected void lstPickup_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstPickup.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            if (Request.QueryString["ID"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                BindList(ID, txtFromDate.Text, txtToDate.Text, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                BindList(ID, txtFromDate.Text, txtToDate.Text, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherStudentPickupReport" + Session["Generator5"].ToString() + ".xlsx");
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

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = (List<MViewStudentPickUp>)Session["PickUpObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherStudentPickupReport.xlsx" + Session["Generator5"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickers Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Pick_Date);
                    CreateCell(row, item.Status);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickerName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=TeacherStudentPickupReport" + Session["Generator5"].ToString() + ".xlsx");
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
                ErrorLogManagement.AddLog(ex);
            }
        }

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            drpPicker.SelectedValue = "";
            drpStatus.SelectedValue = "";
            drpSort.SelectedValue = "Date";
            rbDesc.Checked = true;
            int StudentID = Convert.ToInt32(Request.QueryString["ID"]);
            BindList(StudentID, "", "", "", "", "Date");
        }

        protected void email_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = CreateSpreadsheetWorkbooks(Server.MapPath("../Upload") + "/TeacherStudentPickupReport" + Session["Generator5"].ToString() + ".xlsx");
                Session["UploadFilePath"] = filePath;
                Response.Redirect("~/Teacher/Message.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private string CreateSpreadsheetWorkbooks(string filepath)
        {
            string returnFilePath = "";
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = (List<MViewStudentPickUp>)Session["PickUpObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherStudentPickupReport.xlsx" + Session["Generator5"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickers Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Pick_Date);
                    CreateCell(row, item.Status);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickerName);
                }

                spreadsheetDocument.Close();


                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                //if (isFileDownLoadRequired)
                //{
                //    Response.Clear();
                //    Response.Charset = "";
                //    Response.AddHeader("Content-Disposition", "attachment; filename=TeacherStudentPickupReport" + Session["Generator5"].ToString() + ".xlsx");
                //    Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                //    Response.ContentType = "application/octet-stream";
                //    Response.TransmitFile(fileinfo.FullName);
                //    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                //    Response.Flush();
                //    Response.End();
                //}

                //EmailManagement ObjEmailManagement = new EmailManagement();
                //string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: StudentPickUpReport<br><br>Description &nbsp;: Here we Send you a Mail for Student PickUp Report So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", Authentication.LogginTeacher.Name);
                //string FileNames = Server.MapPath("../Upload/TeacherStudentPickupReport" + Session["Generator5"].ToString() + ".xlsx");
                //returnFilePath = FileNames;
                //ObjEmailManagement.SendEmails(Authentication.LogginTeacher.Email, "StudentPickUpReport", Message, FileNames);
                //AlertMessageManagement.ServerMessage(Page, "Mail Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

            return returnFilePath;
        }
    }
}