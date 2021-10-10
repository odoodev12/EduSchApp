using SchoolApp.API.Models;
using SchoolApp.API.Services;
using System.Web.Http;


namespace SchoolApp.API.Controllers
{
    [Authorize]
    public class ClassController : ApiController
    {
        /// <summary>
        /// Class Service
        /// </summary>
        private ClassService service;

        /// <summary>
        /// Class service Related API's
        /// </summary>
        public ClassController()
        {
            service = new ClassService();
        }


        /// <summary>
        /// To All ClassType list
        /// </summary>
        /// <returns></returns>
        [Route("ClassTypes/List")]
        [HttpGet]
        public ReturnResponce ClassTypeList()
        {
            return service.GetClassTypeList();
        }

        /// <summary>
        ///  To All ClassType list by School Type
        /// </summary>
        /// <param name="SchoolTypeId"></param>
        /// <returns></returns>
        [Route("ClassTypes/BySchoolType")]
        [HttpGet]
        public ReturnResponce ClassTypeListBySchoolType(int SchoolTypeId)
        {
            return service.GetClassTypeListBySchoolType(SchoolTypeId);
        }

        /// <summary>
        /// To Get class list by SchoolId, Year and ClassTypeId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="year"></param>
        /// <param name="ClassTypeId"></param>
        /// <returns></returns>
        [Route("Classes/BySchoolId")]
        [HttpGet]
        public ReturnResponce ClassListBySchoolId(int SchoolId, string year = null, int? ClassTypeId = null)
        {
            return service.GetClassList(SchoolId, year ?? "", ClassTypeId ?? 0);
        }


        /// <summary>
        /// To Get class list by SchoolId,TeacherId, Year and ClassTypeId
        /// </summary>
        /// <param name="SchoolId"></param>
        /// <param name="TeacherId"></param>
        /// <param name="year"></param>
        /// <param name="ClassTypeId"></param>
        /// <returns></returns>
        [Route("Classes/ByTeacherId")]
        [HttpGet]
        public ReturnResponce ClassListByTeacherId(int SchoolId, int TeacherId, string year = null, int? ClassTypeId = null)
        {
            return service.GetClassList(SchoolId, TeacherId, year??"", ClassTypeId ?? 0);
        }

        /// <summary>
        /// To Get Class List By Filter Options (SchoolID , Year, ClasstypeID, Status(Active or InActive),Class Name, SortBy, IsAscending( True then Asc otherwise Desc) )
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="Year"></param>
        /// <param name="ClassTypeId"></param>
        /// <param name="SortBy"></param>
        /// <param name="ClassName"></param>
        /// <param name="IsAscending"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Classes/ByFilter")]
        [HttpGet]
        public ReturnResponce ClassListByFilter(int SchoolID, int TeacherID, string Year = "", int ClassTypeId = 0, string SortBy = "Date", string ClassName = "", bool IsAscending = false, bool? IsActive = null)
        {
            string Status = "";
            if (IsActive != null)
            {
                if (IsActive == true)
                    Status = "1";
                else if (IsActive == true)
                    Status = "2";
            }

            if (TeacherID > 0)
            {
                return service.GetClassListByFilter(SchoolID, TeacherID, Year, ClassTypeId, ClassName, Status, IsAscending, SortBy);
            }
            else
            {
                return service.GetClassListByFilter(SchoolID, Year, ClassTypeId, ClassName, Status, IsAscending, SortBy);
            }
        }



        /// <summary>
        /// To Add Class
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="ClassName"></param>
        /// <param name="Year"></param>
        /// <param name="ClassTypeID"></param>
        /// <param name="AfterSchoolType"></param>
        /// <param name="ExtOrganisation"></param>
        /// <param name="Active"></param>
        /// <param name="EndDate"></param>
        /// <param name="ISNonListed"></param>
        /// <param name="CreateType"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        [Route("Class/Add")]
        [HttpPost]
        public ReturnResponce AddClass(int SchoolID, string ClassName, string Year, int ClassTypeID, string AfterSchoolType, string ExtOrganisation, bool Active, string EndDate, bool ISNonListed, int CreateType, int LoginUserId)
        {
            return (LoginUserId > 0 && CreateType > 0) ? service.AddClass(SchoolID, ClassName, Year, ClassTypeID, AfterSchoolType, ExtOrganisation, Active, EndDate, ISNonListed, CreateType, LoginUserId) : new ReturnResponce("Invalid request or logged in id  must be greater then 0 ");
        }

        /// <summary>
        /// To Update Class
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="SchoolID"></param>
        /// <param name="Name"></param>
        /// <param name="Year"></param>
        /// <param name="ClassTypeID"></param>
        /// <param name="AfterSchoolType"></param>
        /// <param name="ExternalOrganisation"></param>
        /// <param name="Active"></param>
        /// <param name="EndDate"></param>
        /// <param name="ISNonListed"></param>
        /// <param name="CreateType"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        [Route("Class/Update")]
        [HttpPost]
        public ReturnResponce UpdateClass(int ClassID, int SchoolID, string Name, int LoginUserId, int ClassTypeID, bool Active, bool ISNonListed, string Year = "", string AfterSchoolType = "", string ExternalOrganisation = "", string EndDate = "", int CreateType = 0)
        {

            return (ClassID > 0 && LoginUserId > 0) ? service.UpdateClass(ClassID, SchoolID, Name, Year, ClassTypeID, AfterSchoolType, ExternalOrganisation, Active, EndDate, ISNonListed, CreateType, LoginUserId) : new ReturnResponce("Primary id or logged in id  must be greater then 0 ");
        }


        /// <summary>
        /// To Get Class details by Id
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [Route("Class")]
        [HttpGet]
        public ReturnResponce GetClass(int ClassId)
        {
            return service.GetClass(ClassId);
        }
    }
}