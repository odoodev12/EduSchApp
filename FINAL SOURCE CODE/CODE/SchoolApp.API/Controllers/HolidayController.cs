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
    /// Holiday APIs
    /// </summary>
    //[Authorize]    
    public class HolidayController : ApiController
    {
        /// <summary>
        /// Holiday Service
        /// </summary>
        private HolidayService service;

        /// <summary>
        /// Holiday API Constructor
        /// </summary>
        public HolidayController()
        {
            service = new HolidayService();
        }

        #region Holiday


        /// <summary>
        /// Get Holiday details By Id
        /// </summary>
        /// <param name="HolidayId"></param>
        /// <returns></returns>
        [Route("Holiday/Detail")]
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
        [Route("Holiday/List")]
        [HttpGet]
        public ReturnResponce GetHolidayBySchoolId(int SchoolId)
        {
            return service.GetHolidayList(SchoolId);
        }


        /// <summary>
        /// Get All holiday List by School Id with Filter options.
        /// Status (1= Active, 2= Non-Active and Null= Both)
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="Name"></param>
        /// <param name="Datefrom"></param>
        /// <param name="Dateto"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [Route("Holiday/FilterList")]
        [HttpGet]
        public ReturnResponce GetHolidayFilterListBySchoolId(int SchoolId, string Name = "", DateTime? Datefrom = null, DateTime? Dateto = null, int? Status = null)
        {
            return service.GetHolidayFilterList(SchoolId, Name, Datefrom, Dateto, Status);
        }


        /// <summary>
        /// Add new Holiday details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Holiday/Add")]
        [HttpPost]
        public ReturnResponce AddHoliday(HolidayAdd model)
        {
            var ErrorMessage = "";
            return model.ValidAddModel(out ErrorMessage) ? service.AddHoliday(model) : new ReturnResponce("Invalid request :- " + ErrorMessage);
        }


        /// <summary>
        /// Update existing holiday details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Holiday/Update")]
        [HttpPost]
        public ReturnResponce UpdateHoliday(HolidayEdit model)
        {
            var ErrorMessage = "";
            if (model.ID <= 0)
            {
                ErrorMessage = "HoliDayId must be grater then 0";
                new ReturnResponce(ErrorMessage);
            }

            if (!model.ValidUpdateModel(out ErrorMessage))
            {
                new ReturnResponce(ErrorMessage);
            }

            return service.UpdateHoliday(model);
        }

        /// <summary>
        /// Delete mark holiday details
        /// </summary>
        /// <param name="holidayId"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        [Route("Holiday/Delete")]
        [HttpPost]
        public ReturnResponce DeleteHoliday(int holidayId, int LoginUserId)
        {
            return (holidayId > 0 && LoginUserId > 0) ? service.DeleteHoliday(holidayId, LoginUserId) : new ReturnResponce("HoliDayId or LoginUserId must be grater then 0");
        }



        #endregion
    }
}