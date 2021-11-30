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
    /// <summary>
    /// Support Related API
    /// </summary>
    [Authorize]
    public class SupportController : ApiController
    {
        // GET: Support
        private SupportService service;

        /// <summary>
        /// Admin API Constructor
        /// </summary>
        public SupportController()
        {
            service = new SupportService();
        }


        //#region Support
        ///// <summary>
        ///// Get All Support List
        ///// </summary>
        ///// <returns></returns>
        //[Route("Support/GetSupportList")]
        //[HttpGet]
        //public ReturnResponce GetSupportList()
        //{
        //    return service.GetSupportList();
        //}

        ///// <summary>
        ///// Get Support details By id
        ///// </summary>
        ///// <param name="SupportId"></param>
        ///// <returns></returns>
        //[Route("Support/GetSupportById")]
        //[HttpGet]
        //public ReturnResponce GetSupportById(int SupportId)
        //{
        //    return service.GetSupport(SupportId);
        //}

        ///// <summary>
        ///// Get Support List Respective to SchoolId
        ///// </summary>
        ///// <param name="SchoolId"></param>
        ///// <returns></returns>
        //[Route("Support/GetSchoolSupportList")]
        //[HttpGet]
        //public ReturnResponce GetSchoolSupportList(int SchoolId)
        //{
        //    return service.GetSupportList(SchoolId);
        //}

        ///// <summary>
        ///// Get Organization Users List 
        ///// </summary>
        ///// <param name="RoleId"></param>
        ///// <returns></returns>
        //[Route("Support/OrganizationUsersList")]
        //[HttpGet]
        //public ReturnResponce OrganizationUsersList(int RoleId)
        //{
        //    return service.GetOrganizationUsersList(RoleId);
        //}

        ///// <summary>
        ///// Get Organization User details by Id
        ///// </summary>
        ///// <param name="OrganizationUserId"></param>
        ///// <returns></returns>
        //[Route("Support/OrganizationUser")]
        //[HttpGet]
        //public ReturnResponce OrganizationUser(int OrganizationUserId)
        //{
        //    return service.GetOrganizationUser(OrganizationUserId);
        //}
        ///// <summary>
        ///// Add Post Replay
        ///// </summary>
        ///// <param name="details"></param>
        ///// <returns></returns>
        //[Route("Support/PostReplay")]
        //[HttpPost]
        //public ReturnResponce PostReplay(ReplayMessage details)
        //{
        //    return service.PostReplay(details);
        //}

        ///// <summary>
        ///// Set support ticket details
        ///// </summary>
        ///// <param name="details"></param>
        ///// <param name="LogedInUser"></param>
        ///// <returns></returns>
        //[Route("Support/Support")]
        //[HttpPut]
        //public ReturnResponce SetSupportTicket(ISSupport details, int LogedInUser)
        //{
        //    return (details.ID > 0 && LogedInUser > 0) ? service.SetSupport(details, LogedInUser) : new ReturnResponce("Primary id or Logged in user id must be greate then 0");
        //}
        //#endregion


    }
}