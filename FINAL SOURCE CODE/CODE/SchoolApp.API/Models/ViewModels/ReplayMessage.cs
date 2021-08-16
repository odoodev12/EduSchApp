using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class ReplayMessage:LoginType
    {
        public int SupportID { get; set; }
        public string Message { get; set; }
        public string SelectFile { get; set; }
    }
}