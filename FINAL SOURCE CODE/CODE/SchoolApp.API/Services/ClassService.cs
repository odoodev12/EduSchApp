using Newtonsoft.Json;
using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Services
{
    public class ClassService
    {
        public SchoolAppEntities entity;

        public ClassService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region Class  
        public ReturnResponce GetClassTypeList()
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClassType> objLists = objClassManagement.ClassTypeList();

                return new ReturnResponce(objLists, EntityJsonIgnore.ClassTypeIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClassTypeListBySchoolType(int SchoolTypeId)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClassType> objLists = objClassManagement.ClassTypeListSchoolWise(SchoolTypeId);

                return new ReturnResponce(objLists, EntityJsonIgnore.ClassTypeIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClassList(int SchoolId, string year = null, int ClassTypeId = 0)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> responce = objClassManagement.ClassList(SchoolId, year, ClassTypeId);

                return new ReturnResponce(responce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClassListByFilter(int SchoolID, string Year, int ClassTypeId, string ClassName, string Status, bool IsAscending, string SortBy = "Date")
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> responce = objClassManagement.ClassListByFilter(SchoolID, Year, ClassTypeId, Status);


                if (ClassName != "")
                {
                    responce = responce.Where(w => w.Name.ToLower().Contains(ClassName.Trim().ToLower())).ToList();
                }


                if (SortBy.ToLower() == "Date".ToLower())
                {

                    if (IsAscending)
                        responce = responce.OrderBy(o => o.CreatedDateTime).ToList();
                    else
                        responce = responce.OrderByDescending(o => o.CreatedDateTime).ToList();

                }
                else if (SortBy.ToLower() == "IdNo".ToLower())
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.ID).ToList();
                    else
                        responce = responce.OrderByDescending(o => o.ID).ToList();
                }
                else if (SortBy.ToLower() == "TeacherName".ToLower())
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.TeacherName).ToList();
                    else
                        responce = responce.OrderByDescending(o => o.TeacherName).ToList();
                }
                else if (SortBy.ToLower() == "RoleName".ToLower())
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.Name).ToList();
                    else
                        responce = responce.OrderBy(o => o.Name).ToList();
                }
                else if (SortBy.ToLower() == "ClassType".ToLower())
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.ClassType).ToList();
                    else
                        responce = responce.OrderBy(o => o.ClassType).ToList();
                }
                else if (SortBy.ToLower() == "ClassName".ToLower())
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.Name).ToList();
                    else
                        responce = responce.OrderBy(o => o.Name).ToList();
                }

                return new ReturnResponce(responce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetClassList(int SchoolId, int TeacherId, string year, int ClassTypeId)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> responce = objClassManagement.ClassList(SchoolId, year, ClassTypeId);


                var classList = entity.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherId && p.ISClass.Active == true && p.Active == true).Select(s => s.ClassID).ToList();
                List<MISClass> Filterresponce = classList.Count > 0 ? responce.Where(w => classList.Any(c => c == w.ID)).ToList() : new List<MISClass>();

                return new ReturnResponce(Filterresponce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClassListByFilter(int SchoolID, int TeacherId, string Year, int ClassTypeId, string ClassName, string Status, bool IsAscending, string SortBy = "Date")
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> Serviceresponce = objClassManagement.ClassListByFilter(SchoolID, Year, ClassTypeId, Status);

                var classList = entity.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherId && p.ISClass.Active == true && p.Active == true).Select(s => s.ClassID).ToList();
                List<MISClass> responce = classList.Count > 0 ?  Serviceresponce.Where(w => classList.Any(c => c == w.ID)).ToList() : new List<MISClass>();

                if (ClassName != "")
                {
                    responce = responce.Where(w => w.Name.ToLower().Contains(ClassName.Trim().ToLower())).ToList();
                }


                if (SortBy == "Date")
                {

                    if (IsAscending)
                        responce = responce.OrderBy(o => o.CreatedDateTime).ToList();
                    else
                        responce = responce.OrderByDescending(o => o.CreatedDateTime).ToList();

                }
                else if (SortBy == "IdNo")
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.ID).ToList();
                    else
                        responce = responce.OrderByDescending(o => o.ID).ToList();
                }
                else if (SortBy == "TeacherName")
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.TeacherName).ToList();
                    else
                        responce = responce.OrderByDescending(o => o.TeacherName).ToList();
                }
                else if (SortBy == "RoleName")
                {
                    if (IsAscending)
                        responce = responce.OrderBy(o => o.Name).ToList();
                    else
                        responce = responce.OrderBy(o => o.Name).ToList();
                }

                return new ReturnResponce(responce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClass(int ClassID)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                MISClass response = objClassManagement.GetClass(ClassID);

                return new ReturnResponce(response, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce AddClass(int SchoolID, string ClassName, string Year, int ClassTypeID, string AfterSchoolType, string ExtOrganisation, bool Active, string EndDate, bool ISNonListed, int CreateType, int LoginUserId)
        {
            try
            {

                List<ISClass> ObjClasses = entity.ISClasses.Where(p => p.Name == ClassName && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                if (ObjClasses.Count > 0)
                {
                    return new ReturnResponce("ClassName already Exist");
                }
                else
                {
                    ISClass obj = new ISClass();
                    if (ClassTypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                    {
                        List<ISClass> ObjClass = entity.ISClasses.Where(p => p.SchoolID == SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool).ToList();
                        if (ObjClass.Count <= 0)
                        {
                            if (AfterSchoolType == "Internal")
                            {
                                obj.Name = ClassName + "(After School)";
                            }
                            else
                            {
                                obj.Name = ClassName + "(After School Ex)";
                            }
                            obj.TypeID = ClassTypeID;
                            obj.AfterSchoolType = AfterSchoolType;
                            obj.ExternalOrganisation = ExtOrganisation;
                            obj.EndDate = new DateTime(2050, 01, 01);
                            obj.SchoolID = SchoolID;
                            obj.Active = true;
                            obj.Deleted = true;
                            obj.CreatedBy = LoginUserId;
                            obj.CreatedDateTime = DateTime.Now;
                            obj.CreatedByType = CreateType;
                            entity.ISClasses.Add(obj);
                            entity.SaveChanges();

                            ISSchool ObjSchools = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                            //LogManagement.AddLogs("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                            return new ReturnResponce(obj, EntityJsonIgnore.ClassesIgnore);

                        }
                        else
                        {
                            return new ReturnResponce("Only One After School is Allowed for a Standard School");
                        }
                    }
                    else
                    {
                        obj.Name = ClassName;
                        if (ClassTypeID == (int)EnumsManagement.CLASSTYPE.Standard)
                        {
                            obj.Year = Year;
                        }
                        obj.TypeID = ClassTypeID;
                        if (EndDate != "")
                        {
                            string dates = EndDate;
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
                            obj.EndDate = dt2.Date;
                        }
                        obj.SchoolID = SchoolID;
                        obj.Active = true;
                        obj.Deleted = true;
                        obj.CreatedBy = LoginUserId;
                        obj.CreatedDateTime = DateTime.Now;
                        obj.CreatedByType = CreateType;
                        entity.ISClasses.Add(obj);
                        entity.SaveChanges();
                        ISSchool ObjSchools = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        //LogManagement.AddLogs("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");

                        return new ReturnResponce(obj, EntityJsonIgnore.ClassesIgnore);
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce UpdateClass(int ID, int SchoolID, string ClassName, string Year, int ClassTypeID, string AfterSchoolType, string ExtOrganisation, bool Active, string EndDate, bool ISNonListed, int CreateType, int LoginUserId)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                if (ID != 0)
                {
                    List<ISClass> ObjClasses = entity.ISClasses.Where(p => p.ID != ID && p.Name == ClassName && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                    if (ObjClasses.Count > 0)
                    {
                        return new ReturnResponce("ClassName already Exist");
                    }
                    else
                    {
                        ISClass obj = entity.ISClasses.SingleOrDefault(p => p.ID == ID && p.SchoolID == SchoolID && p.Deleted == true);
                        if (obj != null)
                        {
                            if (ClassTypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                            {
                                List<ISClass> ObjClass = entity.ISClasses.Where(p => p.ID != ID && p.SchoolID == SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).ToList();
                                if (ObjClass.Count <= 0)
                                {
                                    if (entity.ISStudents.Where(p => p.ClassID == ID && p.Active == true && p.Deleted == true).Count() > 0)
                                    {
                                        if (Active == false)
                                        {
                                            return new ReturnResponce("Class can not be made InActive");
                                        }
                                        else
                                        {
                                            var response = objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                            ISSchool ObjSchools = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                            //LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");

                                            return new ReturnResponce(response, EntityJsonIgnore.ClassesIgnore);
                                        }
                                    }
                                    else
                                    {
                                        var response = objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                        ISSchool ObjSchools = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                        //LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                        return new ReturnResponce(response, EntityJsonIgnore.ClassesIgnore);
                                    }
                                }
                                else
                                {
                                    return new ReturnResponce("Only One After School is Allowed for a Standard School");
                                }
                            }
                            else
                            {
                                if (entity.ISStudents.Where(p => p.ClassID == ID && p.Active == true && p.Deleted == true).Count() > 0)
                                {
                                    if (Active == false)
                                    {
                                        return new ReturnResponce("Class can not be made InActive");
                                    }
                                    else
                                    {
                                        var response = objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                        ISSchool ObjSchools = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                        //LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                        return new ReturnResponce(response, EntityJsonIgnore.ClassesIgnore);
                                    }
                                }
                                else
                                {
                                    var response = objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                    ISSchool ObjSchools = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                    //LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                    return new ReturnResponce(response, EntityJsonIgnore.ClassesIgnore);
                                }
                            }
                        }
                        else
                        {
                            return new ReturnResponce("Class not Found");

                        }
                    }
                }
                else
                {
                    return new ReturnResponce("Class not Updated");
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(ex);
                return new ReturnResponce(ex.Message);
            }
        }

        #endregion

    }
}