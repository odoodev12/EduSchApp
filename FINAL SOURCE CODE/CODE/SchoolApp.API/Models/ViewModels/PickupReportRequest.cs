using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Models.ViewModels
{
    public class PickupReportRequest
    {
        public int StudentID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PickerID { get; set; }
        public string Status { get; set; }

        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}