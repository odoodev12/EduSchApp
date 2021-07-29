using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SchoolApp.ClassLibrary
{
    public static class Authentication
    {
        public static bool isLogin
        {
            get
            {
                return (bool)HttpContext.Current.Session["LOGIN"];
            }
            set
            {
                HttpContext.Current.Session["LOGIN"] = value;
            }
        }
        public static ISAdminLogin LoggedInUser
        {
            get
            {
                return (ISAdminLogin)HttpContext.Current.Session["USER"];
            }
            set
            {
                HttpContext.Current.Session["USER"] = value;
            }
        }
        public static ISOrganisationUser LoggedInOrgUser
        {
            get
            {
                return (ISOrganisationUser)HttpContext.Current.Session["ORGUSER"];
            }
            set
            {
                HttpContext.Current.Session["ORGUSER"] = value;
            }
        }
        public static int SchoolID
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["SHOOLID"].ToString());
            }
            set
            {
                HttpContext.Current.Session["SHOOLID"] = value;
            }
        }
        public static int SchoolTypeID
        {
            get
            {
                return Convert.ToInt32(HttpContext.Current.Session["SCHOOLTYPEID"].ToString());
            }
            set
            {
                HttpContext.Current.Session["SCHOOLTYPEID"] = value;
            }
        }
        public static string USERTYPE
        {
            get
            {
                return HttpContext.Current.Session["USERTYPE"].ToString();
            }
            set
            {
                HttpContext.Current.Session["USERTYPE"] = value;
            }
        }
        public static ISTeacher LogginTeacher
        {
            get
            {
                return (ISTeacher)HttpContext.Current.Session["TEACHER"];
            }
            set
            {
                HttpContext.Current.Session["TEACHER"] = value;
            }
        }
        public static ISSchool LogginSchool
        {
            get
            {
                return (ISSchool)HttpContext.Current.Session["SCHOOL"];
            }
            set
            {
                HttpContext.Current.Session["SCHOOL"] = value;
            }
        }
        public static ISStudent LogginParent
        {
            get
            {
                return (ISStudent)HttpContext.Current.Session["PARENT"];
            }
            set
            {
                HttpContext.Current.Session["PARENT"] = value;
            }
        }
        public static string LoginParentEmail
        {
            get
            {
                return HttpContext.Current.Session["PARENTEMAIL"].ToString();
            }
            set
            {
                HttpContext.Current.Session["PARENTEMAIL"] = value;
            }
        }
        public static string LoginParentName
        {
            get
            {
                return HttpContext.Current.Session["PARENTNAME"].ToString();
            }
            set
            {
                HttpContext.Current.Session["PARENTNAME"] = value;
            }
        }
        public static string LoginParentRelation
        {
            get
            {
                return HttpContext.Current.Session["PARENTRELATION"].ToString();
            }
            set
            {
                HttpContext.Current.Session["PARENTRELATION"] = value;
            }
        }
        public static bool ISUserLogin()
        {
            if (LoggedInUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ISOrgUserLogin()
        {
            if (LoggedInOrgUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ISTeacherLogin()
        {
            if (LogginTeacher == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ISParentLogin()
        {
            if (LogginParent == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ISSchoolLogin()
        {
            if (LogginSchool == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string AuthorizePage()
        {
            return WebConfigurationManager.AppSettings["LoginPage"] + "?ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public static string TeacherAuthorizePage()
        {
            return WebConfigurationManager.AppSettings["TeacherLoginPage"] + "&ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public static string ParentAuthorizePage()
        {
            return WebConfigurationManager.AppSettings["ParentLoginPage"] + "&ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public static string SchoolAuthorizePage()
        {
            return WebConfigurationManager.AppSettings["SchoolLoginPage"] + "&ReturnURL=" + HttpContext.Current.Request.Url.AbsoluteUri;
        }
        public static string UnAuthorizePage()
        {
            return WebConfigurationManager.AppSettings["UnAuthorizedPage"];
        }
    }
}
