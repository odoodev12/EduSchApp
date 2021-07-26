using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class CreatePicker : System.Web.UI.Page
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
            }
        }

        protected void btnAssignToChildren_Click(object sender, EventArgs e)
        {
            try
            {
                String email = txtEmail.Text;
                if (String.IsNullOrEmpty(txtEmail.Text) && drpContact.SelectedValue == "1")
                {
                    AlertMessageManagement.ServerMessage(Page, "Email must be entered to contact this account in the event of pickup emergency.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    List<ISPicker> _Picker = DB.ISPickers.Where(p => p.StudentID == Authentication.LogginParent.ID && p.Deleted == true).ToList();

                    bool isFLNameExist = _Picker.Any(r => (string.Compare(r.FirstName, txtFirstName.Text, true) == 0 && string.Compare(r.LastName, txtLastName.Text, true) == 0));
                    bool isEmailExist = _Picker.Any(r => string.Compare(r.Email, txtEmail.Text, true) == 0);
                    bool isBlankEmailIdFound = _Picker.Any(r => r.Email.Length == 0);

                    if (isFLNameExist && txtEmail.Text.Length == 0 && isBlankEmailIdFound)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Picker with the Same name already exist please enter another name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (txtEmail.Text != "" && isEmailExist)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Picker with the Same Email already exist please enter another email", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Deleted == true);
                        ISPicker objPicker = new ISPicker();
                        objPicker.PickerType = Convert.ToInt32(drpPickerType.SelectedValue);
                        //objPicker.PickerOrganisationName = txtPickerOrganisationName.Text;
                        objPicker.SchoolID = Authentication.SchoolID;
                        objPicker.ParentID = Authentication.LogginParent.ID;
                        objPicker.StudentID = Authentication.LogginParent.ID;
                        objPicker.Title = drpPickerType.SelectedValue == "1" ? drpTitle.SelectedValue != "0" ? drpTitle.SelectedValue : "" : "";
                        objPicker.FirstName = drpPickerType.SelectedValue == "1" ? txtFirstName.Text : txtPickerOrganisationName.Text;
                        objPicker.LastName = txtLastName.Text;
                        if (drpPickerType.SelectedValue == "1")
                        {
                            if (fpUpload.HasFile == true)
                            {
                                string filename = System.IO.Path.GetFileName(fpUpload.PostedFile.FileName);
                                fpUpload.SaveAs(Server.MapPath("~/Upload/" + filename));
                                objPicker.Photo = "Upload/" + filename;
                            }
                            else
                            {
                                objPicker.Photo = "Upload/user.jpg";
                                NoImageEmailManage(_Student, txtFirstName.Text + " " + txtLastName.Text);
                            }
                        }
                        else
                        {
                            objPicker.Photo = "Upload/DefaultOrg.png";
                        }
                        objPicker.Email = txtEmail.Text;
                        objPicker.EmergencyContact = !String.IsNullOrEmpty(txtEmail.Text) ? drpContact.SelectedValue == "1" ? true : false : false;
                        objPicker.Phone = txtPhoneNumber.Text;
                        objPicker.OneOffPickerFlag = chbOneOffPicker.Checked == true ? true : false;
                        objPicker.ActiveStatus = "Active";
                        objPicker.Active = true;
                        objPicker.Deleted = true;
                        objPicker.CreatedBy = Authentication.LogginParent.ID;
                        objPicker.CreatedByEmail = Authentication.LoginParentEmail;
                        objPicker.CreatedDateTime = DateTime.Now;
                        objPicker.CreatedByName = Authentication.LoginParentName;
                        DB.ISPickers.Add(objPicker);
                        DB.SaveChanges();

                        if (chbOneOffPicker.Checked == true)
                        {
                            ISPickerAssignment obj = new ISPickerAssignment();
                            obj.PickerId = objPicker.ID;
                            obj.StudentID = Authentication.LogginParent.ID;
                            obj.StudentPickAssignFlag = 1;
                            // obj.StudentPickAssignDate = DateTime.Now;
                            obj.RemoveChildStatus = 0;
                            obj.RemoveChildLastUpdateDate = DateTime.Now;
                            obj.PickCodayExDate = DateTime.Now;
                            obj.StudentAssignBy = Authentication.LoginParentName;
                            //objPicker.id_checked = CheckBox_idchkd.Checked == true ? true : false;
                            DB.ISPickerAssignments.Add(obj);
                            DB.SaveChanges();
                        }
                        SendCreateEmailManage(_Student, objPicker);
                        AlertMessageManagement.ServerMessage(Page, "Picker Created and Assigned Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        if (objPicker.EmergencyContact == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Note that Emergency Picker will be Contacted with Email if School send unpick alert in the event of emergency or when student is not picked on time. You can change to NO if you do not want emergency contact unpick alert Email.');window.location ='EditChildrenAssignment.aspx?ID=" + objPicker.ID.ToString() + "';", true);
                        }
                        else
                        {
                            Response.Redirect("EditChildrenAssignment.aspx?ID=" + objPicker.ID, false);
                        }
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void NoImageEmailManage(ISStudent _Student, string PickerName)
        {
            string PP1body = String.Empty;
            string PP2Body = String.Empty;
            string TeacherBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LoginParentEmail;
            string tblPP1body = string.Empty;
            string tblPP2Body = string.Empty;
            string tblTeacherBody = string.Empty;
            tblPP1body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database records show that a picker, " + PickerName + @" created by " + LoggedINName + @" has no identifiable image. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for this Picker.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Failure to do so will result in  you receiving this notification in the future, and the  child pickup process could also become more difficult.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please use the FAQs, videos and guides on your account or contact the School Administrator if you experience any difficulty in carrying out this task.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                PP1body += reader.ReadToEnd();
            }
            PP1body = PP1body.Replace("{Body}", tblPP1body);
            _EmailManagement.SendEmail(_Student.ParantEmail1, "No Picker Image", PP1body);

            if (!String.IsNullOrEmpty(_Student.ParantName2) && !String.IsNullOrEmpty(_Student.ParantEmail2))
            {
                tblPP2Body = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Our database records show that a picker, " + PickerName + @" created by " + LoggedINName + @" has no identifiable image. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please update the record by uploading the correct/latest image for this Picker.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Failure to do so will result in  you receiving this notification in the future, and the  child pickup process could also become more difficult.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        Please use the FAQs, videos and guides on your account or contact the School Administrator if you experience any difficulty in carrying out this task.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        For other enquiries with your account, please contact the School Administrator.
                                    </td>
                                </tr>
                                </table>";

                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    PP2Body += reader.ReadToEnd();
                }
                PP2Body = PP2Body.Replace("{Body}", tblPP2Body);

                _EmailManagement.SendEmail(_Student.ParantEmail2, "No Picker Image", PP2Body);
            }
        }
        public void SendCreateEmailManage(ISStudent _Student, ISPicker _Picker)
        {
            try
            {
                if (!String.IsNullOrEmpty(_Picker.Email))
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
                                    " + LoggedinParent + @" had added you as a Picker for " + _Student.StudentName + @". <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       The parents/guardian has reserve right to remove this access at anytime without  prior notice. So, it is always a good practice to obtain prior authorisation from the parent  in any citcumstances that the " + _Student.StudentName + @" will be picked
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       There will be situations when the pickup will require authorisation codes generated by parents. In such situation, an email will be sent to the address on your record to notify you of this code
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please contact the parent of " + LoggedinParent + @" if unsure about this setup or the child pickup.
                                   </td>
                                </tr></table>";
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                    {
                        SuperwisorBody += reader.ReadToEnd();
                    }
                    SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                    _EmailManagement.SendEmail(_Picker.Email, "Create Picker Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void Clear()
        {
            drpTitle.SelectedValue = "0";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhoneNumber.Text = "";
            chbOneOffPicker.Checked = false;
            txtPickerOrganisationName.Text = "";
            drpContact.SelectedValue = "0";
        }

        protected void drpPickerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpPickerType.SelectedValue == "1")
            {
                fpUpload.Enabled = true;
                pnlIndivisualPicker.Visible = true;
                pnlOrganizationUser.Visible = false;
            }
            else
            {
                fpUpload.Enabled = false;
                fpUpload.Dispose();
                pnlIndivisualPicker.Visible = false;
                pnlOrganizationUser.Visible = true;
            }
        }

        protected void fpUpload_Load(object sender, EventArgs e)
        {
            if (fpUpload.HasFile)
            {
                fpUpload.SaveAs(MapPath("~/Upload/" + fpUpload.FileName));
                System.Drawing.Image img1 = System.Drawing.Image.FromFile(MapPath("~/Upload/") + fpUpload.FileName);
                UploadPhoto.ImageUrl = "~/Upload/" + fpUpload.FileName;
            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            //if (fpUpload.HasFile)
            //{
            //    fpUpload.SaveAs(MapPath("~/Upload/" + fpUpload.FileName));
            //    System.Drawing.Image img1 = System.Drawing.Image.FromFile(MapPath("~/Upload/") + fpUpload.FileName);
            //    UploadPhoto.ImageUrl = "~/Upload/" + fpUpload.FileName;
            //}
        }
    }
}