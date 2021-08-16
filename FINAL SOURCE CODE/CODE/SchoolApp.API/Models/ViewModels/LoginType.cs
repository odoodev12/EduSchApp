using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class LoginType
    {
        public int LoggedInUserId { get; set; }
        public string LoggedInUserName { get; set; }
        public int UserTypeId { get; set; }
    }
}