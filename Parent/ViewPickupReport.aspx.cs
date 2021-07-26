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

namespace SchoolApp.Web.Parent
{
    public partial class ViewPickupReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        PickupManagement objPickupManagement = new PickupManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            //if (!IsPostBack)
            //{
            //    //showStudent();
            //}
            if (!IsPostBack)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator2"] = r.ToString();
                if (Request.QueryString["Name"] != null)
                {
                    string StudentName = Request.QueryString["Name"];
                    int SchoolID = 0;
                    if (Request.QueryString["SchoolID"] != null)
                    {
                        SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                    }
                    ISStudent stud = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == StudentName && p.Active == true);
                    BindList(stud.ID, "", "", "", "", drpSort.SelectedValue, SchoolID);
                    BindDropdown();
                }
            }

        }
        private void BindDropdown()
        {
            if (Request.QueryString["Name"] != null)
            {
                string StudentName = Request.QueryString["Name"].ToString();
                int SchoolID = 0;
                if (Request.QueryString["SchoolID"] != null)
                {
                    SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                }
                ISStudent student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == StudentName && p.Active == true);
                if (student != null)
                {
                    PickerManagement objPickerManagement = new PickerManagement();
                    List<MISPickerAssignment> objPickerList = new List<MISPickerAssignment>();
                    List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(student.ID);
                    List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(student.ID);
                    foreach (MISPickerAssignment items in objList2)
                    {
                        if (objList.Where(p => p.ID == items.ID).Count() <= 0)
                        {
                            objList.Add(items);
                            objPickerList.Add(items);
                        }
                    }
                    var list = objList.Select(p => new { PickerName = p.PickerName }).Distinct().ToList();
                    drpPicker.DataSource = list.OrderBy(r => r.PickerName);//DB.ISPickers.Where(p => p.Deleted == true && p.Active == true && p.StudentID == Authentication.LogginParent.ID).ToList();
                    drpPicker.DataTextField = "PickerName";
                    drpPicker.DataValueField = "PickerName";
                    drpPicker.DataBind();
                    drpPicker.Items.Insert(0, new ListItem { Text = "Select Picker", Value = "" });
                }
            }

            drpStatus.DataSource = DB.ISPickUpStatus.Where(p => p.Active == true).OrderBy(r=>r.Status).ToList();
            drpStatus.DataTextField = "Status";
            drpStatus.DataValueField = "Status";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "" });
        }
        private void BindList(int StudentID, string FromDate, string ToDate, string PickerID, string Status, string SortBy, int schoolID)
        {
            Session["StudentObjectlist"] = null;
            lblInstruction.Visible = (FromDate == "" || ToDate == "");

            DateTime dt = DateTime.Now.Date;
            List<MViewStudentPickUp> objList = objPickupManagement.StudentPickUpReportsWithSchool(StudentID, FromDate, ToDate, PickerID, "", SortBy, rbAsc.Checked == true ? "ASC" : "DESC", StudentID, schoolID);
            int? schooolId = DB.ISSchools.FirstOrDefault(r => r.ID == schoolID)?.TypeID;
            foreach (var item in objList)
            {
                if (OperationManagement.DefaultPickupStatuslist.Contains(item.PickStatus))
                {
                    item.Pick_Time = "";
                    item.PickerName = "";
                }


                if (string.Compare(item.PickStatus, "No School", true) == 0)
                    item.PickStatus = "Weekend (School Closed)";


                if (item.PickDate.Value.DayOfWeek == DayOfWeek.Saturday || item.PickDate.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (item.PickStatus == "Not Marked")
                    {
                        item.PickStatus = "Weekend (School Closed)";
                        item.Pick_Time = "";
                        item.PickerName = "";
                    }
                }

                

                if (schooolId.HasValue && schooolId.Value == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    ISAttendance _Attendence = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.Date.Value) == DbFunctions.TruncateTime(item.PickDate) && p.Active == true && p.Status.Contains("Present"));
                    if (_Attendence != null)
                    {
                        if(item.PickStatus == "Not Marked" || item.PickStatus == "Weekend (School Closed)")
                        {
                            item.PickStatus = "UnPicked";
                        }
                    }
                }

            }

            if (Status != "")
            {
                objList = objList.Where(r => r.PickStatus == Status).ToList();
            }

            lstTable.DataSource = objList.ToList();
            lstTable.DataBind();
            Session["StudentObjectlist"] = objList.ToList();

            ClassManagement obj = new ClassManagement();

            int SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
            string schoolName = " @ " + DB.ISSchools.FirstOrDefault(r => r.ID == SchoolID).Name;

            lstStudeInfo.DataSource = (from item in DB.ISStudents.Where(p => p.ID == StudentID && p.SchoolID == schoolID).ToList()
                                       select new MViewStudentPickUp
                                       {
                                           ID = null,
                                           StudentID = item.ID,
                                           StudentName = item.StudentName,
                                           StudentPic = item.Photo,
                                           TeacherID = null,
                                           PickStatus = "",
                                           Pick_Time = "",
                                           Pick_Date = "",
                                           PickerID = null,
                                           PickDate = null,
                                           PickerName = null,
                                           PickerImage = null,
                                           ClassID = item.ClassID,
                                           StudentPickUpAverage = obj.GetAverage(item.ClassID, item.ID),
                                           SchoolName = schoolName
                                       }).ToList();//DB.ISStudents.Where(p => p.ID == StudentID && p.Deleted == true).ToList();
            lstStudeInfo.DataBind();

        }
        protected void lstTable_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstTable.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            if (Request.QueryString["Name"] != null)
            {
                string StudentName = Request.QueryString["Name"].ToString();
                int SchoolID = 0;
                if (Request.QueryString["SchoolID"] != null)
                {
                    SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                }
                ISStudent stud = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == StudentName && p.Active == true);
                BindList(stud.ID, txtFromDate.Text, txtToDate.Text, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue, SchoolID);
            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Name"] != null)
            {
                string StudentName = Request.QueryString["Name"].ToString();
                int SchoolID = 0;
                if (Request.QueryString["SchoolID"] != null)
                {
                    SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                }
                ISStudent stud = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == StudentName && p.Active == true);
                BindList(stud.ID, txtFromDate.Text, txtToDate.Text, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue, SchoolID);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/ViewStudentPickupReport" + Session["Generator2"].ToString() + ".xlsx");
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
                var lists = (List<MViewStudentPickUp>)Session["StudentObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ViewStudentPickupReport" + Session["Generator2"].ToString() + ".xlsx").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Picker Name");
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
                Response.AddHeader("Content-Disposition", "attachment; filename=ViewStudentPickupReport" + Session["Generator2"].ToString() + ".xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(fileinfo.FullName);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            drpPicker.SelectedValue = "";
            drpStatus.SelectedValue = "";
            drpSort.SelectedValue = "Date";
            rbDesc.Checked = true;
            if (Request.QueryString["Name"] != null)
            {
                string StudentName = Request.QueryString["Name"].ToString();
                int SchoolID = 0;
                if (Request.QueryString["SchoolID"] != null)
                {
                    SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                }
                ISStudent stud = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == StudentName && p.Active == true);
                //BindList(stud.ID, "", "", drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue, SchoolID);
                BindList(stud.ID, "", "", "", "", drpSort.SelectedValue, SchoolID);
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string StudentName = Request.QueryString["Name"];
                int SchoolID = 0;
                if (Request.QueryString["SchoolID"] != null)
                {
                    SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                }

                string filePath = CreateSpreadsheetWorkbooks(Server.MapPath("../Upload") + "/ViewStudentPickupReport" + Session["Generator2"].ToString() + ".xlsx");
                Session["UploadFilePath"] = filePath;
                Response.Redirect($"~/Parent/NewMessageaspx.aspx?Name={StudentName}&SchoolID={SchoolID}");
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
                var lists = (List<MViewStudentPickUp>)Session["StudentObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("ViewStudentPickupReport" + Session["Generator2"].ToString() + ".xlsx").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Picker Name");
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
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

            return returnFilePath;

        }
    }
}