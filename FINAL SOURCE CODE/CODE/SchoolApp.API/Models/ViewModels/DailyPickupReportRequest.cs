using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class DailyPickupReportRequest
    {

        public string Date { get; set; }
        public string StudentID { get; set; }
        public string PickerID { get; set; }
        public string PickupStatus { get; set; }
        public string SortByStudentID { get; set; }
        public string Orderby { get; set; }
    }
}