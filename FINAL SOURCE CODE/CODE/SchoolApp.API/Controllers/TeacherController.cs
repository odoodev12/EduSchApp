using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.API.Services;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SchoolApp.API.Controllers
{
    //[Authorize]
    public class TeacherController : ApiController
    {
        private TeacherService service;
        public TeacherController()
        {
            service = new TeacherService();
        }
        #region Picker

        /// <summary>
        /// Get all pickup List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Teacher/PickupList")]
        public ReturnResponce GetPickupList()
        {
            return service.GetPickupList();
        }

        /// <summary>
        /// Filter pickup
        /// </summary>
        /// <param name="isCompletePickupRun"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Teacher/CompletePickupRun")]
        public ReturnResponce GetCompletePickupRunList(bool isCompletePickupRun)
        {
            return service.GetIsCompletePickupRun(isCompletePickupRun);
        }

        /// <summary>
        /// Get pciker list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Teacher/PickerList")]
        public ReturnResponce GetPIckerList()
        {
            return service.GetPickerList();
        }

        /// <summary>
        /// Get Picker by id
        /// </summary>
        /// <param name="PickerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Teacher/Picker")]
        public ReturnResponce GetPIckerById(int PickerId)
        {
            return service.GetPicker(PickerId);
        }

        [HttpGet]
        [Route("Teacher/PickerActivity")]
        public ReturnResponce GetPickerActivity(int PickerId)
        {
            return service.GetPickerActivity(PickerId);
        }
        #endregion

        #region class
        /// <summary>
        /// Get Class List
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Teacher/ClassList")]
        [HttpGet]
        public ReturnResponce ClassList(int SchoolId)
        {
            return service.GetClassList(SchoolId);
        }
        /// <summary>
        /// Get Class by id
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        [Route("Teacher/Class")]
        [HttpGet]
        public ReturnResponce ClassById(int classID)
        {
            return service.GetClass(classID);
        }

        /// <summary>
        /// Add class
        /// </summary>
        /// <param name="classDetails"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/Class")]
        [HttpPost]
        public ReturnResponce AddClass(ISClass classDetails, int AdminId)
        {
            return service.AddUpdateClass(classDetails ,AdminId);
        }

        /// <summary>
        /// Update Class
        /// </summary>
        /// <param name="classDetails"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/Class")]
        [HttpPut]
        public ReturnResponce UpdateClass(ISClass classDetails, int AdminId)
        {
            return service.AddUpdateClass(classDetails, AdminId);
        }
        #endregion

        #region Student

        /// <summary>
        /// Get All Student Details
        /// </summary>
        /// <returns></returns>
        [Route("Teacher/GetAllStudents")]
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
        [Route("Teacher/GetSchoolStudents")]
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
        [Route("Teacher/GetClassStudentsBySchoolId")]
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
        [Route("Teacher/Student")]
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
        [Route("Teacher/Student")]
        [HttpPut]
        public ReturnResponce SetStudent(ISStudent student, int AdminId)
        {
            return (student.ID > 0 && AdminId > 0) ? service.AddUpdateStudent(student, AdminId) : new ReturnResponce("Primary id or logged in id  must be greater then 0 ");
        }
        #endregion

        #region Holiday       

        /// <summary>
        /// Get All holiday List by School Id
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Teacher/GetHolidayList")]
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
        [Route("Teacher/Holiday")]
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
        [Route("Teacher/Holiday")]
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
        [Route("Teacher/Holiday")]
        [HttpDelete]
        public ReturnResponce DeleteHoliday(int holidayId, int AdminId)
        {
            return (holidayId > 0 && AdminId > 0) ? service.DeleteHoliday(holidayId, AdminId) : new ReturnResponce("Primary id or adminId must be grater then 0");
        }
        #endregion

        #region Teacher        
        /// <summary>
        /// Get Teachers list by SchoolId
        /// </summary>
        /// <returns></returns>
        [Route("Teacher/TeacherList")]
        [HttpGet]
        public ReturnResponce TeacherListBySchoolId(int SchoolId)
        {
            return service.GetTeacherList(SchoolId);
        }

        /// <summary>
        /// Get Teacher details by TeacherId
        /// </summary>
        /// <returns></returns>
        [Route("Teacher/GetTeacher")]
        [HttpGet]
        public ReturnResponce GetTeacher(int TeacherId)
        {
            return service.GetTeacher(TeacherId);
        }
        

        /// <summary>
        /// Add Teacher details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/Teacher")]
        [HttpPost]
        public ReturnResponce AddTeacher(ISTeacher model, int AdminId)
        {
            return model.ID == 0 ? service.AddUpdateTeacher(model, AdminId) : new ReturnResponce("Invalid request");
        }

        /// <summary>
        /// Update Teacher details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/Teacher")]
        [HttpPut]
        public ReturnResponce UpdateTeacher(ISTeacher model, int AdminId)
        {
            return model.ID > 0 ? service.AddUpdateTeacher(model, AdminId) : new ReturnResponce("Primary id must be grater then 0");
        }

        /// <summary>
        /// ReAssign Teacher
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/ReAssignTeacher")]
        [HttpPost]
        public ReturnResponce ReAssignTeacher(ISTeacherReassignHistory model, int AdminId)
        {
            return model.ID == 0 ? service.ReAssignTeacher(model, AdminId) : new ReturnResponce("Invalid request");
        }

        #endregion

        #region NonTeaching
        /// <summary>
        /// Get Non teaching staff list
        /// </summary>
        /// <returns></returns>
        [Route("Teacher/NonTeachingList")]
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
        [Route("Teacher/GetNonTeaching")]
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
        [Route("Teacher/NonTeaching")]
        [HttpPost]
        public ReturnResponce AddNonTeachingStaff(ISTeacher model, int AdminId)
        {
            return model.ID == 0 ? service.AddUpdateNonTeaching(model, AdminId) : new ReturnResponce("Invalid request");
        }

        /// <summary>
        /// Update NonTeaching staff details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/NonTeaching")]
        [HttpPut]
        public ReturnResponce UpdateNonTeachingStaff(ISTeacher model, int AdminId)
        {
            return model.ID > 0 ? service.AddUpdateNonTeaching(model, AdminId) : new ReturnResponce("Primary id must be grater then 0");
        }

        /// <summary>
        /// ReAssign Non Teaching Staff
        /// </summary>
        /// <param name="model"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        [Route("Teacher/ReAssignNonTeaching")]
        [HttpPost]
        public ReturnResponce ReAssignNonTeachingStaff(ISTeacherReassignHistory model, int AdminId)
        {
            return model.ID == 0 ? service.ReAssignTeacher(model, AdminId) : new ReturnResponce("Invalid request");
        }

        #endregion

        #region Support
        /// <summary>
        /// Get Support List Respective to SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Teacher/GetSupport")]
        [HttpGet]
        public ReturnResponce GetSchoolSupportList(int SchoolId)
        {
            return service.GetSupportList(SchoolId);
        }

        /// <summary>
        /// Get Support details By id
        /// </summary>
        /// <param name="SupportId"></param>
        /// <returns></returns>
        [Route("Teacher/GetSupportById")]
        [HttpGet]
        public ReturnResponce GetSupportById(int SupportId)
        {
            return service.GetSupport(SupportId);
        }
       

       
        /// <summary>
        /// Add Post Replay
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [Route("Teacher/PostReplay")]
        [HttpPost]
        public ReturnResponce PostReplay(ReplayMessage details)
        {
            return service.PostReplay(details);
        }
        #endregion

        #region Payment

        /// <summary>
        /// Get Invoice List by SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Teacher/GetInvoiceList")]
        [HttpGet]
        public ReturnResponce GetSchoolInvoiceList(int SchoolId)
        {
            return service.GetInvoiceList(SchoolId);
        }

        /// <summary>
        /// Get Invoice details By Id 
        /// </summary>
        /// <param name="InvoiceId"></param>
        /// <returns></returns>
        [Route("Teacher/GetInvoice")]
        [HttpGet]
        public ReturnResponce GetInvoice(int InvoiceId)
        {
            return service.GetInvoice(InvoiceId);
        }

       
        /// <summary>
        /// Add Invoice Details
        /// </summary>
        /// <param name="iSSchoolInvoice"></param>
        /// <param name="UserLoginId"></param>
        /// <param name="UserLoginName"></param>
        /// <returns></returns>
        [Route("Teacher/Invoice")]
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
        [Route("Teacher/Invoice")]
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
        [Route("Teacher/NotificationList")]
        [HttpGet]
        public ReturnResponce GetNotificationList()
        {
            return service.GetNotification();
        }
        #endregion

        #region PickupReport

        /// <summary>
        /// get student list for pickup report
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("Teacher/PickUpReportList")]
        [HttpGet]
        public ReturnResponce GetPickReportList(int SchoolId)
        {
            return service.GetStudentList(SchoolId);
        }

        /// <summary>
        /// Get student pickup report
        /// </summary>
        /// <param name="pickupReportRequest"></param>
        /// <returns></returns>
        [Route("Teacher/StudentPickupReport")]
        [HttpGet]
        public ReturnResponce GetStudentPickUpReport(PickupReportRequest pickupReportRequest)
        {
            return service.GetStudentPickUpReport(pickupReportRequest);
        }

        #endregion

        #region class Daily Status

        /// <summary>
        /// Get Daily class status 
        /// </summary>
        /// <param name="dailyClassStatusRequest"></param>
        /// <returns></returns>
        [Route("Teacher/DailyClassStatus")]
        [HttpGet]
        public ReturnResponce GetDailyClassStatus(DailyClassStatusRequest dailyClassStatusRequest)
        {
            return service.GetDailyClassReport(dailyClassStatusRequest);
        }
        #endregion
    }
}