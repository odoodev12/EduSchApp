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
    public class AdminService
    {
        public SchoolAppEntities entity;

        public AdminService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region
        public ReturnResponce AdminLogin(string username, string password)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
                {
                    var EncPassword = EncryptionHelper.Encrypt(password);
                    var responce = entity.ISAdminLogins.Where(p => p.Email == username && p.Pass == EncPassword && p.Active == true && p.Deleted == true).ToList();

                    return responce.Count > 0 ? new ReturnResponce(responce, new[] { "" }) : new ReturnResponce("Invalid Username & Password");
                }
                else
                {
                    return new ReturnResponce("Invalid data, Username and password are required fields!");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        #endregion

        #region School List

        public ReturnResponce GetSchoolTypes()
        {
            try
            {
                var responce = entity.ISSchoolTypes.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, new[] { "ISSchools" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetSchool(int SchoolId)
        {
            try
            {
                var responce = (from s in entity.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.ID == SchoolId)
                                select new
                                {
                                    s.ID,
                                    s.CustomerNumber,
                                    s.Name,
                                    s.Number,
                                    s.TypeID,
                                    s.Address1,
                                    s.Address2,
                                    s.Town,
                                    s.CountryID,
                                    s.Logo,
                                    s.AdminFirstName,
                                    s.AdminLastName,
                                    s.AdminEmail,
                                    s.Password,
                                    s.PhoneNumber,
                                    s.Website,
                                    s.SupervisorFirstname,
                                    s.SupervisorLastname,
                                    s.SupervisorEmail,
                                    s.OpningTime,
                                    s.ClosingTime,
                                    s.LateMinAfterClosing,
                                    s.ChargeMinutesAfterClosing,
                                    s.ReportableMinutesAfterClosing,
                                    s.SetupTrainingStatus,
                                    s.SetupTrainingDate,
                                    s.ActivationDate,
                                    s.SchoolEndDate,
                                    s.isAttendanceModule,
                                    s.isNotificationPickup,
                                    s.NotificationAttendance,
                                    s.AttendanceModule,
                                    s.PostCode,
                                    s.BillingAddress,
                                    s.BillingAddress2,
                                    s.BillingPostCode,
                                    s.BillingCountryID,
                                    s.BillingTown,
                                    s.Classfile,
                                    s.Teacherfile,
                                    s.Studentfile,
                                    s.Reportable,
                                    s.PaymentSystems,
                                    s.CustSigned,
                                    s.AccountStatusID,
                                    s.Active,
                                    s.Deleted,
                                    s.CreatedBy,
                                    s.CreatedDateTime,
                                    s.ModifyBy,
                                    s.ModifyDateTime,
                                    s.DeletedBy,
                                    s.DeletedDateTime,
                                    s.CreatedByName,
                                    s.LastUpdatedBy,
                                    s.ActivatedBy,
                                    s.AccountManagerId,
                                    s.MemorableQueAnswer,
                                    s.ISActivated,
                                    s.IsActivationID,
                                    s.IsEmail_Required_For_Create_Class,
                                    s.IsEmail_Required_For_Edit_Class,
                                    s.IsEmail_Required_For_Create_Student,
                                    s.IsEmail_Required_For_Edit_Student,
                                    s.IsEmail_Required_For_Create_Teacher,
                                    s.IsEmail_Required_For_Edit_Teacher,
                                    s.IsEmail_Required_For_Create_Role,
                                    s.IsEmail_Required_For_Edit_Role,
                                    s.IsEmail_Required_For_Create_Holiday,
                                    s.IsEmail_Required_For_Edit_Holiday,
                                    s.IsBell,
                                    TypeName = s.ISSchoolType.Name,
                                    CountryName = s.ISCountry != null ? s.ISCountry.Name : "",
                                    AccountStatusName = s.ISAccountStatu != null ? s.ISAccountStatu.Name : "",
                                }).FirstOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.SchoolIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        public ReturnResponce GetSchoolByType(int SchoolTypeId)
        {
            try
            {
                var responce = (from s in entity.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.TypeID == SchoolTypeId)
                                select new
                                {
                                    s.ID,
                                    s.CustomerNumber,
                                    s.Name,
                                    s.Number,
                                    s.TypeID,
                                    s.Address1,
                                    s.Address2,
                                    s.Town,
                                    s.CountryID,
                                    s.Logo,
                                    s.AdminFirstName,
                                    s.AdminLastName,
                                    s.AdminEmail,
                                    s.Password,
                                    s.PhoneNumber,
                                    s.Website,
                                    s.SupervisorFirstname,
                                    s.SupervisorLastname,
                                    s.SupervisorEmail,
                                    s.OpningTime,
                                    s.ClosingTime,
                                    s.LateMinAfterClosing,
                                    s.ChargeMinutesAfterClosing,
                                    s.ReportableMinutesAfterClosing,
                                    s.SetupTrainingStatus,
                                    s.SetupTrainingDate,
                                    s.ActivationDate,
                                    s.SchoolEndDate,
                                    s.isAttendanceModule,
                                    s.isNotificationPickup,
                                    s.NotificationAttendance,
                                    s.AttendanceModule,
                                    s.PostCode,
                                    s.BillingAddress,
                                    s.BillingAddress2,
                                    s.BillingPostCode,
                                    s.BillingCountryID,
                                    s.BillingTown,
                                    s.Classfile,
                                    s.Teacherfile,
                                    s.Studentfile,
                                    s.Reportable,
                                    s.PaymentSystems,
                                    s.CustSigned,
                                    s.AccountStatusID,
                                    s.Active,
                                    s.Deleted,
                                    s.CreatedBy,
                                    s.CreatedDateTime,
                                    s.ModifyBy,
                                    s.ModifyDateTime,
                                    s.DeletedBy,
                                    s.DeletedDateTime,
                                    s.CreatedByName,
                                    s.LastUpdatedBy,
                                    s.ActivatedBy,
                                    s.AccountManagerId,
                                    s.MemorableQueAnswer,
                                    s.ISActivated,
                                    s.IsActivationID,
                                    s.IsEmail_Required_For_Create_Class,
                                    s.IsEmail_Required_For_Edit_Class,
                                    s.IsEmail_Required_For_Create_Student,
                                    s.IsEmail_Required_For_Edit_Student,
                                    s.IsEmail_Required_For_Create_Teacher,
                                    s.IsEmail_Required_For_Edit_Teacher,
                                    s.IsEmail_Required_For_Create_Role,
                                    s.IsEmail_Required_For_Edit_Role,
                                    s.IsEmail_Required_For_Create_Holiday,
                                    s.IsEmail_Required_For_Edit_Holiday,
                                    s.IsBell,
                                    TypeName = s.ISSchoolType.Name,
                                    CountryName = s.ISCountry != null ? s.ISCountry.Name : "",
                                    AccountStatusName = s.ISAccountStatu != null ? s.ISAccountStatu.Name : "",
                                }).FirstOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.SchoolIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce SetSchool(SchoolOtherEntity school, int AdminId)
        {
            try
            {
                ISSchool insertUpdate = new ISSchool();

                if (school.ID > 0)
                {
                    insertUpdate = entity.ISSchools.Where(w => w.ID == school.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        entity.ISSchools.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        insertUpdate.Name = school.Name;
                        insertUpdate.Website = school.Website;
                        insertUpdate.Logo = school.Logo;
                        insertUpdate.AdminFirstName = school.AdminFirstName;
                        insertUpdate.AdminLastName = school.AdminLastName;
                        insertUpdate.SupervisorFirstname = school.SupervisorFirstname;
                        insertUpdate.SupervisorLastname = school.SupervisorLastname;
                        insertUpdate.SupervisorEmail = school.SupervisorEmail;
                        insertUpdate.OpningTime = school.OpningTime;
                        insertUpdate.ClosingTime = school.ClosingTime;
                        insertUpdate.LateMinAfterClosing = school.LateMinAfterClosing;
                        insertUpdate.ChargeMinutesAfterClosing = school.ChargeMinutesAfterClosing;
                        insertUpdate.PhoneNumber = school.PhoneNumber;
                        insertUpdate.TypeID = school.TypeID;
                        insertUpdate.ChargeMinutesAfterClosing = school.ChargeMinutesAfterClosing;
                        insertUpdate.Address1 = school.Address1;
                        insertUpdate.Address2 = school.Address2;
                        insertUpdate.Town = school.Town;
                        insertUpdate.CountryID = school.CountryID;
                        insertUpdate.PostCode = school.PostCode;

                        insertUpdate.BillingAddress = school.BillingAddress;
                        insertUpdate.BillingAddress2 = school.BillingAddress2;
                        insertUpdate.BillingPostCode = school.BillingPostCode;
                        insertUpdate.BillingTown = school.BillingTown;

                        insertUpdate.ModifyDateTime = school.ModifyDateTime;
                        insertUpdate.ModifyBy = AdminId;

                    }

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, EntityJsonIgnore.SchoolIgnore);
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Invalid model or data, Please try with valid data.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Class  

        public ReturnResponce GetClassTypeList()
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClassType> objLists = objClassManagement.ClassTypeList();

                return new ReturnResponce(objLists, new string[] { });
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

                return new ReturnResponce(objLists, new string[] { });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        public ReturnResponce GetClassList(int SchoolId, string year = null, int TypeId = 0)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> responce = objClassManagement.ClassList(SchoolId, year, TypeId);

                return new ReturnResponce(responce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        public ReturnResponce GetClassListByFilter(int SchoolID, string Year, int typeID, string Status)
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> responce = objClassManagement.ClassListByFilter(SchoolID, Year, typeID, Status);

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

        #region Teacher        

        public ReturnResponce GetTeacherList(int SchoolId, string Year = "", int ClassID = 0, string TeacherName = "", string OrderBy = "", string SortBy = "", int classTypeID = 0, string Status = "")
        {
            try
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();
                List<MISTeacher> responce = objTeacherManagement.TeacherList(SchoolId, Year, ClassID, TeacherName, OrderBy, SortBy, classTypeID, Status);


                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetTeacher(int TeacherId)
        {
            try
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();
                MISTeacher responce = objTeacherManagement.GetTeacher(TeacherId);

                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        public ReturnResponce AddUpdateTeacher(ISTeacher model, int AdminId)
        {
            try
            {
                ISTeacher insertUpdate = new ISTeacher();

                if (model.ID > 0)
                {
                    insertUpdate = entity.ISTeachers.Where(x => x.ID == model.ID && x.Active == true && x.Deleted == true).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.SchoolID = model.SchoolID;
                        insertUpdate.ClassID = model.ClassID;
                        insertUpdate.Role = (int)EnumsManagement.ROLETYPE.TEACHING;
                        insertUpdate.RoleID = model.RoleID;
                        insertUpdate.TeacherNo = model.TeacherNo;
                        insertUpdate.Title = model.Title;
                        insertUpdate.Name = model.Name;
                        insertUpdate.PhoneNo = model.PhoneNo;
                        insertUpdate.Email = model.Email;
                        string Passwords = CommonOperation.GenerateNewRandom();
                        insertUpdate.Password = EncryptionHelper.Encrypt(Passwords);
                        insertUpdate.EndDate = new DateTime(2050, 01, 01);
                        insertUpdate.Photo = "Upload/user.jpg";
                        insertUpdate.Active = true;
                        insertUpdate.Deleted = true;
                        insertUpdate.CreatedBy = AdminId;
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedByType = 1;
                        entity.ISTeachers.Add(insertUpdate);
                        entity.SaveChanges();

                        ISTeacherClassAssignment objClassAssignment = new ISTeacherClassAssignment();
                        objClassAssignment.ClassID = model.ClassID;
                        objClassAssignment.TeacherID = insertUpdate.ID;
                        objClassAssignment.Active = true;
                        objClassAssignment.Deleted = true;
                        objClassAssignment.CreatedBy = AdminId;
                        objClassAssignment.CreatedDateTime = DateTime.Now;
                        objClassAssignment.Out = 0;
                        objClassAssignment.Outbit = false;
                        entity.ISTeacherClassAssignments.Add(objClassAssignment);
                        entity.SaveChanges();
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        insertUpdate.SchoolID = model.SchoolID;
                        insertUpdate.ClassID = model.ClassID;
                        insertUpdate.Role = 1;
                        insertUpdate.RoleID = model.RoleID;
                        insertUpdate.TeacherNo = model.TeacherNo;
                        insertUpdate.Title = model.Title;
                        insertUpdate.Name = model.Name;
                        insertUpdate.PhoneNo = model.PhoneNo;
                        insertUpdate.Email = model.Email;
                        insertUpdate.EndDate = new DateTime(2050, 01, 01);
                        insertUpdate.Photo = "Upload/user.jpg";
                        insertUpdate.Active = true;
                        insertUpdate.Deleted = true;
                        insertUpdate.CreatedBy = AdminId;
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedByType = 1;

                        ISTeacherClassAssignment objClassAssignment = new ISTeacherClassAssignment();
                        objClassAssignment.ClassID = model.ClassID;
                        objClassAssignment.TeacherID = insertUpdate.ID;
                        objClassAssignment.Active = true;
                        objClassAssignment.Deleted = true;
                        objClassAssignment.CreatedBy = AdminId;
                        objClassAssignment.CreatedDateTime = DateTime.Now;
                        objClassAssignment.Out = 0;
                        objClassAssignment.Outbit = false;

                        entity.SaveChanges();
                    }

                    return new ReturnResponce(insertUpdate, EntityJsonIgnore.TeacherIgnore);
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Invalid model or data, Please try with valid data.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        //public ReturnResponce ReAssignTeacher(int SchoolID, int TeacherID, int UserType, int LogInUserId, int[] ClassIds)
        //{

        //    ISTeacher ObjTeachers = entity.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
        //    List<ISTeacherClassAssignment> objClass = entity.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.Active == true).ToList();
        //    string OldClasses = string.Empty;
        //    string NewClasses = string.Empty;

        //    if (objClass.Count > 0)
        //    {
        //        for (int i = 0; i < ClassIds.Length; i++)
        //        {
        //            if (objClass.Count >= 1)
        //            {
        //                int OldClassID  = 
        //                TeacherManagement objTeacherManagement = new TeacherManagement();
        //                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, OldClassID, NewClassID, UserType, LogInUserId);

        //                OldClasses += objClass[0].ISClass.Name + ", ";
        //                NewClasses += ddl1stClass.SelectedItem.Text + ", ";
        //                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, objClass[0].ClassID, Convert.ToInt32(ddl1stClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
        //            }
        //        }

        //        if (ddl1stClass.SelectedValue != "0")
        //        {
                   
        //        }
        //        if (ddl2ndClass.SelectedValue != "0")
        //        {
        //            if (objClass.Count > 1)
        //            {
        //                OldClasses += objClass[1].ISClass.Name + ", ";
        //                NewClasses += ddl2ndClass.SelectedItem.Text + ", ";
        //                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[1].ClassID, Convert.ToInt32(ddl2ndClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
        //            }
        //        }
        //        if (ddl3rdClass.SelectedValue != "0")
        //        {
        //            if (objClass.Count > 2)
        //            {
        //                OldClasses += objClass[2].ISClass.Name + ", ";
        //                NewClasses += ddl3rdClass.SelectedItem.Text + ", ";
        //                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[2].ClassID, Convert.ToInt32(ddl3rdClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
        //            }
        //        }
        //        if (ddl4thClass.SelectedValue != "0")
        //        {
        //            if (objClass.Count > 3)
        //            {
        //                OldClasses += objClass[3].ISClass.Name + ", ";
        //                NewClasses += ddl4thClass.SelectedItem.Text + ", ";
        //                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[3].ClassID, Convert.ToInt32(ddl4thClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
        //            }
        //        }
        //        if (ddl5thClass.SelectedValue != "0")
        //        {
        //            if (objClass.Count > 4)
        //            {
        //                OldClasses += objClass[4].ISClass.Name + ", ";
        //                NewClasses += ddl5thClass.SelectedItem.Text + ", ";
        //                objTeacherManagement.TeacherReassignment(Authentication.SchoolID, ID, objClass[4].ClassID, Convert.ToInt32(ddl5thClass.SelectedValue), Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School, Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID);
        //            }
        //        }
        //        OldClasses = OldClasses.Substring(0, OldClasses.Length - 2);
        //        NewClasses = NewClasses.Substring(0, NewClasses.Length - 2);
        //        DB.ISTeacherClassAssignments.RemoveRange(objClass);
        //        DB.SaveChanges();
        //    }

        //    if (ObjTeachers.Active == true)
        //    {
        //        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
        //        objClass1.ClassID = Convert.ToInt32(ddl1stClass.SelectedValue);
        //        objClass1.TeacherID = ID;
        //        objClass1.Active = true;
        //        objClass1.Deleted = true;
        //        objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //        objClass1.CreatedDateTime = DateTime.Now;
        //        objClass1.Out = 0;
        //        objClass1.Outbit = false;
        //        DB.ISTeacherClassAssignments.Add(objClass1);
        //        DB.SaveChanges();
        //        if (ddl2ndClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
        //            objClass2.ClassID = Convert.ToInt32(ddl2ndClass.SelectedValue);
        //            objClass2.TeacherID = ID;
        //            objClass2.Active = true;
        //            objClass2.Deleted = true;
        //            objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass2.CreatedDateTime = DateTime.Now;
        //            objClass2.Out = 0;
        //            objClass2.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass2);
        //            DB.SaveChanges();
        //            //edited By maharshi @Gatistavam
        //            // Class_item_2_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass2.ClassID).OrderBy(p => p.ID).ToList();

        //        }
        //        if (ddl3rdClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
        //            objClass3.ClassID = Convert.ToInt32(ddl3rdClass.SelectedValue);
        //            objClass3.TeacherID = ID;
        //            objClass3.Active = true;
        //            objClass3.Deleted = true;
        //            objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass3.CreatedDateTime = DateTime.Now;
        //            objClass3.Out = 0;
        //            objClass3.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass3);
        //            DB.SaveChanges();
        //            //edited By maharshi @Gatistavam
        //            // Class_item_3_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass3.ClassID).OrderBy(p => p.ID).ToList();

        //        }
        //        if (ddl4thClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
        //            objClass4.ClassID = Convert.ToInt32(ddl4thClass.SelectedValue);
        //            objClass4.TeacherID = ID;
        //            objClass4.Active = true;
        //            objClass4.Deleted = true;
        //            objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass4.CreatedDateTime = DateTime.Now;
        //            objClass4.Out = 0;
        //            objClass4.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass4);
        //            DB.SaveChanges();
        //            //edited By maharshi @Gatistavam
        //            //Class_item_4_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass4.ClassID).OrderBy(p => p.ID).ToList();

        //        }
        //        if (ddl5thClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
        //            objClass5.ClassID = Convert.ToInt32(ddl5thClass.SelectedValue);
        //            objClass5.TeacherID = ID;
        //            objClass5.Active = true;
        //            objClass5.Deleted = true;
        //            objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass5.CreatedDateTime = DateTime.Now;
        //            objClass5.Out = 0;
        //            objClass5.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass5);
        //            DB.SaveChanges();
        //            //edited By maharshi @Gatistavam
        //            //Class_item_5_ass = DB.ISTeacherClassAssignments.Where(p => p.ClassID == objClass5.ClassID).OrderBy(p => p.ID).ToList();

        //        }
        //    }
        //    else
        //    {

        //        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
        //        objClass1.ClassID = Convert.ToInt32(ddl1stClass.SelectedValue);
        //        objClass1.TeacherID = ID;
        //        objClass1.Active = false;
        //        objClass1.Deleted = false;
        //        objClass1.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //        objClass1.CreatedDateTime = DateTime.Now;
        //        objClass1.Out = 0;
        //        objClass1.Outbit = false;
        //        DB.ISTeacherClassAssignments.Add(objClass1);
        //        DB.SaveChanges();
        //        if (ddl2ndClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
        //            objClass2.ClassID = Convert.ToInt32(ddl2ndClass.SelectedValue);
        //            objClass2.TeacherID = ID;
        //            objClass2.Active = false;
        //            objClass2.Deleted = false;
        //            objClass2.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass2.CreatedDateTime = DateTime.Now;
        //            objClass2.Out = 0;
        //            objClass2.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass2);
        //            DB.SaveChanges();
        //        }
        //        if (ddl3rdClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
        //            objClass3.ClassID = Convert.ToInt32(ddl3rdClass.SelectedValue);
        //            objClass3.TeacherID = ID;
        //            objClass3.Active = false;
        //            objClass3.Deleted = false;
        //            objClass3.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass3.CreatedDateTime = DateTime.Now;
        //            objClass3.Out = 0;
        //            objClass3.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass3);
        //            DB.SaveChanges();
        //        }
        //        if (ddl4thClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
        //            objClass4.ClassID = Convert.ToInt32(ddl4thClass.SelectedValue);
        //            objClass4.TeacherID = ID;
        //            objClass4.Active = false;
        //            objClass4.Deleted = false;
        //            objClass4.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass4.CreatedDateTime = DateTime.Now;
        //            objClass4.Out = 0;
        //            objClass4.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass4);
        //            DB.SaveChanges();
        //        }
        //        if (ddl5thClass.SelectedValue != "0")
        //        {
        //            ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
        //            objClass5.ClassID = Convert.ToInt32(ddl5thClass.SelectedValue);
        //            objClass5.TeacherID = ID;
        //            objClass5.Active = false;
        //            objClass5.Deleted = false;
        //            objClass5.CreatedBy = Authentication.LogginSchool != null ? Authentication.SchoolID : Authentication.LogginTeacher.ID;
        //            objClass5.CreatedDateTime = DateTime.Now;
        //            objClass5.Out = 0;
        //            objClass5.Outbit = false;
        //            DB.ISTeacherClassAssignments.Add(objClass5);
        //            DB.SaveChanges();
        //        }
        //    }
        //    ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
        //    LogManagement.AddLog("Teacher Class Re-Assign Successfully " + "ID : " + ObjTeacher.Name + " Document Category : Teacher", "Teacher");
        //    AlertMessageManagement.ServerMessage(Page, "Teacher Class Re-Assign Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
        //    ReassignEmailManage(ObjTeacher, OldClasses, NewClasses);
        //    Clear();
        //    bindData("", 0, "", "", "", Convert.ToInt32(ddlClassType.SelectedValue), drpStatus.SelectedValue);

        //    try
        //    {
        //        TeacherManagement objTeacherManagement = new TeacherManagement();
        //        objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, OldClassID, NewClassID, UserType, SenderID);
        //        return new ReturnResponce(null, new string[] { });

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ReturnResponce(ex.Message);
        //    }
        //}



        #endregion      

        #region Support

        public ReturnResponce GetSupportList()
        {
            try
            {
                var responce = entity.ISSupports.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.SupportIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetSupport(int SupportId)
        {
            try
            {
                var responce = entity.ISSupports.Where(p => p.Deleted == true && p.Active == true && p.ID == SupportId).FirstOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.SupportIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetSupportList(int SchoolId)
        {
            try
            {
                var responce = entity.ISSupports.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.SupportIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce PostReplay(ReplayMessage model)
        {
            try
            {
                if (model != null)
                {
                    var InsertUpdate = new ISTicketMessage();
                    InsertUpdate.SupportID = model.SupportID;
                    InsertUpdate.SenderID = model.LoggedInUserId;
                    InsertUpdate.Message = model.Message;
                    InsertUpdate.SelectFile = model.SelectFile;
                    InsertUpdate.UserTypeID = model.UserTypeId;
                    entity.ISTicketMessages.Add(InsertUpdate);
                    entity.SaveChanges();
                    return new ReturnResponce(InsertUpdate, new[] { "ISSupport" });
                }
                else
                {
                    return new ReturnResponce("Invalid model data & and Login Detail's");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetOrganizationUsersList(int RoleId)
        {
            try
            {
                var responce = entity.ISOrganisationUsers.Where(p => p.Deleted == true && p.Active == true && p.RoleID == RoleId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.OrganisationUserIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }
        public ReturnResponce GetOrganizationUser(int OrganizationUserId)
        {
            try
            {
                var responce = entity.ISOrganisationUsers.Where(p => p.Deleted == true && p.Active == true && p.ID == OrganizationUserId).SingleOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.OrganisationUserIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }

        public ReturnResponce SetSupport(ISSupport model, int LogedInuser)
        {
            try
            {
                if (model != null)
                {
                    var InsertUpdate = entity.ISSupports.Where(p => p.Active == true && p.Deleted == true && p.ID == model.ID).SingleOrDefault();
                    if (InsertUpdate != null)
                    {
                        InsertUpdate.Priority = model.Priority;
                        if (InsertUpdate.SupportOfficerID != model.SupportOfficerID)
                        {
                            InsertUpdate.AssignBy = LogedInuser.ToString();
                            InsertUpdate.AssignDate = DateTime.Now;
                        }
                        InsertUpdate.SupportOfficerID = model.SupportOfficerID;
                        InsertUpdate.StatusID = model.StatusID;
                        entity.SaveChanges();
                        return new ReturnResponce(InsertUpdate, EntityJsonIgnore.SupportIgnore);
                    }
                    else
                    {
                        return new ReturnResponce("Invalid model data & and Login Detail's");
                    }
                }
                else
                {
                    return new ReturnResponce("Invalid model data & and Login Detail's");
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        #endregion

        #region Payment
        public ReturnResponce GetInvoiceList()
        {
            try
            {
                var responce = entity.ISSchoolInvoices.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.SchoolInvoiceIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }
        public ReturnResponce GetInvoice(int InvoiceId)
        {
            try
            {
                var responce = entity.ISSchoolInvoices.Where(p => p.Deleted == true && p.Active == true && p.ID == InvoiceId).SingleOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.SchoolInvoiceIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }
        public ReturnResponce GetInvoiceList(int Schoolid)
        {
            try
            {
                var responce = entity.ISSchoolInvoices.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == Schoolid).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.SchoolInvoiceIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.ToString());
            }
        }
        public ReturnResponce AddUpdateSchoolInvoice(ISSchoolInvoice iSSchoolInvoice, int UserLoginId, string UserloginName)
        {
            try
            {
                ISSchoolInvoice insertUpdate = new ISSchoolInvoice();

                if (iSSchoolInvoice.ID > 0)
                {
                    insertUpdate = entity.ISSchoolInvoices.Where(w => w.ID == iSSchoolInvoice.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.Active = true;
                        insertUpdate.Deleted = true;
                        insertUpdate.CreatedBy = UserLoginId;
                        insertUpdate.CreatedByName = UserloginName;
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        entity.ISSchoolInvoices.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        if (iSSchoolInvoice.StatusID != insertUpdate.StatusID)
                        {
                            insertUpdate.StatusUpdateBy = UserloginName;
                            insertUpdate.StatusUpdateDate = DateTime.Now;
                        }
                        insertUpdate.StatusID = iSSchoolInvoice.StatusID;
                        insertUpdate.ModifyBy = UserLoginId;
                        insertUpdate.ModifyDateTime = DateTime.Now;
                    }

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, EntityJsonIgnore.SchoolInvoiceIgnore);
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Invalid model or data, Please try with valid data.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Notification
        public ReturnResponce GetNotification()
        {
            try
            {
                var responce = entity.Notifactions.ToList();
                return new ReturnResponce(responce, new[] { "" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Admin Reports

        public ReturnResponce GetDeletedPickers(int SchoolId)
        {
            try
            {
                if (SchoolId > 0)
                {
                    var pickers = entity.ISPickers.Where(p => p.SchoolID.Value == SchoolId && p.Deleted == false && p.Active == false).ToList();
                    return new ReturnResponce(pickers, EntityJsonIgnore.PickerIgnore);
                }
                else
                {
                    return new ReturnResponce("SchoolId must be grater than 0.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetDeletedParents(int SchoolId)
        {
            try
            {
                if (SchoolId > 0)
                {
                    var BackParents = entity.BckupParents.Where(p => p.SchoolID.Value == SchoolId).ToList();
                    return new ReturnResponce(BackParents, new[] { "" });
                }
                else
                {
                    return new ReturnResponce("SchoolId must be grater than 0.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetDeletedStudents(int SchoolId)
        {
            try
            {
                if (SchoolId > 0)
                {
                    var student = entity.ISStudents.Where(p => p.SchoolID == SchoolId && p.Deleted == false && p.Active == false).ToList();
                    return new ReturnResponce(student, EntityJsonIgnore.StudentIgnore);
                }
                else
                {
                    return new ReturnResponce("SchoolId must be grater than 0.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        #endregion

       
    }
}