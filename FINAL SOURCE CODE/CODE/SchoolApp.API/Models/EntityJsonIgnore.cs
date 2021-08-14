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
    }
}