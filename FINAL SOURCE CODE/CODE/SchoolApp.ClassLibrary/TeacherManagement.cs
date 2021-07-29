using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SchoolApp.ClassLibrary
{
    public class TeacherManagement
    {
        public List<MISTeacher> TeacherList(int SchoolID, string Year, int ClassID, string TeacherName, string OrderBy, string SortBy, int classTypeID, string Status)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<ISTeacher> objList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == SchoolID && p.Deleted == true).ToList();

            if (Year != "")
            {
                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ISClass.Year == Year)).ToList();
                //objList = objList.Where(p => p.ISClass.Year == Year).ToList();
            }
            if (classTypeID != 0)
            {
                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ISClass.TypeID == classTypeID)).ToList();
                //objList = objList.Where(p => p.ISClass.Year == Year).ToList();
            }

            if (ClassID != 0)
            {
                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ClassID == ClassID)).ToList();
            }
            if (TeacherName != "")
            {
                objList = objList.Where(p => p.Name.Trim().ToLower().Contains(TeacherName.Trim().ToLower())).ToList();
            }
            if (OrderBy == "ASC")
            {

                if (SortBy == "IdNo")
                {
                    objList = objList.OrderBy(p => p.TeacherNo).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.Name).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    objList = objList.OrderBy(p => p.ISUserRole.RoleName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.CreatedDateTime).ToList();
                }
            }
            else
            {
                if (SortBy == "IdNo")
                {
                    objList = objList.OrderByDescending(p => p.TeacherNo).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.Name).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    objList = objList.OrderByDescending(p => p.ISUserRole.RoleName).ToList();
                }

                else
                {
                    objList = objList.OrderByDescending(p => p.CreatedDateTime).ToList();
                }
            }
            if (Status != "0")
            {
                if (Status == "1")
                {
                    objList = objList.Where(p => p.Active == true).ToList();
                }
                else
                {
                    objList = objList.Where(p => p.Active == false).ToList();
                }
            }
            List<MISTeacher> objList1 = (from Obj in objList
                                         select new MISTeacher
                                         {
                                             ID = Obj.ID,
                                             Name = Obj.Name,
                                             TeacherNo = Obj.TeacherNo,
                                             Title = Obj.Title,
                                             Photo = Obj.Photo,
                                             PhoneNo = Obj.PhoneNo,
                                             Email = Obj.Email,
                                             TeacherEndDate = Obj.EndDate != null ? Obj.EndDate.Value.ToString("dd/MM/yyyy") : "",
                                             SchoolID = Obj.SchoolID,
                                             RoleID = Obj.RoleID,
                                             ClassID = Obj.ClassID,
                                             ClassName = (Obj.ClassID != null && Obj.ClassID != 0) ? DB.ISClasses.FirstOrDefault(p => p.ID == Obj.ClassID)?.Name : "",
                                             RoleName = Obj.ISUserRole.RoleName,
                                             AssignedClass = GetClassAssignedTeacher(Obj.ID),
                                             AssignedClassYear = GetClassYearAssignedTeacher(Obj.ID),
                                             CreatedDateTime = Obj.CreatedDateTime,
                                             ClassName1 = TeacherClassAssignment(Obj.ID).Count >= 1 ? TeacherClassAssignment(Obj.ID)[0].ClassName : "",
                                             ClassName2 = TeacherClassAssignment(Obj.ID).Count >= 2 ? TeacherClassAssignment(Obj.ID)[1].ClassName : "",
                                             ClassName3 = TeacherClassAssignment(Obj.ID).Count >= 3 ? TeacherClassAssignment(Obj.ID)[2].ClassName : "",
                                             ClassName4 = TeacherClassAssignment(Obj.ID).Count >= 4 ? TeacherClassAssignment(Obj.ID)[3].ClassName : "",
                                             ClassName5 = TeacherClassAssignment(Obj.ID).Count >= 5 ? TeacherClassAssignment(Obj.ID)[4].ClassName : "",
                                             CreateDate = Obj.CreatedDateTime != null ? Obj.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "",
                                             Active = Obj.Active,
                                             Status = Obj.Active == true ? "Active" : "InActive",
                                             CreateBy = Obj.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.School ? DB.ISSchools.SingleOrDefault(p => p.ID == Obj.CreatedBy).AdminFirstName + " " + DB.ISSchools.SingleOrDefault(p => p.ID == Obj.CreatedBy).AdminLastName : DB.ISTeachers.SingleOrDefault(p => p.ID == Obj.CreatedBy).Name,
                                         }).ToList();


            return objList1;
        }

        public string GetClassAssignedTeacher(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            try
            {
                var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Active == true && p.Active == true).ToList();
                if (obj.Count > 0)
                {
                    foreach (var items in obj)
                    {
                        Class += items.ISClass.Name + ", ";
                    }
                    ClassName = Class.Remove(Class.Length - 2);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return ClassName;
        }
        public string GetEndDatedClassAssignedTeacher(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            try
            {
                var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID).ToList();
                if (obj.Count > 0)
                {
                    foreach (var items in obj)
                    {
                        Class += items.ISClass.Name + ", ";
                    }
                    ClassName = Class.Remove(Class.Length - 2);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return ClassName;
        }
        public string GetClassYearAssignedTeacher(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            try
            {
                var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Active == true && p.Active == true).ToList();
                if (obj.Count > 0)
                {
                    foreach (var items in obj)
                    {
                        ISClassYear ObjList = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == items.ISClass.Year);
                        Class += (ObjList != null ? ObjList.Year : "") + (ObjList != null ? "," : "");
                    }
                    ClassName = Class.Length != 0 ? Class.Remove(Class.Length - 1) : Class;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return ClassName;
        }
        public string GetEndDatedClassYearAssignedTeacher(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            try
            {
                var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID).ToList();
                if (obj.Count > 0)
                {
                    foreach (var items in obj)
                    {
                        ISClassYear ObjList = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == items.ISClass.Year);
                        Class += (ObjList != null ? ObjList.Year : "") + (ObjList != null ? "," : "");
                    }
                    ClassName = Class.Length != 0 ? Class.Remove(Class.Length - 1) : Class;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return ClassName;
        }
        public MISTeacher TeacherLogin(string Email, string Password)
        {
            string password = EncryptionHelper.Encrypt(Password);
            SchoolAppEntities DB = new SchoolAppEntities();
            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.Email == Email && p.Password == password && p.Active == true && p.Deleted == true);
            if (Obj != null)
            {
                MISTeacher ObjM = new MISTeacher
                {
                    ID = Obj.ID,
                    Name = Obj.Name,
                    TeacherNo = Obj.TeacherNo,
                    Title = Obj.Title,
                    Photo = Obj.Photo,
                    PhoneNo = Obj.PhoneNo,
                    Email = Obj.Email,
                    TeacherEndDate = Obj.EndDate != null ? Obj.EndDate.Value.ToShortDateString() : "",
                    SchoolID = Obj.SchoolID,
                    RoleID = Obj.RoleID,
                    ClassID = Obj.ClassID,
                    ClassName = (Obj.ClassID != null && Obj.ClassID != 0) ? DB.ISClasses.SingleOrDefault(p => p.ID == Obj.ClassID).Name : "",
                    RoleName = Obj.ISUserRole.RoleName,
                    Role = Obj.Role,
                    RoleType = Obj.Role == 1 ? "Teaching Role" : "Non Teaching Role",
                    SchoolType = Obj.ISSchool.TypeID,
                    ISActivated = Obj.ISActivated,
                    IsActivationID = Obj.IsActivationID,
                    MemorableQueAnswer = Obj.MemorableQueAnswer
                };
                return ObjM;
            }
            else
            {
                return null;
            }

        }

        public List<MISTeacherClassAssignment> TeacherClassAssignment(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISTeacherClassAssignment> objlist = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.Active == true).ToList()
                                                       join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                                                       select new MISTeacherClassAssignment
                                                       {
                                                           ID = item.ID,
                                                           ClassID = item.ClassID,
                                                           TeacherID = item.TeacherID,
                                                           ClassName = item2.Name,
                                                           SchoolID = item2.SchoolID,
                                                           ClassTypeID = item2.TypeID,
                                                       }).ToList();
            return objlist;

        }
        public MISTeacher GetTeacher(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
            List<ISClass> objClassList = DB.ISClasses.Where(p => p.SchoolID == Obj.SchoolID && p.Deleted == true && p.Active == true).ToList();
            List<ISTeacherClassAssignment> ObjList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
            MISTeacher objMISTeacher = new MISTeacher();
            if (Obj != null)
            {
                objMISTeacher.ID = Obj.ID;
                objMISTeacher.TeacherNo = Obj.TeacherNo;
                objMISTeacher.Name = Obj.Name;
                objMISTeacher.Title = Obj.Title;
                objMISTeacher.Role = Obj.Role;
                objMISTeacher.RoleName = Obj.ISUserRole.RoleName;
                objMISTeacher.Email = Obj.Email;
                objMISTeacher.PhoneNo = Obj.PhoneNo;
                objMISTeacher.Photo = Obj.Photo;
                objMISTeacher.EndDate = Obj.EndDate;
                objMISTeacher.ClassName = GetTeacherAssignedClasses(Obj.ID) != "" ? GetTeacherAssignedClasses(Obj.ID) : "No Class assigned to this Teacher";
                objMISTeacher.Password = EncryptionHelper.Decrypt(Obj.Password);
                objMISTeacher.TeacherEndDate = Obj.EndDate != null ? Obj.EndDate.Value.ToString("dd/MM/yyyy") : "";
                objMISTeacher.RoleID = Obj.RoleID;
                objMISTeacher.Active = Obj.Active;
                objMISTeacher.Status = Obj.Active == true ? "Active" : "InActive";
                objMISTeacher.ClassID1 = ObjList.Count >= 1 ? ObjList[0].ClassID : 0;
                objMISTeacher.ClassID2 = ObjList.Count >= 2 ? ObjList[1].ClassID : 0;
                objMISTeacher.ClassID3 = ObjList.Count >= 3 ? ObjList[2].ClassID : 0;
                objMISTeacher.ClassID4 = ObjList.Count >= 4 ? ObjList[3].ClassID : 0;
                objMISTeacher.ClassID5 = ObjList.Count >= 5 ? ObjList[4].ClassID : 0;
                try
                {
                    objMISTeacher.ClassName1 = ObjList.Count >= 1 ? objClassList.SingleOrDefault(p => p.ID == ObjList[0].ClassID).Name : "";
                    objMISTeacher.ClassName2 = ObjList.Count >= 2 ? objClassList.SingleOrDefault(p => p.ID == ObjList[1].ClassID).Name : "";
                    objMISTeacher.ClassName3 = ObjList.Count >= 3 ? objClassList.SingleOrDefault(p => p.ID == ObjList[2].ClassID).Name : "";
                    objMISTeacher.ClassName4 = ObjList.Count >= 4 ? objClassList.SingleOrDefault(p => p.ID == ObjList[3].ClassID).Name : "";
                    objMISTeacher.ClassName5 = ObjList.Count >= 5 ? objClassList.SingleOrDefault(p => p.ID == ObjList[4].ClassID).Name : "";
                }
                catch (Exception ex)
                {
                    ErrorLogManagement.AddLog(ex);
                }
            }
            return objMISTeacher;
        }
        public string GetTeacherAssignedClasses(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            try
            {
                var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Active == true && p.Active == true).ToList();
                if (obj.Count > 0)
                {
                    foreach (var items in obj)
                    {
                        Class += items.ISClass.Name + ", ";
                    }
                    ClassName = Class.Remove(Class.Length - 2);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return ClassName;
        }
        public List<MISTeacher> NonTeacherList(int SchoolID, string TeacherName, string OrderBy, string SortBy)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<ISTeacher> objList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.SchoolID == SchoolID && p.Deleted == true).ToList();

            if (TeacherName != "")
            {
                objList = objList.Where(p => p.Name.Trim().ToLower().Contains(TeacherName.Trim().ToLower())).ToList();
            }
            if (OrderBy == "ASC")
            {

                if (SortBy == "IdNo")
                {
                    objList = objList.OrderBy(p => p.TeacherNo).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.Name).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    objList = objList.OrderBy(p => p.ISUserRole.RoleName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.CreatedDateTime).ToList();
                }
            }
            else
            {
                if (SortBy == "IdNo")
                {
                    objList = objList.OrderByDescending(p => p.TeacherNo).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.Name).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    objList = objList.OrderByDescending(p => p.ISUserRole.RoleName).ToList();
                }

                else
                {
                    objList = objList.OrderByDescending(p => p.CreatedDateTime).ToList();
                }
            }

            List<MISTeacher> objList1 = (from Obj in objList
                                         select new MISTeacher
                                         {
                                             ID = Obj.ID,
                                             Name = Obj.Name,
                                             TeacherNo = Obj.TeacherNo,
                                             Title = Obj.Title,
                                             Photo = Obj.Photo,
                                             PhoneNo = Obj.PhoneNo,
                                             Email = Obj.Email,
                                             TeacherEndDate = Obj.EndDate != null ? Obj.EndDate.Value.ToString("dd/MM/yyyy") : "",
                                             SchoolID = Obj.SchoolID,
                                             RoleID = Obj.RoleID,
                                             RoleName = Obj.ISUserRole.RoleName,
                                             Active = Obj.Active,
                                             Status = Obj.Active == true ? "Active" : "InActive",
                                             CreatedDateTime = Obj.CreatedDateTime,
                                             ClassID1 = TeacherClassAssignment(Obj.ID).Count >= 1 ? TeacherClassAssignment(Obj.ID)[0].ClassID : null,
                                             ClassID2 = TeacherClassAssignment(Obj.ID).Count >= 2 ? TeacherClassAssignment(Obj.ID)[1].ClassID : null,
                                             ClassName1 = TeacherClassAssignment(Obj.ID).Count >= 1 ? TeacherClassAssignment(Obj.ID)[0].ClassName : "",
                                             ClassName2 = TeacherClassAssignment(Obj.ID).Count >= 2 ? TeacherClassAssignment(Obj.ID)[1].ClassName : "",
                                             CreateBy = Obj.CreatedByType != (int?)null ? Obj.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.School ? DB.ISSchools.SingleOrDefault(p => p.ID == Obj.CreatedBy).Name : DB.ISTeachers.SingleOrDefault(p => p.ID == Obj.CreatedBy).Name : "",
                                         }).ToList();


            return objList1;
        }
        public MISTeacher GetNonTeacher(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
            List<ISClass> objClassList = DB.ISClasses.Where(p => p.SchoolID == Obj.SchoolID && p.Deleted == true && p.Active == true).ToList();
            List<ISTeacherClassAssignment> ObjList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
            MISTeacher objMISTeacher = new MISTeacher();
            if (Obj != null)
            {
                objMISTeacher.ID = Obj.ID;
                objMISTeacher.TeacherNo = Obj.TeacherNo;
                objMISTeacher.Name = Obj.Name;
                objMISTeacher.Title = Obj.Title;
                objMISTeacher.Role = Obj.Role;
                objMISTeacher.RoleName = Obj.ISUserRole.RoleName;
                objMISTeacher.Email = Obj.Email;
                objMISTeacher.PhoneNo = Obj.PhoneNo;
                objMISTeacher.Photo = Obj.Photo;
                objMISTeacher.EndDate = Obj.EndDate;
                objMISTeacher.Status = Obj.Active == true ? "Active" : "InActive";
                objMISTeacher.Active = Obj.Active;
                objMISTeacher.Password = EncryptionHelper.Decrypt(Obj.Password);
                objMISTeacher.TeacherEndDate = Obj.EndDate != null ? Obj.EndDate.Value.ToString("dd/MM/yyyy") : "";
                objMISTeacher.RoleID = Obj.RoleID;
                objMISTeacher.ClassName = GetTeacherAssignedClasses(Obj.ID) != "" ? GetTeacherAssignedClasses(Obj.ID) : "No Class assigned to this Non-Teacher";
                objMISTeacher.ClassID1 = ObjList.Count >= 1 ? ObjList[0].ClassID : 0;
                objMISTeacher.ClassID2 = ObjList.Count >= 2 ? ObjList[1].ClassID : 0;
                objMISTeacher.ClassName1 = ObjList.Count >= 1 ? objClassList.SingleOrDefault(p => p.ID == ObjList[0].ClassID).Name : "";
                objMISTeacher.ClassName2 = ObjList.Count >= 2 ? objClassList.SingleOrDefault(p => p.ID == ObjList[1].ClassID).Name : "";
            }
            return objMISTeacher;
        }

        //Report Related Functions
        public List<MISTeacher> AllTeacherList(int SchoolID, string Year, int ClassID, string TeacherName, string OrderBy, string SortBy, int classTypeID, string Status)
        {
            SchoolAppEntities DB = new SchoolAppEntities();

            List<ISTeacher> objList = DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == SchoolID && p.Deleted == true).ToList();

            if (Year != "")
            {
                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ISClass.Year == Year)).ToList();
                //objList = objList.Where(p => p.ISClass.Year == Year).ToList();
            }
            if (classTypeID != 0)
            {
                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ISClass.TypeID == classTypeID)).ToList();
                //objList = objList.Where(p => p.ISClass.Year == Year).ToList();
            }

            if (ClassID != 0)
            {
                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ClassID == ClassID)).ToList();
            }
            if (TeacherName != "")
            {
                objList = objList.Where(p => p.Name.Trim().ToLower().Contains(TeacherName.Trim().ToLower())).ToList();
            }
            if (OrderBy == "ASC")
            {

                if (SortBy == "IdNo")
                {
                    objList = objList.OrderBy(p => p.TeacherNo).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderBy(p => p.Name).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    objList = objList.OrderBy(p => p.ISUserRole.RoleName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.CreatedDateTime).ToList();
                }
            }
            else
            {
                if (SortBy == "IdNo")
                {
                    objList = objList.OrderByDescending(p => p.TeacherNo).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    objList = objList.OrderByDescending(p => p.Name).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    objList = objList.OrderByDescending(p => p.ISUserRole.RoleName).ToList();
                }

                else
                {
                    objList = objList.OrderByDescending(p => p.CreatedDateTime).ToList();
                }
            }
            if (Status != "0")
            {
                if (Status == "1")
                {
                    objList = objList.Where(p => p.Active == true).ToList();
                }
                else
                {
                    objList = objList.Where(p => p.Active == false).ToList();
                }
            }
            List<MISTeacher> objList1 = (from Obj in objList
                                         select new MISTeacher
                                         {
                                             ID = Obj.ID,
                                             Name = Obj.Name,
                                             TeacherNo = Obj.TeacherNo,
                                             Title = Obj.Title,
                                             Photo = Obj.Photo,
                                             PhoneNo = Obj.PhoneNo,
                                             Email = Obj.Email,
                                             EndDate = Obj.EndDate,
                                             TeacherEndDate = Obj.ModifyDateTime != null ? Obj.ModifyDateTime.Value.ToString("dd/MM/yyyy") : "",
                                             SchoolID = Obj.SchoolID,
                                             RoleID = Obj.RoleID,
                                             ClassID = Obj.ClassID,
                                             ClassName = (Obj.ClassID != null && Obj.ClassID != 0) ? DB.ISClasses.SingleOrDefault(p => p.ID == Obj.ClassID).Name : "",
                                             RoleName = Obj.ISUserRole.RoleName,
                                             AssignedClass = GetEndDatedClassAssignedTeacher(Obj.ID),
                                             AssignedClassYear = GetEndDatedClassYearAssignedTeacher(Obj.ID),
                                             CreatedDateTime = Obj.CreatedDateTime,
                                             ClassName1 = TeacherClassAssignment(Obj.ID).Count >= 1 ? TeacherClassAssignment(Obj.ID)[0].ClassName : "",
                                             ClassName2 = TeacherClassAssignment(Obj.ID).Count >= 2 ? TeacherClassAssignment(Obj.ID)[1].ClassName : "",
                                             ClassName3 = TeacherClassAssignment(Obj.ID).Count >= 3 ? TeacherClassAssignment(Obj.ID)[2].ClassName : "",
                                             ClassName4 = TeacherClassAssignment(Obj.ID).Count >= 4 ? TeacherClassAssignment(Obj.ID)[3].ClassName : "",
                                             ClassName5 = TeacherClassAssignment(Obj.ID).Count >= 5 ? TeacherClassAssignment(Obj.ID)[4].ClassName : "",
                                             CreateDate = Obj.CreatedDateTime != null ? Obj.CreatedDateTime.Value.ToString("dd/MM/yyyy") : "",
                                             Active = Obj.Active,
                                             Status = Obj.Active == true ? "Active" : "InActive",
                                             CreateBy = Obj.CreatedByType == (int)EnumsManagement.CREATEBYTYPE.School ? DB.ISSchools.SingleOrDefault(p => p.ID == Obj.CreatedBy).Name : DB.ISTeachers.SingleOrDefault(p => p.ID == Obj.CreatedBy).Name,
                                         }).ToList();


            return objList1;
        }

        public string GetClassAssignedAllTeacher(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID).ToList();
            if (obj.Count > 0)
            {
                foreach (var items in obj)
                {
                    Class += items.ISClass.Name + ", ";
                }
                ClassName = Class.Remove(Class.Length - 2);
            }
            return ClassName;
        }
        public string GetClassYearAssignedAllTeacher(int TeacherID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            string Class = "";
            string ClassName = "";
            var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID).ToList();
            if (obj.Count > 0)
            {
                foreach (var items in obj)
                {
                    ISClassYear ObjList = DB.ISClassYears.SingleOrDefault(p => p.ID.ToString() == items.ISClass.Year);
                    Class += (ObjList != null ? ObjList.Year : "") + (ObjList != null ? "," : "");
                }
                ClassName = Class.Length != 0 ? Class.Remove(Class.Length - 1) : Class;
            }
            return ClassName;
        }
        public void TeacherReassignment(int SchoolID, int TeacherID, int? OldClassID, int NewClassID, int UserType, int SenderID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            if (OldClassID != NewClassID)
            {
                ISTeacherReassignHistory ObjReassign = new ISTeacherReassignHistory();
                ObjReassign.SchoolID = SchoolID;
                ObjReassign.FromClass = OldClassID;
                ObjReassign.ToClass = NewClassID;
                ObjReassign.Date = DateTime.Now;
                ObjReassign.TeacherID = TeacherID;
                ObjReassign.Active = true;
                ObjReassign.Deleted = true;
                ObjReassign.CreatedBy = SenderID;
                ObjReassign.CreatedByType = UserType;
                ObjReassign.CreatedDateTime = DateTime.Now;
                DB.ISTeacherReassignHistories.Add(ObjReassign);
                DB.SaveChanges();
            }
        }

        public void ChangePasswordEmailManage(ISTeacher _Teacher)
        {
            EmailManagement _EmailManagement = new EmailManagement();
            SchoolAppEntities DB = new SchoolAppEntities();
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblTeacherBody = string.Empty;

            tblTeacherBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Teacher.Name + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Your password to account " + _Teacher.Email + @" has been updated by " + _School.Name + @"
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ammendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        New Password : " + EncryptionHelper.Decrypt(_Teacher.Password) + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You can see after Login and in your Profile which changes done by School. 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        You will be taken to the login page by clicking on this link. <a href=" + WebConfigurationManager.AppSettings["LoginURL"].ToString() + @">Click Here</a> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Instructions of useful FAQs or on how to used the app effctively is attached to this email or can be obtained electronically by clicking on this link.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        We are on hand to help with queries should you have them and you can email us at support@learningstreet.co.uk with a question. We plan to respond to every question within 24 hours. Please note that we will only be able to respond to the email provided to school for registrating this account.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        For Further enquiries, please contact School Administrator
                                   </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                TeacherBody += reader.ReadToEnd();
            }
            TeacherBody = TeacherBody.Replace("{Body}", tblTeacherBody);

            _EmailManagement.SendEmail(_Teacher.Email, "Change Password Notification", TeacherBody);
        }
    }
}
