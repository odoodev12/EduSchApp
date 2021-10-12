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
    [Authorize]
    public class ParentController : ApiController
    {
        private ParentService service;
        public ParentController()
        {
            service = new ParentService();
        }

        #region Child Pickers

        /// <summary>
        /// GEt PickerList for child picker
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Parent/PickerList")]
        public ReturnResponce GetPIckerList()
        {
            return service.GetPickerList();
        }

        /// <summary>
        /// Add Picker 
        /// </summary>
        /// <param name="iSPicker"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Parent/Picker")]
        public ReturnResponce AddPicker(ISPicker iSPicker)
        {
            return iSPicker.ID == 0 ? service.AddUpdatePicker(iSPicker) : new ReturnResponce("Primary id is must be  0");
        }

        /// <summary>
        /// Update picker
        /// </summary>
        /// <param name="iSPicker"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Parent/Picker")]
        public ReturnResponce UpdatePicker(ISPicker iSPicker)
        {
            return iSPicker.ID > 0 ? service.AddUpdatePicker(iSPicker) : new ReturnResponce("Primary id is must be gretar then  0");
        }

        /// <summary>
        /// Delete Picker
        /// </summary>
        /// <param name="PickerId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Parent/Picker")]
        public ReturnResponce DeletePicker(int PickerId , int LoginId , string LoginName)
        {
            return PickerId > 0 ? service.DeletePicker(PickerId, LoginId, LoginName) : new ReturnResponce("Primary id is must be gretar then  0");
        }

        ///// <summary>
        ///// Edit assgin picker
        ///// </summary>
        ///// <param name="PickerId"></param>
        ///// <param name="iSPickerAssignment"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("Parent/EditAssignPicker")]
        //public ReturnResponce EditAsignPicker(int PickerId , ISPickerAssignment iSPickerAssignment)
        //{
        //    return PickerId > 0 ? service.UpdateAssign(PickerId , iSPickerAssignment) : new ReturnResponce("Primary id is must be gretar then  0");
        //}

        #endregion

        #region Daily Pickup Status

        ///// <summary>
        ///// Daily status pickup status
        ///// </summary>
        ///// <param name="dailyPickupReportRequest"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("Parent/DailyPickupStatus")]
        //public ReturnResponce GetDailyPickupStatus(DailyPickupReportRequest dailyPickupReportRequest)
        //{
        //    return service.GetPckupDailyStatus(dailyPickupReportRequest);

        //}
        #endregion

        #region Pickup Report
        /// <summary>
        /// Get Pickup Report for Parent Login
        /// </summary>
        /// <param name="LogginParentId"></param>
        /// <param name="LoginParentEmail"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Parent/PickUpReport")]
        public ReturnResponce GetPickupReport(int LogginParentId, string LoginParentEmail)
        {
            return (LogginParentId > 0 && !string.IsNullOrWhiteSpace(LoginParentEmail)) ? service.GetPickupReport(LogginParentId, LoginParentEmail) : new ReturnResponce("LoginParentId and Email is required.");
        }
        #endregion

        #region Notification

        /// <summary>
        /// Get NotificationList
        /// </summary>
        /// <returns></returns>
        [Route("Parent/NotificationList")]
        [HttpGet]
        public ReturnResponce GetNotificationList()
        {
            return service.GetNotification();
        }
        #endregion


        #region Message

        /// <summary>
        /// To Send Pickup Message to Teachers(If PickupMessageType is "other" then OtherPickupMessage is required )
        /// </summary>
        /// <param name="studentname"></param>
        /// <param name="SchoolID"></param>
        /// <param name="PickupMessageType"></param>
        /// <param name="LoginParentId"></param>
        /// <param name="OtherPickupMessage"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Parent/SendPickupMessage")]
        public ReturnResponce SendPickupMessage(string studentname, int SchoolID, string PickupMessageType,  int LoginParentId, string OtherPickupMessage="")
        {
            return studentname != null && SchoolID !=0 ? service.SendPickupMessage(studentname, SchoolID, PickupMessageType, OtherPickupMessage, LoginParentId) : new ReturnResponce("Primary id is must be gretar then  0");
        }
        /// <summary>
        /// Send Massage to School Or Teacher
        /// Note :- (ReceiverGroup(1=School, 2=Teacher))
        /// </summary>
        /// <param name="LoginParentId"></param>
        /// <param name="StudentID"></param>
        /// <param name="ReceiverGroup"></param>
        /// <param name="SchoolId"></param>
        /// <param name="MailSubject"></param>
        /// <param name="Message"></param>
        /// <param name="fpUploadAttachment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Parent/SendMessage")]
        public ReturnResponce SendMessage(int LoginParentId, int StudentID, string ReceiverGroup, int SchoolId, string MailSubject, string Message, HttpPostedFile fpUploadAttachment )
        {
            return StudentID != 0 && SchoolId != 0 && !string.IsNullOrWhiteSpace(ReceiverGroup) && LoginParentId !=0  &&  !string.IsNullOrWhiteSpace(MailSubject) && !string.IsNullOrWhiteSpace(Message) ? service.SendMessage(LoginParentId, StudentID, ReceiverGroup, SchoolId, MailSubject, Message, fpUploadAttachment) : new ReturnResponce("All Parameter required except Attechment");
        }





        #endregion

    }
}