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
        public bool ValidModel(out string ErrorMessage)
        {
            ErrorMessage = "";
            if (SchoolID <= 0)
                ErrorMessage += "SchoolID is required, ";

            if (RoleID <= 0)
                ErrorMessage += "RoleID is required, ";

            if (LoginUserId <= 0)
                ErrorMessage += "LoginUserId is required, ";

            if (UserType <= 0)
                ErrorMessage += "UserType is required (1=Teacher and 2=NonTeacher), ";

            if (string.IsNullOrWhiteSpace(Title))
                ErrorMessage += "Title is required, ";

            if (string.IsNullOrWhiteSpace(Name))
                ErrorMessage += "Name is required, ";

            if (string.IsNullOrWhiteSpace(Email))
                ErrorMessage += "Email is required, ";

            if (string.IsNullOrWhiteSpace(PhoneNo))
                ErrorMessage += "PhoneNo is required, ";


            return (SchoolID > 0 && RoleID>0 && LoginUserId > 0 && UserType > 0 && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(PhoneNo)) ? true : false;
        }
    }
    public class UpdateTeacherViewModel : AddTeacherViewModel
    {
        public int TeacherId { get; set; }
    }
}