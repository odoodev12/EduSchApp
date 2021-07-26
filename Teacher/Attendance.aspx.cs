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

namespace SchoolApp.Web.Teacher
{
    public partial class Attendance : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
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
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator3"] = r.ToString();
                lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindDropdown();
                if (Session["drpClass"] != null)
                {
                    drpClass.SelectedValue = Session["drpClass"].ToString();
                }
                BindList(drpClass.SelectedValue, "");
            }

        }
        private void BindCheckBox()
        {
            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
            {
                int Class = Convert.ToInt32(drpClass.SelectedValue);
                DateTime dt = DateTime.Now;

                ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                if (_class != null)
                {
                    List<MISStudent> objList = new List<MISStudent>();
                    //if (_class.Name.Contains("Outside Class"))
                    //{
                    //    objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                    //               select new MISStudent
                    //               {
                    //                   ID = item.ID,
                    //                   StudentName = item.StudentName,
                    //                   Photo = item.Photo,
                    //                   CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                    //                   ClassID = item.ClassID
                    //               }).ToList();
                    //}
                    //else
                    {
                        objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                   select new MISStudent
                                   {
                                       ID = item.ID,
                                       StudentName = item.StudentName,
                                       Photo = item.Photo,
                                       CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                       ClassID = item.ClassID
                                   }).ToList();
                    }

                    foreach (var item in objList.ToList())
                    {
                        ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt) && p.AttendenceComplete == true);
                        if (objs != null)
                        {
                            var itemToRemove = objList.Single(r => r.ID == item.ID);
                            objList.Remove(itemToRemove);
                        }
                    }
                    if (objList.Count == 0)
                    {
                        chkPresent.Checked = true;
                    }
                    else
                    {
                        chkPresent.Checked = false;
                    }
                }
            }
            else
            {
                int Class = Convert.ToInt32(drpClass.SelectedValue);
                List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                            select new MISStudent
                                            {
                                                ID = item.ID,
                                                StudentName = item.StudentName,
                                                Photo = item.Photo,
                                                CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                ClassID = item.ClassID
                                            }).ToList();
                DateTime dt = DateTime.Now;
                foreach (var item in objList.ToList())
                {
                    ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt) && p.AttendenceComplete == true);
                    if (objs != null)
                    {
                        var itemToRemove = objList.Single(r => r.ID == item.ID);
                        objList.Remove(itemToRemove);
                    }
                }
                if (objList.Count == 0)
                {
                    chkPresent.Checked = true;
                }
                else
                {
                    chkPresent.Checked = false;
                }
            }
        }
        private void BindDropdown()
        {
            int ID = Authentication.LogginTeacher.ID;
            int SchoolID = DB.ISTeachers.SingleOrDefault(p => p.ID == ID).SchoolID;
            drpClass.DataSource = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList()
                                   join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.Standard) on item.ClassID equals item2.ID
                                   select new ISClass
                                   {
                                       ID = item2.ID,
                                       Name = item2.Name
                                   }).ToList();
            drpClass.DataTextField = "Name";
            drpClass.DataValueField = "ID";
            drpClass.DataBind();
            ///drpClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }
        private void BindList(string ClassID, string StudentName)
        {
            if (!String.IsNullOrEmpty(ClassID))
            {
                Session["Attendencelist"] = null;
                int ID = Authentication.LogginTeacher.ID;
                int Class = Convert.ToInt32(ClassID);
                DateTime dt = DateTime.Now;
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                List<MISStudent> obj = new List<MISStudent>();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                    obj = (from item in DB.getAttandanceData(dt).Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && (p.StartDate == null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts) && p.StudentDeleted == true).ToList()
                           select new MISStudent
                           {
                               ID = item.StudentsID,
                               StudentName = item.StudentName,
                               Photo = item.Photo,
                               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                               ClassID = item.ClassID,
                               AttStatus = item.Status != null ? item.Status : "",
                               AttDate = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : ""
                           }).OrderBy(r=>r.StudentName).ToList();
                }
                else
                {
                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                    obj = (from item in DB.getAttandanceData(dt).Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && (p.StartDate == null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts) && p.StudentDeleted == true).ToList()
                           select new MISStudent
                           {
                               ID = item.StudentsID,
                               StudentName = item.StudentName,
                               Photo = item.Photo,
                               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                               ClassID = item.ClassID,
                               AttStatus = item.Status != null ? item.Status : "",
                               AttDate = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : ""
                           }).OrderBy(r => r.StudentName).ToList();
                }

                if (StudentName != "Select Student" && StudentName != "")
                {
                    obj = obj.Where(p => p.StudentName.ToLower() == StudentName.ToLower()).ToList();
                }
                else
                {   
                    drpStudent.DataSource = obj;
                    drpStudent.DataTextField = "StudentName";
                    drpStudent.DataValueField = "ID";
                    drpStudent.DataBind();
                    drpStudent.Items.Insert(0, new ListItem { Text = "Select Student", Value = "0" });
                }
                lstAttendance.DataSource = obj.ToList();
                lstAttendance.DataBind();
                BindCheckBox();
                Session["Attendencelist"] = obj.ToList();
            }
            else
            {
                AlertMessageManagement.ServerMessage(Page, "No Classess assigned.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
        }
        protected void lstAttendance_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chk = (CheckBox)e.Item.FindControl("rbtnPresent");
                CheckBox chk1 = (CheckBox)e.Item.FindControl("rbtnAbsent");
                Label lbl = (Label)e.Item.FindControl("StylistIdlbl");
                int StdID = Convert.ToInt32(lbl.Text);

                DateTime dt = Convert.ToDateTime(DateTime.Now);
                ISAttendance obj = DB.ISAttendances.SingleOrDefault(p => p.StudentID == StdID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                if (obj != null)
                {
                    if (obj.Status.Contains("Present"))
                    {
                        chk.Checked = true;
                        chk1.Checked = false;
                    }
                    else
                    {
                        chk.Checked = false;
                        chk1.Checked = true;
                    }
                }
                else
                {
                    chk.Checked = false;
                    chk1.Checked = false;
                }
            }
            //BindList(drpClass.SelectedValue);
        }
        protected void lstAttendance_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstAttendance.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
        }
        protected void btnViewAttendence_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Teacher/ClassAttendanceReport.aspx?ID=" + drpClass.SelectedValue);
        }
        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpClass"] = null;
            if (drpClass.SelectedValue != "0")
            {
                Session["drpClass"] = drpClass.SelectedValue;
                BindList(drpClass.SelectedValue, "");
            }
            else
            {
                Session["drpClass"] = null;
                BindList("0", drpStudent.SelectedItem.Text);
            }
        }
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
                var lists = (List<MISStudent>)Session["Attendencelist"];
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
                    CreateCell(row, item.AttStatus != "" ? item.AttStatus : "Not Marked");
                    CreateCell(row, item.AttDate != "" ? item.AttDate : "");
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
        protected void lstAttendance_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SendStatus")
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                int ID = Convert.ToInt32(e.CommandArgument);
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
                DateTime dt2 = DateTime.Now;
                ISAttendance objAttendance = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Deleted == true && p.Active == true);
                if (objAttendance == null)
                {
                    AlertMessageManagement.ServerMessage(Page, "Student Attendence Should be fill-up First", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    if (objStudent.EmailAfterConfirmAttendence.HasValue && objStudent.EmailAfterConfirmAttendence.Value)
                    {
                        SendStatusEmailManage(objStudent, objAttendance.Status);
                        AlertMessageManagement.ServerMessage(Page, "Message Sent Successfully.", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "Message can not be sent due to parent has not set yet to email notifiction.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }


            }

        }
        protected void rbtnPresent_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;

            int ID = rbtn.ToolTip != null ? Convert.ToInt32(rbtn.ToolTip) : 0;
            ISStudent student = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            DateTime dt2 = DateTime.Now;
            //if (lblDate.Text != "")
            //{
            //    string dates = lblDate.Text;
            //    string Format = "";
            //    if (dates.Contains("/"))
            //    {
            //        string[] arrDate = dates.Split('/');
            //        Format = arrDate[2].ToString() + "-" + arrDate[1].ToString() + "-" + arrDate[0].ToString();
            //    }
            //    else
            //    {
            //        Format = dates;
            //    }

            //    dt2 = Convert.ToDateTime(Format);
            //}
            if (rbtn.Checked)
            {
                if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    ISPickup pickObjectStandardSchool = null;

                    List<ISStudent> _StList = DB.ISStudents.Where(p => p.ID != student.ID && p.StudentName == student.StudentName && (p.ParantEmail1 == student.ParantEmail1 || p.ParantEmail2 == student.ParantEmail2 || p.ParantEmail1 == student.ParantEmail2 || p.ParantEmail2 == student.ParantEmail1) && p.StartDate == (DateTime?)null && p.Deleted == true).ToList();
                    bool ISMarked = false;

                    foreach (var items in _StList)
                    {
                        //if (items.ISSchool.isAttendanceModule == true)
                        {
                            ISAttendance _Attendence = DB.ISAttendances.FirstOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.Status.Contains("Present") && p.StudentID == items.ID && p.Active == true && p.Deleted == true);
                            ISPickup _PickUp = DB.ISPickups.SingleOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2) && p.StudentID == items.ID && (p.PickStatus == "Not Marked" || p.PickStatus == "UnPicked"));
                            if (_Attendence != null && _PickUp != null)
                            {
                                ISMarked = true;
                            }

                            ISPickup _PickUp1 = DB.ISPickups.FirstOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2) && p.StudentID == items.ID && (p.PickStatus == "Office" || p.PickStatus == "After-School" || p.PickStatus == "Club" || p.PickStatus == "UnPicked"));
                            if (_PickUp1 != null)
                            {
                                ISMarked = true;
                            }

                            if (items.ISSchool.TypeID == 2)
                                pickObjectStandardSchool = DB.ISPickups.FirstOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2) && p.StudentID == items.ID && (p.PickStatus == "After-School-Ex"));
                        }
                    }
                    if (ISMarked == false)
                    {
                        var list = DB.ISAttendances.Where(p => p.ISStudent.StudentName == student.StudentName && p.Status.Contains("Present") && p.ISStudent.ParantEmail1 == student.ParantEmail1 && p.ISStudent.ParantEmail2 == student.ParantEmail2 && p.ISStudent.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.Active == true && p.Deleted == true).ToList();
                        if (list.Count() > 0)
                        {
                            ISAttendance attendance = list.OrderByDescending(p => p.ID).FirstOrDefault();
                            if (attendance.ISStudent.ISSchool.isAttendanceModule == false)
                            {
                                ISPickup _PickUp = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                if (_PickUp != null)
                                {
                                    if (attendance.ISStudent.ClassID == student.ClassID)
                                    {
                                        rbtn.Checked = true;
                                        ISAttendance objAttendence = new ISAttendance();
                                        ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                        if (objAtte != null)
                                        {
                                            if (objAtte.AttendenceComplete == true)
                                                objAtte.Status = "Present(Late)";
                                            else
                                                objAtte.Status = "Present";
                                            objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                            objAtte.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                            ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                            if (objs != null)
                                            {
                                                objs.PickStatus = "UnPicked";
                                                DB.SaveChanges();
                                            }
                                            AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                        }
                                        else
                                        {
                                            objAttendence.Date = dt2;
                                            objAttendence.StudentID = ID;
                                            objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                            objAttendence.Status = "Present";
                                            string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                            objAttendence.Time = DateTime.Parse(DTS);
                                            objAttendence.Active = true;
                                            objAttendence.Deleted = true;
                                            objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                            objAttendence.CreatedDateTime = DateTime.Now;
                                            DB.ISAttendances.Add(objAttendence);
                                            DB.SaveChanges();

                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (pickup != null)
                                            {
                                                pickup.PickTime = DateTime.Now;
                                                pickup.PickDate = DateTime.Now;
                                                pickup.PickStatus = "UnPicked";
                                                DB.SaveChanges();
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = ID;
                                                obj.ClassID = student.ClassID;
                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                obj.PickTime = DateTime.Now;
                                                obj.PickDate = DateTime.Now;
                                                obj.PickStatus = "UnPicked";
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                            }
                                            AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                        }
                                    }
                                    else
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "Attendence already filled up in other class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                }
                                else
                                {
                                    if (attendance.ISStudent.ClassID == student.ClassID)
                                    {
                                        rbtn.Checked = true;
                                        ISAttendance objAttendence = new ISAttendance();
                                        ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                        if (objAtte != null)
                                        {
                                            if (objAtte.AttendenceComplete == true)
                                                objAtte.Status = "Present(Late)";
                                            else
                                                objAtte.Status = "Present";
                                            objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                            objAtte.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                            ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                            if (objs != null)
                                            {
                                                objs.PickStatus = "UnPicked";
                                                DB.SaveChanges();
                                            }
                                            AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                        }
                                        else
                                        {
                                            objAttendence.Date = dt2;
                                            objAttendence.StudentID = ID;
                                            objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                            objAttendence.Status = "Present";
                                            string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                            objAttendence.Time = DateTime.Parse(DTS);
                                            objAttendence.Active = true;
                                            objAttendence.Deleted = true;
                                            objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                            objAttendence.CreatedDateTime = DateTime.Now;
                                            DB.ISAttendances.Add(objAttendence);
                                            DB.SaveChanges();

                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (pickup != null)
                                            {
                                                pickup.PickTime = DateTime.Now;
                                                pickup.PickDate = DateTime.Now;
                                                pickup.PickStatus = "UnPicked";
                                                DB.SaveChanges();
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = ID;
                                                obj.ClassID = student.ClassID;
                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                obj.PickTime = DateTime.Now;
                                                obj.PickDate = DateTime.Now;
                                                obj.PickStatus = "UnPicked";
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                            }
                                            AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                        }
                                    }
                                    else
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "Attendence already filled up in other class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                }
                            }
                            else
                            {
                                if (attendance.ISStudent.ClassID == student.ClassID)
                                {
                                    rbtn.Checked = true;
                                    ISAttendance objAttendence = new ISAttendance();
                                    ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                    if (objAtte != null)
                                    {
                                        if (objAtte.AttendenceComplete == true)
                                            objAtte.Status = "Present(Late)";
                                        else
                                            objAtte.Status = "Present";
                                        objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                        objAtte.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                        ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                        if (objs != null)
                                        {
                                            objs.PickStatus = "Not Marked";
                                            DB.SaveChanges();
                                        }
                                        AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                    }
                                    else
                                    {
                                        objAttendence.Date = dt2;
                                        objAttendence.StudentID = ID;
                                        objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                        objAttendence.Status = "Present";
                                        string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                        objAttendence.Time = DateTime.Parse(DTS);
                                        objAttendence.Active = true;
                                        objAttendence.Deleted = true;
                                        objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                        objAttendence.CreatedDateTime = DateTime.Now;
                                        DB.ISAttendances.Add(objAttendence);
                                        DB.SaveChanges();

                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (pickup != null)
                                        {
                                            pickup.PickTime = DateTime.Now;
                                            pickup.PickDate = DateTime.Now;
                                            pickup.PickStatus = "UnPicked";
                                            DB.SaveChanges();
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = ID;
                                            obj.ClassID = student.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            obj.PickStatus = "UnPicked";
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                    }
                                }
                                else
                                {
                                    AlertMessageManagement.ServerMessage(Page, "Attendence already filled up in other class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                            }
                        }
                        else
                        {
                            rbtn.Checked = true;
                            ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                            if (objAtte != null)
                            {
                                if (objAtte.AttendenceComplete == true)
                                    objAtte.Status = "Present(Late)";
                                else
                                    objAtte.Status = "Present";
                                objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                objAtte.ModifyDateTime = DateTime.Now;
                                DB.SaveChanges();
                                ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                if (objs != null)
                                {
                                    objs.PickStatus = "UnPicked";
                                    DB.SaveChanges();
                                }
                                AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            }
                            else
                            {
                                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                                List<ISAttendance> _Attedence = DB.ISAttendances.Where(p => p.ISStudent.StudentName == _Student.StudentName && p.ISStudent.StartDate != (DateTime?)null && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                                if (_Attedence.Count > 0)
                                {
                                    AlertMessageManagement.ServerMessage(Page, "Attendence already Filled Up in another Class", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                }
                                else
                                {
                                    //List<ISAttendance> _Attendence = DB.ISAttendances.Where(p => p.st)
                                    ISAttendance objAttendence = new ISAttendance();
                                    objAttendence.Date = dt2;
                                    objAttendence.StudentID = ID;
                                    objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                    objAttendence.Status = "Present";
                                    string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                    objAttendence.Time = DateTime.Parse(DTS);
                                    objAttendence.Active = true;
                                    objAttendence.Deleted = true;
                                    objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                    objAttendence.CreatedDateTime = DateTime.Now;
                                    DB.ISAttendances.Add(objAttendence);
                                    DB.SaveChanges();

                                  


                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = DateTime.Now;
                                        pickup.PickDate = DateTime.Now;
                                        pickup.PickStatus = "UnPicked";
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = ID;
                                        obj.ClassID = student.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "UnPicked";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                }
                            }

                            if (pickObjectStandardSchool != null)
                            {
                                ISPickup iSPickup = DB.ISPickups.FirstOrDefault(r => r.ID == pickObjectStandardSchool.ID);
                                iSPickup.AfterSchoolFlag = true;
                                DB.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        rbtn.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already Marked as Present in another School or Still not Picked from that School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else
                {
                    List<ISStudent> _StList = DB.ISStudents.Where(p => p.ID != student.ID && p.StudentName == student.StudentName && (p.ParantEmail1 == student.ParantEmail1 || p.ParantEmail2 == student.ParantEmail2 || p.ParantEmail1 == student.ParantEmail2 || p.ParantEmail2 == student.ParantEmail1) && p.StartDate == (DateTime?)null && p.Deleted == true).ToList();
                    bool ISMarked = false;
                    foreach (var items in _StList)
                    {
                        ISAttendance _Attendence = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.Status.Contains("Present") && p.StudentID == items.ID && p.Active == true && p.Deleted == true);
                        ISPickup _PickUp = DB.ISPickups.SingleOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2) && p.StudentID == items.ID && p.PickStatus == "Not Marked");
                        if (_Attendence != null && _PickUp != null)
                        {
                            ISMarked = true;
                        }
                    }
                    if (ISMarked == false)
                    {
                        ISAttendance objAttendence = new ISAttendance();
                        ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                        if (objAtte != null)
                        {
                            ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                            if (objs != null)
                            {
                                if (objs.PickStatus == "Office" || objs.PickStatus == "After-School")
                                {
                                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Attendence already filled up in other class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    objs.PickStatus = "Not Marked";
                                    DB.SaveChanges();

                                    if (objAtte.AttendenceComplete == true)
                                        objAtte.Status = "Present(Late)";
                                    else
                                        objAtte.Status = "Present";
                                    objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                    objAtte.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();

                                    AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                }
                            }
                        }
                        else
                        {
                            rbtn.Checked = true;

                            objAttendence.Date = dt2;
                            objAttendence.StudentID = ID;
                            objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                            objAttendence.Status = "Present";
                            string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                            objAttendence.Time = DateTime.Parse(DTS);
                            objAttendence.Active = true;
                            objAttendence.Deleted = true;
                            objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                            objAttendence.CreatedDateTime = DateTime.Now;
                            DB.ISAttendances.Add(objAttendence);
                            DB.SaveChanges();

                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                            if (pickup != null)
                            {
                                pickup.PickTime = DateTime.Now;
                                pickup.PickDate = DateTime.Now;
                                pickup.PickStatus = "Not Marked";
                                DB.SaveChanges();
                            }
                            else
                            {
                                ISPickup obj = new ISPickup();
                                obj.StudentID = ID;
                                obj.ClassID = student.ClassID;
                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                obj.PickTime = DateTime.Now;
                                obj.PickDate = DateTime.Now;
                                obj.PickStatus = "Not Marked";
                                DB.ISPickups.Add(obj);
                                DB.SaveChanges();
                            }
                            AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                    }
                    else
                    {
                        rbtn.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already Marked as Present in another School and Still not Picked from that School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
            }
            BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
        }
        protected void rbtnAbsent_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            int ID = rbtn.ToolTip != null ? Convert.ToInt32(rbtn.ToolTip) : 0;
            ISStudent student = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            DateTime dt2 = DateTime.Now;
            ISCompleteAttendanceRun completeattendance = DB.ISCompleteAttendanceRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == student.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2));
            //if (lblDate.Text != "")
            //{
            //    string dates = lblDate.Text;
            //    string Format = "";
            //    if (dates.Contains("/"))
            //    {
            //        string[] arrDate = dates.Split('/');
            //        //edited by maharshi @Gatistavam-Softech Changed Formating of date done it as per db formate that is yyyy/MM/dd
            //        Format = arrDate[2].ToString() + "-" + arrDate[1].ToString() + "-" + arrDate[0].ToString();
            //    }
            //    else
            //    {
            //        Format = dates;
            //    }
            //    dt2 = Convert.ToDateTime(Format);
            //}
            if (rbtn.Checked)
            {
                if (DB.ISPickups.Where(p => p.StudentID == ID && p.PickStatus != "UnPicked" && p.PickStatus != "Mark as Absent"
                && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2)).Count() <= 0 && completeattendance == null)
                {
                    List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    if (ObjHoliday.Where(x => x.DateFrom.Value <= dt2 && x.DateTo.Value >= dt2 && x.Active == true).Count() > 0)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Mark as Absent Not Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                        {
                            List<ISStudent> _StList = DB.ISStudents.Where(p => p.ID != student.ID && p.StudentName == student.StudentName && (p.ParantEmail1 == student.ParantEmail1 || p.ParantEmail2 == student.ParantEmail2 || p.ParantEmail1 == student.ParantEmail2 || p.ParantEmail2 == student.ParantEmail1) && p.StartDate == (DateTime?)null && p.Deleted == true).ToList();
                            ISPickup pickObjectStandardSchool = null;

                            foreach (var items in _StList)
                            {
                                if (items.ISSchool.TypeID == 2)
                                    pickObjectStandardSchool = DB.ISPickups.FirstOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2) && p.StudentID == items.ID && (p.PickStatus == "After-School-Ex"));
                            }


                            var list = DB.ISAttendances.Where(p => p.ISStudent.SchoolID == Authentication.SchoolID && p.Status == "" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.Active == true && p.Deleted == true);
                            if (list.Count() > 0)
                            {
                                ISAttendance attendance = list.OrderByDescending(p => p.ID).FirstOrDefault();
                                //if (attendance.ISStudent.ClassID == student.ClassID)
                                //{
                                rbtn.Checked = true;
                                ISAttendance objAttendence = new ISAttendance();
                                ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                if (objAtte != null)
                                {
                                    if (chkPresent.Checked == true)
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "Attendance cannot be changed from Present to Absent after Complete Attendance is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        objAtte.Status = "Absent";
                                        objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                        objAtte.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                        ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                        if (objs != null)
                                        {
                                            objs.PickStatus = "Mark as Absent";
                                            DB.SaveChanges();
                                        }
                                        AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                    }
                                }
                                else
                                {
                                    objAttendence.Date = dt2;
                                    objAttendence.StudentID = ID;
                                    objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                    objAttendence.Status = "Absent";
                                    string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                    objAttendence.Time = DateTime.Parse(DTS);
                                    objAttendence.Active = true;
                                    objAttendence.Deleted = true;
                                    objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                    objAttendence.CreatedDateTime = DateTime.Now;
                                    DB.ISAttendances.Add(objAttendence);
                                    DB.SaveChanges();

                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = DateTime.Now;
                                        pickup.PickDate = DateTime.Now;
                                        pickup.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = ID;
                                        obj.ClassID = student.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                }
                                //}
                                //else
                                //{
                                //    AlertMessageManagement.ServerMessage(Page, "Attendence already filled up in other class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                //}
                            }
                            else
                            {
                                rbtn.Checked = true;
                                ISAttendance objAttendence = new ISAttendance();
                                ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                if (objAtte != null)
                                {
                                    objAtte.Status = "Absent";
                                    objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                    objAtte.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();
                                    ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                    if (objs != null)
                                    {
                                        objs.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();
                                    }
                                    AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                }
                                else
                                {
                                    objAttendence.Date = dt2;
                                    objAttendence.StudentID = ID;
                                    objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                    objAttendence.Status = "Absent";
                                    string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                    objAttendence.Time = DateTime.Parse(DTS);
                                    objAttendence.Active = true;
                                    objAttendence.Deleted = true;
                                    objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                    objAttendence.CreatedDateTime = DateTime.Now;
                                    DB.ISAttendances.Add(objAttendence);
                                    DB.SaveChanges();

                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = DateTime.Now;
                                        pickup.PickDate = DateTime.Now;
                                        pickup.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = ID;
                                        obj.ClassID = student.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                }
                            }

                            if (pickObjectStandardSchool != null)
                            {
                                ISPickup iSPickup = DB.ISPickups.FirstOrDefault(r => r.ID == pickObjectStandardSchool.ID);
                                iSPickup.AfterSchoolFlag = null;
                                DB.SaveChanges();
                            }
                        }
                        else
                        {

                            ISAttendance objAttendence = new ISAttendance();
                            ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                            if (objAtte != null)
                            {
                                if (chkPresent.Checked == true)
                                {
                                    AlertMessageManagement.ServerMessage(Page, "Attendance cannot be changed from Present to Absent after Complete Attendance is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                    if (!objs.PickStatus.Contains("After-School"))
                                    {
                                        objs.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();

                                        objAtte.Status = "Absent";
                                        objAtte.ModifyBy = Authentication.LogginTeacher.ID;
                                        objAtte.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();

                                        AlertMessageManagement.ServerMessage(Page, "Attendence Updated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                    }
                                    else
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "Student already sent to AfterSchool", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                }
                            }
                            else
                            {
                                rbtn.Checked = true;

                                objAttendence.Date = dt2;
                                objAttendence.StudentID = ID;
                                objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                objAttendence.Status = "Absent";
                                string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                objAttendence.Time = DateTime.Parse(DTS);
                                objAttendence.Active = true;
                                objAttendence.Deleted = true;
                                objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                objAttendence.CreatedDateTime = DateTime.Now;
                                DB.ISAttendances.Add(objAttendence);
                                DB.SaveChanges();

                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                if (pickup != null)
                                {
                                    pickup.PickTime = DateTime.Now;
                                    pickup.PickDate = DateTime.Now;
                                    pickup.PickStatus = "Mark as Absent";
                                    DB.SaveChanges();
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = ID;
                                    obj.ClassID = student.ClassID;
                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Mark as Absent";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                AlertMessageManagement.ServerMessage(Page, "Attendence FillUp Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                            }
                        }
                    }
                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Student Pickup Status already Changed in Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
            BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
        }
        protected void chkPresent_CheckedChanged(object sender, EventArgs e)
        {
            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
            {
                if (chkPresent.Checked == true)
                {
                    int ID = Authentication.LogginTeacher.ID;
                    int Class = Convert.ToInt32(drpClass.SelectedValue);
                    DateTime dt = DateTime.Now;

                    ISCompleteAttendanceRun completeattendance = DB.ISCompleteAttendanceRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                    if (completeattendance == null)
                    {
                        ISCompleteAttendanceRun objcompleteattendance = new ISCompleteAttendanceRun();
                        objcompleteattendance.ClassID = Class;
                        objcompleteattendance.TeacherID = ID;
                        objcompleteattendance.Date = dt;
                        objcompleteattendance.Active = true;
                        objcompleteattendance.Deleted = false;
                        objcompleteattendance.CreatedBy = ID;
                        objcompleteattendance.CreatedDateTime = DateTime.Now;
                        DB.ISCompleteAttendanceRuns.Add(objcompleteattendance);
                        DB.SaveChanges();

                        List<MISStudent> obj = new List<MISStudent>();
                        ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                        if (_class != null)
                        {
                            List<MISStudent> objList = new List<MISStudent>();
                            //if (_class.Name.Contains("Outside Class"))
                            //{
                            //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                            //           select new MISStudent
                            //           {
                            //               ID = item.ID,
                            //               StudentName = item.StudentName,
                            //               Photo = item.Photo,
                            //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                            //               ClassID = item.ClassID
                            //           }).ToList();
                            //}
                            //else
                            {
                                obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                       select new MISStudent
                                       {
                                           ID = item.ID,
                                           StudentName = item.StudentName,
                                           Photo = item.Photo,
                                           CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                           ClassID = item.ClassID
                                       }).ToList();
                            }
                        }

                        Session["ObjList"] = obj.ToList();
                        if (chkPresent.Checked == true)
                        {
                            foreach (var item in obj.ToList())
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    var itemToRemove = obj.Single(r => r.ID == item.ID);
                                    obj.Remove(itemToRemove);
                                }
                            }
                        }
                        if (obj.Count == 0)
                        {
                            var lists = (List<MISStudent>)Session["Attendencelist"];
                            foreach (var item in lists)
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    objs.AttendenceComplete = true;
                                    DB.SaveChanges();
                                    ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objs.StudentID && p.Deleted == true);
                                    SendConfirmStatusEmailManage(_Student);
                                }
                            }
                            chkPresent.Checked = true;
                            AlertMessageManagement.ServerMessage(Page, "All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        else
                        {
                            chkPresent.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Not All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
                    }
                    else
                    {
                        completeattendance.ClassID = Class;
                        completeattendance.TeacherID = ID;
                        completeattendance.Date = dt;
                        completeattendance.Active = true;
                        completeattendance.Deleted = false;
                        completeattendance.ModifyBy = ID;
                        completeattendance.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();

                        List<MISStudent> obj = new List<MISStudent>();
                        ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                        if (_class != null)
                        {
                            List<MISStudent> objList = new List<MISStudent>();
                            //if (_class.Name.Contains("Outside Class"))
                            //{
                            //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                            //           select new MISStudent
                            //           {
                            //               ID = item.ID,
                            //               StudentName = item.StudentName,
                            //               Photo = item.Photo,
                            //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                            //               ClassID = item.ClassID
                            //           }).ToList();
                            //}
                            //else
                            {
                                obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                       select new MISStudent
                                       {
                                           ID = item.ID,
                                           StudentName = item.StudentName,
                                           Photo = item.Photo,
                                           CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                           ClassID = item.ClassID
                                       }).ToList();
                            }
                        }

                        Session["ObjList"] = obj.ToList();
                        if (chkPresent.Checked == true)
                        {
                            foreach (var item in obj.ToList())
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    var itemToRemove = obj.Single(r => r.ID == item.ID);
                                    obj.Remove(itemToRemove);
                                }
                            }
                        }
                        if (obj.Count == 0)
                        {
                            var lists = (List<MISStudent>)Session["Attendencelist"];
                            foreach (var item in lists)
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    objs.AttendenceComplete = true;
                                    DB.SaveChanges();
                                    ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objs.StudentID && p.Deleted == true);
                                    SendConfirmStatusEmailManage(_Student);
                                }
                            }
                            chkPresent.Checked = true;
                            AlertMessageManagement.ServerMessage(Page, "All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        else
                        {
                            chkPresent.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Not All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
                    }
                }
                else
                {
                    chkPresent.Checked = true;
                    AlertMessageManagement.ServerMessage(Page, "Complete Attendance run already marked.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
            else
            {
                if (chkPresent.Checked == true)
                {
                    int ID = Authentication.LogginTeacher.ID;
                    int Class = Convert.ToInt32(drpClass.SelectedValue);
                    DateTime dt = DateTime.Now;

                    ISCompleteAttendanceRun completeattendance = DB.ISCompleteAttendanceRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                    if (completeattendance == null)
                    {
                        ISCompleteAttendanceRun objcompleteattendance = new ISCompleteAttendanceRun();
                        objcompleteattendance.ClassID = Class;
                        objcompleteattendance.TeacherID = ID;
                        objcompleteattendance.Date = dt;
                        objcompleteattendance.Active = true;
                        objcompleteattendance.Deleted = false;
                        objcompleteattendance.CreatedBy = ID;
                        objcompleteattendance.CreatedDateTime = DateTime.Now;
                        DB.ISCompleteAttendanceRuns.Add(objcompleteattendance);
                        DB.SaveChanges();

                        List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                select new MISStudent
                                                {
                                                    ID = item.ID,
                                                    StudentName = item.StudentName,
                                                    Photo = item.Photo,
                                                    CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                    ClassID = item.ClassID
                                                }).ToList();
                        Session["ObjList"] = obj.ToList();
                        if (chkPresent.Checked == true)
                        {
                            foreach (var item in obj.ToList())
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    var itemToRemove = obj.Single(r => r.ID == item.ID);
                                    obj.Remove(itemToRemove);
                                }
                            }
                        }
                        if (obj.Count == 0)
                        {
                            var lists = (List<MISStudent>)Session["Attendencelist"];
                            foreach (var item in lists)
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    objs.AttendenceComplete = true;
                                    DB.SaveChanges();
                                    ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objs.StudentID && p.Deleted == true);
                                    SendConfirmStatusEmailManage(_Student);
                                }
                            }
                            chkPresent.Checked = true;
                            AlertMessageManagement.ServerMessage(Page, "All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        else
                        {
                            chkPresent.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Not All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                    else
                    {
                        completeattendance.ClassID = Class;
                        completeattendance.TeacherID = ID;
                        completeattendance.Date = dt;
                        completeattendance.Active = true;
                        completeattendance.Deleted = false;
                        completeattendance.ModifyBy = ID;
                        completeattendance.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();

                        List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                select new MISStudent
                                                {
                                                    ID = item.ID,
                                                    StudentName = item.StudentName,
                                                    Photo = item.Photo,
                                                    CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                    ClassID = item.ClassID
                                                }).ToList();
                        Session["ObjList"] = obj.ToList();
                        if (chkPresent.Checked == true)
                        {
                            foreach (var item in obj.ToList())
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    var itemToRemove = obj.Single(r => r.ID == item.ID);
                                    obj.Remove(itemToRemove);
                                }
                            }
                        }
                        if (obj.Count == 0)
                        {
                            var lists = (List<MISStudent>)Session["Attendencelist"];
                            foreach (var item in lists)
                            {
                                ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (objs != null)
                                {
                                    objs.AttendenceComplete = true;
                                    DB.SaveChanges();
                                    ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objs.StudentID && p.Deleted == true);
                                    SendConfirmStatusEmailManage(_Student);
                                }
                            }
                            chkPresent.Checked = true;
                            AlertMessageManagement.ServerMessage(Page, "All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        else
                        {
                            chkPresent.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Not All Students have been marked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                    BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
                }
                else
                {
                    chkPresent.Checked = true;
                    AlertMessageManagement.ServerMessage(Page, "Complete Attendance run already marked.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
        }
        protected void email_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = CreateSpreadsheetWorkbooks(Server.MapPath("../Upload") + "/AttendenceReport" + Session["Generator3"].ToString() + ".xlsx");
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

                ///Adding Sheet here
                Row Hederrow;
                var lists = (List<MISStudent>)Session["Attendencelist"];
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
                    CreateCell(row, item.AttStatus != "" ? item.AttStatus : "Not Marked");
                    CreateCell(row, item.AttDate != "" ? item.AttDate : "");
                }

                spreadsheetDocument.Close();
                FileInfo fileinfo = new FileInfo(filepath);
                returnFilePath = filepath;

                //EmailManagement ObjEmailManagement = new EmailManagement();
                //string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: AttendenceReport<br><br>Description &nbsp;: Here we Send you a Mail for AttendeneList So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", Authentication.LogginTeacher.Name);
                //string FileNames = Server.MapPath("../Upload/AttendenceReport" + Session["Generator3"].ToString() + ".xlsx");
                //ObjEmailManagement.SendEmails(Authentication.LogginTeacher.Email, "AttendenceReport", Message, FileNames);
                //AlertMessageManagement.ServerMessage(Page, "Mail Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

            return returnFilePath;
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            drpStudent.SelectedValue = "0";
            BindList(drpClass.SelectedValue, "");
        }
        public void SendStatusEmailManage(ISStudent _Student, string Status)
        {
            try
            {

                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);

                string AdminBody = string.Empty;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                string tblAdminBody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be informed of the Latest Attendance Status of Your Child. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Attendance Status : <b>" + Status + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Message Sent by : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any Enquiries, please contact or message " + _School.Name + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be informed of the Latest Attendance Status of Your Child. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Attendance Status : <b>" + Status + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Message Sent by : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any Enquiries, please contact or message " + _School.Name + @"
                                   </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Attendance Status Alert Notification", AdminBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Attendance Status Alert Notification", SuperwisorBody);
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendConfirmStatusEmailManage(ISStudent _Student)
        {
            try
            {
                if ((_Student.EmailAfterConfirmAttendence == null && _Student.ISSchool.isAttendanceModule == true) || (_Student.EmailAfterConfirmAttendence.HasValue && _Student.EmailAfterConfirmAttendence.Value == true))
                {
                    ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);

                    string AdminBody = string.Empty;
                    string SuperwisorBody = string.Empty;
                    string tblSupbody = string.Empty;
                    string tblAdminBody = string.Empty;
                    tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be informed of the Attendance Status of Your Child Today. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Message Sent by : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any Enquiries, please contact or message " + _School.Name + @"
                                   </td>
                                </tr></table>";
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        SuperwisorBody += reader.ReadToEnd();
                    }
                    SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                    tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be informed of the Attendance Status of Your Child Today. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Message Sent by : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any Enquiries, please contact or message " + _School.Name + @"
                                   </td>
                                </tr></table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        AdminBody += reader.ReadToEnd();
                    }
                    AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                    _EmailManagement.SendEmail(_Student.ParantEmail1, "Confirm Attendance Alert Notification", AdminBody);
                    if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                    {
                        _EmailManagement.SendEmail(_Student.ParantEmail2, "Confirm Attendance Alert Notification", SuperwisorBody);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void drpStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList(drpClass.SelectedValue, drpStudent.SelectedItem.Text);
        }
    }
}