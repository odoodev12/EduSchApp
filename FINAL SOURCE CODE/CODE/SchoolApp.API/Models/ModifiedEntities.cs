using Newtonsoft.Json;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models
{

    public class SchoolType : ISSchoolType
    {
        [JsonIgnore]
        public new virtual ISAccountStatu ISAccountStatu { get; set; }
        [JsonIgnore]
        public new virtual ISCountry ISCountry { get; set; }

        [JsonIgnore]
        public new virtual ICollection<ISHoliday> ISHolidays { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPicker> ISPickers1 { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISStudent> ISStudents { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacher> ISTeachers { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISUserActivity> ISUserActivities { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool1 { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool2 { get; set; }
        [JsonIgnore]
        public new virtual ISSchoolType ISSchoolType { get; set; }
        [JsonIgnore]

        public new virtual ICollection<ISSchoolInvoice> ISSchoolInvoices { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISUserRole> ISUserRoles { get; set; }
        [JsonIgnore]
        public new virtual ISOrganisationUser ISOrganisationUser { get; set; }
    }

    public class School : ISSchool
    {
        [JsonIgnore]
        public new virtual ICollection<ISSchool> ISSchools { get; set; }
    }

    public class Classes : ISClass
    {
        [JsonIgnore]
        public new virtual ICollection<ISCompleteAttendanceRun> ISCompleteAttendanceRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISCompletePickupRun> ISCompletePickupRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISStudent> ISStudents { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacherClassAssignment> ISTeacherClassAssignments { get; set; }
        [JsonIgnore]
        public new virtual ISClassType ISClassType { get; set; }
    }

    public class Teacher : ISTeacher
    {
        [JsonIgnore]
        public new virtual ICollection<ISAttendance> ISAttendances { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISCompleteAttendanceRun> ISCompleteAttendanceRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISCompletePickupRun> ISCompletePickupRuns { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISPickup> ISPickups { get; set; }
        [JsonIgnore]
        public new virtual ISSchool ISSchool { get; set; }
        [JsonIgnore]
        public new virtual ISUserRole ISUserRole { get; set; }
        [JsonIgnore]
        public new virtual ICollection<ISTeacherClassAssignment> ISTeacherClassAssignments { get; set; }
    }
   
}