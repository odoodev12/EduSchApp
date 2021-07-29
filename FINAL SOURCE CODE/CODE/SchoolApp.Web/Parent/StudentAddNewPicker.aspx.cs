using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class StudentAddNewPicker : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            if (Request.QueryString != null)
            {
                if (!IsPostBack)
                {

                    BindList();
                }
            }
        }
        private void BindList()
        {
            int ID = Authentication.LogginParent.ID;
            int SID = Convert.ToInt32(Request.QueryString["ID"]);

            var iSStudent = DB.ISStudents.Where(r => r.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() ||
                                                     r.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()).ToList();


            var iSStudentList = iSStudent.Select(r=>r.ID).ToList();

            var allEmailId = new List<string>();
            allEmailId.AddRange(iSStudent.Where(r => !string.IsNullOrEmpty(r.ParantEmail1)).Select(r => r.ParantEmail1).ToList());
            allEmailId.AddRange(iSStudent.Where(r => !string.IsNullOrEmpty(r.ParantEmail2)).Select(r => r.ParantEmail2).ToList());
            allEmailId = allEmailId.Distinct().Select(r=>r.ToLower()).ToList();

            List<MISPICKER> obj = (from item in DB.ISPickers.Where(p =>  allEmailId.Contains(p.CreatedByEmail.ToLower())  && p.Deleted == true).ToList() //p.StudentID == ID && 
                                   select new MISPICKER
                                   {
                                       ID = item.ID,
                                       PickerName = item.FirstName + " " + item.LastName,
                                       Email = item.Email,
                                       Phone = item.Phone,
                                       Photo = item.Photo,
                                       ActiveStatus = item.Active == true ? "Active" : "Inactive",
                                       ParentName = item.CreatedByName,
                                       //DB.ISPickerAssignments.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == item.StudentID && p.RemoveChildStatus == 0 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))) != null ? DB.ISPickerAssignments.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == item.StudentID && p.RemoveChildStatus == 0 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))).StudentAssignBy : item.CreatedByName,
                                       StudentID = item.StudentID,
                                       Active = item.Active,
                                       CreatedByEmail = item.CreatedByEmail,
                                       IsPickerCreatedByMe = item.CreatedByEmail.ToLower() == Authentication.LoginParentEmail.ToLower(),
                                   }).ToList();

           

            PickerManagement objPickerManagement = new PickerManagement();
            ////////List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(SID);
            ////////List<int> objList1 = (from item in DB.ISPickers.Where(p => p.Deleted == true).ToList()
            ////////                      join item1 in objList2.ToList() on item.ID equals item1.PickerId
            ////////                      select ID = item.ID).ToList();
            ////////List<MISPICKER> objList = obj.Where(p => !objList1.Contains(p.ID)).ToList();
            ///
            List<MISPICKER> obj1 = new List<MISPICKER>();
            foreach (var item in obj)
            {
                if (!string.IsNullOrEmpty(item.CreatedByEmail) && item.CreatedByEmail.ToLower() != Authentication.LoginParentEmail.ToLower())
                {
                    if (DB.ISPickerAssignments.Where(r => r.PickerId == item.ID && iSStudentList.Contains(r.StudentID.Value) && r.RemoveChildStatus == 0).Count() > 0)
                    {
                        obj1.Add(item);
                    }
                }
                else
                    obj1.Add(item);
            }


            List<int> listOfAssignedPickerIds = new List<int>();

            //string studentName = DB.ISStudents.FirstOrDefault(r => r.ID == SID).StudentName;

            //List<int> studentIdList = DB.ISStudents.Where(r => r.StudentName == studentName && (r.ParantEmail1 == Authentication.LoginParentEmail || r.ParantEmail2 == Authentication.LoginParentEmail)).Select(r => r.ID).ToList();

            //listOfAssignedPickerIds = DB.ISPickerAssignments.Where(r => studentIdList.Contains(r.StudentID.Value) && r.RemoveChildStatus == 0).Select(r => r.PickerId.Value).ToList();
            listOfAssignedPickerIds = DB.ISPickerAssignments.Where(r => r.StudentID.Value == SID && r.RemoveChildStatus == 0).Select(r => r.PickerId.Value).ToList();

            /// this is for assignment logic if no assign found then should remove to shown
            //foreach (var item in obj)
            //{
            //    if (!item.IsPickerCreatedByMe)
            //    {
            //        if (DB.ISPickerAssignments.Where(r => studentIdList.Contains(r.StudentID.Value) && item.ID == r.PickerId.Value && r.RemoveChildStatus == 0).Count() == 0)
            //            listOfAssignedPickerIds.Add(item.ID);
            //    }
            //}



            List<MISPICKER> objList = obj1.Where(p => !listOfAssignedPickerIds.Contains(p.ID)).ToList();
                                      //obj1.Where(p => !p.PickerName.Contains("(")).ToList().OrderBy(r => r.PickerName).ToList();
                                      //obj.Where(p => !listOfAssignedPickerIds.Contains(p.ID)).ToList();
            ListView1.DataSource = objList.OrderBy(r=>r.PickerName).ToList();
            ListView1.DataBind();


        }
        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)ListView1.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList();
        }
        protected void Clear()
        {
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnAdd")
            {
                int SID = Convert.ToInt32(Request.QueryString["ID"]);
                int PID = Convert.ToInt32(e.CommandArgument);
                if (DB.ISPickers.Where(p => p.ID == PID && p.Active == true && p.Deleted == true).ToList().Count > 0)
                {
                    ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == SID && p.Deleted == true);

                    var findMultipleStudentList = DB.ISStudents.Where(p => p.StudentName.ToLower() == _Student.StudentName.ToLower() &&
                (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()) && p.Deleted == true).ToList();

                    var allEmailId = new List<string>();
                    allEmailId.AddRange(findMultipleStudentList.Where(r => !string.IsNullOrEmpty(r.ParantEmail1)).Select(r => r.ParantEmail1).ToList());
                    allEmailId.AddRange(findMultipleStudentList.Where(r => !string.IsNullOrEmpty(r.ParantEmail2)).Select(r => r.ParantEmail2).ToList());

                    allEmailId = allEmailId.Distinct().Select(r=>r.ToLower()).ToList();

                    ISPicker _Picker = DB.ISPickers.FirstOrDefault(p => p.ID == PID && p.Deleted == true);

                    if (allEmailId.Contains(_Picker.CreatedByEmail.ToLower()))
                    {
                        //List<int> studentIdList = DB.ISStudents.Where(r => r.StudentName == _Student.StudentName && (r.ParantEmail1 == Authentication.LoginParentEmail || r.ParantEmail2 == Authentication.LoginParentEmail)).Select(r => r.ID).ToList();

                        //foreach (var studId in studentIdList)
                        {
                            ISPickerAssignment obj = new ISPickerAssignment();
                            obj.PickerId = PID;
                            obj.StudentID = SID;
                            obj.RemoveChildStatus = 0;
                            obj.StudentAssignBy = Authentication.LoginParentName;
                            DB.ISPickerAssignments.Add(obj);
                            DB.SaveChanges();
                        }
                        SendEmailManage(_Student, _Picker);

                        Clear();
                        AlertMessageManagement.ServerMessage(Page, "Picker Assigned Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                        string schoolName = DB.ISSchools.FirstOrDefault(r => r.ID == _Student.SchoolID).Name;

                        Response.Redirect("StudentPicker.aspx?ID=" + SID + "&SchoolName=" + schoolName + "&StudentName=" + _Student.StudentName);
                    }
                    else
                    {
                        AlertMessageManagement.ServerMessage(Page, "This Picker cannot be assigned to children NOT linked to Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "This Picker is Inactive. Please activate Picker to Add to Child.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
        }
        public void SendEmailManage(ISStudent _Student, ISPicker _Picker)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", assigned to " + _Student.StudentName + @" by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Added : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Updated : " + DateTime.Now.ToString("hh:mm tt") + @"
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", assigned to " + _Student.StudentName + @" by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Added : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Updated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Add Picker Notification", AdminBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Add Picker Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox obj = (CheckBox)sender;
            int ID = Convert.ToInt32(((HiddenField)obj.Parent.FindControl("chkID")).Value);
            ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
            if (objPicker != null)
            {
                objPicker.Active = obj.Checked == true ? true : false;
                DB.SaveChanges();
                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                string Status = obj.Checked == true ? "Activated" : "Deactivated";
                SendADEmailManage(_Student, objPicker, Status);
                if (obj.Checked == true)
                {
                    AlertMessageManagement.ServerMessage(Page, "Picker Activated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                else
                {
                    List<ISPickerAssignment> ObjPickerAssi = DB.ISPickerAssignments.Where(p => p.PickerId == ID).ToList();
                    if (ObjPickerAssi.Count > 0)
                    {
                        DB.ISPickerAssignments.RemoveRange(ObjPickerAssi);
                        DB.SaveChanges();
                    }
                    AlertMessageManagement.ServerMessage(Page, "Picker Deactivated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                BindList();
            }
        }
        public void SendADEmailManage(ISStudent _Student, ISPicker _Picker, string Status)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", " + Status + @" for your child/children by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment Time : " + DateTime.Now.ToString("hh:mm tt") + @"
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", " + Status + @" for your child/children by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment Time : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    AdminBody += reader.ReadToEnd();
                }
                AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Activate/Deactivate Picker Notification", AdminBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Activate/Deactivate Picker Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }
}