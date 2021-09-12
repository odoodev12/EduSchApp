using Microsoft.IdentityModel.Tokens;
using SchoolApp.API.Models;
using SchoolApp.API.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.API.Controllers
{
    //[Authorize]
    public class LoginController : ApiController
    {
        private LoginServices service;

        public LoginController()
        {
            service = new LoginServices();
        }

        [HttpPost]
        public object Login(string username, string password, int UserType)
        {
            if (UserType == (int)USERTYPE.ADMIN)
            {
                return service.AdminLogin(username, password);
            }
            else if (UserType == (int)USERTYPE.TEACHER)
            {
                return service.TeacherLogin(username, password);
            }
            else if (UserType == (int)USERTYPE.PARENT)
            {
                return service.ParentLogin(username, password);
            }
            else if (UserType == (int)USERTYPE.SCHOOL)
            {
                return service.SchoolLogin(username, password);
            }
            else
            {
                return new ReturnResponce("Invalid UserType");
            }

        }
    }
}