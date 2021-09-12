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


namespace SchoolApp.API.Controllers
{

    /// <summary>
    /// Common APIs
    /// </summary>
    public class CommonController : ApiController
    {
        private CommonServices service;

        public CommonController()
        {
            service = new CommonServices();
        }

        #region Login API

        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Common/AdminLogin")]
        public object AdminLogin(string username, string password)
        {
            return service.AdminLogin(username, password);
        }


        /// <summary>
        /// Teacher Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Common/TeacherLogin")]
        public object TeacherLogin(string username, string password)
        {
            return service.TeacherLogin(username, password);
        }

        /// <summary>
        /// School Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Common/SchoolLogin")]
        public object SchoolLogin(string username, string password)
        {
            return service.SchoolLogin(username, password);
        }

        /// <summary>
        /// Parent Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Common/ParentLogin")]
        public object ParentLogin(string username, string password)
        {
            return service.ParentLogin(username, password);
        }

        #endregion

        //[HttpGet]
        //[Route("Common/GetToken")]
        //private string GetToken()
        //{
        //    string key = "wwwapithestmanager123";
        //    var issuer = "http://www.api.thestmanager.com";

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        //    var permClaims = new List<Claim>();
        //    permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //    permClaims.Add(new Claim("valid", "1"));
        //    permClaims.Add(new Claim("userid", "1"));
        //    permClaims.Add(new Claim("name", "SchoolAPI"));

        //    //Create Security Token object by giving required parameters    
        //    var token = new JwtSecurityToken(issuer, //Issure    
        //                    issuer,  //Audience    
        //                    permClaims,
        //                    expires: DateTime.Now.AddDays(1),
        //                    signingCredentials: credentials);
        //    var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt_token;
        //}
    }
}