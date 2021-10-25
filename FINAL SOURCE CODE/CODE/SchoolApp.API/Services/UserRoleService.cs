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
    public class UserRoleService
    {
        public SchoolAppEntities entity;

        public UserRoleService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region Role
        public ReturnResponce GetUsersRoleList(int SchoolId, int RoleType)
        {
            try
            {
                if (SchoolId > 0)
                {
                    var response = entity.ISUserRoles.Where(p => p.RoleType == RoleType && p.SchoolID == SchoolId && p.Deleted == true && p.Active == true).ToList();
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
            }
        }

        public ReturnResponce GetAdminRoleList(int SchoolId, string RoleName, int RoleTypeId, bool? Active)
        {
            try
            {
                if (SchoolId > 0)
                {
                    RolesManagement objRolesManagement = new RolesManagement();
                    List<MISUserRole> ObjList = objRolesManagement.AdminRoleList(SchoolId);

                    if (RoleName != "")
                    {
                        ObjList = ObjList.Where(p => p.RoleName.ToLower() == RoleName.ToLower()).ToList();
                    }

                    if (RoleTypeId > 0)
                    {
                        ObjList = ObjList.Where(p => p.RoleType == RoleTypeId).ToList();
                    }

                    if (Active != null)
                    {
                        if (Active.Value == true)
                        {
                            ObjList = ObjList.Where(p => p.Active == true).ToList();
                        }
                        else
                        {
                            ObjList = ObjList.Where(p => p.Active == false).ToList();
                        }
                    }

                    return new ReturnResponce(ObjList, EntityJsonIgnore.MISUserRoleIgnore);
                }
                else
                {
                    return new ReturnResponce("SchoolId must be grater than 0.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce GetUserRoleDetails(int UserRoleId)
        {
            try
            {
                var response = entity.ISUserRoles.Where(p => p.Deleted == true && p.Active == true && p.ID == UserRoleId).SingleOrDefault();
                return new ReturnResponce(response, new[] { "ISSchool", "ISTeacher" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce AddUserRole(UserRoleAdd model)
        {
            try
            {
                int RID = model.SchoolType == (int)EnumsManagement.SCHOOLTYPE.Standard ? model.RoleType : (int)EnumsManagement.ROLETYPE.TEACHING;

                List<ISUserRole> obj = entity.ISUserRoles.Where(p => p.RoleName == model.RoleName && p.RoleType == RID && p.SchoolID == model.SchoolID && p.Deleted == true).ToList();
                if (obj.Count > 0)
                {
                    return new ReturnResponce("Role Already Exists!!!");                    
                }
                else
                {
                    if (model.ManageClassFlag == true || model.ManageStudentFlag == true || model.ManageSupportFlag == true || model.ManageTeacherFlag == true || model.ManageViewAccountFlag == true || model.ManageNonTeacherFlag || model.ManageHolidayFlag)
                    {
                        ISUserRole insertUpdate = new ISUserRole();

                        insertUpdate.SchoolID = model.SchoolID;
                        insertUpdate.RoleName = model.RoleName;
                        insertUpdate.RoleType = RID;
                        insertUpdate.ManageTeacherFlag = model.ManageTeacherFlag;
                        insertUpdate.ManageClassFlag = model.ManageClassFlag;
                        insertUpdate.ManageSupportFlag = model.ManageSupportFlag;
                        insertUpdate.ManageStudentFlag = model.ManageStudentFlag;
                        insertUpdate.ManageViewAccountFlag = model.ManageViewAccountFlag;
                        insertUpdate.ManageNonTeacherFlag = model.ManageNonTeacherFlag;
                        insertUpdate.ManageHolidayFlag = model.ManageHolidayFlag;
                        insertUpdate.Active = model.Active;

                        insertUpdate.CreatedDateTime = DateTime.Now;
                        insertUpdate.CreatedBy = model.LoginUserId;
                        insertUpdate.Deleted = true;
                        entity.ISUserRoles.Add(insertUpdate);

                        entity.SaveChanges();
                        return new ReturnResponce(insertUpdate, "Role Added Successfully.", new[] { "ISSchool", "ISTeacher" });
                    }
                    else
                    {
                        return new ReturnResponce("Please keep atleast one manage Role selected!!!");
                    }                   
                }
               
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce UpdateUserRole(UserRoleEdit model)
        {
            try
            {
                int RID = model.SchoolType == (int)EnumsManagement.SCHOOLTYPE.Standard ? model.RoleType : (int)EnumsManagement.ROLETYPE.TEACHING;

                List<ISUserRole> obj = entity.ISUserRoles.Where(p => p.RoleName == model.RoleName && p.RoleType == RID && p.SchoolID == model.SchoolID && p.Deleted == true).ToList();
                if (obj.Count > 0)
                {
                    return new ReturnResponce("Role Already Exists!!!");
                }
                else
                {
                    ISUserRole insertUpdate = new ISUserRole();

                    if (model.ID > 0)
                    {
                        insertUpdate = entity.ISUserRoles.Where(w => w.ID == model.ID).FirstOrDefault();
                    }

                    if (insertUpdate != null && insertUpdate.ID > 0)
                    {
                        if (model.ManageClassFlag == true || model.ManageStudentFlag == true || model.ManageSupportFlag == true || model.ManageTeacherFlag == true || model.ManageViewAccountFlag == true || model.ManageNonTeacherFlag || model.ManageHolidayFlag)
                        {

                            insertUpdate.RoleName = model.RoleName;
                            insertUpdate.RoleType = RID;
                            insertUpdate.ManageClassFlag = model.ManageClassFlag;
                            insertUpdate.ManageStudentFlag = model.ManageStudentFlag;
                            insertUpdate.ManageHolidayFlag = model.ManageHolidayFlag;
                            insertUpdate.ManageViewAccountFlag = model.ManageViewAccountFlag;
                            insertUpdate.ManageTeacherFlag = model.ManageTeacherFlag;
                            insertUpdate.ManageNonTeacherFlag = model.ManageNonTeacherFlag;
                            insertUpdate.ManageSupportFlag = model.ManageSupportFlag;
                            insertUpdate.Active = model.Active;
                            insertUpdate.ModifyBy = model.LoginUserId;
                            insertUpdate.ModifyDateTime = DateTime.Now;
                            insertUpdate.Deleted = false;

                            entity.SaveChanges();
                            return new ReturnResponce(insertUpdate, new[] { "ISSchool", "ISTeachers" });
                        }
                        else
                        {
                            return new ReturnResponce("Please keep atleast one manage Role selected!!!");
                        }
                    }
                    else
                    {
                        ///// Error Responce that invalid data here
                        return new ReturnResponce("Invalid model or data, Please try with valid data.");
                    }
                }

            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce DeleteUserRole(int UserRoleId, int UserLoginId)
        {
            try
            {
                ISUserRole insertUpdate = new ISUserRole();
                insertUpdate = entity.ISUserRoles.Where(w => w.ID == UserRoleId).FirstOrDefault();

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
    }
}