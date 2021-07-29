using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web.Teacher
{
    public partial class Pickup : System.Web.UI.Page
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
                if (Session["drpClass4"] != null)
                {
                    drpClass.SelectedValue = Session["drpClass4"].ToString();
                }

                if (Session["drpStudentName"] != null)
                {
                    drpStudentName.SelectedValue = Session["drpStudentName"].ToString();
                }

                BindList(drpClass.SelectedValue, "");
                lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (Session["Picked"] != null)
                {
                    if ((bool)Session["Picked"] == true)
                    {
                        Session["Picked"] = false;
                        AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
            }
            Session["chkPicked"] = chkPicked.Checked;
        }

        protected override void OnPreRender(EventArgs e)
        {
            //Work and It will assign the values to label.  
            Session["chkPicked"] = chkPicked.Checked;
            Session["IsStandardClass"] = !(drpClass.SelectedItem.Text.Contains("Office Class(Office)") ||
                                            //drpClass.SelectedItem.Text.Contains("Local Class") ||
                                            //drpClass.SelectedItem.Text.Contains("Outside Class") ||
                                            drpClass.SelectedItem.Text.Contains("After School") ||
                                            drpClass.SelectedItem.Text.Contains("Club Class(Club)"));
        }

        private void BindDropdown()
        {
            int ID = Authentication.LogginTeacher.ID;
            int SchoolID = DB.ISTeachers.SingleOrDefault(p => p.ID == ID).SchoolID;
            var result = (from item in DB.ISTeacherClassAssignments.Where(p => p.TeacherID == ID && p.Active == true).ToList()
                          join item2 in DB.ISClasses.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SchoolID && !p.Name.Contains("(After School Ex)")) on item.ClassID equals item2.ID
                          select new ISClass
                          {
                              ID = item2.ID,
                              Name = item2.Name
                          }).ToList();

            drpClass.DataSource = result.OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            drpClass.DataTextField = "Name";
            drpClass.DataValueField = "ID";
            drpClass.DataBind();
        }

        private void BindList(string ClassID, string Dates, int studentId = 0)
        {
            if (ClassID != "")
            {
                Session["Pickuplist"] = null;
                int ClasID = Convert.ToInt32(ClassID);
                Session["drpClass4"] = ClassID;
                PickupManagement objPickupManagement = new PickupManagement();
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                DateTime dt = DateTime.Now;
                if (Dates != "")
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

                List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    objList = objPickupManagement.PickupList(Authentication.LogginTeacher.ID, ClasID, Dates).OrderByDescending(p => p.PickStatus).ToList();
                }
                else
                {
                    objList = objPickupManagement.PickupsList(ClasID, Dates).OrderByDescending(p => p.PickStatus).ToList();
                }

                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    foreach (var objT in objList)
                        if (objT.PickStatus == "Not Marked")
                            objT.PickStatus = "Weekend (School Closed)";





                if (studentId == 0)
                {
                    drpStudentName.DataSource = objList.OrderBy(r => r.StudentName).ToList();
                    drpStudentName.DataValueField = "StudentId";
                    drpStudentName.DataTextField = "StudentName";
                    drpStudentName.DataBind(); drpStudentName.Items.Insert(0, new ListItem { Text = "Select Student Name", Value = "0" });

                }
                else
                {
                    objList = objList.Where(r => r.StudentID == Convert.ToInt32(drpStudentName.SelectedValue)).ToList();
                }


                lstPickUp.DataSource = objList.OrderBy(r => r.StudentName).OrderByDescending(r => (r.PickStatus == "Not Marked" || r.PickStatus.StartsWith("Weekend") || r.PickStatus.StartsWith("UnPicked"))).ToList();
                lstPickUp.DataBind();
                Session["Pickuplist"] = objList.ToList();

                BindCheckBox();
                Session["chkPicked"] = chkPicked.Checked;
            }
        }

        private void BindCheckBox()
        {
            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
            {
                int Class = Convert.ToInt32(drpClass.SelectedValue);
                DateTime dt = DateTime.Now;

                ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                if (_class != null)
                {
                    List<MISStudent> objList = new List<MISStudent>();
                    //if (_class.Name.Contains("Outside Class"))
                    //{
                    //    objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                    //               select new MISStudent
                    //               {
                    //                   ID = item.ID,
                    //                   StudentName = item.StudentName,
                    //                   Photo = item.Photo,
                    //                   CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                    //                   ClassID = item.ClassID
                    //               }).ToList();
                    //}
                    //else
                    {
                        objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt) && p.Deleted == true).ToList()
                                   select new MISStudent
                                   {
                                       ID = item.ID,
                                       StudentName = item.StudentName,
                                       Photo = item.Photo,
                                       CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                       ClassID = item.ClassID
                                   }).ToList();
                    }
                    var sids = ((List<MViewStudentPickUp>)Session["Pickuplist"]).Select(p => p.StudentID).ToList();
                    objList = objList.Where(p => sids.Contains(p.ID)).ToList();
                    foreach (var item in objList.ToList())
                    {
                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true);
                        if (objs != null)
                        {
                            var itemToRemove = objList.Single(r => r.ID == item.ID);
                            objList.Remove(itemToRemove);
                        }
                    }
                    if (objList.Count == 0)
                    {
                        ISCompletePickupRun ObjPickUpRun = DB.ISCompletePickupRuns.SingleOrDefault(p => p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                        if (ObjPickUpRun != null)
                        {
                            chkPicked.Checked = true;
                        }
                        else
                        {
                            chkPicked.Checked = false;
                        }
                    }
                    else
                    {
                        chkPicked.Checked = false;
                    }
                }
            }
            else
            {
                int Class = Convert.ToInt32(drpClass.SelectedValue);
                //List<MISStudent> objList = (from item in ((List<MViewStudentPickUp>)Session["Pickuplist"])
                //                            select new MISStudent
                //                            {
                //                                ID = Convert.ToInt32(item.StudentID),
                //                                StudentName = item.StudentName,
                //                                Photo = item.Photo,
                //                                //CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                //                                ClassID = item.ClassID
                //                            }).ToList();
                List<MISStudent> objList = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                            select new MISStudent
                                            {
                                                ID = item.ID,
                                                StudentName = item.StudentName,
                                                Photo = item.Photo,
                                                CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                ClassID = item.ClassID
                                            }).ToList();
                DateTime dt = DateTime.Now;
                var sids = ((List<MViewStudentPickUp>)Session["Pickuplist"]).Select(p => p.StudentID).ToList();
                objList = objList.Where(p => sids.Contains(p.ID)).ToList();
                foreach (var item in objList.ToList())
                {
                    ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true);
                    if (objs != null)
                    {
                        var itemToRemove = objList.Single(r => r.ID == item.ID);
                        objList.Remove(itemToRemove);
                    }
                }
                if (objList.Count == 0)
                {
                    ISCompletePickupRun ObjPickUpRun = DB.ISCompletePickupRuns.SingleOrDefault(p => p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                    if (ObjPickUpRun != null)
                    {
                        chkPicked.Checked = true;
                    }
                    else
                    {
                        chkPicked.Checked = false;
                    }
                    //chkPicked.Checked = true;
                }
                else
                {
                    chkPicked.Checked = false;
                }
            }
        }

        protected void lstPickUp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstPickUp.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(drpClass.SelectedValue, "");
        }

        protected void drpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpClass4"] = null;
            if (drpClass.SelectedValue != "0")
            {
                Session["drpClass4"] = drpClass.SelectedValue;
                BindList(drpClass.SelectedValue, "", 0);
            }
            else
            {
                Session["drpClass4"] = null;
                BindList("0", lblDate.Text);
            }
        }

        protected void drpStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpStudentName"] = null;
            if (drpStudentName.SelectedValue != "0")
            {
                Session["drpStudentName"] = drpStudentName.SelectedValue;
                BindList(drpClass.SelectedValue, "", Convert.ToInt32(drpStudentName.SelectedValue));
            }
            else
            {
                Session["drpStudentName"] = null;
                BindList(drpClass.SelectedValue, "", 0);
            }
        }

        protected void chkPicked_CheckedChanged(object sender, EventArgs e)
        {
            //var time1 = (from s in DB.ISSchools
            //             join t in DB.ISTeachers on s.ID equals t.SchoolID
            //             where t.ID == Authentication.LogginTeacher.ID
            //             select new { s.ClosingTime }).ToList();
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);
            DateTime t1 = DateTime.Parse(_School.ClosingTime.Value.ToString());
            DateTime t2 = DateTime.Now;
            Session["chkPicked"] = chkPicked.Checked;
            //string str = string.Empty;
            //foreach (var item in time1)
            //{
            //    str = item.ClosingTime.ToString();
            //}
            //string[] authorsList = str.Split(' ');
            //str = authorsList[1] + " " + authorsList[0].ToString();
            //if (DateTime.Now.ToString("h:mm:ss tt") == str)
            if (TimeSpan.Compare(t1.TimeOfDay, t2.TimeOfDay) <= 0)
            {
                if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    if (chkPicked.Checked == true)
                    {
                        int ID = Authentication.LogginTeacher.ID;
                        int Class = Convert.ToInt32(drpClass.SelectedValue);
                        DateTime dt = DateTime.Now;

                        ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                        if (completepickup == null)
                        {

                            ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                            if (_class != null)
                            {
                                List<MISStudent> obj = new List<MISStudent>();
                                //if (_class.Name.Contains("Outside Class"))
                                //{
                                //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList()
                                //           select new MISStudent
                                //           {
                                //               ID = item.ID,
                                //               StudentName = item.StudentName,
                                //               Photo = item.Photo,
                                //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                //               ClassID = item.ClassID
                                //           }).ToList();
                                //}
                                //else
                                {
                                    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList()
                                           select new MISStudent
                                           {
                                               ID = item.ID,
                                               StudentName = item.StudentName,
                                               Photo = item.Photo,
                                               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                               ClassID = item.ClassID
                                           }).ToList();
                                }

                                var sids = ((List<MViewStudentPickUp>)Session["Pickuplist"]).Select(p => p.StudentID).ToList();
                                Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                                if (chkPicked.Checked == true)
                                {
                                    foreach (var item in obj.ToList())
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            var itemToRemove = obj.Single(r => r.ID == item.ID);
                                            obj.Remove(itemToRemove);
                                        }
                                    }
                                }
                                if (obj.Count == 0)
                                {
                                    var lists = (List<MViewStudentPickUp>)Session["Pickuplist"];
                                    foreach (var item in lists)
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            objs.CompletePickup = true;
                                            DB.SaveChanges();
                                            var student = DB.ISStudents.FirstOrDefault(r => r.ID == objs.StudentID);
                                            SendConfirmPickedStatusEmailManage(student);
                                        }
                                    }
                                    ISCompletePickupRun objcompletepickup = new ISCompletePickupRun();
                                    objcompletepickup.ClassID = Class;
                                    objcompletepickup.TeacherID = ID;
                                    objcompletepickup.Date = dt;
                                    objcompletepickup.Active = true;
                                    objcompletepickup.Deleted = false;
                                    objcompletepickup.CreatedBy = ID;
                                    objcompletepickup.CreatedDateTime = DateTime.Now;
                                    DB.ISCompletePickupRuns.Add(objcompletepickup);
                                    DB.SaveChanges();

                                    chkPicked.Checked = true;
                                    AlertMessageManagement.ServerMessage(Page, "All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                }
                                else
                                {
                                    chkPicked.Checked = false;
                                    AlertMessageManagement.ServerMessage(Page, "Not All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                            }
                        }
                        else
                        {

                            ISClass _class = DB.ISClasses.SingleOrDefault(p => p.ID == Class);
                            if (_class != null)
                            {
                                List<MISStudent> obj = new List<MISStudent>();
                                //if (_class.Name.Contains("Outside Class"))
                                //{
                                //    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList()
                                //           select new MISStudent
                                //           {
                                //               ID = item.ID,
                                //               StudentName = item.StudentName,
                                //               Photo = item.Photo,
                                //               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                //               ClassID = item.ClassID
                                //           }).ToList();
                                //}
                                //else
                                {
                                    obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true && DbFunctions.TruncateTime(p.StartDate) == DbFunctions.TruncateTime(dt)).ToList()
                                           select new MISStudent
                                           {
                                               ID = item.ID,
                                               StudentName = item.StudentName,
                                               Photo = item.Photo,
                                               CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                               ClassID = item.ClassID
                                           }).ToList();
                                }

                                var sids = ((List<MViewStudentPickUp>)Session["Pickuplist"]).Select(p => p.StudentID).ToList();
                                Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                                if (chkPicked.Checked == true)
                                {
                                    foreach (var item in obj.ToList())
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            var itemToRemove = obj.Single(r => r.ID == item.ID);
                                            obj.Remove(itemToRemove);
                                        }
                                    }
                                }
                                if (obj.Count == 0)
                                {
                                    var lists = (List<MViewStudentPickUp>)Session["Pickuplist"];
                                    foreach (var item in lists)
                                    {
                                        ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objs != null)
                                        {
                                            objs.CompletePickup = true;
                                            DB.SaveChanges();

                                            var student =DB.ISStudents.FirstOrDefault(r => r.ID == objs.StudentID);
                                            SendConfirmPickedStatusEmailManage(student);
                                        }
                                    }
                                    completepickup.ClassID = Class;
                                    completepickup.TeacherID = ID;
                                    completepickup.Date = dt;
                                    completepickup.Active = true;
                                    completepickup.Deleted = false;
                                    completepickup.ModifyBy = ID;
                                    completepickup.ModifyDateTime = DateTime.Now;
                                    DB.SaveChanges();

                                    chkPicked.Checked = true;
                                    AlertMessageManagement.ServerMessage(Page, "All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                }
                                else
                                {
                                    chkPicked.Checked = false;
                                    AlertMessageManagement.ServerMessage(Page, "Not All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                            }
                        }
                        BindList(drpClass.SelectedValue, "");
                    }
                    else
                    {
                        chkPicked.Checked = true;
                    }
                }
                else
                {
                    if (chkPicked.Checked == true)
                    {
                        int ID = Authentication.LogginTeacher.ID;
                        int Class = Convert.ToInt32(drpClass.SelectedValue);
                        DateTime dt = DateTime.Now;

                        ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.TeacherID == ID && p.ClassID == Class && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                        if (completepickup == null)
                        {
                            List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                    select new MISStudent
                                                    {
                                                        ID = item.ID,
                                                        StudentName = item.StudentName,
                                                        Photo = item.Photo,
                                                        CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                        ClassID = item.ClassID
                                                    }).ToList();
                            var sids = ((List<MViewStudentPickUp>)Session["Pickuplist"]).Select(p => p.StudentID).ToList();
                            Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                            if (chkPicked.Checked == true)
                            {
                                foreach (var item in obj.ToList())
                                {
                                    var itemToRemove = obj.Single(r => r.ID == item.ID);
                                    obj.Remove(itemToRemove);
                                }
                            }
                            if (obj.Count == 0)
                            {
                                var lists = (List<MViewStudentPickUp>)Session["Pickuplist"];
                                foreach (var item in lists)
                                {
                                    ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                    if (objs != null)
                                    {
                                        objs.CompletePickup = true;
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup objPickup = new ISPickup();
                                        objPickup.StudentID = item.StudentID;
                                        objPickup.ClassID = item.ClassID;
                                        objPickup.TeacherID = Authentication.LogginTeacher.ID;
                                        objPickup.PickTime = DateTime.Now;
                                        objPickup.PickDate = DateTime.Now;
                                        objPickup.PickStatus = "Not Marked";
                                        objPickup.CompletePickup = true;
                                        DB.ISPickups.Add(objPickup);
                                        DB.SaveChanges();
                                    }

                                    var student = DB.ISStudents.FirstOrDefault(r => r.ID == item.StudentID);
                                    SendConfirmPickedStatusEmailManage(student);
                                }
                                ISCompletePickupRun objcompletepickup = new ISCompletePickupRun();
                                objcompletepickup.ClassID = Class;
                                objcompletepickup.TeacherID = ID;
                                objcompletepickup.Date = dt;
                                objcompletepickup.Active = true;
                                objcompletepickup.Deleted = false;
                                objcompletepickup.CreatedBy = ID;
                                objcompletepickup.CreatedDateTime = DateTime.Now;
                                DB.ISCompletePickupRuns.Add(objcompletepickup);
                                DB.SaveChanges();

                                chkPicked.Checked = true;
                                AlertMessageManagement.ServerMessage(Page, "All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            }
                            else
                            {
                                chkPicked.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Not All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                        }
                        else
                        {


                            List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ClassID == Class && p.Deleted == true).ToList()
                                                    select new MISStudent
                                                    {
                                                        ID = item.ID,
                                                        StudentName = item.StudentName,
                                                        Photo = item.Photo,
                                                        CreatedDateTime = item.CreatedDateTime != null ? item.CreatedDateTime.Value.Date : DateTime.Now,
                                                        ClassID = item.ClassID
                                                    }).ToList();
                            var sids = ((List<MViewStudentPickUp>)Session["Pickuplist"]).Select(p => p.StudentID).ToList();
                            Session["ObjList"] = obj.Where(p => sids.Contains(p.ID)).ToList();
                            if (chkPicked.Checked == true)
                            {
                                foreach (var item in obj.ToList())
                                {
                                    var itemToRemove = obj.Single(r => r.ID == item.ID);
                                    obj.Remove(itemToRemove);
                                }
                            }
                            if (obj.Count == 0)
                            {
                                var lists = (List<MViewStudentPickUp>)Session["Pickuplist"];
                                foreach (var item in lists)
                                {
                                    ISPickup objs = DB.ISPickups.SingleOrDefault(p => p.StudentID == item.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                    if (objs != null)
                                    {
                                        objs.CompletePickup = true;
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup objPickup = new ISPickup();
                                        objPickup.StudentID = item.StudentID;
                                        objPickup.ClassID = item.ClassID;
                                        objPickup.TeacherID = Authentication.LogginTeacher.ID;
                                        objPickup.PickTime = DateTime.Now;
                                        objPickup.PickDate = DateTime.Now;
                                        objPickup.PickStatus = "Not Marked";
                                        objPickup.CompletePickup = true;
                                        DB.ISPickups.Add(objPickup);
                                        DB.SaveChanges();
                                    }

                                    var student = DB.ISStudents.FirstOrDefault(r => r.ID == item.StudentID);
                                    SendConfirmPickedStatusEmailManage(student);
                                }
                                chkPicked.Checked = true;
                                completepickup.ClassID = Class;
                                completepickup.TeacherID = ID;
                                completepickup.Date = dt;
                                completepickup.Active = true;
                                completepickup.Deleted = false;
                                completepickup.ModifyBy = ID;
                                completepickup.ModifyDateTime = DateTime.Now;
                                DB.SaveChanges();
                                AlertMessageManagement.ServerMessage(Page, "All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            }
                            else
                            {
                                chkPicked.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Not All Students have been Picked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                        }
                        BindList(drpClass.SelectedValue, "");
                    }
                    else
                    {
                        chkPicked.Checked = true;
                    }
                }
            }
            else
            {
                chkPicked.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "This slide can only be activated after school closing time", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
        }

        public void SendConfirmPickedStatusEmailManage(ISStudent _Student)
        {
            EmailManagement _EmailManagement = new EmailManagement();

            try
            {
                if ((_Student.EmailAfterConfirmPickUp == null && _Student.ISSchool.isNotificationPickup == true) || (_Student.EmailAfterConfirmPickUp.HasValue && _Student.EmailAfterConfirmPickUp.Value == true))
                {
                    ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);

                    string AdminBody = string.Empty;
                    string SuperwisorBody = string.Empty;
                    string tblSupbody = string.Empty;
                    string tblAdminBody = string.Empty;
                    tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be informed of the Picked Status of Your Child Today. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Message Sent by : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any Enquiries, please contact or message " + _School.Name + @"
                                   </td>
                                </tr></table>";
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        SuperwisorBody += reader.ReadToEnd();
                    }
                    SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                    tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be informed of the Picked Status of Your Child Today. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Message Sent by : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + _School.Name + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any Enquiries, please contact or message " + _School.Name + @"
                                   </td>
                                </tr></table>";

                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        AdminBody += reader.ReadToEnd();
                    }
                    AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                    _EmailManagement.SendEmail(_Student.ParantEmail1, "Confirm Pickup Alert Notification", AdminBody);
                    if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                    {
                        _EmailManagement.SendEmail(_Student.ParantEmail2, "Confirm Pickup Alert Notification", SuperwisorBody);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }

}