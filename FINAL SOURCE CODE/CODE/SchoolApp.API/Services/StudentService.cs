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
    public class StudentService
    {
        public SchoolAppEntities entity;
        ClassManagement objClassManagement;
        UserRoleService _userRoleService;

        public StudentService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
            objClassManagement = new ClassManagement();
            _userRoleService = new UserRoleService();
        }

        //public ReturnResponce GetStudentList()
        //{
        //    try
        //    {
        //        var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true).ToList();
        //        return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ReturnResponce(ex.Message);
        //        throw;
        //    }
        //}

        //public ReturnResponce GetStudentList(int SchoolId)
        //{
        //    try
        //    {
        //        var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList();
        //        return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ReturnResponce(ex.Message);
        //        throw;
        //    }
        //}
        //public ReturnResponce GetStudentList(int SchoolId, int ClassId)
        //{
        //    try
        //    {
        //        var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId && p.ClassID == ClassId).ToList();
        //        return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ReturnResponce(ex.Message);
        //        throw;
        //    }
        //}


        public ReturnResponce GetStudentList(int SchoolId = 0, int ClassId = 0)
        {
            try
            {
                var responce = entity.ISStudents.Where(p => p.Deleted == true && p.Active == true).ToList();

                if (SchoolId != 0)
                {
                    responce = responce.Where(w => w.SchoolID == SchoolId).ToList();
                }
                if (ClassId != 0)
                {
                    responce = responce.Where(w => w.ClassID == ClassId).ToList();
                }

                return new ReturnResponce(responce, EntityJsonIgnore.StudentIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        public ReturnResponce StudentClass(int SchoolID, int LoginUserType, int loginUserId)
        {
            List<MISClass> Result = new List<MISClass>();

            if (LoginUserType != (int)EnumsManagement.USERTYPE.SCHOOL && LoginUserType != (int)EnumsManagement.USERTYPE.ADMIN && LoginUserType != (int)EnumsManagement.USERTYPE.TEACHER)
            {
                return new ReturnResponce("LoginUserType Should be Admin(School) or Teacher.For Other UserType has access denied");
            }

            try
            {
                if (LoginUserType == (int)EnumsManagement.USERTYPE.TEACHER)
                {

                    ISTeacher objTeacher = entity.ISTeachers.SingleOrDefault(p => p.ID == loginUserId && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Deleted == true);
                    Result = objClassManagement.ClassList(objTeacher.SchoolID, "", 0).Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Office && p.TypeID != (int)EnumsManagement.CLASSTYPE.Club && p.TypeID != (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).OrderBy(r => r.Name).ToList();
                    return new ReturnResponce(Result, EntityJsonIgnore.ClassesIgnore);

                }
                else
                {
                    ISSchool ObjSchool = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        Result = objClassManagement.ClassList(loginUserId, "", 0).Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Office && p.TypeID != (int)EnumsManagement.CLASSTYPE.Club && p.TypeID != (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).OrderBy(r => r.Name).ToList();
                        return new ReturnResponce(Result, EntityJsonIgnore.ClassesIgnore);
                    }
                    else
                    {
                        Result = (from item in entity.ISClasses.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                  select new MISClass
                                  {
                                      ID = item.ID,
                                      SchoolID = item.SchoolID,
                                      Name = item.Name,
                                      YearName = !String.IsNullOrEmpty(item.Year) ? objClassManagement.getyeardata(item.Year) : "N/A",
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
                                      StudentCount = item.Name.Contains("Outside") ? objClassManagement.getExternalStudentCount(item.SchoolID) : objClassManagement.getStudentCount(item.ID, item.SchoolID),
                                  }).ToList().OrderBy(r => r.Name).ToList();
                        return new ReturnResponce(Result, EntityJsonIgnore.ClassesIgnore);
                    }
                }
            }
            catch (Exception ex)
            {

                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce StudentClassWithFilter(int SchoolID, int LoginUserType, int loginUserId, int? ClassId = 0, string Year = "")
        {
            List<MISClass> Result = new List<MISClass>();


            if (LoginUserType != (int)EnumsManagement.USERTYPE.SCHOOL && LoginUserType != (int)EnumsManagement.USERTYPE.ADMIN && LoginUserType != (int)EnumsManagement.USERTYPE.TEACHER)
            {
                return new ReturnResponce("LoginUserType Should be Admin(School) or Teacher.For Other UserType has access denied");
            }

            try
            {
                if (ClassId > 0)
                {
                    Result = objClassManagement.ClassList(SchoolID, Year, 0).Where(p => p.ID == ClassId).ToList();
                    return new ReturnResponce(Result, EntityJsonIgnore.ClassesIgnore);
                }
                else
                {
                    return StudentClass(SchoolID, LoginUserType, loginUserId);
                }

            }
            catch (Exception ex)
            {

                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetStudentsByClassId(int SchoolID, int ClassId, string StudentName = "", string StudentNumber = "")
        {
            try
            {
                List<MISStudent> Obj = new List<MISStudent>();

                ISSchool ObjSchool = entity.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    ISClass objClass = entity.ISClasses.SingleOrDefault(p => p.ID == ClassId);
                    Obj = objClassManagement.StudentListByClass(SchoolID, ClassId);
                    if (StudentName != "")
                    {
                        Obj = Obj.Where(p => p.StudentName.Trim().ToLower().Contains(StudentName.Trim().ToLower())).ToList();
                    }
                    if (StudentNumber != "")
                    {
                        Obj = Obj.Where(p => p.StudentNo.Trim().ToLower().Contains(StudentNumber.Trim().ToLower())).ToList();
                    }


                    if (Obj.Count <= 0)
                    {
                        return new ReturnResponce(Obj.ToList(), "There are No Student in " + objClass.Name, EntityJsonIgnore.StudentIgnore);
                    }
                    else
                    {
                        return new ReturnResponce(Obj.ToList(), EntityJsonIgnore.StudentIgnore);
                    }
                }
                else
                {
                    ISClass objClass = entity.ISClasses.SingleOrDefault(p => p.ID == ClassId);
                    if (objClass.Name.Contains("Outside"))
                    {
                        Obj = objClassManagement.StudentListByExtClass(SchoolID, ClassId);
                        if (StudentName != "")
                        {
                            Obj = Obj.Where(p => p.StudentName.Trim().ToLower().Contains(StudentName.Trim().ToLower())).ToList();
                        }
                        if (StudentNumber != "")
                        {
                            Obj = Obj.Where(p => p.StudentNo.Trim().ToLower().Contains(StudentNumber.Trim().ToLower())).ToList();
                        }



                        if (Obj.Count <= 0)
                        {
                            return new ReturnResponce(Obj.ToList(), "There are No Student in " + objClass.Name, EntityJsonIgnore.StudentIgnore);

                        }
                        else
                        {
                            return new ReturnResponce(Obj.ToList(), EntityJsonIgnore.StudentIgnore);
                        }
                    }
                    else
                    {
                        Obj = objClassManagement.StudentListByClass(SchoolID, ClassId);
                        if (StudentName != "")
                        {
                            Obj = Obj.Where(p => p.StudentName.Trim().ToLower().Contains(StudentName.Trim().ToLower())).ToList();
                        }
                        if (StudentNumber != "")
                        {
                            Obj = Obj.Where(p => p.StudentNo.Trim().ToLower().Contains(StudentNumber.Trim().ToLower())).ToList();
                        }


                        if (Obj.Count <= 0)
                        {
                            return new ReturnResponce(Obj.ToList(), "There are No Student in " + objClass.Name, EntityJsonIgnore.StudentIgnore);
                        }
                        else
                        {
                            return new ReturnResponce(Obj.ToList(), EntityJsonIgnore.StudentIgnore);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetStudentProfileData(int StudentId)
        {
            try
            {
                StudentProfileData studentProfileData = new StudentProfileData();

                ClassManagement objClassManagement = new ClassManagement();
                MISStudent Obj = objClassManagement.GetStudentInfo(StudentId);
                if (Obj != null)
                {
                    studentProfileData.StudentId = StudentId;
                    studentProfileData.ParantName1 = Obj.ParantName1;
                    studentProfileData.ParantEmail1 = Obj.ParantEmail1;
                    studentProfileData.ParantPhone1 = Obj.ParantPhone1;
                    studentProfileData.ParantRelation1 = Obj.ParantRelation1;
                    studentProfileData.ParantName2 = Obj.ParantName2;
                    studentProfileData.ParantEmail2 = Obj.ParantEmail2;
                    studentProfileData.ParantPhone2 = Obj.ParantPhone2;
                    studentProfileData.ParantRelation2 = Obj.ParantRelation2;
                    studentProfileData.ClassName = Obj.ClassName;
                    studentProfileData.StudentName = Obj.StudentName;
                    studentProfileData.StudentNo = Obj.StudentNo;
                    studentProfileData.ProfilePhoto = Obj.Photo;
                    studentProfileData.ClassID = Obj.ClassID;

                    return new ReturnResponce(studentProfileData, "Success");

                }
                else
                {
                    return new ReturnResponce("Student Profile not found with provided student Id");
                }
            }
            catch (Exception ex)
            {

                return new ReturnResponce("Error:- " + ex.Message);
            }

        }

        public ReturnResponce DeleteStudent(int StudentId, int LoginUserId)
        {

            try
            {
                ISStudent objStudent = entity.ISStudents.SingleOrDefault(p => p.ID == StudentId);
                if (objStudent != null)
                {
                    objStudent.Active = false;
                    objStudent.Deleted = false;
                    objStudent.DeletedBy = LoginUserId;
                    objStudent.DeletedDateTime = DateTime.Now;
                    entity.SaveChanges();
                    LogManagement.AddLog("Student Deleted Successfully " + "ID : " + StudentId + " Document Category : StudentProfile", "Student");

                    return new ReturnResponce(null, "Student Deleted Successfully");
                }
                else
                {
                    return new ReturnResponce("Student Profile not found with provided student Id");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce("Error:- " + ex.Message);
            }
        }

        public ReturnResponce UpdateStudentNew(AddEditStudentModel model)
        {
            try
            {
                int ID = model.Id;

                if (model.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    IList<ISStudent> obj2 = entity.ISStudents.Where(a => a.ID != ID && a.StudentNo == model.StudentNo && a.SchoolID == model.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        return new ReturnResponce("Not Allowed. Student Number Must Be Unique");
                    }
                    else if (entity.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == model.StudentName.ToLower() &&
                    (a.ParantEmail1.ToLower() == model.ParantEmail1.ToLower() || a.ParantEmail2.ToLower() == model.ParantEmail1.ToLower())
                    && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        return new ReturnResponce("Student Name With Primary Parent Email already Exist");
                    }
                    else if (model.ParantEmail2.Length > 0 && entity.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == model.ParantEmail2.ToLower()
                   && (a.ParantEmail2.ToLower() == model.ParantEmail2.ToLower() || a.ParantEmail1.ToLower() == model.ParantEmail2.ToLower())
                   && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        return new ReturnResponce("Student Name With Secondary Parent Email already Exist");
                    }
                    else
                    {
                        ISStudent objStudent = entity.ISStudents.SingleOrDefault(p => p.ID == ID);
                        if (objStudent != null)
                        {
                            if (objStudent.ParantEmail1.ToLower() != model.ParantEmail1.ToLower())
                            {
                                ///this indicate that student primary parent changed or removed.
                                InsertArchiveParentDetail(objStudent, model.LoginUserTypeId);
                            }
                            ///this indicate that student secondary parent changes or removed. Here there are two possibility
                            if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != model.ParantEmail1.ToLower())
                            {
                                InsertArchiveParentDetail(objStudent, model.LoginUserTypeId, false);
                            }
                            else if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != model.ParantEmail2.ToLower() &&
                                objStudent.ParantEmail2.ToLower() != model.ParantEmail1.ToLower())
                            {
                                InsertArchiveParentDetail(objStudent, model.LoginUserTypeId, false);
                            }

                            objStudent.StudentName = model.StudentName;
                            if (objStudent.ClassID != model.ClassID)
                            {
                                ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                                ObjReassign.SchoolID = model.SchoolID;
                                ObjReassign.FromClass = objStudent.ClassID;
                                ObjReassign.ToClass = model.ClassID;
                                ObjReassign.Date = DateTime.Now;
                                ObjReassign.StduentID = ID;
                                ObjReassign.Active = true;
                                ObjReassign.Deleted = true;
                                ObjReassign.CreatedBy = model.LoginUserId;
                                ObjReassign.CreatedByType = model.LoginUserTypeId;
                                ObjReassign.CreatedDateTime = DateTime.Now;
                                entity.ISStudentReassignHistories.Add(ObjReassign);
                                entity.SaveChanges();
                            }
                            objStudent.ClassID = model.ClassID;
                            objStudent.StudentNo = model.StudentNo;
                            objStudent.ParantName1 = model.ParantName1;
                            if (objStudent.ParantEmail1 != model.ParantEmail1)
                            {
                                //string Message = "";
                                //ISSchool objSchool = entity.ISSchools.SingleOrDefault(p => p.ID == model.SchoolID && p.Deleted == true);
                                //if (!string.IsNullOrWhiteSpace(model.ParantEmail1))
                                //{
                                //    Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your E-Mail has been changed to <br/><br/> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>",  model.ParantName1, model.ParantEmail1, EncryptionHelper.Decrypt(objStudent.ParantPassword1), objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                //    _EmailManagement.SendEmail(objStudent.ParantEmail1, "Your Account Email Changed", Message);
                                //}
                                objStudent.ParantEmail1 = model.ParantEmail1;
                            }
                            else
                            {
                                objStudent.ParantEmail1 = model.ParantEmail1;
                            }
                            objStudent.ParantPhone1 = model.ParantPhone1;
                            objStudent.ParantRelation1 = model.ParantRelation1;
                            objStudent.ParantName2 = model.ParantName2;
                            if (objStudent.ParantEmail2 != model.ParantEmail2)
                            {
                                //string Message = "";
                                //ISSchool objSchool = entity.ISSchools.SingleOrDefault(p => p.ID == model.SchoolID && p.Deleted == true);
                                //if (!string.IsNullOrWhiteSpace(model.ParantEmail2) && !String.IsNullOrEmpty(objStudent.ParantEmail2))
                                //{
                                //    Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Your E-Mail has been changed to <br/><br/> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", model.ParantName2, model.ParantEmail2, EncryptionHelper.Decrypt(objStudent.ParantPassword2), objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                //    _EmailManagement.SendEmail(objStudent.ParantEmail2, "Your Account Email Changed", Message);
                                //}
                                objStudent.ParantEmail2 = model.ParantEmail2;
                            }
                            else
                            {
                                objStudent.ParantEmail2 = model.ParantEmail2;
                            }

                            objStudent.ParantPhone2 = model.ParantPhone2;
                            objStudent.ParantRelation2 = model.ParantRelation2;
                            objStudent.ModifyBy = model.LoginUserId;
                            objStudent.ModifyDateTime = DateTime.Now;
                            entity.SaveChanges();


                            ISPicker objPicker = entity.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == objStudent.ParantName1 && p.Active == true && p.Deleted == true);
                            if (objPicker != null)
                            {
                                objPicker.SchoolID = model.SchoolID;
                                objPicker.ParentID = ID;
                                objPicker.StudentID = ID;
                                objPicker.FirstName = model.ParantName1 + "(" + model.ParantRelation1 + ")";
                                objPicker.Email = model.ParantEmail1;
                                objPicker.Phone = model.ParantPhone1;
                                objPicker.ModifyBy = model.LoginUserId;
                                objPicker.ModifyDateTime = DateTime.Now;
                                entity.SaveChanges();
                            }
                            if (model.ParantName2 != "" && model.ParantEmail2 != "" && !string.IsNullOrWhiteSpace(model.ParantRelation2))
                            {

                                ISPicker objPickers = entity.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == objStudent.StudentName && p.Deleted == true);
                                if (objPickers != null)
                                {
                                    objPickers.SchoolID = model.SchoolID;
                                    objPickers.ParentID = ID;
                                    objPickers.StudentID = ID;
                                    objPickers.FirstName = model.ParantName2 + "(" + model.ParantRelation2 + ")";
                                    objPickers.Email = model.ParantEmail2;
                                    objPickers.Phone = model.ParantPhone2;
                                    objPickers.ModifyBy = model.LoginUserId;
                                    objPickers.ModifyDateTime = DateTime.Now;
                                    entity.SaveChanges();
                                }
                                else
                                {
                                    ISPicker objPickers1 = new ISPicker();
                                    objPickers1.SchoolID = model.SchoolID;
                                    objPickers1.ParentID = ID;
                                    objPickers1.StudentID = ID;
                                    objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                    objPickers1.FirstName = model.ParantName2 + "(" + model.ParantRelation2 + ")";
                                    objPickers1.Photo = GetExistingPickerImage(model.ParantEmail2);
                                    objPickers1.Email = model.ParantEmail2;
                                    objPickers1.Phone = model.ParantPhone2;
                                    objPickers1.OneOffPickerFlag = false;
                                    objPickers1.ActiveStatus = "Active";
                                    objPickers1.Active = true;
                                    objPickers1.Deleted = true;
                                    objPickers1.CreatedBy = model.LoginUserId;
                                    objPickers1.CreatedDateTime = DateTime.Now;
                                    entity.ISPickers.Add(objPickers1);
                                    entity.SaveChanges();
                                    if (entity.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                    {
                                        ISPickerAssignment objAssigns = new ISPickerAssignment();
                                        objAssigns.PickerId = objPickers1.ID;
                                        objAssigns.StudentID = ID;
                                        objAssigns.RemoveChildStatus = 0;
                                        entity.ISPickerAssignments.Add(objAssigns);
                                        entity.SaveChanges();
                                    }
                                }
                            }
                            else
                            {

                                ISPicker objPickers = entity.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && p.SchoolID == model.SchoolID && p.FirstName == objStudent.StudentName);
                                if (objPickers != null)
                                {
                                    objPickers.Active = false;
                                    objPickers.Deleted = false;
                                    objPickers.DeletedBy = model.LoginUserId;
                                    objPickers.DeletedDateTime = DateTime.Now;
                                    entity.SaveChanges();
                                }
                            }

                            return new ReturnResponce(objStudent, "Student Updated Successfully", EntityJsonIgnore.StudentIgnore);
                        }
                        else
                        {
                            return new ReturnResponce(objStudent, "Student Not Updated Successfully", EntityJsonIgnore.StudentIgnore);
                        }
                    }
                }
                else
                {
                    IList<ISStudent> obj2 = entity.ISStudents.Where(a => a.ID != ID && a.StudentNo == model.StudentNo && a.SchoolID == model.SchoolID && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        return new ReturnResponce("Not Allowed. Student Number Must Be Unique");
                    }
                    else if (entity.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == model.StudentName.ToLower() &&
                    (a.ParantEmail1.ToLower() == model.ParantEmail1.ToLower() || a.ParantEmail2.ToLower() == model.ParantEmail1.ToLower())
                    && a.SchoolID == model.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        return new ReturnResponce("Student Name With Primary Parent Email already Exist");
                    }
                    else if (model.ParantEmail2.Length > 0 && entity.ISStudents.Where(a => a.ID != ID && a.StudentName.ToLower() == model.StudentName.ToLower() &&
                    (a.ParantEmail2.ToLower() == model.ParantEmail2.ToLower() || a.ParantEmail1.ToLower() == model.ParantEmail2.ToLower()) &&
                    a.SchoolID == model.SchoolID && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        return new ReturnResponce("Student Name With Secondary Parent Email already Exist");
                    }

                    else
                    {
                        ISStudent objStudent = entity.ISStudents.SingleOrDefault(p => p.ID == ID);
                        if (objStudent != null)
                        {
                            if (objStudent.ParantEmail1.ToLower() != model.ParantEmail1.ToLower())
                            {
                                ///this indicate that student primary parent changed or removed.
                                InsertArchiveParentDetail(objStudent, model.LoginUserTypeId);
                            }

                            ///this indicate that student secondary parent changes or removed. Here there are two possibility
                            if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != model.ParantEmail1.ToLower())
                            {
                                InsertArchiveParentDetail(objStudent, model.LoginUserTypeId, false);
                            }
                            else if (!string.IsNullOrEmpty(objStudent.ParantEmail2) && objStudent.ParantEmail2.ToLower() != model.ParantEmail2.ToLower() &&
                                objStudent.ParantEmail2.ToLower() != model.ParantEmail1.ToLower())
                            {
                                InsertArchiveParentDetail(objStudent, model.LoginUserTypeId, false);
                            }

                            objStudent.StudentName = model.StudentName;
                            if (objStudent.ClassID != model.ClassID)
                            {
                                ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                                ObjReassign.SchoolID = model.SchoolID;
                                ObjReassign.FromClass = objStudent.ClassID;
                                ObjReassign.ToClass = model.ClassID;
                                ObjReassign.Date = DateTime.Now;
                                ObjReassign.StduentID = ID;
                                ObjReassign.Active = true;
                                ObjReassign.Deleted = true;
                                ObjReassign.CreatedBy = model.LoginUserId;
                                ObjReassign.CreatedByType = model.LoginUserTypeId == (int)EnumsManagement.USERTYPE.TEACHER ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                                ObjReassign.CreatedDateTime = DateTime.Now;
                                entity.ISStudentReassignHistories.Add(ObjReassign);
                                entity.SaveChanges();
                            }
                            objStudent.ClassID = model.ClassID;
                            objStudent.StudentNo = model.StudentNo;
                            objStudent.ParantName1 = model.ParantName1;
                            if (objStudent.ParantEmail1 != model.ParantEmail1)
                            {
                                objStudent.ParantEmail1 = model.ParantEmail1;
                            }
                            else
                            {
                                objStudent.ParantEmail1 = model.ParantEmail1;
                            }
                            objStudent.ParantPhone1 = model.ParantPhone1;
                            objStudent.ParantRelation1 = model.ParantRelation1;
                            objStudent.ParantName2 = model.ParantName2;
                            if (objStudent.ParantEmail2 != model.ParantEmail2)
                            {
                                objStudent.ParantEmail2 = model.ParantEmail2;
                            }
                            else
                            {
                                objStudent.ParantEmail2 = model.ParantEmail2;
                            }
                            objStudent.ParantPhone2 = model.ParantPhone2;
                            objStudent.ParantRelation2 = !string.IsNullOrWhiteSpace(model.ParantRelation2) ? model.ParantRelation2 : "";
                            objStudent.ModifyBy = model.LoginUserId;
                            objStudent.ModifyDateTime = DateTime.Now;
                            entity.SaveChanges();


                            ISPicker objPicker = entity.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == objStudent.ParantName1 && p.Active == true && p.Deleted == true);
                            if (objPicker != null)
                            {
                                objPicker.SchoolID = model.SchoolID;
                                objPicker.ParentID = ID;
                                objPicker.StudentID = ID;
                                objPicker.FirstName = model.ParantName1 + "(" + model.ParantRelation1 + ")";
                                objPicker.Email = model.ParantEmail1;
                                objPicker.Phone = model.ParantPhone1;
                                objPicker.ModifyBy = model.LoginUserId;
                                objPicker.ModifyDateTime = DateTime.Now;
                                entity.SaveChanges();
                            }
                            if (!string.IsNullOrWhiteSpace(model.ParantName2) && model.ParantEmail2 != "" && !string.IsNullOrWhiteSpace(model.ParantRelation2))
                            {

                                ISPicker objPickers = entity.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == objStudent.StudentName && p.Deleted == true);
                                if (objPickers != null)
                                {
                                    objPickers.SchoolID = model.SchoolID;
                                    objPickers.ParentID = ID;
                                    objPickers.StudentID = ID;
                                    objPickers.FirstName = model.ParantName2 + "(" + model.ParantRelation2 + ")";
                                    objPickers.Email = model.ParantEmail2;
                                    objPickers.Phone = model.ParantPhone2;
                                    objPickers.ModifyBy = model.LoginUserId;
                                    objPickers.ModifyDateTime = DateTime.Now;
                                    entity.SaveChanges();
                                }
                                else
                                {
                                    ISPicker objPickers1 = new ISPicker();
                                    objPickers1.SchoolID = model.SchoolID;
                                    objPickers1.ParentID = ID;
                                    objPickers1.StudentID = ID;
                                    objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                    objPickers1.FirstName = model.ParantName2 + "(" + model.ParantRelation2 + ")";
                                    objPickers1.Photo = GetExistingPickerImage(model.ParantEmail2);
                                    objPickers1.Email = model.ParantEmail2;
                                    objPickers1.Phone = model.ParantPhone2;
                                    objPickers1.OneOffPickerFlag = false;
                                    objPickers1.ActiveStatus = "Active";
                                    objPickers1.Active = true;
                                    objPickers1.Deleted = true;
                                    objPickers1.CreatedBy = model.LoginUserId;
                                    objPickers1.CreatedDateTime = DateTime.Now;
                                    entity.ISPickers.Add(objPickers1);
                                    entity.SaveChanges();
                                    if (entity.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                    {
                                        ISPickerAssignment objAssigns = new ISPickerAssignment();
                                        objAssigns.PickerId = objPickers1.ID;
                                        objAssigns.StudentID = ID;
                                        objAssigns.RemoveChildStatus = 0;
                                        entity.ISPickerAssignments.Add(objAssigns);
                                        entity.SaveChanges();
                                    }
                                }
                            }
                            else
                            {

                                ISPicker objPickers = entity.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && p.SchoolID == model.SchoolID && p.FirstName == objStudent.StudentName);
                                if (objPickers != null)
                                {
                                    objPickers.Active = false;
                                    objPickers.Deleted = false;
                                    objPickers.DeletedBy = model.LoginUserId;
                                    objPickers.DeletedDateTime = DateTime.Now;
                                    entity.SaveChanges();
                                }
                            }

                            return new ReturnResponce(objStudent, "Student Updated Successfully", EntityJsonIgnore.StudentIgnore);
                        }
                        else
                        {
                            return new ReturnResponce(objStudent, "Student Not Updated Successfully", EntityJsonIgnore.StudentIgnore);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        private void InsertArchiveParentDetail(ISStudent iSStudent, int LoginUserTypeId, bool isPrimaryParent = true)
        {
            BckupParent bckupParent = new BckupParent();

            bckupParent.Name = isPrimaryParent ? iSStudent.ParantName1 : iSStudent.ParantName2;
            bckupParent.StudentId = iSStudent.ID;
            bckupParent.ParentId = iSStudent.ID;
            bckupParent.Email = isPrimaryParent ? iSStudent.ParantEmail1 : iSStudent.ParantEmail2;
            bckupParent.Relationship = isPrimaryParent ? iSStudent.ParantRelation1 : iSStudent.ParantRelation2;
            bckupParent.Phone = isPrimaryParent ? iSStudent.ParantPhone1 : iSStudent.ParantPhone2;
            bckupParent.DeletedDate = DateTime.Now;
            bckupParent.WhoDeleted = _userRoleService.GetLoginUserRole(LoginUserTypeId);
            entity.BckupParents.Add(bckupParent);
            entity.SaveChanges();
        }

        //public void EmailManage(ISStudent _Student)
        //{
        //    EmailManagement _EmailManagement = new EmailManagement();

        //    string PP1body = String.Empty;
        //    string PP2Body = String.Empty;
        //    string TeacherBody = String.Empty;

        //    ISSchool _School = entity.ISSchools.SingleOrDefault(x => x.ID == model.SchoolID);
        //    string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
        //    string tblPP1body = string.Empty;
        //    string tblPP2Body = string.Empty;
        //    string tblTeacherBody = string.Empty;
        //    tblPP1body = @"<table>
        //                    <tr style='float:left;'>
        //                        <td>
        //                         Dear " + _Student.ParantName1 + @" ,<br/><br/>
        //                        </td><br/>
        //                    </tr>
        //                    <tr><br/>
        //                        <td>
        //                            Changes made to student account " + _Student.StudentName + " by " + _School.Name + @"<br/>
        //                        </td>
        //                        </tr>
        //                        <tr>
        //                            <td>
        //                                Student records on the database is being updated with the current information. Please check your records and validate changes that have been made.
        //                            </td>
        //                        </tr>
        //                        <tr>
        //                            <td>
        //                                <br />
        //                                Any queries  should be directed to the School Administrator
        //                           </td>
        //                        </tr>
        //                        </table>";
        //    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
        //    {
        //        PP1body += reader.ReadToEnd();
        //    }
        //    PP1body = PP1body.Replace("{Body}", tblPP1body);
        //    _EmailManagement.SendEmail(_Student.ParantEmail1, "Update Student Account Notification", PP1body);

        //    if (!String.IsNullOrEmpty(_Student.ParantName2) && !String.IsNullOrEmpty(_Student.ParantEmail2))
        //    {
        //        tblPP2Body = @"<table>
        //                    <tr style='float:left;'>
        //                        <td>
        //                         Dear " + _Student.ParantName2 + @" ,<br/><br/>
        //                        </td><br/>
        //                    </tr>
        //                    <tr><br/>
        //                        <td>
        //                            Changes made to student account " + _Student.StudentName + " by " + _School.Name + @"<br/>
        //                        </td>
        //                        </tr>
        //                        <tr>
        //                            <td>
        //                                Student records on the database is being updated with the current information. Please check your records and validate changes that have been made.
        //                            </td>
        //                        </tr>
        //                        <tr>
        //                            <td>
        //                                <br />
        //                                Any queries  should be directed to the School Administrator
        //                           </td>
        //                        </tr>
        //                        </table>";

        //        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
        //        {
        //            PP2Body += reader.ReadToEnd();
        //        }
        //        PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

        //        _EmailManagement.SendEmail(_Student.ParantEmail2, "Update Student Account Notification", PP2Body);
        //    }

        //}

        public string GetExistingPickerImage(string emailId)
        {
            string photo = "Upload/user.jpg";
            ISPicker iSPicker = entity.ISPickers.FirstOrDefault(r => r.Email.ToLower() == emailId.ToLower() && r.Deleted == true && r.Active == true);

            if (iSPicker != null)
                photo = iSPicker.Photo;

            return photo;
        }

        public ReturnResponce AddUpdateStudent(AddStudent model, int AdminId)
        {
            try
            {
                ISStudent insertUpdate = new ISStudent();

                if (model.Id > 0)
                {
                    insertUpdate = entity.ISStudents.Where(w => w.ID == model.Id).FirstOrDefault();
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

                        insertUpdate.ModifyDateTime = DateTime.Now;
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
    }
}