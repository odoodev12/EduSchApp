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
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http.Cors;

namespace SchoolApp.API.Controllers
{
    /// <summary>
    /// Admin APIs
    /// </summary>
    //[Authorize]    
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
        /// To All ClassType list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/ClassTypeList")]
        [HttpGet]
        public ReturnResponce ClassTypeList()
        {
            return service.GetClassTypeList();
        }

        /// <summary>
        ///To All ClassType list by School Type
        /// </summary>
        /// <param name="SchoolTypeId"></param>
        /// <returns></returns>
        [Route("Admin/ClassTypeListSchoolType")]
        [HttpGet]
        public ReturnResponce ClassTypeListBySchoolType(int SchoolTypeId)
        {
            return service.GetClassTypeListBySchoolType(SchoolTypeId);
        }

        /// <summary>
        /// To Get class list by SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="year"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        [Route("Admin/ClassListBySchoolId")]
        [HttpGet]
        public ReturnResponce ClassListBySchoolId(int SchoolId, string year = null, int TypeId = 0)
        {
            return service.GetClassList(SchoolId, year, TypeId);
        }

        /// <summary>
        /// To Get Class List By Filter Options
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="Year"></param>
        /// <param name="typeID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [Route("Admin/ClassListByFilter")]
        [HttpGet]
        public ReturnResponce ClassListByFilter(int SchoolID, string Year, int typeID, string Status)
        {
            return service.GetClassListByFilter(SchoolID, Year, typeID, Status);
        }



        /// <summary>
        /// To Add Class
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="ClassName"></param>
        /// <param name="Year"></param>
        /// <param name="ClassTypeID"></param>
        /// <param name="AfterSchoolType"></param>
        /// <param name="ExtOrganisation"></param>
        /// <param name="Active"></param>
        /// <param name="EndDate"></param>
        /// <param name="ISNonListed"></param>
        /// <param name="CreateType"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        [Route("Admin/Class")]
        [HttpPost]
        public ReturnResponce AddClass(int SchoolID, string ClassName, string Year, int ClassTypeID, string AfterSchoolType, string ExtOrganisation, bool Active, string EndDate, bool ISNonListed, int CreateType, int LoginUserId)
        {
            return (LoginUserId > 0 && CreateType > 0) ? service.AddClass(SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active, EndDate, ISNonListed, CreateType, LoginUserId) : new ReturnResponce("Invalid request or logged in id  must be greater then 0 ");
        }

        /// <summary>
        /// To Update Class
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="SchoolID"></param>
        /// <param name="Name"></param>
        /// <param name="Year"></param>
        /// <param name="TypeID"></param>
        /// <param name="AfterSchoolType"></param>
        /// <param name="ExternalOrganisation"></param>
        /// <param name="Active"></param>
        /// <param name="EndDate"></param>
        /// <param name="ISNonListed"></param>
        /// <param name="CreateType"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        [Route("Admin/Class")]
        [HttpPut]
        public ReturnResponce UpdateClass(int ClassID, int SchoolID, string Name, string Year, int TypeID, string AfterSchoolType, string ExternalOrganisation, bool Active, string EndDate, bool ISNonListed, int CreateType, int LoginUserId)
        {

            return (ClassID > 0 && CreateType > 0 && LoginUserId > 0) ? service.UpdateClass(ClassID, SchoolID, Name, Year, TypeID, AfterSchoolType, ExternalOrganisation, Active, EndDate, ISNonListed, CreateType, LoginUserId) : new ReturnResponce("Primary id or logged in id  must be greater then 0 ");
        }


        /// <summary>
        /// To Get Class details by Id
        /// </summary>
        /// <param name="ClassId"></param>
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
        /// To Get Teachers list by SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Admin/TeacherListBySchoolId")]
        [HttpGet]
        public ReturnResponce TeacherListBySchoolId(int SchoolId)
        {
            return service.GetTeacherList(SchoolId, "", 0, "", "", "", 0, "0");
        }

        /// <summary>
        /// To Get Teachers List With Optional Filters
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="Year"></param>
        /// <param name="ClassID"></param>
        /// <param name="TeacherName"></param>
        /// <param name="OrderBy"></param>
        /// <param name="SortBy"></param>
        /// <param name="classTypeID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [Route("Admin/TeacherListWithFilters")]
        [HttpGet]
        public ReturnResponce TeacherListWithFilters(int SchoolId, string Year = "", int ClassID = 0, string TeacherName = "", string OrderBy = "", string SortBy = "", int classTypeID = 0, string Status = "")
        {
            return service.GetTeacherList(SchoolId, Year, ClassID, TeacherName, OrderBy, SortBy, classTypeID, Status);
        }

        /// <summary>
        /// To Get Teacher details by TeacherId
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <returns></returns>
        [Route("Admin/GetTeacher")]
        [HttpGet]
        public ReturnResponce GetTeacher(int TeacherId)
        {
            return service.GetTeacher(TeacherId);
        }

        /// <summary>
        /// To Get Teacher list by SchoolId and ClassId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [Route("Admin/GetTeachersWithClassId")]
        [HttpGet]
        public ReturnResponce GetTeacherBySchoolOrClassId(int SchoolId, int ClassId)
        {
            return service.GetTeacherList(SchoolId, "", ClassId, "", "", "", 0, "0");
        }

        /// <summary>
        /// To Add Teacher details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Admin/Teacher")]
        [HttpPost]
        public ReturnResponce AddTeacher(ISTeacher model, int AdminId)
        {
            return model.ID == 0 ? service.AddUpdateTeacher(model, AdminId) : new ReturnResponce("Invalid request");
        }

        /// <summary>
        /// To Update Teacher details 
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

        ///// <summary>
        ///// To ReAssign Teacher Class
        ///// </summary>
        ///// <param name="SchoolID"></param>
        ///// <param name="TeacherID"></param>
        ///// <param name="OldClassID"></param>
        ///// <param name="NewClassID"></param>
        ///// <param name="UserType"></param>
        ///// <param name="SenderID"></param>
        ///// <returns></returns>
        //[Route("Admin/TeacherReassignment")]
        //[HttpPost]
        //public ReturnResponce TeacherReassignment(int SchoolID, int TeacherID, int OldClassID, int NewClassID, int UserType, int SenderID)
        //{
        //    return (SchoolID > 0 && TeacherID > 0 && OldClassID > 0 && NewClassID > 0 && UserType > 0 && SenderID > 0) ? service.ReAssignTeacher(SchoolID, TeacherID, OldClassID, NewClassID, UserType, SenderID) : new ReturnResponce("Invalid request");
        //}

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

        
    }
}