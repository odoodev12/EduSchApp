using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web.Teacher
{
    public partial class ClassReport : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindDropdown();
                if (Session["drpClass3"] != null)
                {
                    drpClass.SelectedValue = Session["drpClass3"].ToString();
                }
                BindList(drpClass.SelectedValue);
            }
        }
        private void BindDropdown()
        {
            int ID = Authentication.LogginTeacher.ID;
            int SchoolID = DB.ISTeachers.SingleOrDefault(p => p.ID == ID).SchoolID;
            drpClass.DataSource = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList()
                                   join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                                   select new ISClass
                                   {
                                       ID = item2.ID,
                                       Name = item2.Name
                                   }).ToList().OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList(); //DB.ISTeacherClassAssignments.Where(p => p.Deleted == true && p.Active == true && p.TeacherID == ID).ToList();
            drpClass.DataTextField = "Name";
            drpClass.DataValueField = "ID";
            drpClass.DataBind();
            //drpClass.Items.Insert(0, new ListItem { Text = "7", Value = "0" });
        }
        private void BindList(string ClassId)
        {
            Session["PickObjectlist"] = null;
            int TID = Authentication.LogginTeacher.ID;
            if (ClassId != "")
            {
                int ClassID = Convert.ToInt32(drpClass.SelectedValue);
                ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.ID == ClassID);

                ClassManagement objClassManagement = new ClassManagement();
                List<MViewStudentPickUp> objList = objClassManagement.ClassReport(ClassID, TID, "");
                Session["PickObjectlist"] = objList.ToList();

                if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    Averageclass.Style.Add("display", "none");

                    //foreach (var item in objList.ToList())
                    //{
                    //    ISAttendance objs = DB.ISAttendances.SingleOrDefault(p => p.StudentID == item.StudentID && p.Status.Contains("Present") && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now));
                    //    if (objs == null)
                    //    {

                    //        {
                    //            var itemToRemove = objList.FirstOrDefault(r => r.ID == item.ID);
                    //            if (itemToRemove != null)
                    //                objList.Remove(item);
                    //        }
                    //    }
                    //}
                }
                else
                {
                    Averageclass.Style.Add("display", "block");

                    if (ObjClass.Name.Contains("Office Class(Office)") || ObjClass.Name.Contains("Club Class(Club)"))
                        lblPickup.Text = "Not Applicable";
                    else
                    {
                        double? Count = objList.Sum(p => p.StudentPickUpAverage);
                        double Counts = objList.Where(r => r.StudentPickUpAverage > 0).ToList().Count;
                        if (Count != 0)
                        {
                            double? Avg = Count / Counts;
                            lblPickup.Text = String.Format("{0:N2}", Avg);
                        }
                        else
                        {
                            lblPickup.Text = "0.00";
                        }
                    }
                }




                ListView1.DataSource = objList.OrderBy(r => r.StudentName).ToList();
                ListView1.DataBind();
                //if (ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Office || ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.Club || ObjClass.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool /*|| ObjClass.Name.Contains("Outside Class")*/)
                //{
                //    if (objList.Count <= 0)
                //    {
                //        AlertMessageManagement.ServerMessage(Page, "This Class do not have Permanent Student. Please Click View Class Daily Report to view daily reports for this Class", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //    }
                //}
            }
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpClass.SelectedValue != "0")
            {
                Session["drpClass3"] = null;
                BindList(drpClass.SelectedValue);
                Session["drpClass3"] = drpClass.SelectedValue;
            }
        }
    }
}