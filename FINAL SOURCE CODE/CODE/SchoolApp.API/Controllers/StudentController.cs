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
using SchoolApp.ClassLibrary;

namespace SchoolApp.API.Controllers
{
    /// <summary>
    /// Student APIs
    /// </summary>
    [Authorize]
    public class StudentController : ApiController
    {
        /// <summary>
        /// Student Service
        /// </summary>
        private StudentService service;

        /// <summary>
        /// Student API Constructor
        /// </summary>
        public StudentController()
        {
            service = new StudentService();
        }

        /// <summary>
        /// To Get Classe List with StudentCount
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="LoginUserType"></param>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        [Route("Student/ClassStudents")]
        [HttpGet]
        public ReturnResponce ClassStudents(int SchoolID, int LoginUserType, int loginUserId)
        {
            return (SchoolID != 0 && SchoolID > 0 && loginUserId > 0 && LoginUserType > 0) ? service.StudentClass(SchoolID, LoginUserType, loginUserId) : new ReturnResponce("Invalid request or SchoolId,LoginUserType and loginUserId must be greater then 0 ");
        }


        /// <summary>
        /// To Get Classe List with StudentCount With Class and Year Filter
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="LoginUserType"></param>
        /// <param name="loginUserId"></param>
        /// <param name="ClassId"></param>
        /// <param name="Year"></param>
        /// <returns></returns>

        [Route("Student/ClassStudentsWithFilter")]
        [HttpGet]
        public ReturnResponce ClassStudentsWithFilter(int SchoolID, int LoginUserType, int loginUserId, int? ClassId = 0, string Year = "")
        {
            return (SchoolID != 0 && SchoolID > 0 && loginUserId > 0 && LoginUserType > 0) ? service.StudentClassWithFilter(SchoolID, LoginUserType, loginUserId, ClassId, Year) : new ReturnResponce("Invalid request or SchoolId,LoginUserType and loginUserId must be greater then 0 ");
        }


        /// <summary>
        ///  To Get Student List by ClassId With StudentName and StudentNumber Filter
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <param name="ClassId"></param>
        /// <param name="StudentName"></param>
        /// <param name="StudentNumber"></param>
        /// <returns></returns>
        [Route("Student/List")]
        [HttpGet]
        public ReturnResponce GetStudentsByClassId(int SchoolID, int ClassId, string StudentName = "", string StudentNumber = "")
        {
            return (SchoolID > 0 && ClassId > 0) ? service.GetStudentsByClassId(SchoolID, ClassId, StudentName, StudentNumber) : new ReturnResponce("Invalid request or SchoolId and ClassId must be greater then 0 ");
        }


        ///// <summary>
        ///// To Get All Student Details
        ///// </summary>
        ///// <returns></returns>
        //[Route("Student/GetAllStudents")]
        //[HttpGet]
        //public ReturnResponce GetAllStudents()
        //{
        //    return service.GetStudentList();
        //}

        ///// <summary>
        ///// To Get All Student By SchoolId
        ///// </summary>
        ///// <param name="SchoolId"></param>
        ///// <returns></returns>
        //[Route("Student/GetSchoolStudents")]
        //[HttpGet]
        //public ReturnResponce GetSchoolStudents(int SchoolId)
        //{
        //    return (SchoolId != 0 && SchoolId > 0) ? service.GetStudentList(SchoolId) : new ReturnResponce("Invalid request or SchoolId id  must be greater then 0 ");
        //}

        ///// <summary>
        ///// Get All Student of class Respective to School
        ///// </summary>
        ///// <param name="ClassId"></param>
        ///// <param name="SchoolId"></param>
        ///// <returns></returns>
        //[Route("Student/GetClassStudentsBySchoolId")]
        //[HttpGet]
        //public ReturnResponce GetClassStudentsBySchoolId(int SchoolId, int ClassId)
        //{
        //    return (SchoolId != 0 && SchoolId > 0 && ClassId != 0 && ClassId > 0) ? service.GetStudentList(SchoolId, ClassId) : new ReturnResponce("Invalid request or SchoolId id and ClassId must be greater then 0 ");
        //}

        ///// <summary>
        /////To Add Student detail
        ///// </summary>
        ///// <param name="student"></param>
        ///// <param name="AdminId"></param>
        ///// <returns></returns>
        //[Route("Student/Student")]
        //[HttpPost]
        //public ReturnResponce AddStudent(AddStudent student, int AdminId)
        //{
        //    return (student.Id == 0 && AdminId > 0) ? service.AddUpdateStudent(student, AdminId) : new ReturnResponce("Invalid request or logged in id  must be greater then 0 ");
        //}

        /// <summary>
        /// To Update Student detail
        /// </summary>
        /// <param name="studentModel"></param>
        /// <returns></returns>
        [Route("Student/Update")]
        [HttpPost]
        public ReturnResponce UpdateStudent(AddEditStudentModel studentModel)
        {
            if (studentModel.Id <= 0 || studentModel.SchoolID <= 0 || studentModel.ClassID <= 0 || (studentModel.SchoolTypeID >= 1 && studentModel.SchoolTypeID <= 2))
                new ReturnResponce("StudentId, SchoolId, ClassId and SchoolType(AfterSchool,Standard)  must be greater then 0 and valid");
            else if (studentModel.LoginUserId <= 0 || studentModel.LoginUserTypeId <= 0 || (studentModel.LoginUserTypeId != (int)EnumsManagement.USERTYPE.TEACHER || studentModel.LoginUserTypeId != (int)EnumsManagement.USERTYPE.SCHOOL || studentModel.LoginUserTypeId != (int)EnumsManagement.USERTYPE.ADMIN))
                new ReturnResponce("LoginUserId and LoginUserTypeId (Only Teacher, Admin, School )  must be greater then 0 ");
            else if (string.IsNullOrWhiteSpace(studentModel.StudentName) || string.IsNullOrWhiteSpace(studentModel.StudentNo))
                new ReturnResponce("Student Name and StudentNumber must be have value.");
            else if (string.IsNullOrWhiteSpace(studentModel.ParantName1) || string.IsNullOrWhiteSpace(studentModel.ParantEmail1) || string.IsNullOrWhiteSpace(studentModel.ParantRelation1) || string.IsNullOrWhiteSpace(studentModel.ParantPhone1))
                new ReturnResponce("Primary ParentName, Email, Relation and Phone must be have value.");
            else if (string.IsNullOrWhiteSpace(studentModel.ParantName2) || string.IsNullOrWhiteSpace(studentModel.ParantEmail2) || string.IsNullOrWhiteSpace(studentModel.ParantRelation2) || string.IsNullOrWhiteSpace(studentModel.ParantPhone2))
                new ReturnResponce("Secondary ParentName, Email, Relation and Phone must be have value.");
            else if (string.IsNullOrWhiteSpace(studentModel.ParantName2) || string.IsNullOrWhiteSpace(studentModel.ParantEmail2) || string.IsNullOrWhiteSpace(studentModel.ParantRelation2) || string.IsNullOrWhiteSpace(studentModel.ParantPhone2))
                new ReturnResponce("Secondary ParentName, Email, Relation and Phone must be have value.");

            return (studentModel.Id > 0 && studentModel.LoginUserId > 0) ? service.UpdateStudentNew(studentModel) : new ReturnResponce("StudentId or LoginUserId  must be greater then 0 ");
        }

    }
}