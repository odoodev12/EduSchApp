using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class ClassManagement
    {

        public List<MISClass> ClassList(int SchoolID, string Year, int typeID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISClass> objList = (from item in DB.ISClasses.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                      select new MISClass
                                      {
                                          ID = item.ID,
                                          SchoolID = item.SchoolID,
                                          Name = item.Name,
                                          YearName = !String.IsNullOrEmpty(item.Year) ? getyeardata(item.Year) : "N/A",
                                          Year = item.Year,
                                          TypeID = item.TypeID,
                                          AfterSchoolType = item.AfterSchoolType,
                                          ExternalOrganisation = item.ExternalOrganisation,
                                          EndDate = item.EndDate,
                                          EndDateString = item.EndDate != null ? item.EndDate.Value.ToString("dd/MM/yyyy") : "N/A",
                                          PickupComplete = item.PickupComplete,
                                          Active = item.Active,
                                          Deleted = item.Deleted,
                                          CreatedBy = item.CreatedBy,
                                          CreatedDateTime = item.CreatedDateTime,
                                          ModifyBy = item.ModifyBy,
                                          ModifyDateTime = item.ModifyDateTime,
                                          DeletedBy = item.DeletedBy,
                                          DeletedDateTime = item.DeletedDateTime,
                                          ClassType = item.ISClassType.Name,
                                          CreateBy = item.CreatedByType == null ? "Admin" : (item.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.School) ? DB.ISSchools.SingleOrDefault(p => p.ID == item.CreatedBy).Name : DB.ISTeachers.SingleOrDefault(p => p.ID == item.CreatedBy).Name,
                                          StudentCount = item.TypeID == (int)EnumsManagement.CLASSTYPE.Office ?
                                            getOfficeStudentCount(item.SchoolID) : item.TypeID == (int)EnumsManagement.CLASSTYPE.Club ?
                                            getClubStudentCount(item.SchoolID) : item.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool ?
                                            getInternalStudentCount(item.SchoolID) : item.Name.Contains("Outside") ?
                                            getExternalStudentCount(item.SchoolID) : item.ISStudents.Where(p => p.ClassID == item.ID && p.SchoolID == SchoolID && p.Deleted == true && p.Active == true).ToList().Count,
                                      }).ToList();

            if (Year != "")
            {
                objList = objList.Where(p => p.Year == Year).ToList();
            }
            if (typeID != 0)
            {
                objList = objList.Where(p => p.TypeID == typeID).ToList();
            }
            return objList.OrderByDescending(r => r.TypeID).ToList();
        }

        public List<MISClass> ClassListByFilter(int SchoolID, string Year, int typeID, string Status)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<MISClass> objList = (from item in DB.ISClasses.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                      select new MISClass
                                      {
                                          ID = item.ID,
                                          SchoolID = item.SchoolID,
                                          Name = item.Name,
                                          YearName = (item.Year != "" && item.Year != null) ? getyeardata(item.Year) : "N/A",
                                          Year = item.Year,
                                          TypeID = item.TypeID,
                                          AfterSchoolType = item.AfterSchoolType,
                                          ExternalOrganisation = item.ExternalOrganisation,
                                          EndDate = item.EndDate,
                                          EndDateString = item.EndDate != null ? item.EndDate.Value.ToString("dd/MM/yyyy") : "N/A",
                                          PickupComplete = item.PickupComplete,
                                          Active = item.Active,
                                          Status = item.Active == true ? "Active" : "InActive",
                                          Deleted = item.Deleted,
                                          CreatedBy = item.CreatedBy,
                                          CreatedDateTime = item.CreatedDateTime,
                                          ModifyBy = item.ModifyBy,
                                          ModifyDateTime = item.ModifyDateTime,
                                          DeletedBy = item.DeletedBy,
                                          DeletedDateTime = item.DeletedDateTime,
                                          CreateBy = item.CreatedByType == null ? "Admin" : (item.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.School) ? DB.ISSchools.SingleOrDefault(p => p.ID == item.CreatedBy).Name : DB.ISTeachers.SingleOrDefault(p => p.ID == item.CreatedBy).Name,
                                          ClassType = item.ISClassType.Name,
                                          StudentCount = item.TypeID == (int)EnumsManagement.CLASSTYPE.Office ?
                                            getOfficeStudentCount(item.SchoolID) : item.TypeID == (int)EnumsManagement.CLASSTYPE.Club ?
                                            getClubStudentCount(item.SchoolID) : item.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool ?
                                            getInternalStudentCount(item.SchoolID) : item.ISStudents.Where(p => p.ClassID == item.ID && p.SchoolID == SchoolID && p.Deleted == true && p.Active == true).ToList().Count,
                                      }).ToList();

            if (Year != "")
            {
                objList = objList.Where(p => p.Year == Year).ToList();
            }
            if (typeID != 0)
            {
                objList = objList.Where(p => p.TypeID == typeID).ToList();
            }
            if (Status == "1")
            {
                objList = objList.Where(p => p.Active == true).ToList();
            }
            if (Status == "2")
            {
                objList = objList.Where(p => p.Active == false).ToList();
            }
            return objList;
        }
        public string getyeardata(string year)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISClassYear obj = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == year);
            if (obj != null)
            {
                return obj.Year;
            }
            else
            {
                return "";
            }
        }

        public List<ISClassType> ClassTyleList()
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<ISClassType> objList = DB.ISClassTypes.Where(p => p.Deleted == true && (p.ID != (int)EnumsManagement.CLASSTYPE.Office && p.ID != (int)EnumsManagement.CLASSTYPE.Club)).ToList();


            return objList;
        }
        public List<MISClassType> ClassTypeList()
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISClassType> objList = (from item in DB.ISClassTypes.Where(p => p.Deleted == true).ToList()
                                          select new MISClassType
                                          {
                                              ID = item.ID,
                                              Name = item.Name,
                                              Active = item.Active,
                                              Deleted = item.Deleted,
                                              CreatedBy = item.CreatedBy,
                                              CreatedDateTime = item.CreatedDateTime,
                                              ModifyBy = item.ModifyBy,
                                              ModifyDateTime = item.ModifyDateTime,
                                              DeletedBy = item.DeletedBy,
                                              DeletedDateTime = item.DeletedDateTime,
                                          }).ToList();
            return objList;
        }

        public List<MISClassType> ClassTypeListSchoolWise(int mISSchoolType)
        {

            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISClassType> objList = (from item in DB.ISClassTypes.Where(p => p.Deleted == true).ToList()
                                          select new MISClassType
                                          {
                                              ID = item.ID,
                                              Name = item.Name,
                                              Active = item.Active,
                                              Deleted = item.Deleted,
                                              CreatedBy = item.CreatedBy,
                                              CreatedDateTime = item.CreatedDateTime,
                                              ModifyBy = item.ModifyBy,
                                              ModifyDateTime = item.ModifyDateTime,
                                              DeletedBy = item.DeletedBy,
                                              DeletedDateTime = item.DeletedDateTime,
                                              ClassType = (EnumsManagement.CLASSTYPE)item.ID
                                          }).ToList();

            if (mISSchoolType == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                objList = objList.Where(r => r.ClassType != EnumsManagement.CLASSTYPE.AfterSchool &&
                                            r.ClassType != EnumsManagement.CLASSTYPE.Club &&
                                            r.ClassType != EnumsManagement.CLASSTYPE.Office
                                            ).ToList();
            else
                objList = objList.Where(r => r.ClassType != EnumsManagement.CLASSTYPE.Club &&
                                            r.ClassType != EnumsManagement.CLASSTYPE.Office

                                            ).ToList();
            return objList;
        }
        public MISClass GetClass(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            ISClass Obj = DB.ISClasses.SingleOrDefault(p => p.ID == ID);
            MISClass objMclass = new MISClass();
            if (Obj != null)
            {
                objMclass.ID = Obj.ID;
                objMclass.SchoolID = Obj.SchoolID;
                objMclass.Name = Obj.Name;
                objMclass.Year = String.IsNullOrEmpty(Obj.Year) ? "" : Obj.Year;
                objMclass.TypeID = Obj.TypeID;
                objMclass.AfterSchoolType = String.IsNullOrEmpty(Obj.AfterSchoolType) ? "N/A" : Obj.AfterSchoolType;
                objMclass.ExternalOrganisation = String.IsNullOrEmpty(Obj.ExternalOrganisation) ? "N/A" : Obj.ExternalOrganisation;
                objMclass.EndDate = Obj.EndDate;
                objMclass.EndDateString = String.IsNullOrEmpty(Obj.EndDate.ToString()) ? "N/A" : Obj.EndDate.Value.ToString("dd/MM/yyyy");
                objMclass.ClassType = !String.IsNullOrEmpty(Obj.TypeID.ToString()) ? Obj.ISClassType.Name : "";
                objMclass.StudentCount = Obj.TypeID == (int)EnumsManagement.CLASSTYPE.Office ?
                        getOfficeStudentCount(Obj.SchoolID) : Obj.TypeID == (int)EnumsManagement.CLASSTYPE.Club ?
                        getClubStudentCount(Obj.SchoolID) : Obj.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool ?
                        getInternalStudentCount(Obj.SchoolID) : getStudentCount(Obj.ID, Obj.SchoolID);
                objMclass.TeacherName = GetClassAssignedTeacher(Obj.ID) == "" ? "No Teacher assigned to this Class" : GetClassAssignedTeacher(Obj.ID);
                int IDYear = !String.IsNullOrEmpty(Obj.Year) ? Convert.ToInt32(Obj.Year) : 0;
                objMclass.YearName = IDYear != 0 ? DB.ISClassYears.SingleOrDefault(p => p.ID == IDYear).Year : "N/A";
                objMclass.Active = Obj.Active;
                objMclass.Status = Obj.Active == true ? "Active" : "InActive";
                objMclass.ISNonListed = Obj.ISNonListed;
            }
            return objMclass;


        }
        public string GetClassAssignedTeacher(int ClassID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Teachers = "";
            string TeachersName = "";
            var obj = DB.ISTeacherClassAssignments.Where(p => p.ClassID == ClassID && p.ISTeacher.Active == true && p.Active == true).ToList();
            if (obj.Count > 0)
            {
                foreach (var items in obj)
                {
                    Teachers += items.ISTeacher.Name + ", ";
                }
                TeachersName = Teachers.Remove(Teachers.Length - 2);
            }
            return TeachersName;
        }
        public int getStudentCount(int ClassID, int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            return DB.ISStudents.Where(p => p.ClassID == ClassID && p.SchoolID == SchoolID && p.Deleted == true && p.Active == true).Count();

        }
        public int getOfficeStudentCount(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            return DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.PickStatus == "Send to Office" && p.ISStudent.SchoolID == SchoolID).ToList().Count;
        }
        public int getClubStudentCount(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            return DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.PickStatus == "Club" && p.ISStudent.SchoolID == SchoolID).ToList().Count;
        }
        public int getInternalStudentCount(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            return DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && (p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex") && p.ISStudent.SchoolID == SchoolID).ToList().Count;
        }
        public int getExternalStudentCount(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            return DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && (p.PickStatus == "After-School-Ex") && p.ISStudent.SchoolID == SchoolID).ToList().Count;
        }
        public List<MViewStudentPickUp> ClassReport(int ClassID, int TeacherID, string Date)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
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
                ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                double? Count = objList.Sum(p => p.StudentPickUpAverage);
                double Counts = objList.Count;
                string PickUpAverage = "";

                if (Count != 0)
                {
                    double? Avg = Count / Counts;
                    PickUpAverage = String.Format("{0:N2}", Avg);
                }
                else
                {
                    PickUpAverage = "0.00";
                }

                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Obj.SchoolID);
                List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true).ToList();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)
                                   //join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                               select new MViewStudentPickUp
                               {
                                   ID = item.ID == null ? 0 : item.ID,
                                   StudentID = item.StudID,
                                   StudentName = item.StudentName,
                                   StudentPic = item.Photo,
                                   TeacherID = item.TeacherID,
                                   PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value <= dt && p.DateTo >= dt).Count() > 0) ? "School Closed" : OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                                   //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                   Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                   Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                   PickerID = item.PickerID == null ? 0 : item.PickerID,
                                   PickDate = item.PickDate == null ? null : item.PickDate,
                                   PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                   PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                                   ClassID = item.ClassID,
                                   PickUpAverage = PickUpAverage,
                                   StudentPickUpAverage = GetAverage(item.ClassID, item.StudID),
                                   StudentPickUpAverageStr = String.Format("{0:N2}", GetAverage(item.ClassID, item.StudID)),
                               }).ToList();
                    //if (ClassID != 0)
                    //{
                    //    //int SID = Convert.ToInt32(StudentID);
                    //    objList = objList.Where(p => p.ClassID == ClassID).ToList();
                    //}
                    if (ClassID != 0)
                    {
                        ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            //objList = objList.Where(p => p.PickStatus == "Send to Office").ToList();
                            List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            var list = objList.Where(p => p.PickStatus == "Send to Office").ToList();
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
                            //    //objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus == "After-School-Ex").ToList();
                            //    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            //    var list = objList.Where(p => p.PickStatus == "After-School-Ex" && p.SchoolID == ObjClass.SchoolID).ToList();
                            //    foreach (var item in list)
                            //    {
                            //        if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                            //        {
                            //            Lists.Add(item);
                            //        }
                            //    }
                            //    objList = Lists;
                            //}
                            //else
                            {
                                objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Send to Office" && p.PickStatus != "Club" && p.PickStatus != "After-School" && p.PickStatus != "After-School-Ex").ToList();
                            }
                        }
                    }
                }
                else
                {
                    objList = (from item in DB.getPickUpData(dt).Where(p => p.SchoolID == Obj.SchoolID && p.Deleted == true && (p.StartDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy") || !p.StartDate.HasValue))
                                   //join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                               select new MViewStudentPickUp
                               {
                                   ID = item.ID == null ? 0 : item.ID,
                                   StudentID = item.StudID,
                                   StudentName = item.StudentName,
                                   StudentPic = item.Photo,
                                   TeacherID = item.TeacherID,
                                   PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value <= dt && p.DateTo >= dt).Count() > 0) ? "School Closed" : OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                                   //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                   Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                   Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                   PickerID = item.PickerID == null ? 0 : item.PickerID,
                                   PickDate = item.PickDate == null ? null : item.PickDate,
                                   PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                   PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                                   ClassID = item.ClassID,
                                   PickUpAverage = PickUpAverage,
                                   StudentPickUpAverage = GetAverage(item.ClassID, item.StudID),
                                   StudentPickUpAverageStr = String.Format("{0:N2}", GetAverage(item.ClassID, item.StudID)),
                               }).ToList();
                    //if (ClassID != 0)
                    //{
                    //    //int SID = Convert.ToInt32(StudentID);
                    //    objList = objList.Where(p => p.ClassID == ClassID).ToList();
                    //}
                    if (ClassID != 0)
                    {
                        ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            //objList = objList.Where(p => p.PickStatus == "Send to Office").ToList();
                            List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            var list = objList.Where(p => p.PickStatus == "Send to Office").ToList();
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
                            //    //objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus == "After-School-Ex").ToList();
                            //    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            //    var list = objList.Where(p => p.PickStatus == "After-School-Ex" && p.SchoolID == ObjClass.SchoolID).ToList();
                            //    foreach (var item in list)
                            //    {
                            //        if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                            //        {
                            //            Lists.Add(item);
                            //        }
                            //    }
                            //    objList = Lists;
                            //}
                            //else
                            {
                                objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Send to Office" && p.PickStatus != "Club" && p.PickStatus != "After-School" && p.PickStatus != "After-School-Ex").ToList();
                            }
                        }
                    }
                }
            }
            else
            {
                ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                //int Count = DB.ISPickups.Where(p => p.ISStudent.ClassID == ClassID).GroupBy(p => p.StudentID).ToList().Count;
                //decimal Count = DB.ISPickups.Where(p => p.ISStudent.ClassID == ClassID).GroupBy(p => p.StudentID).ToList().Count;
                //decimal Counts = DB.ISPickups.Where(p => p.ISStudent.SchoolID == Obj.ISSchool.ID).ToList().Count;
                //string PickUpAverage = "";
                //if (Count != 0)
                //{
                //    double Avg = Convert.ToDouble(Count / Counts);
                //    PickUpAverage = String.Format("{0:N2}", Avg);
                //}
                //else
                //{
                //    PickUpAverage = "0.00";
                //}
                double? Count = objList.Sum(p => p.StudentPickUpAverage);
                double Counts = objList.Count;
                string PickUpAverage = "";

                if (Count != 0)
                {
                    double? Avg = Count / Counts;
                    PickUpAverage = String.Format("{0:N2}", Avg);
                }
                else
                {
                    PickUpAverage = "0.00";
                }

                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Obj.SchoolID);
                List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true).ToList();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)
                                   //DB.ISStudents.Where(p => p.ClassID == ClassID && p.Deleted == true).ToList()
                                   //join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                               select new MViewStudentPickUp
                               {
                                   ID = item.ID == null ? 0 : item.ID,
                                   StudentID = item.StudID,
                                   StudentName = item.StudentName,
                                   StudentPic = item.Photo,
                                   TeacherID = item.TeacherID,
                                   PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value <= dt && p.DateTo >= dt).Count() > 0) ? "School Closed" : OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                                   //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                   Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                   Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                   PickerID = item.PickerID == null ? 0 : item.PickerID,
                                   PickDate = item.PickDate == null ? null : item.PickDate,
                                   PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                   PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                                   ClassID = item.ClassID,
                                   PickUpAverage = PickUpAverage,
                                   StudentPickUpAverage = GetAverage(item.ClassID, item.StudID),
                                   StudentPickUpAverageStr = String.Format("{0:N2}", GetAverage(item.ClassID, item.StudID)),
                                   OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                                   AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                                   CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                                   ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                               }).ToList();

                    if (ClassID != 0)
                    {
                        ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            //objList = objList.Where(p => p.PickStatus == "Send to Office").ToList();
                            List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            var list = objList.Where(p => p.PickStatus == "Office" || p.OfficeFlag).ToList();
                            foreach (var item in list)
                            {
                                if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
                                    objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" && p.OfficeFlag)).Count() == 1)
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
                                if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
                                    objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" && p.ClubFlag)).Count() == 1)
                                {
                                    Lists.Add(item);
                                }
                            }
                            objList = Lists;
                        }
                        else if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                        {
                            List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            var list = objList.Where(p => (p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex" || p.AfterSchoolFlag)).ToList();
                            foreach (var item in list)
                            {
                                if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Send to Office" || p.PickStatus == "Club")).Count() <= 0 ||
                                    objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" && p.AfterSchoolFlag)).Count() == 1)
                                {
                                    Lists.Add(item);
                                }

                            }
                            objList = Lists;
                        }
                        else
                        {
                            //    if (ObjClass.Name.Contains("Outside Class"))
                            //    {
                            //        //objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus == "After-School-Ex").ToList();
                            //        List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            //        var list = objList.Where(p => p.PickStatus == "After-School-Ex" && p.SchoolID == ObjClass.SchoolID).ToList();
                            //        foreach (var item in list)
                            //        {
                            //            if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                            //            {
                            //                Lists.Add(item);
                            //            }
                            //        }
                            //        objList = Lists;
                            //    }
                            //else
                            {
                                objList = objList.Where(p => p.ClassID == ClassID /*&& p.PickStatus != "Send to Office"*/).ToList();
                            }
                        }
                    }
                }
                else
                {
                    objList = (from item in DB.ISStudents.Where(p => p.ClassID == ClassID && p.Deleted == true && (DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) || !p.StartDate.HasValue)).ToList()
                                   //join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                               select new MViewStudentPickUp
                               {
                                   ID = null,
                                   StudentID = item.ID,
                                   StudentName = item.StudentName,
                                   StudentPic = item.Photo,
                                   TeacherID = null,
                                   PickStatus = (objHoliday.Where(p => p.DateFrom.Value <= dt && p.DateTo >= dt).Count() > 0) ? "School Closed" : "",
                                   //PickStatus = "",
                                   Pick_Time = "",
                                   Pick_Date = "",
                                   PickerID = null,
                                   PickDate = null,
                                   PickerName = null,
                                   PickerImage = null,
                                   ClassID = item.ClassID,
                                   PickUpAverage = PickUpAverage,
                                   StudentPickUpAverage = GetAverage(item.ClassID, item.ID),
                                   StudentPickUpAverageStr = String.Format("{0:N2}", GetAverage(item.ClassID, item.ID)),
                               }).ToList();

                    if (ClassID != 0)
                    {
                        ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                        if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            //objList = objList.Where(p => p.PickStatus == "Send to Office").ToList();
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
                            //objList = objList.Where(p => p.PickStatus == "Send to Office").ToList();
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
                            //    //objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus == "After-School-Ex").ToList();
                            //    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                            //    var list = objList.Where(p => p.PickStatus == "After-School-Ex" && p.SchoolID == ObjClass.SchoolID).ToList();
                            //    foreach (var item in list)
                            //    {
                            //        if (objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked").Count() <= 0)
                            //        {
                            //            Lists.Add(item);
                            //        }
                            //    }
                            //    objList = Lists;
                            //}
                            //else
                            {
                                objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Send to Office" && p.PickStatus != "Club" && p.PickStatus != "After-School" && p.PickStatus != "After-School-Ex").ToList();
                            }
                        }
                    }
                }
            }
            return objList;
        }

        public double? GetAverage(int? classID, int? studentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            double pickedStatusCount = DB.ISPickups.Where(p => p.StudentID == studentID && (p.PickStatus == "Picked" || 
                                                                    p.PickStatus == "Office" || p.PickStatus == "Club"|| 
                                                                    p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count();

            double pickedLateChargeReportCount = DB.ISPickups.Where(p => p.StudentID == studentID && (p.PickStatus == "Picked(Late)" || 
                                                                    p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)")).Count();

            pickedLateChargeReportCount += pickedStatusCount;
            

            if (pickedLateChargeReportCount > 0 && pickedStatusCount > 0)
            {
                double TotalAvg = (pickedStatusCount / pickedLateChargeReportCount) * 100;
                return TotalAvg;
            }
            else
            {
                return 0.0;
            }
            //double StCount = DB.ISPickups.Where(p => p.StudentID == studentID).ToList().Count;
            ////List<ISStudent> ObjSt = DB.ISStudents.Where(p => p.ClassID == classID).ToList();
            //double ClCount = DB.ISPickups.Where(p => p.ISStudent.ClassID == classID).ToList().Count;
            //double TotalAvg = Convert.ToDouble(StCount / ClCount);

        }

        public List<MISStudent> StudentListByClass(int SchoolID, int ClassID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            ClassID = item.ClassID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            Photo = item.Photo,
                                            ClassName = item.ISClass.Name,
                                            TotalStudent = ClassID != 0 ? DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.ClassID == ClassID).Count() : 0
                                        }).ToList();
            if (ClassID != 0)
            {
                objList = objList.Where(p => p.ClassID == ClassID).ToList();
            }
            return objList;
        }
        public List<MISStudent> StudentListByExtClass(int SchoolID, int ClassID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.Deleted == true && p.Active == true).ToList()
                                        join items in DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && (p.PickStatus == "After-School-Ex") && p.ISStudent.SchoolID == SchoolID).ToList() on item.ID equals items.StudentID
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            ClassID = item.ClassID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            Photo = item.Photo,
                                            ClassName = item.ISClass.Name
                                        }).ToList();
            return objList;
        }
        public List<MISStudent> StudentListByOfficeClass(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID).ToList()
                                        join items in DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.PickStatus == "Send to Office").ToList() on item.ID equals items.StudentID
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            ClassID = item.ClassID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            Photo = item.Photo,
                                            ClassName = item.ISClass.Name
                                        }).ToList();
            return objList;
        }
        public List<MISStudent> StudentListByInternalClass(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID).ToList()
                                        join items in DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && (p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).ToList() on item.ID equals items.StudentID
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            ClassID = item.ClassID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            Photo = item.Photo,
                                            ClassName = item.ISClass.Name
                                        }).ToList();
            return objList;
        }
        public List<MViewStudentPickUp> DailyClassReportFilter(int ClassID, string Date, int TeacherID, int StudentID, string Status, string OrderBy, string SortBy)
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
            }
            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
            List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == Obj.SchoolID && p.Active == true).ToList();

            List<MViewStudentPickUp> objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)//ViewStudentPickUps.Where(p => p.Deleted == true).ToList()
                                                join item1 in Obj.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                                                select new MViewStudentPickUp
                                                {
                                                    ID = item.ID == null ? 0 : item.ID,
                                                    StudentID = item.StudID,
                                                    StudentName = item.StudentName,
                                                    StudentPic = item.Photo,
                                                    TeacherID = item.TeacherID,
                                                    PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value <= dt && p.DateTo >= dt).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                    //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                    Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm"),
                                                    Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                                    PickerID = item.PickerID == null ? 0 : item.PickerID,
                                                    PickDate = item.PickDate == null ? null : item.PickDate,
                                                    PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                                    PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                                                    ClassID = item.ClassID,
                                                }).ToList();
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
                objList = objList.Where(p => p.PickStatus == Status).ToList();
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "Student")
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
                if (SortBy == "Student")
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

        public MISStudent GetStudentInfo(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
            MISStudent objMclass = new MISStudent();
            if (Obj != null)
            {
                objMclass.StudentNo = Obj.StudentNo;
                objMclass.StudentName = Obj.StudentName;
                objMclass.Photo = Obj.Photo;
                objMclass.ClassID = Obj.ClassID;
                objMclass.ClassName = Obj.ISClass.Name;
                objMclass.ParantName1 = Obj.ParantName1;
                objMclass.ParantEmail1 = Obj.ParantEmail1;
                objMclass.ParantPhone1 = Obj.ParantPhone1;
                objMclass.ParantRelation1 = Obj.ParantRelation1;
                objMclass.ParantName2 = Obj.ParantName2;
                objMclass.ParantEmail2 = Obj.ParantEmail2;
                objMclass.ParantPhone2 = Obj.ParantPhone2;
                objMclass.ParantRelation2 = Obj.ParantRelation2;
            }
            return objMclass;
        }
        public ISClass CreateorUpdateClass(int ClassID, int SchoolID, string Name, string Year, int TypeID, string AfterSchoolType, string ExternalOrganisation, bool Active)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISClass obj = new ISClass();
            if (ClassID != 0)
            {
                obj = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID && p.Deleted == true);
                obj.SchoolID = SchoolID;
                if (TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                {
                    if (AfterSchoolType == "Internal")
                    {
                        obj.Name = Name + "(After School)";
                    }
                    else
                    {
                        obj.Name = Name + "(After School Ex)";
                    }
                }
                else
                {
                    obj.Name = Name;
                }
                obj.Year = Year;
                obj.TypeID = TypeID;
                obj.AfterSchoolType = TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool ? AfterSchoolType : "";
                obj.ExternalOrganisation = TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && AfterSchoolType == "External" ? ExternalOrganisation : "";
                obj.Active = Active;
                obj.ISNonListed = false; //TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && AfterSchoolType == "External" ? ISNonListed : false;
                if (Active == false)
                {
                    List<ISTeacherClassAssignment> ObjAssignment = DB.ISTeacherClassAssignments.Where(p => p.ClassID == ClassID && p.Active == true).ToList();
                    foreach (var item in ObjAssignment)
                    {
                        item.Active = false;
                        item.Deleted = false;
                        item.DeletedBy = SchoolID;
                        item.DeletedDateTime = DateTime.Now;
                    }
                }
                obj.ModifyBy = SchoolID;
                obj.ModifyDateTime = DateTime.Now;
                DB.SaveChanges();
            }
            else
            {
                obj = new ISClass();
                obj.SchoolID = SchoolID;
                obj.Name = Name;
                obj.Year = Year;
                obj.TypeID = TypeID;
                obj.AfterSchoolType = AfterSchoolType;
                obj.ExternalOrganisation = TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && AfterSchoolType == "External" ? ExternalOrganisation : "";
                //if (EndDate != "")
                //{
                //    string dates = EndDate;
                //    string Format = "";
                //    if (dates.Contains("/"))
                //    {
                //        string[] arrDate = dates.Split('/');
                //        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                //    }
                //    else
                //    {
                //        Format = dates;
                //    }
                //    DateTime dt2 = Convert.ToDateTime(Format);
                //    obj.EndDate = dt2.Date;
                //}
                //else
                //{
                obj.EndDate = new DateTime(2050, 01, 01);
                //}
                obj.ISNonListed = false; //TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && AfterSchoolType == "External" ? ISNonListed : false;
                obj.Active = Active;
                obj.Deleted = true;
                obj.CreatedBy = SchoolID;
                obj.CreatedDateTime = DateTime.Now;
                DB.ISClasses.Add(obj);
                DB.SaveChanges();
            }
            return obj;
        }
        public List<MViewStudentPickUp> DailyClassReports(int SchoolID, string Date, int ClassID, int TeacherID, int StudentID, string Status, string OrderBy, string SortBy)
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
            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
            List<MISStudent> obj = new List<MISStudent>();
            List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true).ToList();
            if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
            {
                objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)//ViewStudentPickUps.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                                                          //join item1 in DB.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                           select new MViewStudentPickUp
                           {
                               ID = item.ID == null ? 0 : item.ID,
                               StudentID = item.StudID,
                               StudentName = item.StudentName,
                               StudentPic = item.Photo,
                               TeacherID = item.TeacherID,
                               SchoolID = item.SchoolID,
                               PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? GetHolidayName(objHoliday, dt) : "Not Marked" : item.PickStatus,
                               //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                               Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                               Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                               PickerID = item.PickerID == null ? 0 : item.PickerID,
                               PickDate = item.PickDate == null ? null : item.PickDate,
                               PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                               PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                               ClassID = item.ClassID,
                               OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                               AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                               CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                               ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                           }).ToList();
            }
            else
            {
                objList = (from item in DB.getPickUpData(dt).Where(p => p.SchoolID == SchoolID && p.Deleted == true)//ViewStudentPickUps.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                                                                                    //join item1 in DB.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                           select new MViewStudentPickUp
                           {
                               ID = item.ID == null ? 0 : item.ID,
                               StudentID = item.StudID,
                               StudentName = item.StudentName,
                               StudentPic = item.Photo,
                               TeacherID = item.TeacherID,
                               SchoolID = item.SchoolID,
                               PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? GetHolidayName(objHoliday, dt) : "Not Marked" : item.PickStatus,
                               //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                               Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                               Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                               PickerID = item.PickerID == null ? 0 : item.PickerID,
                               PickDate = item.PickDate == null ? null : item.PickDate,
                               PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                               PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                               ClassID = item.ClassID,
                               StartDates = item.StartDate == null ? "" : item.StartDate.Value.ToString("dd/MM/yyyy"),
                               OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                               AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                               CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                               ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                           }).ToList();
            }
            //if (ClassID != 0)
            //{
            //    objList = objList.Where(p => p.ClassID == ClassID).ToList();
            //}
            if (ClassID != 0)
            {
                ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "Office" || p.OfficeFlag).ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
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
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
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
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Send to Office" || p.PickStatus == "Club")).Count() <= 0 ||
                            objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked" && p.AfterSchoolFlag).Count() == 1)
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
                    //    var list = objList.Where(p => p.SchoolID == ObjClass.SchoolID).ToList();
                    //    foreach (var item in list)
                    //    {
                    //        if (objList.Where(p => p.StudentID == item.StudentID && p.StartDates == dt.ToString("dd/MM/yyyy")).Count() > 0)
                    //        {
                    //            Lists.Add(item);
                    //        }
                    //    }
                    //    objList = Lists;
                    //}
                    //else
                    {
                        objList = objList.Where(p => p.ClassID == ClassID).ToList();
                        //objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Send to Office" && p.PickStatus != "After-School" && p.PickStatus != "After-School-Ex").ToList();
                    }
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (Status != "")
            {
                if (Status == "Send to After School")
                {
                    objList = objList.Where(p => p.PickStatus.Contains("After-School")).ToList();
                }
                else
                {
                    objList = objList.Where(p => p.PickStatus == Status).ToList();
                }
            }
            if (OrderBy == "ASC")
            {
                if (SortBy == "Student")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.PickStatus).ToList();
                }
                if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
            }
            else
            {
                if (SortBy == "Student")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.PickStatus).ToList();
                }
                if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
            }
            return objList;
        }

        public string GetHolidayName(List<ISHoliday> objHoliday, DateTime dt)
        {
            var holidayObj = objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).ToList();

            string holidayName = string.Join(",", holidayObj.Select(r => r.Name).ToArray());

            return holidayName + "(School Closed)";
        }

        public List<int> GetStudentListForReportFilterOnly(int ClassID, string Date, int TeacherID, int SchoolID)
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
            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
            List<MISStudent> obj = new List<MISStudent>();
            List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true).ToList();

            objList = (from item in DB.getPickUpData(dt).Where(p => p.Deleted == true)//ViewStudentPickUps.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                                                      //join item1 in DB.ISTeacherClassAssignments on item.ClassID equals item1.ClassID
                       select new MViewStudentPickUp
                       {
                           ID = item.ID == null ? 0 : item.ID,
                           StudentID = item.StudID,
                           StudentName = item.StudentName,
                           StudentPic = item.Photo,
                           TeacherID = item.TeacherID,
                           SchoolID = item.SchoolID,
                           PickStatus = item.PickStatus == null ? (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                           //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                           Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                           Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                           PickerID = item.PickerID == null ? 0 : item.PickerID,
                           PickDate = item.PickDate == null ? null : item.PickDate,
                           PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                           PickerImage = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                           ClassID = item.ClassID,
                           OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                           AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                           CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                           ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                       }).ToList();
            if (ClassID != 0)
            {
                ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                {
                    List<MViewStudentPickUp> Lists = new List<MViewStudentPickUp>();
                    var list = objList.Where(p => p.PickStatus == "Send to Office" || p.OfficeFlag).ToList();
                    foreach (var item in list)
                    {
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
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
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "After-School" || p.PickStatus == "After-School-Ex")).Count() <= 0 ||
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
                        if (objList.Where(p => p.StudentID == item.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Send to Office" || p.PickStatus == "Club")).Count() <= 0 ||
                            objList.Where(p => p.StudentID == item.StudentID && p.PickStatus == "Picked" && p.AfterSchoolFlag).Count() == 1)
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
                    //    var list = objList.Where(p => p.SchoolID == ObjClass.SchoolID).ToList();
                    //    foreach (var item in list)
                    //    {
                    //        if (objList.Where(p => p.StudentID == item.StudentID && p.StartDates == dt.ToString("dd/MM/yyyy")).Count() > 0)
                    //        {
                    //            Lists.Add(item);
                    //        }
                    //    }
                    //    objList = Lists;
                    //}
                    //else
                    {
                        objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Send to Office").ToList();
                        //objList = objList.Where(p => p.ClassID == ClassID && p.PickStatus != "Send to Office" && p.PickStatus != "After-School" && p.PickStatus != "After-School-Ex").ToList();
                    }
                }
            }

            List<int> studentIdList = new List<int>();
            studentIdList = objList.Select(j => j.StudentID.Value).ToList();

            return studentIdList;
        }

        public List<MISTeacherClassAssignment> GetClassTeachers(int ClassID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISTeacherClassAssignment> objlist = (from item in DB.ISTeacherClassAssignments.Where(p => p.Deleted == true && p.Active == true && p.ClassID == ClassID).ToList()
                                                       select new MISTeacherClassAssignment
                                                       {
                                                           ID = item.ID,
                                                           ClassID = item.ClassID,
                                                           TeacherID = item.TeacherID,
                                                           ClassName = item.ISClass.Name,
                                                           TeacherName = item.ISTeacher.Name
                                                       }).ToList();
            return objlist;
        }

        //public void CheckEndDated()
        //{
        //    SchoolAppEntities DB = new SchoolAppEntities();
        //    DateTime dt = DateTime.Now;
        //    List<ISTeacher> ObjTeacher = DB.ISTeachers.Where(p => DbFunctions.TruncateTime(p.EndDate) <= DbFunctions.TruncateTime(dt) && p.Active == true).ToList();
        //    foreach (var item in ObjTeacher)
        //    {
        //        item.Active = false;
        //        DB.SaveChanges();
        //        List<ISTeacherClassAssignment> ObjTAssi = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == item.ID && p.Active == true).ToList();
        //        foreach (var items in ObjTAssi)
        //        {
        //            items.Active = false;
        //            DB.SaveChanges();
        //        }
        //    }
        //    List<ISClass> ObjClass = DB.ISClasses.Where(p => DbFunctions.TruncateTime(p.EndDate) <= DbFunctions.TruncateTime(dt) && p.Active == true).ToList();
        //    foreach (var item in ObjClass)
        //    {
        //        item.Active = false;
        //        DB.SaveChanges();
        //        List<ISTeacherClassAssignment> ObjTAssign = DB.ISTeacherClassAssignments.Where(p => p.ClassID == item.ID && p.Active == true).ToList();
        //        foreach (var items in ObjTAssign)
        //        {
        //            items.Active = false;
        //            DB.SaveChanges();
        //        }
        //    }
        //}
        public List<MISStudent> StudentFullListByClass(int SchoolID, int ClassID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            ClassID = item.ClassID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            Photo = item.Photo,
                                            ClassName = item.ISClass.Name,
                                            ParantName1 = item.ParantName1,
                                            ParantEmail1 = item.ParantEmail1,
                                            ParantPhone1 = item.ParantPhone1,
                                            ParantRelation1 = item.ParantRelation1,
                                            ParantName2 = item.ParantName2,
                                            ParantEmail2 = item.ParantEmail2,
                                            ParantPhone2 = item.ParantPhone2,
                                            ParantRelation2 = item.ParantRelation2,
                                            EmailAfterConfirmAttendence = item.EmailAfterConfirmAttendence,
                                            EmailAfterConfirmPickUp = item.EmailAfterConfirmPickUp,
                                        }).ToList();
            if (ClassID != 0)
            {
                objList = objList.Where(p => p.ClassID == ClassID).ToList();
            }
            return objList;
        }
        public List<MISStudent> StudentFullListByExtClass(int SchoolID, int ClassID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now;
            List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.Deleted == true && p.Active == true).ToList()
                                        join items in DB.ISPickups.Where(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && (p.PickStatus == "After-School-Ex") && p.ISStudent.SchoolID == SchoolID).ToList() on item.ID equals items.StudentID
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            ClassID = item.ClassID,
                                            StudentName = item.StudentName,
                                            StudentNo = item.StudentNo,
                                            Photo = item.Photo,
                                            ClassName = item.ISClass.Name,
                                            ParantName1 = item.ParantName1,
                                            ParantEmail1 = item.ParantEmail1,
                                            ParantPhone1 = item.ParantPhone1,
                                            ParantRelation1 = item.ParantRelation1,
                                            ParantName2 = item.ParantName2,
                                            ParantEmail2 = item.ParantEmail2,
                                            ParantPhone2 = item.ParantPhone2,
                                            ParantRelation2 = item.ParantRelation2,
                                            EmailAfterConfirmAttendence = item.EmailAfterConfirmAttendence,
                                            EmailAfterConfirmPickUp = item.EmailAfterConfirmPickUp,
                                        }).ToList();
            return objList;
        }

    }
}
