using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.API.Services;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolApp.API.Controllers
{
    /// <summary>
    /// Admin APIs
    /// </summary>
    public class AdminController : ApiController
    {
        /// <summary>
        /// Admin Service
        /// </summary>
        private AdminService service;

        /// <summary>
        /// Admin API Constructor
        /// </summary>
        public AdminController()
        {
            service = new AdminService();
        }

        #region Admin Login API
        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin/AdminLogin")]
        public ReturnResponce AdminLogin(string username, string password)
        {
            return service.AdminLogin(username, password);
        }
        #endregion

        #region School
        /// <summary>
        /// All SchoolType list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/SchoolTypeList")]
        [HttpGet]
        public ReturnResponce SchoolTypeList()
        {
            return service.GetSchoolTypes();
        }


        /// <summary>
        /// Get school list by SchoolTypeId
        /// </summary>
        /// <param name="SchoolTypeId"></param>
        /// <returns></returns>
        [Route("Admin/SchoolListByType")]
        [HttpGet]
        public ReturnResponce SchoolListByType(int SchoolTypeId)
        {
            return service.GetSchoolByType(SchoolTypeId);
        }

        /// <summary>
        /// Get School details by Id
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/School")]
        [HttpGet]
        public ReturnResponce SchoolDetails(int SchoolId)
        {
            return service.GetSchool(SchoolId);
        }

        /// <summary>
        /// Add new school details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("Admin/School")]
        public ReturnResponce AddSchool(SchoolOtherEntity model, int AdminId)
        {
            return (model.ID == 0 && AdminId > 0) ? service.SetSchool(model, AdminId) : new ReturnResponce("Invalid request or logged in id  must be greater then 0 ");
        }

        /// <summary>
        /// Update existing school details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Admin/School")]
        public ReturnResponce SetSchool(SchoolOtherEntity model, int AdminId)
        {
            return (model.ID > 0 && AdminId > 0) ? service.SetSchool(model, AdminId) : new ReturnResponce("Primary id or logged in id  must be greater then 0 ");
        }
        #endregion

        #region  Class
        /// <summary>
        /// All Admin Class list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/ClassList")]
        [HttpGet]
        public ReturnResponce ClassList()
        {
            return service.GetClassList();
        }
        /// <summary>
        /// Add Class
        /// </summary>
        /// <param name="classDetails"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Class")]
        [HttpPost]
        public ReturnResponce AddClass(ISClass classDetails , int AdminId)
        {
            return (classDetails.ID == 0 && AdminId > 0) ? service.AddUpdateClass(classDetails, AdminId) : new ReturnResponce("Invalid request or logged in id  must be greater then 0 ");            
        }

        /// <summary>
        /// Update Class
        /// </summary>
        /// <param name="classDetails"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Class")]
        [HttpPut]
        public ReturnResponce UpdateClass(ISClass classDetails, int AdminId)
        {
            return (classDetails.ID > 0 && AdminId > 0) ? service.AddUpdateClass(classDetails, AdminId) : new ReturnResponce("Primary id or logged in id  must be greater then 0 ");
        }
        /// <summary>
        /// Get class list by SchoolId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/ClassListBySchoolId")]
        [HttpGet]
        public ReturnResponce ClassListBySchoolId(int SchoolId)
        {
            return service.GetClassList(SchoolId);
        }

        /// <summary>
        /// Get Class details by Id
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetClass")]
        [HttpGet]
        public ReturnResponce GetClass(int ClassId)
        {
            return service.GetClass(ClassId);
        }

        #endregion

        #region Teacher 
        /// <summary>
        /// All Teacher list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/TeacherList")]
        [HttpGet]
        public ReturnResponce TeacherList()
        {
            return service.GetTeacherList();
        }

        /// <summary>
        /// Get Teachers list by SchoolId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/TeacherListBySchoolId")]
        [HttpGet]
        public ReturnResponce TeacherListBySchoolId(int SchoolId)
        {
            return service.GetTeacherList(SchoolId);
        }

        /// <summary>
        /// Get Teacher details by TeacherId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetTeacher")]
        [HttpGet]
        public ReturnResponce GetTeacher(int TeacherId)
        {
            return service.GetTeacher(TeacherId);
        }

        /// <summary>
        /// Get Teacher list by SchoolId and ClassId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetTeacherList")]
        [HttpGet]
        public ReturnResponce GetTeacherBySchoolOrClassId(int SchoolId, int ClassId)
        {
            return service.GetTeacherList(SchoolId, ClassId);
        }

        /// <summary>
        /// Add Techer 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Teacher")]
        [HttpPost]
        public ReturnResponce AddTeacher(ISTeacher model , int AdminId)
        {
            return model.ID == 0 ? service.AddUpdateTeacher(model,AdminId) :  new ReturnResponce("Invalid request");
        }

        /// <summary>
        /// Update Teacher
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Teacher")]
        [HttpPut]
        public ReturnResponce UpdateTeacher(ISTeacher model, int AdminId)
        {
            return model.ID > 0 ? service.AddUpdateTeacher(model, AdminId) : new ReturnResponce("Primary id must be grater then 0");
        }

        [Route("Admin/ReAssignTeacher")]
        [HttpPost]
        public ReturnResponce ReAssignTeacher(ISTeacherReassignHistory model, int AdminId)
        {
            return model.ID == 0 ? service.ReAssignTeacher(model, AdminId) : new ReturnResponce("Invalid request");
        }

        #endregion

        #region Student

        /// <summary>
        /// Get All Student Details
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetAllStudents")]
        [HttpGet]
        public ReturnResponce GetAllStudents()
        {
            return service.GetStudentList();
        }

        /// <summary>
        /// Get All Student Respective to School
        /// </summary>
        /// <param name="SchooolId"></param>
        /// <returns></returns>
        [Route("Admin/GetSchoolStudents")]
        [HttpGet]
        public ReturnResponce GetSchoolStudents(int SchooolId)
        {
            return service.GetStudentList(SchooolId);
        }

        /// <summary>
        /// Get All Student of class Respective to School
        /// </summary>
        /// <param name="ClassId"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetClassStudentsBySchoolId")]
        [HttpGet]
        public ReturnResponce GetClassStudentsBySchoolId(int SchoolId, int ClassId)
        {
            return service.GetStudentList(SchoolId, ClassId);
        }

        /// <summary>
        /// Add Student detail
        /// </summary>
        /// <param name="student"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Student")]
        [HttpPost]
        public ReturnResponce AddStudent(ISStudent student, int AdminId)
        {
            return (student.ID == 0 && AdminId > 0) ? service.AddUpdateStudent(student, AdminId) : new ReturnResponce("Invalid request or logged in id  must be greater then 0 ");
        }

        /// <summary>
        /// update Student detail
        /// </summary>
        /// <param name="student"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Student")]
        [HttpPut]
        public ReturnResponce SetStudent(ISStudent student, int AdminId)
        {
            return (student.ID > 0 && AdminId > 0) ? service.AddUpdateStudent(student, AdminId) : new ReturnResponce("Primary id or logged in id  must be greater then 0 ");
        }
        #endregion

        #region Holiday

        /// <summary>
        /// Get All Holiday List
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetHolidayList")]
        [HttpGet]
        public ReturnResponce GetHoliday()
        {
            return service.GetHolidayList();
        }
        /// <summary>
        /// Get Holiday details By Id
        /// </summary>
        /// <param name="HolidayId"></param>
        /// <returns></returns>
        [Route("Admin/GetHolidayById")]
        [HttpGet]
        public ReturnResponce GetHolidayById(int HolidayId)
        {
            return service.GetHoliday(HolidayId);
        }

        /// <summary>
        /// Get All holiday List by School Id
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetHolidayBySchoolId")]
        [HttpGet]
        public ReturnResponce GetHolidayBySchoolId(int SchoolId)
        {
            return service.GetHolidayList(SchoolId);
        }



        /// <summary>
        /// Add new Holiday details
        /// </summary>
        /// <param name="holiday"></param>
        /// <param name="AdminId"></param>
        ///// <returns></returns>
        [Route("Admin/Holiday")]
        [HttpPost]
        public ReturnResponce AddHoliday(ISHoliday holiday, int AdminId)
        {
            return holiday.ID == 0 ? service.SetHoliday(holiday, AdminId) : new ReturnResponce("Invalid request");
        }


        /// <summary>
        /// Update existing holiday details
        /// </summary>
        /// <param name="holiday"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Holiday")]
        [HttpPut]
        public ReturnResponce SetHoliday(ISHoliday holiday, int AdminId)
        {
            return holiday.ID > 0 ? service.SetHoliday(holiday, AdminId) : new ReturnResponce("Primary id must be grater then 0");
        }

        /// <summary>
        /// Delete mark holiday details
        /// </summary>
        /// <param name="holidayId"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Holiday")]
        [HttpDelete]
        public ReturnResponce DeleteHoliday(int holidayId, int AdminId)
        {
            return (holidayId > 0 && AdminId > 0) ? service.DeleteHoliday(holidayId, AdminId) : new ReturnResponce("Primary id or adminId must be grater then 0");
        }
        #endregion

        #region Support
        /// <summary>
        /// Get All Support List
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetSupportList")]
        [HttpGet]
        public ReturnResponce GetSupportList()
        {
            return service.GetSupportList();
        }

        /// <summary>
        /// Get Support details By id
        /// </summary>
        /// <param name="SupportId"></param>
        /// <returns></returns>
        [Route("Admin/GetSupportById")]
        [HttpGet]
        public ReturnResponce GetSupportById(int SupportId)
        {
            return service.GetSupport(SupportId);
        }

        /// <summary>
        /// Get Support List Respective to SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetSchoolSupportList")]
        [HttpGet]
        public ReturnResponce GetSchoolSupportList(int SchoolId)
        {
            return service.GetSupportList(SchoolId);
        }

        /// <summary>
        /// Get Organization Users List 
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [Route("Admin/OrganizationUsersList")]
        [HttpGet]
        public ReturnResponce OrganizationUsersList(int RoleId)
        {
            return service.GetOrganizationUsersList(RoleId);
        }

        /// <summary>
        /// Get Organization User details by Id
        /// </summary>
        /// <param name="OrganizationUserId"></param>
        /// <returns></returns>
        [Route("Admin/OrganizationUser")]
        [HttpGet]
        public ReturnResponce OrganizationUser(int OrganizationUserId)
        {
            return service.GetOrganizationUser(OrganizationUserId);
        }
        /// <summary>
        /// Add Post Replay
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [Route("Admin/PostReplay")]
        [HttpPost]
        public ReturnResponce PostReplay(ReplayMessage details)
        {
            return service.PostReplay(details);
        }

        /// <summary>
        /// Set support ticket details
        /// </summary>
        /// <param name="details"></param>
        /// <param name="LogedInUser"></param>
        /// <returns></returns>
        [Route("Admin/Support")]
        [HttpPut]
        public ReturnResponce SetSupportTicket(ISSupport details, int LogedInUser)
        {
            return (details.ID > 0 && LogedInUser > 0) ? service.SetSupport(details, LogedInUser) : new ReturnResponce("Primary id or Logged in user id must be greate then 0");
        }
        #endregion

        #region Payment
        /// <summary>
        /// Get All Invoice List
        /// </summary>
        /// <returns></returns>
        [Route("Admin/InvoiceList")]
        [HttpGet]
        public ReturnResponce InvoiceList()
        {
            return service.GetInvoiceList();
        }

        /// <summary>
        /// Get Invoice details By Id 
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [Route("Admin/GetInvoice")]
        [HttpGet]
        public ReturnResponce GetInvoice(int InvoiceId)
        {
            return service.GetInvoice(InvoiceId);
        }

        /// <summary>
        /// Get Invoice List by SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetSchoolInvoiceList")]
        [HttpGet]
        public ReturnResponce GetSchoolInvoiceList(int SchoolId)
        {
            return service.GetInvoiceList(SchoolId);
        }

        /// <summary>
        /// Add Invoice Details
        /// </summary>
        /// <param name="iSSchoolInvoice"></param>
        /// <param name="UserLoginId"></param>
        /// <param name="UserLoginName"></param>
        /// <returns></returns>
        [Route("Admin/Invoice")]
        [HttpPost]
        public ReturnResponce AddInvoice(ISSchoolInvoice iSSchoolInvoice, int UserLoginId, string UserLoginName)
        {
            if (iSSchoolInvoice.ID == 0 && UserLoginId > 0)
            {
                return service.AddUpdateSchoolInvoice(iSSchoolInvoice, UserLoginId, UserLoginName);
            }
            else
            {
                return new ReturnResponce("Invalid Request");
            }

        }

        /// <summary>
        /// Update Invoice Details
        /// </summary>
        /// <param name="iSSchoolInvoice"></param>
        /// <param name="UserLoginId"></param>
        /// <param name="UserLoginName"></param>
        /// <returns></returns>
        [Route("Admin/Invoice")]
        [HttpPut]
        public ReturnResponce SetInvoice(ISSchoolInvoice iSSchoolInvoice, int UserLoginId, string UserLoginName)
        {
            if (iSSchoolInvoice.ID > 0 && UserLoginId > 0)
            {
                return service.AddUpdateSchoolInvoice(iSSchoolInvoice, UserLoginId, UserLoginName);
            }
            else
            {
                return new ReturnResponce("Primary id must be gretar then 0");
            }
        }
        #endregion

        #region Notification

        /// <summary>
        /// Get All Admin NotificationList
        /// </summary>
        /// <returns></returns>
        [Route("Admin/NotificationList")]
        [HttpGet]
        public ReturnResponce GetNotificationList()
        {
            return service.GetNotification();
        }
        #endregion

        #region Admin Report       
        /// <summary>
        /// Get all deleted students list by SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetDeletedStudents")]
        [HttpGet]
        public ReturnResponce DeletedStudents(int SchoolId)
        {
            return service.GetDeletedStudents(SchoolId);
        }


        /// <summary>
        /// Get all deleted parents list by schoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetDeletedParents")]
        [HttpGet]
        public ReturnResponce DeletedParents(int SchoolId)
        {
            return service.GetDeletedParents(SchoolId);
        }


        /// <summary>
        /// Get all deleted pickers list by schoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetDeletedPickers")]
        [HttpGet]
        public ReturnResponce DeletedPickers(int SchoolId)
        {
            return service.GetDeletedPickers(SchoolId);
        }
        #endregion

        #region Role
        /// <summary>
        /// Get Role List
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/GetUsersRoles")]
        [HttpGet]
        public ReturnResponce GetUsersRoles(int SchoolId)
        {
            return service.GetUsersRoleList(SchoolId);
        }

        /// <summary>
        /// Get Role details By id
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [Route("Admin/GetUserRole")]
        [HttpGet]
        public ReturnResponce GetUserRole(int RoleId)
        {
            return service.GetUserRoleDetails(RoleId);
        }

        /// <summary>
        /// Add User Role details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserLoginId"></param>
        /// <returns></returns>
        [Route("Admin/UserRole")]
        [HttpPost]
        public ReturnResponce AddUserRole(ISUserRole model, int UserLoginId)
        {
            return model.ID == 0 ? service.AddUpdateUserRole(model, UserLoginId) : new ReturnResponce("Invalid request");
        }

        /// <summary>
        /// Update UserRole details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserLoginId"></param>
        /// <returns></returns>
        [Route("Admin/UserRole")]
        [HttpPut]
        public ReturnResponce UpdateUserRole(ISUserRole model, int UserLoginId)
        {
            return model.ID > 0 ? service.AddUpdateUserRole(model, UserLoginId) : new ReturnResponce("Primary id must be gretar then 0");
        }

        /// <summary>
        /// Delete User Role
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="UserLoginId"></param>
        /// <returns></returns>
        [Route("Admin/UserRole")]
        [HttpDelete]
        public ReturnResponce DeleteUserRole(int RoleId, int UserLoginId)
        {
            return RoleId > 0 ? service.DeleteUserRole(RoleId, UserLoginId) : new ReturnResponce("Primary id must be gretar then 0");
        }
        #endregion

        #region NonTeaching
        /// <summary>
        /// Get Non teaching staff list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/NonTeachingList")]
        [HttpGet]
        public ReturnResponce NonTeachingList(int Schoolid)
        {
            return service.GetNonTeacherList(Schoolid);
        }      

        /// <summary>
        /// Get Non teaching summary by id
        /// </summary>
        /// <param name="NonTechingId"></param>
        /// <returns></returns>
        [Route("Admin/GetNonTeaching")]
        [HttpGet]
        public ReturnResponce GetNonTeaching(int NonTechingId)
        {
            return service.GetNonTeacher(NonTechingId);
        }

        /// <summary>
        /// Add Non Teaching staff details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/NonTeaching")]
        [HttpPost]
        public ReturnResponce AddNonTeachingStaff(ISTeacher model, int AdminId)
        {
            return model.ID == 0 ? service.AddUpdateNonTeaching(model, AdminId) : new ReturnResponce("Invalid request");
        }

        [Route("Admin/NonTeaching")]
        [HttpPut]
        public ReturnResponce UpdateNonTeachingStaff(ISTeacher model, int AdminId)
        {
            return model.ID > 0 ? service.AddUpdateNonTeaching(model, AdminId) : new ReturnResponce("Primary id must be grater then 0");
        }

        [Route("Admin/ReAssignNonTeaching")]
        [HttpPost]
        public ReturnResponce ReAssignNonTeachingStaff(ISTeacherReassignHistory model, int AdminId)
        {
            return model.ID == 0 ? service.ReAssignTeacher(model, AdminId) : new ReturnResponce("Invalid request");
        }

        #endregion
    }
}