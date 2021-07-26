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
    public partial class EditPicker : System.Web.UI.Page
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
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);
                    BindData(ID);
                }
            }
        }

        private void BindData(int ID)
        {
            ISPicker obj = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
            if (obj != null)
            {
                if (obj.PickerType == (int)EnumsManagement.PICKERTYPE.Individual)
                {
                    pnlIndivisualPicker.Visible = true;
                    pnlOrganizationUser.Visible = false;
                    fpUpload.Enabled = true;
                    fpUpload.CssClass = "btn btn-primary";
                    drpPickerType.SelectedValue = obj.PickerType.ToString();
                    ddlPickerTitle.SelectedValue = obj.Title;
                    txtFName.Text = obj.FirstName;
                    txtLName.Text = obj.LastName;
                }
                else
                {
                    pnlIndivisualPicker.Visible = false;
                    pnlOrganizationUser.Visible = true;
                    fpUpload.Enabled = false;
                    fpUpload.CssClass = "btn btn-primary";
                    drpPickerType.SelectedValue = obj.PickerType.ToString();
                    txtPickerOrganisationName.Text = obj.FirstName;
                }
                UploadPhoto.ImageUrl = string.Format("{0}{1}", "../", obj.Photo);
                AID.HRef = string.Format("{0}{1}", "../", obj.Photo);
                AID.Attributes["data-title"] = obj.FirstName + " " + obj.LastName;
                drpContact.SelectedValue = obj.EmergencyContact != null ? obj.EmergencyContact == true ? "1" : "0" : "0";
                txtEmai.Text = obj.Email;
                txtPhoneNumber.Text = obj.Phone;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                if (String.IsNullOrEmpty(txtEmai.Text) && drpContact.SelectedValue == "1")
                {
                    AlertMessageManagement.ServerMessage(Page, "Email must be entered to contact this account in the event of pickup emergency.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    List<ISPicker> _Pickers = DB.ISPickers.Where(p => p.ID != ID && p.StudentID == Authentication.LogginParent.ID && p.Deleted == true).ToList();

                    bool isFLNameExist = _Pickers.Any(r => (string.Compare(r.FirstName, txtFName.Text, true) == 0 && string.Compare(r.LastName, txtLName.Text, true) == 0));
                    bool isEmailExist = _Pickers.Any(r => string.Compare(r.Email, txtEmai.Text, true) == 0);
                    bool isBlankEmailIdFound = _Pickers.Any(r => r.Email.Length == 0);

                    if (isFLNameExist && txtEmai.Text.Length == 0 && isBlankEmailIdFound)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Picker with the Same name already exist please enter another name", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else if (txtEmai.Text != "" && isEmailExist)
                    {
                        AlertMessageManagement.ServerMessage(Page, "Picker with the Same Email already exist please enter another email", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                    else
                    {
                        ISPicker obj = DB.ISPickers.SingleOrDefault(p => p.ID == ID);
                        obj.PickerType = Convert.ToInt32(drpPickerType.SelectedValue);
                        //obj.PickerOrganisationName = txtPickerOrganisationName.Text;
                        obj.Title = drpPickerType.SelectedValue == "1" ? ddlPickerTitle.SelectedValue : "";
                        obj.FirstName = drpPickerType.SelectedValue == "1" ? txtFName.Text : txtPickerOrganisationName.Text;
                        obj.LastName = txtLName.Text;
                        if (drpPickerType.SelectedValue == "1")
                        {
                            if (fpUpload.HasFile == true)
                            {
                                string filename = System.IO.Path.GetFileName(fpUpload.PostedFile.FileName);
                                fpUpload.SaveAs(Server.MapPath("../Upload/" + filename));
                                obj.Photo = "../Upload/" + filename;
                            }
                        }
                        obj.Email = txtEmai.Text;
                        obj.EmergencyContact = !String.IsNullOrEmpty(txtEmai.Text) ? drpContact.SelectedValue == "1" ? true : false : false;
                        obj.Phone = txtPhoneNumber.Text;
                        obj.ModifyBy = Authentication.LogginParent.ID;
                        obj.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();
                        ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == obj.StudentID && p.Deleted == true);
                        SendEditEmailManage(_Student, obj);
                        Clear();
                        BindData(ID);
                        //AlertMessageManagement.ServerMessage(Page, "Picker Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        if (obj.EmergencyContact == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Note that Emergency Picker will be Contacted with Email if School send unpick alert in the event of emergency or when student is not picked on time. You can change to NO if you do not want emergency contact unpick alert Email.');window.location ='StudentAllPicker.aspx';", true);
                        }
                        else
                        {
                            Response.Redirect("StudentAllPicker.aspx", false);
                        }
                        //Response.Redirect("StudentAllPicker.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        public void SendEditEmailManage(ISStudent _Student, ISPicker _Picker)
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", has been edited by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Updated Picker Name : " + _Picker.FirstName + " " + _Picker.LastName + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Updated By : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date & Time Updated : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
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
                                    Picker, " + _Picker.FirstName + " " + _Picker.LastName + @", has been edited by " + LoggedinParent + @" <br/>
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Updated Picker Name : " + _Picker.FirstName + " " + _Picker.LastName + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Updated By : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date & Time Updated : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
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

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Edit Picker Notification", AdminBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Edit Picker Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void Clear()
        {
            ddlPickerTitle.SelectedValue = "0";
            txtEmai.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtPhoneNumber.Text = "";
            txtPickerOrganisationName.Text = "";
            drpContact.SelectedValue = "0";
            //chbOneOffPicker.Checked = false;
        }

        protected void drpPickerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpPickerType.SelectedValue == "1")
            {
                pnlIndivisualPicker.Visible = true;
                pnlOrganizationUser.Visible = false;
                fpUpload.Enabled = true;
                fpUpload.CssClass = "btn btn-primary";
            }
            else
            {
                pnlIndivisualPicker.Visible = false;
                pnlOrganizationUser.Visible = true;
                fpUpload.Enabled = false;
                fpUpload.CssClass = "btn btn-primary";
                fpUpload.Dispose();
            }
        }
    }

}