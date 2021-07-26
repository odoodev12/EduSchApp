using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Data.Entity;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.Web.Teacher
{
    public partial class ClassDailyStatus : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement ObjEmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (!IsPostBack)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator1"] = r.ToString();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindDropdown();
                if (Session["drpClass2"] != null)
                {
                    drpClass.SelectedValue = Session["drpClass2"].ToString();
                }
                BindStudentDropdown(drpClass.SelectedValue, txtDate.Text);
                BindList(drpClass.SelectedValue, "", "0", "", "0");

            }
        }
        private void BindStudentDropdown(string classID, string Date)
        {
            if (!String.IsNullOrEmpty(classID))
            {
                int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                List<ISStudent> ObjStudent = new List<ISStudent>();
                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    //if (_Class.Name.Contains("Outside Class"))
                    //{
                    //    string Format = "";
                    //    DateTime dt = DateTime.Now;
                    //    if (Date != "")
                    //    {
                    //        string dates = Date;
                    //        if (dates.Contains("/"))
                    //        {
                    //            string[] arrDate = dates.Split('/');
                    //            Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    //        }
                    //        else
                    //        {
                    //            Format = dates;
                    //        }

                    //    }
                    //    dt = Convert.ToDateTime(Format);
                    //    ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && p.ClassID == ClassID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList();
                    //}
                    //else
                    {
                        ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && p.ClassID == ClassID).ToList();
                    }
                }
                else
                {
                    if (_Class.TypeID == (int)EnumsManagement.CLASSTYPE.Office || _Class.TypeID == (int)EnumsManagement.CLASSTYPE.Club || _Class.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                    {
                        ClassManagement classManagement = new ClassManagement();
                        List<int> result = classManagement.GetStudentListForReportFilterOnly(ClassID, "", Authentication.LogginTeacher.ID, Authentication.SchoolID);
                        ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID).ToList();
                        ObjStudent = ObjStudent.Where(r => result.Contains(r.ID)).ToList();
                    }
                    else
                        ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && p.ClassID == ClassID).ToList();
                }

                drpStudent.DataSource = ObjStudent.OrderBy(r=>r.StudentName).ToList(); //DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && p.ClassID == ClassID).ToList();
                drpStudent.DataTextField = "StudentName";
                drpStudent.DataValueField = "ID";
                drpStudent.DataBind();
                drpStudent.Items.Insert(0, new ListItem { Text = "Select Student", Value = "0" });
            }
            else
            {
                AlertMessageManagement.ServerMessage(Page, "No Classess assigned.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
        }
        private void BindDropdown()
        {
            int ID = Authentication.LogginTeacher.ID;
            int SchoolID = DB.ISTeachers.SingleOrDefault(p => p.ID == ID).SchoolID;

            drpClass.DataSource = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList()
                                   join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                                   select new ISClass
                                   {
                                       ID = item2.ID,
                                       Name = item2.Name
                                   }).ToList().OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            drpClass.DataTextField = "Name";
            drpClass.DataValueField = "ID";
            drpClass.DataBind();

            //drpStudent.DataSource = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID).ToList();
            //drpStudent.DataTextField = "StudentName";
            //drpStudent.DataValueField = "ID";
            //drpStudent.DataBind();
            //drpStudent.Items.Insert(0, new ListItem { Text = "Select Student", Value = "0" });

            drpStatus.DataSource = DB.ISPickUpStatus.Where(p => p.Active == true).ToList().OrderBy(r => r.Status).ToList();
            drpStatus.DataTextField = "Status";
            drpStatus.DataValueField = "Status";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "" });

        }
        private void BindList(string ClassID, string Date, string StudentID, string PickupStatus, string SortByStudentID)
        {
            Session["ClassObjectlist"] = null;
            int ID = Authentication.LogginTeacher.ID;
            int ClasssID = ClassID != "" ? Convert.ToInt32(ClassID) : 0;
            int StudentsID = StudentID != "0" ? Convert.ToInt32(StudentID) : 0;
            if (ClasssID != 0)
            {
                ClassManagement objClassManagement = new ClassManagement();

                List<MViewStudentPickUp> objList = objClassManagement.DailyClassReports(Authentication.SchoolID, Date, ClasssID, ID, StudentsID, PickupStatus, rbAsc.Checked == true ? "ASC" : "DESC", SortByStudentID);

                //edited by Maharshi@gatistavamSoftech   this is ready to work -->  DB.ISClasses.Where().Select(a => a.Name)
                objList.ForEach(x => x.PickStatus = x.PickStatus == "After School Ex" ? x.PickStatus + "(" + getclassneme_forlist(Convert.ToInt32(x.ID)) + ")" : x.PickStatus);

                DateTime dt = DateTime.Now;
                if (Date != "")
                {
                    string dates = Date;
                    string Format = "";
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    }
                    else
                    {
                        Format = dates;
                    }
                    dt = Convert.ToDateTime(Format);
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

                    if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (item.PickStatus == "Not Marked")
                        {
                            item.PickStatus = "Weekend (School Closed)";
                            item.Pick_Time = "";
                            item.PickerName = "";
                        }
                    }

                    if (item.PickStatus.Contains("After-School-Ex"))
                    {
                        ISClass Cid = DB.ISClasses.FirstOrDefault(r => r.TypeID == (int)CLASSTYPE.AfterSchool && r.SchoolID == item.SchoolID);
                        MISClass Obj = objClassManagement.GetClass(Cid.ID);
                        item.SchoolName = Obj.ExternalOrganisation;
                    }
                }

                ListView1.DataSource = objList.OrderBy(r=>r.StudentName).ToList();
                ListView1.DataBind();
                Session["ClassObjectlist"] = objList.ToList();
            }
        }
        //method Created by Maharshi @GatistavamSoftech  Funtion --> it will select Class name by id
        public String getclassneme_forlist(int id)
        {

            var class_name = DB.ISClasses.FirstOrDefault(p => p.ID == id && p.AfterSchoolType == "External");
            if (class_name != null) { return class_name.Name; }
            else { return ""; }
        }

        /////////////////////
        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)ListView1.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }
        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpClass2"] = null;
            if (drpClass.SelectedValue != "0")
            {
                Session["drpClass2"] = drpClass.SelectedValue;
                BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
                BindStudentDropdown(drpClass.SelectedValue, txtDate.Text);
            }
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (txtDate.Text != "")
            {
                BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
                BindStudentDropdown(drpClass.SelectedValue, txtDate.Text);
            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx");
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
                var lists = (List<MViewStudentPickUp>)Session["ClassObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickers Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.PickStatus);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickerName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx");
                Response.AddHeader("Content-Length", fileinfo.Length.ToString());
                Response.ContentType = "application/octet-stream";
                ///GvUserList.HeaderStyle.Font.Bold = true;
                Response.TransmitFile(fileinfo.FullName);
                //Response.Flush();
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindDropdown();
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            drpStudent.SelectedValue = "0";
            drpStatus.SelectedValue = "";
            drpSort.SelectedValue = "0";
            rbDesc.Checked = true;
            BindList(drpClass.SelectedValue, "", "0", "", "0");
        }

        protected void email_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = CreateSpreadsheetWorkbooks(Server.MapPath("../Upload") + "/TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx");
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
                var lists = (List<MViewStudentPickUp>)Session["ClassObjectlist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx").ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickers Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.PickStatus);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickerName);
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;
                //string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: ClassDailyReport<br><br>Description &nbsp;: Here we Send you a Mail for Class Daily Report So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", Authentication.LogginTeacher.Name);
                //string FileNames = Server.MapPath("../Upload/TeacherClassDailyReport" + Session["Generator1"].ToString() + ".xlsx");
                //ObjEmailManagement.SendEmails(Authentication.LogginTeacher.Email, "ClassDailyReport", Message, FileNames);
                //AlertMessageManagement.ServerMessage(Page, "Mail Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

            return returnFilePath;
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var hyperLink = e.Item.FindControl("lnkReport") as HyperLink;
                //if (drpClass.SelectedItem.Text.Contains("Outside Class"))
                //{
                //    hyperLink.Visible = false;
                //}
                //else
                {
                    hyperLink.Visible = true;
                }
            }
        }
    }
}