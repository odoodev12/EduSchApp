using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class StudentPicker : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }

            int studentId = GetStudentId();


            if (!IsPostBack)
            {
                try
                {
                    DB.BusinessRuleClearData();
                }
                catch (Exception ex)
                {

                }
                //if (Request.QueryString["ID"] != null)
                {
                    //int StudentID = Convert.ToInt32(Request.QueryString["ID"]);
                    BindStudentData(studentId);
                    BindData(studentId);
                    lblStudentName.Text = DB.ISStudents.SingleOrDefault(p => p.ID == studentId).StudentName;

                    lblSchoolName.Text = "@ " + Convert.ToString(Request.QueryString["SchoolName"]);
                }
            }
            foreach (var item in lstStudent.Items)
            {
                FileUpload fp = (FileUpload)item.FindControl("fpUpload");
                if (IsPostBack && fp.PostedFile != null && fp != null)
                {
                    if (fp.PostedFile.FileName.Length > 0)
                    {
                        int ID = studentId; //Convert.ToInt32(Request.QueryString["ID"]);
                        ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                        if (objStudent != null)
                        {
                            string filename = System.IO.Path.GetFileName(fp.PostedFile.FileName);
                            fp.SaveAs(Server.MapPath("~/Upload/" + filename));
                            objStudent.Photo = "Upload/" + filename;
                            DB.SaveChanges();
                            SendStudentImageEmailManage(objStudent);
                            AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        BindStudentData(ID);
                    }
                }
            }
            foreach (var items in lstStudentPicker.Items)
            {
                FileUpload fps = (FileUpload)items.FindControl("fpUploads");
                if (IsPostBack && fps.PostedFile != null && fps != null)
                {
                    if (fps.PostedFile.FileName.Length > 0)
                    {
                        int ID = studentId; //Convert.ToInt32(Request.QueryString["ID"]);
                        HiddenField hid = (HiddenField)items.FindControl("HIDImage");
                        int HIDValue = Convert.ToInt32(hid.Value);
                        ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.ID == HIDValue && p.Deleted == true);
                        if (objPicker != null)
                        {
                            if (objPicker.PickerType == (int)EnumsManagement.PICKERTYPE.Individual)
                            {
                                string filename = System.IO.Path.GetFileName(fps.PostedFile.FileName);
                                fps.SaveAs(Server.MapPath("~/Upload/" + filename));
                                objPicker.Photo = "Upload/" + filename;
                                DB.SaveChanges();
                                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                                SendPickerImageEmailManage(_Student, objPicker);
                                AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                            }
                            else
                            {
                                AlertMessageManagement.ServerMessage(Page, "You can not change Organisation Picker Image", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                        }
                        BindData(ID);
                    }
                }
            }
        }

        private int GetStudentId()
        {
            int studentId = 0;
            if (Request.QueryString["SchoolName"] != null && Request.QueryString["StudentName"] != null)
            {
                string schoolName = Convert.ToString(Request.QueryString["SchoolName"]);
                string StudentName = Convert.ToString(Request.QueryString["StudentName"]);

                int schoolId = DB.ISSchools.FirstOrDefault(r => r.Name == schoolName).ID;

                studentId = DB.ISStudents.FirstOrDefault(r => r.StudentName.ToLower() == StudentName.ToLower() &&
                (r.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || r.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()) &&
                r.Deleted == true && r.Deleted == true && r.SchoolID == schoolId).ID;


            }
            return studentId;
        }

        private void BindStudentData(int StudentID)
        {
            List<MISStudent> obj = (from item in DB.ISStudents.Where(p => p.ID == StudentID && p.Active == true && p.Deleted == true).ToList()
                                    select new MISStudent
                                    {
                                        ID = item.ID,
                                        StudentName = item.StudentName,
                                        StudentPic = item.Photo,
                                        StudentNo = item.StudentNo,
                                    }).ToList();
            lstStudent.DataSource = obj.ToList();
            lstStudent.DataBind();
        }

        private void BindData(int StudentID)
        {
            //PickerManagement objPickerManagement = new PickerManagement();
            //List<MISPickerAssignment> ObjList = objPickerManagement.GetPickerAssignment(StudentID);
            PickerManagement objPickerManagement = new PickerManagement();


            //string studentName = DB.ISStudents.FirstOrDefault(r => r.ID == StudentID).StudentName;

            //List<int> studentIdList = DB.ISStudents.Where(r => r.StudentName.ToLower() == studentName.ToLower() && r.Active == true && r.Deleted == true &&
            //(r.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() ||
            //r.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower())).Select(r => r.ID).ToList();


            List<MISPickerAssignment> objList = new List<MISPickerAssignment>();

            //foreach (var studId in studentIdList)
            {
                objList.AddRange(objPickerManagement.GetPickerAssignmentToday(StudentID));
            }


            if (objList.Count > 0)
            {
                HID.Value = "True";
            }
            else
            {
                HID.Value = "False";
            }
            List<string> firstFixedValue = new List<string>
            {
                "Mother", "Father","Guardian"
            };
            List<MISPickerAssignment> objList2 = new List<MISPickerAssignment>();


            //foreach (var studId in studentIdList)
            {
                objList2.AddRange(objPickerManagement.GetPickerAssignment(StudentID));
            }


            objList2 = objList2.OrderBy(r => r.PickerName).ToList();

            foreach (MISPickerAssignment item in objList2)
            {
                if (item.StudentID == StudentID)
                {
                    if (item.PickerName.Contains('(') && objList.Where(p => p.ID == item.ID).Count() <= 0)
                    {
                        objList.Add(item);
                    }
                }
            }
            ISStudent iSStudent = DB.ISStudents.FirstOrDefault(r => r.ID == StudentID);
            foreach (MISPickerAssignment item in objList2)
            {
                if (objList.Where(p => p.PickerId == item.PickerId).Count() <= 0 && !item.PickerName.Contains('('))
                {
                    ///Remove this if condition if not properly working by selecting changes school id for same student
                    ///7/5/2020 date: made this if condition.
                    //if (item.CreatedByEmail.ToLower() == iSStudent.ParantEmail1.ToLower() || item.CreatedByEmail.ToLower() == iSStudent.ParantEmail2.ToLower())
                        objList.Add(item);
                }
            }

            lstStudentPicker.DataSource = objList.ToList();
            lstStudentPicker.DataBind();
            if (objList.Count == 0)
            {
                lblMessage.Text = "No Picker Assigned Yet to Child";
            }
        }

        protected void lstStudentPicker_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstStudentPicker.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            int StudentID = GetStudentId(); //Convert.ToInt32(Request.QueryString["ID"]);
            BindData(StudentID);
        }

        protected void lstStudentPicker_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnGenerateCode")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);

                var result = OperationManagement.IsValidIndividaulPickerFor(objPicker.PickerId.Value, 7);

                int noOfDayRemaining = result.Item3;
                ;
                string appendMessage = ".";

                if (!result.Item1 && !result.Item2)
                {
                    AlertMessageManagement.ServerMessage(Page, "Uploaded image requires for codes to be generated .", (int)AlertMessageManagement.MESSAGETYPE.Error);
                }
                else
                {
                    if (noOfDayRemaining >= 0 && !result.Item2)
                        appendMessage = $", but you have to Upload Picker picture. {noOfDayRemaining} days left to generate code";

                    bool isAllowedToGenerateCode = objPicker != null &&
                        ((objPicker.PickCodeExDate != null && objPicker.PickCodeExDate.Value.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                        || (objPicker.PickCodeExDate == null || string.IsNullOrEmpty(objPicker.PickerCode)));


                    if (!isAllowedToGenerateCode)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Picker Code Already Generated and Sent via Mail.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        var noOfPickers = new List<ISPickerAssignment>();
                        noOfPickers.Add(objPicker);

                        if ((e.Item.FindControl("chkGenerateCodeForAll") as CheckBox).Checked)
                        {
                            noOfPickers = DB.ISPickerAssignments.Where(r => r.PickerId == objPicker.PickerId.Value).ToList();
                        }

                        foreach (var item in noOfPickers)
                        {
                           if(item.PickCodeExDate !=null && item.PickCodeExDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && item.PickerCode != null && item.PickerCode.Length > 0)
                            {
                                continue;
                            }

                            item.PickerCode = CommonOperation.GenerateNewRandom();
                            item.PickCodeExDate = DateTime.Now;
                            DB.SaveChanges();

                            Button btn = (Button)e.Item.FindControl("btnGeneratePickUpCode");
                            btn.Text = item.PickerCode;
                            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == item.StudentID && p.Deleted == true);
                            ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == item.PickerId && p.Deleted == true);
                            SendCodeEmailManage(_Student, _Picker, item.PickerCode);
                        }
                        AlertMessageManagement.ServerMessage(Page, $"Code Generated Successfully{appendMessage}", (int)AlertMessageManagement.MESSAGETYPE.Success);

                    }
                }
            }
            if (e.CommandName == "btnRemove")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                int StudentID = GetStudentId(); //Convert.ToInt32(Request.QueryString["ID"]);
                ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID);
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true && p.Deleted == true);
                string PName = ObjStudent.ParantName1 + "(" + ObjStudent.ParantRelation1 + ")";
                string SName = ObjStudent.ParantName2 + "(" + ObjStudent.ParantRelation2 + ")";
                ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == objPicker.PickerId && (p.FirstName == PName || p.FirstName == SName));
                if (Obj != null)
                {
                    AlertMessageManagement.ServerMessage(Page, "This Picker can only be amended by School. Contact School Admin.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    if (objPicker != null)
                    {
                        var ObjListPicker = DB.ISPickerAssignments.Where(r => r.PickerId == objPicker.PickerId && r.StudentID == objPicker.StudentID).ToList();

                        foreach (var item in ObjListPicker)
                        {
                            item.StudentAssignBy = "";
                            item.RemoveChildStatus = 1;
                            item.RemoveChildLastUpdateDate = DateTime.Now;
                            item.RemoveChildLastupdateParent = Authentication.LogginParent.ID.ToString();
                            DB.SaveChanges(); 
                        }
                    }
                    BindData(StudentID);
                    AlertMessageManagement.ServerMessage(Page, "Picker Removed Successfully", (int)AlertMessageManagement.MESSAGETYPE.info);
                }
            }
            if (e.CommandName == "btnMakePickerToday")
            {
                Button btn = (Button)e.Item.FindControl("btnMakePickerToday");
                int ID = Convert.ToInt32(e.CommandArgument);
                int StudentID = GetStudentId(); //Convert.ToInt32(Request.QueryString["ID"]);
                if (btn.Text == "Make Picker for Today")
                {
                    List<ISPickerAssignment> objList = DB.ISPickerAssignments.Where(p => p.StudentID == StudentID && p.RemoveChildStatus != 1 && DbFunctions.TruncateTime(p.StudentPickAssignDate) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                    if (objList.Count > 0)
                    {
                        foreach (var item in objList)
                        {
                            item.StudentPickAssignDate = null;
                            DB.SaveChanges();
                        }
                        ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID && p.RemoveChildStatus != 1);

                        var result = OperationManagement.IsValidIndividaulPickerFor(obj.PickerId.Value, 7);
                        bool isCodeRequired = false;
                        if (result.Item1 && !result.Item2)
                        {
                            if (obj.PickCodeExDate == null)
                                isCodeRequired = true;
                            else if (obj.PickCodeExDate.Value.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                                isCodeRequired = true;
                        }

                        string appendMessage = ".";

                        if ((!result.Item1 && !result.Item2))
                        {
                            AlertMessageManagement.ServerMessage(Page, "Uploaded image is required to make this Picker a Picker Today.", (int)AlertMessageManagement.MESSAGETYPE.Error);
                        }
                        else if ((result.Item1 && !result.Item2) && isCodeRequired)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Upload image or generate code to make this Picker Today.", (int)AlertMessageManagement.MESSAGETYPE.Error);
                        }
                        else
                        {
                            if (obj != null)
                            {
                                if (result.Item3 >= 0 && !result.Item2)
                                    appendMessage = $"{result.Item3} days left to Codes or uploaded image will be required for this picker";

                                HID.Value = "True";
                                obj.StudentPickAssignFlag = 1;
                                obj.StudentPickAssignLastUpdateParent = Authentication.LogginParent.ID.ToString();
                                obj.StudentPickAssignDate = DateTime.Now;
                                DB.SaveChanges();
                                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == obj.StudentID && p.Deleted == true);
                                ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == obj.PickerId && p.Deleted == true);
                                SendMakeEmailManage(_Student, _Picker);
                                Clear();
                            }
                            AlertMessageManagement.ServerMessage(Page, $"Picker Made for Today Successfully. {appendMessage}", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                    }
                    else
                    {
                        ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID && p.RemoveChildStatus != 1);

                        var result = OperationManagement.IsValidIndividaulPickerFor(obj.PickerId.Value, 7);
                        bool isCodeRequired = false;
                        if (result.Item1 && !result.Item2)
                        {
                            if (obj.PickCodeExDate == null)
                                isCodeRequired = true;
                            else if (obj.PickCodeExDate.Value.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                                isCodeRequired = true;
                        }

                        string appendMessage = ".";

                        if ((!result.Item1 && !result.Item2))
                        {
                            AlertMessageManagement.ServerMessage(Page, "Uploaded image is required to make this Picker a Picker Today.", (int)AlertMessageManagement.MESSAGETYPE.Error);
                        }
                        else if ((result.Item1 && !result.Item2) && isCodeRequired)
                        {
                            AlertMessageManagement.ServerMessage(Page, "Upload image or generate code to make this Picker Today.", (int)AlertMessageManagement.MESSAGETYPE.Error);
                        }
                        else
                        {
                            if (obj != null)
                            {
                                if (result.Item3 >= 0 && !result.Item2)
                                    appendMessage = $"{result.Item3} days left to Codes or uploaded image will be required for this picker";

                                HID.Value = "True";
                                obj.StudentPickAssignFlag = 1;
                                obj.StudentPickAssignLastUpdateParent = Authentication.LogginParent.ID.ToString();
                                obj.StudentPickAssignDate = DateTime.Now;
                                DB.SaveChanges();
                                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == obj.StudentID && p.Deleted == true);
                                ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == obj.PickerId && p.Deleted == true);
                                SendMakeEmailManage(_Student, _Picker);
                                Clear();
                            }
                            AlertMessageManagement.ServerMessage(Page, $"Picker Made for Today Successfully. {appendMessage}", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                    }
                }
                if (btn.Text == "Reset Picker Today")
                {
                    ISPickerAssignment obj = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == ID && p.RemoveChildStatus != 1);
                    if (obj != null)
                    {
                        HID.Value = "False";
                        obj.StudentPickAssignDate = (DateTime?)null;
                        DB.SaveChanges();
                        Clear();
                        ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == obj.StudentID && p.Deleted == true);
                        ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == obj.PickerId && p.Deleted == true);
                        SendRemoveEmailManage(_Student, _Picker);
                    }
                    AlertMessageManagement.ServerMessage(Page, "Picker Reset for Today Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                BindData(StudentID);
            }
        }
        protected void Clear()
        {

        }
        public void SendCodeEmailManage(ISStudent _Student, ISPicker _Picker, string Code)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian  of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    A pickup code has been generated for picking " + _Student.StudentName + @" today. This code is only valid for a one-time use and will expire after it has been used or override if a new code is generated  by any of the parents
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       It is recommended that you contact " + _Student.StudentName + @"'s parents if you are unsure of the location of the child for this pickup
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Pickup Code : " + Code + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Code Generated By : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Generated : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Generated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Generate Pickup Code Notification", SuperwisorBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Generate Pickup Code Notification", SuperwisorBody);
                }
                if (!String.IsNullOrEmpty(_Picker.Email))
                {
                    _EmailManagement.SendEmail(_Picker.Email, "Generate Pickup Code Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendMakeEmailManage(ISStudent _Student, ISPicker _Picker)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Picker.FirstName + " " + _Picker.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    This is to inform you that you have been selected as 'Picker for today' for " + _Student.StudentName + @" by " + LoggedinParent + @"
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       What this means is that relevant organisation has been informed that unless amended, you are the only person with the authorisation to pick the child.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please contact " + _Student.StudentName + @"'s parents if you are unsure of the location of the child for the pickup
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       It is important that you notify the parents if for any reason you will be unable to pick the child as failure to do so may result in charges or an official report to the relevant child protection agency
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Generated : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Generated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Make Picker Today Notification", SuperwisorBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Make Picker Today Notification", SuperwisorBody);
                }
                if (!String.IsNullOrEmpty(_Picker.Email))
                {
                    _EmailManagement.SendEmail(_Picker.Email, "Make Picker Today Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendRemoveEmailManage(ISStudent _Student, ISPicker _Picker)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _Picker.FirstName + " " + _Picker.LastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    This is to inform you that you have been removed as 'Picker for today' for " + _Student.StudentName + @" by " + LoggedinParent + @"
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please contact " + _Student.StudentName + @"'s parents whose are removed you to Pick the Child for today.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Generated : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Generated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Remove Picker Today Notification", SuperwisorBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Remove Picker Today Notification", SuperwisorBody);
                }
                if (!String.IsNullOrEmpty(_Picker.Email))
                {
                    _EmailManagement.SendEmail(_Picker.Email, "Remove Picker Today Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void lstStudentPicker_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField HID = (HiddenField)e.Item.FindControl("HID2");
                HiddenField HIDEmail = (HiddenField)e.Item.FindControl("HIDEmail");
                string Email = HIDEmail.Value.ToString();
                LinkButton LnkEmail = (LinkButton)e.Item.FindControl("LnkEmail");
                HiddenField HIDPickerID = (HiddenField)e.Item.FindControl("HIDImage");
                HiddenField HIDPhone = (HiddenField)e.Item.FindControl("HIDPhone");
                string Phone = HIDPhone.Value.ToString();
                LinkButton LnkPhone = (LinkButton)e.Item.FindControl("lnkPhone");
                Button btn = (Button)e.Item.FindControl("btnGeneratePickUpCode");
                Button btnRemove = (Button)e.Item.FindControl("btnRemove");
                Button btnToday = (Button)e.Item.FindControl("btnMakePickerToday");
                FileUpload fpUploads = (FileUpload)e.Item.FindControl("fpUploads");
                string Name = HID.Value.ToString();
                HiddenField HIDPickerType = (HiddenField)e.Item.FindControl("HIDPickerType");
                int PickertType = Convert.ToInt32(HIDPickerType.Value);
                if (PickertType == (int)EnumsManagement.PICKERTYPE.Organisation)
                {
                    fpUploads.Enabled = false;
                }
                if (Name.Contains("("))
                {
                    //btn.Enabled = false;
                    //btn.CssClass = "btn btn-primary btn-block";
                    //btn.Attributes.Add("style", "background: white; border: 1px solid #cccccc; color: #232323; padding: 10px;");

                    btnRemove.Enabled = false;
                    btnRemove.CssClass = "btn btn-primary btn-block";
                    btnRemove.Attributes.Add("style", "margin-top: 10px;");

                    //btnToday.Enabled = false;
                    //btnToday.CssClass = "btn btn-primary btn-block";
                    //btnToday.Attributes.Add("style", "margin-top: 10px;");
                }
                if (Phone.Contains(" Enter Phone Number"))
                {
                    int PID = Convert.ToInt32(HIDPickerID.Value.ToString());
                    ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == PID && p.Deleted == true);
                    if (_Picker != null)
                    {
                        if (_Picker.CreatedByName == Authentication.LoginParentName && _Picker.CreatedByEmail == Authentication.LoginParentEmail)
                        {
                            LnkPhone.Enabled = true;
                            LnkPhone.PostBackUrl = "EditPicker.aspx?ID=" + HIDPickerID.Value.ToString();
                        }
                        else
                        {
                            LnkPhone.PostBackUrl = "#";
                            LnkPhone.Enabled = false;
                            LnkPhone.ForeColor = Color.DimGray;
                        }
                    }

                }
                else
                {
                    LnkPhone.PostBackUrl = "#";
                    LnkPhone.Enabled = false;
                    LnkPhone.ForeColor = Color.DimGray;
                }

                if (Email.Contains(" Enter Email"))
                {
                    int PID = Convert.ToInt32(HIDPickerID.Value.ToString());
                    ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == PID && p.Deleted == true);
                    if (_Picker != null)
                    {
                        if (_Picker.CreatedByName == Authentication.LoginParentName && _Picker.CreatedByEmail == Authentication.LoginParentEmail)
                        {
                            LnkEmail.Enabled = true;
                            LnkEmail.PostBackUrl = "EditPicker.aspx?ID=" + HIDPickerID.Value.ToString();
                        }
                        else
                        {
                            LnkEmail.PostBackUrl = "#";
                            LnkEmail.Enabled = false;
                            LnkEmail.ForeColor = Color.DimGray;
                        }
                    }

                }
                else
                {
                    LnkEmail.PostBackUrl = "#";
                    LnkEmail.Enabled = false;
                    LnkEmail.ForeColor = Color.DimGray;
                }
            }
        }
        public void SendPickerImageEmailManage(ISStudent _Student, ISPicker _Picker)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian's of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", image updated by " + LoggedinParent + @"
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment by : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Generated : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Generated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Browse Picker Picture Notification", SuperwisorBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Browse Picker Picture Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendStudentImageEmailManage(ISStudent _Student)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian's of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student , " + _Student.StudentName + @", image updated by " + LoggedinParent + @"
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment by : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Generated : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Generated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Browse Picker Picture Notification", SuperwisorBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Browse Picker Picture Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }
}