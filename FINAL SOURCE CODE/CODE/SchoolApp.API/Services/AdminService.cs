using Newtonsoft.Json;
using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
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
                var responce = entity.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.ID == SchoolId).FirstOrDefault();
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
                var responce = entity.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.TypeID == SchoolTypeId).FirstOrDefault();
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

        public ReturnResponce GetClassList()
        {
            try
            {
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetClassList(int SchoolId)
        {
            try
            {
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
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
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.ID == ClassID).FirstOrDefault();
                return new ReturnResponce(responce, EntityJsonIgnore.ClassesIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce AddUpdateClass(ISClass model, int AdminId)
        {
            try
            {
                ISClass insertUpdate = new ISClass();

                if (model.ID > 0)
                {
                    insertUpdate = entity.ISClasses.Where(x => x.ID == model.ID && x.Active == true && x.Deleted == true).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        var className = entity.ISClasses.Where(x => x.Name == model.Name && x.SchoolID == model.SchoolID).SingleOrDefault();
                        if (className.Name == "")
                        {
                            if (model.ISClassType.ID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                            {
                                List<ISClass> ObjClass = entity.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).ToList();
                                if (ObjClass.Count == 0)
                                {
                                    if (model.ISClassType.Name == "Internal")
                                    {
                                        insertUpdate.Name = model.Name + "(After School)";
                                        insertUpdate.TypeID = model.TypeID;
                                        insertUpdate.AfterSchoolType = model.AfterSchoolType;
                                        insertUpdate.ExternalOrganisation = "";
                                        insertUpdate.EndDate = new DateTime(2050, 01, 01);
                                        insertUpdate.SchoolID = model.SchoolID;
                                        insertUpdate.ISNonListed = false;
                                        insertUpdate.Active = true;
                                        insertUpdate.Deleted = true;
                                        insertUpdate.CreatedBy = AdminId;
                                        insertUpdate.CreatedByType = Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                                        insertUpdate.CreatedDateTime = DateTime.Now;
                                        entity.ISClasses.Add(insertUpdate);
                                        entity.SaveChanges();
                                    }
                                    else
                                    {
                                        if (entity.ISSchools.Where(p => p.Name == model.ExternalOrganisation).Count() <= 0)
                                        {                                            
                                            return new ReturnResponce("Input the name of the After-School in the text box below. Please select the non-listed checkbox to continue if the name is not auto-selected from the list on our database");                    
                                        }
                                        else
                                        {
                                            insertUpdate.Name = model.Name + "(After School Ex)";
                                            insertUpdate.TypeID = model.TypeID;
                                            insertUpdate.AfterSchoolType = model.AfterSchoolType;
                                            insertUpdate.ExternalOrganisation = model.ExternalOrganisation;
                                            insertUpdate.EndDate = new DateTime(2050, 01, 01);
                                            insertUpdate.SchoolID = Authentication.SchoolID;
                                            insertUpdate.ISNonListed = false;
                                            insertUpdate.Active = true;
                                            insertUpdate.Deleted = true;
                                            insertUpdate.CreatedBy = AdminId;
                                            insertUpdate.CreatedByType = Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                                            insertUpdate.CreatedDateTime = DateTime.Now;
                                            entity.ISClasses.Add(insertUpdate);
                                            entity.SaveChanges();                                            

                                        }
                                    }
                                }
                                else
                                {
                                    return new ReturnResponce("Only one after school is allowed for a standard school invalid request");
                                }
                            }
                            else
                            {
                                insertUpdate.Name = model.Name;
                                if (model.ISClassType.ID == (int)EnumsManagement.CLASSTYPE.Standard)
                                {
                                    insertUpdate.Year = Authentication.SchoolTypeID == 2 ? model.Year : "1";
                                }
                                insertUpdate.TypeID = model.TypeID;
                                insertUpdate.EndDate = new DateTime(2050, 01, 01);
                                insertUpdate.SchoolID = Authentication.SchoolID;
                                insertUpdate.ISNonListed = false;
                                insertUpdate.Active = true;
                                insertUpdate.Deleted = true;
                                insertUpdate.CreatedBy = AdminId;
                                insertUpdate.CreatedDateTime = DateTime.Now;
                                entity.ISClasses.Add(insertUpdate);
                            }
                        }
                        else
                        {
                            return new ReturnResponce("Class name allready exist");
                        }
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        var ClassTeacherId = entity.ISTeachers.Where(x => x.ID == insertUpdate.ID && x.Active == true && x.Deleted == true).SingleOrDefault();
                       
                        insertUpdate.Name = model.Name;
                        insertUpdate.TypeID = model.TypeID;
                        insertUpdate.EndDate = new DateTime(2050, 01, 01);
                        insertUpdate.SchoolID = Authentication.SchoolID;
                        insertUpdate.ISNonListed = false;
                        insertUpdate.Active = true;
                        insertUpdate.Deleted = true;
                        insertUpdate.ModifyBy = AdminId;
                        insertUpdate.ModifyDateTime = DateTime.Now;
                        entity.SaveChanges();
                    }

                    return new ReturnResponce(insertUpdate, EntityJsonIgnore.ClassesIgnore);
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

        #region Teacher        
        public ReturnResponce GetTeacherList()
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetTeacherList(int SchoolId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING).ToList();
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
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.ID == TeacherId && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetTeacherList(int SchoolId, int ClassId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.ClassID == ClassId && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING).ToList();
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

        public ReturnResponce ReAssignTeacher(ISTeacherReassignHistory model, int AdminId)
        {
            try
            {
                ISTeacherReassignHistory ObjReassign = new ISTeacherReassignHistory();
                if (model.FromClass != model.ToClass)
                {                    
                    ObjReassign.SchoolID = model.SchoolID;
                    ObjReassign.FromClass = model.FromClass;
                    ObjReassign.ToClass = model.ToClass;
                    ObjReassign.Date = DateTime.Now;
                    ObjReassign.TeacherID = model.TeacherID;
                    ObjReassign.Active = true;
                    ObjReassign.Deleted = true;
                    ObjReassign.CreatedBy = AdminId;
                    ObjReassign.CreatedByType = 1;
                    ObjReassign.CreatedDateTime = DateTime.Now;
                    entity.ISTeacherReassignHistories.Add(ObjReassign);
                    entity.SaveChanges();
                    return new ReturnResponce(ObjReassign, EntityJsonIgnore.TeacherIgnore);
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Old or New Class are must be diffrent invalid request");
                }


            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Studnet
        public ReturnResponce GetStudentList()
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce GetStudentList(int SchoolId)
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetStudentList(int SchoolId, int ClassId)
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.ClassID == ClassId).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce AddUpdateStudent(ISStudent model, int AdminId)
        {
            try
            {
                ISStudent insertUpdate = new ISStudent();

                if (model.ID > 0)
                {
                    insertUpdate = entity.ISStudents.Where(w => w.ID == model.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedBy = AdminId;
                        entity.ISStudents.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        insertUpdate.StudentName = model.StudentName;
                        insertUpdate.SchoolID = model.SchoolID;
                        insertUpdate.Photo = model.Photo;
                        insertUpdate.DOB = model.DOB;
                        insertUpdate.ParantName1 = model.ParantName1;
                        insertUpdate.ParantName2 = model.ParantName2;
                        insertUpdate.ParantEmail1 = model.ParantEmail1;
                        insertUpdate.ParantEmail2 = model.ParantEmail2;
                        insertUpdate.ParantPassword1 = model.ParantPassword1;
                        insertUpdate.ParantPassword2 = model.ParantPassword2;
                        insertUpdate.ParantPhone1 = model.ParantPhone1;
                        insertUpdate.ParantPhone2 = model.ParantPhone2;
                        insertUpdate.ParantRelation1 = model.ParantRelation1;
                        insertUpdate.ParantRelation2 = model.ParantRelation2;
                        insertUpdate.ParantPhoto1 = model.ParantPhoto1;
                        insertUpdate.ParantPhoto2 = model.ParantPhoto2;
                        insertUpdate.PickupMessageID = model.PickupMessageID;
                        insertUpdate.PickupMessageTime = model.PickupMessageTime;
                        insertUpdate.PickupMessageDate = model.PickupMessageDate;

                        insertUpdate.StartDate = model.StartDate;
                        insertUpdate.EndDate = model.EndDate;

                        insertUpdate.ModifyDateTime = model.ModifyDateTime;
                        insertUpdate.ModifyBy = AdminId;
                    }

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, EntityJsonIgnore.StudentIgnore);
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

        #region Holiday
        public ReturnResponce GetHoliday(int HoliDayId)
        {
            try
            {
                var responce = entity.ISHolidays.Where(p => p.Deleted == true && p.ID == HoliDayId).ToList();
                return new ReturnResponce(responce, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetHolidayList()
        {
            try
            {
                var responce = entity.ISHolidays.Where(p => p.Deleted == true).ToList();
                return new ReturnResponce(responce, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetHolidayList(int SchoolId)
        {
            try
            {
                var responce = entity.ISHolidays.Where(p => p.Deleted == true && p.SchoolID == SchoolId).ToList();
                return new ReturnResponce(responce, new[] { "ISSchool" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce SetHoliday(ISHoliday model, int AdminId)
        {
            try
            {
                ISHoliday insertUpdate = new ISHoliday();

                if (model.ID > 0)
                {
                    insertUpdate = entity.ISHolidays.Where(w => w.ID == model.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedBy = AdminId;
                        entity.ISHolidays.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {
                        insertUpdate.Name = model.Name;
                        insertUpdate.DateFrom = model.DateFrom;
                        insertUpdate.DateTo = model.DateTo;
                        insertUpdate.Active = model.Active;
                        insertUpdate.ModifyDateTime = model.ModifyDateTime;
                        insertUpdate.ModifyBy = AdminId;
                    }

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISSchool" });
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
        public ReturnResponce DeleteHoliday(int HolidayId, int AdminId)
        {
            try
            {
                ISHoliday insertUpdate = entity.ISHolidays.Where(w => w.ID == HolidayId).FirstOrDefault();

                if (insertUpdate != null)
                {
                    insertUpdate.Deleted = true;
                    insertUpdate.DeletedDateTime = DateTime.UtcNow;
                    insertUpdate.DeletedBy = AdminId;

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISSchool" });
                }
                else
                {
                    ///// Error Responce that invalid data here
                    return new ReturnResponce("Invalid Holiday Id or AdminId, Please try with valid data.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

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

        #region Role

        public ReturnResponce GetUsersRoleList(int SchoolId)
        {
            try
            {
                if (SchoolId > 0)
                {
                    var response = entity.ISUserRoles.Where(p => p.SchoolID == SchoolId && p.Deleted == true && p.Active == true).ToList();
                    return new ReturnResponce(response, new[] { "ISSchool", "ISTeacher" });
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

        public ReturnResponce GetUserRoleDetails(int RoleId)
        {
            try
            {
                var response = entity.ISUserRoles.Where(p => p.Deleted == true && p.Active == true && p.ID == RoleId).SingleOrDefault();
                return new ReturnResponce(response, new[] { "ISSchool", "ISTeacher" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce AddUpdateUserRole(ISUserRole iSUserRole, int UserLoginId)
        {
            try
            {
                ISUserRole insertUpdate = new ISUserRole();

                if (iSUserRole.ID > 0)
                {
                    insertUpdate = entity.ISUserRoles.Where(w => w.ID == iSUserRole.ID).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    if (insertUpdate.ID == 0) //// Insert
                    {
                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedBy = UserLoginId;
                        insertUpdate.RoleName = iSUserRole.RoleName;
                        insertUpdate.RoleType = iSUserRole.RoleType;
                        insertUpdate.SchoolID = iSUserRole.SchoolID;
                        insertUpdate.ManageClassFlag = iSUserRole.ManageClassFlag;
                        insertUpdate.ManageStudentFlag = iSUserRole.ManageStudentFlag;
                        insertUpdate.ManageHolidayFlag = iSUserRole.ManageHolidayFlag;
                        insertUpdate.ManageViewAccountFlag = iSUserRole.ManageViewAccountFlag;
                        insertUpdate.ManageTeacherFlag = iSUserRole.ManageTeacherFlag;
                        insertUpdate.ManageNonTeacherFlag = iSUserRole.ManageNonTeacherFlag;
                        insertUpdate.ManageSupportFlag = iSUserRole.ManageSupportFlag;
                        insertUpdate.Active = iSUserRole.Active;
                        insertUpdate.Deleted = true;
                        entity.ISUserRoles.Add(insertUpdate);
                    }
                    else if (insertUpdate.ID > 0) //// Update
                    {

                        insertUpdate.RoleName = iSUserRole.RoleName;
                        insertUpdate.ManageClassFlag = iSUserRole.ManageClassFlag;
                        insertUpdate.ManageStudentFlag = iSUserRole.ManageStudentFlag;
                        insertUpdate.ManageHolidayFlag = iSUserRole.ManageHolidayFlag;
                        insertUpdate.ManageViewAccountFlag = iSUserRole.ManageViewAccountFlag;
                        insertUpdate.ManageTeacherFlag = iSUserRole.ManageTeacherFlag;
                        insertUpdate.ManageNonTeacherFlag = iSUserRole.ManageNonTeacherFlag;
                        insertUpdate.ManageSupportFlag = iSUserRole.ManageSupportFlag;
                        insertUpdate.Active = iSUserRole.Active;
                        insertUpdate.ModifyBy = UserLoginId;
                        insertUpdate.ModifyDateTime = DateTime.Now;
                    }
                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISSchool", "ISTeacher" });
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
        public ReturnResponce DeleteUserRole(int RoleId, int UserLoginId)
        {
            try
            {
                ISUserRole insertUpdate = new ISUserRole();

                if (RoleId > 0)
                {
                    insertUpdate = entity.ISUserRoles.Where(w => w.ID == RoleId).FirstOrDefault();
                }

                if (insertUpdate != null)
                {
                    insertUpdate.Deleted = false;
                    insertUpdate.DeletedBy = UserLoginId;
                    insertUpdate.DeletedDateTime = DateTime.Now;

                    entity.SaveChanges();
                    return new ReturnResponce(insertUpdate, new[] { "ISSchool", "ISTeacher" });
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

        #region NonTeaching

        public ReturnResponce GetNonTeacherList(int SchoolId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.Role == (int)EnumsManagement.ROLETYPE.NONTEACHING).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetNonTeacher(int TeacherId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.ID == TeacherId && p.Role == (int)EnumsManagement.ROLETYPE.NONTEACHING).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetNonTeacherList(int SchoolId, int ClassId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.ClassID == ClassId && p.Role == (int)EnumsManagement.ROLETYPE.NONTEACHING).ToList();
                return new ReturnResponce(responce, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce AddUpdateNonTeaching(ISTeacher model, int AdminId)
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
                        insertUpdate.Role = (int)EnumsManagement.ROLETYPE.NONTEACHING;
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
        #endregion
    }
}