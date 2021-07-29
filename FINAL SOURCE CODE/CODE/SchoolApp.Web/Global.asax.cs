using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SchoolApp.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            int minutes = 60;
            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Interval = 1000 * minutes * 60; // 10800000
            myTimer.AutoReset = true;
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
        }
        public void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ParentManagement _ParentManagement = new ParentManagement();

            DateTime todayDate = DateTime.Now;

            if (todayDate.DayOfWeek == DayOfWeek.Saturday && todayDate.TimeOfDay.Hours > 9)
            {
                List<ISStudent> _StudentList = DB.ISStudents.Where(p => (String.IsNullOrEmpty(p.Photo) || p.Photo.Contains("Upload/user.jpg")) && p.Deleted == true &&
                (todayDate.Date != p.AutoEmailSendDateForImageUpload.Value)).ToList();
                if (_StudentList.Count > 0)
                {
                    foreach (var item in _StudentList)
                    {
                        _ParentManagement.NoImageEmailManage(item);
                        ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == item.ID && p.Deleted == true);
                        _Student.AutoEmailSendDateForImageUpload = DateTime.Now.Date;
                        _Student.ISImageEmail = true;
                        _Student.SentMailDate = DateTime.Now;
                        DB.SaveChanges();
                    }
                }
            }
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}