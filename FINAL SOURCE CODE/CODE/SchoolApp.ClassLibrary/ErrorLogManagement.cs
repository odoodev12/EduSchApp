using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SchoolApp.ClassLibrary
{
    public static class ErrorLogManagement
    {

        public static void AddLog(Page page, Exception ex)
        {
            try
            {
                StackTrace ObjTrace = new StackTrace(ex, true);
                List<MErrorFrames> List = new List<MErrorFrames>();
                foreach (StackFrame item in ObjTrace.GetFrames())
                {
                    MErrorFrames Obj = new MErrorFrames();
                    Obj.FileName = item.GetFileName();
                    Obj.LineNumber = item.GetFileLineNumber().ToString();
                    Obj.ColumnNumber = item.GetFileColumnNumber().ToString();
                    Obj.Method = item.GetMethod().ToString();
                    Obj.Class = item.GetMethod().DeclaringType.ToString();
                    List.Add(Obj);
                }
                MErrorFrames DbObject = new MErrorFrames();
                if (List.Count > 0)
                {
                    DbObject = List[List.Count - 1];
                }


                SchoolAppEntities DB = new SchoolAppEntities();
                ISErrorLog obj_log = new ISErrorLog();
                obj_log.LogText = ex.Message.ToString() + ((ex.InnerException == null) ? "" : ex.InnerException.ToString());
                obj_log.Deleted = true;
                obj_log.Class = DbObject.Class;
                obj_log.LineNumber = DbObject.LineNumber;
                obj_log.Method = DbObject.Method;
                obj_log.FileName = DbObject.FileName;
                obj_log.ColumnNumber = DbObject.ColumnNumber;
                /// obj_log.UserID = Authentication.LoggedInUser.ID;
                obj_log.CreatedDateTime = DateTime.Now;
                DB.ISErrorLogs.Add(obj_log);
                DB.SaveChanges();

                AlertMessageManagement.ServerMessage(page, "A server error occurred. Please contact the administrator!", 2);
            }
            catch (Exception e)
            {
                AlertMessageManagement.ServerMessage(page, "A server error occurred. Please contact the administrator!", 2);
            }
        }

        public static void AddLog(Exception ex)
        {
            try
            {
                Page page = new Page();
                StackTrace ObjTrace = new StackTrace(ex, true);
                List<MErrorFrames> List = new List<MErrorFrames>();
                foreach (StackFrame item in ObjTrace.GetFrames())
                {
                    MErrorFrames Obj = new MErrorFrames();
                    Obj.FileName = item.GetFileName();
                    Obj.LineNumber = item.GetFileLineNumber().ToString();
                    Obj.ColumnNumber = item.GetFileColumnNumber().ToString();
                    Obj.Method = item.GetMethod().ToString();
                    Obj.Class = item.GetMethod().DeclaringType.ToString();
                    List.Add(Obj);
                }
                MErrorFrames DbObject = new MErrorFrames();
                if (List.Count > 0)
                {
                    DbObject = List[List.Count - 1];
                }


                SchoolAppEntities DB = new SchoolAppEntities();
                ISErrorLog obj_log = new ISErrorLog();
                obj_log.LogText = ex.Message.ToString() + ((ex.InnerException != null) ? ex.InnerException.InnerException.ToString() : "");
                obj_log.Deleted = true;
                obj_log.Class = DbObject.Class;
                obj_log.LineNumber = DbObject.LineNumber;
                obj_log.Method = DbObject.Method;
                obj_log.FileName = DbObject.FileName;
                obj_log.ColumnNumber = DbObject.ColumnNumber;
                //obj_log.UserID = Authentication.LoggedInUser.ID;
                obj_log.CreatedDateTime = DateTime.Now;
                DB.ISErrorLogs.Add(obj_log);
                DB.SaveChanges();
                AlertMessageManagement.ServerMessage(page, "A server error occurred. Please contact the administrator!", 2);
            }
            catch (Exception e)
            {

            }
        }

    }
}
