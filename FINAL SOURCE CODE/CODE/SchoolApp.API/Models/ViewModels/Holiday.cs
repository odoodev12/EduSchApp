using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public partial class HolidayAdd
    {
        public int SchoolID { get; set; }
        public string Name { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public bool Active { get; set; }
        public int LoginUserId { get; set; }

        public bool ValidAddModel(out string ErrorMessage)
        {
            ErrorMessage = "";
            if (SchoolID <= 0)
                ErrorMessage += "SchoolID is required, ";

            if (string.IsNullOrWhiteSpace(Name))
                ErrorMessage += "Name is required, ";

            if (DateFrom.Date >= DateTime.Now.Date) { }
            else
                ErrorMessage += "From Date should be equal and greater than Today date, ";

            if (DateTo.Date >= DateFrom.Date) { }
            else
                ErrorMessage += "To Date should be equal and greater than From date, ";

            if (LoginUserId <= 0)
                ErrorMessage += "LoginUserId is required, ";

            return (SchoolID > 0 && LoginUserId > 0 && !string.IsNullOrWhiteSpace(Name) && (DateFrom.Date >= DateTime.Now.Date) && (DateTo.Date >= DateFrom.Date)) ? true : false;
        }

    }

    public partial class HolidayEdit : HolidayAdd
    {
        public int ID { get; set; }        

        public bool ValidUpdateModel(out string ErrorMessage)
        {
            ErrorMessage = "";
            if (SchoolID <= 0)
                ErrorMessage += "SchoolID is required, ";

            if (string.IsNullOrWhiteSpace(Name))
                ErrorMessage += "Name is required, ";

            if (DateFrom.Date >= DateTime.Now.Date) { }
            else
                ErrorMessage += "From Date should be equal and greater than Today date, ";

            if (DateTo.Date >= DateFrom.Date) { }
            else
                ErrorMessage += "To Date should be equal and greater than From date, ";

            if (LoginUserId <= 0)
                ErrorMessage += "LoginUserId is required, ";

            return (SchoolID > 0 && LoginUserId > 0 && !string.IsNullOrWhiteSpace(Name) && (DateFrom.Date >= DateTime.Now.Date) && (DateTo.Date >= DateFrom.Date)) ? true : false;
        }

    }

}