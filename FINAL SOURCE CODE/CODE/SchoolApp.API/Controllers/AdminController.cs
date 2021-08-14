using SchoolApp.API.Models;
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
    public class AdminController : ApiController
    {
        public AdminService service;
        public AdminController()
        {
            service = new AdminService();
        }

        #region
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
        /// All School list By School Id
        /// </summary>
        /// <returns></returns>
        [Route("Admin/SchoolList")]
        [HttpGet]
        public ReturnResponce SchoolList(int SchoolId)
        {
            return service.GetSchool(SchoolId);
        }


        ///// <summary>
        ///// Add Update School details 
        ///// </summary>
        ///// <param name="school"></param>
        ///// <returns></returns>
        //[Route("Admin/SetSchool")]
        //[HttpPost]
        //public ReturnResponce SetClass(School school)
        //{
        //    return service.SetSchool(school);
        //}
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
        /// All Class list by SchoolId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/ClassListBySchoolId")]
        [HttpGet]
        public ReturnResponce ClassListBySchoolId(int SchoolId)
        {
            return service.GetClassList(SchoolId);
        }

        /// <summary>
        /// All Class list by Classid
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
        /// All Admin Teacher list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/TeacherList")]
        [HttpGet]
        public ReturnResponce TeacherList()
        {
            return service.GetTeacherList();
        }

        /// <summary>
        /// All Teacher list by SchoolId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/TeacherListBySchoolId")]
        [HttpGet]
        public ReturnResponce TeacherListBySchoolId(int SchoolId)
        {
            return service.GetClassList(SchoolId);
        }

        /// <summary>
        /// All Teacher list by TeacherId
        /// </summary>
        /// <returns></returns>
        [Route("Admin/GetTeacher")]
        [HttpGet]
        public ReturnResponce GetTeacher(int TeacherId)
        {
            return service.GetTeacher(TeacherId);
        }
        #endregion
    }
}