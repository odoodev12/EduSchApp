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
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.API.Controllers
{
    /// <summary>
    /// School APIs
    /// </summary>
    [Authorize]
    public class UserRoleController : ApiController
    {
        /// <summary>
        /// UserRole Service
        /// </summary>
        private UserRoleService service;

        /// <summary>
        /// School API Constructor
        /// </summary>
        public UserRoleController()
        {
            service = new UserRoleService();
        }

        #region UserRole (Logic Implemented in school Service Service)
        /// <summary>
        /// Get UserRole for Teacher
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("UserRole/Teacher")]
        [HttpGet]
        public ReturnResponce GetUsersRolesForTeacher(int SchoolId)
        {
            return service.GetUsersRoleList(SchoolId, (int)ROLETYPE.TEACHING);
        }

        /// <summary>
        /// Get UserRole for NONTEACHING
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("UserRole/NonTeacher")]
        [HttpGet]
        public ReturnResponce GetUsersRolesForNONTEACHING(int SchoolId)
        {
            return service.GetUsersRoleList(SchoolId, (int)ROLETYPE.NONTEACHING);
        }

        /// <summary>
        /// Get Role details By id
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        [Route("UserRole/GetUserRoleById")]
        [HttpGet]
        public ReturnResponce GetUserRole(int UserRoleId)
        {
            return (UserRoleId > 0) ? service.GetUserRoleDetails(UserRoleId) : new ReturnResponce("UserRoleId should be greater than 0 !. ");
        }

        /// <summary>
        /// Add User Role details (Schooltype(AfterSchool = 1,Standard=2) , RoleType(Teaching=1,NonTeaching=2) )
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("School/UserRole/AddUser")]
        [HttpPost]
        public ReturnResponce AddUserRole(UserRoleAdd model)
        {
            var ErrorMessage = "";
            return model.ValidAddModel(out ErrorMessage) ? service.AddUserRole(model) : new ReturnResponce("Invalid request");
        }

        /// <summary>
        /// Update UserRole details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("School/UserRole/Update")]
        [HttpPost]
        public ReturnResponce UpdateUserRole(UserRoleEdit model)
        {
            var ErrorMessage = "";
            return model.ValidUpdateModel(out ErrorMessage) ? service.UpdateUserRole(model) : new ReturnResponce("Id must be gretar then 0");
        }

        /// <summary>
        /// Delete UserRole by Id
        /// </summary>
        /// <param name="UserRoleId"></param>
        /// <param name="UserLoginId"></param>
        /// <returns></returns>
        [Route("School/UserRole/Delete")]
        [HttpPost]
        public ReturnResponce DeleteUserRole(int UserRoleId, int UserLoginId)
        {
            return (UserRoleId > 0 && UserLoginId > 0) ? service.DeleteUserRole(UserRoleId, UserLoginId) : new ReturnResponce("UserRoleId must be gretar then 0 and UserLoginId is required.");
        }


        /// <summary>
        /// Get AdminRole list by SchoolId.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        [Route("AdminRole")]
        [HttpGet]
        public ReturnResponce AdminRoleList(int SchoolId)
        {
            return (SchoolId > 0) ? service.GetAdminRoleList(SchoolId, null) : new ReturnResponce("SchoolId should be greater than 0 !. ");        
        }


        /// <summary>
        /// Get AdminRole list by SchoolId with Filter Options.
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="Active"></param>
        /// <param name="RoleName"></param>
        /// <param name="RoleTypeId"></param>
        /// <returns></returns>
        [Route("AdminRole/Filter")]
        [HttpGet]
        public ReturnResponce AdminRoleListWithFilter(int SchoolId, bool? Active, string RoleName = "", int? RoleTypeId = 0)
        {
            return (SchoolId > 0) ? service.GetAdminRoleList(SchoolId, Active, RoleName, RoleTypeId) : new ReturnResponce("SchoolId should be greater than 0 !. ");            
        }

        #endregion



    }
}