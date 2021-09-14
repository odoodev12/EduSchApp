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

        #region Teacher      

        public ReturnResponce GetTeacher(int TeacherID)
        {
            try
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();
                MISTeacher TeacherList = objTeacherManagement.GetTeacher(TeacherID);

                if (TeacherList != null)
                {
                    return new ReturnResponce(TeacherList, EntityJsonIgnore.MISTeacherIgnore);
                }
                else
                {
                    return new ReturnResponce("No records to display.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce ResetPassword(int TeacherID, string NewPassword)
        {
            try
            {
                ISTeacher objTeacher = entity.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                if (objTeacher != null)
                {
                    objTeacher.Password = EncryptionHelper.Encrypt(NewPassword);
                    objTeacher.ModifyBy = Authentication.LogginTeacher.ID;
                    objTeacher.ModifyDateTime = DateTime.Now;
                    entity.SaveChanges();

                    // sending email to admin & supervisor by Maharshi @ Gatistavam-softech
                    var objschoo = entity.ISSchools.SingleOrDefault(x => x.ID == objTeacher.SchoolID);
                    EmailManagement emobj = new EmailManagement();
                    emobj.SendEmail(objTeacher.Email, " Some profile Details(Password Reset) changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + objTeacher.Name + ",<br /> " + "your Profile Details have been changed. please confirm on website <br /> Thnk you");
                    emobj.SendEmail(objschoo.SupervisorEmail, " teacher profile Details(Password Reset) changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + objschoo.SupervisorFirstname + objschoo.SupervisorLastname + " ,<br />" + "a teacher has updated profile details teachername : " + objTeacher.Name + " " + "please confirm on website<br/> Thnk you");
                    emobj.SendEmail(objschoo.AdminEmail, "  teacher profile Details(Password Reset) changed !", "<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear " + objschoo.AdminFirstName + objschoo.AdminLastName + " ,<br />" + "a teacher has updated profile details teachername : " + objTeacher.Name + " " + "please confirm on website<br/> Thnk you");

                    return new ReturnResponce(objTeacher, "Password Updated Successfully", EntityJsonIgnore.TeacherIgnore);
                }
                else
                {
                    return new ReturnResponce("");
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return new ReturnResponce(ex.Message);
            }

        }
        public ReturnResponce GetTeacherList(int SchoolID, int ClassID = 0, string Year = "", string TeacherName = "", string OrderBy = "Asc", string SortBy = "", int classTypeID = 0, string Status = "0")
        {
            try
            {
                TeacherManagement objTeacherManagement = new TeacherManagement();
                List<MISTeacher> objList = objTeacherManagement.TeacherList(SchoolID, Year, ClassID, TeacherName, OrderBy, SortBy, classTypeID, Status);
                return new ReturnResponce(objList, EntityJsonIgnore.MISTeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        public ReturnResponce AddTeacher(AddTeacherViewModel model)
        {
            try
            {
                if (entity.ISTeachers.Where(a => a.Email.ToLower() == model.Email.ToLower() && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Email Address is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.PhoneNo == model.PhoneNo && a.SchoolID == model.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Phone number is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.TeacherNo == model.TeacherNo && a.SchoolID == model.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Number is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.Email == model.Email && a.Name == model.Name && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Name is Already Setup with this Email in the School.");
                }
                else if (model.ClassIDs == null || model.ClassIDs.Length == 0)
                {
                    return new ReturnResponce("Not Allowed. At least one class should select.");
                }
                else
                {
                    ISTeacher objTeacher = new ISTeacher();
                    objTeacher.SchoolID = model.SchoolID;
                    objTeacher.ClassID = model.ClassIDs[0];
                    objTeacher.Role = (int)EnumsManagement.ROLETYPE.TEACHING;
                    objTeacher.RoleID = model.RoleID;
                    objTeacher.TeacherNo = model.TeacherNo;
                    objTeacher.Title = model.Title;
                    objTeacher.Name = model.Name;
                    objTeacher.PhoneNo = model.PhoneNo;
                    objTeacher.Email = model.Email;
                    string Passwords = CommonOperation.GenerateNewRandom();
                    objTeacher.Password = EncryptionHelper.Encrypt(Passwords);
                    objTeacher.EndDate = new DateTime(2050, 01, 01);
                    objTeacher.Photo = "Upload/user.jpg";
                    objTeacher.Active = model.Active;
                    objTeacher.Deleted = model.Active;
                    objTeacher.CreatedBy = model.LoginUserId;
                    objTeacher.CreatedDateTime = DateTime.Now;
                    objTeacher.CreatedByType = model.UserType;
                    entity.ISTeachers.Add(objTeacher);
                    entity.SaveChanges();


                    for (int i = 0; i < model.ClassIDs.Length; i++)
                    {
                        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                        objClass1.ClassID = model.ClassIDs[i];
                        objClass1.TeacherID = objTeacher.ID;
                        objClass1.Active = model.Active;
                        objClass1.Deleted = model.Active;
                        objClass1.CreatedBy = model.UserType;
                        objClass1.CreatedDateTime = DateTime.Now;
                        objClass1.Out = 0;
                        objClass1.Outbit = false;
                        entity.ISTeacherClassAssignments.Add(objClass1);
                        entity.SaveChanges();
                    }

                    LogManagement.AddLog("Teacher Created Successfully " + "Name : " + objTeacher.Name + " Document Category : Teacher", "Teacher");
                    return new ReturnResponce(objTeacher, "Teacher Created Successfully ", EntityJsonIgnore.TeacherIgnore);
                    //EmailManage(objTeacher);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce UpdateTeacher(UpdateTeacherViewModel model)
        {
            try
            {
                if (entity.ISTeachers.Where(a => a.ID != model.TeacherId && a.Email == model.Email && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Email Address is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.ID != model.TeacherId && a.PhoneNo == model.PhoneNo && a.SchoolID == model.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Phone number is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.ID != model.TeacherId && a.TeacherNo == model.TeacherNo && a.SchoolID == model.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Number is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.ID != model.TeacherId && a.Email == model.Email && a.Name == model.Name && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. Teacher Name is Already Setup with this Email in the School.");
                }
                else if (model.ClassIDs == null || model.ClassIDs.Length == 0)
                {
                    return new ReturnResponce("Not Allowed. At least one class should select.");
                }
                else
                {
                    ISTeacher objTeacher = entity.ISTeachers.SingleOrDefault(p => p.ID == model.TeacherId);
                    objTeacher.ClassID = model.ClassIDs[0];
                    objTeacher.RoleID = model.RoleID;
                    objTeacher.TeacherNo = model.TeacherNo;
                    objTeacher.Title = model.Title;
                    objTeacher.Name = model.Name;
                    objTeacher.PhoneNo = model.PhoneNo;
                    objTeacher.Email = model.Email;
                    objTeacher.Active = model.Active;
                    objTeacher.ModifyBy = model.LoginUserId;
                    objTeacher.ModifyDateTime = DateTime.Now;
                    entity.SaveChanges();


                    //Remove Range from Teacher Class Assignment By Teacher ID
                    List<ISTeacherClassAssignment> objList = entity.ISTeacherClassAssignments.Where(p => p.TeacherID == model.TeacherId && p.Active == true).ToList();
                    if (objList.Count > 0)
                    {

                        TeacherManagement objTeacherManagement = new TeacherManagement();
                        for (int i = 0; i < objList.Count; i++)
                        {
                            var NewClassId = model.ClassIDs.Length >= i ? model.ClassIDs[i] : 0;
                            if (NewClassId > 0)
                            {
                                objTeacherManagement.TeacherReassignment(model.SchoolID, model.TeacherId, objList[i].ClassID, NewClassId, model.UserType, model.LoginUserId);
                            }

                        }

                        entity.ISTeacherClassAssignments.RemoveRange(objList);
                        entity.SaveChanges();
                    }

                    //Add Teacher Class Assignment
                    for (int i = 0; i < model.ClassIDs.Length; i++)
                    {
                        ISTeacherClassAssignment objClass = new ISTeacherClassAssignment();
                        objClass.ClassID = model.ClassIDs[i];
                        objClass.TeacherID = model.TeacherId;
                        objClass.Active = model.Active;
                        objClass.Deleted = model.Active;
                        objClass.CreatedBy = model.LoginUserId;
                        objClass.CreatedDateTime = DateTime.Now;
                        objClass.Out = 0;
                        objClass.Outbit = false;
                        entity.ISTeacherClassAssignments.Add(objClass);
                        entity.SaveChanges();
                    }

                    LogManagement.AddLog("Teacher Updated Successfully " + "Name : " + objTeacher.Name + " Document Category : TeacherProfile", "Teacher");
                    return new ReturnResponce(objTeacher, "Teacher Updated Successfully", EntityJsonIgnore.TeacherIgnore);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return new ReturnResponce(ex.Message);
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