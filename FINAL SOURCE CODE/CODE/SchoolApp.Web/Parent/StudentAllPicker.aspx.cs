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
    public partial class StudentAllPicker : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindData();
            }

            foreach (var items in lstStudentPicker.Items)
            {
                FileUpload fps = (FileUpload)items.FindControl("fpUploads");
                if (IsPostBack && fps.PostedFile != null && fps != null)
                {
                    if (fps.PostedFile.FileName.Length > 0)
                    {
                        int ID = Convert.ToInt32(Request.QueryString["ID"]);
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

                        BindData();
                    }
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

        private void BindData()
        {
            int ID = Authentication.LogginParent.ID;
            var iSStudent = DB.ISStudents.Where(r => r.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || 
                r.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()).ToList();

            var studentIdList = DB.ISStudents.Where(r => r.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() ||
                r.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()).Select(r=>r.ID).ToList();


            var allEmailId = new List<string>();
            allEmailId.AddRange(iSStudent.Where(r => !string.IsNullOrEmpty(r.ParantEmail1)).Select(r => r.ParantEmail1).ToList());
            allEmailId.AddRange(iSStudent.Where(r => !string.IsNullOrEmpty(r.ParantEmail2)).Select(r => r.ParantEmail2).ToList());

            allEmailId = allEmailId.Distinct().ToList();


            List<MISPICKER> obj = (from item in DB.ISPickers.Where(p => /* p.CreatedBy == ID &&*/ allEmailId.Contains(p.CreatedByEmail) && p.Deleted == true).ToList()
                                   select new MISPICKER
                                   {
                                       ID = item.ID,
                                       PickerName = item.FirstName + " " + item.LastName,
                                       Email = !String.IsNullOrEmpty(item.Email) ? item.Email : " Enter Email",
                                       Phone = !String.IsNullOrEmpty(item.Phone) ? item.Phone : " Enter Phone Number",
                                       Photo = item.Photo,
                                       PickerType = item.PickerType,
                                       ActiveStatus = item.Active == true ? "Active" : "Inactive",
                                       ParentName = item.CreatedByName,
                                       //DB.ISPickerAssignments.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == item.StudentID && p.RemoveChildStatus == 0 && 
                                       //         (p.PickCodayExDate == null || 
                                       //         DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))) != null ? 
                                       //                 DB.ISPickerAssignments.OrderByDescending(p => p.ID).FirstOrDefault(p => p.StudentID == item.StudentID && p.RemoveChildStatus == 0 
                                       //                 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))).StudentAssignBy : item.CreatedByName,
                                       StudentID = item.StudentID,
                                       Active = item.Active,
                                       CreatedByEmail = item.CreatedByEmail,
                                       IsPickerCreatedByMe= item.CreatedByEmail.ToLower() == Authentication.LoginParentEmail.ToLower(),
                                   }).ToList();

            List<MISPICKER> obj1 = new List<MISPICKER>();
            foreach (var item in obj)
            {
                if(!string.IsNullOrEmpty(item.CreatedByEmail) && item.CreatedByEmail.ToLower() != Authentication.LoginParentEmail.ToLower())
                {
                    if(DB.ISPickerAssignments.Where(r => r.PickerId == item.ID && studentIdList.Contains(r.StudentID.Value) && r.RemoveChildStatus == 0).Count() > 0)
                    {
                        obj1.Add(item);
                    }
                }
                else
                    obj1.Add(item);
            }


            lstStudentPicker.DataSource = obj1.Where(p => !p.PickerName.Contains("(")).ToList().OrderBy(r=>r.PickerName).ToList();
            lstStudentPicker.DataBind();
        }

        protected void lstStudentPicker_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstStudentPicker.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData();
        }

        protected void lstStudentPicker_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkEditAssignment")
            {
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Deleted == true);
                int ID = Convert.ToInt32(e.CommandArgument);
                string PName = ObjStudent.ParantName1 + "(" + ObjStudent.ParantRelation1 + ")";
                string SName = ObjStudent.ParantName2 + "(" + ObjStudent.ParantRelation2 + ")";
                ISPicker ObjPicker = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
                if (ObjPicker.Active == true)
                {
                    ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == ID && (p.FirstName == PName || p.FirstName == SName));
                    if (Obj != null)
                    {
                        AlertMessageManagement.ServerMessage(Page, "This Picker can only be amended by School. Contact School Admin.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        Response.Redirect("EditChildrenAllAssignment.aspx?ID=" + ID);
                    }
                }
                else
                {
                    AlertMessageManagement.ServerMessage(Page, "This Picker is InActive. Please Activate to perform this Action", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
            }
            if (e.CommandName == "lnkEdit")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Deleted == true);
                string PName = ObjStudent.ParantName1 + "(" + ObjStudent.ParantRelation1 + ")";
                string SName = ObjStudent.ParantName2 + "(" + ObjStudent.ParantRelation2 + ")";
                ISPicker ObjPicker = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
                ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.ID == ID && (p.FirstName == PName || p.FirstName == SName));
                if (Obj != null)
                {
                    AlertMessageManagement.ServerMessage(Page, "This Picker can only be amended by School. Contact School Admin.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    Response.Redirect("EditPicker.aspx?ID=" + ID);
                }
            }
            if (e.CommandName == "lnkDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
                if (objPicker != null)
                {
                    if (objPicker.CreatedByEmail.ToLower() == Authentication.LoginParentEmail.ToLower())
                    {

                        objPicker.Active = false;
                        objPicker.Deleted = false;
                        objPicker.DeletedBy = Authentication.LogginParent.ID;
                        objPicker.DeletedDateTime = DateTime.Now;
                        DB.SaveChanges();
                        ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == objPicker.StudentID && p.Deleted == true);
                        SendDeleteEmailManage(_Student, objPicker);
                        AlertMessageManagement.ServerMessage(Page, "Picker Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.info);
                    }
                    else
                        AlertMessageManagement.ServerMessage(Page, "You do not have rights to delete picker.", (int)AlertMessageManagement.MESSAGETYPE.Error);
                }
                BindData();
                
            }
        }

        protected void btnCreatePicker_Click(object sender, EventArgs e)
        {
            int ID = Authentication.LogginParent.ID;
            Response.Redirect("CreatePicker.aspx?ID=" + ID);
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
                    DB.ISPickerAssignments.RemoveRange(ObjPickerAssi);
                    DB.SaveChanges();
                    AlertMessageManagement.ServerMessage(Page, "Picker Deactivated Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                BindData();
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
        public void SendDeleteEmailManage(ISStudent _Student, ISPicker _Picker)
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", removed by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please contact " + LoggedinParent + @" for any enquiries
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", removed by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please contact " + LoggedinParent + @" for any enquiries
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
        //protected void lstStudentPicker_ItemDataBound(object sender, ListViewItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListViewItemType.DataItem)
        //    {
        //        foreach (ListViewDataItem item in lstStudentPicker.Items)
        //        {
        //            CheckBox cb = (CheckBox)item.FindControl("CheckBox1");
        //            int ID = Convert.ToInt32(cb.ToolTip);
        //            ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
        //            if (objPicker != null)
        //            {
        //                if (objPicker.Active == true)
        //                {
        //                    cb.Checked = true;
        //                }
        //                else
        //                {
        //                    cb.Checked = false;
        //                }
        //            }

        //        }
        //    }
        //}
    }
}