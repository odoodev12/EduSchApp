using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class LogManagement
    {

        public static void AddLog(string Text, string DocumentCategory)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISUserActivity obj_log = new ISUserActivity();
            obj_log.LogText = Text;
            obj_log.DocumentCategory = DocumentCategory;
            obj_log.Active = true;
            obj_log.Deleted = true;
            if (Authentication.ISParentLogin())
            {
                obj_log.SchoolID = Authentication.LogginParent.SchoolID;
                obj_log.CreatedBy = Authentication.LogginParent.ID;
                obj_log.UserName = Authentication.LogginParent.StudentName;
            }
            if (Authentication.ISTeacherLogin())
            {
                obj_log.SchoolID = Authentication.LogginTeacher.SchoolID;
                obj_log.CreatedBy = Authentication.LogginTeacher.ID;
                obj_log.UserName = Authentication.LogginTeacher.Name;
            }
            if (Authentication.ISSchoolLogin())
            {
                obj_log.SchoolID = Authentication.LogginSchool.ID;
                obj_log.CreatedBy = Authentication.LogginSchool.ID;
                obj_log.UserName = String.Format("{0} {1}", Authentication.LogginSchool.AdminFirstName, Authentication.LogginSchool.AdminLastName);
            }
            obj_log.CreatedDateTime = DateTime.Now;
            DB.ISUserActivities.Add(obj_log);
            DB.SaveChanges();
        }
        public static void AddLogs(string Text, int SchoolID, int CreatedBy, string UserName, string DocumentCategory)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISUserActivity obj_log = new ISUserActivity();
            obj_log.LogText = Text;
            obj_log.DocumentCategory = DocumentCategory;
            obj_log.Active = true;
            obj_log.Deleted = true;
            obj_log.SchoolID = SchoolID;
            obj_log.CreatedBy = CreatedBy;
            obj_log.UserName = UserName;
            obj_log.CreatedDateTime = DateTime.Now;
            DB.ISUserActivities.Add(obj_log);
            DB.SaveChanges();
        }

        public List<MISUserActivity> UserActivity(int SchoolID, string DateFrom, string DateTo, string LogName, string UpdatedBy, string OrderBy, string SortBy)
        {

            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISUserActivity> objList = (from item in DB.ISUserActivities.Where(p => p.Deleted == true && p.SchoolID == SchoolID).ToList()
                                             select new MISUserActivity
                                             {
                                                 ID = item.ID,
                                                 LogText = item.LogText,
                                                 ShortLogText = item.LogText.Substring(0, 40),
                                                 SchoolID = item.SchoolID,
                                                 SchoolName = item.ISSchool.Name,
                                                 CreatedBy = item.CreatedBy,
                                                 CreatedDateTime = item.CreatedDateTime,
                                                 StrDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt"),
                                                 UserName = item.UserName,
                                                 DocumentCategory = item.DocumentCategory,
                                             }).ToList();
            try
            {
                if (DateFrom != "")
                {
                    string dates = DateFrom;
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
                    DateTime dt1 = Convert.ToDateTime(Format);
                    objList = objList.Where(p => p.CreatedDateTime.Value.Date >= dt1.Date).ToList();
                }
                if (DateTo != "")
                {
                    string dates = DateTo;
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
                    DateTime dt2 = Convert.ToDateTime(Format);
                    objList = objList.Where(p => p.CreatedDateTime.Value.Date <= dt2.Date).ToList();
                }
                if (LogName != "0")
                {
                    objList = objList.Where(p => p.DocumentCategory == LogName).ToList();
                }
                //if (DocCategory != "")
                //{
                //    objList = objList.Where(p => p.LogText.Contains(DocCategory)).ToList();
                //}
                if (UpdatedBy != "0")
                {
                    int ID = Convert.ToInt32(UpdatedBy);
                    objList = objList.Where(p => p.CreatedBy == ID).ToList();
                }
                if (OrderBy == "ASC")
                {
                    if (SortBy == "LogName")
                    {
                        objList = objList.OrderBy(p => p.LogText).ToList();
                    }
                    else if (SortBy == "UpdatedBy")
                    {
                        objList = objList.OrderBy(p => p.UserName).ToList();
                    }
                    else
                    {
                        objList = objList.OrderBy(p => p.CreatedDateTime).ToList();
                    }
                }
                else
                {

                    if (SortBy == "LogName")
                    {
                        objList = objList.OrderByDescending(p => p.LogText).ToList();
                    }
                    else if (SortBy == "UpdatedBy")
                    {
                        objList = objList.OrderByDescending(p => p.UserName).ToList();
                    }
                    else
                    {
                        objList = objList.OrderByDescending(p => p.CreatedDateTime).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
            return objList;
        }
    }

}
