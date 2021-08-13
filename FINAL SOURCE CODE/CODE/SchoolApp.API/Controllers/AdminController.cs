using SchoolApp.API.Models;
using SchoolApp.API.Services;
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

        /// <summary>
        /// All SchoolType list
        /// </summary>
        /// <returns></returns>
        [Route("Admin/SchoolTypeList")]
        public ReturnResponce SchoolTypeList()
        {
            return service.GetSchoolTypes();
        }
    }
}