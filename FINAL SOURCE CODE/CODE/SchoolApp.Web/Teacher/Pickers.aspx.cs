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

namespace SchoolApp.Web.Teacher
{
    public partial class Pickers : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Picked"] = false;
            if (!Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.TeacherAuthorizePage());
            }
            if (!IsPostBack)
            {
                Session["ID"] = null;
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    Session["ID"] = ID;

                    //DateTime dt = DateTime.Now;
                    ISStudent student = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                    if (student != null)
                    {
                        //ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == student.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                        //if (completepickup != null)
                        //{
                        //    chbMarkAbsent.Enabled = false;
                        //    chbSendAftSchool.Enabled = false;
                        //    chbSendDriver.Enabled = false;
                        //}
                        BindList();
                    }
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (Session["ID"] != null)
            {
                int ID = Convert.ToInt32(Session["ID"]);
                List<ISPickUpMessage> ObjMesssges = DB.ISPickUpMessages.Where(p => p.SendID == ID && p.Viewed == false && p.Deleted == true).ToList();
                foreach (var item in ObjMesssges)
                {
                    item.Viewed = true;
                    DB.SaveChanges();
                }
                Session["ID"] = null;
            }
            // code to be executed on user leaves the page    
        }

        private void BindList()
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            Session["Objectlist"] = null;
            int TID = Authentication.LogginTeacher.ID;
            DateTime dt = DateTime.Now;
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);
            if (_School != null)
            {
                if (_School.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    PanelAfterSchool.Visible = false;
                }
                else
                {
                    PanelAfterSchool.Visible = true;
                }
            }

            List<ISHoliday> objHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.LogginTeacher.SchoolID && p.Active == true).ToList();

            bool isTodayIsHoliday = (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0);
            string holidayName = OperationManagement.GetTodayHolidayStatus(objHoliday);

            List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                 select new MViewStudentPickUp
                                                 {
                                                     ID = item.ID == null ? 0 : item.ID,
                                                     StudentID = item.StudID,
                                                     StudentName = item.StudentName,
                                                     StudentPic = item.Photo,
                                                     PickStatus = String.IsNullOrEmpty(item.PickStatus) ? isTodayIsHoliday ? holidayName : "Not Marked" : item.PickStatus,
                                                     Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                                                     Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                                                     PickerID = item.PickerID == null ? 0 : item.PickerID,
                                                     PickerName = item.PickerID == null ? "" : DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                                                     Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                     ClassID = item.ClassID,
                                                     ParantEmail1 = item.ParantEmail1,
                                                     ParantEmail2 = item.ParantEmail2,
                                                     SchoolID = item.SchoolID,
                                                     //IsCodeRequired = 
                                                     //objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                                     //ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                                 }).ToList();
            if (objLists != null)
            {
                imgStudent.ImageUrl = "../" + objLists[0].StudentPic;
                litStudentName.Text = objLists[0].StudentName;
                litStatus.Text = objLists[0].PickStatus == null ? "Not Marked" : objLists[0].PickStatus;
                //if (objLists[0].PickStatus == "Office" || objLists[0].PickStatus == "Mark as Absent" || objLists[0].PickStatus == "After-School" || objLists[0].PickStatus == "After-School-Ex" || Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                //{
                //    chbMarkAbsent.Enabled = false;
                //    chbSendAftSchool.Enabled = false;
                //    chbSendDriver.Enabled = false;
                //}
                int SID = Convert.ToInt32(objLists[0].SchoolID);
                ISSchool ObSchool = DB.ISSchools.SingleOrDefault(p => p.ID == SID);

                if (litStatus.Text == "Not Marked")
                {
                    litStatus.Text = OperationManagement.GetDefaultPickupStatus(); ;

                }
                if (Session["drpClass4"] != null)
                {
                    int CID = Convert.ToInt32(Session["drpClass4"]);
                    //if (DB.ISClasses.Where(p => p.ID == CID && p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office).Count() > 0 ||
                    //    DB.ISClasses.Where(p => p.ID == CID && p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club).Count() > 0 ||
                    //    DB.ISClasses.Where(p => p.ID == CID && p.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool).Count() > 0)
                    //{
                    //    PanelUnPick.Visible = false;
                    //}
                    //else
                    //{
                    //    PanelUnPick.Visible = true;
                    //}
                }
                AID.HRef = "../" + objLists[0].StudentPic;
                AID.Attributes["data-title"] = objLists[0].StudentName;
            }
            ISPickUpMessage ObjMesssge = DB.ISPickUpMessages.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SendID == ID && DbFunctions.TruncateTime(p.CreatedDateTime.Value) == DbFunctions.TruncateTime(DateTime.Now) && p.Deleted == true);
            if (ObjMesssge != null)
            {
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ObjMesssge.SendID && p.Deleted == true);
                string parentMessage = "<font color='red'>" + ObjMesssge.Message + " from " + ObjMesssge.SenderName + "</font>";


                litParentMessage.Text = "<b>Your Student Pickup Reason:</b>" + parentMessage + " <i><b>Date: </b></i>" + ObjMesssge.CreatedDateTime.Value.ToString();
            }
            else
            {
                litParentMessage.Text = "No Message to display";
            }

            PickerManagement objPickerManagement = new PickerManagement();
            List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(ID);
            List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(ID); //.Where(p => p.PickerType != 1 || p.PickerName.Contains("(")).ToList();

            if (objList.Count == 0)
            {
                HID.Value = "True";
            }
            else
            {
                objList2.ForEach(r => r.Count = (r.PickerName.Contains("(Father)") ||
                                                r.PickerName.Contains("(Mother)") ||
                                                r.PickerName.Contains("Guardian")) ||
                                                //r.PickerName.Contains("Friends")) ||
                                                (string.IsNullOrEmpty(r.CreatedByEmail) && string.IsNullOrEmpty(r.CreatedByName)));
                HID.Value = "False";
            }


            foreach (MISPickerAssignment item in objList2)
            {
                if (objList.Where(p => p.ID == item.ID).Count() <= 0)
                {
                    objList.Add(item);
                }
            }


            foreach (var item in objList)
            {
                int remainingDays = 0;
                ISPicker iSPicker = DB.ISPickers.SingleOrDefault(r => r.ID == item.PickerId);
                bool isValid = OperationManagement.GetCalcTodayDateWithGivenDate(iSPicker.CreatedDateTime.Value, 7, ref remainingDays);
                item.BorderColorCode = "";

                var pickerAssignMentList = DB.ISPickerAssignments.FirstOrDefault(r => r.PickerId == item.PickerId &&
                r.StudentID == item.StudentID && r.PickCodeExDate.HasValue && DbFunctions.TruncateTime(r.PickCodeExDate) == DbFunctions.TruncateTime(dt));



                if (item.StrDate == item.CurrentDate)
                    item.BorderColorCode = "border:1px solid #00cc00";
                else
                {
                    if (iSPicker.PickerType.Value == 1)
                    {
                        if (iSPicker.Photo == "Upload/user.jpg" && !isValid && remainingDays <= 0)
                        {
                            //shown border color red and confirm pickup button disabled.
                            item.BorderColorCode = "border:1px solid #cc0000";
                            item.IsRedZone = true;
                        }
                        else
                        {
                            if (pickerAssignMentList != null)
                            {
                                if (pickerAssignMentList.PickerCode.Length > 0)
                                    item.CodeRequiredText = "Code Required";
                            }

                            if (iSPicker.Photo == "Upload/user.jpg" && pickerAssignMentList == null)
                                item.CodeRequiredText = "Code or Image Required";
                        }
                    }
                }
            }

            lstPicker.DataSource = objList.ToList();
            lstPicker.DataBind();
            if (objList.Count <= 0)
            {
                lblNoPicker.Text = "No Picker Assigned to Child.";
            }
            Session["Objectlist"] = objList.ToList();
        }

        protected void btnUnpickAleart_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            int classID = Convert.ToInt32(Session["drpClass4"]);
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            DateTime tdate = DateTime.Now;
            bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
            ISSchool school = DB.ISSchools.SingleOrDefault(p => p.ID == objStudent.SchoolID && p.Deleted == true);

            DateTime schclosetime = Convert.ToDateTime(school.ClosingTime);
            DateTime schclose = new DateTime(tdate.Year, tdate.Month, tdate.Day, schclosetime.Hour, schclosetime.Minute, 0);

            if (schclose < tdate)
            {
                bool isAllowd = false;

                if (school.TypeID == 2 && isStandardClass && (litStatus.Text == "Not Marked" || litStatus.Text.Contains("(School Closed)")))
                {
                    isAllowd = true;
                }
                else if (school.TypeID == 2 && !isStandardClass && (litStatus.Text == "Office" || litStatus.Text == "Club" || litStatus.Text == "After-School"))
                {
                    isAllowd = true;
                }
                else if (school.TypeID == 1 && litStatus.Text == "UnPicked")
                {
                    isAllowd = true;
                }

                if (isAllowd)
                {
                    string StatusText = litStatus.Text;
                    List<ISPicker> _Pickers = DB.ISPickers.Where(p => p.EmergencyContact == true && p.Deleted == true && p.Active == true).ToList();
                    SendEmailManage(objStudent, StatusText, _Pickers);
                    AlertMessageManagement.ServerMessage(Page, "Send UnPick Alert successfully sent to parents and emergency contacts.", (int)AlertMessageManagement.MESSAGETYPE.info);
                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "Send UnPick Alert can only be sent for students with Pickup Status of UnPicked.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
            else
                AlertMessageManagement.ServerMessage(Page, "You cannot do send unpick alert as before school time is closed.", (int)AlertMessageManagement.MESSAGETYPE.warning);

            #region OlderCode
            //if (StatusText == "Mark as Absent")
            //{

            //    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
            //    EmailManagement objEmailManagement = new EmailManagement();
            //    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, "Mark as Absent", "Hello Dear Parents, Your Child is Absent today.", Authentication.LogginTeacher.Name, DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID).ISSchool.Name);
            //    objEmailManagement.SendEmail(ObSt.ParantEmail1, "Mark as Absent", Message);
            //    if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
            //    {
            //        objEmailManagement.SendEmail(ObSt.ParantEmail2, "Mark as Absent", Message);
            //    }
            //    Clear();
            //    AlertMessageManagement.ServerMessage(Page, "Message Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            //    Response.Redirect("Pickup.aspx");
            //}
            //else if (StatusText.Contains("After-School"))
            //{

            //    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
            //    EmailManagement objEmailManagement = new EmailManagement();
            //    string Subject = "";
            //    if (objStudent.ISClass.AfterSchoolType == "Internal")
            //    {
            //        Subject = "Internal After School";
            //    }
            //    else
            //    {
            //        Subject = "External After School";
            //    }
            //    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, Subject, "Hello Dear Parents, Your Child will sent to " + Subject + "", Authentication.LogginTeacher.Name, DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID).ISSchool.Name);
            //    objEmailManagement.SendEmail(ObSt.ParantEmail1, Subject, Message);
            //    if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
            //    {
            //        objEmailManagement.SendEmail(ObSt.ParantEmail2, Subject, Message);
            //    }


            //    Clear();
            //    AlertMessageManagement.ServerMessage(Page, "Message Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            //    Response.Redirect("Pickup.aspx");
            //}
            //else if (StatusText.Contains("Office"))
            //{

            //    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
            //    EmailManagement objEmailManagement = new EmailManagement();
            //    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, "Office", "Hello Dear Parents, Your Child will Send to Driver.", Authentication.LogginTeacher.Name, DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID).ISSchool.Name);
            //    objEmailManagement.SendEmail(ObSt.ParantEmail1, "Office", Message);
            //    if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
            //    {
            //        objEmailManagement.SendEmail(ObSt.ParantEmail2, "Office", Message);
            //    }
            //    Clear();
            //    Response.Redirect("Pickup.aspx");
            //}
            //else
            //{
            //    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.ID == objStudent.ID);
            //    EmailManagement objEmailManagement = new EmailManagement();
            //    string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear Parent Of {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> {3}<br>School : {4}<br>", objStudent.StudentName, "Not Marked", "Hello Dear Parents, Your Child is still UnPicked from School.", Authentication.LogginTeacher.Name, DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID).ISSchool.Name);
            //    objEmailManagement.SendEmail(ObSt.ParantEmail1, "Not Marked", Message);
            //    if (ObSt.ParantEmail2 != null && ObSt.ParantEmail2 != "")
            //    {
            //        objEmailManagement.SendEmail(ObSt.ParantEmail2, "Not Marked", Message);
            //    }
            //    Clear();
            //    Response.Redirect("Pickup.aspx");
            //}
            #endregion
        }
        public void SendEmailManage(ISStudent _Student, string PickUpStatus, List<ISPicker> lstPickers)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginTeacher != null ? Authentication.LogginTeacher.Name : "";
            string LoggedContact = Authentication.LogginTeacher != null ? Authentication.LogginTeacher.PhoneNo : "";
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
                                    Please be advised as parent, guardian or an emergency contact that  Child named above is yet to be " + PickUpStatus + @" from School. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        It is important to contact the us and arranged how the child will be picked.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please be aware that failure to do so may result in charges or the child being taken to the custody of the relevant child protection authority.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Message Sent by : <b>" + _School.Name + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Teacher Contact today : " + LoggedContact + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any enquiries, please contact " + _School.Name + @"
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
                                    Please be advised as parent, guardian or an emergency contact that  Child named above is yet to be " + PickUpStatus + @" from School. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        It is important to contact the us and arranged how the child will be picked.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please be aware that failure to do so may result inn charges or the child being taken to the custody of the relevant child protection authority.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Message Sent by : <b>" + _School.Name + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Teacher Contact today : " + LoggedContact + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any enquiries, please contact " + _School.Name + @"
                                   </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_Student.ParantEmail1, "Send UnPick Alert Notification", AdminBody);
            if (!String.IsNullOrEmpty(_Student.ParantEmail2))
            {
                _EmailManagement.SendEmail(_Student.ParantEmail2, "Send UnPick Alert Notification", SuperwisorBody);
            }
            foreach (var item in lstPickers)
            {
                string PickerBody = string.Empty;
                string tblPickerbody = string.Empty;
                tblPickerbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Please be advised as parent, guardian or an emergency contact that  Child named above is yet to be " + PickUpStatus + @" from School. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        It is important to contact the us and arranged how the child will be picked.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please be aware that failure to do so may result inn charges or the child being taken to the custody of the relevant child protection authority.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Message Sent by : <b>" + _School.Name + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Teacher Contact today : " + LoggedContact + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any enquiries, please contact " + _School.Name + @"
                                   </td>
                                </tr></table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PickerBody += reader.ReadToEnd();
                }
                PickerBody = PickerBody.Replace("{Body}", tblPickerbody);
                if (!String.IsNullOrEmpty(item.Email))
                {
                    _EmailManagement.SendEmail(item.Email, "Send UnPick Alert Notification", PickerBody);
                }
            }
        }
        protected void Clear()
        {
            chbMarkAbsent.Checked = false;
            chbSendAftSchool.Checked = false;
            chbSendDriver.Checked = false;
        }

        protected void lstPicker_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int selectedClassId = Convert.ToInt32(Session["drpClass4"]);
            if (e.CommandName == "btnConfirm")
            {
                bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
                TextBox txtConfirm = (TextBox)e.Item.FindControl("txtConfirm");
                CheckBox IDChk = (CheckBox)e.Item.FindControl("chkID");
                Label codeOrImageRequiredTxt = (Label)e.Item.FindControl("Label2");
                int ID = Convert.ToInt32(e.CommandArgument);
                DateTime dt = DateTime.Now;
                ISPickerAssignment objPickers = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID && (p.PickerCode != null || p.PickerCode != ""));
                ISStudent _student = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == _student.ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                     select new MViewStudentPickUp
                                                     {
                                                         ID = item.ID == null ? 0 : item.ID,
                                                         StudentID = item.StudID,
                                                         StudentName = item.StudentName,
                                                         PickStatus = String.IsNullOrEmpty(item.PickStatus) ? "Not Marked" : item.PickStatus,
                                                         Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                         ClassID = item.ClassID,
                                                         SchoolID = item.SchoolID,
                                                         PickerID = item.PickerID,
                                                     }).ToList();

                if (objLists[0].PickStatus == "Office")
                {
                    selectedClassId = DB.ISClasses.Where(r => r.Name == "Office Class(Office)" && r.SchoolID == Authentication.SchoolID).FirstOrDefault()?.ID ?? 0;
                }
                else if (objLists[0].PickStatus == "Club")
                {
                    selectedClassId = DB.ISClasses.Where(r => r.Name == "Club Class(Club)" && r.SchoolID == Authentication.SchoolID).FirstOrDefault()?.ID ?? 0;
                }

                #region Validation for Image required or generate code

                var pickerAssignMentList = DB.ISPickerAssignments.FirstOrDefault(r => r.ID == ID && r.PickCodeExDate.HasValue && DbFunctions.TruncateTime(r.PickCodeExDate) == DbFunctions.TruncateTime(dt));

                if (IsStudentIsFoundInUnPickedInAfterSchool(objPickers.StudentID.Value))
                {
                    AlertMessageManagement.ServerMessage(Page, "Same student might be UnPicked from another school.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    return;
                }

                else if (codeOrImageRequiredTxt.Text.Length > 0)
                {
                    ISPicker iSPicker = DB.ISPickers.FirstOrDefault(r => r.ID == objPickers.PickerId);

                    if (txtConfirm.Text == "" && iSPicker.Photo == "Upload/user.jpg")
                    {
                        AlertMessageManagement.ServerMessage(Page, "Please upload image or enter Valid Picker Code.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        return;
                    }
                    else if (txtConfirm.Text == "")
                    {
                        AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        return;
                    }
                }


                #endregion

                //if (objLists[0].PickStatus == "Picked")
                //{
                //    PickerManagement pickerManagement = new PickerManagement();
                //    var result = pickerManagement.GetPicker(objLists[0].PickerID.Value);
                //    AlertMessageManagement.ServerMessage(Page, $"{_student.StudentName} is already picked by {result.PickerName}", (int)AlertMessageManagement.MESSAGETYPE.warning);
                //}
                ISSchool school = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                if (school != null)
                {

                    if (school.isAttendanceModule == true)
                    {
                        var att = DB.ISAttendances.SingleOrDefault(p => p.StudentID == objPickers.StudentID && (p.Status == "Present" || p.Status == "Present(Late)") && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                        ISAttendance ObjAttendence = DB.ISAttendances.SingleOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                        if (ObjAttendence != null)
                        {
                            ISAttendance ObjAttendences = DB.ISAttendances.SingleOrDefault(p => p.StudentID == objPickers.StudentID && (p.Status == "Absent") && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                            if (ObjAttendences == null)
                            {
                                if (DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).PickerType == (int)EnumsManagement.PICKERTYPE.Individual && !DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).FirstName.Contains("("))
                                {
                                    List<ISPickup> objPicks = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && (p.PickStatus == "Office" || p.PickStatus == "Club") && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                    if (objPicks.Count > 0)
                                    {
                                        List<ISTeacherClassAssignment> ObjAssign = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Authentication.LogginTeacher.ID && (p.ISClass.Name.Contains("(Office)") || p.ISClass.Name.Contains("(Club)")) && p.ISClass.SchoolID == Authentication.SchoolID && p.Active == true).ToList();
                                        if (ObjAssign.Count > 0)
                                        {
                                            if (objPickers != null && objPickers.PickCodeExDate != null)
                                            {

                                                bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                                ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                                if (objPickers.PickerCode == txtConfirm.Text || isCodeIsOld)
                                                {


                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                    ISPickup pickupObject = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    if (objs.Count > 0)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));

                                                        if (pickup != null)
                                                        {
                                                            if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                {
                                                                    AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                }
                                                                else
                                                                {
                                                                    if (objLists[0].PickStatus == "After-School-Ex")
                                                                    {
                                                                        string Studentname = objLists[0].StudentName;
                                                                        ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                        if (_Student != null)
                                                                        {
                                                                            if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                            {
                                                                                List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                DB.ISPickups.RemoveRange(_Pickup);
                                                                                DB.SaveChanges();

                                                                                List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                foreach (var item in _Picker)
                                                                                {
                                                                                    List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                    DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                    DB.SaveChanges();
                                                                                }
                                                                                DB.ISPickers.RemoveRange(_Picker);
                                                                                DB.SaveChanges();

                                                                                DB.ISStudents.Remove(_Student);
                                                                                DB.SaveChanges();
                                                                            }
                                                                        }
                                                                    }
                                                                    int OfficeID = 0;
                                                                    if (Session["drpClass4"] != null)
                                                                    {
                                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                        if (_Class != null)
                                                                        {
                                                                            if (pickup.PickStatus == "Office")
                                                                                pickup.OfficeFlag = true;
                                                                            else if (pickup.PickStatus == "Club")
                                                                                pickup.ClubFlag = true;
                                                                        }

                                                                        if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                            pickup.AfterSchoolFlag = true;
                                                                    }
                                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                    {
                                                                        //pickup.AfterSchoolFlag = true;
                                                                    }
                                                                    pickup.PickTime = dt;
                                                                    pickup.PickDate = dt;
                                                                    pickup.PickerID = objPickers.PickerId;
                                                                    pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                    DB.SaveChanges();

                                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                                    SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                    Response.Redirect("Pickup.aspx");
                                                                }

                                                            }

                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPickers.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPickers.PickerId;
                                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                                            obj.PickTime = dt;
                                                            obj.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                //obj.AfterSchoolFlag = true;
                                                            }
                                                            obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Student, GetPickupStatus(_student.ClassID));
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }


                                                    }

                                                }
                                                else
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }

                                            }
                                            else
                                            {
                                                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                                ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                                if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                                {
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                        if (pickup != null)
                                                        {
                                                            if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                {
                                                                    AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                }
                                                                else
                                                                {
                                                                    if (objLists[0].PickStatus == "After-School-Ex")
                                                                    {
                                                                        string Studentname = objLists[0].StudentName;
                                                                        ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                        if (_Student != null)
                                                                        {
                                                                            if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                            {
                                                                                List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                DB.ISPickups.RemoveRange(_Pickup);
                                                                                DB.SaveChanges();

                                                                                List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                foreach (var item in _Picker)
                                                                                {
                                                                                    List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                    DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                    DB.SaveChanges();
                                                                                }
                                                                                DB.ISPickers.RemoveRange(_Picker);
                                                                                DB.SaveChanges();

                                                                                DB.ISStudents.Remove(_Student);
                                                                                DB.SaveChanges();
                                                                            }
                                                                        }
                                                                    }
                                                                    int OfficeID = 0;
                                                                    if (Session["drpClass4"] != null)
                                                                    {
                                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                        if (_Class != null)
                                                                        {
                                                                            if (pickup.PickStatus == "Office")
                                                                                pickup.OfficeFlag = true;
                                                                            else if (pickup.PickStatus == "Club")
                                                                                pickup.ClubFlag = true;
                                                                        }

                                                                        if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                            pickup.AfterSchoolFlag = true;
                                                                    }
                                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                    {
                                                                        //pickup.AfterSchoolFlag = true;
                                                                    }
                                                                    pickup.PickTime = dt;
                                                                    pickup.PickDate = dt;
                                                                    pickup.PickerID = objPicker.PickerId;
                                                                    pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                    DB.SaveChanges();
                                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                                    SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                    Response.Redirect("Pickup.aspx");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPicker.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPicker.PickerId;
                                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                                            obj.PickTime = dt;
                                                            obj.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                // obj.AfterSchoolFlag = true;
                                                            }
                                                            obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();

                                                            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Student, GetPickupStatus(_student.ClassID));
                                                        }
                                                        ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                        if (ObSt != null)
                                                        {
                                                            ISPickup objp = new ISPickup();
                                                            objp.StudentID = ObSt.ID;
                                                            objp.ClassID = ObSt.ClassID;
                                                            objp.PickerID = objPickers.PickerId;
                                                            objp.TeacherID = Authentication.LogginTeacher.ID;
                                                            objp.PickTime = dt;
                                                            objp.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                //objp.AfterSchoolFlag = true;
                                                            }
                                                            //objp.AfterSchoolFlag = true;
                                                            objp.PickStatus = "Picked";
                                                            DB.ISPickups.Add(objp);
                                                            DB.SaveChanges();
                                                            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                            SendPickupEmailManage(_Student, "Picked");
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }
                                                    }
                                                }
                                                else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                                {
                                                    bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                                    if (objPicker.PickerCode == txtConfirm.Text || isCodeIsOld)
                                                    {
                                                        if (Obj.CreatedDateTime >= dt.AddDays(-30))
                                                        {
                                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                            if (objs.Count > 0)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                                if (pickup != null)
                                                                {
                                                                    if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                                    {
                                                                        AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                        {
                                                                            AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (objLists[0].PickStatus == "After-School-Ex")
                                                                            {
                                                                                string Studentname = objLists[0].StudentName;
                                                                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                                if (_Student != null)
                                                                                {
                                                                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                                    {
                                                                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                        DB.ISPickups.RemoveRange(_Pickup);
                                                                                        DB.SaveChanges();

                                                                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                        foreach (var item in _Picker)
                                                                                        {
                                                                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                            DB.SaveChanges();
                                                                                        }
                                                                                        DB.ISPickers.RemoveRange(_Picker);
                                                                                        DB.SaveChanges();

                                                                                        DB.ISStudents.Remove(_Student);
                                                                                        DB.SaveChanges();
                                                                                    }
                                                                                }
                                                                            }
                                                                            int OfficeID = 0;
                                                                            if (Session["drpClass4"] != null)
                                                                            {
                                                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                                if (_Class != null)
                                                                                {
                                                                                    if (pickup.PickStatus == "Office")
                                                                                        pickup.OfficeFlag = true;
                                                                                    else if (pickup.PickStatus == "Club")
                                                                                        pickup.ClubFlag = true;
                                                                                }
                                                                                if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                                    pickup.AfterSchoolFlag = true;
                                                                            }
                                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                            {
                                                                                //pickup.AfterSchoolFlag = true;
                                                                            }
                                                                            pickup.PickTime = dt;
                                                                            pickup.PickDate = dt;
                                                                            pickup.PickerID = objPicker.PickerId;
                                                                            pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                            DB.SaveChanges();
                                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                            SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                            Response.Redirect("Pickup.aspx");
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ISPickup obj = new ISPickup();
                                                                    obj.StudentID = objPicker.StudentID;
                                                                    obj.ClassID = _student.ClassID;
                                                                    obj.PickerID = objPicker.PickerId;
                                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                                    obj.PickTime = dt;
                                                                    obj.PickDate = dt;
                                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                    {
                                                                        //obj.AfterSchoolFlag = true;
                                                                    }
                                                                    obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                                    DB.ISPickups.Add(obj);
                                                                    DB.SaveChanges();

                                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                    SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                    //Response.Redirect("Pickup.aspx");
                                                                }
                                                                ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                                ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                                if (ObSt != null)
                                                                {
                                                                    ISPickup objp = new ISPickup();
                                                                    objp.StudentID = ObSt.ID;
                                                                    objp.ClassID = ObSt.ClassID;
                                                                    objp.PickerID = objPickers.PickerId;
                                                                    objp.TeacherID = Authentication.LogginTeacher.ID;
                                                                    objp.PickTime = dt;
                                                                    objp.PickDate = dt;
                                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                    {
                                                                        // objp.AfterSchoolFlag = true;
                                                                    }
                                                                    objp.PickStatus = "Picked";
                                                                    DB.ISPickups.Add(objp);
                                                                    DB.SaveChanges();
                                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                                    SendPickupEmailManage(_Students, "Picked");
                                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                }
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Picker does not allow to Pick Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                }
                                                else
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "InValid Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            AlertMessageManagement.ServerMessage(Page, "You are not Able to do Child Pickup", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                    }
                                    else
                                    {
                                        if (objPickers != null && objPickers.PickCodeExDate != null)
                                        {
                                            //if (objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"))
                                            //{
                                            bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                            ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);

                                            if (objPickers.PickerCode == txtConfirm.Text || isCodeIsOld)
                                            {
                                                //&& p.StudentID == ID && DbFunctions.TruncateTime(p.StudentPickAssignDate.Value) == DbFunctions.TruncateTime(dt)
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Office Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;

                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    //pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPickers.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                DB.SaveChanges();

                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPickers.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPickers.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            // obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();

                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }
                                                    List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                    if (objss.Count > 0)
                                                    {
                                                        ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                        if (ObSt != null)
                                                        {
                                                            ISPickup objp = new ISPickup();
                                                            objp.StudentID = ObSt.ID;
                                                            objp.ClassID = ObSt.ClassID;
                                                            objp.PickerID = objPickers.PickerId;
                                                            objp.TeacherID = Authentication.LogginTeacher.ID;
                                                            objp.PickTime = dt;
                                                            objp.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                // objp.AfterSchoolFlag = true;
                                                            }
                                                            objp.PickStatus = "Picked";
                                                            DB.ISPickups.Add(objp);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, "Picked");
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }

                                                    }
                                                }

                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            //}
                                            //else
                                            //{
                                            //    AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            //}
                                        }
                                        else
                                        {
                                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);

                                            //bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
                                            ISPickup pickupObject = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (pickupObject != null && isStandardClass && ((pickupObject.AfterSchoolFlag.HasValue && pickupObject.AfterSchoolFlag.Value)
                                                    || (pickupObject.OfficeFlag.HasValue && pickupObject.OfficeFlag.Value) || (pickupObject.ClubFlag.HasValue && pickupObject.ClubFlag.Value)))
                                            {
                                                AlertMessageManagement.ServerMessage(Page, $"Student is {pickupObject.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                            {
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                    if (objss.Count > 0)
                                                    {
                                                        ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                        if (ObSt != null)
                                                        {
                                                            ISPickup objp = new ISPickup();
                                                            objp.StudentID = ObSt.ID;
                                                            objp.ClassID = ObSt.ClassID;
                                                            objp.PickerID = objPickers.PickerId;
                                                            objp.TeacherID = Authentication.LogginTeacher.ID;
                                                            objp.PickTime = dt;
                                                            objp.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                //objp.AfterSchoolFlag = true;
                                                            }
                                                            objp.PickStatus = "Picked";
                                                            DB.ISPickups.Add(objp);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, "Picked");
                                                        }
                                                    }
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;
                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    // pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(selectedClassId);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(selectedClassId));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            //obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }
                                                }
                                            }
                                            else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                            {
                                                bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                                if (objPicker.PickerCode == txtConfirm.Text || isCodeIsOld)
                                                {
                                                    if (Obj.CreatedDateTime >= dt.AddDays(-30))
                                                    {
                                                        //&& p.StudentID == ID && DbFunctions.TruncateTime(p.StudentPickAssignDate.Value) == DbFunctions.TruncateTime(dt)
                                                        isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
                                                        ISPickup pickupObj = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));

                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else if (pickupObj != null && isStandardClass && ((pickupObj.AfterSchoolFlag.HasValue && pickupObj.AfterSchoolFlag.Value)
                                                                || (pickupObj.OfficeFlag.HasValue && pickupObj.OfficeFlag.Value) || (pickupObj.ClubFlag.HasValue && pickupObj.ClubFlag.Value)))
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, $"Student is {pickupObj.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                            if (objss.Count > 0)
                                                            {
                                                                ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                                ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                                if (ObSt != null)
                                                                {
                                                                    ISPickup objp = new ISPickup();
                                                                    objp.StudentID = ObSt.ID;
                                                                    objp.ClassID = ObSt.ClassID;
                                                                    objp.PickerID = objPickers.PickerId;
                                                                    objp.TeacherID = Authentication.LogginTeacher.ID;
                                                                    objp.PickTime = dt;
                                                                    objp.PickDate = dt;
                                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                    {
                                                                        // objp.AfterSchoolFlag = true;
                                                                    }
                                                                    objp.PickStatus = "Picked";
                                                                    DB.ISPickups.Add(objp);
                                                                    DB.SaveChanges();
                                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                                    SendPickupEmailManage(_Students, "Picked");
                                                                }
                                                            }
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                            if (pickup != null)
                                                            {
                                                                if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                                {
                                                                    AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                }
                                                                else
                                                                {
                                                                    if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                    {
                                                                        AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (objLists[0].PickStatus == "After-School-Ex")
                                                                        {
                                                                            string Studentname = objLists[0].StudentName;
                                                                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                            if (_Student != null)
                                                                            {
                                                                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                                {
                                                                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                    DB.ISPickups.RemoveRange(_Pickup);
                                                                                    DB.SaveChanges();

                                                                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                    foreach (var item in _Picker)
                                                                                    {
                                                                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                        DB.SaveChanges();
                                                                                    }
                                                                                    DB.ISPickers.RemoveRange(_Picker);
                                                                                    DB.SaveChanges();

                                                                                    DB.ISStudents.Remove(_Student);
                                                                                    DB.SaveChanges();
                                                                                }
                                                                            }
                                                                        }
                                                                        int OfficeID = 0;
                                                                        if (Session["drpClass4"] != null)
                                                                        {
                                                                            OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                            if (_Class != null)
                                                                            {
                                                                                if (pickup.PickStatus == "Office")
                                                                                    pickup.OfficeFlag = true;
                                                                                else if (pickup.PickStatus == "Club")
                                                                                    pickup.ClubFlag = true;

                                                                            }
                                                                            if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                                pickup.AfterSchoolFlag = true;
                                                                        }
                                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                        {
                                                                            // pickup.AfterSchoolFlag = true;
                                                                        }
                                                                        pickup.PickTime = dt;
                                                                        pickup.PickDate = dt;
                                                                        pickup.PickerID = objPicker.PickerId;
                                                                        pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                        DB.SaveChanges();
                                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                        SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                        Response.Redirect("Pickup.aspx");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPicker.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPicker.PickerId;
                                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                                obj.PickTime = dt;
                                                                obj.PickDate = dt;
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    //obj.AfterSchoolFlag = true;
                                                                }
                                                                obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Picker does not allow to Pick Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                }
                                                else
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "InValid Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                    if (Objs.PickerType == (int)EnumsManagement.PICKERTYPE.Organisation && IDChk.Checked == false)
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "ID Must Be Checked for An Organisation Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)" || p.PickStatus == "Picked(Late)") && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                        if (objs.Count > 0)
                                        {
                                            AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                        else
                                        {
                                            //bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                            if (pickup != null)
                                            {
                                                if (isStandardClass && ((pickup.AfterSchoolFlag.HasValue && pickup.AfterSchoolFlag.Value)
                                                    || (pickup.OfficeFlag.HasValue && pickup.OfficeFlag.Value) || (pickup.ClubFlag.HasValue && pickup.ClubFlag.Value)))
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, $"Student is {pickup.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                    {
                                                        string msg = "Student already Picked within Same Picker.";
                                                        if (pickup.OfficeFlag.HasValue && pickup.OfficeFlag.Value)
                                                            msg = "Pickup Status can not change due to Student Picked from Office Class.";
                                                        else if (pickup.ClubFlag.HasValue && pickup.ClubFlag.Value)
                                                            msg = "Pickup Status can not change due to Student Picked from Club Class.";

                                                        AlertMessageManagement.ServerMessage(Page, msg, (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        if (objLists[0].PickStatus == "After-School-Ex")
                                                        {
                                                            string Studentname = objLists[0].StudentName;
                                                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                            if (_Student != null)
                                                            {
                                                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                {
                                                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                    DB.ISPickups.RemoveRange(_Pickup);
                                                                    DB.SaveChanges();

                                                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                    foreach (var item in _Picker)
                                                                    {
                                                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                        DB.SaveChanges();
                                                                    }
                                                                    DB.ISPickers.RemoveRange(_Picker);
                                                                    DB.SaveChanges();

                                                                    DB.ISStudents.Remove(_Student);
                                                                    DB.SaveChanges();
                                                                }
                                                            }
                                                        }
                                                        int OfficeID = 0;
                                                        if (Session["drpClass4"] != null)
                                                        {
                                                            OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                            if (_Class != null)
                                                            {
                                                                if (pickup.PickStatus == "Office")
                                                                    pickup.OfficeFlag = true;
                                                                else if (pickup.PickStatus == "Club")
                                                                    pickup.ClubFlag = true;
                                                            }
                                                            if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                pickup.AfterSchoolFlag = true;
                                                        }
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            //pickup.AfterSchoolFlag = true;
                                                        }
                                                        pickup.PickTime = dt;
                                                        pickup.PickDate = dt;
                                                        pickup.PickerID = objPicker.PickerId;
                                                        pickup.PickStatus = GetPickupStatus(selectedClassId);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);

                                                        if (_Students.EmailAfterConfirmPickUp.HasValue && _Students.EmailAfterConfirmPickUp.Value)
                                                        {
                                                            SendPickupEmailManage(_Students, GetPickupStatus(selectedClassId));
                                                        }

                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                                                        Response.Redirect("Pickup.aspx");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = objPicker.StudentID;
                                                obj.ClassID = _student.ClassID;
                                                obj.PickerID = objPicker.PickerId;
                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                obj.PickTime = dt;
                                                obj.PickDate = dt;
                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                {
                                                    //obj.AfterSchoolFlag = true;
                                                }
                                                obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                Response.Redirect("Pickup.aspx");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                AlertMessageManagement.ServerMessage(Page, "Mark As Absent should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                        }
                        else
                        {
                            AlertMessageManagement.ServerMessage(Page, "First of all Fill Up Attendence for Today", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                    }
                    else
                    {
                        List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                        if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                        {
                            ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == _student.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                            //if (ObjAttendance == null)
                            //{
                            //    txtConfirm.Text = "";
                            //    AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            //}
                            //else
                            //{
                            if (DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).PickerType == (int)EnumsManagement.PICKERTYPE.Individual && !DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).FirstName.Contains("("))
                            {
                                List<ISPickup> objPicks = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "Office" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                if (objPicks.Count > 0)
                                {
                                    List<ISTeacherClassAssignment> ObjAssign = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Authentication.LogginTeacher.ID && p.ISClass.Name.Contains("(Office)") && p.ISClass.SchoolID == Authentication.SchoolID && p.Active == true).ToList();
                                    if (ObjAssign.Count > 0)
                                    {
                                        if (objPickers != null && objPickers.PickCodeExDate != null)
                                        {

                                            bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));

                                            ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                            if (objPickers.PickerCode == txtConfirm.Text || isCodeIsOld)
                                            {

                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;
                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPickers.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                DB.SaveChanges();

                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPickers.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPickers.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }

                                        }
                                        else
                                        {
                                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                            if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                            {
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = Authentication.LogginTeacher.ID;
                                                        objp.PickTime = dt;
                                                        objp.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            objp.AfterSchoolFlag = true;
                                                        }
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, "Picked");
                                                    }
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;
                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }

                                                }
                                            }
                                            else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                            {
                                                bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                                if (objPicker.PickerCode == txtConfirm.Text || isCodeIsOld)
                                                {
                                                    if (Obj.CreatedDateTime >= dt.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = Authentication.LogginTeacher.ID;
                                                                objp.PickTime = dt;
                                                                objp.PickDate = dt;
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    objp.AfterSchoolFlag = true;
                                                                }
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, "Picked");
                                                            }
                                                            if (pickup != null)
                                                            {
                                                                if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                                {
                                                                    AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                }
                                                                else
                                                                {
                                                                    if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                    {
                                                                        AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (objLists[0].PickStatus == "After-School-Ex")
                                                                        {
                                                                            string Studentname = objLists[0].StudentName;
                                                                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                            if (_Student != null)
                                                                            {
                                                                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                                {
                                                                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                    DB.ISPickups.RemoveRange(_Pickup);
                                                                                    DB.SaveChanges();

                                                                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                    foreach (var item in _Picker)
                                                                                    {
                                                                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                        DB.SaveChanges();
                                                                                    }
                                                                                    DB.ISPickers.RemoveRange(_Picker);
                                                                                    DB.SaveChanges();

                                                                                    DB.ISStudents.Remove(_Student);
                                                                                    DB.SaveChanges();
                                                                                }
                                                                            }
                                                                        }
                                                                        int OfficeID = 0;
                                                                        if (Session["drpClass4"] != null)
                                                                        {
                                                                            OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                            if (_Class != null)
                                                                            {
                                                                                if (pickup.PickStatus == "Office")
                                                                                    pickup.OfficeFlag = true;
                                                                                else if (pickup.PickStatus == "Club")
                                                                                    pickup.ClubFlag = true;
                                                                            }
                                                                            if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                                pickup.AfterSchoolFlag = true;
                                                                        }
                                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                        {
                                                                            pickup.AfterSchoolFlag = true;
                                                                        }
                                                                        pickup.PickTime = dt;
                                                                        pickup.PickDate = dt;
                                                                        pickup.PickerID = objPicker.PickerId;
                                                                        pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                        DB.SaveChanges();
                                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                        SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                        Response.Redirect("Pickup.aspx");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPicker.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPicker.PickerId;
                                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                                obj.PickTime = dt;
                                                                obj.PickDate = dt;
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    obj.AfterSchoolFlag = true;
                                                                }
                                                                obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Picker does not allow to Pick Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                }
                                                else
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "InValid Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "You are not Able to do Child Pickup", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                }
                                else
                                {
                                    if (objPickers != null && objPickers.PickCodeExDate != null)
                                    {

                                        bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                        ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                        if (objPickers.PickerCode == txtConfirm.Text || isCodeIsOld)
                                        {

                                            //&& p.StudentID == ID && DbFunctions.TruncateTime(p.StudentPickAssignDate.Value) == DbFunctions.TruncateTime(dt)
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                            if (objs.Count > 0)
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                if (objss.Count > 0)
                                                {
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = Authentication.LogginTeacher.ID;
                                                        objp.PickTime = dt;
                                                        objp.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            objp.AfterSchoolFlag = true;
                                                        }
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, "Picked");
                                                    }
                                                }
                                                if (pickup != null)
                                                {
                                                    if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (objLists[0].PickStatus == "After-School-Ex")
                                                            {
                                                                string Studentname = objLists[0].StudentName;
                                                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                if (_Student != null)
                                                                {
                                                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                    {
                                                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                        DB.ISPickups.RemoveRange(_Pickup);
                                                                        DB.SaveChanges();

                                                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                        foreach (var item in _Picker)
                                                                        {
                                                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                            DB.SaveChanges();
                                                                        }
                                                                        DB.ISPickers.RemoveRange(_Picker);
                                                                        DB.SaveChanges();

                                                                        DB.ISStudents.Remove(_Student);
                                                                        DB.SaveChanges();
                                                                    }
                                                                }
                                                            }
                                                            int OfficeID = 0;
                                                            if (Session["drpClass4"] != null)
                                                            {
                                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                if (_Class != null)
                                                                {
                                                                    if (pickup.PickStatus == "Office")
                                                                        pickup.OfficeFlag = true;
                                                                    else if (pickup.PickStatus == "Club")
                                                                        pickup.ClubFlag = true;
                                                                }
                                                                if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                    pickup.AfterSchoolFlag = true;
                                                            }
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                            pickup.PickTime = dt;
                                                            pickup.PickDate = dt;
                                                            pickup.PickerID = objPickers.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objPickers.StudentID;
                                                    obj.ClassID = _student.ClassID;
                                                    obj.PickerID = objPickers.PickerId;
                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                    obj.PickTime = dt;
                                                    obj.PickDate = dt;
                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    {
                                                        obj.AfterSchoolFlag = true;
                                                    }
                                                    obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                    SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                    Response.Redirect("Pickup.aspx");
                                                }
                                            }

                                        }
                                        else
                                        {
                                            AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }

                                    }
                                    else
                                    {
                                        ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                        ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                        if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                        {
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                            if (objs.Count > 0)
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                if (objss.Count > 0)
                                                {
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = Authentication.LogginTeacher.ID;
                                                        objp.PickTime = dt;
                                                        objp.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            objp.AfterSchoolFlag = true;
                                                        }
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, "Picked");
                                                    }
                                                }
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                if (pickup != null)
                                                {
                                                    if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (objLists[0].PickStatus == "After-School-Ex")
                                                            {
                                                                string Studentname = objLists[0].StudentName;
                                                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                if (_Student != null)
                                                                {
                                                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                    {
                                                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                        DB.ISPickups.RemoveRange(_Pickup);
                                                                        DB.SaveChanges();

                                                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                        foreach (var item in _Picker)
                                                                        {
                                                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                            DB.SaveChanges();
                                                                        }
                                                                        DB.ISPickers.RemoveRange(_Picker);
                                                                        DB.SaveChanges();

                                                                        DB.ISStudents.Remove(_Student);
                                                                        DB.SaveChanges();

                                                                    }
                                                                }
                                                            }
                                                            int OfficeID = 0;
                                                            if (Session["drpClass4"] != null)
                                                            {
                                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                if (_Class != null)
                                                                {
                                                                    if (pickup.PickStatus == "Office")
                                                                        pickup.OfficeFlag = true;
                                                                    else if (pickup.PickStatus == "Club")
                                                                        pickup.ClubFlag = true;
                                                                }
                                                                if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                    pickup.AfterSchoolFlag = true;
                                                            }
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                            pickup.PickTime = dt;
                                                            pickup.PickDate = dt;
                                                            pickup.PickerID = objPicker.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objPicker.StudentID;
                                                    obj.ClassID = _student.ClassID;
                                                    obj.PickerID = objPicker.PickerId;
                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                    obj.PickTime = dt;
                                                    obj.PickDate = dt;
                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    {
                                                        obj.AfterSchoolFlag = true;
                                                    }
                                                    obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                    SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                    Response.Redirect("Pickup.aspx");
                                                }
                                            }
                                        }
                                        else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                        {
                                            bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                            if (objPicker.PickerCode == txtConfirm.Text || isCodeIsOld)
                                            {
                                                if (Obj.CreatedDateTime >= dt.AddDays(-30))
                                                {
                                                    //&& p.StudentID == ID && DbFunctions.TruncateTime(p.StudentPickAssignDate.Value) == DbFunctions.TruncateTime(dt)
                                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                    if (objs.Count > 0)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                        List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                        if (objss.Count > 0)
                                                        {
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            if (ObSt != null)
                                                            {
                                                                ISPickup objp = new ISPickup();
                                                                objp.StudentID = ObSt.ID;
                                                                objp.ClassID = ObSt.ClassID;
                                                                objp.PickerID = objPickers.PickerId;
                                                                objp.TeacherID = Authentication.LogginTeacher.ID;
                                                                objp.PickTime = dt;
                                                                objp.PickDate = dt;
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    objp.AfterSchoolFlag = true;
                                                                }
                                                                objp.PickStatus = "Picked";
                                                                DB.ISPickups.Add(objp);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, "Picked");
                                                            }
                                                        }
                                                        if (pickup != null)
                                                        {
                                                            if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                {
                                                                    AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                }
                                                                else
                                                                {
                                                                    if (objLists[0].PickStatus == "After-School-Ex")
                                                                    {
                                                                        string Studentname = objLists[0].StudentName;
                                                                        ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                        if (_Student != null)
                                                                        {
                                                                            if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                            {
                                                                                List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                DB.ISPickups.RemoveRange(_Pickup);
                                                                                DB.SaveChanges();

                                                                                List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                foreach (var item in _Picker)
                                                                                {
                                                                                    List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                    DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                    DB.SaveChanges();
                                                                                }
                                                                                DB.ISPickers.RemoveRange(_Picker);
                                                                                DB.SaveChanges();

                                                                                DB.ISStudents.Remove(_Student);
                                                                                DB.SaveChanges();
                                                                            }
                                                                        }
                                                                    }
                                                                    int OfficeID = 0;
                                                                    if (Session["drpClass4"] != null)
                                                                    {
                                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                        if (_Class != null)
                                                                        {
                                                                            if (pickup.PickStatus == "Office")
                                                                                pickup.OfficeFlag = true;
                                                                            else if (pickup.PickStatus == "Club")
                                                                                pickup.ClubFlag = true;
                                                                        }
                                                                        if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                            pickup.AfterSchoolFlag = true;
                                                                    }
                                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                    {
                                                                        pickup.AfterSchoolFlag = true;
                                                                    }
                                                                    pickup.PickTime = dt;
                                                                    pickup.PickDate = dt;
                                                                    pickup.PickerID = objPicker.PickerId;
                                                                    pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                    DB.SaveChanges();
                                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                    SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                    Response.Redirect("Pickup.aspx");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ISPickup obj = new ISPickup();
                                                            obj.StudentID = objPicker.StudentID;
                                                            obj.ClassID = _student.ClassID;
                                                            obj.PickerID = objPicker.PickerId;
                                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                                            obj.PickTime = dt;
                                                            obj.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                obj.AfterSchoolFlag = true;
                                                            }
                                                            obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                            DB.ISPickups.Add(obj);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Picker does not allow to Pick Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                        }
                                        else
                                        {
                                            AlertMessageManagement.ServerMessage(Page, "InValid Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                if (Objs.PickerType == (int)EnumsManagement.PICKERTYPE.Organisation && IDChk.Checked == false)
                                {
                                    AlertMessageManagement.ServerMessage(Page, "ID Must Be Checked for An Organisation Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)" || p.PickStatus == "Picked(Late)") && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                    if (objs.Count > 0)
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (pickup != null)
                                        {
                                            if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    if (objLists[0].PickStatus == "After-School-Ex")
                                                    {
                                                        string Studentname = objLists[0].StudentName;
                                                        ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                        if (_Student != null)
                                                        {
                                                            if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                            {
                                                                List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                DB.ISPickups.RemoveRange(_Pickup);
                                                                DB.SaveChanges();

                                                                List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                foreach (var item in _Picker)
                                                                {
                                                                    List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                    DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                    DB.SaveChanges();
                                                                }
                                                                DB.ISPickers.RemoveRange(_Picker);
                                                                DB.SaveChanges();

                                                                DB.ISStudents.Remove(_Student);
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                    }
                                                    int OfficeID = 0;
                                                    if (Session["drpClass4"] != null)
                                                    {
                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                        if (_Class != null)
                                                        {
                                                            if (pickup.PickStatus == "Office")
                                                                pickup.OfficeFlag = true;
                                                            else if (pickup.PickStatus == "Club")
                                                                pickup.ClubFlag = true;
                                                        }
                                                        if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                            pickup.AfterSchoolFlag = true;
                                                    }
                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    {
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                    pickup.PickTime = dt;
                                                    pickup.PickDate = dt;
                                                    pickup.PickerID = objPicker.PickerId;
                                                    pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                    DB.SaveChanges();
                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                    SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                    Response.Redirect("Pickup.aspx");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objPicker.StudentID;
                                            obj.ClassID = _student.ClassID;
                                            obj.PickerID = objPicker.PickerId;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = dt;
                                            obj.PickDate = dt;
                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                            {
                                                obj.AfterSchoolFlag = true;
                                            }
                                            obj.PickStatus = GetPickupStatus(_student.ClassID);
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                            SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                            Response.Redirect("Pickup.aspx");
                                        }
                                    }
                                }
                            }
                            //}
                        }
                        else
                        {
                            if (DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).PickerType == (int)EnumsManagement.PICKERTYPE.Individual && !DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId).FirstName.Contains("("))
                            {
                                List<ISPickup> objPicks = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "Office" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                if (objPicks.Count > 0)
                                {
                                    List<ISTeacherClassAssignment> ObjAssign = DB.ISTeacherClassAssignments.Where(p => p.TeacherID == Authentication.LogginTeacher.ID && p.ISClass.Name.Contains("(Office)") && p.ISClass.SchoolID == Authentication.SchoolID && p.Active == true).ToList();
                                    if (ObjAssign.Count > 0)
                                    {
                                        if (objPickers != null && objPickers.PickCodeExDate != null)
                                        {


                                            bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                            ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                            if (objPickers.PickerCode == txtConfirm.Text || isCodeIsOld)
                                            {

                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;
                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPickers.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPickers.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPickers.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }

                                        }
                                        else
                                        {
                                            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                            if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                            {
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    //if (ObSt != null)
                                                    //{
                                                    //    ISPickup objp = new ISPickup();
                                                    //    objp.StudentID = ObSt.ID;
                                                    //    objp.ClassID = ObSt.ClassID;
                                                    //    objp.PickerID = objPickers.PickerId;
                                                    //    objp.TeacherID = Authentication.LogginTeacher.ID;
                                                    //    objp.PickTime = dt;
                                                    //    objp.PickDate = dt;
                                                    //    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    //    {
                                                    //        objp.AfterSchoolFlag = true;
                                                    //    }
                                                    //    objp.PickStatus = "Picked";
                                                    //    DB.ISPickups.Add(objp);
                                                    //    DB.SaveChanges();
                                                    //    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                    //    SendPickupEmailManage(_Students, "Picked");
                                                    //}
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;
                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }

                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                }
                                                Response.Redirect("Pickup.aspx");
                                            }
                                            else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                            {
                                                bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                                if (objPicker.PickerCode == txtConfirm.Text || isCodeIsOld)
                                                {
                                                    if (Obj.CreatedDateTime >= dt.AddDays(-30))
                                                    {
                                                        List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                        if (objs.Count > 0)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "Office" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                            ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                            ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                            //if (ObSt != null)
                                                            //{
                                                            //    ISPickup objp = new ISPickup();
                                                            //    objp.StudentID = ObSt.ID;
                                                            //    objp.ClassID = ObSt.ClassID;
                                                            //    objp.PickerID = objPickers.PickerId;
                                                            //    objp.TeacherID = Authentication.LogginTeacher.ID;
                                                            //    objp.PickTime = dt;
                                                            //    objp.PickDate = dt;
                                                            //    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            //    {
                                                            //        objp.AfterSchoolFlag = true;
                                                            //    }
                                                            //    objp.PickStatus = "Picked";
                                                            //    DB.ISPickups.Add(objp);
                                                            //    DB.SaveChanges();
                                                            //    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                            //    SendPickupEmailManage(_Students, "Picked");
                                                            //}
                                                            if (pickup != null)
                                                            {
                                                                if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                                {
                                                                    AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                }
                                                                else
                                                                {
                                                                    if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                                    {
                                                                        AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (objLists[0].PickStatus == "After-School-Ex")
                                                                        {
                                                                            string Studentname = objLists[0].StudentName;
                                                                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                            if (_Student != null)
                                                                            {
                                                                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                                {
                                                                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                                    DB.ISPickups.RemoveRange(_Pickup);
                                                                                    DB.SaveChanges();

                                                                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                                    foreach (var item in _Picker)
                                                                                    {
                                                                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                        DB.SaveChanges();
                                                                                    }
                                                                                    DB.ISPickers.RemoveRange(_Picker);
                                                                                    DB.SaveChanges();

                                                                                    DB.ISStudents.Remove(_Student);
                                                                                    DB.SaveChanges();
                                                                                }
                                                                            }
                                                                        }
                                                                        int OfficeID = 0;
                                                                        if (Session["drpClass4"] != null)
                                                                        {
                                                                            OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                            if (_Class != null)
                                                                            {
                                                                                if (pickup.PickStatus == "Office")
                                                                                    pickup.OfficeFlag = true;
                                                                                else if (pickup.PickStatus == "Club")
                                                                                    pickup.ClubFlag = true;
                                                                            }
                                                                            if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                                pickup.AfterSchoolFlag = true;
                                                                        }
                                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                        {
                                                                            pickup.AfterSchoolFlag = true;
                                                                        }
                                                                        pickup.PickTime = dt;
                                                                        pickup.PickDate = dt;
                                                                        pickup.PickerID = objPicker.PickerId;
                                                                        pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                        DB.SaveChanges();
                                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                        SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                        Response.Redirect("Pickup.aspx");
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ISPickup obj = new ISPickup();
                                                                obj.StudentID = objPicker.StudentID;
                                                                obj.ClassID = _student.ClassID;
                                                                obj.PickerID = objPicker.PickerId;
                                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                                obj.PickTime = dt;
                                                                obj.PickDate = dt;
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    obj.AfterSchoolFlag = true;
                                                                }
                                                                obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                                DB.ISPickups.Add(obj);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Picker does not allow to Pick Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                }
                                                else
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "InValid Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "You are not Able to do Child Pickup", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                }
                                else
                                {
                                    if (objPickers != null && objPickers.PickCodeExDate != null)
                                    {
                                        bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                        ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                        ISPickup pickupObject = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (objPickers.PickerCode == txtConfirm.Text || isCodeIsOld)
                                        {

                                            //&& p.StudentID == ID && DbFunctions.TruncateTime(p.StudentPickAssignDate.Value) == DbFunctions.TruncateTime(dt)
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                            if (objs.Count > 0)
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else if (pickupObject != null && isStandardClass && ((pickupObject.AfterSchoolFlag.HasValue && pickupObject.AfterSchoolFlag.Value)
                                                    || (pickupObject.OfficeFlag.HasValue && pickupObject.OfficeFlag.Value) || (pickupObject.ClubFlag.HasValue && pickupObject.ClubFlag.Value)))
                                            {
                                                AlertMessageManagement.ServerMessage(Page, $"Student is {pickupObject.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPickers.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                if (objss.Count > 0)
                                                {
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = Authentication.LogginTeacher.ID;
                                                        objp.PickTime = dt;
                                                        objp.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            objp.AfterSchoolFlag = true;
                                                        }
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, "Picked");
                                                    }
                                                }
                                                if (pickup != null)
                                                {
                                                    if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (objLists[0].PickStatus == "After-School-Ex")
                                                            {
                                                                string Studentname = objLists[0].StudentName;
                                                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                if (_Student != null)
                                                                {
                                                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                    {
                                                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                        DB.ISPickups.RemoveRange(_Pickup);
                                                                        DB.SaveChanges();

                                                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                        foreach (var item in _Picker)
                                                                        {
                                                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                            DB.SaveChanges();
                                                                        }
                                                                        DB.ISPickers.RemoveRange(_Picker);
                                                                        DB.SaveChanges();

                                                                        DB.ISStudents.Remove(_Student);
                                                                        DB.SaveChanges();
                                                                    }
                                                                }
                                                            }
                                                            int OfficeID = 0;
                                                            if (Session["drpClass4"] != null)
                                                            {
                                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                if (_Class != null)
                                                                {
                                                                    if (pickup.PickStatus == "Office")
                                                                        pickup.OfficeFlag = true;
                                                                    else if (pickup.PickStatus == "Club")
                                                                        pickup.ClubFlag = true;
                                                                }
                                                                if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                    pickup.AfterSchoolFlag = true;
                                                            }
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                            pickup.PickTime = dt;
                                                            pickup.PickDate = dt;
                                                            pickup.PickerID = objPickers.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objPickers.StudentID;
                                                    obj.ClassID = _student.ClassID;
                                                    obj.PickerID = objPickers.PickerId;
                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                    obj.PickTime = dt;
                                                    obj.PickDate = dt;
                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    {
                                                        obj.AfterSchoolFlag = true;
                                                    }
                                                    obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID && p.Deleted == true);
                                                    SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                    Response.Redirect("Pickup.aspx");
                                                }

                                            }

                                        }
                                        else
                                        {
                                            AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }


                                    }
                                    else
                                    {
                                        ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                        ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                                        ISPickup pickupObject = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (Obj.Photo != null && Obj.Photo != "Upload/user.jpg")
                                        {
                                            List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();

                                            if (objs.Count > 0)
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else if (pickupObject != null && isStandardClass && ((pickupObject.AfterSchoolFlag.HasValue && pickupObject.AfterSchoolFlag.Value)
                                                    || (pickupObject.OfficeFlag.HasValue && pickupObject.OfficeFlag.Value) || (pickupObject.ClubFlag.HasValue && pickupObject.ClubFlag.Value)))
                                            {
                                                AlertMessageManagement.ServerMessage(Page, $"Student is {pickupObject.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                if (objss.Count > 0)
                                                {
                                                    ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                    ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                    if (ObSt != null)
                                                    {
                                                        ISPickup objp = new ISPickup();
                                                        objp.StudentID = ObSt.ID;
                                                        objp.ClassID = ObSt.ClassID;
                                                        objp.PickerID = objPickers.PickerId;
                                                        objp.TeacherID = Authentication.LogginTeacher.ID;
                                                        objp.PickTime = dt;
                                                        objp.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            objp.AfterSchoolFlag = true;
                                                        }
                                                        objp.PickStatus = "Picked";
                                                        DB.ISPickups.Add(objp);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, "Picked");
                                                    }
                                                }
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                if (pickup != null)
                                                {
                                                    if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                    {
                                                        AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                    }
                                                    else
                                                    {
                                                        if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (objLists[0].PickStatus == "After-School-Ex")
                                                            {
                                                                string Studentname = objLists[0].StudentName;
                                                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                if (_Student != null)
                                                                {
                                                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                    {
                                                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                        DB.ISPickups.RemoveRange(_Pickup);
                                                                        DB.SaveChanges();

                                                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                        foreach (var item in _Picker)
                                                                        {
                                                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                            DB.SaveChanges();
                                                                        }
                                                                        DB.ISPickers.RemoveRange(_Picker);
                                                                        DB.SaveChanges();

                                                                        DB.ISStudents.Remove(_Student);
                                                                        DB.SaveChanges();
                                                                    }
                                                                }
                                                            }
                                                            int OfficeID = 0;
                                                            if (Session["drpClass4"] != null)
                                                            {
                                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                if (_Class != null)
                                                                {
                                                                    if (pickup.PickStatus == "Office")
                                                                        pickup.OfficeFlag = true;
                                                                    else if (pickup.PickStatus == "Club")
                                                                        pickup.ClubFlag = true;
                                                                }
                                                                if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                    pickup.AfterSchoolFlag = true;
                                                            }
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                            pickup.PickTime = dt;
                                                            pickup.PickDate = dt;
                                                            pickup.PickerID = objPicker.PickerId;
                                                            pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                            Response.Redirect("Pickup.aspx");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objPicker.StudentID;
                                                    obj.ClassID = _student.ClassID;
                                                    obj.PickerID = objPicker.PickerId;
                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                    obj.PickTime = dt;
                                                    obj.PickDate = dt;
                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    {
                                                        obj.AfterSchoolFlag = true;
                                                    }
                                                    obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                    SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                    Response.Redirect("Pickup.aspx");
                                                }
                                            }
                                        }
                                        else if (Obj.Photo == null || Obj.Photo == "Upload/user.jpg")
                                        {
                                            bool isCodeIsOld = objPickers.PickCodeExDate == null ? true : !(objPickers.PickCodeExDate.Value.ToString("dd/MM/yyyy") == dt.ToString("dd/MM/yyyy"));
                                            if (objPicker.PickerCode == txtConfirm.Text || isCodeIsOld)
                                            {
                                                //&& p.StudentID == ID && DbFunctions.TruncateTime(p.StudentPickAssignDate.Value) == DbFunctions.TruncateTime(dt)
                                                List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && isStandardClass && (p.PickStatus != "Not Marked" && p.PickStatus != "UnPicked" && !p.PickStatus.StartsWith("Weekend")) && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                                if (objs.Count > 0)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else if (pickupObject != null && isStandardClass && ((pickupObject.AfterSchoolFlag.HasValue && pickupObject.AfterSchoolFlag.Value)
                                               || (pickupObject.OfficeFlag.HasValue && pickupObject.OfficeFlag.Value) || (pickupObject.ClubFlag.HasValue && pickupObject.ClubFlag.Value)))
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, $"Student is {pickupObject.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                                    List<ISPickup> objss = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && p.PickStatus == "After-School-Ex" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt)).ToList();
                                                    if (objss.Count > 0)
                                                    {
                                                        ISStudent ObStu = DB.ISStudents.SingleOrDefault(p => p.ID == objPickers.StudentID);
                                                        ISStudent ObSt = DB.ISStudents.SingleOrDefault(p => p.StudentNo == ObStu.StudentNo && p.StartDate == null && p.EndDate == null);
                                                        if (ObSt != null)
                                                        {
                                                            ISPickup objp = new ISPickup();
                                                            objp.StudentID = ObSt.ID;
                                                            objp.ClassID = ObSt.ClassID;
                                                            objp.PickerID = objPickers.PickerId;
                                                            objp.TeacherID = Authentication.LogginTeacher.ID;
                                                            objp.PickTime = dt;
                                                            objp.PickDate = dt;
                                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                            {
                                                                objp.AfterSchoolFlag = true;
                                                            }
                                                            objp.PickStatus = "Picked";
                                                            DB.ISPickups.Add(objp);
                                                            DB.SaveChanges();
                                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == ObSt.ID && p.Deleted == true);
                                                            SendPickupEmailManage(_Students, "Picked");
                                                        }
                                                    }
                                                    if (pickup != null)
                                                    {
                                                        if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                                        {
                                                            AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                        }
                                                        else
                                                        {
                                                            if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                            {
                                                                AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                            }
                                                            else
                                                            {
                                                                if (objLists[0].PickStatus == "After-School-Ex")
                                                                {
                                                                    string Studentname = objLists[0].StudentName;
                                                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                                    if (_Student != null)
                                                                    {
                                                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                                        {
                                                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                            DB.ISPickups.RemoveRange(_Pickup);
                                                                            DB.SaveChanges();

                                                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                            foreach (var item in _Picker)
                                                                            {
                                                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                                DB.SaveChanges();
                                                                            }
                                                                            DB.ISPickers.RemoveRange(_Picker);
                                                                            DB.SaveChanges();

                                                                            DB.ISStudents.Remove(_Student);
                                                                            DB.SaveChanges();
                                                                        }
                                                                    }
                                                                }
                                                                int OfficeID = 0;
                                                                if (Session["drpClass4"] != null)
                                                                {
                                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                                    if (_Class != null)
                                                                    {
                                                                        if (pickup.PickStatus == "Office")
                                                                            pickup.OfficeFlag = true;
                                                                        else if (pickup.PickStatus == "Club")
                                                                            pickup.ClubFlag = true;
                                                                    }
                                                                    if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                                        pickup.AfterSchoolFlag = true;
                                                                }
                                                                if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                                {
                                                                    pickup.AfterSchoolFlag = true;
                                                                }
                                                                pickup.PickTime = dt;
                                                                pickup.PickDate = dt;
                                                                pickup.PickerID = objPicker.PickerId;
                                                                pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                                DB.SaveChanges();
                                                                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                                SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                                AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                                Response.Redirect("Pickup.aspx");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ISPickup obj = new ISPickup();
                                                        obj.StudentID = objPicker.StudentID;
                                                        obj.ClassID = _student.ClassID;
                                                        obj.PickerID = objPicker.PickerId;
                                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                                        obj.PickTime = dt;
                                                        obj.PickDate = dt;
                                                        if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                        {
                                                            obj.AfterSchoolFlag = true;
                                                        }
                                                        obj.PickStatus = GetPickupStatus(_student.ClassID);
                                                        DB.ISPickups.Add(obj);
                                                        DB.SaveChanges();
                                                        ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                        SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                                        AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                        Response.Redirect("Pickup.aspx");
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Please Enter Valid Picker Code", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                        }
                                        else
                                        {
                                            AlertMessageManagement.ServerMessage(Page, "InValid Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                    }
                                }
                            }
                            else
                            {

                                ISPicker Objs = DB.ISPickers.SingleOrDefault(p => p.ID == objPickers.PickerId);
                                if (Objs.PickerType == (int)EnumsManagement.PICKERTYPE.Organisation && IDChk.Checked == false)
                                {
                                    AlertMessageManagement.ServerMessage(Page, "ID Must Be Checked for An Organisation Picker", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                                    List<ISPickup> objs = DB.ISPickups.Where(p => p.StudentID == objPicker.StudentID && (p.PickStatus == "Picked" || p.PickStatus == "Picked(Chargeable)" || p.PickStatus == "Picked(Reportable)" || p.PickStatus == "Picked(Late)") && p.PickStatus != "Not Marked" && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.CompletePickup == true).ToList();
                                    ISPickup pickupObject = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                    if (objs.Count > 0)
                                    {
                                        AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed from UnPicked after Complete Pickup Slide is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else if (pickupObject != null && isStandardClass && ((pickupObject.AfterSchoolFlag.HasValue && pickupObject.AfterSchoolFlag.Value)
                                                   || (pickupObject.OfficeFlag.HasValue && pickupObject.OfficeFlag.Value) || (pickupObject.ClubFlag.HasValue && pickupObject.ClubFlag.Value)))
                                    {
                                        AlertMessageManagement.ServerMessage(Page, $"Student is {pickupObject.PickStatus} can not be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objPickers.StudentID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt));
                                        if (pickup != null)
                                        {
                                            if ((pickup.PickStatus == "After-School" || pickup.PickStatus == "After-School-Ex") && pickup.AfterSchoolFlag == true)
                                            {
                                                AlertMessageManagement.ServerMessage(Page, "Student is Sent After-School can't be picked from here", (int)AlertMessageManagement.MESSAGETYPE.warning);

                                            }
                                            else
                                            {
                                                if (pickup.PickStatus.Contains("Picked") && pickup.PickerID == objPickers.PickerId)
                                                {
                                                    AlertMessageManagement.ServerMessage(Page, "Student already Picked within Same Picker.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                                }
                                                else
                                                {
                                                    if (objLists[0].PickStatus == "After-School-Ex")
                                                    {
                                                        string Studentname = objLists[0].StudentName;
                                                        ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                                        if (_Student != null)
                                                        {
                                                            if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                                            {
                                                                List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                                                DB.ISPickups.RemoveRange(_Pickup);
                                                                DB.SaveChanges();

                                                                List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                                                foreach (var item in _Picker)
                                                                {
                                                                    List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                                    DB.ISPickerAssignments.RemoveRange(_Assign);
                                                                    DB.SaveChanges();
                                                                }
                                                                DB.ISPickers.RemoveRange(_Picker);
                                                                DB.SaveChanges();

                                                                DB.ISStudents.Remove(_Student);
                                                                DB.SaveChanges();
                                                            }
                                                        }
                                                    }
                                                    int OfficeID = 0;
                                                    if (Session["drpClass4"] != null)
                                                    {
                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                        if (_Class != null)
                                                        {
                                                            if (pickup.PickStatus == "Office")
                                                                pickup.OfficeFlag = true;
                                                            else if (pickup.PickStatus == "Club")
                                                                pickup.ClubFlag = true;
                                                        }
                                                        if (OperationManagement.IsAfterSchoolFlagEnable(OfficeID, pickup.PickStatus))
                                                            pickup.AfterSchoolFlag = true;
                                                    }
                                                    if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                                    {
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                    pickup.PickTime = dt;
                                                    pickup.PickDate = dt;
                                                    pickup.PickerID = objPicker.PickerId;
                                                    pickup.PickStatus = GetPickupStatus(pickup.ClassID);
                                                    DB.SaveChanges();

                                                    ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                                    SendPickupEmailManage(_Students, GetPickupStatus(pickup.ClassID));
                                                    AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                                    Response.Redirect("Pickup.aspx");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objPicker.StudentID;
                                            obj.ClassID = _student.ClassID;
                                            obj.PickerID = objPicker.PickerId;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = dt;
                                            obj.PickDate = dt;
                                            if (school.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool)
                                            {
                                                obj.AfterSchoolFlag = true;
                                            }
                                            obj.PickStatus = GetPickupStatus(_student.ClassID);
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();

                                            ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                            SendPickupEmailManage(_Students, GetPickupStatus(_student.ClassID));
                                            AlertMessageManagement.ServerMessage(Page, "Student can Leave now Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                                            Response.Redirect("Pickup.aspx");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            BindList();
        }

        private bool IsStudentIsFoundInUnPickedInAfterSchool(int studentId)
        {
            bool isvalid = false;

            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
            {

                ISStudent iSStudent = DB.ISStudents.FirstOrDefault(r => r.ID == studentId);

                List<string> emailId = new List<string>();
                emailId.Add(iSStudent.ParantEmail1.ToLower());

                if (iSStudent.ParantEmail1.Length > 0)
                    emailId.Add(iSStudent.ParantEmail1.ToLower());

                ISStudent findStudentImAFterSchool = DB.ISStudents.FirstOrDefault(r => r.ID != iSStudent.ID && r.StudentName.ToLower() == iSStudent.StudentName.ToLower() &&
                (emailId.Contains(r.ParantEmail1.ToLower()) || emailId.Contains(r.ParantEmail2.ToLower())) && r.Deleted == true && r.Active == true);

                if (findStudentImAFterSchool != null)
                {
                    DateTime dt = DateTime.Now;
                    ISPickup pickup = DB.ISPickups.FirstOrDefault(r => r.StudentID == findStudentImAFterSchool.ID && DbFunctions.TruncateTime(r.PickDate.Value) == DbFunctions.TruncateTime(dt));
                    if (pickup != null)
                        isvalid = pickup.PickStatus == "UnPicked";
                }
            }
            return isvalid;
        }

        public void SendPickupEmailManage(ISStudent _Student, string PickUpStatus)
        {
            try
            {
                if (_Student.EmailAfterConfirmPickUp == true)
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
                                    Please be adviced of your child today " + PickUpStatus + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Message Sent by : <b>" + _School.Name + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any enquiries, please contact " + _School.Name + @"
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
                                    Please be adviced of your child today " + PickUpStatus + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Message Sent by : <b>" + _School.Name + @"</b>
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date and Time Sent : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       For any enquiries, please contact " + _School.Name + @"
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
        public string GetPickupStatus(int? ClassID)
        {
            int schid = Authentication.SchoolID;
            ISSchool school = DB.ISSchools.SingleOrDefault(p => p.ID == schid && p.Deleted == true);
            if (school != null)
            {
                ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now));
                if (completepickup != null)
                {
                    DateTime tdate = DateTime.Now;
                    DateTime schclosetime = Convert.ToDateTime(completepickup.Date);
                    DateTime schclose = new DateTime(tdate.Year, tdate.Month, tdate.Day, schclosetime.Hour, schclosetime.Minute, 0);
                    DateTime schLate = schclose.AddMinutes(Convert.ToInt32(school.LateMinAfterClosing));
                    DateTime schCharge = schLate.AddMinutes(Convert.ToInt32(school.ChargeMinutesAfterClosing));
                    DateTime schReport = schCharge.AddMinutes(Convert.ToInt32(school.ReportableMinutesAfterClosing));
                    if (schclose < tdate)
                    {
                        if (schLate < tdate)
                        {
                            if (tdate > schReport)
                            {
                                return "Picked(Reportable)";
                            }
                            else if (tdate > schCharge)
                            {
                                return "Picked(Chargeable)";
                            }
                            else
                            {
                                return "Picked(Late)";
                            }



                            //if (schCharge < tdate)
                            //{
                            //    if (schReport < tdate)
                            //    {
                            //        return "Picked(Reportable)";
                            //    }
                            //    else
                            //    {
                            //        return "Picked(Chargeable)";
                            //    }
                            //}
                            //else
                            //{
                            //    return "Picked(Late)";
                            //}
                        }
                        else
                        {
                            return "Picked";
                        }
                    }
                    else
                    {
                        return "Picked";
                    }
                }
                else
                {
                    return "Picked";
                }
            }
            else
            {
                return "Picked";
            }
        }

        protected void lstPicker_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                CheckBox chkID = (CheckBox)e.Item.FindControl("chkID");
                HiddenField HiddenID = (HiddenField)e.Item.FindControl("HID");
                int ID = Convert.ToInt32(HiddenID.Value);
                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId);
                if (_Picker != null)
                {
                    if (_Picker.PickerType == 1)
                    {
                        chkID.Visible = false;
                    }
                    else
                    {
                        chkID.Visible = true;
                    }
                }
            }
        }

        protected void chbMarkAbsent_CheckedChanged(object sender, EventArgs e)
        {
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            DateTime dt = DateTime.Now;
            ISPickerAssignment objPickers = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID && (p.PickerCode != null || p.PickerCode != ""));
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == objStudent.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
            bool isCompletePickUpRun = Convert.ToBoolean(Session["chkPicked"]);
            if (isCompletePickUpRun)
            {
                chbMarkAbsent.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed to Picked after Complete PickUp is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.OfficeFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbMarkAbsent.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Office Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.ClubFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbMarkAbsent.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Club Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (DB.ISPickups.Where(p => p.ISStudent.StudentNo == objStudent.StudentNo && p.AfterSchoolFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbMarkAbsent.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from After School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                int CID = 0;
                if (Session["drpClass4"] != null)
                {
                    CID = Convert.ToInt32(Session["drpClass4"]);
                }
                if (DB.ISClasses.Where(p => p.ID == CID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club || p.AfterSchoolType == "Internal")).Count() > 0)
                {
                    chbMarkAbsent.Checked = false;
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Mark as absent not allowed for this Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    ISSchool _Schools = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);
                    if (_Schools.isAttendanceModule == true)
                    {
                        List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                        List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                             select new MViewStudentPickUp
                                                             {
                                                                 ID = item.ID == null ? 0 : item.ID,
                                                                 StudentID = item.StudID,
                                                                 StudentName = item.StudentName,
                                                                 PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                                 Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                                 ClassID = item.ClassID,
                                                                 SchoolID = item.SchoolID,
                                                             }).ToList();
                        if (objLists != null)
                        {
                            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Mark As Absent should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (objLists[0].PickStatus == "Mark as Absent")// || objLists[0].PickStatus == "Mark as Absent"
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Student Already Marked as absent.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count <= 0)
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "First of All fill up the attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && p.Status == "Absent" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Student already Marked as Absent in Attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Not Allowed. Attendence already filled Up as Present.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {
                                if (objLists[0].PickStatus == "After-School-Ex")
                                {
                                    string Studentname = objLists[0].StudentName;
                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                    if (_Student != null)
                                    {
                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                        {
                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                            DB.ISPickups.RemoveRange(_Pickup);
                                            DB.SaveChanges();

                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                            foreach (var item in _Picker)
                                            {
                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                DB.SaveChanges();
                                            }
                                            DB.ISPickers.RemoveRange(_Picker);
                                            DB.SaveChanges();

                                            DB.ISStudents.Remove(_Student);
                                            DB.SaveChanges();
                                        }
                                    }
                                }
                                if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                                {
                                    ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                    if (ObjAttendance == null)
                                    {
                                        chbMarkAbsent.Checked = false;
                                        AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        //ObjAttendance.Status = "Absent";
                                        //DB.SaveChanges();
                                        if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                        {
                                            ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (ObjPickUp != null)
                                            {
                                                ObjPickUp.PickerID = (int?)null;
                                                ObjPickUp.PickStatus = "Mark as Absent";
                                                DB.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            obj.PickStatus = "Mark as Absent";
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        Response.Redirect("Pickup.aspx");
                                    }
                                }
                                else
                                {
                                    //ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                    //if (ObjAttendance == null)
                                    //{
                                    //    ISAttendance objAttendence = new ISAttendance();
                                    //    objAttendence.Date = DateTime.Now;
                                    //    objAttendence.StudentID = ID;
                                    //    objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                    //    objAttendence.Status = "Absent";
                                    //    string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                    //    objAttendence.Time = DateTime.Parse(DTS);
                                    //    objAttendence.Active = true;
                                    //    objAttendence.Deleted = true;
                                    //    objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                    //    objAttendence.CreatedDateTime = DateTime.Now;
                                    //    DB.ISAttendances.Add(objAttendence);
                                    //    DB.SaveChanges();
                                    //}
                                    //else
                                    //{
                                    //    ObjAttendance.Status = "Absent";
                                    //    DB.SaveChanges();
                                    //}
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Mark as Absent";
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                        }
                    }
                    else
                    {
                        List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                        List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                             select new MViewStudentPickUp
                                                             {
                                                                 ID = item.ID == null ? 0 : item.ID,
                                                                 StudentID = item.StudID,
                                                                 StudentName = item.StudentName,
                                                                 PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                                 Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                                 ClassID = item.ClassID,
                                                                 SchoolID = item.SchoolID,
                                                             }).ToList();
                        if (objLists != null)
                        {
                            if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Mark As Absent should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (objLists[0].PickStatus == "Mark as Absent")
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Student Already Marked as absent.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {
                                if (objLists[0].PickStatus == "After-School-Ex")
                                {
                                    string Studentname = objLists[0].StudentName;
                                    ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                    if (_Student != null)
                                    {
                                        if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                        {
                                            List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                            DB.ISPickups.RemoveRange(_Pickup);
                                            DB.SaveChanges();

                                            List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                            foreach (var item in _Picker)
                                            {
                                                List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                                DB.ISPickerAssignments.RemoveRange(_Assign);
                                                DB.SaveChanges();
                                            }
                                            DB.ISPickers.RemoveRange(_Picker);
                                            DB.SaveChanges();

                                            DB.ISStudents.Remove(_Student);
                                            DB.SaveChanges();
                                        }
                                    }
                                }
                                if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                                {

                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Mark as Absent";
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                                else
                                {
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Mark as Absent";
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Mark as Absent";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                        }
                    }
                }

            }
        }

        protected void chbSendAftSchool_CheckedChanged(object sender, EventArgs e)
        {
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);

            bool isCompletePickUpRun = Convert.ToBoolean(Session["chkPicked"]);
            bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            DateTime dt = DateTime.Now;
            ISPickerAssignment objPickers1 = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID && (p.PickerCode != null || p.PickerCode != ""));
            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == objStudent.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
            if (isCompletePickUpRun)
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed to Picked after Complete PickUp is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.OfficeFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Office Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.ClubFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Club Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.ISStudent.StudentNo == objStudent.StudentNo && p.AfterSchoolFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from After School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (_School.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Send to AfterSchool should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (DB.ISClasses.Where(p => p.SchoolID == _School.ID && p.TypeID == (int?)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true && p.Deleted == true).Count() <= 0)
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School Class not Setup in the School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (IsStudentIsFoundInUnPickedInAfterSchool(ID))
            {
                chbSendAftSchool.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Same student might be UnPicked from another school.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                List<ISStudent> _SList = DB.ISStudents.Where(p => p.ID != objStudent.ID && p.StudentName == objStudent.StudentName && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2 || p.ParantEmail1 == objStudent.ParantEmail2 || p.ParantEmail2 == objStudent.ParantEmail1) && p.StartDate == (DateTime?)null && p.Deleted == true).ToList();
                bool ISMarked = false;
                foreach (var items in _SList)
                {
                    if (items.ISSchool.isAttendanceModule == true)
                    {
                        ISAttendance _Attendence = DB.ISAttendances.SingleOrDefault(p => DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt) && p.Status.Contains("Present") && p.StudentID == items.ID && p.Active == true && p.Deleted == true);
                        ISPickup _PickUp = DB.ISPickups.SingleOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.StudentID == items.ID && p.PickStatus == "Not Marked");
                        if (_Attendence != null && _PickUp != null)
                        {
                            ISMarked = true;
                        }
                    }
                    else
                    {
                        ISPickup _PickUp = DB.ISPickups.SingleOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) && p.StudentID == items.ID && (p.PickStatus == "Office" || p.PickStatus == "After-School"));
                        if (_PickUp != null)
                        {
                            ISMarked = true;
                        }
                    }
                }
                int CID = 0;
                if (Session["drpClass4"] != null)
                {
                    CID = Convert.ToInt32(Session["drpClass4"]);
                }
                if (DB.ISClasses.Where(p => p.ID == CID && p.AfterSchoolType == "Internal").Count() > 0)
                {
                    chbSendAftSchool.Checked = false;
                    AlertMessageManagement.ServerMessage(Page, "Not Allowed. Send to AfterSchool not allowed for this Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if (ISMarked == true)
                {
                    chbSendAftSchool.Checked = false;
                    AlertMessageManagement.ServerMessage(Page, "Student already Present in AfterSchool Local Class and Still not Picked from that School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                    List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                         select new MViewStudentPickUp
                                                         {
                                                             ID = item.ID == null ? 0 : item.ID,
                                                             StudentID = item.StudID,
                                                             StudentName = item.StudentName,
                                                             PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                             Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                             ClassID = item.ClassID,
                                                             SchoolID = item.SchoolID,
                                                         }).ToList();
                    if (objLists != null)
                    {
                        ISSchool _Schools = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);
                        if (_Schools.isAttendanceModule == true)
                        {
                            if (objLists[0].PickStatus.Contains("After-School"))// || objLists[0].PickStatus == "Mark as Absent"
                            {
                                chbSendAftSchool.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Student Already Sent to AfterSchool.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count <= 0)
                            {
                                chbMarkAbsent.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "First of All fill up the attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && p.Status == "Absent" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                            {
                                chbSendAftSchool.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Student already Marked as Absent in Attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {
                                if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                                {
                                    ISClass ObjClass = DB.ISClasses.OrderByDescending(p => p.ID).FirstOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == Authentication.SchoolID);
                                    if (ObjClass.AfterSchoolType == "External")
                                    {
                                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.Name.Trim().ToLower() == ObjClass.ExternalOrganisation.Trim().ToLower() && p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active && p.Active == true);
                                        if (ObjSchool != null)
                                        {
                                            List<ISHoliday> _Holidays = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true && p.Deleted == true && p.DateFrom.Value <= dt && p.DateTo.Value >= dt).ToList();
                                            if (_Holidays.Count > 0)
                                            {
                                                chbSendAftSchool.Checked = false;
                                                AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School is Closed for the day.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                                if (pickup != null)
                                                {
                                                    int OfficeID = 0;
                                                    if (Session["drpClass4"] != null)
                                                    {
                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                        if (_Class != null)
                                                        {
                                                            if (pickup.PickStatus == "Office")
                                                            {
                                                                pickup.OfficeFlag = true;
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                            else if (pickup.PickStatus == "Club")
                                                            {
                                                                pickup.ClubFlag = true;
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                        }
                                                    }
                                                    pickup.PickerID = (int?)null;
                                                    pickup.PickTime = DateTime.Now;
                                                    pickup.PickDate = DateTime.Now;
                                                    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    {
                                                        if (ObjClass.AfterSchoolType == "Internal")
                                                        {
                                                            pickup.PickStatus = "After-School";
                                                        }
                                                        else
                                                        {
                                                            pickup.PickStatus = "After-School-Ex";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    DB.SaveChanges();
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objStudent.ID;
                                                    obj.ClassID = objStudent.ClassID;
                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                    obj.PickTime = DateTime.Now;
                                                    obj.PickDate = DateTime.Now;
                                                    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    {
                                                        if (ObjClass.AfterSchoolType == "Internal")
                                                        {
                                                            obj.PickStatus = "After-School";
                                                        }
                                                        else
                                                        {
                                                            obj.PickStatus = "After-School-Ex";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                }

                                                //ISClass ObSClass = DB.ISClasses.SingleOrDefault(p => p.SchoolID == ObjSchool.ID && p.Name.Contains("Outside"));
                                                //if (ObSClass != null)
                                                //{
                                                //    ISStudent objST = new ISStudent();
                                                //    objST.StudentName = objStudent.StudentName;
                                                //    objST.ClassID = ObSClass.ID;
                                                //    objST.StudentNo = objStudent.StudentNo;
                                                //    objST.SchoolID = ObjSchool.ID;
                                                //    objST.Photo = objStudent.Photo;
                                                //    objST.ParantName1 = objStudent.ParantName1;
                                                //    objST.ParantEmail1 = objStudent.ParantEmail1;
                                                //    objST.ParantPhone1 = objStudent.ParantPhone1;
                                                //    objST.ParantRelation1 = objStudent.ParantRelation1;
                                                //    objST.ParantName2 = objStudent.ParantName2;
                                                //    objST.ParantEmail2 = objStudent.ParantEmail2;
                                                //    objST.ParantPhone2 = objStudent.ParantPhone2;
                                                //    objST.ParantRelation2 = objStudent.ParantRelation2;
                                                //    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                                                //    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                                                //    string Message = "";
                                                //    if (objStudent.ParantName1 != "")
                                                //    {
                                                //        objST.ParantPassword1 = EncryptionHelper.Encrypt(ParantPassword1);
                                                //    }
                                                //    if (objStudent.ParantName2 != "" && objStudent.ParantName2 != null)
                                                //    {
                                                //        objST.ParantPassword2 = EncryptionHelper.Encrypt(ParantPassword2);
                                                //    }
                                                //    objST.CreatedBy = ObjSchool.ID;
                                                //    objST.CreatedDateTime = DateTime.Now;
                                                //    objST.Active = true;
                                                //    objST.Deleted = true;
                                                //    objST.Out = 0;
                                                //    objST.Outbit = false;
                                                //    objST.StartDate = DateTime.Now;
                                                //    objST.EndDate = DateTime.Now;
                                                //    DB.ISStudents.Add(objST);
                                                //    DB.SaveChanges();

                                                //    ISPicker objPicker = new ISPicker();
                                                //    objPicker.PickerType = 1;
                                                //    objPicker.SchoolID = ObjSchool.ID;
                                                //    objPicker.ParentID = objST.ID;
                                                //    objPicker.StudentID = objST.ID;
                                                //    objPicker.FirstName = objStudent.ParantName1 + "(" + objStudent.ParantRelation1 + ")";
                                                //    objPicker.Photo = "Upload/user.jpg";
                                                //    objPicker.Email = objStudent.ParantEmail1;
                                                //    objPicker.Phone = objStudent.ParantPhone1;
                                                //    objPicker.OneOffPickerFlag = false;
                                                //    objPicker.ActiveStatus = "Active";
                                                //    objPicker.Active = true;
                                                //    objPicker.Deleted = true;
                                                //    objPicker.CreatedBy = ObjSchool.ID;
                                                //    objPicker.CreatedDateTime = DateTime.Now;
                                                //    DB.ISPickers.Add(objPicker);
                                                //    DB.SaveChanges();
                                                //    if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                                                //    {
                                                //        ISPickerAssignment objAssign = new ISPickerAssignment();
                                                //        objAssign.PickerId = objPicker.ID;
                                                //        objAssign.StudentID = objST.ID;
                                                //        objAssign.RemoveChildStatus = 0;
                                                //        DB.ISPickerAssignments.Add(objAssign);
                                                //        DB.SaveChanges();
                                                //    }
                                                //    if (objST.ParantName2 != null && objST.ParantEmail2 != null)
                                                //    {
                                                //        ISPicker objPickers = new ISPicker();
                                                //        objPickers.PickerType = 1;
                                                //        objPickers.SchoolID = ObjSchool.ID;
                                                //        objPickers.ParentID = objST.ID;
                                                //        objPickers.StudentID = objST.ID;
                                                //        objPickers.FirstName = objStudent.ParantName2 + "(" + objStudent.ParantRelation2 + ")";
                                                //        objPickers.Photo = "Upload/user.jpg";
                                                //        objPickers.Email = objStudent.ParantEmail2;
                                                //        objPickers.Phone = objStudent.ParantPhone2;
                                                //        objPickers.OneOffPickerFlag = false;
                                                //        objPickers.ActiveStatus = "Active";
                                                //        objPickers.Active = true;
                                                //        objPickers.Deleted = true;
                                                //        objPickers.CreatedBy = ObjSchool.ID;
                                                //        objPickers.CreatedDateTime = DateTime.Now;
                                                //        DB.ISPickers.Add(objPickers);
                                                //        DB.SaveChanges();
                                                //        if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                                                //        {
                                                //            ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                //            objAssigns.PickerId = objPickers.ID;
                                                //            objAssigns.StudentID = objST.ID;
                                                //            objAssigns.RemoveChildStatus = 0;
                                                //            DB.ISPickerAssignments.Add(objAssigns);
                                                //            DB.SaveChanges();
                                                //        }
                                                //    }
                                                //    ISPickup objPick = new ISPickup();
                                                //    objPick.StudentID = objST.ID;
                                                //    objPick.ClassID = objST.ClassID;
                                                //    objPick.TeacherID = Authentication.LogginTeacher.ID;
                                                //    objPick.PickTime = DateTime.Now;
                                                //    objPick.PickDate = DateTime.Now;
                                                //    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                //    {
                                                //        objPick.PickStatus = "After-School-Ex";
                                                //    }
                                                //    else
                                                //    {
                                                //        objPick.PickStatus = "After-School-Ex";
                                                //    }
                                                //    DB.ISPickups.Add(objPick);
                                                //    DB.SaveChanges();
                                                //}
                                                Response.Redirect("Pickup.aspx");
                                            }
                                        }
                                        else
                                        {
                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (pickup != null)
                                            {
                                                int OfficeID = 0;
                                                if (Session["drpClass4"] != null)
                                                {
                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                    if (_Class != null)
                                                    {
                                                        if (pickup.PickStatus == "Office")
                                                        {
                                                            pickup.OfficeFlag = true;
                                                            pickup.AfterSchoolFlag = true;
                                                        }
                                                        else if (pickup.PickStatus == "Club")
                                                        {
                                                            pickup.ClubFlag = true;
                                                            pickup.AfterSchoolFlag = true;
                                                        }
                                                    }
                                                }
                                                pickup.PickerID = (int?)null;
                                                pickup.PickTime = DateTime.Now;
                                                pickup.PickDate = DateTime.Now;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                DB.SaveChanges();
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = objStudent.ID;
                                                obj.ClassID = objStudent.ClassID;
                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                obj.PickTime = DateTime.Now;
                                                obj.PickDate = DateTime.Now;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                            }
                                            Response.Redirect("Pickup.aspx");
                                            //chbSendAftSchool.Checked = false;
                                            //AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School is not Activated.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                    }
                                    else
                                    {
                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (pickup != null)
                                        {
                                            int OfficeID = 0;
                                            if (Session["drpClass4"] != null)
                                            {
                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                if (_Class != null)
                                                {
                                                    if (pickup.PickStatus == "Office")
                                                    {
                                                        pickup.OfficeFlag = true;
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                    else if (pickup.PickStatus == "Club")
                                                    {
                                                        pickup.ClubFlag = true;
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                }
                                            }
                                            pickup.PickerID = (int?)null;
                                            pickup.PickTime = DateTime.Now;
                                            pickup.PickDate = DateTime.Now;
                                            if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            {
                                                if (ObjClass.AfterSchoolType == "Internal")
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School-Ex";
                                                }
                                            }
                                            else
                                            {
                                                pickup.PickStatus = "After-School";
                                            }
                                            DB.SaveChanges();
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            {
                                                if (ObjClass.AfterSchoolType == "Internal")
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School-Ex";
                                                }
                                            }
                                            else
                                            {
                                                obj.PickStatus = "After-School";
                                            }
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        Response.Redirect("Pickup.aspx");
                                    }
                                }
                                else
                                {
                                    ISClass ObjClass = DB.ISClasses.SingleOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == Authentication.SchoolID && p.Deleted == true && p.Active == true);
                                    if (ObjClass.AfterSchoolType == "External")
                                    {
                                        ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.Name.Trim().ToLower() == ObjClass.ExternalOrganisation.Trim().ToLower() && p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active && p.Active == true);
                                        if (ObjSchool != null)
                                        {
                                            List<ISHoliday> _Holidays = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true && p.Deleted == true && p.DateFrom.Value <= dt && p.DateTo.Value >= dt).ToList();
                                            if (_Holidays.Count > 0)
                                            {
                                                chbSendAftSchool.Checked = false;
                                                AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School is Closed for the day.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                            }
                                            else
                                            {
                                                ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                                if (pickup != null)
                                                {
                                                    int OfficeID = 0;
                                                    if (Session["drpClass4"] != null)
                                                    {
                                                        OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                        ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                        if (_Class != null)
                                                        {
                                                            if (pickup.PickStatus == "Office")
                                                            {
                                                                pickup.OfficeFlag = true;
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                            else if (pickup.PickStatus == "Club")
                                                            {
                                                                pickup.ClubFlag = true;
                                                                pickup.AfterSchoolFlag = true;
                                                            }
                                                        }
                                                    }
                                                    pickup.PickerID = (int?)null;
                                                    pickup.PickTime = DateTime.Now;
                                                    pickup.PickDate = DateTime.Now;
                                                    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    {
                                                        if (ObjClass.AfterSchoolType == "Internal")
                                                        {
                                                            pickup.PickStatus = "After-School";
                                                        }
                                                        else
                                                        {
                                                            pickup.PickStatus = "After-School-Ex";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    DB.SaveChanges();
                                                }
                                                else
                                                {
                                                    ISPickup obj = new ISPickup();
                                                    obj.StudentID = objStudent.ID;
                                                    obj.ClassID = objStudent.ClassID;
                                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                                    obj.PickTime = DateTime.Now;
                                                    obj.PickDate = DateTime.Now;
                                                    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                    {
                                                        if (ObjClass.AfterSchoolType == "Internal")
                                                        {
                                                            obj.PickStatus = "After-School";
                                                        }
                                                        else
                                                        {
                                                            obj.PickStatus = "After-School-Ex";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    DB.ISPickups.Add(obj);
                                                    DB.SaveChanges();
                                                }

                                                //ISClass ObSClass = DB.ISClasses.SingleOrDefault(p => p.SchoolID == ObjSchool.ID && p.Name.Contains("Outside"));
                                                //if (ObSClass != null)
                                                //{
                                                //    ISStudent objST = new ISStudent();
                                                //    objST.StudentName = objStudent.StudentName;
                                                //    objST.ClassID = ObSClass.ID;
                                                //    objST.StudentNo = objStudent.StudentNo;
                                                //    objST.SchoolID = ObjSchool.ID;
                                                //    objST.Photo = objStudent.Photo;
                                                //    objST.ParantName1 = objStudent.ParantName1;
                                                //    objST.ParantEmail1 = objStudent.ParantEmail1;
                                                //    objST.ParantPhone1 = objStudent.ParantPhone1;
                                                //    objST.ParantRelation1 = objStudent.ParantRelation1;
                                                //    objST.ParantName2 = objStudent.ParantName2;
                                                //    objST.ParantEmail2 = objStudent.ParantEmail2;
                                                //    objST.ParantPhone2 = objStudent.ParantPhone2;
                                                //    objST.ParantRelation2 = objStudent.ParantRelation2;
                                                //    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                                                //    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                                                //    string Message = "";
                                                //    if (objStudent.ParantName1 != "")
                                                //    {
                                                //        objST.ParantPassword1 = EncryptionHelper.Encrypt(ParantPassword1);
                                                //    }
                                                //    if (objStudent.ParantName2 != "" && objStudent.ParantName2 != null)
                                                //    {
                                                //        objST.ParantPassword2 = EncryptionHelper.Encrypt(ParantPassword2);
                                                //    }
                                                //    objST.CreatedBy = ObjSchool.ID;
                                                //    objST.CreatedDateTime = DateTime.Now;
                                                //    objST.Active = true;
                                                //    objST.Deleted = true;
                                                //    objST.Out = 0;
                                                //    objST.Outbit = false;
                                                //    objST.StartDate = DateTime.Now;
                                                //    objST.EndDate = DateTime.Now;
                                                //    DB.ISStudents.Add(objST);
                                                //    DB.SaveChanges();

                                                //    //ISAttendance objAttendence = new ISAttendance();
                                                //    //objAttendence.Date = DateTime.Now.Date;
                                                //    //objAttendence.StudentID = objST.ID;
                                                //    //objAttendence.TeacherID = Authentication.LogginTeacher.ID;
                                                //    //objAttendence.Status = "Present";
                                                //    //string DTS = "01/01/2018 " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ":00.000";
                                                //    //objAttendence.Time = DateTime.Parse(DTS);
                                                //    //objAttendence.Active = true;
                                                //    //objAttendence.Deleted = true;
                                                //    //objAttendence.CreatedBy = Authentication.LogginTeacher.ID;
                                                //    //objAttendence.CreatedDateTime = DateTime.Now;
                                                //    //DB.ISAttendances.Add(objAttendence);
                                                //    //DB.SaveChanges();

                                                //    ISPicker objPicker = new ISPicker();
                                                //    objPicker.PickerType = 1;
                                                //    objPicker.SchoolID = ObjSchool.ID;
                                                //    objPicker.ParentID = objST.ID;
                                                //    objPicker.StudentID = objST.ID;
                                                //    objPicker.FirstName = objStudent.ParantName1 + "(" + objStudent.ParantRelation1 + ")";
                                                //    objPicker.Photo = "Upload/user.jpg";
                                                //    objPicker.Email = objStudent.ParantEmail1;
                                                //    objPicker.Phone = objStudent.ParantPhone1;
                                                //    objPicker.OneOffPickerFlag = false;
                                                //    objPicker.ActiveStatus = "Active";
                                                //    objPicker.Active = true;
                                                //    objPicker.Deleted = true;
                                                //    objPicker.CreatedBy = ObjSchool.ID;
                                                //    objPicker.CreatedDateTime = DateTime.Now;
                                                //    DB.ISPickers.Add(objPicker);
                                                //    DB.SaveChanges();

                                                //    ISPickerAssignment objAssign = new ISPickerAssignment();
                                                //    objAssign.PickerId = objPicker.ID;
                                                //    objAssign.StudentID = objST.ID;
                                                //    objAssign.RemoveChildStatus = 0;
                                                //    DB.ISPickerAssignments.Add(objAssign);
                                                //    DB.SaveChanges();
                                                //    if (objST.ParantName2 != null && objST.ParantEmail2 != null)
                                                //    {
                                                //        ISPicker objPickers = new ISPicker();
                                                //        objPickers.PickerType = 1;
                                                //        objPickers.SchoolID = ObjSchool.ID;
                                                //        objPickers.ParentID = objST.ID;
                                                //        objPickers.StudentID = objST.ID;
                                                //        objPickers.FirstName = objStudent.ParantName2 + "(" + objStudent.ParantRelation2 + ")";
                                                //        objPickers.Photo = "Upload/user.jpg";
                                                //        objPickers.Email = objStudent.ParantEmail2;
                                                //        objPickers.Phone = objStudent.ParantPhone2;
                                                //        objPickers.OneOffPickerFlag = false;
                                                //        objPickers.ActiveStatus = "Active";
                                                //        objPickers.Active = true;
                                                //        objPickers.Deleted = true;
                                                //        objPickers.CreatedBy = ObjSchool.ID;
                                                //        objPickers.CreatedDateTime = DateTime.Now;
                                                //        DB.ISPickers.Add(objPickers);
                                                //        DB.SaveChanges();
                                                //        ISPickerAssignment objAssigns = new ISPickerAssignment();
                                                //        objAssigns.PickerId = objPickers.ID;
                                                //        objAssigns.StudentID = objST.ID;
                                                //        objAssigns.RemoveChildStatus = 0;
                                                //        DB.ISPickerAssignments.Add(objAssigns);
                                                //        DB.SaveChanges();
                                                //    }
                                                //    ISPickup objPick = new ISPickup();
                                                //    objPick.StudentID = objST.ID;
                                                //    objPick.ClassID = objST.ClassID;
                                                //    objPick.TeacherID = Authentication.LogginTeacher.ID;
                                                //    objPick.PickTime = DateTime.Now;
                                                //    objPick.PickDate = DateTime.Now;
                                                //    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                //    {
                                                //        objPick.PickStatus = "After-School-Ex";
                                                //    }
                                                //    else
                                                //    {
                                                //        objPick.PickStatus = "After-School-Ex";
                                                //    }
                                                //    DB.ISPickups.Add(objPick);
                                                //    DB.SaveChanges();
                                                //}
                                                Response.Redirect("Pickup.aspx");
                                            }
                                        }
                                        else
                                        {

                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (pickup != null)
                                            {
                                                int OfficeID = 0;
                                                if (Session["drpClass4"] != null)
                                                {
                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                    if (_Class != null)
                                                    {
                                                        if (pickup.PickStatus == "Office")
                                                        {
                                                            pickup.OfficeFlag = true;
                                                            pickup.AfterSchoolFlag = true;
                                                        }
                                                        else if (pickup.PickStatus == "Club")
                                                        {
                                                            pickup.ClubFlag = true;
                                                            pickup.AfterSchoolFlag = true;
                                                        }
                                                    }
                                                }
                                                pickup.PickerID = (int?)null;
                                                pickup.PickTime = DateTime.Now;
                                                pickup.PickDate = DateTime.Now;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                DB.SaveChanges();
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = objStudent.ID;
                                                obj.ClassID = objStudent.ClassID;
                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                obj.PickTime = DateTime.Now;
                                                obj.PickDate = DateTime.Now;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                            }
                                            Response.Redirect("Pickup.aspx");
                                            //chbSendAftSchool.Checked = false;
                                            //AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School is not Activated.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                    }
                                    else
                                    {
                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (pickup != null)
                                        {
                                            int OfficeID = 0;
                                            if (Session["drpClass4"] != null)
                                            {
                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                if (_Class != null)
                                                {
                                                    if (pickup.PickStatus == "Office")
                                                    {
                                                        pickup.OfficeFlag = true;
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                    else if (pickup.PickStatus == "Club")
                                                    {
                                                        pickup.ClubFlag = true;
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                }
                                            }
                                            //pickup.PickerID = (int?)null;
                                            pickup.PickTime = DateTime.Now;
                                            pickup.PickDate = DateTime.Now;
                                            if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            {
                                                if (ObjClass.AfterSchoolType == "Internal")
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School-Ex";
                                                }
                                            }
                                            else
                                            {
                                                pickup.PickStatus = "After-School";
                                            }
                                            DB.SaveChanges();
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            {
                                                if (ObjClass.AfterSchoolType == "Internal")
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School-Ex";
                                                }
                                            }
                                            else
                                            {
                                                obj.PickStatus = "After-School";
                                            }
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                    }
                                }
                                Response.Redirect("Pickup.aspx");
                            }
                        }
                        else
                        {
                            if (objLists[0].PickStatus.Contains("After-School"))// || objLists[0].PickStatus == "Mark as Absent"
                            {
                                chbSendAftSchool.Checked = false;
                                AlertMessageManagement.ServerMessage(Page, "Student Already Sent to AfterSchool.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            else
                            {

                                ISClass ObjClass = DB.ISClasses.OrderByDescending(p => p.ID).FirstOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == Authentication.SchoolID);
                                if (ObjClass.AfterSchoolType == "External")
                                {
                                    ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.Name.Trim().ToLower() == ObjClass.ExternalOrganisation.Trim().ToLower() && p.AccountStatusID == (int?)EnumsManagement.ACCOUNTSTATUS.Active && p.Active == true);
                                    if (ObjSchool != null)
                                    {
                                        List<ISHoliday> _Holidays = DB.ISHolidays.Where(p => p.SchoolID == ObjSchool.ID && p.Active == true && p.Deleted == true && p.DateFrom.Value <= dt && p.DateTo.Value >= dt).ToList();
                                        if (_Holidays.Count > 0)
                                        {
                                            chbSendAftSchool.Checked = false;
                                            AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School is Closed for the day.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                        }
                                        else
                                        {
                                            ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (pickup != null)
                                            {
                                                int OfficeID = 0;
                                                if (Session["drpClass4"] != null)
                                                {
                                                    OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                    ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                    if (_Class != null)
                                                    {
                                                        if (pickup.PickStatus == "Office")
                                                        {
                                                            pickup.OfficeFlag = true;
                                                            pickup.AfterSchoolFlag = true;
                                                        }
                                                        else if (pickup.PickStatus == "Club")
                                                        {
                                                            pickup.ClubFlag = true;
                                                            pickup.AfterSchoolFlag = true;
                                                        }
                                                    }
                                                }
                                                pickup.PickerID = (int?)null;
                                                pickup.PickTime = DateTime.Now;
                                                pickup.PickDate = DateTime.Now;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        pickup.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        pickup.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                DB.SaveChanges();
                                            }
                                            else
                                            {
                                                ISPickup obj = new ISPickup();
                                                obj.StudentID = objStudent.ID;
                                                obj.ClassID = objStudent.ClassID;
                                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                                obj.PickTime = DateTime.Now;
                                                obj.PickDate = DateTime.Now;
                                                if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                                {
                                                    if (ObjClass.AfterSchoolType == "Internal")
                                                    {
                                                        obj.PickStatus = "After-School";
                                                    }
                                                    else
                                                    {
                                                        obj.PickStatus = "After-School-Ex";
                                                    }
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                DB.ISPickups.Add(obj);
                                                DB.SaveChanges();
                                            }

                                            //ISClass ObSClass = DB.ISClasses.SingleOrDefault(p => p.SchoolID == ObjSchool.ID && p.Name.Contains("Outside"));
                                            //if (ObSClass != null)
                                            //{
                                            //    ISStudent objST = new ISStudent();
                                            //    objST.StudentName = objStudent.StudentName;
                                            //    objST.ClassID = ObSClass.ID;
                                            //    objST.StudentNo = objStudent.StudentNo;
                                            //    objST.SchoolID = ObjSchool.ID;
                                            //    objST.Photo = objStudent.Photo;
                                            //    objST.ParantName1 = objStudent.ParantName1;
                                            //    objST.ParantEmail1 = objStudent.ParantEmail1;
                                            //    objST.ParantPhone1 = objStudent.ParantPhone1;
                                            //    objST.ParantRelation1 = objStudent.ParantRelation1;
                                            //    objST.ParantName2 = objStudent.ParantName2;
                                            //    objST.ParantEmail2 = objStudent.ParantEmail2;
                                            //    objST.ParantPhone2 = objStudent.ParantPhone2;
                                            //    objST.ParantRelation2 = objStudent.ParantRelation2;
                                            //    string ParantPassword1 = CommonOperation.GenerateNewRandom();
                                            //    string ParantPassword2 = CommonOperation.GenerateNewRandom();
                                            //    string Message = "";
                                            //    if (objStudent.ParantName1 != "")
                                            //    {
                                            //        objST.ParantPassword1 = EncryptionHelper.Encrypt(ParantPassword1);
                                            //    }
                                            //    if (objStudent.ParantName2 != "" && objStudent.ParantName2 != null)
                                            //    {
                                            //        objST.ParantPassword2 = EncryptionHelper.Encrypt(ParantPassword2);
                                            //    }
                                            //    objST.CreatedBy = ObjSchool.ID;
                                            //    objST.CreatedDateTime = DateTime.Now;
                                            //    objST.Active = true;
                                            //    objST.Deleted = true;
                                            //    objST.Out = 0;
                                            //    objST.Outbit = false;
                                            //    objST.StartDate = DateTime.Now;
                                            //    objST.EndDate = DateTime.Now;
                                            //    DB.ISStudents.Add(objST);
                                            //    DB.SaveChanges();

                                            //    ISPicker objPicker = new ISPicker();
                                            //    objPicker.PickerType = 1;
                                            //    objPicker.SchoolID = ObjSchool.ID;
                                            //    objPicker.ParentID = objST.ID;
                                            //    objPicker.StudentID = objST.ID;
                                            //    objPicker.FirstName = objStudent.ParantName1 + "(" + objStudent.ParantRelation1 + ")";
                                            //    objPicker.Photo = "Upload/user.jpg";
                                            //    objPicker.Email = objStudent.ParantEmail1;
                                            //    objPicker.Phone = objStudent.ParantPhone1;
                                            //    objPicker.OneOffPickerFlag = false;
                                            //    objPicker.ActiveStatus = "Active";
                                            //    objPicker.Active = true;
                                            //    objPicker.Deleted = true;
                                            //    objPicker.CreatedBy = ObjSchool.ID;
                                            //    objPicker.CreatedDateTime = DateTime.Now;
                                            //    DB.ISPickers.Add(objPicker);
                                            //    DB.SaveChanges();
                                            //    if (DB.ISPickers.Where(p => p.ID == objPicker.ID && p.Active == true).ToList().Count > 0)
                                            //    {
                                            //        ISPickerAssignment objAssign = new ISPickerAssignment();
                                            //        objAssign.PickerId = objPicker.ID;
                                            //        objAssign.StudentID = objST.ID;
                                            //        objAssign.RemoveChildStatus = 0;
                                            //        DB.ISPickerAssignments.Add(objAssign);
                                            //        DB.SaveChanges();
                                            //    }
                                            //    if (objST.ParantName2 != null && objST.ParantEmail2 != null)
                                            //    {
                                            //        ISPicker objPickers = new ISPicker();
                                            //        objPickers.PickerType = 1;
                                            //        objPickers.SchoolID = ObjSchool.ID;
                                            //        objPickers.ParentID = objST.ID;
                                            //        objPickers.StudentID = objST.ID;
                                            //        objPickers.FirstName = objStudent.ParantName2 + "(" + objStudent.ParantRelation2 + ")";
                                            //        objPickers.Photo = "Upload/user.jpg";
                                            //        objPickers.Email = objStudent.ParantEmail2;
                                            //        objPickers.Phone = objStudent.ParantPhone2;
                                            //        objPickers.OneOffPickerFlag = false;
                                            //        objPickers.ActiveStatus = "Active";
                                            //        objPickers.Active = true;
                                            //        objPickers.Deleted = true;
                                            //        objPickers.CreatedBy = ObjSchool.ID;
                                            //        objPickers.CreatedDateTime = DateTime.Now;
                                            //        DB.ISPickers.Add(objPickers);
                                            //        DB.SaveChanges();
                                            //        if (DB.ISPickers.Where(p => p.ID == objPickers.ID && p.Active == true).ToList().Count > 0)
                                            //        {
                                            //            ISPickerAssignment objAssigns = new ISPickerAssignment();
                                            //            objAssigns.PickerId = objPickers.ID;
                                            //            objAssigns.StudentID = objST.ID;
                                            //            objAssigns.RemoveChildStatus = 0;
                                            //            DB.ISPickerAssignments.Add(objAssigns);
                                            //            DB.SaveChanges();
                                            //        }
                                            //    }
                                            //    ISPickup objPick = new ISPickup();
                                            //    objPick.StudentID = objST.ID;
                                            //    objPick.ClassID = objST.ClassID;
                                            //    objPick.TeacherID = Authentication.LogginTeacher.ID;
                                            //    objPick.PickTime = DateTime.Now;
                                            //    objPick.PickDate = DateTime.Now;
                                            //    if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            //    {
                                            //        objPick.PickStatus = "After-School-Ex";
                                            //    }
                                            //    else
                                            //    {
                                            //        objPick.PickStatus = "After-School-Ex";
                                            //    }
                                            //    DB.ISPickups.Add(objPick);
                                            //    DB.SaveChanges();
                                            //}
                                            Response.Redirect("Pickup.aspx");
                                        }
                                    }
                                    else
                                    {
                                        ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (pickup != null)
                                        {
                                            int OfficeID = 0;
                                            if (Session["drpClass4"] != null)
                                            {
                                                OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                                ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                                if (_Class != null)
                                                {
                                                    if (pickup.PickStatus == "Office")
                                                    {
                                                        pickup.OfficeFlag = true;
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                    else if (pickup.PickStatus == "Club")
                                                    {
                                                        pickup.ClubFlag = true;
                                                        pickup.AfterSchoolFlag = true;
                                                    }
                                                }
                                            }
                                            pickup.PickerID = (int?)null;
                                            pickup.PickTime = DateTime.Now;
                                            pickup.PickDate = DateTime.Now;
                                            if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            {
                                                if (ObjClass.AfterSchoolType == "Internal")
                                                {
                                                    pickup.PickStatus = "After-School";
                                                }
                                                else
                                                {
                                                    pickup.PickStatus = "After-School-Ex";
                                                }
                                            }
                                            else
                                            {
                                                pickup.PickStatus = "After-School";
                                            }
                                            DB.SaveChanges();
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                            {
                                                if (ObjClass.AfterSchoolType == "Internal")
                                                {
                                                    obj.PickStatus = "After-School";
                                                }
                                                else
                                                {
                                                    obj.PickStatus = "After-School-Ex";
                                                }
                                            }
                                            else
                                            {
                                                obj.PickStatus = "After-School";
                                            }
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        Response.Redirect("Pickup.aspx");
                                        //chbSendAftSchool.Checked = false;
                                        //AlertMessageManagement.ServerMessage(Page, "Not Allowed. After School is not Activated.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                }
                                else
                                {
                                    ISPickup pickup = DB.ISPickups.FirstOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (pickup != null)
                                    {
                                        int OfficeID = 0;
                                        if (Session["drpClass4"] != null)
                                        {
                                            OfficeID = Convert.ToInt32(Session["drpClass4"]);
                                            ISClass _Class = DB.ISClasses.SingleOrDefault(p => p.ID == OfficeID && (p.TypeID == (int?)EnumsManagement.CLASSTYPE.Office || p.TypeID == (int?)EnumsManagement.CLASSTYPE.Club));
                                            if (_Class != null)
                                            {
                                                if (pickup.PickStatus == "Office")
                                                {
                                                    pickup.OfficeFlag = true;
                                                    pickup.AfterSchoolFlag = true;
                                                }
                                                else if (pickup.PickStatus == "Club")
                                                {
                                                    pickup.ClubFlag = true;
                                                    pickup.AfterSchoolFlag = true;
                                                }
                                            }
                                        }
                                        pickup.PickerID = (int?)null;
                                        pickup.PickTime = DateTime.Now;
                                        pickup.PickDate = DateTime.Now;
                                        if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                        {
                                            if (ObjClass.AfterSchoolType == "Internal")
                                            {
                                                pickup.PickStatus = "After-School";
                                            }
                                            else
                                            {
                                                pickup.PickStatus = "After-School-Ex";
                                            }
                                        }
                                        else
                                        {
                                            pickup.PickStatus = "After-School";
                                        }
                                        DB.SaveChanges();
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        if (ObjClass.AfterSchoolType != null && ObjClass.AfterSchoolType != "")
                                        {
                                            if (ObjClass.AfterSchoolType == "Internal")
                                            {
                                                obj.PickStatus = "After-School";
                                            }
                                            else
                                            {
                                                obj.PickStatus = "After-School-Ex";
                                            }
                                        }
                                        else
                                        {
                                            obj.PickStatus = "After-School";
                                        }
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void chbSendDriver_CheckedChanged(object sender, EventArgs e)
        {
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            DateTime dt = DateTime.Now;
            bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == objStudent.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
            bool isCompletePickUpRun = Convert.ToBoolean(Session["chkPicked"]);
            if (isCompletePickUpRun)
            {
                chbSendDriver.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed to Picked after Complete PickUp is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.OfficeFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendDriver.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Office Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.ClubFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendDriver.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Club Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.ISStudent.StudentNo == objStudent.StudentNo && p.AfterSchoolFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendDriver.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from After School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (IsStudentIsFoundInUnPickedInAfterSchool(ID))
            {
                chbSendDriver.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Same student might be UnPicked from another school.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                     select new MViewStudentPickUp
                                                     {
                                                         ID = item.ID,
                                                         StudentID = item.StudID,
                                                         StudentName = item.StudentName,
                                                         StudentNo = item.StudentNo,
                                                         PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                         Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                         ClassID = item.ClassID,
                                                         SchoolID = item.SchoolID,
                                                     }).ToList();
                if (objLists != null)
                {
                    ISSchool _Schools = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);
                    if (_Schools.isAttendanceModule == true)
                    {
                        if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                        {
                            chbSendDriver.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Send to Office should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count <= 0)
                        {
                            chbSendDriver.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "First of All fill up the attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (objLists[0].PickStatus == "Office")
                        {
                            chbSendDriver.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Student already Sent to Office", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && p.Status == "Absent" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                        {
                            chbSendDriver.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Student already Marked as Absent in Attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else
                        {
                            if (objLists[0].PickStatus == "After-School-Ex")
                            {
                                string Studentname = objLists[0].StudentName;
                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                if (_Student != null)
                                {
                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                    {
                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                        DB.ISPickups.RemoveRange(_Pickup);
                                        DB.SaveChanges();

                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                        foreach (var item in _Picker)
                                        {
                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                            DB.SaveChanges();
                                        }
                                        DB.ISPickers.RemoveRange(_Picker);
                                        DB.SaveChanges();

                                        DB.ISStudents.Remove(_Student);
                                        DB.SaveChanges();
                                    }
                                }
                            }
                            if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                            {
                                if (_School.isAttendanceModule == true)
                                {
                                    ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                    if (ObjAttendance == null)
                                    {
                                        chbSendDriver.Checked = false;
                                        AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                        {
                                            ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (ObjPickUp != null)
                                            {
                                                if (!isStandardClass && ObjPickUp.PickStatus == "Club")
                                                    ObjPickUp.ClubFlag = true;

                                                ObjPickUp.PickerID = (int?)null;
                                                ObjPickUp.PickStatus = "Office";
                                                ObjPickUp.PickDate = DateTime.Now;
                                                ObjPickUp.PickTime = DateTime.Now;
                                                DB.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            obj.PickStatus = "Office";
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        Response.Redirect("Pickup.aspx");
                                    }
                                }
                                else
                                {
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            if (!isStandardClass && ObjPickUp.PickStatus == "Club")
                                                ObjPickUp.ClubFlag = true;

                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Office";
                                            ObjPickUp.PickDate = DateTime.Now;
                                            ObjPickUp.PickTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Office";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                            else
                            {
                                if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                {
                                    ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (ObjPickUp != null)
                                    {
                                        if (string.Compare(ObjPickUp.PickStatus, "After-School", true) == 0)
                                            ObjPickUp.AfterSchoolFlag = true;

                                        ObjPickUp.PickerID = (int?)null;
                                        ObjPickUp.PickStatus = "Office";
                                        ObjPickUp.PickDate = DateTime.Now;
                                        ObjPickUp.PickTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = objStudent.ID;
                                    obj.ClassID = objStudent.ClassID;
                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Office";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                Response.Redirect("Pickup.aspx");
                            }
                        }
                    }
                    else
                    {
                        if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                        {
                            chbSendDriver.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Send to Office should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else if (objLists[0].PickStatus == "Office")
                        {
                            chbSendDriver.Checked = false;
                            AlertMessageManagement.ServerMessage(Page, "Student already Sent to Office", (int)AlertMessageManagement.MESSAGETYPE.warning);
                        }
                        else
                        {
                            if (objLists[0].PickStatus == "After-School-Ex")
                            {
                                string Studentname = objLists[0].StudentName;
                                ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                                if (_Student != null)
                                {
                                    if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                    {
                                        List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                        DB.ISPickups.RemoveRange(_Pickup);
                                        DB.SaveChanges();

                                        List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                        foreach (var item in _Picker)
                                        {
                                            List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                            DB.ISPickerAssignments.RemoveRange(_Assign);
                                            DB.SaveChanges();
                                        }
                                        DB.ISPickers.RemoveRange(_Picker);
                                        DB.SaveChanges();

                                        DB.ISStudents.Remove(_Student);
                                        DB.SaveChanges();
                                    }
                                }
                            }
                            if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                            {
                                if (_School.isAttendanceModule == true)
                                {
                                    ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                    if (ObjAttendance == null)
                                    {
                                        chbSendDriver.Checked = false;
                                        AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                    }
                                    else
                                    {
                                        if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                        {
                                            ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                            if (ObjPickUp != null)
                                            {
                                                if (!isStandardClass && ObjPickUp.PickStatus == "Club")
                                                    ObjPickUp.ClubFlag = true;
                                                else if (!isStandardClass && ObjPickUp.PickStatus == "After-School-Ex")
                                                {
                                                    ObjPickUp.AfterSchoolFlag = true;
                                                }

                                                ObjPickUp.PickerID = (int?)null;
                                                ObjPickUp.PickStatus = "Office";
                                                ObjPickUp.PickDate = DateTime.Now;
                                                ObjPickUp.PickTime = DateTime.Now;
                                                DB.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            ISPickup obj = new ISPickup();
                                            obj.StudentID = objStudent.ID;
                                            obj.ClassID = objStudent.ClassID;
                                            obj.TeacherID = Authentication.LogginTeacher.ID;
                                            obj.PickTime = DateTime.Now;
                                            obj.PickDate = DateTime.Now;
                                            obj.PickStatus = "Office";
                                            DB.ISPickups.Add(obj);
                                            DB.SaveChanges();
                                        }
                                        Response.Redirect("Pickup.aspx");
                                    }
                                }
                                else
                                {
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            if (!isStandardClass && ObjPickUp.PickStatus == "Club")
                                                ObjPickUp.ClubFlag = true;
                                            else if (!isStandardClass && (ObjPickUp.PickStatus == "After-School-Ex" || ObjPickUp.PickStatus == "After-School"))
                                            {
                                                ObjPickUp.AfterSchoolFlag = true;
                                            }

                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Office";
                                            ObjPickUp.PickDate = DateTime.Now;
                                            ObjPickUp.PickTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Office";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                            else
                            {
                                if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                {
                                    ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (ObjPickUp != null)
                                    {
                                        if (!isStandardClass && ObjPickUp.PickStatus == "Club")
                                            ObjPickUp.ClubFlag = true;
                                        else if (!isStandardClass && (ObjPickUp.PickStatus == "After-School-Ex" || ObjPickUp.PickStatus == "After-School"))
                                        {
                                            ObjPickUp.AfterSchoolFlag = true;
                                        }

                                        ObjPickUp.PickerID = (int?)null;
                                        ObjPickUp.PickStatus = "Office";
                                        ObjPickUp.PickDate = DateTime.Now;
                                        ObjPickUp.PickTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = objStudent.ID;
                                    obj.ClassID = objStudent.ClassID;
                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Office";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                Response.Redirect("Pickup.aspx");
                            }
                        }
                    }
                }
            }

        }

        protected void chbSendToClub_CheckedChanged(object sender, EventArgs e)
        {
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            DateTime dt = DateTime.Now;
            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == objStudent.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
            bool isCompletePickUpRun = Convert.ToBoolean(Session["chkPicked"]);
            if (isCompletePickUpRun)
            {
                chbSendToClub.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed to Picked after Complete PickUp is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.ClubFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendToClub.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Club Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.OfficeFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendToClub.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Office Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (isStandardClass && DB.ISPickups.Where(p => p.ISStudent.StudentNo == objStudent.StudentNo && p.AfterSchoolFlag == true && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                chbSendToClub.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from After School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (IsStudentIsFoundInUnPickedInAfterSchool(ID))
            {
                chbSendToClub.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Same student might be UnPicked from another school.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                     select new MViewStudentPickUp
                                                     {
                                                         ID = item.ID,
                                                         StudentID = item.StudID,
                                                         StudentName = item.StudentName,
                                                         StudentNo = item.StudentNo,
                                                         PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                         Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                         ClassID = item.ClassID,
                                                         SchoolID = item.SchoolID,
                                                         ClubFlag = item.ClubFlag.HasValue ? item.ClubFlag.Value : false,
                                                     }).ToList();
                ISSchool _Schools = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);
                if (_Schools.isAttendanceModule == true)
                {
                    if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                    {
                        chbSendToClub.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Send to Club should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count <= 0)
                    {
                        chbSendToClub.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "First of All fill up the attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (objLists[0].PickStatus == "Club")
                    {
                        chbSendToClub.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already Sent to Club", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && p.Status == "Absent" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                    {
                        chbSendToClub.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already Marked as Absent in Attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        if (objLists[0].PickStatus == "After-School-Ex")
                        {
                            string Studentname = objLists[0].StudentName;
                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                            if (_Student != null)
                            {
                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                {
                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                    DB.ISPickups.RemoveRange(_Pickup);
                                    DB.SaveChanges();

                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                    foreach (var item in _Picker)
                                    {
                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                        DB.SaveChanges();
                                    }
                                    DB.ISPickers.RemoveRange(_Picker);
                                    DB.SaveChanges();

                                    DB.ISStudents.Remove(_Student);
                                    DB.SaveChanges();
                                }
                            }
                        }
                        if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                        {
                            if (_School.isAttendanceModule == true)
                            {
                                ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (ObjAttendance == null)
                                {
                                    chbSendToClub.Checked = false;
                                    AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            if (!isStandardClass && ObjPickUp.PickStatus == "Office")
                                            {
                                                ObjPickUp.OfficeFlag = true;
                                            }
                                            else if (!isStandardClass && ObjPickUp.PickStatus == "After-School-Ex")
                                            {
                                                ObjPickUp.AfterSchoolFlag = true;
                                            }

                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Club";
                                            ObjPickUp.PickDate = DateTime.Now;
                                            ObjPickUp.PickTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Club";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                            else
                            {
                                if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                {
                                    ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (ObjPickUp != null)
                                    {
                                        if (!isStandardClass && ObjPickUp.PickStatus == "Office")
                                        {
                                            ObjPickUp.OfficeFlag = true;
                                        }
                                        else if (!isStandardClass && ObjPickUp.PickStatus == "After-School-Ex")
                                        {
                                            ObjPickUp.AfterSchoolFlag = true;
                                        }

                                        ObjPickUp.PickerID = (int?)null;
                                        ObjPickUp.PickStatus = "Club";
                                        ObjPickUp.PickDate = DateTime.Now;
                                        ObjPickUp.PickTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = objStudent.ID;
                                    obj.ClassID = objStudent.ClassID;
                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Club";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                Response.Redirect("Pickup.aspx");
                            }
                        }
                        else
                        {
                            if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                            {
                                ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                if (ObjPickUp != null)
                                {
                                    if (!isStandardClass && ObjPickUp.PickStatus == "Office")
                                    {
                                        ObjPickUp.OfficeFlag = true;
                                    }
                                    else if (!isStandardClass && (ObjPickUp.PickStatus == "After-School-Ex" || ObjPickUp.PickStatus == "After-School"))
                                    {
                                        ObjPickUp.AfterSchoolFlag = true;
                                    }

                                    ObjPickUp.PickerID = (int?)null;
                                    ObjPickUp.PickStatus = "Club";
                                    ObjPickUp.PickDate = DateTime.Now;
                                    ObjPickUp.PickTime = DateTime.Now;
                                    DB.SaveChanges();
                                }
                            }
                            else
                            {
                                ISPickup obj = new ISPickup();
                                obj.StudentID = objStudent.ID;
                                obj.ClassID = objStudent.ClassID;
                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                obj.PickTime = DateTime.Now;
                                obj.PickDate = DateTime.Now;
                                obj.PickStatus = "Club";
                                DB.ISPickups.Add(obj);
                                DB.SaveChanges();
                            }
                            Response.Redirect("Pickup.aspx");
                        }
                    }
                }
                else
                {
                    if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)// || objLists[0].PickStatus == "Mark as Absent"
                    {
                        chbSendToClub.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Send to Club should not be Allowed.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (objLists[0].PickStatus == "Club")
                    {
                        chbSendToClub.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already Sent to Club", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        if (objLists[0].PickStatus == "After-School-Ex")
                        {
                            string Studentname = objLists[0].StudentName;
                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                            if (_Student != null)
                            {
                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                {
                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                    DB.ISPickups.RemoveRange(_Pickup);
                                    DB.SaveChanges();

                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                    foreach (var item in _Picker)
                                    {
                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                        DB.SaveChanges();
                                    }
                                    DB.ISPickers.RemoveRange(_Picker);
                                    DB.SaveChanges();

                                    DB.ISStudents.Remove(_Student);
                                    DB.SaveChanges();
                                }
                            }
                        }
                        if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                        {
                            if (_School.isAttendanceModule == true)
                            {
                                ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (ObjAttendance == null)
                                {
                                    chbSendToClub.Checked = false;
                                    AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            if (!isStandardClass && ObjPickUp.PickStatus == "Office")
                                            {
                                                ObjPickUp.OfficeFlag = true;
                                            }
                                            else if (!isStandardClass && (ObjPickUp.PickStatus == "After-School-Ex" || ObjPickUp.PickStatus == "After-School"))
                                            {
                                                ObjPickUp.AfterSchoolFlag = true;
                                            }
                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Club";
                                            ObjPickUp.PickDate = DateTime.Now;
                                            ObjPickUp.PickTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Club";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                            else
                            {
                                if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                {
                                    ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (ObjPickUp != null)
                                    {
                                        if (!isStandardClass && ObjPickUp.PickStatus == "Office")
                                        {
                                            ObjPickUp.OfficeFlag = true;
                                        }
                                        else if (!isStandardClass && (ObjPickUp.PickStatus == "After-School-Ex" || ObjPickUp.PickStatus == "After-School"))
                                        {
                                            ObjPickUp.AfterSchoolFlag = true;
                                        }

                                        ObjPickUp.PickerID = (int?)null;
                                        ObjPickUp.PickStatus = "Club";
                                        ObjPickUp.PickDate = DateTime.Now;
                                        ObjPickUp.PickTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = objStudent.ID;
                                    obj.ClassID = objStudent.ClassID;
                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Club";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                Response.Redirect("Pickup.aspx");
                            }
                        }
                        else
                        {
                            if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                            {
                                ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                if (ObjPickUp != null)
                                {
                                    ObjPickUp.PickerID = (int?)null;
                                    if (!isStandardClass && ObjPickUp.PickStatus == "Office")
                                    {
                                        ObjPickUp.OfficeFlag = true;
                                    }
                                    else if (!isStandardClass && (ObjPickUp.PickStatus == "After-School-Ex" || ObjPickUp.PickStatus == "After-School"))
                                    {
                                        ObjPickUp.AfterSchoolFlag = true;
                                    }

                                    ObjPickUp.PickStatus = "Club";
                                    ObjPickUp.PickDate = DateTime.Now;
                                    ObjPickUp.PickTime = DateTime.Now;
                                    DB.SaveChanges();
                                }
                            }
                            else
                            {
                                ISPickup obj = new ISPickup();
                                obj.StudentID = objStudent.ID;
                                obj.ClassID = objStudent.ClassID;
                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                obj.PickTime = DateTime.Now;
                                obj.PickDate = DateTime.Now;
                                obj.PickStatus = "Club";
                                DB.ISPickups.Add(obj);
                                DB.SaveChanges();
                            }
                            Response.Redirect("Pickup.aspx");
                        }
                    }
                }
            }
        }

        protected void lnkUnPicked_Click(object sender, EventArgs e)
        {
            ISSchool _School = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID && p.Deleted == true);
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true && p.Active == true);
            DateTime dt = DateTime.Now;
            ISCompletePickupRun completepickup = DB.ISCompletePickupRuns.FirstOrDefault(p => p.ClassID == objStudent.ClassID && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
            bool isStandardClass = Convert.ToBoolean(Session["IsStandardClass"]);

            bool isCompletePickUpRun = Convert.ToBoolean(Session["chkPicked"]);
            if (isCompletePickUpRun)
            {
                //chbUnpicked.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can only be changed to Picked after Complete PickUp is activated", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.OfficeFlag == true && isStandardClass && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                //chbUnpicked.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Office Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && p.ClubFlag == true && isStandardClass && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                //chbUnpicked.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from Club Class.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else if (
                //(objStudent.ISClass.Name != "Local Class" && objStudent.ISClass.Name != "Outside Class") &&
                DB.ISPickups.Where(p => p.ISStudent.StudentNo == objStudent.StudentNo && p.AfterSchoolFlag == true && isStandardClass && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
            {
                //chbUnpicked.Checked = false;
                AlertMessageManagement.ServerMessage(Page, "Pickup Status can not change due to Student Picked from After School.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {
                List<ISHoliday> ObjHoliday = DB.ISHolidays.Where(p => p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true).ToList();
                List<MViewStudentPickUp> objLists = (from item in DB.getPickUpData(dt).Where(p => p.StudID == ID)/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                                                     select new MViewStudentPickUp
                                                     {
                                                         ID = item.ID,
                                                         StudentID = item.StudID,
                                                         StudentName = item.StudentName,
                                                         StudentNo = item.StudentNo,
                                                         PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : "Not Marked" : item.PickStatus,
                                                         Status = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                                                         ClassID = item.ClassID,
                                                         SchoolID = item.SchoolID,
                                                         OfficeFlag = item.OfficeFlag.HasValue ? item.OfficeFlag.Value : false,
                                                         ClubFlag = item.ClubFlag.HasValue ? item.ClubFlag.Value : false,
                                                         AfterSchoolFlag = item.AfterSchoolFlag.HasValue ? item.AfterSchoolFlag.Value : false
                                                     }).ToList();


                if (objLists != null)
                {
                    ISSchool _Schools = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.SchoolID);

                    if (objLists[0].PickStatus == "Not Marked")
                    {
                        //chbUnpicked.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already UnPicked", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (DB.ISAttendances.Where(p => p.StudentID == objStudent.ID && p.Status == "Absent" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                    {
                        //chbUnpicked.Checked = false;
                        AlertMessageManagement.ServerMessage(Page, "Student already Marked as Absent in Attendence.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        string pickStatus = "Not Marked";

                        if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                            pickStatus = "UnPicked";
                        else
                        {
                            int CID = Convert.ToInt32(Session["drpClass4"]);

                            if (isStandardClass)
                            {
                                if ((ObjHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0))
                                    pickStatus = OperationManagement.GetTodayHolidayStatus(ObjHoliday); /// If holiday is found
                                else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                                    pickStatus = "Weekend (School Closed)"; /// School week ends
                            }
                            else
                            {
                                /// Club, Office After class                               


                                if (objLists[0].ClubFlag)
                                    pickStatus = "Club";
                                else if (objLists[0].OfficeFlag)
                                    pickStatus = "Office";
                                else if (objLists[0].AfterSchoolFlag)
                                    pickStatus = "After-School";
                                else
                                    pickStatus = "";
                            }
                        }

                        if (objLists[0].PickStatus == "After-School-Ex")
                        {
                            string Studentname = objLists[0].StudentName;
                            ISStudent _Student = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentName == Studentname && p.StartDate != (DateTime?)null);
                            if (_Student != null)
                            {
                                if (_Student.StartDate.Value.Date == DateTime.Now.Date)
                                {
                                    List<ISPickup> _Pickup = DB.ISPickups.Where(p => p.StudentID == _Student.ID).ToList();
                                    DB.ISPickups.RemoveRange(_Pickup);
                                    DB.SaveChanges();

                                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == _Student.ID && p.Deleted == true).ToList();
                                    foreach (var item in _Picker)
                                    {
                                        List<ISPickerAssignment> _Assign = DB.ISPickerAssignments.Where(p => p.PickerId == item.ID).ToList();
                                        DB.ISPickerAssignments.RemoveRange(_Assign);
                                        DB.SaveChanges();
                                    }
                                    DB.ISPickers.RemoveRange(_Picker);
                                    DB.SaveChanges();

                                    DB.ISStudents.Remove(_Student);
                                    DB.SaveChanges();
                                }
                            }
                        }
                        if (ObjHoliday.Where(x => x.DateFrom.Value <= dt.Date && x.DateTo.Value >= dt.Date).Count() > 0)
                        {
                            if (_School.isAttendanceModule == true)
                            {
                                ISAttendance ObjAttendance = DB.ISAttendances.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == objStudent.ID && p.Status == "Present" && DbFunctions.TruncateTime(p.Date) == DbFunctions.TruncateTime(dt));
                                if (ObjAttendance == null)
                                {
                                    //chbUnpicked.Checked = false;
                                    AlertMessageManagement.ServerMessage(Page, "Action Cannot Be Completed Because School Closed for the Child", (int)AlertMessageManagement.MESSAGETYPE.warning);
                                }
                                else
                                {
                                    if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                    {
                                        ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                        if (ObjPickUp != null)
                                        {
                                            ObjPickUp.PickerID = (int?)null;
                                            ObjPickUp.PickStatus = "Not Marked";
                                            ObjPickUp.PickDate = DateTime.Now;
                                            ObjPickUp.PickTime = DateTime.Now;
                                            DB.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        ISPickup obj = new ISPickup();
                                        obj.StudentID = objStudent.ID;
                                        obj.ClassID = objStudent.ClassID;
                                        obj.TeacherID = Authentication.LogginTeacher.ID;
                                        obj.PickTime = DateTime.Now;
                                        obj.PickDate = DateTime.Now;
                                        obj.PickStatus = "Not Marked";
                                        DB.ISPickups.Add(obj);
                                        DB.SaveChanges();
                                    }
                                    Response.Redirect("Pickup.aspx");
                                }
                            }
                            else
                            {
                                if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                                {
                                    ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                    if (ObjPickUp != null)
                                    {
                                        ObjPickUp.PickerID = (int?)null;
                                        //reset status based on holiday name

                                        if (!string.IsNullOrEmpty(pickStatus))
                                            ObjPickUp.PickStatus = pickStatus;

                                        ObjPickUp.PickDate = DateTime.Now;
                                        ObjPickUp.PickTime = DateTime.Now;
                                        DB.SaveChanges();
                                    }
                                }
                                else
                                {
                                    ISPickup obj = new ISPickup();
                                    obj.StudentID = objStudent.ID;
                                    obj.ClassID = objStudent.ClassID;
                                    obj.TeacherID = Authentication.LogginTeacher.ID;
                                    obj.PickTime = DateTime.Now;
                                    obj.PickDate = DateTime.Now;
                                    obj.PickStatus = "Not Marked";
                                    DB.ISPickups.Add(obj);
                                    DB.SaveChanges();
                                }
                                Response.Redirect("Pickup.aspx");
                            }
                        }
                        else
                        {
                            if (DB.ISPickups.Where(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList().Count > 0)
                            {
                                ISPickup ObjPickUp = DB.ISPickups.SingleOrDefault(p => p.StudentID == objStudent.ID && DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(DateTime.Now));
                                if (ObjPickUp != null)
                                {
                                    ObjPickUp.PickerID = (int?)null;
                                    if (!string.IsNullOrEmpty(pickStatus))
                                        ObjPickUp.PickStatus = pickStatus;

                                    ObjPickUp.PickDate = DateTime.Now;
                                    ObjPickUp.PickTime = DateTime.Now;
                                    DB.SaveChanges();
                                }
                            }
                            else
                            {
                                ISPickup obj = new ISPickup();
                                obj.StudentID = objStudent.ID;
                                obj.ClassID = objStudent.ClassID;
                                obj.TeacherID = Authentication.LogginTeacher.ID;
                                obj.PickTime = DateTime.Now;
                                obj.PickDate = DateTime.Now;
                                obj.PickStatus = "Not Marked";
                                DB.ISPickups.Add(obj);
                                DB.SaveChanges();
                            }
                            Response.Redirect("Pickup.aspx");
                        }
                    }
                }
            }
        }
    }
}