using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models
{
    public class EntityJsonIgnore
    {
        public static string[] SchoolIgnore
        {
            get
            {
                return new[] { "ISAccountStatu", "ISCountry", "ISHolidays", "ISPickers", "ISPickers1", "ISStudents", "ISTeachers", "ISUserActivities", "ISSchool1", "ISSchool2", "ISSchoolType", "ISSchoolInvoices", "ISUserRoles", "ISOrganisationUser" };
            }
        }

        public static string[] TeacherIgnore
        {
            get
            {
                return new[] { "ISAttendances", "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISPickups", "ISSchool", "ISUserRole", "ISTeacherClassAssignments" , "ISClass", "ISTeacher" };
            }
        }
        public static string[] StudentIgnore
        {
            get
            {
                return new[] { "ISAttendances", "ISClass", "ISPickers", "ISPickers1", "ISPickups", "ISSchool" };
            }
        }

        public static string[] SupportIgnore
        {
            get
            {
                return new[] { "ISLogType", "ISSupportStatu", "ISTicketMessages" };
            }
        }
        public static string[] OrganisationUserIgnore
        {
            get
            {               
                return new[] { "ISCountry", "ISRole", "ISSchools" };
            }
        }

        public static string[] SchoolInvoiceIgnore
        {
            get
            {
                return new[] { "ISSchool", "ISTrasectionStatu", "ISTrasectionType" };
            }
        }
        public static string[] PickerIgnore
        {
            get
            {
                return new[] { "ISStudent", "ISSchool", "ISTrasectionType" , "ISPickup" };
            }
        }
        public static string[] ClassesIgnore
        {
            get
            {
                return new[] { "ISCompleteAttendanceRuns", "ISCompletePickupRuns", "ISStudents", "ISTeacherClassAssignments", "ISClassType" };
            }
        }

    }
}