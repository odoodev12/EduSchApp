using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.API.Services;
using SchoolApp.Database;
using System;
using System.Web.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SchoolApp.API.Controllers
{
    [Authorize]
    public class NonTeacherController : ApiController
    {
        /// <summary>
        /// Non Teacher API
        /// </summary>
        private NonTeacherService service;

        /// <summary>
        /// Non Teacher service Related API's
        /// </summary>
        public NonTeacherController()
        {
            service = new NonTeacherService();
        }

        #region NonTeaching
        /// <summary>
        /// Get Non teaching staff list by SchoolId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("NonTeacher/List")]
        [HttpGet]
        public ReturnResponce NonTeachingList(int SchoolId)
        {
            return (SchoolId > 0) ? service.GetNonTeacherList(SchoolId) : new ReturnResponce("SchoolId is required!");
        }

        /// <summary>
        /// Get NonTeaching details by Id
        /// </summary>
        /// <param name="NonTeacherId"></param>
        /// <returns></returns>
        [Route("NonTeacher/Detail")]
        [HttpGet]
        public ReturnResponce GetNonTeaching(int NonTeacherId)
        {
            return (NonTeacherId > 0) ? service.GetNonTeacher(NonTeacherId) : new ReturnResponce("NonTeacherId is required!");
        }


        /// <summary>
        /// To Get NonTeachers list by filters
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="TeacherName"></param>
        /// <param name="OrderBy"></param>
        /// <param name="SortBy"></param>
        /// <returns></returns>
        [Route("NonTeacher/ListByFilters")]
        [HttpGet]
        public ReturnResponce NonTeacherListBySchoolIdNonTeacherList(int SchoolID, string TeacherName = "", string OrderBy = "Asc", string SortBy = "")
        {
            return service.GetNonTeacherList(SchoolID, TeacherName, OrderBy, SortBy);
        }

        /// <summary>
        /// Add New NonTeaching staff
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("NonTeacher/Add")]
        [HttpPost]
        public ReturnResponce AddNonTeachingStaff(AddNonTeacherViewModel model)
        {
            string ErrorValidation = "";
            return model.ValidModel(out ErrorValidation) ? service.AddNonTeaching(model) : new ReturnResponce(ErrorValidation);
        }

        /// <summary>
        /// Update New NonTeaching staff
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("NonTeacher/Update")]
        [HttpPost]
        public ReturnResponce UpdateTeacher(UpdateNonTeacherViewModel model)
        {
            var ErrorMessage = "";
            if (model.Id <= 0)
            {
                ErrorMessage = "Id must be grater then 0";
                new ReturnResponce(ErrorMessage);
            }

            if (!model.ValidModel(out ErrorMessage))
            {
                new ReturnResponce(ErrorMessage);
            }

            return service.UpdateNonTeacher(model);
        }

        /// <summary>
        /// To Reset Password for NonTeacher Login
        /// </summary>
        /// <param name="NonTeacherID"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [Route("NonTeacher/ResetPassword")]
        [HttpGet]
        public ReturnResponce ResetPassword(int NonTeacherID, string NewPassword)
        {
            return service.ResetPassword(NonTeacherID, NewPassword);
        }

        /// <summary>
        ///  To Get Assign Class List
        /// </summary>
        /// <param name="NonTeacherId"></param>
        /// <returns></returns>
        [Route("NonTeacher/GetTeacherClassList")]
        [HttpGet]
        public ReturnResponce GetTeacherClassList(int NonTeacherId)
        {
            try
            {
                SchoolAppEntities DB = new SchoolAppEntities();
                string Class = "";
                string ClassName = "";

                var obj = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == NonTeacherId && p.ISClass.Active == true && p.Active == true).ToList();
                if (obj.Count > 0)
                {
                    foreach (var items in obj)
                    {
                        Class += items.ISClass.Name + ", ";
                    }
                    ClassName = Class.Remove(Class.Length - 2);
                }

                return new ReturnResponce(ClassName, null);


            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }


        /// <summary>
        /// To Add class Assign
        /// LoginUserType 1 for Admin and 2 for SchoolLogin
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="NonTeacherId"></param>
        /// <param name="ClassIds"></param>
        /// <param name="LoginUserType"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        [Route("NonTeacher/AddClassAssign")]
        [HttpPost]
        public ReturnResponce AddClassAssign(int SchoolID, int NonTeacherId, int[] ClassIds, int LoginUserType, int LoginUserId)
        {
            return SchoolID > 0 && NonTeacherId > 0 && LoginUserId > 0 ? service.AddClassAssign(SchoolID, NonTeacherId, ClassIds, LoginUserType, LoginUserId) : new ReturnResponce("invalid param or value, Please provide valid details !");
        }


        #endregion
    }
}