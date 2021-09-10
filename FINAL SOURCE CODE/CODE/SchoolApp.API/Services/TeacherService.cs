using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.API.Services
{
    public class TeacherService
    {
        public SchoolAppEntities entity;

        public TeacherService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region Pickup
        public ReturnResponce GetPickupList()
        {
            try
            {
                var response = entity.ISPickups.ToList();
                return new ReturnResponce(response, EntityJsonIgnore.PickupIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce GetIsCompletePickupRun(bool IsCompletePickupRun)
        {
            try
            {
                if (IsCompletePickupRun)
                {
                    var response = entity.ISCompletePickupRuns.ToList();
                    return new ReturnResponce(response, EntityJsonIgnore.ISCompletePickupRunIgnore);
                }
                else
                {
                    var response = entity.ISPickups.ToList();
                    return new ReturnResponce(response, EntityJsonIgnore.PickupIgnore);
                }
                
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetPickerList()
        {
            try
            {
                var response = entity.ISPickers.ToList();
                return new ReturnResponce(response, EntityJsonIgnore.PickerIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetPicker(int PickerId)
        {
            try
            {
                var response = entity.ISPickers.Where(x => x.ID == PickerId).SingleOrDefault();
                return new ReturnResponce(response, EntityJsonIgnore.PickerIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetPickerActivity(int PickerId)
        {
            try
            {
                var response = entity.ISPickups.Where(x => x.PickerID == PickerId).ToList();
                return new ReturnResponce(response, EntityJsonIgnore.PickupIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        #endregion

        #region  class
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

        #region Support

        

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

        #region PickupReport

        public ReturnResponce GetStudentPickUpReport(PickupReportRequest pickupReportRequest)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pickupReportRequest.FromDate) && !string.IsNullOrWhiteSpace(pickupReportRequest.ToDate))
                {
                    ISStudent _Student = entity.ISStudents.SingleOrDefault(p => p.ID == pickupReportRequest.StudentID && p.Active == true && p.Deleted == true);

                    if (_Student != null)
                    {
                        List<StudentAttendenceData> studentAttendenceDataList = new List<StudentAttendenceData>();

                        if (_Student.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            var responce = StudentPickUpReports(pickupReportRequest);
                            return new ReturnResponce(responce, new[] { "" });
                        }
                        else
                        {
                            var responce = StudentPickUpReportsAfterSchoolOnly(pickupReportRequest.StudentID, pickupReportRequest.FromDate, pickupReportRequest.ToDate, pickupReportRequest.PickerID, pickupReportRequest.Status, pickupReportRequest.SortBy, pickupReportRequest.OrderBy, pickupReportRequest.StudentID);
                            return new ReturnResponce(responce, new[] { "" });
                        }
                    }
                    else
                    {
                        return new ReturnResponce("Student id must be required");
                    }
                }
                else
                {
                    return new ReturnResponce("Frome Date and To date are  invalid");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public List<MViewStudentPickUp> StudentPickUpReports(PickupReportRequest pickupReportRequest)
        {
            List<MViewStudentPickUp> objStudentPickupList = new List<MViewStudentPickUp>();
            ISStudent objStudent = entity.ISStudents.SingleOrDefault(p => p.ID == pickupReportRequest.StudentID);
            var objPickUp = entity.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).ToList();

            DateTime dt = DateTime.Now;
            List<DateTime> list = new List<DateTime>();
            if (pickupReportRequest.FromDate != "" && pickupReportRequest.ToDate != "")
            {
                string dates = pickupReportRequest.FromDate;
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

                string dates2 = pickupReportRequest.ToDate;
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
                list = AllDates(Convert.ToDateTime(dt2), Convert.ToDateTime(dt3), pickupReportRequest.StudentID);
            }
            else
            {
                list = AllDatesInMonth(dt.Year, dt.Month, pickupReportRequest.StudentID);
            }
            foreach (DateTime date in list)
            {
                if (date.Date >= objStudent.CreatedDateTime.Value.Date)
                {
                    List<ISHoliday> List = entity.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
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
                                if (entity.ISAttendances.Where(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate)).Count() <= 0)
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
                                            newobj.PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                        }
                                    }

                                    else
                                    {
                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                        newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                        newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                        newobj.PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                    }
                                }
                                else
                                {
                                    newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                    newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                    newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                    newobj.PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                }

                                newobj.Status = String.IsNullOrEmpty(item.PickStatus) ? "Not Marked" : item.PickStatus;
                                newobj.PickStatus = newobj.Status;
                                newobj.ClassID = item.ClassID;

                                newobj.SchoolName = !String.IsNullOrEmpty(item.PickStatus) ? item.PickStatus.Contains("After-School-Ex") ? entity.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "" : "";

                                if (!String.IsNullOrEmpty(item.PickStatus) && item.PickStatus.Contains("After-School-Ex"))
                                {
                                    ClassManagement objClassManagement = new ClassManagement();

                                    ISClass Cid = entity.ISClasses.FirstOrDefault(r => r.TypeID == (int)CLASSTYPE.AfterSchool && r.SchoolID == item.SchoolID);
                                    MISClass Obj = objClassManagement.GetClass(Cid.ID);
                                    newobj.SchoolName = Obj.ExternalOrganisation;
                                }
                            }
                        }
                        else
                        {
                            newobj.PickDate = date;
                            if (entity.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
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
                        if (entity.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
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
                    objStudentPickupList.Add(newobj);
                }
            }
            if (pickupReportRequest.StudentID != 0)
            {
                objStudentPickupList = objStudentPickupList.Where(p => p.StudentID == pickupReportRequest.StudentID).ToList();
            }
            if (pickupReportRequest.PickerID != "")
            {
                objStudentPickupList = objStudentPickupList.Where(p => p.PickerName == pickupReportRequest.PickerID).ToList();
            }
            if (pickupReportRequest.Status != "")
            {
                if (pickupReportRequest.Status == "School Closed")
                {
                    objStudentPickupList = objStudentPickupList.Where(p => p.PickStatus.Contains(pickupReportRequest.Status)).ToList();
                }
                else
                {
                    objStudentPickupList = objStudentPickupList.Where(p => pickupReportRequest.Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == pickupReportRequest.Status).ToList();
                }
            }
            if (pickupReportRequest.OrderBy == "ASC")
            {
                if (pickupReportRequest.SortBy == "ChildName")
                {
                    objStudentPickupList = objStudentPickupList.OrderBy(p => p.StudentName).ToList();
                }
                else if (pickupReportRequest.SortBy == "Status")
                {
                    objStudentPickupList = objStudentPickupList.OrderBy(p => p.Status).ToList();
                }
                else if (pickupReportRequest.SortBy == "Picker")
                {
                    objStudentPickupList = objStudentPickupList.OrderBy(p => p.PickerName).ToList();
                }
                else
                {
                    objStudentPickupList = objStudentPickupList.OrderBy(p => p.PickDate).ToList();
                }
            }
            else
            {
                if (pickupReportRequest.SortBy == "ChildName")
                {
                    objStudentPickupList = objStudentPickupList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (pickupReportRequest.SortBy == "Status")
                {
                    objStudentPickupList = objStudentPickupList.OrderByDescending(p => p.Status).ToList();
                }
                else if (pickupReportRequest.SortBy == "Picker")
                {
                    objStudentPickupList = objStudentPickupList.OrderByDescending(p => p.PickerName).ToList();
                }
                else
                {
                    objStudentPickupList = objStudentPickupList.OrderByDescending(p => p.PickDate).ToList();
                }
            }
            return objStudentPickupList;
        }

        public List<MViewStudentPickUp> StudentPickUpReportsAfterSchoolOnly(int StudentID, string DateFrom, string DateTo, string PickerID, string Status, string SortBy, string OrderBy, int StudID)
        {           
            int ID = StudID;
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISStudent objStudent = entity.ISStudents.SingleOrDefault(p => p.ID == ID);
            var objPickUp = entity.getStudentReport(objStudent.ParantEmail1, objStudent.ParantEmail2).ToList();

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
                    List<ISHoliday> List = entity.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
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
                                if (entity.ISAttendances.Where(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate)).Count() <= 0)
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
                                            newobj.PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                        }
                                    }

                                    else
                                    {
                                        ISAttendance attendance = entity.ISAttendances.FirstOrDefault(p => p.StudentID == item.StudID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(item.PickDate));


                                        newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                        newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                        newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                        newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                        newobj.PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                    }
                                }
                                else
                                {
                                    newobj.Pick_Date = item.PickDate.Value.ToString("dd/MM/yyyy");
                                    newobj.PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus;
                                    newobj.Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt");
                                    newobj.PickerID = item.PickerID == null ? 0 : item.PickerID;
                                    newobj.PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName;
                                }

                                newobj.Status = String.IsNullOrEmpty(item.PickStatus) ? "Not Marked" : item.PickStatus;
                                newobj.PickStatus = newobj.Status;
                                newobj.ClassID = item.ClassID;
                                newobj.SchoolName = !String.IsNullOrEmpty(item.PickStatus) ? item.PickStatus.Contains("After-School-Ex") ? entity.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "" : "";
                            }
                        }
                        else
                        {
                            newobj.PickDate = date;
                            if (entity.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
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
                        if (entity.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(date)).Count() <= 0)
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
        public static List<DateTime> AllDates(DateTime FromDate, DateTime ToDate, int studentID)
        {
            List<DateTime> AuthorList = new List<DateTime>();
            DateTime startDate = ToDate;
            DateTime expiryDate = FromDate;

            for (int i = 0; i <= startDate.Subtract(expiryDate).Days; i++)
            {
                AuthorList.Add(expiryDate.AddDays(i));
            }

            return AuthorList;
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
        #endregion

        #region class Daily Status 
        public ReturnResponce GetDailyClassReport(DailyClassStatusRequest dailyClassStatusRequest)
        {
            try
            {
                int ID = Authentication.LogginTeacher.ID;
                int ClasssID = dailyClassStatusRequest.ClassID != "" ? Convert.ToInt32(dailyClassStatusRequest.ClassID) : 0;
                int StudentsID = dailyClassStatusRequest.StudentID != "0" ? Convert.ToInt32(dailyClassStatusRequest.StudentID) : 0;
                if (ClasssID != 0)
                {
                    var responce = DailyClassReports(Authentication.SchoolID, dailyClassStatusRequest.Date, ClasssID, ID, StudentsID, dailyClassStatusRequest.PickupStatus, dailyClassStatusRequest.orderBy, dailyClassStatusRequest.SortByStudentID);
                    return new ReturnResponce(responce, new[] { "" });
                }
                else
                {
                    return new ReturnResponce("Class id must be greater then 0");
                }
                
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public List<MViewStudentPickUp> DailyClassReports(int SchoolID, string Date, int ClassID, int TeacherID, int StudentID, string Status, string OrderBy, string SortBy)
        {
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
            ISTeacher Obj = entity.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            ISSchool ObjSchool = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
            List<MISStudent> obj = new List<MISStudent>();
            List<ISHoliday> objHoliday = entity.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true).ToList();
            if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
            {
                objList = (from item in entity.getPickUpData(dt).Where(p => p.Deleted == true)//ViewStudentPickUps.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
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
                               Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                               Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                               PickerID = item.PickerID == null ? 0 : item.PickerID,
                               PickDate = item.PickDate == null ? null : item.PickDate,
                               PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                               PickerImage = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                               ClassID = item.ClassID,
                               OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                               AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                               CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                               ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                           }).ToList();
            }
            else
            {
                objList = (from item in entity.getPickUpData(dt).Where(p => p.SchoolID == SchoolID && p.Deleted == true)//ViewStudentPickUps.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
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
                               PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                               PickerImage = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).Photo,
                               ClassID = item.ClassID,
                               StartDates = item.StartDate == null ? "" : item.StartDate.Value.ToString("dd/MM/yyyy"),
                               OfficeFlag = (item.OfficeFlag.HasValue && item.OfficeFlag.Value) ? true : false,
                               AfterSchoolFlag = (item.AfterSchoolFlag.HasValue && item.AfterSchoolFlag.Value) ? true : false,
                               CompletePickup = (item.CompletePickup.HasValue && item.CompletePickup.Value) ? true : false,
                               ClubFlag = (item.ClubFlag.HasValue && item.ClubFlag.Value) ? true : false,
                           }).ToList();
            }
            if (ClassID != 0)
            {
                ISClass ObjClass = entity.ISClasses.SingleOrDefault(p => p.ID == ClassID);
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
                        objList = objList.Where(p => p.ClassID == ClassID).ToList();                       
                    
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

        #endregion
    }
}