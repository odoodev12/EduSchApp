using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SchoolApp.ClassLibrary
{
    public static class OperationManagement
    {
        public static List<string> DefaultPickupStatuslist = new List<string>
        {
            "Weekend (School Closed)",
            "Not Marked",
            "UnPicked"
        };

        public static int Opration
        {
            get
            {
                return (int)HttpContext.Current.Session["Opration"];
            }

            set
            {
                HttpContext.Current.Session["Opration"] = value;
            }
        }

        public static int ID
        {
            get
            {
                return (int)HttpContext.Current.Session["ID"];
            }

            set
            {
                HttpContext.Current.Session["ID"] = value;
            }
        }

        public enum OPERRATION
        {
            None = 0,
            Add = 1,
            Edit = 2,
            Delete = 3
        }

        public static string GetOperation(string Value)
        {
            if (HttpContext.Current.Request.QueryString.AllKeys.Contains(Value))
            {
                return HttpContext.Current.Request.QueryString[Value].ToString().ToUpper();
            }
            else
            {
                return "";
            }

        }
        public static string GetPostValues(string Value)
        {
            if (HttpContext.Current.Request.Form.AllKeys.Contains(Value))
            {
                return HttpContext.Current.Request.Form[Value].ToString();
            }
            else
            {
                return "";
            }

        }

        public static string GetDefaultPickupStatus(string Dates = "")
        {
            DateTime dt = DateTime.Now;

            string returnStatus = "";
            if (Dates != "")
            {
                try
                {
                    dt = Convert.ToDateTime(Dates);

                    if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                        returnStatus = DefaultPickupStatuslist[0];
                    else
                        returnStatus = DefaultPickupStatuslist[1];
                }
                catch (Exception)
                {
                    string dates = Dates;
                    string Format = "";

                    if (dates.Contains("/"))
                    {
                        string[] arrDate = dates.Split('/');
                        Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                    }
                    else
                    {
                        Format = dates;
                    }
                    dt = Convert.ToDateTime(Format);
                }
            }

            if (returnStatus == "")
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    return DefaultPickupStatuslist[0];
                else
                    return DefaultPickupStatuslist[1];
            }

            return returnStatus;
        }

        public static string GetTodayHolidayStatus(List<ISHoliday> ObjHoliday)
        {
            string holiday = "";
            DateTime dt = DateTime.Now;
            if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
            {
                foreach (var i in ObjHoliday)
                {
                    if (dt.Date >= i.DateFrom.Value && dt.Date <= i.DateTo.Value ||
                        dt.Date.ToString("dd/MM/yyyy") == i.DateFrom.Value.ToString("dd/MM/yyyy") ||
                        dt.Date.ToString("dd/MM/yyyy") == i.DateTo.Value.ToString("dd/MM/yyyy"))
                    {
                        holiday = $"{i.Name}(School Closed)";
                    }
                }
            }

            return holiday;
        }

        public static bool GetCalcTodayDateWithGivenDate(DateTime pickerCreationDate, int noOfAddDays, ref int noOfDayRemaining)
        {
            DateTime todayDate = DateTime.Now;


            bool isDateBetweenTwoDate = false;


            DateTime dt = pickerCreationDate;
            try
            {

                noOfDayRemaining = noOfAddDays - (todayDate - dt).Days;

                isDateBetweenTwoDate = (todayDate - dt).Days < noOfAddDays;
            }
            catch (Exception)
            {

                noOfDayRemaining = noOfAddDays - (todayDate - dt).Days;

                isDateBetweenTwoDate = (todayDate - dt).Days < noOfAddDays;

            }


            return isDateBetweenTwoDate;
        }

        //public static bool GetCalcTodayDateWithGivenDate(string pickerCreationDate, int noOfAddDays, ref int noOfDayRemaining)
        //{
        //    DateTime todayDate = DateTime.Now;


        //    bool isDateBetweenTwoDate = false;

        //    if (pickerCreationDate != "")
        //    {
        //        DateTime dt;
        //        try
        //        {
        //            dt = Convert.ToDateTime(pickerCreationDate);

        //            string dates = pickerCreationDate;
        //            string Format = "";

        //            if (dates.Contains("/"))
        //            {
        //                string[] arrDate = dates.Split('/');
        //                Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
        //            }
        //            else
        //            {
        //                Format = dates;
        //            }
        //            dt = Convert.ToDateTime(Format);

        //            noOfDayRemaining = noOfAddDays - (todayDate - dt).Days;

        //            isDateBetweenTwoDate = (todayDate - dt).Days < noOfAddDays;
        //        }
        //        catch (Exception)
        //        {
        //            string dates = pickerCreationDate;
        //            string Format = "";

        //            if (dates.Contains("/"))
        //            {
        //                string[] arrDate = dates.Split('/');
        //                Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
        //            }
        //            else
        //            {
        //                Format = dates;
        //            }
        //            dt = Convert.ToDateTime(Format);

        //            noOfDayRemaining = noOfAddDays - (todayDate - dt).Days;

        //            isDateBetweenTwoDate = (todayDate - dt).Days < noOfAddDays;

        //        }
        //    }

        //    return isDateBetweenTwoDate;
        //}

        public static Tuple<bool, bool, int> IsValidIndividaulPickerFor(int pickerId, int noOfAddDays)
        {
            int noOfDayRemaining = -1;

            Tuple<bool, bool, int> isValid = new Tuple<bool, bool, int>(true, true, noOfDayRemaining);
            SchoolAppEntities DB = new SchoolAppEntities();

            ISPicker ispickerObj = DB.ISPickers.SingleOrDefault(r => r.ID == pickerId);

            if (ispickerObj.PickerType.Value == 1)
            {
                bool isValidCreatedDate = GetCalcTodayDateWithGivenDate(ispickerObj.CreatedDateTime.Value, noOfAddDays, ref noOfDayRemaining);

                isValid = new Tuple<bool, bool, int>(isValidCreatedDate, ispickerObj.Photo != "Upload/user.jpg", noOfDayRemaining);
            }

            return isValid;
        }



        public static bool IsAfterSchoolFlagEnable(int OfficeID, string pickStatus)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            bool isAfterSchool = false;
            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && p.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool);
            if (_Class != null)
            {
                if (pickStatus == "After-School" || pickStatus == "After-School-Ex")
                {
                    isAfterSchool = true;
                }
            }

            return isAfterSchool;
        }
    }
}
