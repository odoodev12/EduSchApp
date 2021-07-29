using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class RolesManagement

    {
        public List<MISUserRole> AdminRoleList(int SchoolID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();


            List<MISUserRole> objList = (from item in DB.ISUserRoles.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                         select new MISUserRole
                                         {
                                             ID = item.ID,
                                             SchoolID = item.SchoolID,
                                             RoleName = item.RoleName,
                                             RoleType = item.RoleType,
                                             RoleTypes = item.RoleType == 1 ? "Teaching Role" : item.RoleType == 2 ? "Non Teaching Role" : "",
                                             ManageTeacherFlag = item.ManageTeacherFlag,
                                             ManageClassFlag = item.ManageClassFlag,
                                             ManageSupportFlag = item.ManageSupportFlag,
                                             ManageStudentFlag = item.ManageStudentFlag,
                                             ManageViewAccountFlag = item.ManageViewAccountFlag,
                                             ManageNonTeacherFlag = item.ManageNonTeacherFlag,
                                             ManageHolidayFlag = item.ManageHolidayFlag,
                                             Active = item.Active,
                                             Deleted = item.Deleted,
                                             strCreateBy = GetCreatedBy(item.CreatedBy.Value),
                                             strCreatedDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy"),
                                             Status = item.Active == true ? "Active" : "InActive",
                                             IsDefaultRole = (item.RoleName == "Admin" && (item.RoleType == 1 || item.RoleType == 2)) ||
                                                                (item.RoleName == "Standard" && (item.RoleType == 1 || item.RoleType == 2)),
                                         }).ToList();


            return objList;
        }
        public string GetCreatedBy(int CreatedByID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.ID == CreatedByID);
            if (obj != null)
            {
                return string.Format("{0} {1}", obj.AdminFirstName, obj.AdminLastName);
            }
            else
            {
                return "";
            }
        }
        public ISUserRole CreateorUpdateRole(int RollID, int SchoolID, string RollName, int RoleType, bool ManageTeacherFlag, bool ManageClassFlag, bool ManageSupportFlag, bool ManageStudentFlag, bool ManageViewAccountFlag, bool ManageNonTeacherFlag, bool ManageHolidayFlag, bool Active)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISUserRole obj = new ISUserRole();
            if (RollID != 0)
            {

                obj = DB.ISUserRoles.SingleOrDefault(p => p.ID == RollID && p.Deleted == true);
                obj.RoleName = RollName;
                obj.ManageTeacherFlag = ManageTeacherFlag;
                obj.ManageClassFlag = ManageClassFlag;
                obj.ManageSupportFlag = ManageSupportFlag;
                obj.ManageStudentFlag = ManageStudentFlag;
                obj.ManageViewAccountFlag = ManageViewAccountFlag;
                obj.ManageNonTeacherFlag = ManageNonTeacherFlag;
                obj.ManageHolidayFlag = ManageHolidayFlag;
                if (Active == false)
                {
                    ISUserRole ObjUser = DB.ISUserRoles.SingleOrDefault(p => p.RoleName == "Standard" && p.RoleType == RoleType && p.SchoolID == SchoolID && p.Active == true && p.Deleted == true);
                    if (ObjUser != null)
                    {
                        List<ISTeacher> ObjList = DB.ISTeachers.Where(p => p.RoleID == RollID && p.Active == true && p.Deleted == true).ToList();
                        if (ObjList.Count > 0)
                        {
                            foreach (var item in ObjList)
                            {
                                item.RoleID = ObjUser.ID;
                                DB.SaveChanges();
                            }
                        }
                    }
                }
                obj.Active = Active;
                obj.ModifyBy = SchoolID;
                obj.ModifyDateTime = DateTime.Now;
                DB.SaveChanges();

            }
            else
            {
                obj = new ISUserRole();
                obj.RoleName = RollName;
                obj.SchoolID = SchoolID;
                obj.RoleType = RoleType;
                obj.ManageTeacherFlag = ManageTeacherFlag;
                obj.ManageClassFlag = ManageClassFlag;
                obj.ManageSupportFlag = ManageSupportFlag;
                obj.ManageStudentFlag = ManageStudentFlag;
                obj.ManageViewAccountFlag = ManageViewAccountFlag;
                obj.ManageNonTeacherFlag = ManageNonTeacherFlag;
                obj.ManageHolidayFlag = ManageHolidayFlag;
                obj.CreatedBy = SchoolID;
                obj.Active = Active;
                obj.Deleted = true;
                obj.CreatedDateTime = DateTime.Now;
                DB.ISUserRoles.Add(obj);
                DB.SaveChanges();
            }
            return obj;
        }
        public ISUserRole DeleteRole(int RoleID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISUserRole obj = new ISUserRole();
            if (RoleID != 0)
            {
                obj = DB.ISUserRoles.SingleOrDefault(p => p.ID == RoleID && p.Deleted == true);
                obj.Active = false;
                obj.Deleted = false;
                obj.DeletedBy = Authentication.LogginSchool.ID;
                obj.DeletedDateTime = DateTime.Now;
                DB.SaveChanges();
            }
            return obj;
        }
        public MISUserRole GetRole(int ID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISUserRole Obj = DB.ISUserRoles.SingleOrDefault(p => p.ID == ID);
            MISUserRole objMRole = new MISUserRole();
            if (Obj != null)
            {
                objMRole.ID = Obj.ID;
                objMRole.SchoolID = Obj.SchoolID;
                objMRole.RoleName = Obj.RoleName;
                objMRole.RoleType = Obj.RoleType;
                objMRole.RoleTypes = Obj.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING ? "Teaching Role" : "Non Teaching Role";
                objMRole.ManageClassFlag = Obj.ManageClassFlag;
                objMRole.ManageStudentFlag = Obj.ManageStudentFlag;
                objMRole.ManageSupportFlag = Obj.ManageSupportFlag;
                objMRole.ManageTeacherFlag = Obj.ManageTeacherFlag;
                objMRole.ManageViewAccountFlag = Obj.ManageViewAccountFlag;
                objMRole.ManageNonTeacherFlag = Obj.ManageNonTeacherFlag;
                objMRole.ManageHolidayFlag = Obj.ManageHolidayFlag;
                objMRole.Active = Obj.Active;
            }
            return objMRole;
        }
    }
}
