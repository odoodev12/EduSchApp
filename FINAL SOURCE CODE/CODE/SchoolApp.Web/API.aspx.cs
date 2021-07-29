using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.Admin
{
    public partial class API : System.Web.UI.Page
    {
        #region Global Variable Declaration
        SchoolAppEntities DB = new SchoolAppEntities();
        Responses ResponseObject = new Responses();
        JavaScriptSerializer js = new JavaScriptSerializer();
        TokenManagement ObjToken = new TokenManagement();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (OperationManagement.GetOperation("OP") == APIOPERATION.GETTOKEN.ToString())
                {
                    //required IMEINO
                    GetToken();
                }
                else
                {
                    if (ObjToken.CheckToken(OperationManagement.GetOperation("TOKEN")))
                    {
                        #region Parant Section
                        if (OperationManagement.GetOperation("OP") == APIOPERATION.PARENTLOGIN.ToString())
                        {
                            ParentLogin();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.IMAGEUPLOAD.ToString())
                        {
                            ImageUpload();
                        }

                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PARENTFORGOTPASSWORD.ToString())
                        {
                            ParentForgotPassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DAILYPICKUPSTATUS.ToString())
                        {
                            DailyPickUpStatus();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTLISTBYPARENT.ToString())
                        {
                            StudentListByParent();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ALLPICKERLIST.ToString())
                        {
                            AllPickerList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKERLIST.ToString())
                        {
                            PickerList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GENERATEPICKERCODE.ToString())
                        {
                            GeneratePickerCode();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETPICKER.ToString())
                        {
                            GetPicker();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DELETEPICKER.ToString())
                        {
                            DeletePicker();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ADDUPDATEPICKER.ToString())
                        {
                            AddUpdatePicker();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EDITCHILDRENASSIGNMENT.ToString())
                        {
                            EditChildrenAssignment();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.REMOVEPICKERFROMCHILD.ToString())
                        {
                            RemovePickerFromChild();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.VIEWPICKERASSIGNMENT.ToString())
                        {
                            ViewPickerAssignment();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLMESSAGE.ToString())
                        {
                            SchoolMessage();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKUPMESSAGE.ToString())
                        {
                            PickUpMessage();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EDITMORECHILDRENASSIGNMENT.ToString())
                        {
                            EditMoreChildrenAssignment();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.MAKEPICKERFORTODAY.ToString())
                        {
                            MakePickerForToday();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTWISESCHOOL.ToString())
                        {
                            StudentWiseSchool();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.MAKEACTIVEDEACTIVE.ToString())
                        {
                            MakeActiveDeActive();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKERSBYSTUDENTID.ToString())
                        {
                            PickersByStudentID();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.RESETPICKER.ToString())
                        {
                            ResetPicker();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDMSGTOTEACHERSCHOOL.ToString())
                        {
                            SendMsgToTeacherSchool();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKERTYPELIST.ToString())
                        {
                            GetPickerTypeList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKERIMAGEUPLOAD.ToString())
                        {
                            PickerImageUpload();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTBYPICKER.ToString())
                        {
                            StudentByPicker();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PARENTPROFILE.ToString())
                        {
                            ParentProfile();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PARENTCHANGEPASSWORD.ToString())
                        {
                            ParentChangePassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ALLPICKERS.ToString())
                        {
                            AllPickers();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKUPSTATUS.ToString())
                        {
                            PickUpStatus();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETASSIGNEDPICKERS.ToString())
                        {
                            GetAssignedPickers();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETSTUDENTSCHOOLS.ToString())
                        {
                            GetStudentSchools();
                        }
                        #endregion

                        #region Teacher Section
                        if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERLOGIN.ToString())
                        {
                            TeacherLogin();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERFORGOTPASSWORD.ToString())
                        {
                            TeacherForgotPassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PICKUPLIST.ToString())
                        {
                            PickUpList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERCLASSASSIGNMENT.ToString())
                        {
                            TeacherClassAssignment();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DAILYCLASSREPORT.ToString())
                        {
                            DailyClassReport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DAILYCLASSREPORTFILTER.ToString())
                        {
                            DailyClassReportFilter();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTLISTBYCLASS.ToString())
                        {
                            StudentListByClass();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTPICKUPREPORT.ToString())
                        {
                            StudentPickUpReport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ATTENDENCE.ToString())
                        {
                            Attendence();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ATTENDENCELISTBYCLASS.ToString())
                        {
                            AttendenceListByClass();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDUNPICKALERT.ToString())
                        {
                            SendUnPickAlert();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.VIEWPICKERFORTODAY.ToString())
                        {
                            ViewPickerForToday();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.CONFIRMPICKUP.ToString())
                        {
                            ConfirmPickup();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EXPORTTOEXCELATTENDENCE.ToString())
                        {
                            ExportToExcelAttendence();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EXPORTTOEXCELSTUDENTPICKUP.ToString())
                        {
                            ExportToExcelStudentPickUp();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDMSGTOSCHOOLSTUDENT.ToString())
                        {
                            SendMsgToSchoolStudent();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETPICKUPMESSAGE.ToString())
                        {
                            GetPickupMessage();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.MARKASABSENT.ToString())
                        {
                            SetMarkAsAbsent();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDTOAFTERSCHOOL.ToString())
                        {
                            SetSendToAfterSchool();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDTOOFFICE.ToString())
                        {
                            SetSendToOffice();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERPROFILE.ToString())
                        {
                            TeacherProfile();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERRESETPASSWORD.ToString())
                        {
                            TeacherResetPassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.COMPLETEATTENDENCERUN.ToString())
                        {
                            CompleteAttendenceRun();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.COMPLETEPICKUPRUN.ToString())
                        {
                            CompletePickUpRun();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EXPORTTOEXCELATTE.ToString())
                        {
                            ExportToExcelAtte();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EXPORTDAILYCLASSREPORT.ToString())
                        {
                            ExportToExcelClassReport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTATTENDENCEREPORT.ToString())
                        {
                            StudentAttendenceReport();
                        }
                        #endregion

                        #region School Section
                        if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLLOGIN.ToString())
                        {
                            SchoolLogin();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLFORGOTPASSWORD.ToString())
                        {
                            SchoolForgotPassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETADMINROLES.ToString())
                        {
                            GetAdminRoles();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLCLASSYEAR.ToString())
                        {
                            SchoolClassYear();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETCLASSBYYEAR.ToString())
                        {
                            GetClassByYear();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETCLASSTYPELIST.ToString())
                        {
                            GetClassTypeList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETSCHOOLTYPE.ToString())
                        {
                            GetSchoolTypeList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.CREATECLASS.ToString())
                        {
                            CreateClass();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DELETEADMINROLES.ToString())
                        {
                            DeleteAdminRoles();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ADDUPDATEADMINROLES.ToString())
                        {
                            AddUpdateAdminRole();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.EDITADMINROLES.ToString())
                        {
                            EditAdminRole();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.CLASSDETAILS.ToString())
                        {
                            ClassDetails();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.UPDATECLASS.ToString())
                        {
                            UpdateClass();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.USERACTIVITY.ToString())
                        {
                            UserActivity();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PAYMENTLIST.ToString())
                        {
                            PaymentList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERLIST.ToString())
                        {
                            TeacherList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETSTUDENTINFO.ToString())
                        {
                            GetStudentInfo();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.CREATEUPDATESTUDENT.ToString())
                        {
                            CreateUpdateStudent();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SUPPORTLIST.ToString())
                        {
                            SupportList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ADDUPDATETEACHER.ToString())
                        {
                            AddUpdateTeacher();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETTEACHERBYID.ToString())
                        {
                            GetTeacherByID();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETROLES.ToString())
                        {
                            GetRoles();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SUPPORTSTATUS.ToString())
                        {
                            GetSupportStatus();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DELETESTUDENT.ToString())
                        {
                            DeleteStudent();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.CREATESUPPORT.ToString())
                        {
                            CreateSupport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.VIEWSUPPORT.ToString())
                        {
                            ViewSupport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SUDENTCLASSASSIGNMENT.ToString())
                        {
                            StudentClassAssignment();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.STUDENTIMAGEUPLOAD.ToString())
                        {
                            StudentImageUpload();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERIMAGEUPLOAD.ToString())
                        {
                            TeacherImageUpload();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLIMAGEUPLOAD.ToString())
                        {
                            SchoolImageUpload();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.RECIVEDMESSAGE.ToString())
                        {
                            ReceivedMessage();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENTMESSAGE.ToString())
                        {
                            SendMessage();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DELETEMESSAGE.ToString())
                        {
                            DeleteMessage();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETRECEIVERLIST.ToString())
                        {
                            GetReceiverList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDMESSAGETOUSER.ToString())
                        {
                            SendMessageToUser();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.REASSIGNTEACHER.ToString())
                        {
                            ReassignTeacher();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.LOGTYPELIST.ToString())
                        {
                            LogTypeList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDSUPPORT.ToString())
                        {
                            SendSupport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLPROFILE.ToString())
                        {
                            SchoolProfile();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.UPDATESCHOOLPROFILE.ToString())
                        {
                            UpdateSchoolProfile();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDATTENDENCESTATUS.ToString())
                        {
                            SendAttendenceStatus();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TEACHERCHANGEPASSWORD.ToString())
                        {
                            TeacherChangePassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.TICKETMESSAGEDETAILSBYSUPPORT.ToString())
                        {
                            TicketMessageDetialsBySupport();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLORGANIZATIONLIST.ToString())
                        {
                            SchoolOrganizationList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETREASSIGNCLASSES.ToString())
                        {
                            GetReassignClasses();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETROLETYPE.ToString())
                        {
                            GetRoleTypeList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETCLASSTYPES.ToString())
                        {
                            GetClassTypes();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SUPPORTINCREMENTNO.ToString())
                        {
                            GetSupportNo();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETNONTEACHINGROLES.ToString())
                        {
                            GetNonTeachingRoles();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.NONTEACHERLIST.ToString())
                        {
                            NonTeacherList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ADDUPDATENONTEACHER.ToString())
                        {
                            AddUpdateNonTeacher();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETNONTEACHERBYID.ToString())
                        {
                            GetNonTeacherByID();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.HOLIDAYLIST.ToString())
                        {
                            HolidayList();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.ADDUPDATEHOLIDAY.ToString())
                        {
                            AddUpdateHoliday();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SENDMSGTOTEACHERSTUDENT.ToString())
                        {
                            SendMsgToTeacherStudent();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETHOLIDAYBYID.ToString())
                        {
                            GetHolidayByID();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.DELETEHOLIDAY.ToString())
                        {
                            DeleteHoliday();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.LOGACTIVITYDATA.ToString())
                        {
                            LogActivityData();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.PAYMENTSTATUS.ToString())
                        {
                            PaymentStatus();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETSTANDARDCLASS.ToString())
                        {
                            GetStandardClass();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.CLEARACTIVITY.ToString())
                        {
                            ClearActivity();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.GETAFTEROFFICECLASS.ToString())
                        {
                            GetAfterOfficeClass();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SCHOOLCHANGEPASSWORD.ToString())
                        {
                            SchoolChangePassword();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SEARCHAFTERSCHOOL.ToString())
                        {
                            SearchAfterSchool();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.SEARCHTEACHERBYSCHOOL.ToString())
                        {
                            SearchTeacherBySchool();
                        }
                        else if (OperationManagement.GetOperation("OP") == APIOPERATION.VIEWINVOICEPDF.ToString())
                        {
                            ViewPDFInvoice();
                        }
                        #endregion
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Invalid Token";
                        ResponseObject.Data = "";
                    }

                }

                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(js.Serialize(ResponseObject));
                Response.End();
                #region API

                //Teachers API's List
                TeacherLogin();
                TeacherForgotPassword();
                PickUpList();
                // ClassReport();
                TeacherClassAssignment();
                DailyClassReportFilter();
                StudentListByClass();
                StudentPickUpReport();
                #endregion
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        #region Tokens
        public void GetToken()
        {
            try
            {
                if (OperationManagement.GetPostValues("IMEINO") != "")
                {


                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Token Generated Successfully";
                    ResponseObject.Data = ObjToken.GenerateToken(OperationManagement.GetPostValues("IMEINO"));
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Parameter IMEINO Required";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        #endregion

        #region Parent Section
        public void ParentLogin()
        {
            try
            {

                ParentManagement objParentManagement = new ParentManagement();
                MISStudent Obj = objParentManagement.ParentLogin(OperationManagement.GetPostValues("EMAIL"), OperationManagement.GetPostValues("PASSWORD"));
                if (Obj != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Logged in Successfully";
                    ResponseObject.Data = Obj;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Invalid Credential";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void StudentImageUpload()
        {
            try
            {
                int StudentID = (OperationManagement.GetPostValues("STUDENTID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                string fileName = OperationManagement.GetPostValues("FILENAME");

                ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID);
                if (Obj != null)
                {
                    Obj.Photo = fileName;
                    DB.SaveChanges();
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Image Uploaded Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Image Uploaded Not Successfully";
                    ResponseObject.Data = "";
                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PickerImageUpload()
        {
            try
            {
                int PickerID = (OperationManagement.GetPostValues("PICKERID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("PICKERID"));
                string fileName = OperationManagement.GetPostValues("FILENAME");

                ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == PickerID);
                if (Obj != null)
                {
                    Obj.Photo = fileName;
                    DB.SaveChanges();
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Image Uploaded Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Image Uploaded Not Successfully";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherImageUpload()
        {
            try
            {
                int TeacherID = (OperationManagement.GetPostValues("TEACHERID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID"));
                string fileName = OperationManagement.GetPostValues("FILENAME");

                ISTeacher Obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                if (Obj != null)
                {
                    Obj.Photo = fileName;
                    DB.SaveChanges();
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Image Uploaded Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Image Uploaded Not Successfully";
                    ResponseObject.Data = "";
                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SchoolImageUpload()
        {
            try
            {
                int SchoolID = (OperationManagement.GetPostValues("SCHOOLID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));
                string fileName = OperationManagement.GetPostValues("FILENAME");

                ISSchool Obj = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                if (Obj != null)
                {
                    Obj.Logo = fileName;
                    DB.SaveChanges();
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Image Uploaded Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Image Uploaded Not Successfully";
                    ResponseObject.Data = "";
                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ReceivedMessage()
        {
            try
            {
                int ID = (OperationManagement.GetPostValues("ID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("ID"));
                int TYPE = (OperationManagement.GetPostValues("TYPE") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TYPE"));
                string fileName = OperationManagement.GetPostValues("FILENAME");
                MessageManagement objMessageManagement = new MessageManagement();

                List<MISMessage> ObjList = objMessageManagement.ReceivedMessageList(ID, TYPE);
                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Received Message Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Messages Found";
                    ResponseObject.Data = "";
                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendMessage()
        {
            try
            {
                int ID = (OperationManagement.GetPostValues("ID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("ID"));
                int TYPE = (OperationManagement.GetPostValues("TYPE") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TYPE"));
                string fileName = OperationManagement.GetPostValues("FILENAME");
                MessageManagement objMessageManagement = new MessageManagement();

                List<MISMessage> ObjList = objMessageManagement.SentMessageList(ID, TYPE);
                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Send Message Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Messages Found";
                    ResponseObject.Data = "";
                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DeleteMessage()
        {
            try
            {
                int ID = (OperationManagement.GetPostValues("ID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("ID"));

                ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                DB.ISMessages.Remove(objMessage);
                DB.SaveChanges();

                ResponseObject.Status = "S";
                ResponseObject.Message = "Delete Message Successfully";
                ResponseObject.Data = "";


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ImageUpload()
        {
            try
            {
                var fileName = "";
                var file = (HttpContext.Current.Request.Files.Count > 0) ? HttpContext.Current.Request.Files[0] : null;
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        file = HttpContext.Current.Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {

                            fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload"), fileName);
                            file.SaveAs(path);
                        }
                    }
                }
                ResponseObject.Status = "S";
                ResponseObject.Message = "Image Uploaded Successfully";
                ResponseObject.Data = fileName;

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ParentForgotPassword()
        {
            try
            {

                string Email = OperationManagement.GetPostValues("EMAIL");
                ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.Deleted == true && (p.ParantEmail1 == Email || p.ParantEmail2 == Email) && p.Active == true);
                if (obj != null)
                {

                    string password = CommonOperation.GenerateNewRandom();
                    string Message = "";
                    if (obj.ParantEmail1 == Email)
                    {
                        obj.ParantPassword1 = EncryptionHelper.Encrypt(password);
                        DB.SaveChanges();
                        Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br>Author Name<br>Mobile No :  <br>Email id : <br>", obj.ParantEmail1, obj.ParantEmail1, password);
                    }
                    else
                    {
                        obj.ParantPassword2 = EncryptionHelper.Encrypt(password);
                        DB.SaveChanges();
                        Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br>Author Name<br>Mobile No :  <br>Email id : <br>", obj.ParantEmail2, obj.ParantEmail2, password);
                    }

                    EmailManagement objEmailManagement = new EmailManagement();
                    if (objEmailManagement.SendEmail(Email, "Reset Password", Message))
                    {

                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Password Send Successfully in your Email";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Email sent failed";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Email address not found!";
                    ResponseObject.Data = "";

                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DailyPickUpStatus()
        {
            try
            {

                string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : null;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int StudID = OperationManagement.GetPostValues("STUDID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDID")) : 0;
                string PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? OperationManagement.GetPostValues("PICKERID") : "";
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS") : "";
                string SortBy = OperationManagement.GetPostValues("SORTBY") != "" ? OperationManagement.GetPostValues("SORTBY") : "";
                string OrderBy = OperationManagement.GetPostValues("ORDERBY") != "" ? OperationManagement.GetPostValues("ORDERBY") : "";
                PickupManagement objPikupManagement = new PickupManagement();
                List<MViewStudentPickUp> List = objPikupManagement.DailyPickUpStatus(Date, StudentID, StudID, PickerID, Status, OrderBy, SortBy);
                if (List.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Daily PickUp Status Found Successfully";
                    ResponseObject.Data = List;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Data found for Daily PickUp";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void StudentListByParent()
        {
            try
            {
                ParentManagement objParentManagement = new ParentManagement();
                ClassManagement ObjClassManagement = new ClassManagement();
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                List<MISStudent> ObjList = (from item in DB.ISStudents.Where(p => (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2) && p.StartDate == null && p.Active == true && p.Deleted == true).ToList()
                                            select new MISStudent
                                            {
                                                ID = item.ID,
                                                StudentNo = item.StudentNo,
                                                StudentName = item.StudentName,
                                                ClassID = item.ClassID,
                                                ClassName = item.ISClass.Name,
                                                Photo = item.Photo,
                                                SchoolName = item.ISSchool.Name,
                                                SchoolNumber = item.ISSchool.PhoneNumber,
                                                StrPickUpAverage = String.Format("{0:N2}", ObjClassManagement.GetAverage(item.ClassID, item.ID)),
                                                ObjList = GetSchools(item.ID)
                                            }).ToList();

                if (ObjList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Student Found Successfully";
                    ResponseObject.Data = ObjList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Student Found";
                    ResponseObject.Data = "";

                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AllPickerList()
        {
            try
            {
                {
                    int ParentID = OperationManagement.GetPostValues("PARENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PARENTID")) : 0;
                    int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                    PickerManagement objPickerManagement = new PickerManagement();
                    List<MISPICKER> objList = objPickerManagement.AllPickerList(ParentID, StudentID);

                    if (objList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Picker List Found Successfully";
                        ResponseObject.Data = objList.ToList();

                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Picker List Found";
                        ResponseObject.Data = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PickerList()
        {
            try
            {
                int StudentID = Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                PickerManagement objPickerManagement = new PickerManagement();
                List<MISPICKER> objList = (from item in DB.ISPickers.Where(p => p.StudentID == StudentID && p.Deleted == true).ToList()
                                           select new MISPICKER
                                           {
                                               ID = item.ID,
                                               PickerName = item.FirstName + " " + item.LastName,
                                               Email = item.Email,
                                               Phone = item.Phone,
                                               Photo = item.Photo,
                                               ActiveStatus = item.ActiveStatus,
                                               ParentName = item.ISStudent.ParantName1,
                                               StudentID = item.StudentID,
                                               Active = item.Active,
                                               PickerType = item.PickerType,
                                           }).ToList();
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker List Found";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GeneratePickerCode()
        {
            try
            {

                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                int ParentID = OperationManagement.GetPostValues("PARENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("PARENTID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;

                PickerManagement objPickerManagement = new PickerManagement();
                List<ISPickerAssignment> objPicker = DB.ISPickerAssignments.Where(p => p.ID == PickerID).ToList();
                if (objPicker != null && (objPicker[0].PickCodeExDate == null || string.IsNullOrEmpty(objPicker[0].PickerCode)))
                {
                    List<MISPickerAssignment> objList = objPickerManagement.GeneratePickerCode(PickerID, ParentID, StudentID);
                    if (objList != null)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Picker Code Generated Successfully";
                        ResponseObject.Data = objList;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Picker Code not Generated";
                        ResponseObject.Data = "";
                    }
                }
                else if (objPicker != null && objPicker[0].PickCodeExDate.Value.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                {
                    List<MISPickerAssignment> objList = objPickerManagement.GeneratePickerCode2ndCondition(PickerID, ParentID, StudentID);
                    if (objList != null)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Picker Code Generated Successfully";
                        ResponseObject.Data = objList;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Picker Code not Generated";
                        ResponseObject.Data = "";
                    }
                    //objPicker.PickerCode = CommonOperation.GenerateNewRandom();
                    //objPicker.PickCodeExDate = DateTime.Now;
                    //DB.SaveChanges();
                    //Button btn = (Button)e.Item.FindControl("btnGeneratePickUpCode");
                    //btn.Text = objPicker.PickerCode;
                    //AlertMessageManagement.ServerMessage(Page, "Code Generated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Picker Code Already Generated";
                    ResponseObject.Data = objPicker;
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DeletePicker()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                int ParentID = OperationManagement.GetPostValues("PARENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("PARENTID")) : 0;
                PickerManagement objPickerManagement = new PickerManagement();
                if (objPickerManagement.DeletePicker(PickerID, ParentID))
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Deleted Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Picker not Deleted";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetPicker()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                PickerManagement objPickerManagement = new PickerManagement();
                MISPICKER Obj = objPickerManagement.GetPicker(PickerID);
                if (Obj != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Data Found Successfully";
                    ResponseObject.Data = Obj;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker Data Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AddUpdatePicker()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ParentID = OperationManagement.GetPostValues("PARENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PARENTID")) : 0;
                int PickerType = OperationManagement.GetPostValues("PICKERTYPE") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERTYPE")) : 0;
                string Title = OperationManagement.GetPostValues("TITLE");
                string FirstName = OperationManagement.GetPostValues("FIRSTNAME");
                string LastName = OperationManagement.GetPostValues("LASTNAME");
                string PhotoURL = OperationManagement.GetPostValues("PHOTOURL");
                string Email = OperationManagement.GetPostValues("EMAIL");
                string PhoneNo = OperationManagement.GetPostValues("PHONE");
                bool OneOffPicker = OperationManagement.GetPostValues("ONEOFFPICKER") == "TRUE" ? true : false;

                PickerManagement objPickerManagement = new PickerManagement();
                ISPicker Obj = objPickerManagement.AddUpdatePicker(PickerID, StudentID, SchoolID, ParentID, PickerType, Title, FirstName, LastName, PhotoURL, Email, PhoneNo, OneOffPicker);

                if (PickerID != 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Updated Successfully";
                    ResponseObject.Data = Obj.ID;
                }
                else
                {
                    if (PickerID == 0)
                    {
                        PickerID = Obj.ID;
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Added Successfully";
                    ResponseObject.Data = Obj.ID;
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void EditChildrenAssignment()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                ISPickerAssignment obj = new ISPickerAssignment();
                if (obj != null)
                {
                    List<ISPickerAssignment> objPicker = DB.ISPickerAssignments.Where(p => p.PickerId == PickerID && p.StudentID == StudentID && p.RemoveChildStatus == 0).ToList();
                    if (objPicker.Count == 0)
                    {
                        obj.PickerId = PickerID;
                        obj.StudentID = StudentID;
                        obj.RemoveChildStatus = 0;
                        DB.ISPickerAssignments.Add(obj);
                        DB.SaveChanges();
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Children Assignment Edited Successfully";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Picker already assigned";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Children Assignment Edited";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void RemovePickerFromChild()
        {
            try
            {
                int PickerAssignmentID = OperationManagement.GetPostValues("PICKERASSIGNMENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERASSIGNMENTID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                if (obj != null)
                {
                    obj.RemoveChildStatus = 1;
                    obj.RemoveChildLastUpdateDate = DateTime.Now;
                    obj.RemoveChildLastupdateParent = StudentID.ToString();
                    DB.SaveChanges();

                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Removed from Child Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker Removed from Child";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ViewPickerAssignment()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                PickerManagement objPickerManagement = new PickerManagement();
                //List<MISPickerAssignment> ObjList = objPickerManagement.GetPickerAssignment(StudentID);
                List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(StudentID);

                List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(StudentID);
                foreach (MISPickerAssignment item in objList2)
                {
                    if (objList.Where(p => p.ID == item.ID).Count() <= 0)
                    {
                        objList.Add(item);
                    }
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "PickerAssignment Data Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No PickerAssignment Data Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SchoolMessage()
        {
            try
            {
                int StudentID = (OperationManagement.GetPostValues("STUDENTID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                int ReceiveGroupID = OperationManagement.GetPostValues("RECEIVERGROUPID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERGROUPID")) : 0;
                int ReceiverID = (OperationManagement.GetPostValues("RECEIVERID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERID"));
                string Subject = OperationManagement.GetPostValues("SUBJECT");
                string FileURL = OperationManagement.GetPostValues("ATTACHNMENT");
                string Desc = OperationManagement.GetPostValues("DESCRIPTION");

                if (ReceiverID != 0)
                {
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                    if (ReceiveGroupID == 1)
                    {
                        string Name = DB.ISSchools.SingleOrDefault(p => p.ID == ReceiverID).Name;
                        string Email = DB.ISSchools.SingleOrDefault(p => p.ID == ReceiverID).AdminEmail;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", Name, Subject, Desc, objStudent.StudentName, objStudent.ParantName1, objStudent.ISClass.Name);
                        objEmailManagement.SendEmails(Email, Subject, Message, FileURL);
                    }
                    if (ReceiveGroupID == 2)
                    {
                        string Name = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID).Name;
                        string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID).Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", Name, Subject, Desc, objStudent.StudentName, objStudent.ParantName1, objStudent.ISClass.Name);
                        objEmailManagement.SendEmails(Email, Subject, Message, FileURL);
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Message Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Message not Sent";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PickUpMessage()
        {
            try
            {
                int StudentID = (OperationManagement.GetPostValues("STUDENTID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                string Desc = OperationManagement.GetPostValues("DESCRIPTION");
                int SchoolID = (OperationManagement.GetPostValues("SCHOOLID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));
                if (StudentID != 0)
                {
                    ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                    ISStudent CID = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == ObjStudent.StudentName && p.Active == true);
                    var tlist = DB.ISTeacherClassAssignments.Where(p => p.ISTeacher.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.ClassID == CID.ClassID && p.Active == true).ToList();

                    foreach (var TID in tlist)
                    {
                        string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == TID.TeacherID).Email;
                        try
                        {
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", TID.ISTeacher.Name, "PickUp Message", Desc, CID.StudentName, CID.ParantName1, CID.ISClass.Name);
                            objEmailManagement.SendEmail(Email, "Pickup Message", Message);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogManagement.AddLog(ex);
                        }
                        ISPickUpMessage ObjMessage = new ISPickUpMessage();
                        ObjMessage.SchoolID = CID.SchoolID;
                        ObjMessage.Message = Desc;
                        ObjMessage.ReceiverID = TID.TeacherID;
                        ObjMessage.ClassID = CID.ClassID;
                        ObjMessage.SendID = CID.ID;
                        ObjMessage.SenderName = CID.ParantName1;
                        ObjMessage.Viewed = false;
                        ObjMessage.Active = true;
                        ObjMessage.Deleted = true;
                        ObjMessage.CreatedBy = CID.ID;
                        ObjMessage.CreatedDateTime = DateTime.Now;
                        DB.ISPickUpMessages.Add(ObjMessage);
                        DB.SaveChanges();
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "PickUp Message Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "PickUp Message not Sent";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void EditMoreChildrenAssignment()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                string StudentID = OperationManagement.GetPostValues("STUDENTID");
                string RemovableStudentID = OperationManagement.GetPostValues("REMOVESTUDENTID");
                if (StudentID != "" || RemovableStudentID != "")
                {
                    if (StudentID != "")
                    {
                        string[] Students = StudentID.Split(',');
                        foreach (var item in Students)
                        {
                            int SID = Convert.ToInt32(item);
                            ISPickerAssignment obj = new ISPickerAssignment();
                            List<ISPickerAssignment> objPicker = DB.ISPickerAssignments.Where(p => p.PickerId == PickerID && p.StudentID == SID && p.RemoveChildStatus == 0).ToList();
                            if (objPicker.Count > 0)
                            {
                                continue;
                            }
                            else
                            {
                                obj.PickerId = PickerID;
                                obj.StudentID = SID;
                                obj.RemoveChildStatus = 0;
                                DB.ISPickerAssignments.Add(obj);
                                DB.SaveChanges();
                            }
                        }
                    }
                    if (RemovableStudentID != "")
                    {
                        string[] RStudents = RemovableStudentID.Split(',');
                        foreach (var items in RStudents)
                        {
                            int StID = Convert.ToInt32(items);
                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.PickerId == PickerID && p.StudentID == StID && p.RemoveChildStatus == 0);
                            if (objPicker != null)
                            {
                                objPicker.RemoveChildStatus = 1;
                                objPicker.RemoveChildLastUpdateDate = DateTime.Now;
                                objPicker.RemoveChildLastupdateParent = items;
                                DB.SaveChanges();
                            }
                        }
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Children Assignment Edited Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Children Assignment Edited";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void MakePickerForToday()
        {
            try
            {
                int PickerAssiID = OperationManagement.GetPostValues("PICKERASSIGNMENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERASSIGNMENTID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : DateTime.Now.ToString("dd/MM/yyyy");
                string Message = "";
                DateTime dt = DateTime.Now;
                if (Date != "")
                {
                    string dates = Date;
                    string Format = "";
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    }
                    else
                    {
                        Format = dates;
                    }
                    dt = Convert.ToDateTime(Format);
                }
                List<ISPickerAssignment> Objes = DB.ISPickerAssignments.Where(p => p.ID == PickerAssiID && p.RemoveChildStatus != 1).ToList();
                if (Objes.Count > 0)
                {
                    List<ISPickerAssignment> objList = DB.ISPickerAssignments.Where(p => p.StudentID == StudentID && p.RemoveChildStatus != 1 && DbFunctions.TruncateTime(p.StudentPickAssignDate) == DbFunctions.TruncateTime(dt)).ToList();
                    if (objList.Count > 0)
                    {
                        foreach (var item in objList)
                        {
                            item.StudentPickAssignDate = null;
                            DB.SaveChanges();
                        }
                        ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssiID && p.RemoveChildStatus != 1);
                        if (obj != null)
                        {
                            obj.StudentPickAssignFlag = 1;
                            obj.StudentPickAssignLastUpdateParent = StudentID.ToString();
                            obj.StudentPickAssignDate = dt;
                            DB.SaveChanges();
                        }
                        Message = "Picker Made for Today Successfully";
                    }
                    else
                    {
                        ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssiID && p.RemoveChildStatus != 1);
                        if (obj != null)
                        {
                            obj.StudentPickAssignFlag = 1;
                            obj.StudentPickAssignLastUpdateParent = StudentID.ToString();
                            obj.StudentPickAssignDate = dt;
                            DB.SaveChanges();
                        }
                        Message = "Picker Made for Today Successfully";
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = Message;
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker Made for Today";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void StudentWiseSchool()
        {
            try
            {
                string ParentEmail = OperationManagement.GetPostValues("PARENTEMAIL") != "" ? OperationManagement.GetPostValues("PARENTEMAIL") : "";

                List<ISStudent> obj = (from item in DB.ISStudents.Where(p => (p.ParantEmail1 == ParentEmail || p.ParantEmail2 == ParentEmail) && p.Deleted == true).ToList()
                                       group item by item.SchoolID into g
                                       select new ISStudent
                                       {
                                           SchoolID = g.Key,
                                           ISSchool = g.Select(p => p.ISSchool).FirstOrDefault()
                                       }).ToList(); //DB.ISStudents.Where(p => (p.ParantEmail1 == ParentEmail || p.ParantEmail2 == ParentEmail) && p.Deleted == true).GroupBy(m => m.SchoolID).ToList();
                if (obj.Count > 0)
                {
                    List<MISSchool1> objList = new List<MISSchool1>();
                    foreach (var item in obj)
                    {
                        objList.Add(new MISSchool1()
                        {
                            SchoolID = item.SchoolID,
                            SchoolName = item.ISSchool.Name
                        });
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "School Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No School Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void MakeActiveDeActive()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                bool Active = OperationManagement.GetPostValues("ACTIVE") != "" ? OperationManagement.GetPostValues("ACTIVE") == "TRUE" ? true : false : false;
                ISPicker obj = DB.ISPickers.SingleOrDefault(p => p.ID == PickerID && p.Deleted == true);
                string Message;
                if (obj != null)
                {
                    if (Active == true)
                    {
                        obj.Active = true;
                        DB.SaveChanges();
                        Message = "Picker Active Successfully";
                    }
                    else
                    {
                        obj.Active = false;
                        DB.SaveChanges();
                        List<ISPickerAssignment> ObjPickerAssi = DB.ISPickerAssignments.Where(p => p.PickerId == PickerID).ToList();
                        DB.ISPickerAssignments.RemoveRange(ObjPickerAssi);
                        DB.SaveChanges();
                        Message = "Picker DeActive Successfully";
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = Message;
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Picker Status Not Changed";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PickersByStudentID()
        {
            try
            {
                {
                    int StudentID = Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                    PickerManagement objPickerManagement = new PickerManagement();
                    List<MISPICKER> objList = objPickerManagement.AllPickerByStudent(StudentID);
                    if (objList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Picker List Found Successfully";
                        ResponseObject.Data = objList.ToList();

                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Picker List Found";
                        ResponseObject.Data = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ResetPicker()
        {
            try
            {
                int PickerAssiID = OperationManagement.GetPostValues("PICKERASSIGNMENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERASSIGNMENTID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : DateTime.Now.ToString("dd/MM/yyyy");
                DateTime dt = DateTime.Now;
                if (Date != "")
                {
                    string dates = Date;
                    string Format = "";
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    }
                    else
                    {
                        Format = dates;
                    }
                    dt = Convert.ToDateTime(Format);
                }
                ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssiID && p.RemoveChildStatus != 1);
                if (obj != null)
                {
                    obj.StudentPickAssignDate = null;
                    DB.SaveChanges();
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Reset Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker Reset";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendMsgToTeacherSchool()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                int ReceiveGroupID = OperationManagement.GetPostValues("RECEIVERGROUPID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERGROUPID"));
                int ReceiverID = OperationManagement.GetPostValues("RECEIVERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERID"));
                string Subject = OperationManagement.GetPostValues("SUBJECT");
                string FileURL = OperationManagement.GetPostValues("ATTACHNMENT") != "" ? Server.MapPath("~/Upload/" + OperationManagement.GetPostValues("ATTACHNMENT")) : "";
                string Desc = OperationManagement.GetPostValues("DESCRIPTION");
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID);
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjStudent.SchoolID);
                if (ReceiveGroupID != 0)
                {
                    if (ReceiveGroupID == 1)
                    {
                        ISSchool ObSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ReceiverID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", ObSchool.Name, Subject, Desc, ObjStudent.StudentName, ObjStudent.ParantName1, ObjStudent.ISClass.Name);
                        //objEmailManagement.SendEmails(ObSchool.AdminEmail, Subject, Message, FileURL);
                        if (FileURL != "")
                        {
                            objEmailManagement.SendEmails(ObSchool.AdminEmail, Subject, Message, FileURL);
                        }
                        else
                        {
                            objEmailManagement.SendEmail(ObSchool.AdminEmail, Subject, Message);
                        }
                    }
                    if (ReceiveGroupID == 2)
                    {
                        ISTeacher ObTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", ObTeacher.Name, Subject, Desc, ObjStudent.StudentName, ObjStudent.ParantName1, ObjStudent.ISClass.Name);
                        //objEmailManagement.SendEmails(ObTeacher.Email, Subject, Message, FileURL);
                        if (FileURL != "")
                        {
                            objEmailManagement.SendEmails(ObTeacher.Email, Subject, Message, FileURL);
                        }
                        else
                        {
                            objEmailManagement.SendEmail(ObTeacher.Email, Subject, Message);
                        }
                    }
                    if (ReceiveGroupID == 4)
                    {
                        ISTeacher ObTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", ObTeacher.Name, Subject, Desc, ObjStudent.StudentName, ObjStudent.ParantName1, ObjStudent.ISClass.Name);
                        //objEmailManagement.SendEmails(ObTeacher.Email, Subject, Message, FileURL);
                        if (FileURL != "")
                        {
                            objEmailManagement.SendEmails(ObTeacher.Email, Subject, Message, FileURL);
                        }
                        else
                        {
                            objEmailManagement.SendEmail(ObTeacher.Email, Subject, Message);
                        }
                    }
                    if (StudentID != 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Message Sent Successfully";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Message Not Sent";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Please Select ReceiverGroup";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void GetPickerTypeList()
        {
            try
            {
                List<MISPickerType> people = new List<MISPickerType>{
                   new MISPickerType{ID = 1, PickerType = "Individual Picker"},
                   new MISPickerType{ID = 2, PickerType = "Organisation Picker"}
                   };


                if (people.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker Types Found Successfully";
                    ResponseObject.Data = people;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker Type Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void StudentByPicker()
        {
            try
            {
                int PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                ISPickerAssignment obj = new ISPickerAssignment();
                if (obj != null)
                {
                    List<MISPickerAssignment> objList = new List<MISPickerAssignment>();
                    List<MISStudent> ObjStudentList = new List<MISStudent>();
                    objList = (from item in DB.ISPickerAssignments.Where(p => p.RemoveChildStatus == 0 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))).ToList()
                               join item2 in DB.ISPickers.Where(p => p.ID == PickerID && p.Active == true && p.Deleted == true && p.SchoolID == SchoolID).ToList() on item.PickerId equals item2.ID
                               select new MISPickerAssignment
                               {
                                   ID = item.ID,
                                   StudentID = item.StudentID,
                                   PickerId = item2.ID,
                                   PickerName = item2.FirstName + " " + item2.LastName,
                                   StrStudentPickAssignDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                               }).OrderBy(p => p.StudentID).ToList();
                    foreach (var item in objList)
                    {
                        MISStudent ObjStudent = new MISStudent();
                        ObjStudent.ID = Convert.ToInt32(item.StudentID);
                        ObjStudent.StudentName = DB.ISStudents.SingleOrDefault(p => p.ID == item.StudentID).StudentName;
                        ObjStudentList.Add(ObjStudent);
                    }
                    if (ObjStudentList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Children Assignment Found Successfully";
                        ResponseObject.Data = ObjStudentList;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Children Assignment Found";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Children Assignment Edited";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ParentProfile()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string ParentEmail = OperationManagement.GetPostValues("PARENTEMAIL") != "" ? OperationManagement.GetPostValues("PARENTEMAIL") : "";
                ProfileManagement objProfileManagement = new ProfileManagement();
                MISStudent objList = objProfileManagement.GetStudent(StudentID, ParentEmail);
                if (objList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Parent Data Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Parent Data Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ParentChangePassword()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string ParentEmail = OperationManagement.GetPostValues("PARENTEMAIL") != "" ? OperationManagement.GetPostValues("PARENTEMAIL") : "";
                string ParentPASSWORD = OperationManagement.GetPostValues("PASSWORD") != "" ? OperationManagement.GetPostValues("PASSWORD") : "";
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID);
                if (objStudent != null)
                {
                    if (ParentEmail == objStudent.ParantEmail1)
                    {
                        objStudent.ParantPassword1 = EncryptionHelper.Encrypt(ParentPASSWORD);
                    }
                    if (ParentEmail == objStudent.ParantEmail2)
                    {
                        objStudent.ParantPassword2 = EncryptionHelper.Encrypt(ParentPASSWORD);
                    }
                    objStudent.ModifyBy = StudentID;
                    objStudent.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();

                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Parent Password Reset Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Password not Reset";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AllPickers()
        {
            try
            {
                int StudentID = Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                PickerManagement objPickerManagement = new PickerManagement();
                List<MISPICKER> objList = (from item in DB.ISPickers.Where(p => p.StudentID == StudentID && p.Deleted == true).ToList()
                                           select new MISPICKER
                                           {
                                               ID = item.ID,
                                               PickerName = item.FirstName + " " + item.LastName,
                                               Email = item.Email,
                                               Phone = item.Phone,
                                               Photo = item.Photo,
                                               ActiveStatus = item.ActiveStatus,
                                               ParentName = item.ISStudent.ParantName1,
                                               StudentID = item.StudentID,
                                               Active = item.Active,
                                               PickerType = item.PickerType,
                                           }).ToList();
                objList = objList.Where(p => !p.PickerName.Contains("(")).ToList();
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker List Found";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PickUpStatus()
        {
            try
            {
                List<MISPickUpStatus> ObjList = (from item in DB.ISPickUpStatus.Where(p => p.Active == true && p.Deleted == true).ToList()
                                                 select new MISPickUpStatus
                                                 {
                                                     ID = item.ID,
                                                     Status = item.Status
                                                 }).ToList();
                if (ObjList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Status List Found Successfully";
                    ResponseObject.Data = ObjList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Status List Found";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetAssignedPickers()
        {
            try
            {
                int ParentID = OperationManagement.GetPostValues("PARENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PARENTID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                List<ISStudent> ObjLists = new List<ISStudent>();
                if (StudentID != 0)
                {
                    ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true && p.Active == true);
                    ISStudent ObjStudents = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == ObjStudent.StudentName && p.SchoolID == SchoolID && p.Deleted == true);
                    if (ObjStudents != null)
                    {
                        ObjLists = DB.ISStudents.Where(p => p.ID == ObjStudents.ID && p.Deleted == true).ToList();
                    }
                }
                else
                {
                    var obj = DB.ISStudents.SingleOrDefault(p => p.ID == ParentID && p.Deleted == true);
                    ObjLists = DB.ISStudents.Where(p => p.SchoolID == SchoolID && (p.ParantEmail1 == obj.ParantEmail1 || p.ParantEmail2 == obj.ParantEmail2) && p.Deleted == true && p.Active == true).ToList();
                }
                PickerManagement objPickerManagement = new PickerManagement();
                List<MISPickerAssignment> objPickerList = new List<MISPickerAssignment>();
                foreach (var item in ObjLists)
                {
                    List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(item.ID);
                    List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(item.ID);

                    foreach (MISPickerAssignment items in objList)
                    {
                        objPickerList.Add(items);
                    }
                    foreach (MISPickerAssignment items in objList2)
                    {
                        objPickerList.Add(items);
                    }
                }
                var list = objPickerList.Select(p => new { PickerName = p.PickerName }).Distinct().ToList();
                if (list.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Picker List Found Successfully";
                    ResponseObject.Data = list.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Picker List Found";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public List<MISSchool1> GetSchools(int StudentID)
        {
            ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
            List<MISSchool1> ObjStudent = new List<MISSchool1>();
            if (Obj != null)
            {
                var shids = DB.ISStudents.Where(p => p.StudentName == Obj.StudentName && p.Deleted == true).Select(q => q.SchoolID).Distinct().ToList();

                ObjStudent = (from item in DB.ISSchools.Where(p => shids.Contains(p.ID) && p.Deleted == true).ToList()
                              select new MISSchool1
                              {
                                  SchoolID = item.ID,
                                  SchoolName = item.Name,
                                  SchoolPhoneNumber = item.PhoneNumber
                              }).ToList();
                return ObjStudent;
            }
            return ObjStudent;
        }
        public void GetStudentSchools()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                if (StudentID != 0)
                {
                    ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                    if (Obj != null)
                    {
                        var shids = DB.ISStudents.Where(p => p.StudentName == Obj.StudentName && p.Deleted == true).Select(q => q.SchoolID).Distinct().ToList();

                        List<MISSchool> ObjStudent = (from item in DB.ISSchools.Where(p => shids.Contains(p.ID) && p.Deleted == true).ToList()
                                                      select new MISSchool
                                                      {
                                                          ID = item.ID,
                                                          Name = item.Name
                                                      }).ToList();

                        if (ObjStudent.Count > 0)
                        {
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "School List Found Successfully";
                            ResponseObject.Data = ObjStudent.ToList();
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "No School List Found";
                            ResponseObject.Data = "";
                        }
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Please Pass StudentID";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Invalid Student";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        #endregion

        #region Teacher Section

        //Teachers API's List
        public void TeacherLogin()
        {
            try
            {

                TeacherManagement objTeacherManagement = new TeacherManagement();
                MISTeacher Obj = objTeacherManagement.TeacherLogin(OperationManagement.GetPostValues("EMAIL"), OperationManagement.GetPostValues("PASSWORD"));
                if (Obj != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Logged in Successfully";
                    ResponseObject.Data = Obj;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Invalid Credential";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherForgotPassword()
        {
            try
            {

                string Email = OperationManagement.GetPostValues("EMAIL");
                ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.Deleted == true && (p.Email == Email) && p.Active == true);
                if (obj != null)
                {

                    string password = CommonOperation.GenerateNewRandom();
                    string Message = "";
                    if (obj.Email == Email)
                    {
                        obj.Password = EncryptionHelper.Encrypt(password);
                        DB.SaveChanges();
                        Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br>Author Name<br>Mobile No :  <br>Email id : <br>", obj.Email, obj.Email, obj.Password);
                    }


                    EmailManagement objEmailManagement = new EmailManagement();
                    if (objEmailManagement.SendEmail(Email, "Reset Password", Message))
                    {

                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Password Send Successfully in your Email";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Email sent failed";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Email address not found!";
                    ResponseObject.Data = "";

                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherProfile()
        {
            try
            {
                int TeacherID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                TeacherManagement objTeacherManagement = new TeacherManagement();
                MISTeacher objList = objTeacherManagement.GetTeacher(TeacherID);
                if (objList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Teacher Data Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Teacher Data Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherResetPassword()
        {
            try
            {
                int TeacherID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                string TeacherPass = OperationManagement.GetPostValues("PASSWORD") != "" ? OperationManagement.GetPostValues("PASSWORD") : "";
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                if (objTeacher != null)
                {
                    objTeacher.Password = EncryptionHelper.Encrypt(TeacherPass);
                    objTeacher.ModifyBy = TeacherID;
                    objTeacher.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();

                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Teacher Password Reset Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Password not Reset";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PickUpList()
        {
            try
            {
                {
                    int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                    int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                    string CreatedDate = OperationManagement.GetPostValues("CREATEDATE");
                    int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                    bool ChkValue = false;
                    PickupManagement objPickupManagement = new PickupManagement();
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        objList = objPickupManagement.PickupList(TeacherID, ClassID, CreatedDate).OrderByDescending(p => p.PickStatus).ToList();
                        ChkValue = objPickupManagement.BindPickUpCheckBox(SchoolID, ClassID, objList);
                    }
                    else
                    {
                        objList = objPickupManagement.PickupsList(ClassID, CreatedDate).OrderByDescending(p => p.PickStatus).ToList();
                        ChkValue = objPickupManagement.BindPickUpCheckBox(SchoolID, ClassID, objList);
                    }
                    if (objList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "PickUp List Found Successfully";
                        ResponseObject.Data = objList;
                        ResponseObject.Data1 = ChkValue;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No PickUp List Found";
                        ResponseObject.Data = "";
                        ResponseObject.Data1 = ChkValue;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DailyClassReport()
        {
            try
            {
                {
                    int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                    int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                    string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : "";
                    ClassManagement objClassManagement = new ClassManagement();

                    List<MViewStudentPickUp> objList = objClassManagement.ClassReport(ClassID, TeacherID, Date);
                    string PickUpAvg = "";
                    double? Count = objList.Sum(p => p.StudentPickUpAverage);
                    double Counts = objList.Count;
                    if (Count != 0)
                    {
                        double? Avg = Count / Counts;
                        PickUpAvg = String.Format("{0:N2}", Avg);
                    }
                    else
                    {
                        PickUpAvg = "0.00";
                    }

                    if (objList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Class Report Found Successfully";
                        ResponseObject.Data = objList;
                        ResponseObject.Data1 = PickUpAvg;

                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Class Report Found";
                        ResponseObject.Data = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherClassAssignment()
        {
            try
            {
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                TeacherManagement objTeacherManagement = new TeacherManagement();
                List<MISTeacherClassAssignment> ObjList = objTeacherManagement.TeacherClassAssignment(TeacherID).Where(p => p.SchoolID == SchoolID).ToList();
                if (ObjList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Class Assignment Found Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Class Assignment Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DailyClassReportFilter()
        {
            try
            {
                {
                    int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                    int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                    string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : "";
                    int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                    int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                    string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS").ToString() : "";
                    string SortBy = OperationManagement.GetPostValues("SORTBY") != "" ? OperationManagement.GetPostValues("SORTBY").ToString() : "";
                    string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                    ClassManagement objClassManagement = new ClassManagement();
                    //List<MViewStudentPickUp> objList = objClassManagement.DailyClassReportFilter(ClassID, Date, TeacherID, StudentID, Status, OrderBy, SortBy);
                    List<MViewStudentPickUp> objLists = objClassManagement.DailyClassReports(SchoolID, Date, ClassID, TeacherID, StudentID, Status, OrderBy, SortBy);
                    if (objLists.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Class Daily Report Found Successfully";
                        ResponseObject.Data = objLists;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Class Daily Report Found";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void StudentListByClass()
        {
            try
            {
                {
                    int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                    int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                    string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : "";
                    string OP = OperationManagement.GetPostValues("OP");
                    int TypeID = OperationManagement.GetPostValues("CLASSTYPEID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSTYPEID")) : 0;
                    string AfterSchool = OperationManagement.GetPostValues("AFTERSCHOOLTYPE") != "" ? OperationManagement.GetPostValues("AFTERSCHOOLTYPE") : "";

                    ClassManagement objTeacherManagement = new ClassManagement();
                    List<MISStudent> ObjST = new List<MISStudent>();
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    List<ISStudent> ObjStudent = new List<ISStudent>();
                    if (OP == "Teacher")
                    {
                        if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                        {
                            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                            //if (_Class.Name.Contains("Outside Class"))
                            //{
                            //    string Format = "";
                            //    DateTime dt = DateTime.Now;
                            //    if (Date != "")
                            //    {
                            //        string dates = Date;
                            //        if (dates.Contains("/"))
                            //        {
                            //            string[] arrDate = dates.Split('/');
                            //            Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                            //        }
                            //        else
                            //        {
                            //            Format = dates;
                            //        }

                            //    }
                            //    dt = Convert.ToDateTime(Format);
                            //    ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && p.ClassID == ClassID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList();
                            //}
                            //else
                            {
                                ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && p.ClassID == ClassID).ToList();
                            }
                        }
                        else
                        {
                            ObjStudent = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && p.ClassID == ClassID).ToList();
                        }
                        if (ObjStudent.Count > 0)
                        {
                            ObjST = (from item in ObjStudent.ToList()
                                     select new MISStudent
                                     {
                                         ID = item.ID,
                                         StudentName = item.StudentName,
                                     }).ToList();
                        }
                    }
                    else
                    {
                        ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);

                        if (TypeID == (int)EnumsManagement.CLASSTYPE.Standard)
                        {
                            if (ObjClass.Name.Contains("Outside"))
                            {
                                ObjST = objTeacherManagement.StudentListByExtClass(SchoolID, ClassID);
                            }
                            else
                            {
                                ObjST = objTeacherManagement.StudentListByClass(SchoolID, ClassID);
                            }
                        }
                        if (TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                        {
                            ObjST = objTeacherManagement.StudentListByOfficeClass(SchoolID);
                        }
                        if (TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                        {
                            if (AfterSchool == "External")
                            {
                                ObjST = objTeacherManagement.StudentListByExtClass(SchoolID, ClassID);
                            }
                            else
                            {
                                ObjST = objTeacherManagement.StudentListByInternalClass(SchoolID);
                            }
                        }
                    }
                    if (ObjST.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Student List Found Successfully";
                        ResponseObject.Data = ObjST.ToList();

                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Student List Found";
                        ResponseObject.Data = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void StudentPickUpReport()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM") != "" ? OperationManagement.GetPostValues("DATEFROM") : "";
                string DateTo = OperationManagement.GetPostValues("DATETO") != "" ? OperationManagement.GetPostValues("DATETO") : "";
                string PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToString(OperationManagement.GetPostValues("PICKERID")) : "";
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS") : "";
                string SortBy = OperationManagement.GetPostValues("SORTBY") != "" ? OperationManagement.GetPostValues("SORTBY") : "";
                string OrderBy = OperationManagement.GetPostValues("ORDERBY") != "" ? OperationManagement.GetPostValues("ORDERBY") : "";
                int StudID = OperationManagement.GetPostValues("STUDID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDID")) : 0;
                string OP = OperationManagement.GetPostValues("OP");
                PickupManagement objPickupManagement = new PickupManagement();
                ClassManagement objClassManagement = new ClassManagement();
                List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
                string PickUpAverage = "0.00";
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                if (OP == "Parent")
                {
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        if (ObjStudent != null)
                        {
                            ISStudent stud = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == ObjStudent.StudentName && p.Active == true);
                            objList = objPickupManagement.StudentPickUpReportsWithSchool(stud.ID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, stud.ID, SchoolID);
                            PickUpAverage = String.Format("{0:N2}", objClassManagement.GetAverage(stud.ClassID, stud.ID));
                        }
                    }
                    else
                    {
                        objList = objPickupManagement.StudentPickUpReportsWithSchool(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID, SchoolID);//StudentPickUpReports(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID);
                        PickUpAverage = String.Format("{0:N2}", objClassManagement.GetAverage(ObjStudent.ClassID, ObjStudent.ID));
                    }
                }
                if (OP == "Teacher")
                {
                    if (ObjStudent != null)
                    {
                        List<MGetAttendenceData> ObjList = new List<MGetAttendenceData>();
                        if (ObjStudent.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            objList = objPickupManagement.StudentPickUpReports(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID);
                        }
                        else
                        {
                            objList = objPickupManagement.StudentPickUpReportsAfterSchool(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID);
                        }
                    }
                    PickUpAverage = String.Format("{0:N2}", objClassManagement.GetAverage(ObjStudent.ClassID, ObjStudent.ID));
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Student PickUp Report Found Successfully";
                    ResponseObject.Data = objList;
                    ResponseObject.Data1 = PickUpAverage;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Student PickUp Report Found";
                    ResponseObject.Data = "";
                    ResponseObject.Data1 = PickUpAverage;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendUnPickAlert()
        {
            try
            {
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string StatusText = OperationManagement.GetPostValues("STATUSTEXT") != "" ? OperationManagement.GetPostValues("STATUSTEXT") : "";
                ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true && p.Active == true);
                if (objStudent != null)
                {
                    if (StatusText == "Mark as Absent")
                    {
                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, "Mark as Absent", "Hello Dear Parents, Your Child is Absent today.", ObjTeacher.Name, ObjTeacher.ISSchool.Name);
                        objEmailManagement.SendEmail(ObSt.ParantEmail1, "Mark as Absent", Message);
                        if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
                        {
                            objEmailManagement.SendEmail(ObSt.ParantEmail2, "Mark as Absent", Message);
                        }
                    }
                    else if (StatusText.Contains("After-School"))
                    {
                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Subject = "";
                        if (objStudent.ISClass.AfterSchoolType == "Internal")
                        {
                            Subject = "Internal After School";
                        }
                        else
                        {
                            Subject = "External After School";
                        }
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, Subject, "Hello Dear Parents, Your Child will sent to " + Subject + "", ObjTeacher.Name, ObjTeacher.ISSchool.Name);
                        objEmailManagement.SendEmail(ObSt.ParantEmail1, Subject, Message);
                        if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
                        {
                            objEmailManagement.SendEmail(ObSt.ParantEmail2, Subject, Message);
                        }
                    }
                    else if (StatusText.Contains("Office"))
                    {

                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, "Office", "Hello Dear Parents, Your Child will Send to Driver.", ObjTeacher.Name, ObjTeacher.ISSchool.Name);
                        objEmailManagement.SendEmail(ObSt.ParantEmail1, "Office", Message);
                        if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
                        {
                            objEmailManagement.SendEmail(ObSt.ParantEmail2, "Office", Message);
                        }
                    }
                    else
                    {
                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, StatusText, "Hello Dear Parents, Your Child is " + StatusText + " from School.", ObjTeacher.Name, ObjTeacher.ISSchool.Name);
                        objEmailManagement.SendEmail(ObSt.ParantEmail1, StatusText, Message);
                        if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
                        {
                            objEmailManagement.SendEmail(ObSt.ParantEmail2, StatusText, Message);
                        }
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "UnPick Alert Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "UnPick Alert not Sent Successfully";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ViewPickerForToday()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                PickerManagement objPickerManagement = new PickerManagement();
                List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(StudentID);

                List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(StudentID).Where(p => p.PickerType != 1 || p.PickerName.Contains("(")).ToList();
                foreach (MISPickerAssignment item in objList2)
                {
                    if (objList.Where(p => p.ID == item.ID).Count() <= 0)
                    {
                        objList.Add(item);
                    }
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "PickerAssignment Data Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No PickerAssignment Data Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public string GetPickupStatus(int SchoolID)
        {
            ISSchool school = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
            if (school != null)
            {
                DateTime tdate = DateTime.Now;
                DateTime schclosetime = school.ClosingTime.Value;
                DateTime schclose = new DateTime(tdate.Year, tdate.Month, tdate.Day, schclosetime.Hour, schclosetime.Minute, 0);
                DateTime schLate = schclose.AddMinutes(Convert.ToInt32(school.LateMinAfterClosing));
                DateTime schCharge = schLate.AddMinutes(Convert.ToInt32(school.ChargeMinutesAfterClosing));
                DateTime schReport = schCharge.AddMinutes(Convert.ToInt32(school.ReportableMinutesAfterClosing));
                if (schclose < tdate)
                {
                    if (schLate < tdate)
                    {
                        if (schCharge < tdate)
                        {
                            if (schReport < tdate)
                            {
                                return "Picked(Reportable)";
                            }
                            else
                            {
                                return "Picked(Chargeable)";
                            }
                        }
                        else
                        {
                            return "Picked(Late)";
                        }
                    }
                    else
                    {
                        return "Picked";
                    }
                }
                else
                {
                    return "Picked";
                }
            }
            else
            {
                return "Picked";
            }
        }
        public void ConfirmPickup()
        {
            try
            {
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string PickerCode = OperationManagement.GetPostValues("PICKERCODE");
                int PickerAssignmentID = OperationManagement.GetPostValues("PICKERASSIGNMENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("PICKERASSIGNMENTID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                DateTime dtNow = DateTime.Now;
                ISPickerAssignment objPickers = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID && (p.PickerCode != null || p.PickerCode != ""));
                ISStudent _student = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                ISSchool school = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                if (school != null)
                {
                    if (school.isAttendanceModule == true)
                    {
                        ISAttendance ObjAttendence = DB.ISAttendances.SingleOrDefault(p => p.StudentID == objPickers.StudentID && p.Status.Contains("Present") && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dtNow));
                        if (ObjAttendence != null)
                        {
                            if (DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).PickerType == (int)EnumsManagement.PICKERTYPE.Individual && !DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).FirstName.Contains("("))
                            {
                                List<ISPickup> objPicks = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "Office" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                if (objPicks.Count > 0)
                                {
                                    List<ISTeacherClassAssignment> ObjAssign = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Name.Contains("(Office)") && p.ISClass.SchoolID == SchoolID && p.Active == true).ToList();
                                    if (ObjAssign.Count > 0)
                                    {
                                        if (objPickers != null && objPickers.PickCodeExDate != null)
                                        {
                                            if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dtNow.ToString("dd/MM/yyyy"))
                                            {
                                                ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                                if (objPickers.PickerCode == PickerCode)
                                                {
                                                    if (Objs.CreatedDateTime >= dtNow.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                            ResponseObject.Data = "";
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                            if (pickup != null)
                                                            {
                                                                pickup.PickTime = dtNow;
                                                                pickup.PickDate = dtNow;
                                                                pickup.PickerID = objPickers.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPickers.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPickers.PickerId;
                                                                obj.TeacherID = TeacherID;
                                                                obj.PickTime = dtNow;
                                                                obj.PickDate = dtNow;
                                                                obj.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                            }
                                                            ResponseObject.Status = "S";
                                                            ResponseObject.Message = "Student can Leave now Successfully";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Picker does not allow to Pick Child";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                            if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                            {
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                    ResponseObject.Data = "";
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                    if (pickup != null)
                                                    {
                                                        pickup.PickTime = dtNow;
                                                        pickup.PickDate = dtNow;
                                                        pickup.PickerID = objPicker.PickerId;
                                                        pickup.PickStatus = GetPickupStatus(SchoolID);
                                                        DB.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = TeacherID;
                                                        obj.PickTime = dtNow;
                                                        obj.PickDate = dtNow;
                                                        obj.PickStatus = GetPickupStatus(SchoolID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                    }
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = TeacherID;
                                                        objp.PickTime = dtNow;
                                                        objp.PickDate = dtNow;
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                    }
                                                    ResponseObject.Status = "S";
                                                    ResponseObject.Message = "Student can Leave now Successfully";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                            {
                                                if (objPicker.PickerCode == PickerCode)
                                                {
                                                    if (Obj.CreatedDateTime >= dtNow.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                            ResponseObject.Data = "";
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                            if (pickup != null)
                                                            {
                                                                pickup.PickTime = dtNow;
                                                                pickup.PickDate = dtNow;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPicker.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPicker.PickerId;
                                                                obj.TeacherID = TeacherID;
                                                                obj.PickTime = dtNow;
                                                                obj.PickDate = dtNow;
                                                                obj.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                            }
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = TeacherID;
                                                                objp.PickTime = dtNow;
                                                                objp.PickDate = dtNow;
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                            }
                                                            ResponseObject.Status = "S";
                                                            ResponseObject.Message = "Student can Leave now Successfully";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Picker does not allow to Pick Child";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "InValid Picker";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "You are not Able to do Child Pickup";
                                        ResponseObject.Data = "";
                                    }
                                }
                                else
                                {
                                    if (objPickers != null && objPickers.PickCodeExDate != null)
                                    {
                                        if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dtNow.ToString("dd/MM/yyyy"))
                                        {
                                            ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                            if (objPickers.PickerCode == PickerCode)
                                            {
                                                if (Objs.CreatedDateTime >= dtNow.AddDays(-30))
                                                {
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                        ResponseObject.Data = "";
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                        if (pickup != null)
                                                        {
                                                            pickup.PickTime = dtNow;
                                                            pickup.PickDate = dtNow;
                                                            pickup.PickerID = objPickers.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPickers.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPickers.PickerId;
                                                            obj.TeacherID = TeacherID;
                                                            obj.PickTime = dtNow;
                                                            obj.PickDate = dtNow;
                                                            obj.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                        }
                                                        List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                        if (objss.Count > 0)
                                                        {
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = TeacherID;
                                                                objp.PickTime = dtNow;
                                                                objp.PickDate = dtNow;
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                        ResponseObject.Status = "S";
                                                        ResponseObject.Message = "Student can Leave now Successfully";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Picker does not allow to Pick Child";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "Please Enter Valid Picker Code";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                        ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                        if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                        {
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                            if (objs.Count > 0)
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                ResponseObject.Data = "";
                                            }
                                            else
                                            {
                                                List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                if (objss.Count > 0)
                                                {
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = TeacherID;
                                                        objp.PickTime = dtNow;
                                                        objp.PickDate = dtNow;
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                    }
                                                }
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                if (pickup != null)
                                                {
                                                    pickup.PickTime = dtNow;
                                                    pickup.PickDate = dtNow;
                                                    pickup.PickerID = objPicker.PickerId;
                                                    pickup.PickStatus = GetPickupStatus(SchoolID);
                                                    DB.SaveChanges();
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objPicker.StudentID;
                                                    obj.ClassID = _student.ClassID;
                                                    obj.PickerID = objPicker.PickerId;
                                                    obj.TeacherID = TeacherID;
                                                    obj.PickTime = dtNow;
                                                    obj.PickDate = dtNow;
                                                    obj.PickStatus = GetPickupStatus(SchoolID);
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                }
                                                ResponseObject.Status = "S";
                                                ResponseObject.Message = "Student can Leave now Successfully";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                        {
                                            if (objPicker.PickerCode == PickerCode)
                                            {
                                                if (Obj.CreatedDateTime >= dtNow.AddDays(-30))
                                                {
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                        ResponseObject.Data = "";
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                        if (pickup != null)
                                                        {
                                                            pickup.PickTime = dtNow;
                                                            pickup.PickDate = dtNow;
                                                            pickup.PickerID = objPicker.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPicker.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPicker.PickerId;
                                                            obj.TeacherID = TeacherID;
                                                            obj.PickTime = dtNow;
                                                            obj.PickDate = dtNow;
                                                            obj.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                        }
                                                        List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                        if (objss.Count > 0)
                                                        {
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = TeacherID;
                                                                objp.PickTime = dtNow;
                                                                objp.PickDate = dtNow;
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                        ResponseObject.Status = "S";
                                                        ResponseObject.Message = "Student can Leave now Successfully";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Picker does not allow to Pick Child";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "InValid Picker";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)" || p.PickStatus == "Picked(Late)") && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                if (objs.Count > 0)
                                {
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                    ResponseObject.Data = "";
                                }
                                else
                                {
                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = dtNow;
                                        pickup.PickDate = dtNow;
                                        pickup.PickerID = objPicker.PickerId;
                                        pickup.PickStatus = GetPickupStatus(SchoolID);
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objPicker.StudentID;
                                        obj.ClassID = _student.ClassID;
                                        obj.PickerID = objPicker.PickerId;
                                        obj.TeacherID = TeacherID;
                                        obj.PickTime = dtNow;
                                        obj.PickDate = dtNow;
                                        obj.PickStatus = GetPickupStatus(SchoolID);
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Student can Leave now Successfully";
                                    ResponseObject.Data = "";
                                }
                            }
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "First of all Fill Up Attendence for Today";
                            ResponseObject.Data = "";
                        }
                    }
                    else
                    {
                        List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == SchoolID && p.Active == true && p.Deleted == true).ToList();
                        if (ObjHoliday.Where(x => x.DateFrom.Value <= dtNow.Date && x.DateTo.Value >= dtNow.Date).Count() > 0)
                        {
                            ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == _student.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dtNow));
                            if (ObjAttendance == null)
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "Action Cannot Be Completed Because School Closed for the Child";
                                ResponseObject.Data = "";
                            }
                            else
                            {
                                if (DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).PickerType == (int)EnumsManagement.PICKERTYPE.Individual && !DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).FirstName.Contains("("))
                                {
                                    List<ISPickup> objPicks = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "Office" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                    if (objPicks.Count > 0)
                                    {
                                        List<ISTeacherClassAssignment> ObjAssign = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Name.Contains("(Office)") && p.ISClass.SchoolID == SchoolID && p.Active == true).ToList();
                                        if (ObjAssign.Count > 0)
                                        {
                                            if (objPickers != null && objPickers.PickCodeExDate != null)
                                            {
                                                if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dtNow.ToString("dd/MM/yyyy"))
                                                {
                                                    ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                                    if (objPickers.PickerCode == PickerCode)
                                                    {
                                                        if (Objs.CreatedDateTime >= dtNow.AddDays(-30))
                                                        {
                                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                            if (objs.Count > 0)
                                                            {
                                                                ResponseObject.Status = "F";
                                                                ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                                ResponseObject.Data = "";
                                                            }
                                                            else
                                                            {
                                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                                if (pickup != null)
                                                                {
                                                                    pickup.PickTime = dtNow;
                                                                    pickup.PickDate = dtNow;
                                                                    pickup.PickerID = objPickers.PickerId;
                                                                    pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                    DB.SaveChanges();
                                                                }
                                                                else
                                                                {
                                                                    ISPickup obj = new ISPickup();
                                                                    obj.StudentID = objPickers.StudentID;
                                                                    obj.ClassID = _student.ClassID;
                                                                    obj.PickerID = objPickers.PickerId;
                                                                    obj.TeacherID = TeacherID;
                                                                    obj.PickTime = dtNow;
                                                                    obj.PickDate = dtNow;
                                                                    obj.PickStatus = GetPickupStatus(SchoolID);
                                                                    DB.ISPickups.Add(obj);
                                                                    DB.SaveChanges();
                                                                }
                                                                ResponseObject.Status = "S";
                                                                ResponseObject.Message = "Student can Leave now Successfully";
                                                                ResponseObject.Data = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Picker does not allow to Pick Child";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Please Enter Valid Picker Code";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                                ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                                if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                                {
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                        ResponseObject.Data = "";
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                        if (pickup != null)
                                                        {
                                                            pickup.PickTime = dtNow;
                                                            pickup.PickDate = dtNow;
                                                            pickup.PickerID = objPicker.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPicker.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPicker.PickerId;
                                                            obj.TeacherID = TeacherID;
                                                            obj.PickTime = dtNow;
                                                            obj.PickDate = dtNow;
                                                            obj.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                        }
                                                        ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                        if (ObSt != null)
                                                        {
                                                            ISPickup objp = new ISPickup();
                                                            objp.StudentID = ObSt.ID;
                                                            objp.ClassID = ObSt.ClassID;
                                                            objp.PickerID = objPickers.PickerId;
                                                            objp.TeacherID = TeacherID;
                                                            objp.PickTime = dtNow;
                                                            objp.PickDate = dtNow;
                                                            objp.PickStatus = "Picked";
                                                            DB.ISPickups.Add(objp);
                                                            DB.SaveChanges();
                                                        }
                                                        ResponseObject.Status = "S";
                                                        ResponseObject.Message = "Student can Leave now Successfully";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                                {
                                                    if (objPicker.PickerCode == PickerCode)
                                                    {
                                                        if (Obj.CreatedDateTime >= dtNow.AddDays(-30))
                                                        {
                                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                            if (objs.Count > 0)
                                                            {
                                                                ResponseObject.Status = "F";
                                                                ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                                ResponseObject.Data = "";
                                                            }
                                                            else
                                                            {
                                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                                if (pickup != null)
                                                                {
                                                                    pickup.PickTime = dtNow;
                                                                    pickup.PickDate = dtNow;
                                                                    pickup.PickerID = objPicker.PickerId;
                                                                    pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                    DB.SaveChanges();
                                                                }
                                                                else
                                                                {
                                                                    ISPickup obj = new ISPickup();
                                                                    obj.StudentID = objPicker.StudentID;
                                                                    obj.ClassID = _student.ClassID;
                                                                    obj.PickerID = objPicker.PickerId;
                                                                    obj.TeacherID = TeacherID;
                                                                    obj.PickTime = dtNow;
                                                                    obj.PickDate = dtNow;
                                                                    obj.PickStatus = GetPickupStatus(SchoolID);
                                                                    DB.ISPickups.Add(obj);
                                                                    DB.SaveChanges();
                                                                }
                                                                ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                                ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                                if (ObSt != null)
                                                                {
                                                                    ISPickup objp = new ISPickup();
                                                                    objp.StudentID = ObSt.ID;
                                                                    objp.ClassID = ObSt.ClassID;
                                                                    objp.PickerID = objPickers.PickerId;
                                                                    objp.TeacherID = TeacherID;
                                                                    objp.PickTime = dtNow;
                                                                    objp.PickDate = dtNow;
                                                                    objp.PickStatus = "Picked";
                                                                    DB.ISPickups.Add(objp);
                                                                    DB.SaveChanges();
                                                                }
                                                                ResponseObject.Status = "S";
                                                                ResponseObject.Message = "Student can Leave now Successfully";
                                                                ResponseObject.Data = "";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Picker does not allow to Pick Child";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Please Enter Valid Picker Code";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "InValid Picker";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "You are not Able to do Child Pickup";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        if (objPickers != null && objPickers.PickCodeExDate != null)
                                        {
                                            if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dtNow.ToString("dd/MM/yyyy"))
                                            {
                                                ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                                if (objPickers.PickerCode == PickerCode)
                                                {
                                                    if (Objs.CreatedDateTime >= dtNow.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                            ResponseObject.Data = "";
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                            if (pickup != null)
                                                            {
                                                                pickup.PickTime = dtNow;
                                                                pickup.PickDate = dtNow;
                                                                pickup.PickerID = objPickers.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPickers.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPickers.PickerId;
                                                                obj.TeacherID = TeacherID;
                                                                obj.PickTime = dtNow;
                                                                obj.PickDate = dtNow;
                                                                obj.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                            }
                                                            List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                            if (objss.Count > 0)
                                                            {
                                                                ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                                ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                                if (ObSt != null)
                                                                {
                                                                    ISPickup objp = new ISPickup();
                                                                    objp.StudentID = ObSt.ID;
                                                                    objp.ClassID = ObSt.ClassID;
                                                                    objp.PickerID = objPickers.PickerId;
                                                                    objp.TeacherID = TeacherID;
                                                                    objp.PickTime = dtNow;
                                                                    objp.PickDate = dtNow;
                                                                    objp.PickStatus = "Picked";
                                                                    DB.ISPickups.Add(objp);
                                                                    DB.SaveChanges();
                                                                }
                                                            }
                                                            ResponseObject.Status = "S";
                                                            ResponseObject.Message = "Student can Leave now Successfully";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Picker does not allow to Pick Child";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                            if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                            {
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                    ResponseObject.Data = "";
                                                }
                                                else
                                                {
                                                    List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                    if (objss.Count > 0)
                                                    {
                                                        ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                        if (ObSt != null)
                                                        {
                                                            ISPickup objp = new ISPickup();
                                                            objp.StudentID = ObSt.ID;
                                                            objp.ClassID = ObSt.ClassID;
                                                            objp.PickerID = objPickers.PickerId;
                                                            objp.TeacherID = TeacherID;
                                                            objp.PickTime = dtNow;
                                                            objp.PickDate = dtNow;
                                                            objp.PickStatus = "Picked";
                                                            DB.ISPickups.Add(objp);
                                                            DB.SaveChanges();
                                                        }
                                                    }
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                    if (pickup != null)
                                                    {
                                                        pickup.PickTime = dtNow;
                                                        pickup.PickDate = dtNow;
                                                        pickup.PickerID = objPicker.PickerId;
                                                        pickup.PickStatus = GetPickupStatus(SchoolID);
                                                        DB.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = TeacherID;
                                                        obj.PickTime = dtNow;
                                                        obj.PickDate = dtNow;
                                                        obj.PickStatus = GetPickupStatus(SchoolID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                    }
                                                    ResponseObject.Status = "S";
                                                    ResponseObject.Message = "Student can Leave now Successfully";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                            {
                                                if (objPicker.PickerCode == PickerCode)
                                                {
                                                    if (Obj.CreatedDateTime >= dtNow.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                            ResponseObject.Data = "";
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                            if (pickup != null)
                                                            {
                                                                pickup.PickTime = dtNow;
                                                                pickup.PickDate = dtNow;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPicker.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPicker.PickerId;
                                                                obj.TeacherID = TeacherID;
                                                                obj.PickTime = dtNow;
                                                                obj.PickDate = dtNow;
                                                                obj.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                            }
                                                            List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                            if (objss.Count > 0)
                                                            {
                                                                ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                                ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                                if (ObSt != null)
                                                                {
                                                                    ISPickup objp = new ISPickup();
                                                                    objp.StudentID = ObSt.ID;
                                                                    objp.ClassID = _student.ClassID;
                                                                    objp.PickerID = objPickers.PickerId;
                                                                    objp.TeacherID = TeacherID;
                                                                    objp.PickTime = dtNow;
                                                                    objp.PickDate = dtNow;
                                                                    objp.PickStatus = "Picked";
                                                                    DB.ISPickups.Add(objp);
                                                                    DB.SaveChanges();
                                                                }
                                                            }
                                                            ResponseObject.Status = "S";
                                                            ResponseObject.Message = "Student can Leave now Successfully";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Picker does not allow to Pick Child";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "InValid Picker";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)" || p.PickStatus == "Picked(Late)") && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                    if (objs.Count > 0)
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                        ResponseObject.Data = "";
                                    }
                                    else
                                    {
                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                        if (pickup != null)
                                        {
                                            pickup.PickTime = dtNow;
                                            pickup.PickDate = dtNow;
                                            pickup.PickerID = objPicker.PickerId;
                                            pickup.PickStatus = GetPickupStatus(SchoolID);
                                            DB.SaveChanges();
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objPicker.StudentID;
                                            obj.ClassID = _student.ClassID;
                                            obj.PickerID = objPicker.PickerId;
                                            obj.TeacherID = TeacherID;
                                            obj.PickTime = dtNow;
                                            obj.PickDate = dtNow;
                                            obj.PickStatus = GetPickupStatus(SchoolID);
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Student can Leave now Successfully";
                                        ResponseObject.Data = "";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).PickerType == (int)EnumsManagement.PICKERTYPE.Individual && !DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).FirstName.Contains("("))
                            {
                                List<ISPickup> objPicks = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "Office" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                if (objPicks.Count > 0)
                                {
                                    List<ISTeacherClassAssignment> ObjAssign = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.ISClass.Name.Contains("(Office)") && p.ISClass.SchoolID == SchoolID && p.Active == true).ToList();
                                    if (ObjAssign.Count > 0)
                                    {
                                        if (objPickers != null && objPickers.PickCodeExDate != null)
                                        {
                                            if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dtNow.ToString("dd/MM/yyyy"))
                                            {
                                                ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                                if (objPickers.PickerCode == PickerCode)
                                                {
                                                    if (Objs.CreatedDateTime >= dtNow.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                            ResponseObject.Data = "";
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                            if (pickup != null)
                                                            {
                                                                pickup.PickTime = dtNow;
                                                                pickup.PickDate = dtNow;
                                                                pickup.PickerID = objPickers.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPickers.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPickers.PickerId;
                                                                obj.TeacherID = TeacherID;
                                                                obj.PickTime = dtNow;
                                                                obj.PickDate = dtNow;
                                                                obj.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                            }
                                                            ResponseObject.Status = "S";
                                                            ResponseObject.Message = "Student can Leave now Successfully";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Picker does not allow to Pick Child";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                            if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                            {
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                    ResponseObject.Data = "";
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                    if (pickup != null)
                                                    {
                                                        pickup.PickTime = dtNow;
                                                        pickup.PickDate = dtNow;
                                                        pickup.PickerID = objPicker.PickerId;
                                                        pickup.PickStatus = GetPickupStatus(SchoolID);
                                                        DB.SaveChanges();
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = TeacherID;
                                                        obj.PickTime = dtNow;
                                                        obj.PickDate = dtNow;
                                                        obj.PickStatus = GetPickupStatus(SchoolID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                    }
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = TeacherID;
                                                        objp.PickTime = dtNow;
                                                        objp.PickDate = dtNow;
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                    }
                                                    ResponseObject.Status = "S";
                                                    ResponseObject.Message = "Student can Leave now Successfully";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                            {
                                                if (objPicker.PickerCode == PickerCode)
                                                {
                                                    if (Obj.CreatedDateTime >= dtNow.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            ResponseObject.Status = "F";
                                                            ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                            ResponseObject.Data = "";
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                            if (pickup != null)
                                                            {
                                                                pickup.PickTime = dtNow;
                                                                pickup.PickDate = dtNow;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPicker.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPicker.PickerId;
                                                                obj.TeacherID = TeacherID;
                                                                obj.PickTime = dtNow;
                                                                obj.PickDate = dtNow;
                                                                obj.PickStatus = GetPickupStatus(SchoolID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                            }
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = TeacherID;
                                                                objp.PickTime = dtNow;
                                                                objp.PickDate = dtNow;
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                            }
                                                            ResponseObject.Status = "S";
                                                            ResponseObject.Message = "Student can Leave now Successfully";
                                                            ResponseObject.Data = "";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Picker does not allow to Pick Child";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Please Enter Valid Picker Code";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "InValid Picker";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "You are not Able to do Child Pickup";
                                        ResponseObject.Data = "";
                                    }
                                }
                                else
                                {
                                    if (objPickers != null && objPickers.PickCodeExDate != null)
                                    {
                                        if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dtNow.ToString("dd/MM/yyyy"))
                                        {
                                            ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                            if (objPickers.PickerCode == PickerCode)
                                            {
                                                if (Objs.CreatedDateTime >= dtNow.AddDays(-30))
                                                {
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                        ResponseObject.Data = "";
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                        if (pickup != null)
                                                        {
                                                            pickup.PickTime = dtNow;
                                                            pickup.PickDate = dtNow;
                                                            pickup.PickerID = objPickers.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPickers.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPickers.PickerId;
                                                            obj.TeacherID = TeacherID;
                                                            obj.PickTime = dtNow;
                                                            obj.PickDate = dtNow;
                                                            obj.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                        }
                                                        List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                        if (objss.Count > 0)
                                                        {
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = TeacherID;
                                                                objp.PickTime = dtNow;
                                                                objp.PickDate = dtNow;
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                        ResponseObject.Status = "S";
                                                        ResponseObject.Message = "Student can Leave now Successfully";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Picker does not allow to Pick Child";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "Please Enter Valid Picker Code";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                        ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                        if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                        {
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                            if (objs.Count > 0)
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                ResponseObject.Data = "";
                                            }
                                            else
                                            {
                                                List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                if (objss.Count > 0)
                                                {
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = TeacherID;
                                                        objp.PickTime = dtNow;
                                                        objp.PickDate = dtNow;
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                    }
                                                }
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                if (pickup != null)
                                                {
                                                    pickup.PickTime = dtNow;
                                                    pickup.PickDate = dtNow;
                                                    pickup.PickerID = objPicker.PickerId;
                                                    pickup.PickStatus = GetPickupStatus(SchoolID);
                                                    DB.SaveChanges();
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objPicker.StudentID;
                                                    obj.ClassID = _student.ClassID;
                                                    obj.PickerID = objPicker.PickerId;
                                                    obj.TeacherID = TeacherID;
                                                    obj.PickTime = dtNow;
                                                    obj.PickDate = dtNow;
                                                    obj.PickStatus = GetPickupStatus(SchoolID);
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                }
                                                ResponseObject.Status = "S";
                                                ResponseObject.Message = "Student can Leave now Successfully";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                        {
                                            if (objPicker.PickerCode == PickerCode)
                                            {
                                                if (Obj.CreatedDateTime >= dtNow.AddDays(-30))
                                                {
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        ResponseObject.Status = "F";
                                                        ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                                        ResponseObject.Data = "";
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                                        if (pickup != null)
                                                        {
                                                            pickup.PickTime = dtNow;
                                                            pickup.PickDate = dtNow;
                                                            pickup.PickerID = objPicker.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPicker.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPicker.PickerId;
                                                            obj.TeacherID = TeacherID;
                                                            obj.PickTime = dtNow;
                                                            obj.PickDate = dtNow;
                                                            obj.PickStatus = GetPickupStatus(SchoolID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                        }
                                                        List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow)).ToList();
                                                        if (objss.Count > 0)
                                                        {
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = _student.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = TeacherID;
                                                                objp.PickTime = dtNow;
                                                                objp.PickDate = dtNow;
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                        ResponseObject.Status = "S";
                                                        ResponseObject.Message = "Student can Leave now Successfully";
                                                        ResponseObject.Data = "";
                                                    }
                                                }
                                                else
                                                {
                                                    ResponseObject.Status = "F";
                                                    ResponseObject.Message = "Picker does not allow to Pick Child";
                                                    ResponseObject.Data = "";
                                                }
                                            }
                                            else
                                            {
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Please Enter Valid Picker Code";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "InValid Picker";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignmentID);
                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)" || p.PickStatus == "Picked(Late)") && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow) && p.CompletePickup == true).ToList();
                                if (objs.Count > 0)
                                {
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated";
                                    ResponseObject.Data = "";
                                }
                                else
                                {
                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dtNow));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = dtNow;
                                        pickup.PickDate = dtNow;
                                        pickup.PickerID = objPicker.PickerId;
                                        pickup.PickStatus = GetPickupStatus(SchoolID);
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objPicker.StudentID;
                                        obj.ClassID = _student.ClassID;
                                        obj.PickerID = objPicker.PickerId;
                                        obj.TeacherID = TeacherID;
                                        obj.PickTime = dtNow;
                                        obj.PickDate = dtNow;
                                        obj.PickStatus = GetPickupStatus(SchoolID);
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Student can Leave now Successfully";
                                    ResponseObject.Data = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void CreateCell(Row rw, String value)
        {
            try
            {
                Cell newCell = new Cell();
                rw.Append(newCell);
                newCell.CellValue = new CellValue(value);
                newCell.DataType = new EnumValue<CellValues>(CellValues.String);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ExportToExcelAttendence()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM");
                string DateTo = OperationManagement.GetPostValues("DATETO");
                string Status = OperationManagement.GetPostValues("STATUS");
                string SortBy = OperationManagement.GetPostValues("SORTBY");
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                bool? Email = OperationManagement.GetPostValues("ISEMAIL") != "" ? OperationManagement.GetPostValues("ISEMAIL") == "True" ? true : false : false;

                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                StudentManagement objStudentManagement = new StudentManagement();
                List<MISAttendance> objList = objStudentManagement.StudentAttendenceReport(StudentID, DateFrom, DateTo, TeacherID, Status, OrderBy, SortBy);
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator100"] = r.ToString();

                string filepath = Server.MapPath("~/Upload") + "/TeacherStudentAttendenceReport" + Session["Generator100"].ToString() + ".xlsx";
                string FileNames = "Upload/TeacherStudentAttendenceReport" + Session["Generator100"].ToString() + ".xlsx";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = objList;
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherStudentAttendenceReport" + Session["Generator100"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "StudentName");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Date");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.Status);
                    CreateCell(row, item.Date.Value.ToString("dd/MM/yyyy"));
                }
                spreadsheetDocument.Close();

                if (Email == true)
                {
                    EmailManagement ObjEmailManagement = new EmailManagement();
                    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: StudentAttendenceReport<br><br>Description &nbsp;: Here we Send you a Mail for Student Attendence Report So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", objTeacher.Name);
                    string FileNamess = Server.MapPath("~/Upload/TeacherStudentAttendenceReport" + Session["Generator100"].ToString() + ".xlsx");
                    ObjEmailManagement.SendEmails(objTeacher.Email, "StudentAttendenceReport", Message, FileNamess);
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Attendence List Exported Successfully";
                    ResponseObject.Data = FileNames;
                    Session["Generator100"] = null;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Attendence List Exported";
                    ResponseObject.Data = "";
                    Session["Generator100"] = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ExportToExcelStudentPickUp()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM") != "" ? OperationManagement.GetPostValues("DATEFROM") : "";
                string DateTo = OperationManagement.GetPostValues("DATETO") != "" ? OperationManagement.GetPostValues("DATETO") : "";
                string PickerID = OperationManagement.GetPostValues("PICKERID") != "" ? Convert.ToString(OperationManagement.GetPostValues("PICKERID")) : "";
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS") : "";
                string SortBy = OperationManagement.GetPostValues("SORTBY") != "" ? OperationManagement.GetPostValues("SORTBY") : "";
                string OrderBy = OperationManagement.GetPostValues("ORDERBY") != "" ? OperationManagement.GetPostValues("ORDERBY") : "";
                int StudID = OperationManagement.GetPostValues("STUDID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDID")) : 0;
                string OP = OperationManagement.GetPostValues("OP");
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                bool? Email = OperationManagement.GetPostValues("ISEMAIL") != "" ? OperationManagement.GetPostValues("ISEMAIL") == "True" ? true : false : false;
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator101"] = r.ToString();
                PickupManagement objPickupManagement = new PickupManagement();
                List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
                if (OP == "Parent")
                {
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                        if (ObjStudent != null)
                        {
                            ISStudent stud = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == SchoolID && p.StudentName == ObjStudent.StudentName && p.Active == true);
                            objList = objPickupManagement.StudentPickUpReportsWithSchool(stud.ID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, stud.ID, SchoolID);
                        }
                    }
                    else
                    {
                        objList = objPickupManagement.StudentPickUpReportsWithSchool(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID, SchoolID);//StudentPickUpReports(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID);
                    }
                }
                if (OP == "Teacher")
                {
                    objList = objPickupManagement.StudentPickUpReports(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID);
                }
                //List<MViewStudentPickUp> objList = objPickupManagement.StudentPickUpReports(StudentID, DateFrom, DateTo, PickerID, Status, SortBy, OrderBy, StudID);
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
                string filepath = Server.MapPath("~/Upload") + "/TeacherStudentPickupReport" + Session["Generator101"].ToString() + ".xlsx";
                string FileNames = "Upload/TeacherStudentPickupReport" + Session["Generator101"].ToString() + ".xlsx";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = objList;
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherStudentPickupReport" + Session["Generator101"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Date");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickers Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.Pick_Date);
                    CreateCell(row, item.Status);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickerName);
                }
                spreadsheetDocument.Close();
                if (Email == true)
                {
                    EmailManagement ObjEmailManagement = new EmailManagement();
                    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: StudentAttendenceReport<br><br>Description &nbsp;: Here we Send you a Mail for Student Attendence Report So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", objTeacher.Name);
                    string FileNamess = Server.MapPath("~/Upload/TeacherStudentPickupReport" + Session["Generator101"].ToString() + ".xlsx");
                    ObjEmailManagement.SendEmails(objTeacher.Email, "TeacherStudentPickupReport", Message, FileNamess);
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Student PickUp Report Exported Successfully";
                    ResponseObject.Data = FileNames;
                    Session["Generator101"] = null;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Student PickUp Report Exported";
                    ResponseObject.Data = null;
                    Session["Generator101"] = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendMsgToSchoolStudent()
        {
            try
            {
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID"));
                int ReceiveGroupID = OperationManagement.GetPostValues("RECEIVERGROUPID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERGROUPID"));
                int ReceiverID = OperationManagement.GetPostValues("RECEIVERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERID"));
                int ClassIDs = OperationManagement.GetPostValues("CLASSID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("CLASSID"));
                string Subject = OperationManagement.GetPostValues("SUBJECT");
                //string FileURL = OperationManagement.GetPostValues("ATTACHNMENT");
                string FileURL = OperationManagement.GetPostValues("ATTACHNMENT") != "" ? Server.MapPath("~/Upload/" + OperationManagement.GetPostValues("ATTACHNMENT")) : "";
                string Desc = OperationManagement.GetPostValues("DESCRIPTION");
                ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjTeacher.SchoolID);
                if (ReceiveGroupID != 0)
                {
                    if (ReceiveGroupID == 1)
                    {
                        if (ReceiverID == -1)
                        {
                            SchoolManagement objSchoolManagement = new SchoolManagement();
                            List<MISSchool> obj = objSchoolManagement.SchoolList().Where(p => p.ID == ObjTeacher.SchoolID).ToList();
                            string Email = "";
                            foreach (var item in obj)
                            {
                                Email += item.AdminEmail + ",";
                            }

                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear School,<br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br/>Thanks, <br/> {2}<br>School : {3}<br>", Subject, Desc, ObjTeacher.Name, ObjSchool.Name);
                            EmailManagement objEmailManagement = new EmailManagement();
                            objEmailManagement.SendEmailToMultiple(Email, Subject, Message, FileURL);
                        }
                        else
                        {
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", ObjSchool.Name, Subject, Desc, ObjTeacher.Name, ObjSchool.Name);
                            objEmailManagement.SendEmails(ObjSchool.AdminEmail, Subject, Message, FileURL);
                        }
                    }
                    if (ReceiveGroupID == 3)
                    {
                        if (ReceiverID == -1)
                        {
                            ParentManagement objParentManagement = new ParentManagement();
                            List<MISStudent> obStudent = new List<MISStudent>();
                            if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                            {
                                obStudent = objParentManagement.ParentList(ObjSchool.ID);
                            }
                            else
                            {
                                string dts = DateTime.Now.ToString("dd/MM/yyyy");
                                obStudent = objParentManagement.ParentList(ObjSchool.ID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                            }
                            if (ClassIDs != 0 && ClassIDs != -1)
                            {
                                int ClassID = Convert.ToInt32(ClassIDs);
                                ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                                if (ObjCl.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                                {
                                    ClassManagement objClassManagement = new ClassManagement();
                                    obStudent = objClassManagement.StudentListByOfficeClass(ObjSchool.ID);
                                }
                                else
                                {
                                    obStudent = obStudent.Where(p => p.ClassID == ClassID).ToList();
                                }
                            }
                            string Email = "";
                            foreach (var item in obStudent)
                            {
                                Email += item.ParantEmail1 + ",";
                            }

                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parents,<br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br/>Thanks, <br/> {2}<br>School : {3}<br>", Subject, Desc, ObjTeacher.Name, ObjSchool.Name);
                            EmailManagement objEmailManagement = new EmailManagement();
                            objEmailManagement.SendEmailToMultiple(Email, Subject, Message, FileURL);
                        }
                        else
                        {
                            ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiverID);
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", ObjStudent.StudentName, Subject, Desc, ObjTeacher.Name, ObjSchool.Name);
                            objEmailManagement.SendEmails(ObjStudent.ParantEmail1, Subject, Message, FileURL);
                        }
                    }
                    if (TeacherID != 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Message Sent Successfully";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Message Not Sent";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Please Select ReceiverGroup";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetPickupMessage()
        {
            try
            {
                {
                    int ID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                    ISPickUpMessage ObjMesssage = DB.ISPickUpMessages.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SendID == ID && p.Viewed == false && p.Deleted == true);
                    if (ObjMesssage != null)
                    {
                        MISPickUpMessage ObjMessages = new MISPickUpMessage();
                        ObjMessages.ID = ObjMesssage.ID;
                        ObjMessages.Message = "Your Student Pickup Reason " + ObjMesssage.Message + "From : " + ObjMesssage.SenderName;
                        ObjMessages.SchoolID = ObjMesssage.SchoolID;
                        ObjMessages.ReceiverID = ObjMesssage.ReceiverID;
                        ObjMessages.ClassID = ObjMesssage.ClassID;
                        ObjMessages.SendID = ObjMesssage.SendID;
                        ObjMessages.SenderName = ObjMesssage.SenderName;
                        ObjMessages.Viewed = ObjMesssage.Viewed;
                        ObjMessages.StrDate = ObjMesssage.CreatedDateTime.Value.ToString("dd/MM/yyyy");

                        ObjMesssage.Viewed = true;
                        DB.SaveChanges();

                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Message Found Successfully";
                        ResponseObject.Data = ObjMessages;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Message to Display";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SetMarkAsAbsent()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                DateTime dt = DateTime.Now;
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
                ISTeacher ObjTeachers = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Active == true && p.Deleted == true);
                if (objStudent != null)
                {
                    List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                         select new MViewStudentPickUp
                                                         {
                                                             ID = item.ID == null ? 0 : item.ID,
                                                             StudentID = item.StudID,
                                                             StudentName = item.StudentName,
                                                             PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                             Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                             ClassID = item.ClassID,
                                                             SchoolID = item.SchoolID,
                                                         }).ToList();
                    if (objLists != null)
                    {
                        if (objLists[0].PickStatus == "Office" || objLists[0].PickStatus == "Mark as Absent" || objLists[0].PickStatus == "After-School" || objLists[0].PickStatus == "After-School-Ex" || ObjTeachers.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "Mark As Absent should not be Allowed.";
                            ResponseObject.Data = "";
                        }
                        else
                        {
                            if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                            {
                                ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (ObjAttendance == null)
                                {
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Action Cannot Be Completed Because School Closed for the Child";
                                    ResponseObject.Data = "";
                                }
                                else
                                {
                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                    if (objs.Count > 0)
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Student already Picked";
                                        ResponseObject.Data = "";
                                    }
                                    else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            ObjPickUp.PickStatus = "Mark as Absent";
                                            DB.SaveChanges();

                                            ResponseObject.Status = "S";
                                            ResponseObject.Message = "Student Marked As Absent Successfully";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = TeacherID;
                                        obj.PickTime = dt;
                                        obj.PickDate = dt;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();

                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Student Marked As Absent Successfully";
                                        ResponseObject.Data = "";
                                    }
                                }
                            }
                            else
                            {
                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                if (objs.Count > 0)
                                {
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Student already Picked";
                                    ResponseObject.Data = "";
                                }
                                else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList().Count > 0)
                                {
                                    ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (ObjPickUp != null)
                                    {
                                        ObjPickUp.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();

                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Student Marked As Absent Successfully";
                                        ResponseObject.Data = "";
                                    }
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = objStudent.ID;
                                    obj.ClassID = objStudent.ClassID;
                                    obj.TeacherID = TeacherID;
                                    obj.PickTime = dt;
                                    obj.PickDate = dt;
                                    obj.PickStatus = "Mark as Absent";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();

                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Student Marked As Absent Successfully";
                                    ResponseObject.Data = "";
                                }
                            }
                        }
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Student not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SetSendToAfterSchool()
        {
            try
            {
                {
                    int ID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                    int TeacherID = OperationManagement.GetPostValues("TEACHERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                    int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                    DateTime dt = DateTime.Now;
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
                    ISTeacher ObjTeachers = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Active == true && p.Deleted == true);
                    if (objStudent != null)
                    {
                        List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
                        List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                             select new MViewStudentPickUp
                                                             {
                                                                 ID = item.ID == null ? 0 : item.ID,
                                                                 StudentID = item.StudID,
                                                                 StudentName = item.StudentName,
                                                                 PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                                 Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                                 ClassID = item.ClassID,
                                                                 SchoolID = item.SchoolID,
                                                             }).ToList();
                        if (objLists != null)
                        {
                            if (objLists[0].PickStatus == "Office" || objLists[0].PickStatus == "Mark as Absent" || objLists[0].PickStatus == "After-School" || objLists[0].PickStatus == "After-School-Ex" || ObjTeachers.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "Send to AfterSchool should not be Allowed.";
                                ResponseObject.Data = "";
                            }
                            else
                            {
                                if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                                {
                                    ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                    if (ObjAttendance == null)
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Action Cannot Be Completed Because School Closed for the Child";
                                        ResponseObject.Data = "";
                                    }
                                    else
                                    {
                                        ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == SchoolID);
                                        if (ObjClass != null)
                                        {
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                            if (objs.Count > 0)
                                            {
                                                Session["Picked"] = true;
                                                ResponseObject.Status = "F";
                                                ResponseObject.Message = "Student already Picked";
                                                ResponseObject.Data = "";
                                            }
                                            else
                                            {
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                if (pickup != null)
                                                {
                                                    pickup.PickTime = dt;
                                                    pickup.PickDate = dt;
                                                    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    {
                                                        if (ObjClass.AfterSchoolType == "Internal")
                                                        {
                                                            pickup.PickStatus = "After-School";
                                                        }
                                                        else
                                                        {
                                                            pickup.PickStatus = "After-School-Ex";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    DB.SaveChanges();
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objStudent.ID;
                                                    obj.ClassID = objStudent.ClassID;
                                                    obj.TeacherID = TeacherID;
                                                    obj.PickTime = dt;
                                                    obj.PickDate = dt;
                                                    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    {
                                                        if (ObjClass.AfterSchoolType == "Internal")
                                                        {
                                                            obj.PickStatus = "After-School";
                                                        }
                                                        else
                                                        {
                                                            obj.PickStatus = "After-School-Ex";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                }

                                                if (ObjClass.AfterSchoolType == "External")
                                                {
                                                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.Name == ObjClass.ExternalOrganisation);
                                                    if (ObjSchool != null)
                                                    {
                                                        //ISClass ObSClass = DB.ISClasses.SingleOrDefault(p => p.SchoolID == ObjSchool.ID && p.Name.Contains("Outside"));
                                                        //if (ObSClass != null)
                                                        //{
                                                        //    ISStudent objST = new ISStudent();
                                                        //    objST.StudentName = objStudent.StudentName;
                                                        //    objST.ClassID = ObSClass.ID;
                                                        //    objST.StudentNo = objStudent.StudentNo;
                                                        //    objST.SchoolID = ObjSchool.ID;
                                                        //    objST.Photo = objStudent.Photo;
                                                        //    objST.ParantName1 = objStudent.ParantName1;
                                                        //    objST.ParantEmail1 = objStudent.ParantEmail1;
                                                        //    objST.ParantPhone1 = objStudent.ParantPhone1;
                                                        //    objST.ParantRelation1 = objStudent.ParantRelation1;
                                                        //    objST.ParantName2 = objStudent.ParantName2;
                                                        //    objST.ParantEmail2 = objStudent.ParantEmail2;
                                                        //    objST.ParantPhone2 = objStudent.ParantPhone2;
                                                        //    objST.ParantRelation2 = objStudent.ParantRelation2;
                                                        //    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                                                        //    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                                                        //    string Message = "";
                                                        //    if (objStudent.ParantName1 != "")
                                                        //    {
                                                        //        objST.ParantPassword1 = EncryptionHelper.Encrypt(ParantPassword1);
                                                        //    }
                                                        //    if (objStudent.ParantName2 != "" && objStudent.ParantName2 != null)
                                                        //    {
                                                        //        objST.ParantPassword2 = EncryptionHelper.Encrypt(ParantPassword2);
                                                        //    }
                                                        //    objST.CreatedBy = ObjSchool.ID;
                                                        //    objST.CreatedDateTime = dt;
                                                        //    objST.Active = true;
                                                        //    objST.Deleted = true;
                                                        //    objST.Out = 0;
                                                        //    objST.Outbit = false;
                                                        //    objST.StartDate = dt;
                                                        //    objST.EndDate = dt;
                                                        //    DB.ISStudents.Add(objST);
                                                        //    DB.SaveChanges();

                                                        //    ISPicker objPicker = new ISPicker();
                                                        //    objPicker.PickerType = 1;
                                                        //    objPicker.SchoolID = ObjSchool.ID;
                                                        //    objPicker.ParentID = objST.ID;
                                                        //    objPicker.StudentID = objST.ID;
                                                        //    objPicker.FirstName = objStudent.ParantName1 + "(" + objStudent.ParantRelation1 + ")";
                                                        //    objPicker.Photo = "Upload/user.jpg";
                                                        //    objPicker.Email = objStudent.ParantEmail1;
                                                        //    objPicker.Phone = objStudent.ParantPhone1;
                                                        //    objPicker.OneOffPickerFlag = false;
                                                        //    objPicker.ActiveStatus = "Active";
                                                        //    objPicker.Active = true;
                                                        //    objPicker.Deleted = true;
                                                        //    objPicker.CreatedBy = ObjSchool.ID;
                                                        //    objPicker.CreatedDateTime = dt;
                                                        //    DB.ISPickers.Add(objPicker);
                                                        //    DB.SaveChanges();
                                                        //    if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                                                        //    {
                                                        //        ISPickerAssignment objAssign = new ISPickerAssignment();
                                                        //        objAssign.PickerId = objPicker.ID;
                                                        //        objAssign.StudentID = objST.ID;
                                                        //        objAssign.RemoveChildStatus = 0;
                                                        //        DB.ISPickerAssignments.Add(objAssign);
                                                        //        DB.SaveChanges();
                                                        //    }
                                                        //    if (objST.ParantName2 != null && objST.ParantEmail2 != null)
                                                        //    {
                                                        //        ISPicker objPickers = new ISPicker();
                                                        //        objPickers.PickerType = 1;
                                                        //        objPickers.SchoolID = ObjSchool.ID;
                                                        //        objPickers.ParentID = objST.ID;
                                                        //        objPickers.StudentID = objST.ID;
                                                        //        objPickers.FirstName = objStudent.ParantName2 + "(" + objStudent.ParantRelation2 + ")";
                                                        //        objPickers.Photo = "Upload/user.jpg";
                                                        //        objPickers.Email = objStudent.ParantEmail2;
                                                        //        objPickers.Phone = objStudent.ParantPhone2;
                                                        //        objPickers.OneOffPickerFlag = false;
                                                        //        objPickers.ActiveStatus = "Active";
                                                        //        objPickers.Active = true;
                                                        //        objPickers.Deleted = true;
                                                        //        objPickers.CreatedBy = ObjSchool.ID;
                                                        //        objPickers.CreatedDateTime = dt;
                                                        //        DB.ISPickers.Add(objPickers);
                                                        //        DB.SaveChanges();
                                                        //        if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                                                        //        {
                                                        //            ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                        //            objAssigns.PickerId = objPickers.ID;
                                                        //            objAssigns.StudentID = objST.ID;
                                                        //            objAssigns.RemoveChildStatus = 0;
                                                        //            DB.ISPickerAssignments.Add(objAssigns);
                                                        //            DB.SaveChanges();
                                                        //        }
                                                        //    }
                                                        //    ISPickup objPick = new ISPickup();
                                                        //    objPick.StudentID = objST.ID;
                                                        //    objPick.ClassID = objST.ClassID;
                                                        //    objPick.TeacherID = TeacherID;
                                                        //    objPick.PickTime = dt;
                                                        //    objPick.PickDate = dt;
                                                        //    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                        //    {
                                                        //        objPick.PickStatus = "After-School-Ex";
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        objPick.PickStatus = "After-School-Ex";
                                                        //    }
                                                        //    DB.ISPickups.Add(objPick);
                                                        //    DB.SaveChanges();
                                                        //}
                                                    }
                                                }
                                                ResponseObject.Status = "S";
                                                ResponseObject.Message = "Student Sent To AfterSchool Successfully";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "No Class Found";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                }
                                else
                                {
                                    ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == SchoolID);
                                    if (ObjClass != null)
                                    {
                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                        if (objs.Count > 0)
                                        {
                                            Session["Picked"] = true;
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "Student already Picked";
                                            ResponseObject.Data = "";
                                        }
                                        else
                                        {
                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (pickup != null)
                                            {
                                                pickup.PickTime = dt;
                                                pickup.PickDate = dt;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                DB.SaveChanges();
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = objStudent.ID;
                                                obj.ClassID = objStudent.ClassID;
                                                obj.TeacherID = TeacherID;
                                                obj.PickTime = dt;
                                                obj.PickDate = dt;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                            }

                                            if (ObjClass.AfterSchoolType == "External")
                                            {
                                                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.Name == ObjClass.ExternalOrganisation);
                                                if (ObjSchool != null)
                                                {
                                                    //ISClass ObSClass = DB.ISClasses.SingleOrDefault(p => p.SchoolID == ObjSchool.ID && p.Name.Contains("Outside"));
                                                    //if (ObSClass != null)
                                                    //{
                                                    //    ISStudent objST = new ISStudent();
                                                    //    objST.StudentName = objStudent.StudentName;
                                                    //    objST.ClassID = ObSClass.ID;
                                                    //    objST.StudentNo = objStudent.StudentNo;
                                                    //    objST.SchoolID = ObjSchool.ID;
                                                    //    objST.Photo = objStudent.Photo;
                                                    //    objST.ParantName1 = objStudent.ParantName1;
                                                    //    objST.ParantEmail1 = objStudent.ParantEmail1;
                                                    //    objST.ParantPhone1 = objStudent.ParantPhone1;
                                                    //    objST.ParantRelation1 = objStudent.ParantRelation1;
                                                    //    objST.ParantName2 = objStudent.ParantName2;
                                                    //    objST.ParantEmail2 = objStudent.ParantEmail2;
                                                    //    objST.ParantPhone2 = objStudent.ParantPhone2;
                                                    //    objST.ParantRelation2 = objStudent.ParantRelation2;
                                                    //    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                                                    //    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                                                    //    string Message = "";
                                                    //    if (objStudent.ParantName1 != "")
                                                    //    {
                                                    //        objST.ParantPassword1 = EncryptionHelper.Encrypt(ParantPassword1);
                                                    //    }
                                                    //    if (objStudent.ParantName2 != "" && objStudent.ParantName2 != null)
                                                    //    {
                                                    //        objST.ParantPassword2 = EncryptionHelper.Encrypt(ParantPassword2);
                                                    //    }
                                                    //    objST.CreatedBy = ObjSchool.ID;
                                                    //    objST.CreatedDateTime = dt;
                                                    //    objST.Active = true;
                                                    //    objST.Deleted = true;
                                                    //    objST.Out = 0;
                                                    //    objST.Outbit = false;
                                                    //    objST.StartDate = dt;
                                                    //    objST.EndDate = dt;
                                                    //    DB.ISStudents.Add(objST);
                                                    //    DB.SaveChanges();

                                                    //    ISPicker objPicker = new ISPicker();
                                                    //    objPicker.PickerType = 1;
                                                    //    objPicker.SchoolID = ObjSchool.ID;
                                                    //    objPicker.ParentID = objST.ID;
                                                    //    objPicker.StudentID = objST.ID;
                                                    //    objPicker.FirstName = objStudent.ParantName1 + "(" + objStudent.ParantRelation1 + ")";
                                                    //    objPicker.Photo = "Upload/user.jpg";
                                                    //    objPicker.Email = objStudent.ParantEmail1;
                                                    //    objPicker.Phone = objStudent.ParantPhone1;
                                                    //    objPicker.OneOffPickerFlag = false;
                                                    //    objPicker.ActiveStatus = "Active";
                                                    //    objPicker.Active = true;
                                                    //    objPicker.Deleted = true;
                                                    //    objPicker.CreatedBy = ObjSchool.ID;
                                                    //    objPicker.CreatedDateTime = dt;
                                                    //    DB.ISPickers.Add(objPicker);
                                                    //    DB.SaveChanges();
                                                    //    if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                                                    //    {
                                                    //        ISPickerAssignment objAssign = new ISPickerAssignment();
                                                    //        objAssign.PickerId = objPicker.ID;
                                                    //        objAssign.StudentID = objST.ID;
                                                    //        objAssign.RemoveChildStatus = 0;
                                                    //        DB.ISPickerAssignments.Add(objAssign);
                                                    //        DB.SaveChanges();
                                                    //    }
                                                    //    if (objST.ParantName2 != null && objST.ParantEmail2 != null)
                                                    //    {
                                                    //        ISPicker objPickers = new ISPicker();
                                                    //        objPickers.PickerType = 1;
                                                    //        objPickers.SchoolID = ObjSchool.ID;
                                                    //        objPickers.ParentID = objST.ID;
                                                    //        objPickers.StudentID = objST.ID;
                                                    //        objPickers.FirstName = objStudent.ParantName2 + "(" + objStudent.ParantRelation2 + ")";
                                                    //        objPickers.Photo = "Upload/user.jpg";
                                                    //        objPickers.Email = objStudent.ParantEmail2;
                                                    //        objPickers.Phone = objStudent.ParantPhone2;
                                                    //        objPickers.OneOffPickerFlag = false;
                                                    //        objPickers.ActiveStatus = "Active";
                                                    //        objPickers.Active = true;
                                                    //        objPickers.Deleted = true;
                                                    //        objPickers.CreatedBy = ObjSchool.ID;
                                                    //        objPickers.CreatedDateTime = dt;
                                                    //        DB.ISPickers.Add(objPickers);
                                                    //        DB.SaveChanges();
                                                    //        if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                                                    //        {
                                                    //            ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                    //            objAssigns.PickerId = objPickers.ID;
                                                    //            objAssigns.StudentID = objST.ID;
                                                    //            objAssigns.RemoveChildStatus = 0;
                                                    //            DB.ISPickerAssignments.Add(objAssigns);
                                                    //            DB.SaveChanges();
                                                    //        }
                                                    //    }
                                                    //    ISPickup objPick = new ISPickup();
                                                    //    objPick.StudentID = objST.ID;
                                                    //    objPick.ClassID = objST.ClassID;
                                                    //    objPick.TeacherID = TeacherID;
                                                    //    objPick.PickTime = dt;
                                                    //    objPick.PickDate = dt;
                                                    //    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    //    {
                                                    //        objPick.PickStatus = "After-School-Ex";
                                                    //    }
                                                    //    else
                                                    //    {
                                                    //        objPick.PickStatus = "After-School-Ex";
                                                    //    }
                                                    //    DB.ISPickups.Add(objPick);
                                                    //    DB.SaveChanges();
                                                    //}
                                                }
                                            }
                                            ResponseObject.Status = "S";
                                            ResponseObject.Message = "Student Sent To AfterSchool Successfully";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "No Class Found";
                                        ResponseObject.Data = "";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Student Found";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SetSendToOffice()
        {
            try
            {
                {
                    int ID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                    int TeacherID = OperationManagement.GetPostValues("TEACHERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                    DateTime dt = DateTime.Now;
                    ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
                    ISTeacher ObjTeachers = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Active == true && p.Deleted == true);
                    if (objStudent != null)
                    {
                        List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == objStudent.SchoolID && p.Active == true && p.Deleted == true).ToList();
                        List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                             select new MViewStudentPickUp
                                                             {
                                                                 ID = item.ID == null ? 0 : item.ID,
                                                                 StudentID = item.StudID,
                                                                 StudentName = item.StudentName,
                                                                 PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                                 Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                                 ClassID = item.ClassID,
                                                                 SchoolID = item.SchoolID,
                                                             }).ToList();
                        if (objLists != null)
                        {
                            if (objLists[0].PickStatus == "Office" || objLists[0].PickStatus == "Mark as Absent" || objLists[0].PickStatus == "After-School" || objLists[0].PickStatus == "After-School-Ex" || ObjTeachers.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "Send to Office should not be Allowed.";
                                ResponseObject.Data = "";
                            }
                            else
                            {
                                if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                                {
                                    ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                    if (ObjAttendance == null)
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Action Cannot Be Completed Because School Closed for the Child";
                                        ResponseObject.Data = "";
                                    }
                                    else
                                    {
                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                        if (objs.Count > 0)
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "Student Already Picked";
                                            ResponseObject.Data = "";
                                        }
                                        else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList().Count > 0)
                                        {
                                            ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (ObjPickUp != null)
                                            {
                                                ObjPickUp.PickStatus = "Office";
                                                DB.SaveChanges();
                                                ResponseObject.Status = "S";
                                                ResponseObject.Message = "Student Sent To Office Successfully";
                                                ResponseObject.Data = "";
                                            }
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = TeacherID;
                                            obj.PickTime = dt;
                                            obj.PickDate = dt;
                                            obj.PickStatus = "Office";
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();

                                            ResponseObject.Status = "S";
                                            ResponseObject.Message = "Student Sent To Office Successfully";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                }
                                else
                                {
                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                    if (objs.Count > 0)
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Student Already Picked";
                                        ResponseObject.Data = "";
                                    }
                                    else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && p.PickStatus != "Picked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (ObjPickUp != null)
                                        {
                                            ObjPickUp.PickStatus = "Office";
                                            DB.SaveChanges();
                                            ResponseObject.Status = "S";
                                            ResponseObject.Message = "Student Sent To Office Successfully";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = TeacherID;
                                        obj.PickTime = dt;
                                        obj.PickDate = dt;
                                        obj.PickStatus = "Office";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();

                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Student Sent To Office Successfully";
                                        ResponseObject.Data = "";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Student not Found";
                        ResponseObject.Data = "";
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void CompleteAttendenceRun()
        {
            try
            {
                {
                    int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                    int ClassID = OperationManagement.GetPostValues("CLASSID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                    int TeacherID = OperationManagement.GetPostValues("TEACHERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                    bool Chk = OperationManagement.GetPostValues("CHKVALUE") == "True" ? true : false;
                    Session["Attendencelist"] = null;
                    Session["ObjList"] = null;
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    int IDS = TeacherID;
                    int Classs = ClassID;
                    DateTime dts = DateTime.Now;
                    List<MISStudent> objStudent = new List<MISStudent>();
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        objStudent = (from item in DB.getAttandanceData(dts).Where(p => p.ClassID == Classs && p.SchoolID == SchoolID && p.StudentDeleted == true).ToList()
                                      select new MISStudent
                                      {
                                          ID = item.StudentsID,
                                          StudentName = item.StudentName,
                                          Photo = item.Photo,
                                          CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                          ClassID = item.ClassID,
                                          AttStatus = item.Status != null ? item.Status : "",
                                          AttDate = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : ""
                                      }).ToList();
                    }
                    else
                    {

                        string dtss = DateTime.Now.ToString("dd/MM/yyyy");
                        objStudent = (from item in DB.getAttandanceData(dts).Where(p => p.ClassID == Classs && p.SchoolID == SchoolID && (p.StartDate == null || p.StartDate.Value.ToString("dd/MM/yyyy") == dtss) && p.StudentDeleted == true).ToList()
                                      select new MISStudent
                                      {
                                          ID = item.StudentsID,
                                          StudentName = item.StudentName,
                                          Photo = item.Photo,
                                          CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                          ClassID = item.ClassID,
                                          AttStatus = item.Status != null ? item.Status : "",
                                          AttDate = item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : ""
                                      }).ToList();
                    }
                    Session["Attendencelist"] = objStudent.ToList();
                    if (ObjSchool != null)
                    {
                        if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                        {
                            if (Chk == true)
                            {
                                int ID = TeacherID;
                                int Class = ClassID;
                                DateTime dt = DateTime.Now;

                                ISCompleteAttendanceRun completeattendance = DB.ISCompleteAttendanceRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (completeattendance == null)
                                {
                                    ISCompleteAttendanceRun objcompleteattendance = new ISCompleteAttendanceRun();
                                    objcompleteattendance.ClassID = Class;
                                    objcompleteattendance.TeacherID = ID;
                                    objcompleteattendance.Date = dt;
                                    objcompleteattendance.Active = true;
                                    objcompleteattendance.Deleted = false;
                                    objcompleteattendance.CreatedBy = ID;
                                    objcompleteattendance.CreatedDateTime = DateTime.Now;
                                    DB.ISCompleteAttendanceRuns.Add(objcompleteattendance);
                                    DB.SaveChanges();

                                    List<MISStudent> obj = new List<MISStudent>();
                                    ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                                    if (_class != null)
                                    {
                                        List<MISStudent> objList = new List<MISStudent>();
                                        //if (_class.Name.Contains("Outside Class"))
                                        //{
                                        //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                                        //           select new MISStudent
                                        //           {
                                        //               ID = item.ID,
                                        //               StudentName = item.StudentName,
                                        //               Photo = item.Photo,
                                        //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                        //               ClassID = item.ClassID
                                        //           }).ToList();
                                        //}
                                        //else
                                        {
                                            obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                   select new MISStudent
                                                   {
                                                       ID = item.ID,
                                                       StudentName = item.StudentName,
                                                       Photo = item.Photo,
                                                       CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                       ClassID = item.ClassID
                                                   }).ToList();
                                        }
                                    }

                                    Session["ObjList"] = obj.ToList();
                                    if (Chk == true)
                                    {
                                        foreach (var item in obj.ToList())
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                var itemToRemove = obj.Single(r => r.ID == item.ID);
                                                obj.Remove(itemToRemove);
                                            }
                                        }
                                    }
                                    if (obj.Count == 0)
                                    {
                                        var lists = (List<MISStudent>)Session["Attendencelist"];
                                        foreach (var item in lists)
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                objs.AttendenceComplete = true;
                                                DB.SaveChanges();
                                            }
                                        }
                                        Chk = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                    else
                                    {
                                        Chk = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Not All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                }
                                else
                                {
                                    completeattendance.ClassID = Class;
                                    completeattendance.TeacherID = ID;
                                    completeattendance.Date = dt;
                                    completeattendance.Active = true;
                                    completeattendance.Deleted = false;
                                    completeattendance.ModifyBy = ID;
                                    completeattendance.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();

                                    List<MISStudent> obj = new List<MISStudent>();
                                    ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                                    if (_class != null)
                                    {
                                        List<MISStudent> objList = new List<MISStudent>();
                                        //if (_class.Name.Contains("Outside Class"))
                                        //{
                                        //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                                        //           select new MISStudent
                                        //           {
                                        //               ID = item.ID,
                                        //               StudentName = item.StudentName,
                                        //               Photo = item.Photo,
                                        //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                        //               ClassID = item.ClassID
                                        //           }).ToList();
                                        //}
                                        //else
                                        {
                                            obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                   select new MISStudent
                                                   {
                                                       ID = item.ID,
                                                       StudentName = item.StudentName,
                                                       Photo = item.Photo,
                                                       CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                       ClassID = item.ClassID
                                                   }).ToList();
                                        }
                                    }

                                    Session["ObjList"] = obj.ToList();
                                    if (Chk == true)
                                    {
                                        foreach (var item in obj.ToList())
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                var itemToRemove = obj.Single(r => r.ID == item.ID);
                                                obj.Remove(itemToRemove);
                                            }
                                        }
                                    }
                                    if (obj.Count == 0)
                                    {
                                        var lists = (List<MISStudent>)Session["Attendencelist"];
                                        foreach (var item in lists)
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                objs.AttendenceComplete = true;
                                                DB.SaveChanges();
                                            }
                                        }
                                        Chk = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                    else
                                    {
                                        Chk = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Not All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                }
                            }
                            else
                            {
                                Chk = true;
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "Complete Attendance run already marked";
                                ResponseObject.Data = Chk;
                            }
                        }
                        else
                        {
                            if (Chk == true)
                            {
                                int ID = TeacherID;
                                int Class = ClassID;
                                DateTime dt = DateTime.Now;

                                ISCompleteAttendanceRun completeattendance = DB.ISCompleteAttendanceRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (completeattendance == null)
                                {
                                    ISCompleteAttendanceRun objcompleteattendance = new ISCompleteAttendanceRun();
                                    objcompleteattendance.ClassID = Class;
                                    objcompleteattendance.TeacherID = ID;
                                    objcompleteattendance.Date = dt;
                                    objcompleteattendance.Active = true;
                                    objcompleteattendance.Deleted = false;
                                    objcompleteattendance.CreatedBy = ID;
                                    objcompleteattendance.CreatedDateTime = DateTime.Now;
                                    DB.ISCompleteAttendanceRuns.Add(objcompleteattendance);
                                    DB.SaveChanges();

                                    List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                            select new MISStudent
                                                            {
                                                                ID = item.ID,
                                                                StudentName = item.StudentName,
                                                                Photo = item.Photo,
                                                                CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                                ClassID = item.ClassID
                                                            }).ToList();
                                    Session["ObjList"] = obj.ToList();
                                    if (Chk == true)
                                    {
                                        foreach (var item in obj.ToList())
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                var itemToRemove = obj.Single(r => r.ID == item.ID);
                                                obj.Remove(itemToRemove);
                                            }
                                        }
                                    }
                                    if (obj.Count == 0)
                                    {
                                        var lists = (List<MISStudent>)Session["Attendencelist"];
                                        foreach (var item in lists)
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                objs.AttendenceComplete = true;
                                                DB.SaveChanges();
                                            }
                                        }
                                        Chk = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                    else
                                    {
                                        Chk = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Not All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                }
                                else
                                {
                                    completeattendance.ClassID = Class;
                                    completeattendance.TeacherID = ID;
                                    completeattendance.Date = dt;
                                    completeattendance.Active = true;
                                    completeattendance.Deleted = false;
                                    completeattendance.ModifyBy = ID;
                                    completeattendance.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();

                                    List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                            select new MISStudent
                                                            {
                                                                ID = item.ID,
                                                                StudentName = item.StudentName,
                                                                Photo = item.Photo,
                                                                CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                                ClassID = item.ClassID
                                                            }).ToList();
                                    Session["ObjList"] = obj.ToList();
                                    if (Chk == true)
                                    {
                                        foreach (var item in obj.ToList())
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                var itemToRemove = obj.Single(r => r.ID == item.ID);
                                                obj.Remove(itemToRemove);
                                            }
                                        }
                                    }
                                    if (obj.Count == 0)
                                    {
                                        var lists = (List<MISStudent>)Session["Attendencelist"];
                                        foreach (var item in lists)
                                        {
                                            ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                objs.AttendenceComplete = true;
                                                DB.SaveChanges();
                                            }
                                        }
                                        Chk = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                    else
                                    {
                                        Chk = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Not All Students have been marked";
                                        ResponseObject.Data = Chk;
                                    }
                                }
                            }
                            else
                            {
                                Chk = true;
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "Complete Attendance run already marked.";
                                ResponseObject.Data = Chk;
                            }
                        }
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Data to Display";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void CompletePickUpRun()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                bool Chk = OperationManagement.GetPostValues("CHKVALUE") == "True" ? true : false;
                Session["ObjList"] = null;
                DateTime dts = DateTime.Now;
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                int ClasID = Convert.ToInt32(ClassID);
                PickupManagement objPickupManagement = new PickupManagement();
                List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objPickupManagement.PickupList(TeacherID, ClasID, dts.ToString("dd/MM/yyyy")).OrderByDescending(p => p.PickStatus).ToList();
                }
                else
                {
                    objList = objPickupManagement.PickupsList(ClasID, dts.ToString("dd/MM/yyyy")).OrderByDescending(p => p.PickStatus).ToList();
                }

                if (ObjSchool != null)
                {
                    if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        if (Chk == true)
                        {
                            int ID = TeacherID;
                            int Class = ClassID;
                            DateTime dt = DateTime.Now;

                            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                            if (completepickup == null)
                            {

                                ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                                if (_class != null)
                                {
                                    List<MISStudent> obj = new List<MISStudent>();
                                    //if (_class.Name.Contains("Outside Class"))
                                    //{
                                    //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList()
                                    //           select new MISStudent
                                    //           {
                                    //               ID = item.ID,
                                    //               StudentName = item.StudentName,
                                    //               Photo = item.Photo,
                                    //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                    //               ClassID = item.ClassID
                                    //           }).ToList();
                                    //}
                                    //else
                                    {
                                        obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                               select new MISStudent
                                               {
                                                   ID = item.ID,
                                                   StudentName = item.StudentName,
                                                   Photo = item.Photo,
                                                   CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                   ClassID = item.ClassID
                                               }).ToList();
                                    }

                                    var sids = objList.Select(p => p.StudentID).ToList();
                                    Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                                    if (Chk == true)
                                    {
                                        foreach (var item in obj.ToList())
                                        {
                                            ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                var itemToRemove = obj.Single(r => r.ID == item.ID);
                                                obj.Remove(itemToRemove);
                                            }
                                        }
                                    }
                                    if (obj.Count == 0)
                                    {
                                        var lists = objList;
                                        foreach (var item in lists)
                                        {
                                            ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                objs.CompletePickup = true;
                                                DB.SaveChanges();
                                            }
                                        }

                                        ISCompletePickupRun objcompletepickup = new ISCompletePickupRun();
                                        objcompletepickup.ClassID = Class;
                                        objcompletepickup.TeacherID = ID;
                                        objcompletepickup.Date = dt;
                                        objcompletepickup.Active = true;
                                        objcompletepickup.Deleted = false;
                                        objcompletepickup.CreatedBy = ID;
                                        objcompletepickup.CreatedDateTime = DateTime.Now;
                                        DB.ISCompletePickupRuns.Add(objcompletepickup);
                                        DB.SaveChanges();

                                        Chk = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "All Students have been Picked";
                                        ResponseObject.Data = Chk;
                                    }
                                    else
                                    {
                                        Chk = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Not All Students have been Picked";
                                        ResponseObject.Data = Chk;
                                    }
                                }
                            }
                            else
                            {

                                ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                                if (_class != null)
                                {
                                    List<MISStudent> obj = new List<MISStudent>();
                                    //if (_class.Name.Contains("Outside Class"))
                                    //{
                                    //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList()
                                    //           select new MISStudent
                                    //           {
                                    //               ID = item.ID,
                                    //               StudentName = item.StudentName,
                                    //               Photo = item.Photo,
                                    //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                    //               ClassID = item.ClassID
                                    //           }).ToList();
                                    //}
                                    //else
                                    {
                                        obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                               select new MISStudent
                                               {
                                                   ID = item.ID,
                                                   StudentName = item.StudentName,
                                                   Photo = item.Photo,
                                                   CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                   ClassID = item.ClassID
                                               }).ToList();
                                    }

                                    var sids = objList.Select(p => p.StudentID).ToList();
                                    Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                                    if (Chk == true)
                                    {
                                        foreach (var item in obj.ToList())
                                        {
                                            ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                var itemToRemove = obj.Single(r => r.ID == item.ID);
                                                obj.Remove(itemToRemove);
                                            }
                                        }
                                    }
                                    if (obj.Count == 0)
                                    {
                                        var lists = objList;
                                        foreach (var item in lists)
                                        {
                                            ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (objs != null)
                                            {
                                                objs.CompletePickup = true;
                                                DB.SaveChanges();
                                            }
                                        }
                                        completepickup.ClassID = Class;
                                        completepickup.TeacherID = ID;
                                        completepickup.Date = dt;
                                        completepickup.Active = true;
                                        completepickup.Deleted = false;
                                        completepickup.ModifyBy = ID;
                                        completepickup.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();

                                        Chk = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "All Students have been Picked";
                                        ResponseObject.Data = Chk;
                                    }
                                    else
                                    {
                                        Chk = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Not All Students have been Picked";
                                        ResponseObject.Data = Chk;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Chk = true;
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Already Checked";
                            ResponseObject.Data = Chk;
                        }
                    }
                    else
                    {
                        if (Chk == true)
                        {
                            int ID = TeacherID;
                            int Class = ClassID;
                            DateTime dt = DateTime.Now;

                            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                            if (completepickup == null)
                            {

                                List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                        select new MISStudent
                                                        {
                                                            ID = item.ID,
                                                            StudentName = item.StudentName,
                                                            Photo = item.Photo,
                                                            CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                            ClassID = item.ClassID
                                                        }).ToList();
                                var sids = objList.Select(p => p.StudentID).ToList();
                                Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                                if (Chk == true)
                                {
                                    foreach (var item in obj.ToList())
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            var itemToRemove = obj.Single(r => r.ID == item.ID);
                                            obj.Remove(itemToRemove);
                                        }
                                    }
                                }
                                if (obj.Count == 0)
                                {
                                    var lists = objList;
                                    foreach (var item in lists)
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            objs.CompletePickup = true;
                                            DB.SaveChanges();
                                        }
                                    }

                                    ISCompletePickupRun objcompletepickup = new ISCompletePickupRun();
                                    objcompletepickup.ClassID = Class;
                                    objcompletepickup.TeacherID = ID;
                                    objcompletepickup.Date = dt;
                                    objcompletepickup.Active = true;
                                    objcompletepickup.Deleted = false;
                                    objcompletepickup.CreatedBy = ID;
                                    objcompletepickup.CreatedDateTime = DateTime.Now;
                                    DB.ISCompletePickupRuns.Add(objcompletepickup);
                                    DB.SaveChanges();

                                    Chk = true;
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "All Students have been Picked";
                                    ResponseObject.Data = Chk;
                                }
                                else
                                {
                                    Chk = false;
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Not All Students have been Picked";
                                    ResponseObject.Data = Chk;
                                }
                            }
                            else
                            {

                                List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                        select new MISStudent
                                                        {
                                                            ID = item.ID,
                                                            StudentName = item.StudentName,
                                                            Photo = item.Photo,
                                                            CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                            ClassID = item.ClassID
                                                        }).ToList();
                                var sids = objList.Select(p => p.StudentID).ToList();
                                Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                                if (Chk == true)
                                {
                                    foreach (var item in obj.ToList())
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            var itemToRemove = obj.Single(r => r.ID == item.ID);
                                            obj.Remove(itemToRemove);
                                        }
                                    }
                                }
                                if (obj.Count == 0)
                                {
                                    var lists = objList;
                                    foreach (var item in lists)
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            objs.CompletePickup = true;
                                            DB.SaveChanges();
                                        }
                                    }

                                    completepickup.ClassID = Class;
                                    completepickup.TeacherID = ID;
                                    completepickup.Date = dt;
                                    completepickup.Active = true;
                                    completepickup.Deleted = false;
                                    completepickup.ModifyBy = ID;
                                    completepickup.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();

                                    Chk = true;
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "All Students have been Picked";
                                    ResponseObject.Data = Chk;
                                }
                                else
                                {
                                    Chk = false;
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Not All Students have been Picked";
                                    ResponseObject.Data = Chk;
                                }
                            }
                        }
                        else
                        {
                            Chk = true;
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Already Checked";
                            ResponseObject.Data = Chk;
                        }
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Data to Display";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ExportToExcelAtte()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                string Date = OperationManagement.GetPostValues("DATE");
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string Status = OperationManagement.GetPostValues("STATUS");
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                string SortBy = OperationManagement.GetPostValues("SORTBY");
                bool? Email = OperationManagement.GetPostValues("ISEMAIL") != "" ? OperationManagement.GetPostValues("ISEMAIL") == "True" ? true : false : false;
                StudentManagement objStudentManagement = new StudentManagement();
                PickupManagement ObjPickupManagement = new PickupManagement();
                List<MISAttendance> objList = new List<MISAttendance>();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objStudentManagement.AttendenceList(SchoolID, ClassID, Date, StudentID, TeacherID, Status, OrderBy, SortBy);
                }
                else
                {
                    objList = objStudentManagement.AttendenceLists(SchoolID, ClassID, Date, StudentID, TeacherID, Status, OrderBy, SortBy);
                }

                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator191"] = r.ToString();

                string filepath = Server.MapPath("~/Upload") + "/AttendenceReport" + Session["Generator191"].ToString() + ".xlsx";
                string FileNames = "Upload/AttendenceReport" + Session["Generator191"].ToString() + ".xlsx";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = objList;
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("AttendenceReport" + Session["Generator191"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "StudentName");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Date");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.Status);
                    CreateCell(row, item.Date != null ? item.Date.Value.ToString("dd/MM/yyyy") : "");
                }
                spreadsheetDocument.Close();

                if (Email == true)
                {
                    EmailManagement ObjEmailManagement = new EmailManagement();
                    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: AttendenceReport<br><br>Description &nbsp;: Here we Send you a Mail for Student Attendence So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", objTeacher.Name);
                    string FileNamess = Server.MapPath("~/Upload/AttendenceReport" + Session["Generator191"].ToString() + ".xlsx");
                    ObjEmailManagement.SendEmails(objTeacher.Email, "AttendenceReport", Message, FileNamess);
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Attendence List Exported Successfully";
                    ResponseObject.Data = FileNames;
                    Session["Generator191"] = null;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Attendence List Exported";
                    ResponseObject.Data = "";
                    Session["Generator191"] = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ExportToExcelClassReport()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                string Date = OperationManagement.GetPostValues("DATE") != "" ? OperationManagement.GetPostValues("DATE") : "";
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS").ToString() : "";
                string SortBy = OperationManagement.GetPostValues("SORTBY") != "" ? OperationManagement.GetPostValues("SORTBY").ToString() : "";
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                bool? Email = OperationManagement.GetPostValues("ISEMAIL") != "" ? OperationManagement.GetPostValues("ISEMAIL") == "True" ? true : false : false;
                ClassManagement objClassManagement = new ClassManagement();
                List<MViewStudentPickUp> objList = objClassManagement.DailyClassReports(SchoolID, Date, ClassID, TeacherID, StudentID, Status, OrderBy, SortBy);

                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID);
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator192"] = r.ToString();

                string filepath = Server.MapPath("~/Upload") + "/TeacherClassDailyReport" + Session["Generator192"].ToString() + ".xlsx";
                string FileNames = "Upload/TeacherClassDailyReport" + Session["Generator192"].ToString() + ".xlsx";
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                //////////////////Adding Sheet here
                Row Hederrow;
                var lists = objList;
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = ("TeacherClassDailyReport" + Session["Generator192"].ToString()).ToString() };
                sheets.Append(sheet);

                Hederrow = new Row();
                sheetData.Append(Hederrow);
                CreateCell(Hederrow, "Sr No");
                CreateCell(Hederrow, "Student Name");
                CreateCell(Hederrow, "Status");
                CreateCell(Hederrow, "Pickup Time");
                CreateCell(Hederrow, "Pickers Name");
                int Total = 0;
                foreach (var item in lists)
                {
                    Row row = new Row();
                    sheetData.Append(row);
                    CreateCell(row, (Total += 1).ToString());
                    CreateCell(row, item.StudentName);
                    CreateCell(row, item.PickStatus);
                    CreateCell(row, item.Pick_Time);
                    CreateCell(row, item.PickerName);
                }
                spreadsheetDocument.Close();

                if (Email == true)
                {
                    EmailManagement ObjEmailManagement = new EmailManagement();
                    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: ClassDailyReport<br><br>Description &nbsp;: Here we Send you a Mail for Class Daily Report So Please Find Below Attachment.<br /><br/>Thanks, <br/> SchoolApp Management System", objTeacher.Name);
                    string FileNamess = Server.MapPath("~/Upload/TeacherClassDailyReport" + Session["Generator192"].ToString() + ".xlsx");
                    ObjEmailManagement.SendEmails(objTeacher.Email, "Class Daily Report", Message, FileNamess);
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Class Daily Report Exported Successfully";
                    ResponseObject.Data = FileNames;
                    Session["Generator192"] = null;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Class Daily Report Exported";
                    ResponseObject.Data = "";
                    Session["Generator192"] = null;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void StudentAttendenceReport()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM");
                string DateTo = OperationManagement.GetPostValues("DATETO");
                string Status = OperationManagement.GetPostValues("STATUS");
                string SortBy = OperationManagement.GetPostValues("SORTBY");
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");

                PickupManagement ObjpickupManagement = new PickupManagement();
                List<MGetAttendenceData> objList = new List<MGetAttendenceData>();
                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Active == true && p.Deleted == true);
                if (_Student != null)
                {
                    List<MGetAttendenceData> ObjList = new List<MGetAttendenceData>();
                    if (_Student.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                    {
                        objList = ObjpickupManagement.StudentAttendenceReport(StudentID, DateFrom, DateTo, TeacherID.ToString(), Status, SortBy, OrderBy, StudentID);
                    }
                    else
                    {
                        objList = ObjpickupManagement.StudentAttendenceReportAfterSchool(StudentID, DateFrom, DateTo, TeacherID.ToString(), Status, SortBy, OrderBy, StudentID);
                    }
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Student Attendence List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Student Attendence List Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        #endregion

        #region School Section
        public void SchoolLogin()
        {
            try
            {

                SchoolManagement objSchoolManagement = new SchoolManagement();
                ISSchool Obj = objSchoolManagement.SchoolLogin(OperationManagement.GetPostValues("EMAIL"), OperationManagement.GetPostValues("PASSWORD"));
                if (Obj != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Logged in Successfully";
                    ResponseObject.Data = Obj;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Invalid Credential";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetAdminRoles()
        {
            try
            {
                int SchoolID = Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));

                RolesManagement objRolesManagement = new RolesManagement();
                var ObjList = objRolesManagement.AdminRoleList(SchoolID).Where(p => p.RoleType == (int)EnumsManagement.ROLETYPE.TEACHING && p.Active == true);
                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Roles Found";
                    ResponseObject.Data = ObjList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "no Roles Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void SchoolForgotPassword()
        {
            try
            {

                string Email = OperationManagement.GetPostValues("EMAIL");
                ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.Deleted == true && (p.AdminEmail == Email) && p.Active == true);
                if (obj != null)
                {

                    string password = CommonOperation.GenerateNewRandom();
                    string Message = "";
                    if (obj.AdminEmail == Email)
                    {
                        obj.Password = EncryptionHelper.Encrypt(password);
                        DB.SaveChanges();
                        Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br>Author Name<br>Mobile No :  <br>Email id : <br>", obj.AdminEmail, obj.AdminEmail, password);
                    }



                    EmailManagement objEmailManagement = new EmailManagement();
                    if (objEmailManagement.SendEmail(Email, "Reset Password", Message))
                    {
                        LogManagement.AddLogs("Password Changed Successfully " + "Name : " + obj.Name + " Document Category : Reset Password", obj.ID, obj.ID, String.Format("{0} {1}", obj.AdminFirstName, obj.AdminLastName), "Profile");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Password Send Successfully in your Email";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Email sent failed";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Email address not found!";
                    ResponseObject.Data = "";

                }


            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SchoolClassYear()
        {
            try
            {
                var Obj = CommonOperation.GetYear();
                if (Obj != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Year Found Successfully";
                    ResponseObject.Data = Obj.ToList();

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Year Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetClassByYear()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int StatusValue = OperationManagement.GetPostValues("STATUS") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STATUS")) : 1;
                int AID = OperationManagement.GetPostValues("AID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("AID")) : 0;
                int GroupID = OperationManagement.GetPostValues("GROUPID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("GROUPID")) : 0;
                ClassManagement objClassManagement = new ClassManagement();
                if (GroupID == (int)EnumsManagement.MESSAGEUSERTYPE.NonTeacher)
                {
                    List<MISClass> ObjList = objClassManagement.ClassList(SchoolID, OperationManagement.GetPostValues("YEAR"), 0);
                    ObjList = ObjList.Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex")).ToList();
                    if (StatusValue == 1)
                    {
                        ObjList = ObjList.Where(p => p.Active == true).ToList();
                    }
                    if (StatusValue == 2)
                    {
                        ObjList = ObjList.Where(p => p.Active == false).ToList();
                    }
                    if (ObjList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Class Found Successfully";
                        ResponseObject.Data = ObjList.ToList();
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Class Found";
                        ResponseObject.Data = "";

                    }
                }
                else
                {
                    List<MISClass> ObjList = objClassManagement.ClassList(SchoolID, OperationManagement.GetPostValues("YEAR"), Convert.ToInt32(OperationManagement.GetPostValues("TYPEID") == "" ? "0" : OperationManagement.GetPostValues("TYPEID")));
                    if (AID != 0)
                    {
                        ObjList = ObjList.Where(p => !p.Name.Contains("(After School Ex)")).ToList();
                    }
                    if (StatusValue == 1)
                    {
                        ObjList = ObjList.Where(p => p.Active == true).ToList();
                    }
                    if (StatusValue == 2)
                    {
                        ObjList = ObjList.Where(p => p.Active == false).ToList();
                    }
                    if (ObjList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Class Found Successfully";
                        ResponseObject.Data = ObjList.ToList();
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Class Found";
                        ResponseObject.Data = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetClassTypeList()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("ID"));
                int AID = OperationManagement.GetPostValues("AID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("AID"));
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClassType> objLists = objClassManagement.ClassTypeList();
                if (ID != 0)
                {
                    objLists = objLists.Where(p => p.ID == ID).ToList();
                }
                if (AID != 0)
                {
                    objLists = objLists.Where(p => p.ID != AID).ToList();
                }
                if (objLists.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "ClassTypes Found Successfully";
                    ResponseObject.Data = objLists;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No ClassType Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetSchoolTypeList()
        {
            try
            {
                List<MISAfterSchoolType> people = new List<MISAfterSchoolType>{
                   new MISAfterSchoolType{ID = 1, AfterSchoolType = "Internal"},
                   new MISAfterSchoolType{ID = 2, AfterSchoolType = "External"}
                   };


                if (people.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "After School Types Found Successfully";
                    ResponseObject.Data = people;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No After School Type Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void CreateClass()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID"));
                string ClassName = OperationManagement.GetPostValues("CLASSNAME");
                string Year = OperationManagement.GetPostValues("YEAR");
                int ClassTypeID = OperationManagement.GetPostValues("CLASSTYPEID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("CLASSTYPEID"));
                string AfterSchoolType = OperationManagement.GetPostValues("AFTERSCHOOLTYPE");
                string ExtOrganisation = OperationManagement.GetPostValues("EXTERNALORGANISATION");
                string EndDate = OperationManagement.GetPostValues("ENDDATE") == "" ? "" : OperationManagement.GetPostValues("ENDDATE");
                int Type = OperationManagement.GetPostValues("CREATEDTYPE") == "" ? 1 : Convert.ToInt32(OperationManagement.GetPostValues("CREATEDTYPE"));
                //bool Active = OperationManagement.GetPostValues("ACTIVE") == "False" ? false : true;

                List<ISClass> ObjClasses = DB.ISClasses.Where(p => p.Name == ClassName && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                if (ObjClasses.Count > 0)
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "ClassName already Exist";
                    ResponseObject.Data = "";
                }
                else
                {
                    ISClass obj = new ISClass();
                    if (ClassTypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                    {
                        List<ISClass> ObjClass = DB.ISClasses.Where(p => p.SchoolID == SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool).ToList();
                        if (ObjClass.Count <= 0)
                        {
                            if (AfterSchoolType == "Internal")
                            {
                                obj.Name = ClassName + "(After School)";
                            }
                            else
                            {
                                obj.Name = ClassName + "(After School Ex)";
                            }
                            obj.TypeID = ClassTypeID;
                            obj.AfterSchoolType = AfterSchoolType;
                            obj.ExternalOrganisation = ExtOrganisation;
                            //if (EndDate != "")
                            //{
                            //    string dates = EndDate;
                            //    string Format = "";
                            //    if (dates.Contains("/"))
                            //    {
                            //        string[] arrDate = dates.Split('/');
                            //        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                            //    }
                            //    else
                            //    {
                            //        Format = dates;
                            //    }
                            //    DateTime dt2 = Convert.ToDateTime(Format);
                            //    obj.EndDate = dt2.Date;
                            //}
                            obj.EndDate = new DateTime(2050, 01, 01);
                            obj.SchoolID = SchoolID;
                            obj.Active = true;
                            obj.Deleted = true;
                            if (Type == (int)EnumsManagement.CREATEBYTYPE.School)
                            {
                                obj.CreatedBy = SchoolID;
                            }
                            else
                            {
                                obj.CreatedBy = TeacherID;
                            }
                            obj.CreatedDateTime = DateTime.Now;
                            obj.CreatedByType = Type;
                            DB.ISClasses.Add(obj);
                            DB.SaveChanges();
                            ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                            LogManagement.AddLogs("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Class Created Successfully";
                            ResponseObject.Data = "";
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "Only One After School is Allowed for a Standard School";
                            ResponseObject.Data = "";
                        }
                    }
                    else
                    {
                        obj.Name = ClassName;
                        if (ClassTypeID == (int)EnumsManagement.CLASSTYPE.Standard)
                        {
                            obj.Year = Year;
                        }
                        obj.TypeID = ClassTypeID;
                        if (EndDate != "")
                        {
                            string dates = EndDate;
                            string Format = "";
                            if (dates.Contains("/"))
                            {
                                string[] arrDate = dates.Split('/');
                                Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                            }
                            else
                            {
                                Format = dates;
                            }
                            DateTime dt2 = Convert.ToDateTime(Format);
                            obj.EndDate = dt2.Date;
                        }
                        obj.SchoolID = SchoolID;
                        obj.Active = true;
                        obj.Deleted = true;
                        if (Type == (int)EnumsManagement.CREATEBYTYPE.School)
                        {
                            obj.CreatedBy = SchoolID;
                        }
                        else
                        {
                            obj.CreatedBy = TeacherID;
                        }
                        obj.CreatedDateTime = DateTime.Now;
                        obj.CreatedByType = Type;
                        DB.ISClasses.Add(obj);
                        DB.SaveChanges();
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Class Created Successfully";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DeleteAdminRoles()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                ISUserRole objRole = DB.ISUserRoles.SingleOrDefault(p => p.ID == ID && p.SchoolID == SchoolID && p.Deleted == true);
                if (objRole != null)
                {
                    if (objRole.RoleName == "Standard" || objRole.RoleName == "Admin")
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Role Can't be Deleted";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        objRole.Active = false;
                        objRole.Deleted = false;
                        objRole.DeletedBy = SchoolID;
                        objRole.DeletedDateTime = DateTime.Now;
                        DB.SaveChanges();

                        ISUserRole ObjRoles = DB.ISUserRoles.SingleOrDefault(p => p.RoleName == "Standard" && p.RoleType == objRole.RoleType && p.SchoolID == SchoolID);
                        List<ISTeacher> ObJ = DB.ISTeachers.Where(p => p.RoleID == ID && p.Active == true && p.Deleted == true).ToList();
                        foreach (var item in ObJ)
                        {
                            item.RoleID = ObjRoles.ID;
                            DB.SaveChanges();
                        }

                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Role Deleted Successfully " + "Name : " + objRole.RoleName + " Document Category : UserRole", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "UserRole");

                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Role Deleted Successfully";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Role not Deleted";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void AddUpdateAdminRole()
        {
            try
            {
                RolesManagement objRolesManagement = new RolesManagement();
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string RoleName = OperationManagement.GetPostValues("ROLENAME");
                int RoleType = OperationManagement.GetPostValues("ROLETYPE") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ROLETYPE")) : 0;
                bool ManageTeacherFlag = OperationManagement.GetPostValues("MANAGETEACHERFLAG") == "True" ? true : false;
                bool ManageClassFlag = OperationManagement.GetPostValues("MANAGECLASSFLAG") == "True" ? true : false;
                bool ManageSupportFlag = OperationManagement.GetPostValues("MANAGESUPPORTFLAG") == "True" ? true : false;
                bool ManageStudentFlag = OperationManagement.GetPostValues("MANAGESTUDENTFLAG") == "True" ? true : false;
                bool ManageViewAccountFlag = OperationManagement.GetPostValues("MANAGEVIEWACCOUNTFLAG") == "True" ? true : false;
                bool ManageNonTeacherFlag = OperationManagement.GetPostValues("MANAGENONTEACHERFLAG") == "True" ? true : false;
                bool ManageHolidayFlag = OperationManagement.GetPostValues("MANAGEHOLIDAYFLAG") == "True" ? true : false;
                bool Active = OperationManagement.GetPostValues("ACTIVE") == "True" ? true : false;
                if (ID != 0)
                {
                    List<ISUserRole> obj = DB.ISUserRoles.Where(p => p.RoleName == RoleName && p.RoleType == RoleType && p.SchoolID == SchoolID && p.ID != ID && p.Deleted == true).ToList();
                    if (obj.Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "RoleName already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        if ((RoleName == "Standard" || RoleName == "Admin") && Active == false)
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "You can not made this role InActive!!!";
                            ResponseObject.Data = "";
                        }
                        else
                        {
                            objRolesManagement.CreateorUpdateRole(ID, SchoolID, RoleName, RoleType, ManageTeacherFlag, ManageClassFlag, ManageSupportFlag, ManageStudentFlag, ManageViewAccountFlag, ManageNonTeacherFlag, ManageHolidayFlag, Active);
                            ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                            LogManagement.AddLogs("Role Updated Successfully " + "Name : " + RoleName + " Document Category : UserRole", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "UserRole");
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Role Updated Successfully";
                            ResponseObject.Data = "";
                        }
                    }
                }
                else
                {
                    List<ISUserRole> obj = DB.ISUserRoles.Where(p => p.RoleName == RoleName && p.RoleType == RoleType && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                    if (obj.Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "RoleName already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        objRolesManagement.CreateorUpdateRole(0, SchoolID, RoleName, RoleType, ManageTeacherFlag, ManageClassFlag, ManageSupportFlag, ManageStudentFlag, ManageViewAccountFlag, ManageNonTeacherFlag, ManageHolidayFlag, Active);
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Role Created Successfully " + "Name : " + RoleName + " Document Category : UserRole", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "UserRole");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Role Created Successfully";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void EditAdminRole()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                RolesManagement objRolesManagement = new RolesManagement();
                MISUserRole Obj = objRolesManagement.GetRole(ID);
                if (Obj != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Role Edited Successfully";
                    ResponseObject.Data = Obj;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Role not Edited";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void ClassDetails()
        {
            try
            {
                int ClassID = OperationManagement.GetPostValues("CLASSID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                ClassManagement objClassManagement = new ClassManagement();
                MISClass ObjList = objClassManagement.GetClass(ClassID);
                if (ObjList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Class Details Found Successfully";
                    ResponseObject.Data = ObjList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Class Details Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void UpdateClass()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string ClassName = OperationManagement.GetPostValues("CLASSNAME");
                string Year = OperationManagement.GetPostValues("YEAR");
                int ClassTypeID = OperationManagement.GetPostValues("CLASSTYPEID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSTYPEID")) : 0;
                string AfterSchoolType = OperationManagement.GetPostValues("AFTERSCHOOLTYPE");
                string ExtOrganisation = OperationManagement.GetPostValues("EXTERNALORGANISATION");
                string EndDate = OperationManagement.GetPostValues("ENDDATE");
                bool Active = OperationManagement.GetPostValues("ACTIVE") == "False" ? false : true;
                bool? ISNonListed = OperationManagement.GetPostValues("ISNONLISTED") == "True" ? true : false;
                ClassManagement objClassManagement = new ClassManagement();
                if (ID != 0)
                {

                    List<ISClass> ObjClasses = DB.ISClasses.Where(p => p.ID != ID && p.Name == ClassName && p.SchoolID == SchoolID && p.Deleted == true).ToList();
                    if (ObjClasses.Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "ClassName already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ISClass obj = DB.ISClasses.SingleOrDefault(p => p.ID == ID && p.SchoolID == SchoolID && p.Deleted == true);
                        if (obj != null)
                        {
                            if (ClassTypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool)
                            {
                                List<ISClass> ObjClass = DB.ISClasses.Where(p => p.ID != ID && p.SchoolID == Authentication.SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).ToList();
                                if (ObjClass.Count <= 0)
                                {
                                    if (DB.ISStudents.Where(p => p.ClassID == ID && p.Active == true && p.Deleted == true).Count() > 0)
                                    {
                                        if (Active == false)
                                        {
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "Class can not be made InActive";
                                            ResponseObject.Data = "";
                                        }
                                        else
                                        {
                                            objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                            ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                            LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                            ResponseObject.Status = "S";
                                            ResponseObject.Message = "Class Updated Successfully";
                                            ResponseObject.Data = "";
                                        }
                                    }
                                    else
                                    {
                                        objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                        LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Class Updated Successfully";
                                        ResponseObject.Data = "";
                                    }
                                }
                                else
                                {
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Only One After School is Allowed for a Standard School";
                                    ResponseObject.Data = "";
                                }
                            }
                            else
                            {
                                if (DB.ISStudents.Where(p => p.ClassID == ID && p.Active == true && p.Deleted == true).Count() > 0)
                                {
                                    if (Active == false)
                                    {
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Class can not be made InActive";
                                        ResponseObject.Data = "";
                                    }
                                    else
                                    {
                                        objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                        LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Class Updated Successfully";
                                        ResponseObject.Data = "";
                                    }
                                }
                                else
                                {
                                    objClassManagement.CreateorUpdateClass(ID, SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active);
                                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                    LogManagement.AddLogs("Class Updated Successfully " + "Name : " + obj.Name + " Document Category : Class", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Class");
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Class Updated Successfully";
                                    ResponseObject.Data = "";
                                }
                            }
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "Class not Found";
                            ResponseObject.Data = "";
                        }
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Class not Updated";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void UserActivity()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM");
                string DateTo = OperationManagement.GetPostValues("DATETO");
                string LogName = OperationManagement.GetPostValues("LOGNAME");
                string DocCategory = OperationManagement.GetPostValues("DOCCATEGORY");
                string UpdatedBy = OperationManagement.GetPostValues("UPDATEDBY");
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                string SortBy = OperationManagement.GetPostValues("SORTBY");

                LogManagement objLogManagement = new LogManagement();
                List<MISUserActivity> obj = objLogManagement.UserActivity(SchoolID, DateFrom, DateTo, LogName, UpdatedBy, OrderBy, SortBy);

                if (obj.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "User Activity Found Successfully";
                    ResponseObject.Data = obj;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "User Activity not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PaymentList()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM") != "" ? OperationManagement.GetPostValues("DATEFROM") : "";
                string DateTo = OperationManagement.GetPostValues("DATETO") != "" ? OperationManagement.GetPostValues("DATETO") : "";
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS") : "0";
                string OrderBy = OperationManagement.GetPostValues("ORDERBY") != "" ? OperationManagement.GetPostValues("ORDERBY") : "";
                string SortBy = OperationManagement.GetPostValues("SORTBY") != "" ? OperationManagement.GetPostValues("SORTBY") : "";

                PaymentManagement objPaymentManagement = new PaymentManagement();
                List<MISSchoolInvoice> obj = objPaymentManagement.InvoicesList(SchoolID, DateFrom, DateTo, Status, OrderBy, SortBy);

                if (obj.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Payment List Found Successfully";
                    ResponseObject.Data = obj;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Payment List not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void PaymentStatus()
        {
            try
            {

                List<MISTrasectionStatus> objList = (from item in DB.ISTrasectionStatus.Where(p => p.Active == true && p.Deleted == true).ToList()
                                                     select new MISTrasectionStatus
                                                     {
                                                         ID = item.ID,
                                                         Name = item.Name,
                                                         Active = item.Active,
                                                         Deleted = item.Deleted,
                                                     }).ToList();

                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Payment List Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Payment List not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherList()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string Year = OperationManagement.GetPostValues("YEAR");
                int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                string TeacherName = OperationManagement.GetPostValues("TEACHERNAME");
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                string SortBy = OperationManagement.GetPostValues("SORTBY");
                string TYPEID = OperationManagement.GetPostValues("TYPEID") == "" ? "0" : OperationManagement.GetPostValues("TYPEID");
                string Status = OperationManagement.GetPostValues("STATUS") == "" ? "0" : OperationManagement.GetPostValues("STATUS");

                DB.Configuration.ProxyCreationEnabled = false;

                TeacherManagement objTeacherManagement = new TeacherManagement();
                List<MISTeacher> objList = objTeacherManagement.TeacherList(SchoolID, Year, ClassID, TeacherName, OrderBy, SortBy, Convert.ToInt32(TYPEID), "0");
                if (Status != "0")
                {
                    if (Status == "1")
                    {
                        objList = objList.Where(p => p.Active == true).ToList();
                    }
                    else
                    {
                        objList = objList.Where(p => p.Active == false).ToList();
                    }
                }

                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Teacher List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Teacher List not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetStudentInfo()
        {
            try
            {
                {
                    int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                    ClassManagement objClassManagement = new ClassManagement();

                    MISStudent objList = objClassManagement.GetStudentInfo(ID);

                    if (objList != null)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Student Information Found Successfully";
                        ResponseObject.Data = objList;

                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Student Information Found";
                        ResponseObject.Data = "";

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void CreateUpdateStudent()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string StudentName = OperationManagement.GetPostValues("STUDENTNAME");
                string StudentNo = OperationManagement.GetPostValues("STUDENTNO");
                string Photo = OperationManagement.GetPostValues("PHOTOURL") != "" ? OperationManagement.GetPostValues("PHOTOURL") : "";
                string PPName = OperationManagement.GetPostValues("PPNAME");
                string PPEmail = OperationManagement.GetPostValues("PPEMAIL");
                string PPPhone = OperationManagement.GetPostValues("PPPHONE");
                string PPRelation = OperationManagement.GetPostValues("PPRELATION");
                string SPName = OperationManagement.GetPostValues("SPNAME");
                string SPEmail = OperationManagement.GetPostValues("SPEMAIL");
                string SPPhone = OperationManagement.GetPostValues("SPPHONE");
                string SPRelation = !String.IsNullOrEmpty(OperationManagement.GetPostValues("SPRELATION")) ? OperationManagement.GetPostValues("SPRELATION") : "";
                Session["PName"] = null;
                Session["SName"] = null;
                string Message = "";
                ISStudent obj = new ISStudent();
                if (ID != 0)
                {
                    IList<ISStudent> obj2 = DB.ISStudents.Where(a => a.ID != ID && a.StudentNo == StudentNo && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Student ID Number Already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName == StudentName && a.ParantName1 == PPName && a.ParantEmail1 == PPEmail && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Student Name With Primary Parent Email already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.ID != ID && a.StudentName == StudentName && a.ParantName2 == SPName && a.ParantEmail2 == SPEmail && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Student Name With Secondary Parent Email already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantName1 != PPName && a.ParantEmail1 == PPEmail && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Primary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.ID != ID && a.ParantName2 != SPName && a.ParantEmail2 == SPEmail && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Secondary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                        if (obj != null)
                        {
                            Session["PName"] = obj.ParantName1 + "(" + obj.ParantRelation1 + ")";
                            string PNames = Session["PName"] != null ? Convert.ToString(Session["PName"]) : "";
                            if (obj.ParantName2 != "" && obj.ParantName2 != null)
                            {
                                Session["SName"] = obj.ParantName2 + "(" + obj.ParantRelation2 + ")";
                            }
                            string SNames = Session["SName"] != null ? Session["SName"].ToString() : "";

                            obj.StudentName = StudentName;
                            if (obj.ClassID != ClassID)
                            {
                                ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                                ObjReassign.SchoolID = Authentication.SchoolID;
                                ObjReassign.FromClass = obj.ClassID;
                                ObjReassign.ToClass = ClassID;
                                ObjReassign.Date = DateTime.Now;
                                ObjReassign.StduentID = ID;
                                ObjReassign.Active = true;
                                ObjReassign.Deleted = true;
                                ObjReassign.CreatedBy = SchoolID;
                                ObjReassign.CreatedByType = (int)EnumsManagement.CREATEBYTYPE.School;
                                ObjReassign.CreatedDateTime = DateTime.Now;
                                DB.ISStudentReassignHistories.Add(ObjReassign);
                                DB.SaveChanges();
                            }
                            obj.ClassID = ClassID;
                            obj.StudentNo = StudentNo;
                            obj.SchoolID = SchoolID;
                            obj.ParantName1 = PPName;
                            obj.ParantEmail1 = PPEmail;
                            obj.ParantPhone1 = PPPhone;
                            obj.ParantRelation1 = PPRelation;
                            obj.ParantName2 = SPName;
                            obj.ParantEmail2 = SPEmail;
                            obj.ParantPhone2 = SPPhone;
                            obj.ParantRelation2 = SPRelation;
                            obj.ModifyBy = SchoolID;
                            obj.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                            try
                            {
                                ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == PNames && p.Deleted == true);
                                if (objPicker != null)
                                {
                                    objPicker.SchoolID = SchoolID;
                                    objPicker.ParentID = ID;
                                    objPicker.StudentID = ID;
                                    objPicker.FirstName = PPName + "(" + PPRelation + ")";
                                    objPicker.Email = PPEmail;
                                    objPicker.Phone = PPPhone;
                                    objPicker.ModifyBy = SchoolID;
                                    objPicker.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();
                                }
                                if (SPName != "" && SPEmail != "" && SPRelation != "")
                                {
                                    ISPicker objPickers = DB.ISPickers.SingleOrDefault(p => p.StudentID == ID && p.FirstName == SNames && p.Deleted == true);
                                    if (objPickers != null)
                                    {
                                        objPickers.SchoolID = SchoolID;
                                        objPickers.ParentID = ID;
                                        objPickers.StudentID = ID;
                                        objPickers.FirstName = SPName + "(" + SPRelation + ")";
                                        objPickers.Email = SPEmail;
                                        objPickers.Phone = SPPhone;
                                        objPickers.ModifyBy = SchoolID;
                                        objPickers.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPicker objPickers1 = new ISPicker();
                                        objPickers1.SchoolID = SchoolID;
                                        objPickers1.ParentID = ID;
                                        objPickers1.StudentID = ID;
                                        objPickers1.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                                        objPickers1.FirstName = SPName + "(" + SPRelation + ")";
                                        objPickers1.Photo = "Upload/user.jpg";
                                        objPickers1.Email = SPEmail;
                                        objPickers1.Phone = SPPhone;
                                        objPickers1.OneOffPickerFlag = false;
                                        objPickers1.ActiveStatus = "Active";
                                        objPickers1.Active = true;
                                        objPickers1.Deleted = true;
                                        objPickers1.CreatedBy = SchoolID;
                                        objPickers1.CreatedDateTime = DateTime.Now;
                                        DB.ISPickers.Add(objPickers1);
                                        DB.SaveChanges();
                                        if (DB.ISPickers.Where(p => p.ID == objPickers1.ID && p.Active == true).ToList().Count > 0)
                                        {
                                            ISPickerAssignment objAssigns = new ISPickerAssignment();
                                            objAssigns.PickerId = objPickers1.ID;
                                            objAssigns.StudentID = ID;
                                            objAssigns.RemoveChildStatus = 0;
                                            DB.ISPickerAssignments.Add(objAssigns);
                                            DB.SaveChanges();
                                        }
                                    }
                                }
                                else
                                {
                                    ISPicker objPickers = DB.ISPickers.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && p.SchoolID == SchoolID && p.FirstName == SNames);
                                    if (objPickers != null)
                                    {
                                        objPickers.Active = false;
                                        objPickers.Deleted = false;
                                        objPickers.DeletedBy = SchoolID;
                                        objPickers.DeletedDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                LogManagement.AddLogs("Student Updated Successfully " + "Name : " + obj.StudentName + " Document Category : StudentProfile", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Student");
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Student Updated Successfully";
                            ResponseObject.Data = "";
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "Student not Updated";
                            ResponseObject.Data = "";
                        }

                    }
                }
                else
                {
                    IList<ISStudent> objStudent = DB.ISStudents.Where(a => a.StudentNo == StudentNo && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList();
                    if (objStudent.Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Student ID Number Already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.StudentName == StudentName && a.ParantName1 == PPName && a.ParantEmail1 == PPEmail && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Student Name With Primary Parent Email already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.StudentName == StudentName && a.ParantName2 == SPName && a.ParantEmail2 == SPEmail && a.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Student Name With Secondary Parent Email already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.ParantName1 != PPName && a.ParantEmail1 == PPEmail && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Primary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISStudents.Where(a => a.ParantName2 != SPName && a.ParantEmail2 == SPEmail && a.StartDate == (DateTime?)null && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Secondary email address has been created with a different name. Can you please confirm the parents names with the parents before you proceed";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        obj.StudentName = StudentName;
                        obj.ClassID = ClassID;
                        obj.StudentNo = StudentNo;
                        obj.SchoolID = SchoolID;
                        obj.Photo = "Upload/user.jpg";
                        obj.ParantName1 = PPName;
                        obj.ParantEmail1 = PPEmail;
                        obj.ParantPhone1 = PPPhone;
                        obj.ParantRelation1 = PPRelation;
                        string ParantPassword1 = CommonOperation.GenerateNewRandom();
                        string ParantPassword2 = CommonOperation.GenerateNewRandom();
                        if (PPEmail != "")
                        {
                            obj.ParantPassword1 = EncryptionHelper.Encrypt(ParantPassword1);
                        }
                        if (SPEmail != "")
                        {
                            obj.ParantPassword2 = EncryptionHelper.Encrypt(ParantPassword2);
                        }
                        obj.ParantName2 = SPName;
                        obj.ParantEmail2 = SPEmail;
                        obj.ParantPhone2 = SPPhone;
                        obj.ParantRelation2 = SPRelation;
                        obj.CreatedBy = SchoolID;
                        obj.CreatedDateTime = DateTime.Now;
                        obj.Active = true;
                        obj.Deleted = true;
                        obj.Out = 0;
                        obj.Outbit = false;
                        DB.ISStudents.Add(obj);
                        DB.SaveChanges();

                        ISPicker objPicker = new ISPicker();
                        objPicker.SchoolID = SchoolID;
                        objPicker.ParentID = obj.ID;
                        objPicker.StudentID = obj.ID;
                        objPicker.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                        objPicker.FirstName = PPName + "(" + PPRelation + ")";
                        objPicker.Photo = "Upload/user.jpg";
                        objPicker.Email = PPEmail;
                        objPicker.Phone = PPPhone;
                        objPicker.OneOffPickerFlag = false;
                        objPicker.ActiveStatus = "Active";
                        objPicker.Active = true;
                        objPicker.Deleted = true;
                        objPicker.CreatedBy = SchoolID;
                        objPicker.CreatedDateTime = DateTime.Now;
                        DB.ISPickers.Add(objPicker);
                        DB.SaveChanges();
                        if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                        {
                            ISPickerAssignment objAssign = new ISPickerAssignment();
                            objAssign.PickerId = objPicker.ID;
                            objAssign.StudentID = obj.ID;
                            objAssign.RemoveChildStatus = 0;
                            DB.ISPickerAssignments.Add(objAssign);
                            DB.SaveChanges();
                        }
                        if (SPName != "" && SPEmail != "" && SPRelation != "")
                        {
                            ISPicker objPickers = new ISPicker();
                            objPickers.SchoolID = SchoolID;
                            objPickers.ParentID = obj.ID;
                            objPickers.StudentID = obj.ID;
                            objPickers.PickerType = (int)EnumsManagement.PICKERTYPE.Individual;
                            objPickers.FirstName = SPName + "(" + SPRelation + ")";
                            objPickers.Photo = "Upload/user.jpg";
                            objPickers.Email = SPEmail;
                            objPickers.Phone = SPPhone;
                            objPickers.OneOffPickerFlag = false;
                            objPickers.ActiveStatus = "Active";
                            objPickers.Active = true;
                            objPickers.Deleted = true;
                            objPickers.CreatedBy = SchoolID;
                            objPickers.CreatedDateTime = DateTime.Now;
                            DB.ISPickers.Add(objPickers);
                            DB.SaveChanges();
                            if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                            {
                                ISPickerAssignment objAssigns = new ISPickerAssignment();
                                objAssigns.PickerId = objPickers.ID;
                                objAssigns.StudentID = obj.ID;
                                objAssigns.RemoveChildStatus = 0;
                                DB.ISPickerAssignments.Add(objAssigns);
                                DB.SaveChanges();
                            }
                        }
                        try
                        {
                            EmailManagement objEmailManagement = new EmailManagement();
                            ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                            if (PPEmail != "")
                            {
                                Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", PPName, PPEmail, ParantPassword1, objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                objEmailManagement.SendEmail(PPEmail, "Your Account Credentials", Message);
                            }
                            if (SPEmail != "")
                            {
                                Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3} <br>Mobile No : {4} <br>Email id : {5} <br>", SPName, SPEmail, ParantPassword2, objSchool.Name, objSchool.PhoneNumber, objSchool.AdminEmail);
                                objEmailManagement.SendEmail(SPEmail, "Your Account Credentials", Message);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogManagement.AddLog(ex);
                        }
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Student Saved Successfully " + "Name : " + obj.StudentName + "Document Category : StudentClass", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Student");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Student Created Successfully";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SupportList()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string DateFrom = OperationManagement.GetPostValues("DATEFROM");
                string DateTo = OperationManagement.GetPostValues("DATETO");
                string StatusID = OperationManagement.GetPostValues("STATUSID") != "" ? OperationManagement.GetPostValues("STATUSID") : "0";
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                string SortBy = OperationManagement.GetPostValues("SORTBY");

                SupportManagement objSupportManagement = new SupportManagement();
                List<MISSupport> objList = objSupportManagement.SupportList(SchoolID, DateFrom, DateTo, StatusID, OrderBy, SortBy);

                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Support List Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Support List not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AddUpdateTeacher()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                int ClassID1 = OperationManagement.GetPostValues("CLASSID1") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID1")) : 0;
                int ClassID2 = OperationManagement.GetPostValues("CLASSID2") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID2")) : 0;
                int ClassID3 = OperationManagement.GetPostValues("CLASSID3") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID3")) : 0;
                int ClassID4 = OperationManagement.GetPostValues("CLASSID4") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID4")) : 0;
                int ClassID5 = OperationManagement.GetPostValues("CLASSID5") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID5")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ROLEID = OperationManagement.GetPostValues("ROLEID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ROLEID")) : 0;
                string TeacherNo = OperationManagement.GetPostValues("TEACHERNO");
                string TeacherTitle = OperationManagement.GetPostValues("TEACHERTITLE");
                string TeacherName = OperationManagement.GetPostValues("TEACHERNAME");
                string Phone = OperationManagement.GetPostValues("PHONE");
                string Email = OperationManagement.GetPostValues("EMAIL");
                string EndDate = OperationManagement.GetPostValues("ENDDATE");
                string Photo = OperationManagement.GetPostValues("PHOTO");
                int Type = OperationManagement.GetPostValues("CREATEDTYPE") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CREATEDTYPE")) : 1;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                bool Active = OperationManagement.GetPostValues("ACTIVE") == "False" ? false : true;
                EmailManagement objEmailManagement = new EmailManagement();
                TeacherManagement objTeacherManagement = new TeacherManagement();
                ISTeacher objTeacher = new ISTeacher();
                if (ID != 0)
                {
                    if (DB.ISTeachers.Where(a => a.ID != ID && a.Email == Email && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Email ID already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.ID != ID && a.PhoneNo == Phone && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Phone Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.ID != ID && a.TeacherNo == TeacherNo && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Teacher Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
                        objTeacher.SchoolID = SchoolID;
                        objTeacher.ClassID = ClassID1;
                        objTeacher.RoleID = ROLEID;
                        objTeacher.TeacherNo = TeacherNo;
                        objTeacher.Title = TeacherTitle;
                        objTeacher.Name = TeacherName;
                        objTeacher.PhoneNo = Phone;
                        objTeacher.Email = Email;
                        //if (EndDate != "")
                        //{
                        //    string dates = EndDate;
                        //    string Format = "";
                        //    if (dates.Contains("/"))
                        //    {
                        //        string[] arrDate = dates.Split('/');
                        //        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                        //    }
                        //    else
                        //    {
                        //        Format = dates;
                        //    }
                        //    DateTime dt2 = Convert.ToDateTime(Format);
                        //    objTeacher.EndDate = dt2.Date;
                        //}
                        //else
                        //{
                        //    objTeacher.EndDate = (DateTime?)null;
                        //}
                        //objTeacher.Photo = (!String.IsNullOrEmpty(Photo)) ? Photo : "Upload/user.jpg";
                        if (!String.IsNullOrEmpty(Photo))
                        {
                            objTeacher.Photo = Photo;
                        }
                        objTeacher.Active = Active;
                        //objTeacher.ModifyBy = SchoolID;

                        if (Type == (int)EnumsManagement.CREATEBYTYPE.School)
                        {
                            objTeacher.ModifyBy = SchoolID;
                        }
                        else
                        {
                            objTeacher.ModifyBy = TeacherID;
                        }
                        objTeacher.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();
                        //Remove Range from Teacher Class Assignment By Teacher ID
                        List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
                        if (objList.Count > 0)
                        {
                            if (ClassID1 != 0)
                            {
                                if (objList.Count >= 1)
                                {
                                    objTeacherManagement.TeacherReassignment(SchoolID, ID, objList[0].ClassID, ClassID1, Type, Type == (int)EnumsManagement.CREATEBYTYPE.School ? SchoolID : TeacherID);
                                }
                            }
                            if (ClassID2 != 0)
                            {
                                if (objList.Count > 1)
                                {
                                    objTeacherManagement.TeacherReassignment(SchoolID, ID, objList[1].ClassID, ClassID2, Type, Type == (int)EnumsManagement.CREATEBYTYPE.School ? SchoolID : TeacherID);
                                }
                            }
                            if (ClassID3 != 0)
                            {
                                if (objList.Count > 2)
                                {
                                    objTeacherManagement.TeacherReassignment(SchoolID, ID, objList[2].ClassID, ClassID3, Type, Type == (int)EnumsManagement.CREATEBYTYPE.School ? SchoolID : TeacherID);
                                }
                            }
                            if (ClassID4 != 0)
                            {
                                if (objList.Count > 3)
                                {
                                    objTeacherManagement.TeacherReassignment(SchoolID, ID, objList[3].ClassID, ClassID4, Type, Type == (int)EnumsManagement.CREATEBYTYPE.School ? SchoolID : TeacherID);
                                }
                            }
                            if (ClassID5 != 0)
                            {
                                if (objList.Count > 4)
                                {
                                    objTeacherManagement.TeacherReassignment(SchoolID, ID, objList[4].ClassID, ClassID5, Type, Type == (int)EnumsManagement.CREATEBYTYPE.School ? SchoolID : TeacherID);
                                }
                            }
                        }
                        DB.ISTeacherClassAssignments.RemoveRange(objList);
                        DB.SaveChanges();

                        //Add Teacher Class Assignment
                        if (Active == true)
                        {
                            ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                            objClass1.ClassID = ClassID1;
                            objClass1.TeacherID = ID;
                            objClass1.Active = true;
                            objClass1.Deleted = true;
                            objClass1.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                            objClass1.CreatedDateTime = DateTime.Now;
                            objClass1.Out = 0;
                            objClass1.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass1);
                            DB.SaveChanges();
                            if (ClassID2 != 0)
                            {
                                ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                                objClass2.ClassID = ClassID2;
                                objClass2.TeacherID = ID;
                                objClass2.Active = true;
                                objClass2.Deleted = true;
                                objClass2.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass2.CreatedDateTime = DateTime.Now;
                                objClass2.Out = 0;
                                objClass2.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass2);
                                DB.SaveChanges();
                            }
                            if (ClassID3 != 0)
                            {
                                ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                                objClass3.ClassID = ClassID3;
                                objClass3.TeacherID = ID;
                                objClass3.Active = true;
                                objClass3.Deleted = true;
                                objClass3.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass3.CreatedDateTime = DateTime.Now;
                                objClass3.Out = 0;
                                objClass3.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass3);
                                DB.SaveChanges();
                            }
                            if (ClassID4 != 0)
                            {
                                ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                                objClass4.ClassID = ClassID4;
                                objClass4.TeacherID = ID;
                                objClass4.Active = true;
                                objClass4.Deleted = true;
                                objClass4.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass4.CreatedDateTime = DateTime.Now;
                                objClass4.Out = 0;
                                objClass4.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass4);
                                DB.SaveChanges();
                            }
                            if (ClassID5 != 0)
                            {
                                ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                                objClass5.ClassID = ClassID5;
                                objClass5.TeacherID = ID;
                                objClass5.Active = true;
                                objClass5.Deleted = true;
                                objClass5.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass5.CreatedDateTime = DateTime.Now;
                                objClass5.Out = 0;
                                objClass5.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass5);
                                DB.SaveChanges();
                            }
                        }
                        else
                        {
                            ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                            objClass1.ClassID = ClassID1;
                            objClass1.TeacherID = ID;
                            objClass1.Active = false;
                            objClass1.Deleted = false;
                            objClass1.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                            objClass1.CreatedDateTime = DateTime.Now;
                            objClass1.Out = 0;
                            objClass1.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass1);
                            DB.SaveChanges();
                            if (ClassID2 != 0)
                            {
                                ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                                objClass2.ClassID = ClassID2;
                                objClass2.TeacherID = ID;
                                objClass2.Active = false;
                                objClass2.Deleted = false;
                                objClass2.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass2.CreatedDateTime = DateTime.Now;
                                objClass2.Out = 0;
                                objClass2.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass2);
                                DB.SaveChanges();
                            }
                            if (ClassID3 != 0)
                            {
                                ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                                objClass3.ClassID = ClassID3;
                                objClass3.TeacherID = ID;
                                objClass3.Active = false;
                                objClass3.Deleted = false;
                                objClass3.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass3.CreatedDateTime = DateTime.Now;
                                objClass3.Out = 0;
                                objClass3.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass3);
                                DB.SaveChanges();
                            }
                            if (ClassID4 != 0)
                            {
                                ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                                objClass4.ClassID = ClassID4;
                                objClass4.TeacherID = ID;
                                objClass4.Active = false;
                                objClass4.Deleted = false;
                                objClass4.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass4.CreatedDateTime = DateTime.Now;
                                objClass4.Out = 0;
                                objClass4.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass4);
                                DB.SaveChanges();
                            }
                            if (ClassID5 != 0)
                            {
                                ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                                objClass5.ClassID = ClassID5;
                                objClass5.TeacherID = ID;
                                objClass5.Active = false;
                                objClass5.Deleted = false;
                                objClass5.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass5.CreatedDateTime = DateTime.Now;
                                objClass5.Out = 0;
                                objClass5.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass5);
                                DB.SaveChanges();
                            }
                        }
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Teacher Updated Successfully " + "Name : " + objTeacher.Name + " Document Category : TeacherProfile", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Teacher");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Teacher Updated Successfully";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    if (DB.ISTeachers.Where(a => a.Email == Email && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Email ID already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.PhoneNo == Phone && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Phone Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.TeacherNo == TeacherNo && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Teacher Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        objTeacher.SchoolID = SchoolID;
                        objTeacher.ClassID = ClassID1;
                        objTeacher.RoleID = ROLEID;
                        objTeacher.Role = (int)EnumsManagement.ROLETYPE.TEACHING;
                        objTeacher.TeacherNo = TeacherNo;
                        objTeacher.Title = TeacherTitle;
                        objTeacher.Name = TeacherName;
                        objTeacher.PhoneNo = Phone;
                        objTeacher.Email = Email;
                        string Password = CommonOperation.GenerateNewRandom();
                        objTeacher.Password = EncryptionHelper.Encrypt(Password);
                        //if (EndDate != "")
                        //{
                        //    string dates = EndDate;
                        //    string Format = "";
                        //    if (dates.Contains("/"))
                        //    {
                        //        string[] arrDate = dates.Split('/');
                        //        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                        //    }
                        //    else
                        //    {
                        //        Format = dates;
                        //    }
                        //    DateTime dt2 = Convert.ToDateTime(Format);
                        //    objTeacher.EndDate = Convert.ToDateTime(dt2);
                        //}
                        objTeacher.EndDate = new DateTime(2050, 01, 01);
                        if (!String.IsNullOrEmpty(Photo))
                        {
                            objTeacher.Photo = Photo;
                        }
                        else
                        {
                            objTeacher.Photo = "Upload/user.jpg";
                        }
                        objTeacher.Active = Active;
                        objTeacher.Deleted = true;
                        if (Type == (int)EnumsManagement.CREATEBYTYPE.School)
                        {
                            objTeacher.CreatedBy = SchoolID;
                        }
                        else
                        {
                            objTeacher.CreatedBy = TeacherID;
                        }
                        objTeacher.CreatedByType = Type;
                        objTeacher.CreatedDateTime = DateTime.Now;
                        if (Active == false)
                        {
                            if (Type == (int)EnumsManagement.CREATEBYTYPE.School)
                            {
                                objTeacher.ModifyBy = SchoolID;
                            }
                            else
                            {
                                objTeacher.ModifyBy = TeacherID;
                            }
                            objTeacher.ModifyDateTime = DateTime.Now;
                        }
                        DB.ISTeachers.Add(objTeacher);
                        DB.SaveChanges();
                        if (Active == true)
                        {
                            ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                            objClass1.ClassID = ClassID1;
                            objClass1.TeacherID = objTeacher.ID;
                            objClass1.Active = true;
                            objClass1.Deleted = true;
                            objClass1.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                            objClass1.CreatedDateTime = DateTime.Now;
                            objClass1.Out = 0;
                            objClass1.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass1);
                            DB.SaveChanges();
                            if (ClassID2 != 0)
                            {
                                ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                                objClass2.ClassID = ClassID2;
                                objClass2.TeacherID = objTeacher.ID;
                                objClass2.Active = true;
                                objClass2.Deleted = true;
                                objClass2.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass2.CreatedDateTime = DateTime.Now;
                                objClass2.Out = 0;
                                objClass2.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass2);
                                DB.SaveChanges();
                            }
                            if (ClassID3 != 0)
                            {
                                ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                                objClass3.ClassID = ClassID3;
                                objClass3.TeacherID = objTeacher.ID;
                                objClass3.Active = true;
                                objClass3.Deleted = true;
                                objClass3.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass3.CreatedDateTime = DateTime.Now;
                                objClass3.Out = 0;
                                objClass3.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass3);
                                DB.SaveChanges();
                            }
                            if (ClassID4 != 0)
                            {
                                ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                                objClass4.ClassID = ClassID4;
                                objClass4.TeacherID = objTeacher.ID;
                                objClass4.Active = true;
                                objClass4.Deleted = true;
                                objClass4.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass4.CreatedDateTime = DateTime.Now;
                                objClass4.Out = 0;
                                objClass4.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass4);
                                DB.SaveChanges();
                            }
                            if (ClassID5 != 0)
                            {
                                ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                                objClass5.ClassID = ClassID5;
                                objClass5.TeacherID = objTeacher.ID;
                                objClass5.Active = true;
                                objClass5.Deleted = true;
                                objClass5.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass5.CreatedDateTime = DateTime.Now;
                                objClass5.Out = 0;
                                objClass5.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass5);
                                DB.SaveChanges();
                            }
                        }
                        else
                        {
                            ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                            objClass1.ClassID = ClassID1;
                            objClass1.TeacherID = objTeacher.ID;
                            objClass1.Active = false;
                            objClass1.Deleted = false;
                            objClass1.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                            objClass1.CreatedDateTime = DateTime.Now;
                            objClass1.Out = 0;
                            objClass1.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass1);
                            DB.SaveChanges();
                            if (ClassID2 != 0)
                            {
                                ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                                objClass2.ClassID = ClassID2;
                                objClass2.TeacherID = objTeacher.ID;
                                objClass2.Active = false;
                                objClass2.Deleted = false;
                                objClass2.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass2.CreatedDateTime = DateTime.Now;
                                objClass2.Out = 0;
                                objClass2.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass2);
                                DB.SaveChanges();
                            }
                            if (ClassID3 != 0)
                            {
                                ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                                objClass3.ClassID = ClassID3;
                                objClass3.TeacherID = objTeacher.ID;
                                objClass3.Active = false;
                                objClass3.Deleted = false;
                                objClass3.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass3.CreatedDateTime = DateTime.Now;
                                objClass3.Out = 0;
                                objClass3.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass3);
                                DB.SaveChanges();
                            }
                            if (ClassID4 != 0)
                            {
                                ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                                objClass4.ClassID = ClassID4;
                                objClass4.TeacherID = objTeacher.ID;
                                objClass4.Active = false;
                                objClass4.Deleted = false;
                                objClass4.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass4.CreatedDateTime = DateTime.Now;
                                objClass4.Out = 0;
                                objClass4.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass4);
                                DB.SaveChanges();
                            }
                            if (ClassID5 != 0)
                            {
                                ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                                objClass5.ClassID = ClassID5;
                                objClass5.TeacherID = objTeacher.ID;
                                objClass5.Active = false;
                                objClass5.Deleted = false;
                                objClass5.CreatedBy = (Type == (int)EnumsManagement.CREATEBYTYPE.School) ? SchoolID : TeacherID;
                                objClass5.CreatedDateTime = DateTime.Now;
                                objClass5.Out = 0;
                                objClass5.Outbit = false;
                                DB.ISTeacherClassAssignments.Add(objClass5);
                                DB.SaveChanges();
                            }
                        }
                        if (Email != "")
                        {
                            var obj = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                            string Messages = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", TeacherName, Email, Password, obj.Name, obj.PhoneNumber, obj.AdminEmail);
                            objEmailManagement.SendEmail(Email, "Your Account Credentials", Messages);
                        }
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Teacher Created Successfully " + "Name : " + objTeacher.Name + " Document Category : Teacher", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Teacher");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Teacher Created Successfully";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetTeacherByID()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                TeacherManagement objTeacherManagement = new TeacherManagement();
                MISTeacher ObjList = objTeacherManagement.GetTeacher(ID);
                if (ObjList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Teacher Found Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Teacher Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetRoles()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string RoleName = OperationManagement.GetPostValues("ROLENAME") != "" ? OperationManagement.GetPostValues("ROLENAME") : "";
                string RoleType = OperationManagement.GetPostValues("ROLETYPE") != "" ? OperationManagement.GetPostValues("ROLETYPE") : "0";
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS") : "1";
                RolesManagement objRolesManagement = new RolesManagement();
                List<MISUserRole> ObjList = objRolesManagement.AdminRoleList(SchoolID);
                if (RoleName != "")
                {
                    ObjList = ObjList.Where(p => p.RoleName.Contains(RoleName)).ToList();
                }
                if (RoleType != "0")
                {
                    int TypeID = Convert.ToInt32(RoleType);
                    ObjList = ObjList.Where(p => p.RoleType == TypeID).ToList();
                }
                if (Status != "0")
                {
                    if (Status == "1")
                    {
                        ObjList = ObjList.Where(p => p.Active == true).ToList();
                    }
                    else
                    {
                        ObjList = ObjList.Where(p => p.Active == false).ToList();
                    }
                }
                if (ObjList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Roles Found Successfully";
                    ResponseObject.Data = ObjList.ToList();

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Roles Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetSupportStatus()
        {
            try
            {
                SupportManagement objSupportManagement = new SupportManagement();
                List<MISSupportStatus> objList = objSupportManagement.SupportStatus();

                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Support Status Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Support Status not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void DeleteStudent()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                if (objStudent != null)
                {
                    objStudent.Active = false;
                    objStudent.Deleted = false;
                    objStudent.DeletedBy = SchoolID;
                    objStudent.DeletedDateTime = DateTime.Now;
                    DB.SaveChanges();
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Student Deleted Successfully " + "Name : " + objStudent.StudentName + " Document Category : StudentClass", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Student");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Student Deleted Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Student not Deleted";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void CreateSupport()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string TicketNo = OperationManagement.GetPostValues("TICKETNO");
                string Subject = OperationManagement.GetPostValues("SUBJECT");
                int LogTypeID = OperationManagement.GetPostValues("LOGTYPEID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("LOGTYPEID")) : 0;
                int StatusID = OperationManagement.GetPostValues("STATUSID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STATUSID")) : 0;
                string Details = OperationManagement.GetPostValues("DETAILS");
                string FileName = OperationManagement.GetPostValues("FILENAME");
                int CreatedBy = OperationManagement.GetPostValues("CREATEDBY") != null ? Convert.ToInt32(OperationManagement.GetPostValues("CREATEDBY")) : 0;
                string CreatedName = OperationManagement.GetPostValues("CREATEDBYNAME");

                ISSupport obj = new ISSupport();
                obj.SchoolID = SchoolID;
                obj.TicketNo = TicketNo;
                obj.Request = Subject;
                obj.StatusID = StatusID;
                obj.LogTypeID = LogTypeID;
                obj.Active = true;
                obj.Deleted = true;
                obj.CreatedBy = CreatedBy;
                obj.CreatedByName = CreatedName;
                obj.CreatedDateTime = DateTime.Now;
                DB.ISSupports.Add(obj);
                DB.SaveChanges();
                ISTicketMessage objTm = new ISTicketMessage();
                objTm.SupportID = obj.ID;
                objTm.SenderID = SchoolID;
                objTm.Message = Details;
                objTm.SelectFile = "Upload/" + FileName;
                objTm.CreatedDatetime = DateTime.Now;
                DB.ISTicketMessages.Add(objTm);
                DB.SaveChanges();
                if (obj != null)
                {
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Support Created Successfully " + "Subject : " + obj.Request + " Document Category : SupportLogCreate", ObjSchools.ID, CreatedBy, CreatedName, "Support");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Support Created Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Support not Created";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ViewSupport()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("SUPPORTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SUPPORTID")) : 0;
                SupportManagement objSupportManagement = new SupportManagement();
                MISSupport ObjList = objSupportManagement.GetSupport(ID);
                if (ObjList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Support Found Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Support Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void StudentClassAssignment()
        {
            try
            {
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                StudentManagement objStudentManagement = new StudentManagement();
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
                if (ObjStudent != null)
                {
                    if (ObjStudent.ClassID != ClassID)
                    {
                        ISStudentReassignHistory ObjReassign = new ISStudentReassignHistory();
                        ObjReassign.SchoolID = Authentication.SchoolID;
                        ObjReassign.FromClass = ObjStudent.ClassID;
                        ObjReassign.ToClass = ClassID;
                        ObjReassign.Date = DateTime.Now;
                        ObjReassign.StduentID = StudentID;
                        ObjReassign.Active = true;
                        ObjReassign.Deleted = true;
                        ObjReassign.CreatedBy = ObjStudent.SchoolID;
                        ObjReassign.CreatedByType = (int)EnumsManagement.CREATEBYTYPE.School;
                        ObjReassign.CreatedDateTime = DateTime.Now;
                        DB.ISStudentReassignHistories.Add(ObjReassign);
                        DB.SaveChanges();
                    }
                    if (objStudentManagement.StudentClassAssignment(StudentID, ClassID))
                    {
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == ObjStudent.SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Student Class ReAssigned Successfully " + "Name : " + ObjStudent.StudentName + " Document Category : StudentClass", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Student");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Student Assignment Success";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Student Class Assignment";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Student Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetReceiverList()
        {
            try
            {
                int Type = (OperationManagement.GetPostValues("TYPE") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TYPE"));
                int SchoolID = (OperationManagement.GetPostValues("SCHOOLID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));
                int StudentID = (OperationManagement.GetPostValues("STUDENTID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID"));
                int ClassID = (OperationManagement.GetPostValues("CLASSID") == "") ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("CLASSID"));
                ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                if (Type != 0)
                {
                    if (Type == (int)EnumsManagement.MESSAGEUSERTYPE.Teacher)
                    {
                        if (ObjSchools != null && ObjSchools.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            TeacherManagement objTeacherManagement = new TeacherManagement();
                            if (ClassID < 1)
                            {
                                ClassID = 0;
                            }
                            List<MISTeacher> objList = objTeacherManagement.TeacherList(SchoolID, "", ClassID, "", "", "", 0, "1");
                            if (objList.Count > 0)
                            {
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "TeacherList Found Successfully";
                                ResponseObject.Data = objList.ToList();
                            }
                            else
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "No TeacherList Found";
                                ResponseObject.Data = "";
                            }
                        }
                        else
                        {

                            TeacherManagement objTeacherManagement = new TeacherManagement();

                            List<MISTeacher> objList = objTeacherManagement.TeacherList(SchoolID, "", 0, "", "", "", 0, "1");
                            if (objList.Count > 0)
                            {
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "TeacherList Found Successfully";
                                ResponseObject.Data = objList.ToList();
                            }
                            else
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "No TeacherList Found";
                                ResponseObject.Data = "";
                            }
                        }
                    }
                    else if (Type == (int)EnumsManagement.MESSAGEUSERTYPE.Parent)
                    {
                        StudentManagement objStudentManagement = new StudentManagement();
                        ParentManagement objParentManagement = new ParentManagement();
                        //List<MISStudent> objList = objParentManagement.ParentList(SchoolID);
                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                        List<MISStudent> objList = new List<MISStudent>();
                        if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            objList = objParentManagement.ParentList(SchoolID);
                        }
                        else
                        {
                            string dts = DateTime.Now.ToString("dd/MM/yyyy");
                            objList = objParentManagement.ParentList(SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                        }
                        if (ClassID < 1)
                        {
                            ClassID = 0;
                        }
                        else
                        {
                            ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);
                            if (ObjCl.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                            {
                                ClassManagement objClassManagement = new ClassManagement();
                                objList = objClassManagement.StudentListByOfficeClass(SchoolID);
                            }
                            else
                            {
                                objList = objList.Where(p => p.ClassID == ClassID).ToList();
                            }
                        }
                        if (objList.Count > 0)
                        {
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "ParentList Found Successfully";
                            ResponseObject.Data = objList.ToList();
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "No ParentList Found";
                            ResponseObject.Data = "";
                        }
                    }
                    else if (Type == (int)EnumsManagement.MESSAGEUSERTYPE.School)
                    {
                        SchoolManagement objSchoolManagement = new SchoolManagement();
                        List<MISSchool> objList = objSchoolManagement.SchoolList().Where(p => p.ID == SchoolID).ToList();
                        if (objList.Count > 0)
                        {
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "SchoolList Found Successfully";
                            ResponseObject.Data = objList.ToList();
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "No SchoolList Found";
                            ResponseObject.Data = "";
                        }
                    }
                    else if (Type == (int)EnumsManagement.MESSAGEUSERTYPE.NonTeacher)
                    {
                        if (ObjSchools != null && ObjSchools.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                        {
                            TeacherManagement objTeacherManagement = new TeacherManagement();
                            if (ClassID < 1)
                            {
                                ClassID = 0;
                            }
                            List<MISTeacher> objList = objTeacherManagement.NonTeacherList(SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                            if (ClassID != 0)
                            {
                                objList = objList.Where(p => p.ISTeacherClassAssignments.Any(m => m.ClassID == ClassID)).ToList();
                            }
                            if (objList.Count > 0)
                            {
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "NonTeacherList Found Successfully";
                                ResponseObject.Data = objList.ToList();
                            }
                            else
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "No NonTeacherList Found";
                                ResponseObject.Data = "";
                            }
                        }
                        else
                        {
                            TeacherManagement objTeacherManagement = new TeacherManagement();
                            List<MISTeacher> objList = objTeacherManagement.NonTeacherList(SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                            if (objList.Count > 0)
                            {
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "NonTeacherList Found Successfully";
                                ResponseObject.Data = objList.ToList();
                            }
                            else
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "No NonTeacherList Found";
                                ResponseObject.Data = "";
                            }
                        }
                    }
                    else
                    {
                        TeacherManagement objTeacherManagement = new TeacherManagement();
                        if (StudentID != 0)
                        {
                            int? ClasssID = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID).ClassID;
                            int ClassTypeID = DB.ISClasses.SingleOrDefault(p => p.ID == ClasssID).TypeID;
                            string Year = DB.ISClasses.SingleOrDefault(p => p.ID == ClasssID).Year;
                            List<MISTeacher> objList = objTeacherManagement.TeacherList(SchoolID, Year, 0, "", "", "", ClassTypeID, "0");
                            if (objList.Count > 0)
                            {
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "Teacher List Found Successfully";
                                ResponseObject.Data = objList.ToList();
                            }
                            else
                            {
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "No TeacherList Found";
                                ResponseObject.Data = "";
                            }
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "Please Pass Parameters";
                            ResponseObject.Data = "";
                        }
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Please Select Receiver Group Type";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendMessageToUser()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID"));
                int ReceiveGroupID = OperationManagement.GetPostValues("RECEIVERGROUPID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERGROUPID"));
                int ReceiveID = OperationManagement.GetPostValues("RECEIVERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERID"));
                string Subject = OperationManagement.GetPostValues("SUBJECT");
                string FileURL = OperationManagement.GetPostValues("ATTACHNMENT");
                string Desc = OperationManagement.GetPostValues("DESCRIPTION");

                if (TeacherID != 0)
                {
                    ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
                    string FileName = Server.MapPath(FileURL);
                    if (ReceiveGroupID == 1)
                    {
                        string Email = DB.ISSchools.SingleOrDefault(p => p.ID == ReceiveID).AdminEmail;
                        string Name = DB.ISSchools.SingleOrDefault(p => p.ID == ReceiveID).Name;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", Name, Subject, Desc, obj.Name, obj.ISSchool.Name);
                        objEmailManagement.SendEmails(Email, Subject, Message, FileName);
                    }
                    if (ReceiveGroupID == 3)
                    {
                        string Email = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiveID).ParantEmail1;
                        string Name = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiveID).StudentName;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", Name, Subject, Desc, obj.Name, obj.ISSchool.Name);
                        objEmailManagement.SendEmails(Email, Subject, Message, FileName);
                    }
                }
                else
                {
                    ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    string FileName = Server.MapPath(FileURL);
                    if (ReceiveGroupID == 2)
                    {
                        string Name = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiveID).Name;
                        string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiveID).Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", Name, Subject, Desc, obj.Name, obj.PhoneNumber, obj.AdminEmail);
                        objEmailManagement.SendEmail(Email, Subject, Message);
                    }
                    if (ReceiveGroupID == 3)
                    {
                        string Name = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiveID).StudentName;
                        string Email = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiveID).ParantEmail1;
                        EmailManagement objEmailManagement = new EmailManagement();
                        string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", Name, Subject, Desc, obj.Name, obj.PhoneNumber, obj.AdminEmail);
                        objEmailManagement.SendEmails(Email, Subject, Message, FileName);
                    }
                }


                if (SchoolID != 0)
                {
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Message Sent Successfully" + "Subject : " + Subject + " Document Category : Message", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Message");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Message Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Message Not Sent";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ReassignTeacher()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                int ClassID1 = OperationManagement.GetPostValues("CLASSID1") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID1")) : 0;
                int ClassID2 = OperationManagement.GetPostValues("CLASSID2") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID2")) : 0;
                int CLASSID3 = OperationManagement.GetPostValues("CLASSID3") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID3")) : 0;
                int CLASSID4 = OperationManagement.GetPostValues("CLASSID4") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID4")) : 0;
                int CLASSID5 = OperationManagement.GetPostValues("CLASSID5") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID5")) : 0;
                int TYPE = OperationManagement.GetPostValues("TYPE") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TYPE")) : 1;
                int TYPEID = OperationManagement.GetPostValues("TYPEID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TYPEID")) : 0;
                TeacherManagement objTeacherManagement = new TeacherManagement();
                if (TeacherID != 0)
                {
                    ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
                    //Remove Range from Teacher Class Assignment By Teacher ID
                    List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == TeacherID && p.Active == true).ToList();
                    if (objList.Count > 0)
                    {
                        if (ClassID1 != 0)
                        {
                            if (objList.Count >= 1)
                            {
                                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, objList[0].ClassID, ClassID1, TYPE, TYPEID);
                            }
                        }
                        if (ClassID2 != 0)
                        {
                            if (objList.Count > 1)
                            {
                                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, objList[1].ClassID, ClassID2, TYPE, TYPEID);
                            }
                        }
                        if (CLASSID3 != 0)
                        {
                            if (objList.Count > 2)
                            {
                                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, objList[2].ClassID, CLASSID3, TYPE, TYPEID);
                            }
                        }
                        if (CLASSID4 != 0)
                        {
                            if (objList.Count > 3)
                            {
                                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, objList[3].ClassID, CLASSID4, TYPE, TYPEID);
                            }
                        }
                        if (CLASSID5 != 0)
                        {
                            if (objList.Count > 4)
                            {
                                objTeacherManagement.TeacherReassignment(SchoolID, TeacherID, objList[4].ClassID, CLASSID5, TYPE, TYPEID);
                            }
                        }
                        DB.ISTeacherClassAssignments.RemoveRange(objList);
                        DB.SaveChanges();
                    }
                    //Add Teacher Class Assignment
                    if (ClassID1 != 0)
                    {
                        ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                        objClass1.ClassID = ClassID1;
                        objClass1.TeacherID = TeacherID;
                        objClass1.Active = true;
                        objClass1.Deleted = true;
                        objClass1.CreatedBy = TYPEID;
                        objClass1.CreatedDateTime = DateTime.Now;
                        objClass1.Out = 0;
                        objClass1.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass1);
                        DB.SaveChanges();
                    }
                    if (ClassID2 != 0)
                    {
                        ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                        objClass2.ClassID = ClassID2;
                        objClass2.TeacherID = TeacherID;
                        objClass2.Active = true;
                        objClass2.Deleted = true;
                        objClass2.CreatedBy = TYPEID;
                        objClass2.CreatedDateTime = DateTime.Now;
                        objClass2.Out = 0;
                        objClass2.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass2);
                        DB.SaveChanges();
                    }
                    if (CLASSID3 != 0)
                    {
                        ISTeacherClassAssignment objClass3 = new ISTeacherClassAssignment();
                        objClass3.ClassID = CLASSID3;
                        objClass3.TeacherID = TeacherID;
                        objClass3.Active = true;
                        objClass3.Deleted = true;
                        objClass3.CreatedBy = TYPEID;
                        objClass3.CreatedDateTime = DateTime.Now;
                        objClass3.Out = 0;
                        objClass3.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass3);
                        DB.SaveChanges();
                    }
                    if (CLASSID4 != 0)
                    {
                        ISTeacherClassAssignment objClass4 = new ISTeacherClassAssignment();
                        objClass4.ClassID = CLASSID4;
                        objClass4.TeacherID = TeacherID;
                        objClass4.Active = true;
                        objClass4.Deleted = true;
                        objClass4.CreatedBy = TYPEID;
                        objClass4.CreatedDateTime = DateTime.Now;
                        objClass4.Out = 0;
                        objClass4.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass4);
                        DB.SaveChanges();
                    }
                    if (CLASSID5 != 0)
                    {
                        ISTeacherClassAssignment objClass5 = new ISTeacherClassAssignment();
                        objClass5.ClassID = CLASSID5;
                        objClass5.TeacherID = TeacherID;
                        objClass5.Active = true;
                        objClass5.Deleted = true;
                        objClass5.CreatedBy = TYPEID;
                        objClass5.CreatedDateTime = DateTime.Now;
                        objClass5.Out = 0;
                        objClass5.Outbit = false;
                        DB.ISTeacherClassAssignments.Add(objClass5);
                        DB.SaveChanges();
                    }
                    if (TYPE == (int)EnumsManagement.CREATEBYTYPE.School)
                    {
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == TYPEID && p.Deleted == true);
                        LogManagement.AddLogs("Teacher ReAssign Successfully " + "Name : " + ObjTeacher.Name + " Document Category : TeacherReAssign", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Teacher");
                    }
                    else
                    {
                        ISTeacher ObjSchools = DB.ISTeachers.SingleOrDefault(p => p.ID == TYPEID && p.Deleted == true);
                        LogManagement.AddLogs("Teacher ReAssign Successfully " + "Name : " + ObjTeacher.Name + " Document Category : TeacherReAssign", ObjSchools.SchoolID, ObjSchools.ID, String.Format("{0}", ObjSchools.Name), "Teacher");
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Teacher ReAssign Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Teacher ReAssign";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void LogTypeList()
        {
            try
            {
                SupportManagement objSupportManagement = new SupportManagement();
                List<MISLogType> objList = objSupportManagement.LogTypeList();
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "LogTypes Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No LogTypes Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendSupport()
        {
            try
            {
                int SenderID = OperationManagement.GetPostValues("SENDERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SENDERID")) : 0;
                int SupportID = OperationManagement.GetPostValues("SUPPORTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SUPPORTID")) : 0;
                string Comments = OperationManagement.GetPostValues("COMMENTS");
                string FileURL = OperationManagement.GetPostValues("FILEURL");
                int UserTypeID = OperationManagement.GetPostValues("USERTYPEID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("USERTYPEID")) : 0;
                ISTicketMessage objTicketMessage = new ISTicketMessage();
                objTicketMessage.SupportID = SupportID;
                objTicketMessage.SenderID = SenderID;
                objTicketMessage.Message = Comments;
                objTicketMessage.SelectFile = "../Upload/" + FileURL;
                objTicketMessage.CreatedDatetime = DateTime.Now;
                objTicketMessage.UserTypeID = UserTypeID;
                DB.ISTicketMessages.Add(objTicketMessage);
                DB.SaveChanges();
                //AlertMessageManagement.MESSAGETYPE.Success
                if (objTicketMessage != null)
                {
                    if (UserTypeID == 1)
                    {
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SenderID && p.Deleted == true);
                        LogManagement.AddLogs("Support Sent Successfully " + "ID : " + SupportID + " Document Category : SendSupportViewed", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Support");
                    }
                    if (UserTypeID == 2)
                    {
                        ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == SenderID && p.Deleted == true);
                        LogManagement.AddLogs("Support Sent Successfully " + "ID : " + SupportID + " Document Category : SendSupportViewed", ObjTeacher.SchoolID, ObjTeacher.ID, ObjTeacher.Name, "Support");
                    }
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Support Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Support not Sent";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SchoolProfile()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                ProfileManagement objProfileManagement = new ProfileManagement();
                MISSchool objList = objProfileManagement.GetSchool(SchoolID);
                if (objList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "School Data Found Successfully";
                    ResponseObject.Data = objList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No School Data Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void UpdateSchoolProfile()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string OpeningTime = OperationManagement.GetPostValues("OPENINGTIME");
                string ClosingTime = OperationManagement.GetPostValues("CLOSINGTIME");
                string LateMinutesAfterClose = OperationManagement.GetPostValues("LATEMINUTESAFTERCLOSING");
                string ChargeMinutesAfterClose = OperationManagement.GetPostValues("CHARGEMINUTESAFTERCLOSING");
                string ReportableMinutesAfterClose = OperationManagement.GetPostValues("REPORTABLEMINUTESAFTERCLOSING");
                string NotificationConfirmPickUp = OperationManagement.GetPostValues("NOTIFICATIONCONFIRMPICKUP");
                string NotificationConfirmAttendence = OperationManagement.GetPostValues("NOTIFICATIONCONFIRMATTENDENCE");

                ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Active == true && p.Deleted == true);
                if (objSchool != null)
                {
                    if (OpeningTime.Contains(":"))
                    {
                        string[] arrDate = OpeningTime.Split(':');
                        string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
                        objSchool.OpningTime = DateTime.Parse(DTS);
                    }
                    if (ClosingTime.Contains(":"))
                    {
                        string[] arrDate = ClosingTime.Split(':');
                        string DTS = "01/01/2018 " + arrDate[0].ToString() + ":" + arrDate[1].ToString() + ":00.000";
                        objSchool.ClosingTime = DateTime.Parse(DTS);
                    }
                    objSchool.LateMinAfterClosing = LateMinutesAfterClose;
                    objSchool.ChargeMinutesAfterClosing = ChargeMinutesAfterClose;
                    objSchool.ReportableMinutesAfterClosing = ReportableMinutesAfterClose;
                    objSchool.NotificationAttendance = NotificationConfirmAttendence == "TRUE" ? true : false;
                    objSchool.isNotificationPickup = NotificationConfirmPickUp == "TRUE" ? true : false;
                    DB.SaveChanges();
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("School Profile Updated Successfully " + "Name : " + objSchool.Name + " Document Category : Profile", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Profile");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "School Data Updated Successfully";
                    ResponseObject.Data = "";

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "School Data not Updated";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void Attendence()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string Status = OperationManagement.GetPostValues("STATUS");
                string Date = OperationManagement.GetPostValues("DATE");
                bool ChkCompleteValue = OperationManagement.GetPostValues("CHKCOMPLETEVALUE") == "True" ? true : false;
                bool ChkValue = false;
                ISStudent ObjSt = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
                DateTime dt2 = new DateTime();
                if (Date != "")
                {
                    string dates = Date;
                    string Format = "";
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    }
                    else
                    {
                        Format = dates;
                    }
                    dt2 = Convert.ToDateTime(Format);
                }
                //ISAttendance objAttendence = new ISAttendance();

                if (Status == "Present")
                {
                    if (ObjSt.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                    {
                        var list = DB.ISAttendances.Where(p => p.ISStudent.SchoolID == ObjSt.ISSchool.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.Active == true && p.Deleted == true).ToList();
                        if (list.Count() > 0)
                        {
                            ISAttendance attendance = list.OrderByDescending(p => p.ID).FirstOrDefault();
                            ISStudent student = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                            if (attendance.ISStudent.ClassID == student.ClassID)
                            {
                                ChkValue = true;
                                ISAttendance objAttendence = new ISAttendance();
                                ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                if (objAtte != null)
                                {
                                    if (objAtte.AttendenceComplete == true)
                                        objAtte.Status = "Present(Late)";
                                    else
                                        objAtte.Status = "Present";
                                    objAtte.ModifyBy = TeacherID;
                                    objAtte.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();
                                    ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                    if (objs != null)
                                    {
                                        objs.PickStatus = "Not Marked";
                                        DB.SaveChanges();
                                    }
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Attendence Updated Successfully";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                                else
                                {
                                    objAttendence.Date = dt2;
                                    objAttendence.StudentID = ID;
                                    objAttendence.TeacherID = TeacherID;
                                    objAttendence.Status = "Present";
                                    string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                    objAttendence.Time = DateTime.Parse(DTS);
                                    objAttendence.Active = true;
                                    objAttendence.Deleted = true;
                                    objAttendence.CreatedBy = TeacherID;
                                    objAttendence.CreatedDateTime = DateTime.Now;
                                    DB.ISAttendances.Add(objAttendence);
                                    DB.SaveChanges();

                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = DateTime.Now;
                                        pickup.PickDate = DateTime.Now;
                                        pickup.PickStatus = "Not Marked";
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = ID;
                                        obj.ClassID = ObjSt.ClassID;
                                        obj.TeacherID = TeacherID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Not Marked";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Attendence FillUp Successfully";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                            }
                            else
                            {
                                ChkValue = false;
                                ResponseObject.Status = "F";
                                ResponseObject.Message = "Attendence already filled up in other class";
                                ResponseObject.Data = "";
                                ResponseObject.Data1 = ChkValue;
                            }
                        }
                        else
                        {
                            ChkValue = true;
                            ISAttendance objAttendence = new ISAttendance();
                            ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                            if (objAtte != null)
                            {
                                if (objAtte.AttendenceComplete == true)
                                    objAtte.Status = "Present(Late)";
                                else
                                    objAtte.Status = "Present";
                                objAtte.ModifyBy = TeacherID;
                                objAtte.ModifyDateTime = DateTime.Now;
                                DB.SaveChanges();
                                ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                if (objs != null)
                                {
                                    objs.PickStatus = "Not Marked";
                                    DB.SaveChanges();
                                }
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "Attendence Updated Successfully";
                                ResponseObject.Data = "";
                                ResponseObject.Data1 = ChkValue;
                            }
                            else
                            {
                                objAttendence.Date = dt2;
                                objAttendence.StudentID = ID;
                                objAttendence.TeacherID = TeacherID;
                                objAttendence.Status = "Present";
                                string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                objAttendence.Time = DateTime.Parse(DTS);
                                objAttendence.Active = true;
                                objAttendence.Deleted = true;
                                objAttendence.CreatedBy = TeacherID;
                                objAttendence.CreatedDateTime = DateTime.Now;
                                DB.ISAttendances.Add(objAttendence);
                                DB.SaveChanges();

                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                if (pickup != null)
                                {
                                    pickup.PickTime = DateTime.Now;
                                    pickup.PickDate = DateTime.Now;
                                    pickup.PickStatus = "Not Marked";
                                    DB.SaveChanges();
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = ID;
                                    obj.ClassID = ObjSt.ClassID;
                                    obj.TeacherID = TeacherID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Not Marked";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "Attendence FillUp Successfully";
                                ResponseObject.Data = "";
                                ResponseObject.Data1 = ChkValue;
                            }
                        }
                    }
                    else
                    {

                        ISAttendance objAttendence = new ISAttendance();
                        ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                        if (objAtte != null)
                        {
                            ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                            if (objs != null)
                            {
                                if (!objs.PickStatus.Contains("After-School"))
                                {
                                    objs.PickStatus = "Not Marked";
                                    DB.SaveChanges();

                                    if (objAtte.AttendenceComplete == true)
                                        objAtte.Status = "Present(Late)";
                                    else
                                        objAtte.Status = "Present";
                                    objAtte.ModifyBy = TeacherID;
                                    objAtte.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();
                                    ChkValue = true;
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Attendence Updated Successfully";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                                else
                                {
                                    ChkValue = false;
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Student already sent to AfterSchool";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                            }
                        }
                        else
                        {
                            ChkValue = true;

                            objAttendence.Date = dt2;
                            objAttendence.StudentID = ID;
                            objAttendence.TeacherID = TeacherID;
                            objAttendence.Status = "Present";
                            string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                            objAttendence.Time = DateTime.Parse(DTS);
                            objAttendence.Active = true;
                            objAttendence.Deleted = true;
                            objAttendence.CreatedBy = TeacherID;
                            objAttendence.CreatedDateTime = DateTime.Now;
                            DB.ISAttendances.Add(objAttendence);
                            DB.SaveChanges();

                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                            if (pickup != null)
                            {
                                pickup.PickTime = DateTime.Now;
                                pickup.PickDate = DateTime.Now;
                                pickup.PickStatus = "Not Marked";
                                DB.SaveChanges();
                            }
                            else
                            {
                                ISPickup obj = new ISPickup();
                                obj.StudentID = ID;
                                obj.ClassID = ObjSt.ClassID;
                                obj.TeacherID = TeacherID;
                                obj.PickTime = DateTime.Now;
                                obj.PickDate = DateTime.Now;
                                obj.PickStatus = "Not Marked";
                                DB.ISPickups.Add(obj);
                                DB.SaveChanges();
                            }
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Attendence FillUp Successfully";
                            ResponseObject.Data = "";
                            ResponseObject.Data1 = ChkValue;
                        }
                    }
                }
                else
                {
                    List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == ObjSt.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    if (ObjHoliday.Where(x => x.DateFrom.Value <= dt2 && x.DateTo.Value >= dt2 && x.Active == true).Count() > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Mark as Absent Not Allowed.";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        if (ObjSt.ISSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                        {
                            var list = DB.ISAttendances.Where(p => p.ISStudent.SchoolID == ObjSt.ISSchool.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.Active == true && p.Deleted == true);
                            if (list.Count() > 0)
                            {
                                ISAttendance attendance = list.OrderByDescending(p => p.ID).FirstOrDefault();
                                ISStudent student = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                                if (attendance.ISStudent.ClassID == student.ClassID)
                                {
                                    ChkValue = true;
                                    ISAttendance objAttendence = new ISAttendance();
                                    ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                    if (objAtte != null)
                                    {
                                        if (ChkCompleteValue == true)
                                        {
                                            ChkValue = false;
                                            ResponseObject.Status = "F";
                                            ResponseObject.Message = "Attendance cannot be changed from Present to Absent after Complete Attendance is activated";
                                            ResponseObject.Data = "";
                                            ResponseObject.Data1 = ChkValue;
                                        }
                                        else
                                        {
                                            objAtte.Status = "Absent";
                                            objAtte.ModifyBy = TeacherID;
                                            objAtte.ModifyDateTime = DateTime.Now;
                                            DB.SaveChanges();
                                            ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                            if (objs != null)
                                            {
                                                objs.PickStatus = "Mark as Absent";
                                                DB.SaveChanges();
                                            }
                                            ChkValue = true;
                                            ResponseObject.Status = "S";
                                            ResponseObject.Message = "Attendence Updated Successfully";
                                            ResponseObject.Data = "";
                                            ResponseObject.Data1 = ChkValue;
                                        }
                                    }
                                    else
                                    {
                                        objAttendence.Date = dt2;
                                        objAttendence.StudentID = ID;
                                        objAttendence.TeacherID = TeacherID;
                                        objAttendence.Status = "Absent";
                                        string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                        objAttendence.Time = DateTime.Parse(DTS);
                                        objAttendence.Active = true;
                                        objAttendence.Deleted = true;
                                        objAttendence.CreatedBy = TeacherID;
                                        objAttendence.CreatedDateTime = DateTime.Now;
                                        DB.ISAttendances.Add(objAttendence);
                                        DB.SaveChanges();

                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (pickup != null)
                                        {
                                            pickup.PickTime = DateTime.Now;
                                            pickup.PickDate = DateTime.Now;
                                            pickup.PickStatus = "Mark as Absent";
                                            DB.SaveChanges();
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = ID;
                                            obj.ClassID = ObjSt.ClassID;
                                            obj.TeacherID = TeacherID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            obj.PickStatus = "Mark as Absent";
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        ChkValue = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Attendence FillUp Successfully";
                                        ResponseObject.Data = "";
                                        ResponseObject.Data1 = ChkValue;
                                    }
                                }
                                else
                                {
                                    ChkValue = false;
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Attendence already filled up in other class";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                            }
                            else
                            {
                                ChkValue = true;
                                ISAttendance objAttendence = new ISAttendance();
                                ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                                if (objAtte != null)
                                {
                                    objAtte.Status = "Absent";
                                    objAtte.ModifyBy = TeacherID;
                                    objAtte.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();
                                    ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                    if (objs != null)
                                    {
                                        objs.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();
                                    }
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Attendence Updated Successfully";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                                else
                                {
                                    objAttendence.Date = dt2;
                                    objAttendence.StudentID = ID;
                                    objAttendence.TeacherID = TeacherID;
                                    objAttendence.Status = "Absent";
                                    string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                    objAttendence.Time = DateTime.Parse(DTS);
                                    objAttendence.Active = true;
                                    objAttendence.Deleted = true;
                                    objAttendence.CreatedBy = TeacherID;
                                    objAttendence.CreatedDateTime = DateTime.Now;
                                    DB.ISAttendances.Add(objAttendence);
                                    DB.SaveChanges();

                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (pickup != null)
                                    {
                                        pickup.PickTime = DateTime.Now;
                                        pickup.PickDate = DateTime.Now;
                                        pickup.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = ID;
                                        obj.ClassID = ObjSt.ClassID;
                                        obj.TeacherID = TeacherID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    ResponseObject.Status = "S";
                                    ResponseObject.Message = "Attendence FillUp Successfully";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                            }
                        }
                        else
                        {

                            ISAttendance objAttendence = new ISAttendance();
                            ISAttendance objAtte = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt2) && p.StudentID == ID && p.Active == true && p.Deleted == true);
                            if (objAtte != null)
                            {
                                if (ChkCompleteValue == true)
                                {
                                    ChkValue = false;
                                    ResponseObject.Status = "F";
                                    ResponseObject.Message = "Attendance cannot be changed from Present to Absent after Complete Attendance is activated";
                                    ResponseObject.Data = "";
                                    ResponseObject.Data1 = ChkValue;
                                }
                                else
                                {
                                    ISPickup objs = DB.ISPickups.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt2));
                                    if (!objs.PickStatus.Contains("After-School"))
                                    {
                                        objs.PickStatus = "Mark as Absent";
                                        DB.SaveChanges();

                                        objAtte.Status = "Absent";
                                        objAtte.ModifyBy = TeacherID;
                                        objAtte.ModifyDateTime = DateTime.Now;
                                        DB.SaveChanges();
                                        ChkValue = true;
                                        ResponseObject.Status = "S";
                                        ResponseObject.Message = "Attendence Updated Successfully";
                                        ResponseObject.Data = "";
                                        ResponseObject.Data1 = ChkValue;
                                    }
                                    else
                                    {
                                        ChkValue = false;
                                        ResponseObject.Status = "F";
                                        ResponseObject.Message = "Student already sent to AfterSchool";
                                        ResponseObject.Data = "";
                                        ResponseObject.Data1 = ChkValue;
                                    }
                                }
                            }
                            else
                            {
                                ChkValue = true;

                                objAttendence.Date = dt2;
                                objAttendence.StudentID = ID;
                                objAttendence.TeacherID = TeacherID;
                                objAttendence.Status = "Absent";
                                string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                objAttendence.Time = DateTime.Parse(DTS);
                                objAttendence.Active = true;
                                objAttendence.Deleted = true;
                                objAttendence.CreatedBy = TeacherID;
                                objAttendence.CreatedDateTime = DateTime.Now;
                                DB.ISAttendances.Add(objAttendence);
                                DB.SaveChanges();

                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                if (pickup != null)
                                {
                                    pickup.PickTime = DateTime.Now;
                                    pickup.PickDate = DateTime.Now;
                                    pickup.PickStatus = "Mark as Absent";
                                    DB.SaveChanges();
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = ID;
                                    obj.ClassID = ObjSt.ClassID;
                                    obj.TeacherID = TeacherID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Mark as Absent";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                ResponseObject.Status = "S";
                                ResponseObject.Message = "Attendence FillUp Successfully";
                                ResponseObject.Data = "";
                                ResponseObject.Data1 = ChkValue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendAttendenceStatus()
        {
            try
            {
                EmailManagement objEmailManagement = new EmailManagement();
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string Date = OperationManagement.GetPostValues("DATE");
                string dates = Date;
                string Format = "";
                if (dates.Contains("/"))
                {
                    string[] arrDate = dates.Split('/');
                    Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                }
                else
                {
                    Format = dates;
                }
                DateTime dt1 = Convert.ToDateTime(Format);
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true && p.Active == true);
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
                ISAttendance objAttendance = DB.ISAttendances.SingleOrDefault(p => p.Date == dt1 && p.StudentID == StudentID && p.Deleted == true && p.Active == true);
                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parents,<br><br> Your Child {0} is {1} on {2}.<br /><br>Thanks,<br>School APP<br> Teacher Name : {3}<br>Mobile No : {4} <br>Email id : {5}<br> School : {6}", objStudent.StudentName, objAttendance.Status, dt1.ToString("dd/MM/yyyy"), objTeacher.Name, objTeacher.PhoneNo, objTeacher.Email, objTeacher.ISSchool.Name);
                objEmailManagement.SendEmail(objStudent.ParantEmail1, "Your Child Attendance Report", Message);

                if (objAttendance != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Status Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Status Not Sent";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AttendenceListByClass()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                string Date = OperationManagement.GetPostValues("DATE");
                int StudentID = OperationManagement.GetPostValues("STUDENTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STUDENTID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string Status = OperationManagement.GetPostValues("STATUS");
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                string SortBy = OperationManagement.GetPostValues("SORTBY");
                bool ChkValue = false;
                StudentManagement objStudentManagement = new StudentManagement();
                PickupManagement ObjPickupManagement = new PickupManagement();
                List<MISAttendance> objList = new List<MISAttendance>();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objStudentManagement.AttendenceList(SchoolID, ClassID, Date, StudentID, TeacherID, Status, OrderBy, SortBy);
                    ChkValue = ObjPickupManagement.BindAttendenceCheckBox(SchoolID, ClassID, objList);
                }
                else
                {
                    objList = objStudentManagement.AttendenceLists(SchoolID, ClassID, Date, StudentID, TeacherID, Status, OrderBy, SortBy);
                    ChkValue = ObjPickupManagement.BindAttendenceCheckBox(SchoolID, ClassID, objList);
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Attendence List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                    ResponseObject.Data1 = ChkValue;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Attendence List Found";
                    ResponseObject.Data = "";
                    ResponseObject.Data1 = ChkValue;
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TeacherChangePassword()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int TeacherID = OperationManagement.GetPostValues("TEACHERID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("TEACHERID")) : 0;
                string Email = OperationManagement.GetPostValues("EMAIL");
                string Password = OperationManagement.GetPostValues("PASSWORD");
                if (TeacherID != 0)
                {
                    IList<ISTeacher> obj2 = DB.ISTeachers.Where(a => a.ID != TeacherID && a.Email == Email && a.Active == true && a.Deleted == true).ToList();
                    if (obj2.Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "EmailID already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == TeacherID && p.Deleted == true);
                        if (objTeacher != null)
                        {
                            objTeacher.Password = EncryptionHelper.Encrypt(Password);
                            objTeacher.ModifyBy = SchoolID;
                            objTeacher.ModifyDateTime = DateTime.Now;
                            DB.SaveChanges();
                            TeacherManagement objTeacherManagement = new TeacherManagement();
                            MISTeacher ObjList = objTeacherManagement.GetNonTeacher(objTeacher.ID);

                            ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                            LogManagement.AddLogs("Teacher Password Updated Successfully " + "Name : " + objTeacher.Name + " Document Category : TeacherProfile", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Teacher");
                            ResponseObject.Status = "S";
                            ResponseObject.Message = "Password Updated Successfully";
                            ResponseObject.Data = ObjList;
                        }
                        else
                        {
                            ResponseObject.Status = "F";
                            ResponseObject.Message = "Password not Updated";
                            ResponseObject.Data = "";
                        }
                    }
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Please Pass TeacherID";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void TicketMessageDetialsBySupport()
        {
            try
            {
                int SupportID = OperationManagement.GetPostValues("SUPPORTID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SUPPORTID")) : 0;
                SupportManagement objSupportManagement = new SupportManagement();
                MISSupport objMSupport = objSupportManagement.GetSupport(SupportID);
                List<MISTicketMessage> objMessageList = new List<MISTicketMessage>();

                if (objMSupport != null)
                {

                    foreach (ISTicketMessage item in DB.ISTicketMessages.Where(p => p.SupportID == objMSupport.ID))
                    {
                        ISSchool objSchool = new ISSchool();
                        ISTeacher ObjTeacher = new ISTeacher();
                        if (item.UserTypeID == 1)
                        {
                            objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == item.SenderID);
                        }
                        if (item.UserTypeID == 2)
                        {
                            ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == item.SenderID);
                        }
                        //ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.ID == item.SenderID);
                        MISTicketMessage objM = new MISTicketMessage();
                        objM.CreatedDate = item.CreatedDatetime;
                        objM.CreateDate = item.CreatedDatetime != null ? item.CreatedDatetime.Value.ToString("dd/MM/yyyy hh:mm tt") : "";
                        objM.Messages = item.Message;
                        objM.SName = item.UserTypeID == 1 ? objSchool.AdminFirstName + " " + objSchool.AdminLastName + "(" + objSchool.Name + ")" : item.UserTypeID == 2 ? ObjTeacher.Name + "(" + ObjTeacher.ISSchool.Name + ")" : "Support";
                        objM.FileName = item.SelectFile;
                        objMessageList.Add(objM);
                    }
                    if (objMessageList.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Ticket Messages Found Successfully";
                        ResponseObject.Data = objMessageList.OrderByDescending(p => p.CreatedDate).ToList();
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Ticket Message Found";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SchoolOrganizationList()
        {
            try
            {
                SchoolManagement objSchoolManagement = new SchoolManagement();
                List<MISSchool> objList = objSchoolManagement.SchoolList().Where(p => p.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool).ToList();
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "School List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No School List Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetReassignClasses()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> ObjList = objClassManagement.ClassList(SchoolID, OperationManagement.GetPostValues("YEAR"), 0).Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Office && p.TypeID != (int)EnumsManagement.CLASSTYPE.AfterSchool).ToList();

                if (ObjList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Class Found Successfully";
                    ResponseObject.Data = ObjList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Class Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetRoleTypeList()
        {
            try
            {
                List<MISRoleType> people = new List<MISRoleType>{
                   new MISRoleType{ID = 1, RoleType = "Teaching Role"},
                   new MISRoleType{ID = 2, RoleType = "Non Teaching Role"}
                   };


                if (people.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Role Types Found Successfully";
                    ResponseObject.Data = people;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Role Type Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetClassTypes()
        {
            try
            {
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClassType> objList = (from item in DB.ISClassTypes.Where(p => p.Deleted == true && p.ID != (int)EnumsManagement.CLASSTYPE.Office).ToList()
                                              select new MISClassType
                                              {
                                                  ID = item.ID,
                                                  Name = item.Name,
                                                  Active = item.Active,
                                                  Deleted = item.Deleted,
                                                  CreatedBy = item.CreatedBy,
                                                  CreatedDateTime = item.CreatedDateTime,
                                                  ModifyBy = item.ModifyBy,
                                                  ModifyDateTime = item.ModifyDateTime,
                                                  DeletedBy = item.DeletedBy,
                                                  DeletedDateTime = item.DeletedDateTime,
                                              }).ToList();
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "ClassTypes Found Successfully";
                    ResponseObject.Data = objList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No ClassType Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetSupportNo()
        {
            try
            {
                string SupportNumber = CommonOperation.GenerateSequenceNumber();
                if (SupportNumber != "")
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Support Number Generated Successfully";
                    ResponseObject.Data = SupportNumber;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Support Number not Generated";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetNonTeachingRoles()
        {
            try
            {
                int SchoolID = Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));

                RolesManagement objRolesManagement = new RolesManagement();
                var ObjList = objRolesManagement.AdminRoleList(SchoolID).Where(p => p.RoleType == (int)EnumsManagement.ROLETYPE.NONTEACHING && p.Active == true);
                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Roles Found";
                    ResponseObject.Data = ObjList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "no Roles Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void NonTeacherList()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string TeacherName = OperationManagement.GetPostValues("NONTEACHERNAME");
                int Status = OperationManagement.GetPostValues("STATUS") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("STATUS")) : 1;
                string OrderBy = OperationManagement.GetPostValues("ORDERBY");
                string SortBy = OperationManagement.GetPostValues("SORTBY");


                DB.Configuration.ProxyCreationEnabled = false;

                TeacherManagement objTeacherManagement = new TeacherManagement();
                List<MISTeacher> objList = objTeacherManagement.NonTeacherList(SchoolID, TeacherName, OrderBy, SortBy);
                if (Status != 0)
                {
                    if (Status == 1)
                    {
                        objList = objList.Where(p => p.Active == true).ToList();
                    }
                    else
                    {
                        objList = objList.Where(p => p.Active == false).ToList();
                    }
                }

                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "NonTeacher List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "NonTeacher List not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AddUpdateNonTeacher()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ROLEID = OperationManagement.GetPostValues("ROLEID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ROLEID")) : 0;
                int ClassID1 = OperationManagement.GetPostValues("CLASSID1") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID1")) : 0;
                int ClassID2 = OperationManagement.GetPostValues("CLASSID2") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID2")) : 0;
                string TeacherNo = OperationManagement.GetPostValues("TEACHERNO");
                string TeacherTitle = OperationManagement.GetPostValues("TEACHERTITLE");
                string TeacherName = OperationManagement.GetPostValues("TEACHERNAME");
                string Phone = OperationManagement.GetPostValues("PHONE");
                string Email = OperationManagement.GetPostValues("EMAIL");
                string EndDate = OperationManagement.GetPostValues("ENDDATE");
                string Photo = OperationManagement.GetPostValues("PHOTO");
                EmailManagement objEmailManagement = new EmailManagement();
                ISTeacher objTeacher = new ISTeacher();
                if (ID != 0)
                {
                    if (DB.ISTeachers.Where(a => a.ID != ID && a.Email == Email && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Email already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.ID != ID && a.PhoneNo == Phone && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Phone Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.ID != ID && a.TeacherNo == TeacherNo && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This NonTeacher Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
                        objTeacher.SchoolID = SchoolID;
                        objTeacher.ClassID = ClassID1 != 0 ? ClassID1 : (int?)null;
                        objTeacher.RoleID = ROLEID;
                        objTeacher.TeacherNo = TeacherNo;
                        objTeacher.Title = TeacherTitle;
                        objTeacher.Name = TeacherName;
                        objTeacher.PhoneNo = Phone;
                        objTeacher.Email = Email;
                        if (EndDate != "")
                        {
                            string dates = EndDate;
                            string Format = "";
                            if (dates.Contains("/"))
                            {
                                string[] arrDate = dates.Split('/');
                                Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                            }
                            else
                            {
                                Format = dates;
                            }
                            DateTime dt2 = Convert.ToDateTime(Format);
                            objTeacher.EndDate = dt2.Date;
                        }
                        else
                        {
                            objTeacher.EndDate = (DateTime?)null;
                        }
                        if (!String.IsNullOrEmpty(Photo))
                        {
                            objTeacher.Photo = Photo;
                        }
                        //objTeacher.Photo = (!String.IsNullOrEmpty(Photo)) ? Photo : "Upload/user.jpg"; //Photo !="" ? Photo : "Upload/user.jpg";
                        objTeacher.ModifyBy = SchoolID;
                        objTeacher.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();
                        //Remove Range from Teacher Class Assignment By Teacher ID
                        List<ISTeacherClassAssignment> objList = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList();
                        DB.ISTeacherClassAssignments.RemoveRange(objList);
                        DB.SaveChanges();
                        //Add Teacher Class Assignment
                        if (ClassID1 != 0)
                        {
                            ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                            objClass1.ClassID = ClassID1;
                            objClass1.TeacherID = ID;
                            objClass1.Active = true;
                            objClass1.Deleted = true;
                            objClass1.CreatedBy = SchoolID;
                            objClass1.CreatedDateTime = DateTime.Now;
                            objClass1.Out = 0;
                            objClass1.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass1);
                            DB.SaveChanges();
                        }
                        if (ClassID2 != 0)
                        {
                            ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                            objClass2.ClassID = ClassID2;
                            objClass2.TeacherID = ID;
                            objClass2.Active = true;
                            objClass2.Deleted = true;
                            objClass2.CreatedBy = SchoolID;
                            objClass2.CreatedDateTime = DateTime.Now;
                            objClass2.Out = 0;
                            objClass2.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass2);
                            DB.SaveChanges();
                        }
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("NonTeacher Updated Successfully " + "Name : " + objTeacher.Name + " Document Category : NonTeacherProfile", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "NonTeacher");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Non Teacher Updated Successfully";
                        ResponseObject.Data = "";
                    }
                }
                else
                {
                    if (DB.ISTeachers.Where(a => a.Email == Email && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Email already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.PhoneNo == Phone && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This Phone Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else if (DB.ISTeachers.Where(a => a.TeacherNo == TeacherNo && a.SchoolID == SchoolID && a.Active == true && a.Deleted == true).ToList().Count > 0)
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "This NonTeacher Number already Exist";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        objTeacher = new ISTeacher();
                        objTeacher.SchoolID = SchoolID;
                        objTeacher.ClassID = ClassID1 != 0 ? ClassID1 : (int?)null;
                        objTeacher.RoleID = ROLEID;
                        objTeacher.Role = (int)EnumsManagement.ROLETYPE.NONTEACHING;
                        objTeacher.TeacherNo = TeacherNo;
                        objTeacher.Title = TeacherTitle;
                        objTeacher.Name = TeacherName;
                        objTeacher.PhoneNo = Phone;
                        objTeacher.Email = Email;
                        string Password = CommonOperation.GenerateNewRandom();
                        objTeacher.Password = EncryptionHelper.Encrypt(Password);
                        if (EndDate != "")
                        {
                            string dates = EndDate;
                            string Format = "";
                            if (dates.Contains("/"))
                            {
                                string[] arrDate = dates.Split('/');
                                Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                            }
                            else
                            {
                                Format = dates;
                            }
                            DateTime dt2 = Convert.ToDateTime(Format);
                            objTeacher.EndDate = Convert.ToDateTime(dt2);
                        }
                        if (!String.IsNullOrEmpty(Photo))
                        {
                            objTeacher.Photo = Photo;
                        }
                        else
                        {
                            objTeacher.Photo = "Upload/user.jpg";
                        }
                        objTeacher.Active = true;
                        objTeacher.Deleted = true;
                        objTeacher.CreatedBy = SchoolID;
                        objTeacher.CreatedDateTime = DateTime.Now;
                        DB.ISTeachers.Add(objTeacher);
                        DB.SaveChanges();
                        if (ClassID1 != 0)
                        {
                            ISTeacherClassAssignment objClass1 = new ISTeacherClassAssignment();
                            objClass1.ClassID = ClassID1;
                            objClass1.TeacherID = objTeacher.ID;
                            objClass1.Active = true;
                            objClass1.Deleted = true;
                            objClass1.CreatedBy = SchoolID;
                            objClass1.CreatedDateTime = DateTime.Now;
                            objClass1.Out = 0;
                            objClass1.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass1);
                            DB.SaveChanges();
                        }
                        if (ClassID2 != 0)
                        {
                            ISTeacherClassAssignment objClass2 = new ISTeacherClassAssignment();
                            objClass2.ClassID = ClassID2;
                            objClass2.TeacherID = objTeacher.ID;
                            objClass2.Active = true;
                            objClass2.Deleted = true;
                            objClass2.CreatedBy = SchoolID;
                            objClass2.CreatedDateTime = DateTime.Now;
                            objClass2.Out = 0;
                            objClass2.Outbit = false;
                            DB.ISTeacherClassAssignments.Add(objClass2);
                            DB.SaveChanges();
                        }
                        if (Email != "")
                        {
                            try
                            {
                                var obj = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                                string Messages = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Email id : {1}<br><br>Password &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", TeacherName, Email, Password, obj.Name, obj.PhoneNumber, obj.AdminEmail);
                                objEmailManagement.SendEmail(Email, "Your Account Credentials", Messages);
                            }
                            catch (Exception ex)
                            {
                                ErrorLogManagement.AddLog(ex);
                            }
                        }
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("Non Teacher Created Successfully " + "Name : " + objTeacher.Name + " Document Category : NonTeacher", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "NonTeacher");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Non Teacher Created Successfully";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetNonTeacherByID()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                TeacherManagement objTeacherManagement = new TeacherManagement();
                MISTeacher ObjList = objTeacherManagement.GetNonTeacher(ID);
                if (ObjList != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Non Teacher Found Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Non Teacher Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void HolidayList()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string HolidayName = OperationManagement.GetPostValues("NAME") != "" ? OperationManagement.GetPostValues("NAME") : "";
                string DateFrom = OperationManagement.GetPostValues("FROMDATE") != "" ? OperationManagement.GetPostValues("FROMDATE") : "";
                string DateTo = OperationManagement.GetPostValues("TODATE") != "" ? OperationManagement.GetPostValues("TODATE") : "";
                string Status = OperationManagement.GetPostValues("STATUS") != "" ? OperationManagement.GetPostValues("STATUS") : "";
                DB.Configuration.ProxyCreationEnabled = false;

                List<MISHoliday> objList = new List<MISHoliday>();
                objList = (from Obj in DB.ISHolidays.Where(p => p.SchoolID == SchoolID && p.Deleted == true).ToList()
                           select new MISHoliday
                           {
                               ID = Obj.ID,
                               Name = Obj.Name,
                               DateFrom = Obj.DateFrom,
                               DateTo = Obj.DateTo,
                               FromDate = Obj.DateFrom.Value.ToString("dd/MM/yyyy"),
                               ToDate = Obj.DateTo.Value.ToString("dd/MM/yyyy"),
                               ActiveStatus = Obj.Active == true ? "Active" : "InActive",
                               Active = Obj.Active
                           }).ToList();

                if (HolidayName != "")
                {
                    objList = objList.Where(p => p.Name.Contains(HolidayName)).ToList();
                }
                if (DateFrom != "")
                {
                    DateTime dt = DateTime.Now;
                    string dates = DateFrom;
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        dt = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                    }
                    else
                    {
                        dt = Convert.ToDateTime(dates);
                    }
                    objList = objList.Where(p => p.DateFrom >= dt).ToList();
                }
                if (DateTo != "")
                {
                    DateTime dt = DateTime.Now;
                    string dates = DateTo;
                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        dt = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                    }
                    else
                    {
                        dt = Convert.ToDateTime(dates);
                    }
                    objList = objList.Where(p => p.DateTo <= dt).ToList();
                }
                if (Status != "0")
                {
                    if (Status == "1")
                    {
                        objList = objList.Where(p => p.Active == true).ToList();
                    }
                    else
                    {
                        objList = objList.Where(p => p.Active == false).ToList();
                    }
                }
                if (objList.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Holiday List Found Successfully";
                    ResponseObject.Data = objList.ToList();
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Holiday List not Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void AddUpdateHoliday()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string Name = OperationManagement.GetPostValues("HOLIDAYNAME");
                string FromDate = OperationManagement.GetPostValues("DATEFROM");
                string ToDate = OperationManagement.GetPostValues("DATETO");
                string Active = OperationManagement.GetPostValues("ACTIVE");
                ISHoliday objHoliday = new ISHoliday();
                if (ID != 0)
                {
                    objHoliday = DB.ISHolidays.SingleOrDefault(p => p.ID == ID);
                    objHoliday.SchoolID = SchoolID;
                    objHoliday.Name = Name;
                    DateTime dt2 = DateTime.Now;
                    if (FromDate != "")
                    {
                        string dates = FromDate;
                        if (dates.Contains("/"))
                        {
                            string[] arrDate = dates.Split('/');
                            dt2 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                            //Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                        }
                        else
                        {
                            dt2 = Convert.ToDateTime(dates);
                        }

                        objHoliday.DateFrom = dt2;
                    }
                    DateTime dt3 = DateTime.Now;
                    if (ToDate != "")
                    {
                        string dates = ToDate;
                        if (dates.Contains("/"))
                        {
                            string[] arrDate = dates.Split('/');
                            dt3 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                            //Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                        }
                        else
                        {
                            dt3 = Convert.ToDateTime(dates);
                        }

                        objHoliday.DateTo = dt3;
                    }
                    objHoliday.Active = Active == "True" ? true : false;
                    objHoliday.ModifyBy = SchoolID;
                    objHoliday.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Holiday Updated Successfully " + "Name : " + objHoliday.Name + " Document Category : HolidayAdd", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Holiday");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Holiday Updated Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    objHoliday = new ISHoliday();
                    objHoliday.SchoolID = SchoolID;
                    objHoliday.Name = Name;
                    DateTime dt2 = DateTime.Now;
                    if (FromDate != "")
                    {
                        string dates = FromDate;
                        if (dates.Contains("/"))
                        {
                            string[] arrDate = dates.Split('/');
                            dt2 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                            //Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                        }
                        else
                        {
                            dt2 = Convert.ToDateTime(dates);
                        }

                        objHoliday.DateFrom = dt2;
                    }
                    DateTime dt3 = DateTime.Now;
                    if (ToDate != "")
                    {
                        string dates = ToDate;
                        if (dates.Contains("/"))
                        {
                            string[] arrDate = dates.Split('/');
                            dt3 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                            //Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                        }
                        else
                        {
                            dt3 = Convert.ToDateTime(dates);
                        }

                        objHoliday.DateTo = dt3;
                    }
                    objHoliday.Active = true;
                    objHoliday.Deleted = true;
                    objHoliday.CreatedBy = SchoolID;
                    objHoliday.CreatedDateTime = DateTime.Now;
                    DB.ISHolidays.Add(objHoliday);
                    DB.SaveChanges();
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Holiday Created Successfully " + "Name : " + objHoliday.Name + " Document Category : HolidayAdd", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Holiday");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Holiday Created Successfully";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendMsgToTeacherStudent()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID"));
                int ReceiveGroupID = OperationManagement.GetPostValues("RECEIVERGROUPID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERGROUPID"));
                int ReceiverID = OperationManagement.GetPostValues("RECEIVERID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("RECEIVERID"));
                int ClassIDs = OperationManagement.GetPostValues("CLASSID") == "" ? 0 : Convert.ToInt32(OperationManagement.GetPostValues("CLASSID"));
                string Subject = OperationManagement.GetPostValues("SUBJECT");
                string FileURL = OperationManagement.GetPostValues("ATTACHNMENT");
                string Desc = OperationManagement.GetPostValues("DESCRIPTION");
                string FileName = "";
                if (FileURL != "")
                {
                    FileName = Server.MapPath("~/Upload/" + FileURL);
                }
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID);
                if (ReceiveGroupID != 0)
                {
                    if (ReceiveGroupID == 2)
                    {
                        if (ReceiverID == -1)
                        {
                            TeacherManagement objTeacherManagement = new TeacherManagement();
                            if (ClassIDs != 0 && ClassIDs != -1)
                            {
                                int ClassID = Convert.ToInt32(ClassIDs);
                                List<MISTeacher> obj = objTeacherManagement.TeacherList(SchoolID, "", ClassID, "", "", "", 0, "0");
                                string Email = "";
                                foreach (var item in obj)
                                {
                                    Email += item.Email + ",";
                                }
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Teachers, <br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                EmailManagement objEmailManagement = new EmailManagement();
                                objEmailManagement.SendEmailToMultiple(Email, Subject, Message, FileName);
                                //foreach (var item in obj)
                                //{
                                //    string Email = item.Email;
                                //    EmailManagement objEmailManagement = new EmailManagement();
                                //    string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", item.Name, Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                //    objEmailManagement.SendEmails(Email, Subject, Message, FileURL);
                                //}
                            }
                            else
                            {
                                List<MISTeacher> obj = objTeacherManagement.TeacherList(SchoolID, "", 0, "", "", "", 0, "0");
                                string Email = "";
                                foreach (var item in obj)
                                {
                                    Email += item.Email + ",";
                                }

                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Teachers, <br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                EmailManagement objEmailManagement = new EmailManagement();
                                objEmailManagement.SendEmailToMultiple(Email, Subject, Message, FileName);
                                //foreach (var item in obj)
                                //{
                                //    string Email = item.Email;
                                //    EmailManagement objEmailManagement = new EmailManagement();
                                //    string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", item.Name, Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                //    objEmailManagement.SendEmails(Email, Subject, Message, FileURL);
                                //}
                            }
                        }
                        else
                        {
                            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID);
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", ObjTeacher.Name, Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                            objEmailManagement.SendEmails(ObjTeacher.Email, Subject, Message, FileName);
                        }
                    }
                    if (ReceiveGroupID == 3)
                    {
                        if (ReceiverID == -1)
                        {
                            ParentManagement objParentManagement = new ParentManagement();
                            if (ClassIDs != 0 && ClassIDs != -1)
                            {
                                int IDs = Convert.ToInt32(ClassIDs);
                                List<MISStudent> objList = new List<MISStudent>();
                                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                                {
                                    objList = objParentManagement.ParentList(SchoolID);
                                    ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == IDs);
                                    if (ObjCl.TypeID == (int)EnumsManagement.CLASSTYPE.Office)
                                    {
                                        ClassManagement objClassManagement = new ClassManagement();
                                        objList = objClassManagement.StudentListByOfficeClass(SchoolID);
                                    }
                                    else
                                    {
                                        objList = objList.Where(p => p.ClassID == IDs).ToList();
                                    }
                                }
                                else
                                {
                                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                                    objList = objParentManagement.ParentList(SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                                    ISClass ObjCl = DB.ISClasses.SingleOrDefault(p => p.ID == IDs);
                                    objList = objList.Where(p => p.ClassID == IDs).ToList();
                                }

                                string Email = "";
                                foreach (var item in objList)
                                {
                                    Email += item.ParantEmail1 + ",";
                                }

                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parents, <br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                EmailManagement objEmailManagement = new EmailManagement();
                                objEmailManagement.SendEmailToMultiple(Email, Subject, Message, FileName);
                            }
                            else
                            {
                                List<MISStudent> objList = new List<MISStudent>();
                                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                                {
                                    objList = objParentManagement.ParentList(Authentication.SchoolID);
                                }
                                else
                                {
                                    string dts = DateTime.Now.ToString("dd/MM/yyyy");
                                    objList = objParentManagement.ParentList(Authentication.SchoolID).Where(p => p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == dts).ToList();
                                }
                                string Email = "";
                                foreach (var item in objList)
                                {
                                    Email += item.ParantEmail1 + ",";
                                }
                                string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parents, <br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                EmailManagement objEmailManagement = new EmailManagement();
                                objEmailManagement.SendEmailToMultiple(Email, Subject, Message, FileName);
                            }
                        }
                        else
                        {
                            ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ReceiverID);
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", ObjStudent.StudentName, Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                            objEmailManagement.SendEmails(ObjStudent.ParantEmail1, Subject, Message, FileName);
                        }
                    }
                    if (ReceiveGroupID == 4)
                    {
                        if (ReceiverID == -1)
                        {
                            TeacherManagement objTeacherManagement = new TeacherManagement();
                            if (ClassIDs != 0 && ClassIDs != -1)
                            {
                                int ClassID = Convert.ToInt32(ClassIDs);
                                List<MISTeacher> obj = objTeacherManagement.NonTeacherList(SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                                if (ClassID != 0)
                                {
                                    obj = obj.Where(p => p.ISTeacherClassAssignments.Any(m => m.ClassID == ClassID)).ToList();
                                }
                                foreach (var item in obj)
                                {
                                    string Email = item.Email;
                                    EmailManagement objEmailManagement = new EmailManagement();
                                    string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Non Teacher, <br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                    objEmailManagement.SendEmails(Email, Subject, Message, FileName);
                                }
                            }
                            else
                            {
                                List<MISTeacher> obj = objTeacherManagement.NonTeacherList(SchoolID, "", "", "").Where(p => p.Active == true).ToList();
                                foreach (var item in obj)
                                {
                                    string Email = item.Email;
                                    EmailManagement objEmailManagement = new EmailManagement();
                                    string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear Non Teacher, <br><br> Subject &nbsp;: {0}<br><br>Description &nbsp;: {1}.<br /><br>Thanks,<br>School APP<br> {2}<br>Mobile No : {3} <br>Email id : {4}<br>", Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                                    objEmailManagement.SendEmails(Email, Subject, Message, FileName);
                                }
                            }
                        }
                        else
                        {
                            ISTeacher ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID);
                            string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == ReceiverID).Email;
                            EmailManagement objEmailManagement = new EmailManagement();
                            string Message = string.Format("<center><font size='5' color='blue'>Welcome To School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br>Thanks,<br>School APP<br> {3}<br>Mobile No : {4} <br>Email id : {5}<br>", ObjTeacher.Name, Subject, Desc, ObjSchool.Name, ObjSchool.PhoneNumber, ObjSchool.AdminEmail);
                            objEmailManagement.SendEmails(Email, Subject, Message, FileName);
                        }
                    }
                }

                if (SchoolID != 0)
                {
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Message Send Successfully to Teacher/Student By " + ObjSchool.Name + " Document Category : Message", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Message");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Message Sent Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Message Not Sent";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetHolidayByID()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                ISHoliday Obj = DB.ISHolidays.SingleOrDefault(p => p.ID == ID);
                MISHoliday objMISHoliday = new MISHoliday();
                if (Obj != null)
                {
                    objMISHoliday.ID = Obj.ID;
                    objMISHoliday.FromDate = Obj.DateFrom != null ? Obj.DateFrom.Value.ToString("dd/MM/yyyy") : "";
                    objMISHoliday.ToDate = Obj.DateTo != null ? Obj.DateTo.Value.ToString("dd/MM/yyyy") : "";
                    objMISHoliday.Name = Obj.Name;
                    objMISHoliday.ActiveStatus = Obj.Active == true ? "Active" : "InActive";
                    objMISHoliday.Active = Obj.Active;
                    objMISHoliday.Deleted = Obj.Deleted;
                    objMISHoliday.DateFrom = Obj.DateFrom;
                    objMISHoliday.DateTo = Obj.DateTo;
                    objMISHoliday.SchoolID = Obj.SchoolID;
                }
                if (objMISHoliday != null)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Holiday Found Successfully";
                    ResponseObject.Data = objMISHoliday;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Holiday Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void DeleteHoliday()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                ISHoliday Obj = DB.ISHolidays.SingleOrDefault(p => p.ID == ID);
                if (Obj != null)
                {
                    Obj.Active = false;
                    Obj.Deleted = false;
                    Obj.DeletedBy = 1;
                    Obj.DeletedDateTime = DateTime.Now;
                    DB.SaveChanges();
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == Obj.SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Holiday Deleted Successfully " + "Name : " + Obj.Name + " Document Category : HolidayList", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Holiday");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Holiday Deleted Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "Holiday Can not Deleted";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void LogActivityData()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != null ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;

                var ObjList = from item in DB.ISUserActivities.Where(p => p.Deleted == true && p.SchoolID == SchoolID).ToList()
                              group item by new { item.CreatedBy, item.UserName } into g
                              select new { CreatedBy = g.Key.CreatedBy, UserName = g.Key.UserName };

                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Logs Found Successfully";
                    ResponseObject.Data = ObjList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Logs Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }

        }
        public void GetStandardClass()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                int ClassID = OperationManagement.GetPostValues("CLASSID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("CLASSID")) : 0;
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> ObjList = objClassManagement.ClassList(SchoolID, OperationManagement.GetPostValues("YEAR"), 0);
                if (SchoolID != 0)
                {
                    ObjList = ObjList.Where(p => p.Active == true && p.TypeID != (int)EnumsManagement.CLASSTYPE.AfterSchool && p.TypeID != (int)EnumsManagement.CLASSTYPE.Office).ToList();
                }
                if (ClassID != 0)
                {
                    ObjList = ObjList.Where(p => p.ID == ClassID).ToList();
                }
                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Class Found Successfully";
                    ResponseObject.Data = ObjList;

                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Class Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ClearActivity()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                List<ISUserActivity> ObjList = DB.ISUserActivities.Where(p => p.SchoolID == SchoolID).ToList();
                DB.ISUserActivities.RemoveRange(ObjList);
                DB.SaveChanges();
                if (ObjList.Count() > 0)
                {
                    ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    LogManagement.AddLogs("Activities Clear Successfully " + "School ID : " + SchoolID.ToString() + " Document Category : LogAcitivity", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Activities");
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Activities Clear Successfully";
                    ResponseObject.Data = "";
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Activities Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void GetAfterOfficeClass()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                ClassManagement objClassManagement = new ClassManagement();
                List<MISClass> ObjList = new List<MISClass>();
                if (SchoolID != 0)
                {
                    ObjList = (from item in DB.ISClasses.Where(p => p.SchoolID == SchoolID && p.TypeID != (int)EnumsManagement.CLASSTYPE.Standard && !p.Name.Contains("After School Ex") && p.Deleted == true).ToList()
                               select new MISClass
                               {
                                   ID = item.ID,
                                   SchoolID = item.SchoolID,
                                   Name = item.Name,
                                   YearName = item.Year != "" ? objClassManagement.getyeardata(item.Year) : "",
                                   Year = item.Year,
                                   TypeID = item.TypeID,
                                   AfterSchoolType = item.AfterSchoolType,
                                   ExternalOrganisation = item.ExternalOrganisation,
                                   EndDate = item.EndDate,
                                   EndDateString = item.EndDate != null ? item.EndDate.Value.ToString("dd/MM/yyyy") : "",
                                   PickupComplete = item.PickupComplete,
                                   Active = item.Active,
                                   Deleted = item.Deleted,
                                   CreatedBy = item.CreatedBy,
                                   CreatedDateTime = item.CreatedDateTime,
                                   ModifyBy = item.ModifyBy,
                                   ModifyDateTime = item.ModifyDateTime,
                                   DeletedBy = item.DeletedBy,
                                   DeletedDateTime = item.DeletedDateTime,
                                   ClassType = item.ISClassType.Name,
                                   StudentCount = item.TypeID == (int)EnumsManagement.CLASSTYPE.Office ?
                                    objClassManagement.getOfficeStudentCount(item.SchoolID) : item.TypeID == (int)EnumsManagement.CLASSTYPE.Club ?
                                    objClassManagement.getClubStudentCount(item.SchoolID) : item.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool ? 
                                    objClassManagement.getInternalStudentCount(item.SchoolID) : item.ISStudents.Where(p => p.ClassID == item.ID && p.SchoolID == SchoolID && p.Deleted == true && p.Active == true).ToList().Count,
                               }).ToList();
                }
                if (ObjList.Count() > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Class Found Successfully";
                    ResponseObject.Data = ObjList;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Class Found";
                    ResponseObject.Data = "";

                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SchoolChangePassword()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                string Password = OperationManagement.GetPostValues("PASSWORD");
                if (SchoolID != 0)
                {
                    ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                    if (objSchool != null)
                    {
                        objSchool.Password = EncryptionHelper.Encrypt(Password);
                        objSchool.ModifyBy = SchoolID;
                        objSchool.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();
                        ISSchool ObjSchools = DB.ISSchools.SingleOrDefault(p => p.ID == SchoolID && p.Deleted == true);
                        LogManagement.AddLogs("SchoolAdmin Password Updated Successfully " + "Name : " + ObjSchools.Name + " Document Category : School", ObjSchools.ID, ObjSchools.ID, String.Format("{0} {1}", ObjSchools.AdminFirstName, ObjSchools.AdminLastName), "Profile");
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Password Updated Successfully";
                        ResponseObject.Data = "";
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "Password not Updated";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SearchAfterSchool()
        {
            try
            {
                List<MISSchool> objSchool = (from item in DB.ISSchools.Where(p => p.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool && p.Active == true && p.Deleted == true).ToList()
                                             select new MISSchool
                                             {
                                                 ID = item.ID,
                                                 Name = item.Name
                                             }).ToList();
                if (objSchool.Count > 0)
                {
                    ResponseObject.Status = "S";
                    ResponseObject.Message = "AfterSchool Found Successfully";
                    ResponseObject.Data = objSchool;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No AfterSchool Found";
                    ResponseObject.Data = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SearchTeacherBySchool()
        {
            try
            {
                int SchoolID = OperationManagement.GetPostValues("SCHOOLID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("SCHOOLID")) : 0;
                if (SchoolID != 0)
                {
                    List<MISTeacher> objTeacher = (from item in DB.ISTeachers.Where(p => p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.SchoolID == SchoolID && p.Deleted == true).ToList()
                                                   select new MISTeacher
                                                   {
                                                       ID = item.ID,
                                                       Name = item.Name
                                                   }).ToList();
                    if (objTeacher.Count > 0)
                    {
                        ResponseObject.Status = "S";
                        ResponseObject.Message = "Teachers Found Successfully";
                        ResponseObject.Data = objTeacher;
                    }
                    else
                    {
                        ResponseObject.Status = "F";
                        ResponseObject.Message = "No Teachers Found";
                        ResponseObject.Data = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void ViewPDFInvoice()
        {
            try
            {
                int ID = OperationManagement.GetPostValues("ID") != "" ? Convert.ToInt32(OperationManagement.GetPostValues("ID")) : 0;
                //int ID = Convert.ToInt32(e.CommandArgument);
                string body = string.Empty;
                string tblbody = string.Empty;
                Random generator = new Random();
                int r = generator.Next(100000, 1000000);
                Session["Generator1111"] = r.ToString();

                ISSchoolInvoice ObjInvoice = DB.ISSchoolInvoices.SingleOrDefault(p => p.ID == ID);

                if (ObjInvoice != null)
                {
                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == ObjInvoice.SchoolID && p.Deleted == true);

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/invoice.html")))
                    {
                        body += reader.ReadToEnd();
                    }
                    decimal TaxAmount = Convert.ToDecimal((ObjInvoice.TaxRate * ObjInvoice.TransactionAmount) / 100);
                    body = body.Replace("{InvoiceNumber}", ObjInvoice.InvoiceNo);
                    body = body.Replace("{CreatedDate}", ObjInvoice.CreatedDateTime.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("{Due}", ObjInvoice.DateTo.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("{AddressSchool1}", ObjSchool.BillingAddress);
                    body = body.Replace("{AddressSchool2}", ObjSchool.BillingAddress2);
                    body = body.Replace("{TransactionType}", ObjInvoice.ISTrasectionType.Name);
                    //body = body.Replace("{Description}", ObjInvoice.TransactionDesc);
                    body = body.Replace("{Amount}", ObjInvoice.TransactionAmount.ToString());
                    body = body.Replace("{TaxRate}", ObjInvoice.TaxRate.ToString());
                    body = body.Replace("{TaxAmount}", Convert.ToString(TaxAmount));
                    body = body.Replace("{TotalAmount}", Convert.ToString(TaxAmount + ObjInvoice.TransactionAmount));

                    string output_path_pdf = Server.MapPath("~/Upload/SchoolInvoice" + Session["Generator1111"].ToString() + ".pdf");
                    string FileName = "Upload/SchoolInvoice" + Session["Generator1111"].ToString() + ".pdf";
                    var pdfDoc = new NReco.PdfGenerator.HtmlToPdfConverter(); //created an object of HtmlToPdfConverter class.
                    pdfDoc.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                    pdfDoc.Size = NReco.PdfGenerator.PageSize.A4;   //8.27 in × 11.02 in //Page Size
                    NReco.PdfGenerator.PageMargins pageMargins = new NReco.PdfGenerator.PageMargins();     //Margins in mm
                    pageMargins.Bottom = 05;
                    pageMargins.Left = 05;
                    pageMargins.Right = 05;
                    pageMargins.Top = 05;
                    pdfDoc.Margins = pageMargins;                      //margins added to PDF.
                    string content = body;
                    pdfDoc.PageFooterHtml = "<div style='float:right;'></div>";
                    var pdfBytes = pdfDoc.GeneratePdf(content);
                    File.WriteAllBytes(output_path_pdf, pdfBytes);
                    //if (System.IO.File.Exists(output_path_pdf))
                    //{
                    //    System.IO.File.Delete(output_path_pdf);
                    //}
                    //using (FileStream stream = new FileStream(output_path_pdf, FileMode.Create))
                    //{
                    //    StringReader sr = new StringReader(body);
                    //    Document pdfDocs = new Document(PageSize.A4, 05f, 05f, 05f, 05f);
                    //    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDocs, stream);
                    //    pdfDocs.Open();
                    //    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDocs, sr);
                    //    pdfDocs.Close();
                    //}

                    ResponseObject.Status = "S";
                    ResponseObject.Message = "Invoice Generated Successfully";
                    ResponseObject.Data = FileName;
                }
                else
                {
                    ResponseObject.Status = "F";
                    ResponseObject.Message = "No Invoice Generated";
                    ResponseObject.Data = "";
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        #endregion

    }
}