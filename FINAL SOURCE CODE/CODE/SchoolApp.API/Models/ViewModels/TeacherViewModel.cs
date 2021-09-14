using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class AddTeacherViewModel
    {
        public int SchoolID { get; set; }
        public string Title { get; set; }

        public string Name { get; set; }

        public int RoleID { get; set; }

        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string TeacherNo { get; set; }
        public int[] ClassIDs { get; set; }
        public int UserType { get; set; }
        public int LoginUserId { get; set; }
        public bool Active { get; set; }


        public bool ValidModel()
        {
            return (SchoolID > 0 && LoginUserId > 0 && UserType > 0 && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(PhoneNo)) ? true : false;
        }

    }
    public class UpdateTeacherViewModel : AddTeacherViewModel
    {
        public int TeacherId { get; set; }
    }
}