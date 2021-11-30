using SchoolApp.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class AddNonTeacherViewModel
    {
        public int SchoolID { get; set; }
        public int[] ClassIDs { get; set; }
        public int RoleID { get; set; }
        public string TeacherNo { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int LoginUserId { get; set; }
        public int LoginUserType { get; set; }




        //public DateTime? EndDate { get; set; }
        //public string Password { get; set; }
        //public string Photo { get; set; }
        //public Nullable<bool> Deleted { get; set; }
        //public Nullable<bool> ISActivated { get; set; }
        //public string IsActivationID { get; set; }
        //public string MemorableQueAnswer { get; set; }
        //public Nullable<bool> IsBell { get; set; }

        public bool ValidModel(out string ErrorMessage)
        {
            ErrorMessage = "";
            if (SchoolID <= 0)
                ErrorMessage += "SchoolID is required, ";

            if (RoleID <= 0)
                ErrorMessage += "RoleID is required, ";

            if (LoginUserId <= 0)
                ErrorMessage += "LoginUserId is required, ";

            if (LoginUserType <= 0 || LoginUserType > 4)
                ErrorMessage += "UserType is required (1=Admin, 2=School, Teacher=3, NonTeacher=4), ";

            if (string.IsNullOrWhiteSpace(Title))
                ErrorMessage += "Title is required, ";

            if (string.IsNullOrWhiteSpace(Name))
                ErrorMessage += "Name is required, ";

            if (string.IsNullOrWhiteSpace(Email))
                ErrorMessage += "Email is required, ";

            if (string.IsNullOrWhiteSpace(PhoneNo))
                ErrorMessage += "PhoneNo is required, ";


            return (SchoolID > 0 && RoleID > 0 && LoginUserId > 0 && LoginUserType > 0 && LoginUserType < 5 && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(PhoneNo)) ? true : false;
        }
    }
    public class UpdateNonTeacherViewModel : AddTeacherViewModel
    {
        public int Id { get; set; }

        public bool Active { get; set; }
    }
    public class TeacherDetailsViewModel
    {
        public int ID { get; set; }
        public string TeacherNo { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public Nullable<int> Role { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Photo { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Status { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Password { get; set; }
        public string TeacherEndDate { get; set; }
        public int RoleID { get; set; }
        public string ClassName { get; set; }
        public int? ClassID1 { get; set; }
        public int? ClassID2 { get; set; }
        public string ClassName1 { get; set; }
        public string ClassName2 { get; set; }       
    }
}