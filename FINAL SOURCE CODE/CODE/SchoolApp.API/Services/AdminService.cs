using Newtonsoft.Json;
using SchoolApp.API.Models;
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
        }

        public ReturnResponce GetSchoolTypes()
        {
            try
            {
                var responce = entity.ISSchoolTypes.Where(p => p.Deleted == true && p.Active == true).ToList() as IEnumerable<SchoolType>;
                return new ReturnResponce(responce, new[] { "ISSchools" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }

        #region School List

        public ReturnResponce GetSchool(int SchoolId)
        {
            try
            {
                var responce = entity.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.ID == SchoolId).FirstOrDefault() as IEnumerable<School>;
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }


        #endregion

        #region Class List 

        public ReturnResponce GetClassList()
        {
            try
            {
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true).ToList() as IEnumerable<Classes>;
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
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
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList() as IEnumerable<Classes>;
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
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
                var responce = entity.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.ID == ClassID).FirstOrDefault() as IEnumerable<Classes>;
                return new ReturnResponce(responce, new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion

        #region Teacher List 

        public ReturnResponce GetTeacherList(int SchoolId)
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolId).ToList() as IEnumerable<Teacher>;
                return new ReturnResponce(responce, new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        public ReturnResponce GetTeacherList()
        {
            try
            {
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true ).ToList() as IEnumerable<Teacher>;
                return new ReturnResponce(responce, new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" });
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
                var responce = entity.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.ID == TeacherId).ToList() as IEnumerable<Teacher>;
                return new ReturnResponce(responce, new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" });
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