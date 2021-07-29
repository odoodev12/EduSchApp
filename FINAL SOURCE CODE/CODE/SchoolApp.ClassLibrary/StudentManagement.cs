using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class StudentManagement
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        public bool StudentClassAssignment(int StudentID, int ClassID)
        {

            ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID);
            if (obj != null)
            {
                obj.ClassID = ClassID;
                DB.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<MISAttendance> AttendenceList(int SchoolID, int ClassID, string Date, int StudentID, int TeacherID, string Status, string OrderBy, string SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
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
            ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
            List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == SchoolID && p.Active == true && p.Deleted == true).ToList();
            List<MISAttendance> objList = (from item in DB.getAttandanceData(dt).Where(p => p.StudentDeleted == true).ToList()
                                           //join item1 in obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                                           select new MISAttendance
                                           {
                                               ID = item.ID != null ? item.ID.Value : 0,
                                               SchoolID = item.SchoolID,
                                               StudentID = item.StudentsID,
                                               StudentName = item.StudentName,
                                               StudentPic = item.Photo,
                                               //AttStatus = (item.Status != "" && item.Status != null) ? item.Status : "Not Marked",
                                               Status = (item.Status != "" && item.Status != null) ? item.Status : (ObjHoliday.Where(x => x.SchoolID == item.SchoolID && x.DateFrom.Value.Date <= dt.Date && x.DateTo.Value.Date >= dt.Date && x.Active == true).Count() > 0) ? "School Closed" : "Not Marked",
                                               Date = item.Dates != null ? item.Dates : null,
                                               MarkedDate = item.Dates != null ? item.Dates.Value.ToString("dd/MM/yyyy") : "",
                                               MarkedTime = item.Time != null ? item.Time.Value.ToString("hh:mm tt") : "",
                                               TeacherName = item.TeacherID != null ? DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name : "",
                                               TeacherID = item.TeacherID != null ? item.TeacherID : null,
                                               ClassID = item.ClassID,
                                           }).ToList();
            if (SchoolID != 0)
            {
                objList = objList.Where(p => p.SchoolID == SchoolID).ToList();
            }
            if (ClassID != 0)
            {
                objList = objList.Where(p => p.ClassID == ClassID).ToList();
            }

            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
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
                if (SortBy == "StudentName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.TeacherName).ToList();
                }
                else if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.StudentID).ToList();
                }
            }
            else
            {
                if (SortBy == "StudentName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.TeacherName).ToList();
                }
                else if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.StudentID).ToList();
                }
            }
            return objList;
        }
        public List<MISAttendance> AttendenceLists(int SchoolID, int ClassID, string Date, int StudentID, int TeacherID, string Status, string OrderBy, string SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
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
            string Dates = DateTime.Now.ToString("dd/MM/yyyy");
            List<MISAttendance> objList = new List<MISAttendance>();

            ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
            List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == SchoolID && p.Active == true && p.Deleted == true).ToList();

            ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);

            //if (_class.Name.Contains("Outside Class"))
            //{
            //    var list = DB.getAttandanceData(dt).Where(p => p.StartDate != null && p.StudentDeleted == true).ToList();
            //    objList = (from item in list.Where(p => p.SchoolID == SchoolID && p.StartDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy") || !p.StartDate.HasValue).ToList()
            //               join item1 in obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
            //               select new MISAttendance
            //               {
            //                   ID = item.ID != null ? item.ID.Value : 0,
            //                   SchoolID = item.SchoolID,
            //                   StudentID = item.StudentsID,
            //                   StudentName = item.StudentName,
            //                   StudentPic = item.Photo,
            //                   //AttStatus = (item.Status != "" && item.Status != null) ? item.Status : "Not Marked",
            //                   Status = (item.Status != "" && item.Status != null) ? item.Status : (ObjHoliday.Where(x => x.DateFrom.Value <= dt && x.DateTo.Value >= dt && x.Active == true).Count() > 0) ? "School Closed" : "Not Marked",
            //                   Date = item.Dates != null ? item.Dates : null,
            //                   MarkedDate = item.Dates != null ? item.Dates.Value.ToString("dd/MM/yyyy") : "",
            //                   MarkedTime = item.Time != null ? item.Time.Value.ToString("hh:mm tt") : "",
            //                   TeacherName = item.TeacherID != null ? DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name : "",
            //                   TeacherID = item.TeacherID != null ? item.TeacherID : null,
            //                   ClassID = item.ClassID,
            //               }).ToList();
            //}
            //else
            {
                objList = (from item in DB.getAttandanceData(dt).Where(p => p.SchoolID == SchoolID && p.StudentDeleted == true)
                           join item1 in obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                           select new MISAttendance
                           {
                               ID = item.ID != null ? item.ID.Value : 0,
                               SchoolID = item.SchoolID,
                               StudentID = item.StudentsID,
                               StudentName = item.StudentName,
                               StudentPic = item.Photo,
                               //AttStatus = (item.Status != "" && item.Status != null) ? item.Status : "Not Marked",
                               Status = (item.Status != "" && item.Status != null) ? item.Status : (ObjHoliday.Where(x => x.DateFrom.Value <= dt && x.DateTo.Value >= dt && x.Active == true).Count() > 0) ? "School Closed" : "Not Marked",
                               Date = item.Dates != null ? item.Dates : null,
                               MarkedDate = item.Dates != null ? item.Dates.Value.ToString("dd/MM/yyyy") : "",
                               MarkedTime = item.Time != null ? item.Time.Value.ToString("hh:mm tt") : "",
                               TeacherName = item.TeacherID != null ? DB.ISTeachers.SingleOrDefault(p => p.ID == item.TeacherID).Name : "",
                               TeacherID = item.TeacherID != null ? item.TeacherID : null,
                               ClassID = item.ClassID,
                           }).ToList();
            }
            if (ClassID != 0)
            {
                objList = objList.Where(p => p.ClassID == ClassID).ToList();
            }

            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
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
                if (SortBy == "StudentName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.TeacherName).ToList();
                }
                else if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.StudentID).ToList();
                }
            }
            else
            {
                if (SortBy == "StudentName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.TeacherName).ToList();
                }
                else if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.StudentID).ToList();
                }
            }
            return objList;
        }
        public List<MISAttendance> StudentAttendenceReport(int StudentID, string DateFrom, string DateTo, int TeacherID, string Status, string OrderBy, string SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISAttendance> objList = (from item in DB.ISAttendances.Where(p => p.StudentID == StudentID && p.Deleted == true && p.Active == true).ToList()
                                           select new MISAttendance
                                           {
                                               ID = item.ID,
                                               StudentID = item.StudentID,
                                               StudentName = item.ISStudent.StudentName,
                                               StudentPic = item.ISStudent.Photo,
                                               Status = item.Status,
                                               Date = item.Date,
                                               MarkedDate = item.Date.Value.ToString("dd/MM/yyyy"),
                                               MarkedTime = item.Time.Value.ToString("hh:mm tt"),
                                               TeacherName = item.ISTeacher.Name,
                                               TeacherID = item.TeacherID,
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
                objList = objList.Where(p => p.Date.Value.Date >= dt.Date).ToList();
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
                objList = objList.Where(p => p.Date.Value.Date <= dt2.Date).ToList();
            }
            if (TeacherID != 0)
            {
                objList = objList.Where(p => p.TeacherID == TeacherID).ToList();
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
                if (SortBy == "StudentName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.TeacherName).ToList();
                }
                else if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.Date).ToList();
                }
            }
            else
            {
                if (SortBy == "StudentName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.TeacherName).ToList();
                }
                else if (SortBy == "AttendenceStatus")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.Date).ToList();
                }
            }
            return objList;
        }
    }
}
