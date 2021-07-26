using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class ClassAttendanceReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
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
                BindDropdown();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (Session["drpClass1"] != null)
                {
                    drpClass.SelectedValue = Session["drpClass1"].ToString();
                }
                if (Request.QueryString["ID"] != null)
                {
                    drpClass.SelectedValue = Request.QueryString["ID"].ToString();
                }
                BindList(drpClass.SelectedValue, "", "0", "", "0");
                BindStudentDropdown(drpClass.SelectedValue, txtDate.Text);
                
            }
        }

        private void BindStudentDropdown(string classID, string Date)
        {
            if (!String.IsNullOrEmpty(classID))
            {
                int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                List<ISStudent> ObjStudent = new List<ISStudent>();
                if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
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
                    ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && p.ClassID == ClassID).ToList();
                }

                drpStudent.DataSource = ObjStudent; //DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Authentication.SchoolID && p.ClassID == ClassID).ToList();
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
                                   join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Office && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                                   select new ISClass
                                   {
                                       ID = item2.ID,
                                       Name = item2.Name
                                   }).ToList();
            drpClass.DataTextField = "Name";
            drpClass.DataValueField = "ID";
            drpClass.DataBind();
        }

        private void BindList(string ClassID, string Date, string StudentID, string AttendenceStatus, string SortByStudentID)
        {
            if (!String.IsNullOrEmpty(ClassID)) {
                int ID = Authentication.LogginTeacher.ID;
                int ClassIDs = Convert.ToInt32(ClassID);
                int StudentIDs = Convert.ToInt32(StudentID);
                StudentManagement objStudentManagement = new StudentManagement();
                string Format = "";
                if (Date != "")
                {
                    string dates = Date;
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    }
                    else
                    {
                        Format = dates;
                    }
                }
                List<MISAttendance> objList = new List<MISAttendance>();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objStudentManagement.AttendenceList(Authentication.SchoolID, ClassIDs, Format, StudentIDs, ID, AttendenceStatus, rbAsc.Checked == true ? "ASC" : "DESC", SortByStudentID);
                }
                else
                {
                    objList = objStudentManagement.AttendenceLists(Authentication.SchoolID, ClassIDs, Format, StudentIDs, ID, AttendenceStatus, rbAsc.Checked == true ? "ASC" : "DESC", SortByStudentID);
                }
                lstAttendanceRpt.DataSource = objList.ToList();
                lstAttendanceRpt.DataBind();
                //start by shailesh parmar
                Session["ClassAttendencelist"] = null;
                Session["ClassAttendencelist"] = objList.ToList();
                //end
            }
            else
            {
                AlertMessageManagement.ServerMessage(Page, "No Classess assigned.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
        }
        protected void lstAttendanceRpt_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstAttendanceRpt.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }
        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpClass1"] = null;
            if (drpClass.SelectedValue != "0")
            {
                Session["drpClass1"] = drpClass.SelectedValue;
                BindStudentDropdown(drpClass.SelectedValue, txtDate.Text);
                BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (txtDate.Text != "")
            {
                BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BindDropdown();
            BindStudentDropdown(drpClass.SelectedValue, txtDate.Text);
            drpStudent.SelectedValue = "0";
            drpStatus.SelectedValue = "";
            drpSort.SelectedValue = "0";
            rbDesc.Checked = true;
            BindList(drpClass.SelectedValue, txtDate.Text, drpStudent.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }

        protected void lstAttendanceRpt_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var hyperLink = e.Item.FindControl("lnkReport") as HyperLink;
                //////////var HID = e.Item.FindControl("HID") as HiddenField;
                //////////int ID = Convert.ToInt32(HID.Value);
                //////////ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                

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
        private class CustomerGroupingKey
        {
            public CustomerGroupingKey(string SName, int ID)
            {
                StudentName = SName;
                this.ID = ID;
            }

            public string StudentName { get; }

            public int ID { get; }
        }

        //start by shailesh parmar
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbook(Server.MapPath("../Upload") + "/AttendenceReport" + Session["Generator3"].ToString() + ".xlsx");
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
                NumberGenerator();
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                ///Adding Sheet here
                Row Hederrow;
                var lists = (List<MISAttendance>)Session["ClassAttendencelist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("AttendenceReport" + Session["Generator3"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "StudentName");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Date");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.Status != "" ? item.Status : "Not Marked");
                    CreateCell(row, item.Date.ToString() != "" ? item.Date.ToString() : "");
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                Response.Clear();
                Response.Charset = "";
                Response.AddHeader("Content-Disposition", "attachment; filename=AttendenceReport" + Session["Generator3"].ToString() + ".xlsx");
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

        protected void email_Click(object sender, EventArgs e)
        {
            try
            {
                CreateSpreadsheetWorkbooks(Server.MapPath("../Upload") + "/AttendenceReport" + Session["Generator3"].ToString() + ".xlsx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void NumberGenerator()
        {
            
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            Session["Generator3"] = null;
            Session["Generator3"] = r.ToString();
           
        }
        private void CreateSpreadsheetWorkbooks(string filepath)
        {
            try
            {
                NumberGenerator();
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                ///Adding Sheet here
                Row Hederrow;
                var lists = (List<MISAttendance>)Session["ClassAttendencelist"];
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("AttendenceReport" + Session["Generator3"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "StudentName");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Date");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.Status != "" ? item.Status : "Not Marked");
                    CreateCell(row, item.Date.ToString() != "" ? item.Date.ToString() : "");
                }

                spreadsheetDocument.Close();
                EmailManagement ObjEmailManagement = new EmailManagement();
                string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: AttendenceReport<br><br>Description &nbsp;: Here we Send you a Mail for AttendeneList So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", Authentication.LogginTeacher.Name);
                string FileNames = Server.MapPath("../Upload/AttendenceReport" + Session["Generator3"].ToString() + ".xlsx");
                ObjEmailManagement.SendEmails(Authentication.LogginTeacher.Email, "AttendenceReport", Message, FileNames);
                AlertMessageManagement.ServerMessage(Page, "Mail Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        //End
    }
}