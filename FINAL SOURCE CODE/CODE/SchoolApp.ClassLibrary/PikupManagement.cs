using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.ClassLibrary
{
    public class PickupManagement
    {
        public List<MViewStudentPickUp> DailyPickUpStatus(string date, int StudentID, int StudID, string PickerID, string Status, string OrderBy, string SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            if (date != null && date != "")
            {
                string dates = date;
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
            int ID = StudID;
            ISStudent objSt = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            List<MViewStudentPickUp> objList2 = new List<MViewStudentPickUp>();
            if (objSt != null)
            {
                List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == objSt.SchoolID && p.Active == true).ToList();
                objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true && (p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")))/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                           select new MViewStudentPickUp
                           {
                               ID = item.ID == null ? 0 : item.ID,
                               StudentID = item.StudID,
                               StudentName = item.StudentName,
                               StudentPic = item.Photo,
                               PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                               //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                               Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                               Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                               PickerID = item.PickerID == null ? 0 : item.PickerID,
                               PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                               Status = item.PickStatus == null ? OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                               ClassID = item.ClassID,
                               ParantEmail1 = item.ParantEmail1,
                               ParantEmail2 = item.ParantEmail2,
                               SchoolName = item.SchoolID != null ? DB.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "",
                               SchoolType = item.SchoolID != null ? DB.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).TypeID.Value : 2,
                               StartDates = item.StartDate != (DateTime?)null ? item.StartDate.Value.ToString("dd/MM/yyyy") : "",
                               AfterSchoolName = item.StartDate != (DateTime?)null ? DB.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "",
                           }).ToList();
                objList = objList.Where(p => String.IsNullOrEmpty(p.StartDates) || p.StartDates == DateTime.Now.ToString("dd/MM/yyyy")).ToList();

                if (StudID != 0)
                {
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                    objList = objList.Where(p => p.ParantEmail1 == Authentication.LoginParentEmail || p.ParantEmail2 == Authentication.LoginParentEmail).GroupBy(r => r.StudentID).Select(r => r.First()).ToList();
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (PickerID != "")
            {
                objList = objList.Where(p => p.PickerName == PickerID).ToList();
            }
            if (Status != "")
            {
                objList = objList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();
                //objList = objList.Where(p => p.PickStatus == Status).ToList();
            }
            objList2 = objList.ToList();
            foreach (var items in objList)
            {
                //if (!String.IsNullOrEmpty(items.StartDates))
                //{
                //    ISAttendance _Attendence = DB.ISAttendances.SingleOrDefault(p => p.StudentID == items.StudentID && DbFunctions.TruncateTime(p.Date.Value) == DbFunctions.TruncateTime(DateTime.Now) && p.Active == true && p.Status.Contains("Present"));
                //    if(_Attendence == null)
                //    {
                //        objList2.Remove(items);
                //    }
                //}
                if (items.SchoolType == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    ISAttendance _Attendence = DB.ISAttendances.SingleOrDefault(p => p.StudentID == items.StudentID && DbFunctions.TruncateTime(p.Date.Value) == DbFunctions.TruncateTime(DateTime.Now) && p.Active == true && p.Status.Contains("Present"));
                    if (_Attendence == null)
                    {
                        objList2.Remove(items);
                    }
                    //else
                    //{
                    //    items.PickStatus = items.Status = "UnPicked";
                    //}
                }
            }
            objList = objList2.ToList();
            if (OrderBy == "ASC")
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.PickDate).ToList();
                }
            }
            else
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.PickDate).ToList();
                }
            }
            return objList;
        }
        public List<MViewStudentPickUp> PickupList(int TeacherID, int ClassID, string CreatedDate)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            if (CreatedDate != "")
            {
                string dates = CreatedDate;
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
            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
            List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == Obj.SchoolID && p.Active == true).ToList();

            bool isTodayIsHoliday = objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0;
            string holidayName = OperationManagement.GetTodayHolidayStatus(objHoliday);

            List<MViewStudentPickUp> objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)
                                                    //join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                                                select new MViewStudentPickUp
                                                {
                                                    ID = item.ID == null ? 0 : item.ID,
                                                    StudentID = item.StudID,
                                                    StudentName = item.StudentName,
                                                    StudentPic = item.Photo,
                                                    TeacherID = item.TeacherID,
                                                    PickStatus = String.IsNullOrEmpty(item.PickStatus) ? 
                                                    isTodayIsHoliday ? holidayName : OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                                                    Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm"),
                                                    Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                                    PickerID = item.PickerID == null ? 0 : item.PickerID,
                                                    PickDate = item.PickDate == null ? null : item.PickDate,
                                                    PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                                    PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                                                    ClassID = item.ClassID,
                                                    ParentMessage = DB.ISPickUpMessages.Where(p => p.SendID == item.StudID && p.ReceiverID == TeacherID && p.Viewed == false && p.Deleted == true && p.Active == true).ToList().Count,
                                                    OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                                                    AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                                                    CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                                                    ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                                                    SchoolID = item.SchoolID,
                                                }).ToList();
            if (ClassID != 0)
            {
                objList = objList.Where(r => r.SchoolID == Obj.SchoolID).ToList();

                ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "Office" || p.OfficeFlag).ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Club" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
                            objList.Where(p => (p.StudentID == item.StudentID && p.PickStatus == "Picked" && p.OfficeFlag)).Count() == 1)
                        {
                            Lists.Add(item);
                        }
                    }
                    objList = Lists;
                }
                else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Club)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "Club" || p.ClubFlag).ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Office" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
                            objList.Where(p => (p.StudentID == item.StudentID && p.PickStatus == "Picked" && p.ClubFlag)).Count() == 1)
                        {
                            Lists.Add(item);
                        }
                    }
                    objList = Lists;
                }
                else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex" || p.AfterSchoolFlag).ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Office" || p.PickStatus == "Club")).Count() <= 0 ||
                            objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked" && p.AfterSchoolFlag).Count() == 1)
                        {
                            Lists.Add(item);
                        }
                    }
                    objList = Lists;
                }
                else
                {
                    objList = objList.Where(p => p.ClassID == ClassID).ToList(); // && p.PickStatus != "Office"
                }
            }
            return objList;
        }
        public List<MViewStudentPickUp> PickupsList(int ClassID, string CreatedDate)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            if (CreatedDate != "")
            {
                string dates = CreatedDate;
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
            ISClass Obj = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
            List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == Obj.SchoolID && p.Active == true).ToList();
            List<MViewStudentPickUp> objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)
                                                    //join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                                                select new MViewStudentPickUp
                                                {
                                                    ID = item.ID == null ? 0 : item.ID,
                                                    StudentID = item.StudID,
                                                    StudentName = item.StudentName,
                                                    StudentPic = item.Photo,
                                                    TeacherID = item.TeacherID,
                                                    PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "UnPicked" : item.PickStatus,
                                                    Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm"),
                                                    Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                                    PickerID = item.PickerID == null ? 0 : item.PickerID,
                                                    PickDate = item.PickDate == null ? null : item.PickDate,
                                                    PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                                    PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                                                    ClassID = item.ClassID,
                                                    SchoolID = item.SchoolID,
                                                    ParentMessage = DB.ISPickUpMessages.Where(p => p.SendID == item.StudID && p.Viewed == false && p.Deleted == true && p.Active == true).ToList().Count,
                                                }).ToList();

            if (ClassID != 0)
            {
                objList = objList.Where(r => r.SchoolID == Obj.SchoolID).ToList();

                ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "Office").ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                        {
                            Lists.Add(item);
                        }
                    }
                    objList = Lists;
                }
                else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Club)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "Club").ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                        {
                            Lists.Add(item);
                        }
                    }
                    objList = Lists;
                }
                else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex").ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                        {
                            Lists.Add(item);
                        }
                    }
                    objList = Lists;
                }
                else
                {
                    //if (ObjClass.Name.Contains("Outside Class"))
                    //{
                    //    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    //    objList = objList.Where(p => p.PickDate != null).ToList();
                    //    List<MViewStudentPickUp> list = objList.Where(p => p.SchoolID == ObjClass.SchoolID && p.ClassID == ObjClass.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy")).ToList();
                    //    foreach (var item in list)
                    //    {
                    //        if (objList.Where(p => p.StudentID == item.StudentID).Count() > 0)
                    //        {
                    //            Lists.Add(item);
                    //        }
                    //    }
                    //    objList = Lists;
                    //}
                    //else
                    {
                        objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Office" && p.PickStatus != "Club" && p.PickStatus != "After-School" && p.PickStatus != "After-School-Ex").ToList();
                        foreach (var item in objList.ToList())
                        {
                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.StudentID && p.Status.Contains("Present") && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                            if (objs == null)
                            {
                                foreach (var items in objList.Where(r => r.StudentID == item.StudentID).ToList())
                                {
                                    //var itemToRemove = objList.Single(r => r.ID == item.ID);
                                    objList.Remove(items);
                                }
                            }
                        }
                    }
                }
            }
            return objList;
        }
        public List<MViewStudentPickUp> StudentPickUpReport(int StudentID, string DateFrom, string DateTo, int PickerID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
            List<MViewStudentPickUp> objList = (from item in DB.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2)//ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()
                                                select new MViewStudentPickUp
                                                {
                                                    ID = item.ID == null ? 0 : item.ID,
                                                    StudentID = item.StudID,
                                                    StudentName = item.StudentName,
                                                    StudentPic = item.Photo,
                                                    TeacherID = item.TeacherID,
                                                    PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                    Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                                    Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                                    PickerID = item.PickerID == null ? 0 : item.PickerID,
                                                    PickDate = item.PickDate == null ? null : item.PickDate,
                                                    PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                                    Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                    ClassID = item.ClassID,
                                                }).OrderByDescending(p => p.StudentID).ToList();
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (DateFrom != "")
            {
                string dates = DateFrom;
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
                DateTime dt = Convert.ToDateTime(Format);
                objList = objList.Where(p => p.PickDate.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                string dates = DateTo;
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
                DateTime dt2 = Convert.ToDateTime(Format);
                objList = objList.Where(p => p.PickDate.Value.Date <= dt2.Date).ToList();
            }
            if (PickerID != 0)
            {
                objList = objList.Where(p => p.PickerID == PickerID).ToList();
            }
            if (Status != "")
            {
                if (Status.Contains("School Closed"))
                {
                    objList = objList.Where(p => p.PickStatus.Contains(Status)).ToList();
                }
                else
                {
                    objList = objList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "Date")
                {
                    objList = objList.OrderBy(p => p.PickDate).ToList();
                }
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
            }
            else
            {
                if (SortBy == "Date")
                {
                    objList = objList.OrderByDescending(p => p.PickDate).ToList();
                }
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
            }
            return objList;
        }
        public List<MViewStudentPickUp> StudentPickUpReports(int StudentID, string DateFrom, string DateTo, string PickerID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            var objPickUp = DB.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).ToList();

            DateTime dt = DateTime.Now;
            List<DateTime> list = new List<DateTime>();
            if (DateFrom != "" && DateTo != "")
            {
                string dates = DateFrom;
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

                string dates2 = DateTo;
                string Format2 = "";
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    Format2 = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format2 = dates2;
                }
                DateTime dt3 = Convert.ToDateTime(Format2);
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), StudentID);
            }
            else
            {
                list = AllDatesInMonth(dt.Year, dt.Month, StudentID);
            }
            foreach (DateTime date in list)
            {
                if (date.Date >= objStudent.CreatedDateTime.Value.Date)
                {
                    List<ISHoliday> List = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    MViewStudentPickUp newobj = new MViewStudentPickUp();
                    if (objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate != (DateTime?)null).Count() > 0)
                    {
                        if (objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Count() > 0)
                        {
                            foreach (var item in objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).ToList())
                            {
                                newobj.ID = item.ID;
                                newobj.StudentID = item.StudID;
                                newobj.StudentName = item.StudentName;
                                newobj.StudentPic = item.Photo;
                                newobj.TeacherID = item.TeacherID;
                                newobj.PickDate = item.PickDate;
                                if (DB.ISAttendances.Where(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate)).Count() <= 0)
                                {

                                    if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date).Count() > 0)
                                    {
                                        string Holiday = String.Empty;
                                        foreach (var i in List)
                                        {
                                            if (item.PickDate.Value >= i.DateFrom.Value && item.PickDate.Value <= i.DateTo.Value ||
                                                item.PickDate.Value.ToString("dd/MM/yyyy") == i.DateFrom.Value.ToString("dd/MM/yyyy") ||
                                                item.PickDate.Value.ToString("dd/MM/yyyy") == i.DateTo.Value.ToString("dd/MM/yyyy"))
                                            {
                                                Holiday += i.Name + ", ";

                                            }
                                        }
                                        Holiday = Holiday.Remove(Holiday.Length - 2);
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = Holiday + " (School Closed)";
                                        newobj.Pick_Time = "";
                                        newobj.PickerName = "";
                                    }
                                    else if (item.PickDate.Value.DayOfWeek == DayOfWeek.Saturday || item.PickDate.Value.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                        newobj.Pick_Time = "";
                                        newobj.PickerName = "";

                                        if (item.PickStatus == "Not Marked")
                                            newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy"));
                                        else
                                        {
                                            newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                            newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                            newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                        }
                                    }

                                    else
                                    {
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                        newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                        newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                        newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                    }
                                }
                                else
                                {
                                    newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                    newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                    newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                    newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                }

                                newobj.Status = String.IsNullOrEmpty(item.PickStatus) ? "Not Marked" : item.PickStatus;
                                newobj.PickStatus = newobj.Status;
                                newobj.ClassID = item.ClassID;
                                                                
                                newobj.SchoolName = !String.IsNullOrEmpty(item.PickStatus) ? item.PickStatus.Contains("After-School-Ex") ? DB.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "" : "";

                                if (!String.IsNullOrEmpty(item.PickStatus) && item.PickStatus.Contains("After-School-Ex"))
                                {
                                    ClassManagement objClassManagement = new ClassManagement();

                                    ISClass Cid = DB.ISClasses.FirstOrDefault(r => r.TypeID == (int)CLASSTYPE.AfterSchool && r.SchoolID == item.SchoolID);
                                    MISClass Obj = objClassManagement.GetClass(Cid.ID);
                                    newobj.SchoolName = Obj.ExternalOrganisation;
                                }
                            }
                        }
                        else
                        {
                            newobj.PickDate = date;
                            if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                            {
                                if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                                {
                                    string Holiday = String.Empty;
                                    foreach (var i in List)
                                    {
                                        if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                        {
                                            Holiday += i.Name + ", ";
                                        }
                                    }
                                    Holiday = Holiday.Remove(Holiday.Length - 2);
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = Holiday + " (School Closed)";
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }

                                else
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = "Not Marked";
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                            }
                            else
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = "Not Marked";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            newobj.StudentID = objStudent.ID;
                            newobj.StudentName = objStudent.StudentName;
                            newobj.StudentPic = objStudent.Photo;
                            newobj.Status = "Not Marked";
                            newobj.ClassID = objStudent.ClassID;
                        }
                    }
                    else
                    {
                        newobj.PickDate = date;
                        if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                        {

                            if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                            {
                                string Holiday = String.Empty;
                                foreach (var i in List)
                                {
                                    if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                    {
                                        Holiday += i.Name + ", ";
                                    }
                                }
                                Holiday = Holiday.Remove(Holiday.Length - 2);
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = Holiday + " (School Closed)";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }

                            else
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = "Not Marked";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                        }
                        else
                        {
                            newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                            newobj.PickStatus = "Not Marked";
                            newobj.Pick_Time = "";
                            newobj.PickerName = "";
                        }

                        newobj.StudentID = objStudent.ID;
                        newobj.StudentName = objStudent.StudentName;
                        newobj.StudentPic = objStudent.Photo;
                        newobj.Status = "Not Marked";
                        newobj.ClassID = objStudent.ClassID;
                    }
                    objList.Add(newobj);
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (PickerID != "")
            {
                objList = objList.Where(p => p.PickerName == PickerID).ToList();
            }
            if (Status != "")
            {
                if (Status == "School Closed")
                {
                    objList = objList.Where(p => p.PickStatus.Contains(Status)).ToList();
                }
                else
                {
                    objList = objList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.PickDate).ToList();
                }
            }
            else
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.PickDate).ToList();
                }
            }
            return objList;
        }


        public List<MViewStudentPickUp> StudentPickUpReportsAfterSchoolOnly(int StudentID, string DateFrom, string DateTo, string PickerID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            var objPickUp = DB.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).ToList();

            DateTime dt = DateTime.Now;
            List<DateTime> list = new List<DateTime>();
            if (DateFrom != "" && DateTo != "")
            {
                string dates = DateFrom;
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

                string dates2 = DateTo;
                string Format2 = "";
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    Format2 = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format2 = dates2;
                }
                DateTime dt3 = Convert.ToDateTime(Format2);
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), StudentID);
            }
            else
            {
                list = AllDatesInMonth(dt.Year, dt.Month, StudentID);
            }
            foreach (DateTime date in list)
            {
                if (date.Date >= objStudent.CreatedDateTime.Value.Date)
                {
                    List<ISHoliday> List = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    MViewStudentPickUp newobj = new MViewStudentPickUp();
                    if (objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate != (DateTime?)null).Count() > 0)
                    {
                        if (objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Count() > 0)
                        {
                            foreach (var item in objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).ToList())
                            {
                                newobj.ID = item.ID;
                                newobj.StudentID = item.StudID;
                                newobj.StudentName = item.StudentName;
                                newobj.StudentPic = item.Photo;
                                newobj.TeacherID = item.TeacherID;
                                newobj.PickDate = item.PickDate;
                                if (DB.ISAttendances.Where(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate)).Count() <= 0)
                                {
                                    if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date).Count() > 0)
                                    {
                                        string Holiday = String.Empty;
                                        foreach (var i in List)
                                        {
                                            if (item.PickDate.Value >= i.DateFrom.Value && item.PickDate.Value <= i.DateTo.Value ||
                                                item.PickDate.Value.ToString("dd/MM/yyyy") == i.DateFrom.Value.ToString("dd/MM/yyyy") ||
                                                item.PickDate.Value.ToString("dd/MM/yyyy") == i.DateTo.Value.ToString("dd/MM/yyyy"))
                                            {
                                                Holiday += i.Name + ", ";

                                            }
                                        }
                                        Holiday = Holiday.Remove(Holiday.Length - 2);
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = Holiday + " (School Closed)";
                                        newobj.Pick_Time = "";
                                        newobj.PickerName = "";
                                    }
                                    else if (item.PickDate.Value.DayOfWeek == DayOfWeek.Saturday || item.PickDate.Value.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                        newobj.Pick_Time = "";
                                        newobj.PickerName = "";

                                        if (item.PickStatus == "Not Marked")
                                            newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy"));
                                        else
                                        {
                                            newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                            newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                            newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                        }
                                    }

                                    else
                                    {
                                        ISAttendance attendance = DB.ISAttendances.FirstOrDefault(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate));
                                        

                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                        newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                        newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                        newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                    }
                                }
                                else
                                {
                                    newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                    newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                    newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                    newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                }

                                newobj.Status = String.IsNullOrEmpty(item.PickStatus) ? "Not Marked" : item.PickStatus;
                                newobj.PickStatus = newobj.Status;
                                newobj.ClassID = item.ClassID;
                                newobj.SchoolName = !String.IsNullOrEmpty(item.PickStatus) ? item.PickStatus.Contains("After-School-Ex") ? DB.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "" : "";
                            }
                        }
                        else
                        {
                            newobj.PickDate = date;
                            if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                            {
                                if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                                {
                                    string Holiday = String.Empty;
                                    foreach (var i in List)
                                    {
                                        if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                        {
                                            Holiday += i.Name + ", ";
                                        }
                                    }
                                    Holiday = Holiday.Remove(Holiday.Length - 2);
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = Holiday + " (School Closed)";
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }

                                else
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = "Not Marked";
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                            }
                            else
                            {
                                 //DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date));

                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = "Not Marked";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            newobj.StudentID = objStudent.ID;
                            newobj.StudentName = objStudent.StudentName;
                            newobj.StudentPic = objStudent.Photo;
                            newobj.Status = newobj.PickStatus;
                            newobj.ClassID = objStudent.ClassID;
                        }
                    }
                    else
                    {
                        newobj.PickDate = date;
                        if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                        {

                            if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                            {
                                string Holiday = String.Empty;
                                foreach (var i in List)
                                {
                                    if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                    {
                                        Holiday += i.Name + ", ";
                                    }
                                }
                                Holiday = Holiday.Remove(Holiday.Length - 2);
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = Holiday + " (School Closed)";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }

                            else
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = "Not Marked";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                        }
                        else
                        {
                            newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                            newobj.PickStatus = "Not Marked";
                            newobj.Pick_Time = "";
                            newobj.PickerName = "";
                        }

                        newobj.StudentID = objStudent.ID;
                        newobj.StudentName = objStudent.StudentName;
                        newobj.StudentPic = objStudent.Photo;
                        newobj.Status = "Not Marked";
                        newobj.ClassID = objStudent.ClassID;
                    }
                    objList.Add(newobj);
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (PickerID != "")
            {
                objList = objList.Where(p => p.PickerName == PickerID).ToList();
            }
            if (Status != "")
            {
                if (Status == "School Closed")
                {
                    objList = objList.Where(p => p.PickStatus.Contains(Status)).ToList();
                }
                else
                {
                    objList = objList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.PickDate).ToList();
                }
            }
            else
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.PickDate).ToList();
                }
            }
            return objList;
        }



        public List<MViewStudentPickUp> StudentPickUpReportsAfterSchool(int StudentID, string DateFrom, string DateTo, string PickerID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MViewStudentPickUp> ObjList = new List<MViewStudentPickUp>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            ObjList = (from item in DB.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).Where(p => p.StudID == objStudent.ID)
                       select new MViewStudentPickUp
                       {
                           StudentID = item.StudID,
                           StudentName = item.StudentName,
                           StudentPic = item.Photo,
                           TeacherID = item.TeacherID == null ? 0 : item.TeacherID,
                           PickDate = item.PickDate,
                           Pick_Date = item.PickDate != (DateTime?)null ? item.PickDate.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy"),
                           PickDateStr = item.PickDate != (DateTime?)null ? item.PickDate.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToString("dd/MM/yyyy"),
                           Status = !String.IsNullOrEmpty(item.PickStatus) ? item.PickStatus : OperationManagement.GetDefaultPickupStatus(),
                           PickStatus = !String.IsNullOrEmpty(item.PickStatus) ? item.PickStatus : OperationManagement.GetDefaultPickupStatus(),
                           Pick_Time = item.PickTime != (DateTime?)null ? item.PickTime.Value.ToString("HH:mm tt") : "",
                           PickerID = item.PickerID,
                           PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                           ClassID = item.ClassID,
                           SchoolName = DB.ISSchools.SingleOrDefault(p => p.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && p.ID == item.SchoolID).Name,
                       }).ToList();
            if (DateFrom != "")
            {
                string dates = DateFrom;
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
                DateTime dt = Convert.ToDateTime(Format);
                ObjList = ObjList.Where(p => p.PickDate.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                string dates = DateFrom;
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
                DateTime dt = Convert.ToDateTime(Format);
                ObjList = ObjList.Where(p => p.PickDate.Value.Date <= dt.Date).ToList();
            }
            if (StudentID != 0)
            {
                ObjList = ObjList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (PickerID != "")
            {
                ObjList = ObjList.Where(p => p.PickerName == PickerID).ToList();
            }
            if (Status != "")
            {
                if (Status == "School Closed")
                {
                    ObjList = ObjList.Where(p => p.PickStatus.Contains(Status)).ToList();
                }
                else
                {
                    ObjList = ObjList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "ChildName")
                {
                    ObjList = ObjList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    ObjList = ObjList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    ObjList = ObjList.OrderBy(p => p.PickerName).ToList();
                }
                else
                {
                    ObjList = ObjList.OrderBy(p => p.PickDate).ToList();
                }
            }
            else
            {
                if (SortBy == "ChildName")
                {
                    ObjList = ObjList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    ObjList = ObjList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    ObjList = ObjList.OrderByDescending(p => p.PickerName).ToList();
                }
                else
                {
                    ObjList = ObjList.OrderByDescending(p => p.PickDate).ToList();
                }
            }
            return ObjList;
        }
        public List<MViewStudentPickUp> StudentPickUpReportsWithSchool(int StudentID, string DateFrom, string DateTo, string PickerID, string Status, string SortBy, string OrderBy, int StudID, int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            List<getStudentReport_Result> objPickUp = new List<getStudentReport_Result>();
            if (SchoolID > 0)
            {
                objPickUp = DB.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).Where(p => p.SchoolID == SchoolID).ToList();
            }
            else
            {
                objPickUp = DB.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).ToList();
            }

            DateTime dt = DateTime.Now;
            List<DateTime> list = new List<DateTime>();
            if (DateFrom != "" && DateTo != "")
            {
                string dates = DateFrom;
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

                string dates2 = DateTo;
                string Format2 = "";
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    Format2 = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format2 = dates2;
                }
                DateTime dt3 = Convert.ToDateTime(Format2);
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), StudentID);
            }
            else
            {
                list = AllDatesInMonth(dt.Year, dt.Month, StudentID);
            }
            foreach (DateTime date in list)
            {
                if (date.Date >= objStudent.CreatedDateTime.Value.Date)
                {
                    List<ISHoliday> List = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    MViewStudentPickUp newobj = new MViewStudentPickUp();
                    if (objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate != null).Count() > 0)
                    {
                        if (objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Count() > 0)
                        {
                            foreach (var item in objPickUp.Where(p => p.StudID == objStudent.ID && p.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).ToList())
                            {
                                if (item.PickDate.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy"))
                                {
                                    newobj.ID = item.ID;
                                    newobj.StudentID = item.StudID;
                                    newobj.StudentName = item.StudentName;
                                    newobj.StudentPic = item.Photo;
                                    newobj.TeacherID = item.TeacherID;
                                    newobj.PickDate = item.PickDate;
                                    if (DB.ISAttendances.Where(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate)).Count() <= 0)
                                    {
                                        if (item.PickDate.Value.DayOfWeek == DayOfWeek.Saturday || item.PickDate.Value.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                            newobj.Pick_Time = "";
                                            newobj.PickerName = "";

                                            if (item.PickStatus == "Not Marked")
                                            {
                                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                                newobj.PickStatus = item.PickStatus == null ? OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy")) : item.PickStatus;
                                                newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                                newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                                newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                            }

                                        }
                                        else if (List.Where(x => x.DateFrom.Value <= item.PickDate.Value && x.DateTo.Value >= item.PickDate.Value).Count() > 0)
                                        {
                                            string Holiday = String.Empty;
                                            foreach (var i in List)
                                            {
                                                if (item.PickDate.Value >= i.DateFrom.Value && item.PickDate.Value <= i.DateTo.Value)
                                                {
                                                    Holiday += i.Name + ", ";

                                                }
                                            }
                                            Holiday = Holiday.Remove(Holiday.Length - 2);
                                            newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                            newobj.PickStatus = Holiday + " (School Closed)";
                                            newobj.Pick_Time = "";
                                            newobj.PickerName = "";
                                        }
                                        else
                                        {
                                            newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                            newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                            newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                            newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                            newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                        }
                                    }
                                    else
                                    {
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy")) : item.PickStatus;
                                        newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                        newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                        newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                    }
                                    newobj.Status = item.PickStatus == null ? OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy")) : item.PickStatus;
                                    newobj.ClassID = item.ClassID;
                                }
                                else
                                {
                                    newobj.PickDate = date;
                                    if (DB.ISAttendances.Where(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate)).Count() <= 0)
                                    {
                                        if (item.PickDate.Value.DayOfWeek == DayOfWeek.Saturday || item.PickDate.Value.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                            newobj.Pick_Time = "";
                                            newobj.PickerName = "";

                                            if (item.PickStatus == "Not Marked")
                                            {
                                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                                newobj.PickStatus = item.PickStatus == null ? OperationManagement.GetDefaultPickupStatus(item.PickDate.Value.ToString("dd/MM/yyyy")) : item.PickStatus;
                                                newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                                newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                                newobj.PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                            }

                                        }
                                        else if (List.Where(x => x.DateFrom.Value <= item.PickDate.Value && x.DateTo.Value >= item.PickDate.Value && x.Active == true).Count() > 0)
                                        {
                                            string Holiday = String.Empty;
                                            foreach (var i in List)
                                            {
                                                if (item.PickDate.Value >= i.DateFrom.Value && item.PickDate.Value <= i.DateTo.Value)
                                                {
                                                    Holiday += i.Name + ", ";

                                                }
                                            }
                                            Holiday = Holiday.Remove(Holiday.Length - 2);
                                            newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                            newobj.PickStatus = Holiday + " (School Closed)";
                                            newobj.Pick_Time = "";
                                            newobj.PickerName = "";
                                        }
                                        else
                                        {
                                            newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                            newobj.PickStatus = "Not Marked";
                                            newobj.Pick_Time = "";
                                            newobj.PickerName = "";
                                        }
                                    }
                                    else
                                    {
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = "Not Marked";
                                        newobj.Pick_Time = "";
                                        newobj.PickerName = "";
                                    }

                                    newobj.StudentID = objStudent.ID;
                                    newobj.StudentName = objStudent.StudentName;
                                    newobj.StudentPic = objStudent.Photo;
                                    newobj.Status = "Not Marked";
                                    newobj.ClassID = objStudent.ClassID;
                                }
                            }
                        }
                        else
                        {
                            newobj.PickDate = date;
                            if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                            {
                                if (date.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                                {
                                    string Holiday = String.Empty;
                                    foreach (var i in List)
                                    {
                                        if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                        {
                                            Holiday += i.Name + ", ";
                                        }
                                    }
                                    Holiday = Holiday.Remove(Holiday.Length - 2);
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = Holiday + " (School Closed)";
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                                else
                                {
                                    newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = "Not Marked";
                                    newobj.Pick_Time = "";
                                    newobj.PickerName = "";
                                }
                            }
                            else
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = "Not Marked";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            newobj.StudentID = objStudent.ID;
                            newobj.StudentName = objStudent.StudentName;
                            newobj.StudentPic = objStudent.Photo;
                            newobj.Status = "Not Marked";
                            newobj.ClassID = objStudent.ClassID;
                        }
                    }
                    else
                    {
                        newobj.PickDate = date;
                        if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                        {
                            if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                            {
                                string Holiday = String.Empty;
                                foreach (var i in List)
                                {
                                    if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                    {
                                        Holiday += i.Name + ", ";
                                    }
                                }
                                Holiday = Holiday.Remove(Holiday.Length - 2);
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = Holiday + " (School Closed)";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                            else
                            {
                                newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                                newobj.PickStatus = "Not Marked";
                                newobj.Pick_Time = "";
                                newobj.PickerName = "";
                            }
                        }
                        else
                        {
                            newobj.Pick_Date = date.ToString("dd/MM/yyyy");
                            newobj.PickStatus = "Not Marked";
                            newobj.Pick_Time = "";
                            newobj.PickerName = "";
                        }

                        newobj.StudentID = objStudent.ID;
                        newobj.StudentName = objStudent.StudentName;
                        newobj.StudentPic = objStudent.Photo;
                        newobj.Status = "Not Marked";
                        newobj.ClassID = objStudent.ClassID;
                    }
                    objList.Add(newobj);
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (PickerID != "")
            {
                objList = objList.Where(p => p.PickerName == PickerID).ToList();
            }
            if (Status != "")
            {
                if (Status == OperationManagement.DefaultPickupStatuslist[0])
                {
                    objList = objList.Where(p => p.PickStatus.Contains(OperationManagement.DefaultPickupStatuslist[0])).ToList();
                }

                else
                {
                    objList = objList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "Date")
                {
                    objList = objList.OrderBy(p => p.PickDate).ToList();
                }
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
            }
            else
            {
                if (SortBy == "Date")
                {
                    objList = objList.OrderByDescending(p => p.PickDate).ToList();
                }
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
            }
            return objList;
        }
        public List<MGetAttendenceData> StudentAttendenceReport(int StudentID, string DateFrom, string DateTo, string TeacherID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MGetAttendenceData> objList = new List<MGetAttendenceData>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);

            DateTime dt = DateTime.Now;
            List<DateTime> list = new List<DateTime>();
            if (DateFrom != "" && DateTo != "")
            {
                string dates = DateFrom;
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

                string dates2 = DateTo;
                string Format2 = "";
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    Format2 = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format2 = dates2;
                }
                DateTime dt3 = Convert.ToDateTime(Format2);
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), StudentID);
            }
            else
            {
                list = AllDatesInMonth(dt.Year, dt.Month, StudentID);
            }
            List<ISHoliday> List = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
            foreach (DateTime date in list)
            {
                if (date.Date >= objStudent.CreatedDateTime.Value.Date)
                {
                    var objPickUp = DB.getAttandanceData(date).ToList();

                    MGetAttendenceData newobj = new MGetAttendenceData();
                    if (objPickUp.Where(p => p.StudentsID == objStudent.ID && p.Date != null).Count() > 0)
                    {
                        if (objPickUp.Where(p => p.StudentsID == objStudent.ID && p.Date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Count() > 0)
                        {
                            foreach (var item in objPickUp.Where(p => p.StudentsID == objStudent.ID && p.Date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).ToList())
                            {
                                if (item.Date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy"))
                                {
                                    newobj.StudentID = item.StudentsID;
                                    newobj.StudentName = item.StudentName;
                                    newobj.StudentPic = item.Photo;
                                    newobj.TeacherID = item.TeacherID == null ? 0 : item.TeacherID;
                                    newobj.Date = item.Date;
                                    if (DB.ISAttendances.Where(p => p.StudentID == item.StudentsID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.Date)).Count() <= 0)
                                    {
                                        if (item.Date.Value.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString("dd/MM/yyyy"));
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (item.Date.Value.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString("dd/MM/yyyy"));
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (List.Where(x => x.DateFrom.Value <= item.Date.Value && x.DateTo.Value >= item.Date.Value).Count() > 0)
                                        {
                                            string Holiday = String.Empty;
                                            foreach (var i in List)
                                            {
                                                if (item.Date.Value >= i.DateFrom.Value && item.Date.Value <= i.DateTo.Value)
                                                {
                                                    Holiday += i.Name + ", ";
                                                }
                                            }
                                            Holiday = Holiday.Remove(Holiday.Length - 2);
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = Holiday + " (School Closed)";
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = item.Status == null ? "Not Marked" : item.Status;
                                            newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                            newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                        }
                                    }
                                    else
                                    {
                                        newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                        newobj.Status = item.Status == null ? "Not Marked" : item.Status;
                                        newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                        newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                    }
                                    newobj.ClassID = item.ClassID;
                                }
                                else
                                {
                                    newobj.Date = date;
                                    newobj.TeacherID = item.TeacherID == null ? 0 : item.TeacherID;
                                    if (DB.ISAttendances.Where(p => p.StudentID == item.StudentsID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.Date)).Count() <= 0)
                                    {
                                        if (item.Date.Value.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString("dd/MM/yyyy"));
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (item.Date.Value.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString("dd/MM/yyyy"));
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (List.Where(x => x.DateFrom.Value <= item.Date.Value && x.DateTo.Value >= item.Date.Value && x.Active == true).Count() > 0)
                                        {
                                            string Holiday = String.Empty;
                                            foreach (var i in List)
                                            {
                                                if (item.Date.Value >= i.DateFrom.Value && item.Date.Value <= i.DateTo.Value)
                                                {
                                                    Holiday += i.Name + ", ";
                                                }
                                            }
                                            Holiday = Holiday.Remove(Holiday.Length - 2);
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = Holiday + " (School Closed)";
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = item.Status == null ? "Not Marked" : item.Status;
                                            newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                            newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                        }
                                    }
                                    else
                                    {
                                        newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                        newobj.Status = item.Status == null ? "Not Marked" : item.Status;
                                        newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                        newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                    }

                                    newobj.StudentID = objStudent.ID;
                                    newobj.StudentName = objStudent.StudentName;
                                    newobj.StudentPic = objStudent.Photo;
                                    newobj.ClassID = objStudent.ClassID;
                                }
                            }
                        }
                        else
                        {
                            newobj.Date = date;
                            if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                            {
                                if (date.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                                else if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                                {
                                    string Holiday = String.Empty;
                                    foreach (var i in List)
                                    {
                                        if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                        {
                                            Holiday += i.Name + ", ";
                                        }
                                    }
                                    Holiday = Holiday.Remove(Holiday.Length - 2);
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = Holiday + " (School Closed)";
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                                else
                                {
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = "Not Marked";
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                            }
                            else
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = "Not Marked";
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            newobj.StudentID = objStudent.ID;
                            newobj.StudentName = objStudent.StudentName;
                            newobj.StudentPic = objStudent.Photo;
                            newobj.ClassID = objStudent.ClassID;
                        }
                    }
                    else
                    {
                        newobj.Date = date;
                        if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                        {
                            if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString("dd/MM/yyyy"));
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            else if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                            {
                                string Holiday = String.Empty;
                                foreach (var i in List)
                                {
                                    if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                    {
                                        Holiday += i.Name + ", ";
                                    }
                                }
                                Holiday = Holiday.Remove(Holiday.Length - 2);
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = Holiday + " (School Closed)";
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            else
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = "Not Marked";
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                        }
                        else
                        {
                            newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                            newobj.Status = "Not Marked";
                            newobj.MarkedTime = "";
                            newobj.TeacherName = "";
                        }

                        newobj.StudentID = objStudent.ID;
                        newobj.StudentName = objStudent.StudentName;
                        newobj.StudentPic = objStudent.Photo;
                        newobj.ClassID = objStudent.ClassID;
                    }
                    objList.Add(newobj);
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (TeacherID != "0")
            {
                int TID = Convert.ToInt32(TeacherID);
                objList = objList.Where(p => p.TeacherID == TID).ToList();
            }
            if (Status != "")
            {
                if (Status == "Close")
                {
                    objList = objList.Where(p => p.Status.Contains(Status)).ToList();
                }
                else
                {
                    objList = objList.Where(p => p.Status == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {

                if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.TeacherName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.Date).ToList();
                }
            }
            else
            {

                if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.TeacherName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.Date).ToList();
                }
            }
            return objList;
        }

        public List<MGetAttendenceData> StudentAttendenceReport1(int StudentID, string DateFrom, string DateTo, string TeacherID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MGetAttendenceData> objList = new List<MGetAttendenceData>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);

            DateTime dt = DateTime.Now;
            List<DateTime> list = new List<DateTime>();
            if (DateFrom != "" && DateTo != "")
            {
                string dates = DateFrom;
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

                string dates2 = DateTo;
                string Format2 = "";
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    Format2 = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format2 = dates2;
                }
                DateTime dt3 = Convert.ToDateTime(Format2);
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), StudentID);
            }
            else
            {
                list = AllDatesInMonth(dt.Year, dt.Month, StudentID);
            }
            List<ISHoliday> List = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
            foreach (DateTime date in list)
            {
                if (date.Date >= objStudent.CreatedDateTime.Value.Date)
                {
                    var objPickUp = DB.getAttandanceData(date).ToList();

                    MGetAttendenceData newobj = new MGetAttendenceData();
                    if (objPickUp.Where(p => p.StudentsID == objStudent.ID && p.Date != null).Count() > 0)
                    {
                        if (objPickUp.Where(p => p.StudentsID == objStudent.ID && p.Date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).Count() > 0)
                        {
                            foreach (var item in objPickUp.Where(p => p.StudentsID == objStudent.ID && p.Date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")).ToList())
                            {
                                if (item.Date.Value.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy"))
                                {
                                    newobj.StudentID = item.StudentsID;
                                    newobj.StudentName = item.StudentName;
                                    newobj.StudentPic = item.Photo;
                                    newobj.TeacherID = item.TeacherID == null ? 0 : item.TeacherID;
                                    newobj.Date = item.Date;
                                    if (DB.ISAttendances.Where(p => p.StudentID == item.StudentsID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.Date)).Count() <= 0)
                                    {
                                        if (item.Date.Value.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString());
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (item.Date.Value.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString());
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (List.Where(x => x.DateFrom.Value <= item.Date.Value && x.DateTo.Value >= item.Date.Value).Count() > 0)
                                        {
                                            string Holiday = String.Empty;
                                            foreach (var i in List)
                                            {
                                                if (item.Date.Value >= i.DateFrom.Value && item.Date.Value <= i.DateTo.Value)
                                                {
                                                    Holiday += i.Name + ", ";
                                                }
                                            }
                                            Holiday = Holiday.Remove(Holiday.Length - 2);
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = Holiday + " (School Closed)";
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = item.Status == null ? "Absent" : item.Status;
                                            newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                            newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                        }
                                    }
                                    else
                                    {
                                        newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                        newobj.Status = item.Status == null ? "Absent" : item.Status;
                                        newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                        newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                    }
                                    newobj.ClassID = item.ClassID;
                                }
                                else
                                {
                                    newobj.Date = date;
                                    newobj.TeacherID = item.TeacherID == null ? 0 : item.TeacherID;
                                    if (DB.ISAttendances.Where(p => p.StudentID == item.StudentsID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.Date)).Count() <= 0)
                                    {
                                        if (item.Date.Value.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString());
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (item.Date.Value.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = OperationManagement.GetDefaultPickupStatus(item.Date.Value.ToString());
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else if (List.Where(x => x.DateFrom.Value <= item.Date.Value && x.DateTo.Value >= item.Date.Value && x.Active == true).Count() > 0)
                                        {
                                            string Holiday = String.Empty;
                                            foreach (var i in List)
                                            {
                                                if (item.Date.Value >= i.DateFrom.Value && item.Date.Value <= i.DateTo.Value)
                                                {
                                                    Holiday += i.Name + ", ";
                                                }
                                            }
                                            Holiday = Holiday.Remove(Holiday.Length - 2);
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = Holiday + " (School Closed)";
                                            newobj.MarkedTime = "";
                                            newobj.TeacherName = "";
                                        }
                                        else
                                        {
                                            newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                            newobj.Status = item.Status == null ? "Absent" : item.Status;
                                            newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                            newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                        }
                                    }
                                    else
                                    {
                                        newobj.MarkedDate = item.Date.Value.ToString("dd/MM/yyyy");
                                        newobj.Status = item.Status == null ? "Absent" : item.Status;
                                        newobj.MarkedTime = item.Time == null ? "" : item.Time.Value.ToString("HH:mm tt");
                                        newobj.TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name;
                                    }

                                    newobj.StudentID = objStudent.ID;
                                    newobj.StudentName = objStudent.StudentName;
                                    newobj.StudentPic = objStudent.Photo;
                                    newobj.ClassID = objStudent.ClassID;
                                }
                            }
                        }
                        else
                        {
                            newobj.Date = date;
                            if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                            {
                                if (date.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString());
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                                else if (date.DayOfWeek == DayOfWeek.Sunday)
                                {
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString());
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                                else if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                                {
                                    string Holiday = String.Empty;
                                    foreach (var i in List)
                                    {
                                        if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                        {
                                            Holiday += i.Name + ", ";
                                        }
                                    }
                                    Holiday = Holiday.Remove(Holiday.Length - 2);
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = Holiday + " (School Closed)";
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                                else
                                {
                                    newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                    newobj.Status = "Absent";
                                    newobj.MarkedTime = "";
                                    newobj.TeacherName = "";
                                }
                            }
                            else
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = "Absent";
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            newobj.StudentID = objStudent.ID;
                            newobj.StudentName = objStudent.StudentName;
                            newobj.StudentPic = objStudent.Photo;
                            newobj.ClassID = objStudent.ClassID;
                        }
                    }
                    else
                    {
                        newobj.Date = date;
                        if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
                        {
                            if (date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString());
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday)
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = OperationManagement.GetDefaultPickupStatus(date.ToString());
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            else if (List.Where(x => x.DateFrom.Value <= date && x.DateTo.Value >= date && x.Active == true).Count() > 0)
                            {
                                string Holiday = String.Empty;
                                foreach (var i in List)
                                {
                                    if (date >= i.DateFrom.Value && date <= i.DateTo.Value)
                                    {
                                        Holiday += i.Name + ", ";
                                    }
                                }
                                Holiday = Holiday.Remove(Holiday.Length - 2);
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = Holiday + " (School Closed)";
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                            else
                            {
                                newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                                newobj.Status = "Absent";
                                newobj.MarkedTime = "";
                                newobj.TeacherName = "";
                            }
                        }
                        else
                        {
                            newobj.MarkedDate = date.ToString("dd/MM/yyyy");
                            newobj.Status = "Absent";
                            newobj.MarkedTime = "";
                            newobj.TeacherName = "";
                        }

                        newobj.StudentID = objStudent.ID;
                        newobj.StudentName = objStudent.StudentName;
                        newobj.StudentPic = objStudent.Photo;
                        newobj.ClassID = objStudent.ClassID;
                    }
                    objList.Add(newobj);
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (TeacherID != "0")
            {
                int TID = Convert.ToInt32(TeacherID);
                objList = objList.Where(p => p.TeacherID == TID).ToList();
            }
            if (Status != "")
            {
                if (Status == "Close")
                {
                    objList = objList.Where(p => p.Status.Contains(Status)).ToList();
                }
                else
                {
                    objList = objList.Where(p => p.Status == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {

                if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.TeacherName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.Date).ToList();
                }
            }
            else
            {

                if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.TeacherName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.Date).ToList();
                }
            }
            return objList;
        }
        public List<MGetAttendenceData> StudentAttendenceReportAfterSchool(int StudentID, string DateFrom, string DateTo, string TeacherID, string Status, string SortBy, string OrderBy, int StudID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            int ID = StudID;
            List<MGetAttendenceData> ObjList = new List<MGetAttendenceData>();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            ObjList = (from item in DB.ISAttendances.Where(p => p.StudentID == StudentID /*&& (p.Status == "Present" || p.Status == "Present(Late)")*/).ToList()
                       join item2 in DB.ISStudents.Where(p => p.Deleted == true) on item.StudentID equals item2.ID
                       select new MGetAttendenceData
                       {
                           StudentID = item2.ID,
                           StudentName = item2.StudentName,
                           StudentPic = item2.Photo,
                           TeacherID = item.TeacherID == null ? 0 : item.TeacherID,
                           Date = item.Date,
                           MarkedDate = item.Date.Value.ToString("dd/MM/yyyy"),
                           Status = item.Status,
                           MarkedTime = item.Time.Value.ToString("HH:mm tt"),
                           TeacherName = item.TeacherID == null ? "" : DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name,
                           ClassID = item2.ClassID,
                       }).ToList();
            if (DateFrom != "")
            {
                string dates = DateFrom;
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
                DateTime dt = Convert.ToDateTime(Format);
                ObjList = ObjList.Where(p => p.Date.Value.Date >= dt.Date).ToList();
            }
            if (DateTo != "")
            {
                string dates = DateFrom;
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
                DateTime dt = Convert.ToDateTime(Format);
                ObjList = ObjList.Where(p => p.Date.Value.Date <= dt.Date).ToList();
            }

            
            List<DateTime> list = new List<DateTime>();
            if (DateFrom != "" && DateTo != "")
            {
                string dates = DateFrom;
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

                string dates2 = DateTo;
                string Format2 = "";
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    Format2 = arrDate[1].ToString() + "-" + arrDate[0].ToString() + "-" + arrDate[2].ToString();
                }
                else
                {
                    Format2 = dates2;
                }
                DateTime dt3 = Convert.ToDateTime(Format2);
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), StudentID);
            }
            else
            {
                DateTime dt = DateTime.Now;
                list = AllDatesInMonth(dt.Year, dt.Month, StudentID);
            }
            if (StudentID != 0)
            {
                ObjList = ObjList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (TeacherID != "0")
            {
                int TID = Convert.ToInt32(TeacherID);
                ObjList = ObjList.Where(p => p.TeacherID == TID).ToList();
            }
            if (Status != "")
            {
                if (Status == "Close")
                {
                    ObjList = ObjList.Where(p => p.Status.Contains(Status)).ToList();
                }
                else
                {
                    ObjList = ObjList.Where(p => p.Status == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {

                if (SortBy == "AttendenceStatus")
                {
                    ObjList = ObjList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    ObjList = ObjList.OrderBy(p => p.TeacherName).ToList();
                }
                else
                {
                    ObjList = ObjList.OrderBy(p => p.Date).ToList();
                }
            }
            else
            {

                if (SortBy == "AttendenceStatus")
                {
                    ObjList = ObjList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    ObjList = ObjList.OrderByDescending(p => p.TeacherName).ToList();
                }
                else
                {
                    ObjList = ObjList.OrderByDescending(p => p.Date).ToList();
                }
            }
            return ObjList;
        }
        public static List<DateTime> AllDatesInMonth(int year, int month, int studentID)
        {
            List<DateTime> AuthorList = new List<DateTime>();
            SchoolAppEntities DB = new SchoolAppEntities();
            ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == studentID);
            if (Obj != null)
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime expiryDate = startDate.AddDays(-30);

                if (Obj.CreatedDateTime.Value.Date > expiryDate.Date)
                {
                    expiryDate = Obj.CreatedDateTime.Value.Date;
                }

                for (int i = 1; i <= startDate.Subtract(expiryDate).Days + 1; i++)
                {
                    AuthorList.Add(expiryDate.AddDays(i - 1));
                }
            }
            return AuthorList;
        }
        public static List<DateTime> AllDates(DateTime FromDate, DateTime ToDate, int studentID)
        {
            List<DateTime> AuthorList = new List<DateTime>();
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime startDate = ToDate;
            DateTime expiryDate = FromDate;

            for (int i = 0; i <= startDate.Subtract(expiryDate).Days; i++)
            {
                AuthorList.Add(expiryDate.AddDays(i));
            }

            return AuthorList;
        }
        public bool BindPickUpCheckBox(int SchoolID, int ClassID, List<MViewStudentPickUp> ObjLists)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
            if (ObjSchool != null)
            {
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    int Class = ClassID;
                    DateTime dt = DateTime.Now;

                    ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                    if (_class != null)
                    {
                        List<MISStudent> objList = new List<MISStudent>();
                        //if (_class.Name.Contains("Outside Class"))
                        //{
                        //    objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
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
                            objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                       select new MISStudent
                                       {
                                           ID = item.ID,
                                           StudentName = item.StudentName,
                                           Photo = item.Photo,
                                           CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                           ClassID = item.ClassID
                                       }).ToList();
                        }
                        var sids = ((List<MViewStudentPickUp>)ObjLists).Select(p => p.StudentID).ToList();
                        objList = objList.Where(p => sids.Contains(p.ID)).ToList();
                        foreach (var item in objList.ToList())
                        {
                            ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true);
                            if (objs != null)
                            {
                                var itemToRemove = objList.Single(r => r.ID == item.ID);
                                objList.Remove(itemToRemove);
                            }
                        }
                        if (objList.Count == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    int Class = ClassID;
                    List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                select new MISStudent
                                                {
                                                    ID = item.ID,
                                                    StudentName = item.StudentName,
                                                    Photo = item.Photo,
                                                    CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                    ClassID = item.ClassID
                                                }).ToList();
                    DateTime dt = DateTime.Now;
                    var sids = ((List<MViewStudentPickUp>)ObjLists).Select(p => p.StudentID).ToList();
                    objList = objList.Where(p => sids.Contains(p.ID)).ToList();
                    foreach (var item in objList.ToList())
                    {
                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true);
                        if (objs != null)
                        {
                            var itemToRemove = objList.Single(r => r.ID == item.ID);
                            objList.Remove(itemToRemove);
                        }
                    }
                    if (objList.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public bool BindAttendenceCheckBox(int SchoolID, int ClassID, List<MISAttendance> ObjLists)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
            if (ObjSchool != null)
            {
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    int Class = ClassID;
                    DateTime dt = DateTime.Now;

                    ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                    if (_class != null)
                    {
                        List<MISStudent> objList = new List<MISStudent>();
                        //if (_class.Name.Contains("Outside Class"))
                        //{
                        //    objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
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
                            objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && p.Deleted == true).ToList()
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    int Class = ClassID;
                    List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && p.Deleted == true).ToList()
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
