using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SchoolApp.ClassLibrary.EnumsManagement;

namespace SchoolApp.API.Models.ViewModels
{
    public class UserRoleEdit : UserRoleAdd
    {
        public int ID { get; set; }

        public bool ValidUpdateModel(out string ErrorMessage)
        {
            ErrorMessage = "";

            if (ID <= 0)
                ErrorMessage += "UserRoleId is required, ";

            if (SchoolID <= 0)
                ErrorMessage += "SchoolID is required, ";

            if (string.IsNullOrWhiteSpace(RoleName))
                ErrorMessage += "Role Name is required, ";

            if (RoleType <= 0)
                ErrorMessage += "RoleType is required, ";
            else if (RoleType < 1 || RoleType > 2)
            {
                ErrorMessage += "Invalid RoleType(Teaching=1,NonTeaching=2), ";
            }

            if (SchoolType <= 0)
                ErrorMessage += "SchoolType is required, ";
            else if (SchoolType < 1 || SchoolType > 2)
            {
                ErrorMessage += "Invalid SchoolType(AfterSchool=1,Standard=2), ";
            }

            if (LoginUserId <= 0)
                ErrorMessage += "LoginUserId is required, ";

            return (ID > 0 && SchoolID > 0 && LoginUserId > 0 && !string.IsNullOrWhiteSpace(RoleName) && (RoleType > 0 && RoleType < 3) && (SchoolType > 0 && SchoolType < 3)) ? true : false;
        }

    }

    public class UserRoleAdd
    {
        public int SchoolID { get; set; }
        public string RoleName { get; set; }
        public int RoleType { get; set; }
        public bool ManageTeacherFlag { get; set; }
        public bool ManageClassFlag { get; set; }
        public bool ManageSupportFlag { get; set; }
        public bool ManageStudentFlag { get; set; }
        public bool ManageViewAccountFlag { get; set; }
        public bool ManageNonTeacherFlag { get; set; }
        public bool ManageHolidayFlag { get; set; }
        public bool Active { get; set; }
        public int LoginUserId { get; set; }

        public int SchoolType { get; set; }


        public bool ValidAddModel(out string ErrorMessage)
        {
            ErrorMessage = "";
            if (SchoolID <= 0)
                ErrorMessage += "SchoolID is required, ";

            if (string.IsNullOrWhiteSpace(RoleName))
                ErrorMessage += "RoleName is required, ";

            if (RoleType <= 0)
                ErrorMessage += "RoleType is required, ";
            else if (RoleType < 1 || RoleType > 2)
            {
                ErrorMessage += "Invalid RoleType(Teaching=1,NonTeaching=2), ";
            }

            if (SchoolType <= 0)
                ErrorMessage += "SchoolType is required, ";
            else if (SchoolType < 1 || SchoolType > 2)
            {
                ErrorMessage += "Invalid SchoolType(AfterSchool=1,Standard=2), ";
            }

            if (LoginUserId <= 0)
                ErrorMessage += "LoginUserId is required, ";

            return (SchoolID > 0 && LoginUserId > 0 && !string.IsNullOrWhiteSpace(RoleName) && (RoleType > 0 && RoleType <3 ) && (SchoolType > 0 && SchoolType < 3)) ? true : false;
        }

    }
}