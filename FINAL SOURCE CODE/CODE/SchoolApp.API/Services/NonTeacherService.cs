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
    public class NonTeacherService
    {
        private SchoolAppEntities entity;
        private TeacherManagement objTeacherManagement;

        public NonTeacherService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
            objTeacherManagement = new TeacherManagement();
        }

        #region NonTeaching
        public ReturnResponce GetNonTeacherList(int SchoolId)
        {
            try
            {
                List<MISTeacher> objList = objTeacherManagement.NonTeacherList(SchoolId, "", "", "");
                return new ReturnResponce(objList, EntityJsonIgnore.MISTeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetNonTeacher(int TeacherID)
        {
            try
            {
                var Obj = objTeacherManagement.GetNonTeacher(TeacherID);

                TeacherDetailsViewModel objMISTeacher = new TeacherDetailsViewModel();

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
                    objMISTeacher.ClassName = Obj.ClassName;
                    objMISTeacher.ClassID1 = Obj.ClassID1;
                    objMISTeacher.ClassID2 = Obj.ClassID2;
                    objMISTeacher.ClassName1 = Obj.ClassName1;
                    objMISTeacher.ClassName2 = Obj.ClassName2;

                }

                return new ReturnResponce(objMISTeacher, EntityJsonIgnore.TeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce AddNonTeaching(AddNonTeacherViewModel model)
        {
            try
            {
                if (entity.ISTeachers.Where(a => a.Email.ToLower() == model.Email.ToLower() && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. NonTeacher Email Address is Already Setup in the School.");
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
                    objTeacher.Role = (int)EnumsManagement.ROLETYPE.NONTEACHING;
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
                    objTeacher.Active = true;
                    objTeacher.Deleted = true;
                    objTeacher.CreatedBy = model.LoginUserId;
                    objTeacher.CreatedDateTime = DateTime.Now;
                    objTeacher.CreatedByType = model.LoginUserType;
                    entity.ISTeachers.Add(objTeacher);
                    entity.SaveChanges();


                    for (int i = 0; i < model.ClassIDs.Length; i++)
                    {
                        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                        objClass1.ClassID = model.ClassIDs[i];
                        objClass1.TeacherID = objTeacher.ID;
                        objClass1.Active = true;
                        objClass1.Deleted = true;
                        objClass1.CreatedBy = model.LoginUserType;
                        objClass1.CreatedDateTime = DateTime.Now;
                        objClass1.Out = 0;
                        objClass1.Outbit = false;
                        entity.ISTeacherClassAssignments.Add(objClass1);
                    }
                    entity.SaveChanges();

                    return new ReturnResponce(objTeacher, "Non Teacher Created Successfully ", EntityJsonIgnore.TeacherIgnore);
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce UpdateNonTeacher(UpdateNonTeacherViewModel model)
        {
            try
            {
                if (entity.ISTeachers.Where(a => a.ID != model.Id && a.Email == model.Email && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. NonTeacher Email Address is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.ID != model.Id && a.PhoneNo == model.PhoneNo && a.SchoolID == model.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. NonTeacher Phone number is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.ID != model.Id && a.TeacherNo == model.TeacherNo && a.SchoolID == model.SchoolID && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. NonTeacher Number is Already Setup in the School.");
                }
                else if (entity.ISTeachers.Where(a => a.ID != model.Id && a.Email == model.Email && a.Name == model.Name && a.Deleted == true).ToList().Count > 0)
                {
                    return new ReturnResponce("Not Allowed. NonTeacher Name is Already Setup with this Email in the School.");
                }
                else if (model.ClassIDs == null || model.ClassIDs.Length == 0)
                {
                    return new ReturnResponce("Not Allowed. At least one class should select.");
                }
                else
                {
                    ISTeacher objTeacher = entity.ISTeachers.SingleOrDefault(p => p.ID == model.Id);
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


                    //Remove Range from Teacher Class Assignment By NonTeacher ID
                    List<ISTeacherClassAssignment> objList = entity.ISTeacherClassAssignments.Where(p => p.TeacherID == model.Id && p.Active == true).ToList();
                    if (objList.Count > 0)
                    {

                        TeacherManagement objTeacherManagement = new TeacherManagement();
                        for (int i = 0; i < objList.Count; i++)
                        {
                            var NewClassId = model.ClassIDs.Length >= i ? model.ClassIDs[i] : 0;
                            if (NewClassId > 0)
                            {
                                objTeacherManagement.TeacherReassignment(model.SchoolID, model.Id, objList[i].ClassID, NewClassId, model.LoginUserType, model.LoginUserId);
                            }

                        }

                        entity.ISTeacherClassAssignments.RemoveRange(objList);
                        entity.SaveChanges();
                    }

                    //Add NonTeacher Class Assignment
                    for (int i = 0; i < model.ClassIDs.Length; i++)
                    {
                        ISTeacherClassAssignment objClass = new ISTeacherClassAssignment();
                        objClass.ClassID = model.ClassIDs[i];
                        objClass.TeacherID = model.Id;
                        objClass.Active = model.Active;
                        objClass.Deleted = model.Active;
                        objClass.CreatedBy = model.LoginUserId;
                        objClass.CreatedDateTime = DateTime.Now;
                        objClass.Out = 0;
                        objClass.Outbit = false;
                        entity.ISTeacherClassAssignments.Add(objClass);
                        entity.SaveChanges();
                    }

                    return new ReturnResponce(objTeacher, "NonTeacher Updated Successfully", EntityJsonIgnore.TeacherIgnore);
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetNonTeacherList(int SchoolID, string TeacherName = "", string OrderBy = "Asc", string SortBy = "")
        {
            try
            {
                List<MISTeacher> objList = objTeacherManagement.NonTeacherList(SchoolID, TeacherName, OrderBy, SortBy);
                return new ReturnResponce(objList, EntityJsonIgnore.MISTeacherIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce ResetPassword(int NonTeacherID, string NewPassword)
        {
            try
            {
                ISTeacher objTeacher = entity.ISTeachers.SingleOrDefault(p => p.ID == NonTeacherID);
                if (objTeacher != null)
                {
                    objTeacher.Password = EncryptionHelper.Encrypt(NewPassword);
                    objTeacher.ModifyBy = NonTeacherID;
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
                return new ReturnResponce(ex.Message);
            }

        }

        public ReturnResponce AddClassAssign(int SchoolID, int TeacherId, int[] ClassIds, int LogInUserType, int LoginUserId)
        {
            try
            {
                if (ClassIds == null || ClassIds.Length == 0)
                {
                    return new ReturnResponce("At least 1 class required for assign process, Please select classes for assign!");
                }
                else if (LogInUserType > 0 && LogInUserType < 3)
                {
                    return new ReturnResponce("Permission denied, only School and Admin login user can access this API!");
                }

                ISTeacher ObjTeachers = entity.ISTeachers.SingleOrDefault(p => p.ID == TeacherId && p.Deleted == true);
                List<ISTeacherClassAssignment> objClasses = entity.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherId && p.Active == true).ToList();
                string OldClasses = string.Empty;
                string NewClasses = string.Empty;


                if (objClasses.Count > 0)
                {
                    TeacherManagement objTeacherManagement = new TeacherManagement();

                    for (int i = 0; i < ClassIds.Length; i++)
                    {
                        int? oldclassId = objClasses[i] != null ? objClasses[i].ClassID : null;
                        objTeacherManagement.TeacherReassignment(SchoolID, TeacherId, oldclassId, ClassIds[i], LogInUserType, LoginUserId);
                    }

                    entity.ISTeacherClassAssignments.RemoveRange(objClasses);
                    entity.SaveChanges();
                }

                for (int i = 0; i < ClassIds.Length; i++)
                {
                    ISTeacherClassAssignment objClass = new ISTeacherClassAssignment();
                    objClass.ClassID = ClassIds[i];
                    objClass.TeacherID = TeacherId;
                    objClass.Active = ObjTeachers.Active;
                    objClass.Deleted = ObjTeachers.Active;
                    objClass.CreatedBy = LoginUserId;
                    objClass.CreatedDateTime = DateTime.Now;
                    objClass.Out = 0;
                    objClass.Outbit = false;
                    entity.ISTeacherClassAssignments.Add(objClass);
                    entity.SaveChanges();
                }

                ISTeacher ObjTeacher = entity.ISTeachers.SingleOrDefault(p => p.ID == TeacherId);
                return new ReturnResponce(ObjTeacher, "NonTeacher Class Re-Assign Successfully", EntityJsonIgnore.TeacherIgnore);

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }



        #endregion
    }
}