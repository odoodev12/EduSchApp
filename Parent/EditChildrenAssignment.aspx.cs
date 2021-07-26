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
    public partial class EditChildrenAssignment : System.Web.UI.Page
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
                    int PID = Convert.ToInt32(Request.QueryString["ID"]);
                    BindCheckbox();
                    BindData(PID);
                }
            }

        }

        private void BindData(int PID)
        {
            List<MISPickerAssignment> objList = new List<MISPickerAssignment>();
            objList = (from item in DB.ISPickerAssignments.Where(p => p.RemoveChildStatus == 0 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))).ToList()
                       join item2 in DB.ISPickers.Where(p => p.ID == PID && p.Active == true && p.Deleted == true /*&& p.SchoolID == Authentication.SchoolID*/).ToList() on item.PickerId equals item2.ID
                       select new MISPickerAssignment
                       {
                           ID = item.ID,
                           StudentID = item.StudentID,
                           PickerId = item2.ID,
                           PickerName = item2.FirstName + " " + item2.LastName,
                           PickerEmail = item2.Email,
                           PickerPhone = item2.Phone,
                           PickerType = item2.PickerType,
                           AssignBy = item2.ISStudent.ParantName1,
                           Status = item2.Active == true ? "Active" : "Inactive",
                           Photo = item2.Photo,
                           StrDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                           PickerCode = item.PickCodeExDate != null ? item.PickCodeExDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ? item.PickerCode != null ? item.PickerCode : "" : "" : "",
                           StrStudentPickAssignDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                           CurrentDate = DateTime.Now.ToString("dd/MM/yyyy"),
                           Count = false
                       }).ToList();
            //List<ISPickerAssignment> obj = DB.ISPickerAssignments.Where(p => p.PickerId == PID && p.RemoveChildStatus == 0).ToList();
            ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == PID && p.Deleted == true);


            foreach (ListItem lis in cbl.Items)
            {
                int SID = Convert.ToInt32(lis.Value);
                ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == SID && p.Deleted == true);

                if (_Picker.CreatedByEmail.ToLower() != _Student.ParantEmail1.ToLower() &&
                    (string.IsNullOrEmpty(_Student.ParantEmail2) || _Picker.CreatedByEmail.ToLower() != _Student.ParantEmail2.ToLower()))
                {
                    lis.Enabled = false;
                    lis.Attributes["title"] = $"{lis.Text} child is not linked to this picker.";
                }

                if (objList.Where(p => p.StudentID == SID).ToList().Count > 0)
                {
                    lis.Selected = true;
                }
                else
                {
                    lis.Selected = false;
                }
            }

            foreach (ListItem li in cbl.Items)
            {
                if (li.Selected)
                {
                    chbSelectAll.Checked = true;
                }
                else
                {
                    chbSelectAll.Checked = false;
                    break;
                }
            }
        }

        protected void BindCheckbox()
        {
            //ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.Active == true && p.Deleted == true && p.ID == Authentication.LogginParent.ID);

            var result = DB.ISStudents.Where(p => p.Active == true && p.StartDate == null && p.EndDate == null && p.Deleted == true && (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower())).ToList();

            var uniqueStudent = new List<ISStudent>();
            foreach (var item in result)
            {
                if (!uniqueStudent.Any(r => r.StudentName.ToLower() == item.StudentName.ToLower()))
                {
                    uniqueStudent.Add(item);
                }
            }

            cbl.DataSource = uniqueStudent;
            cbl.DataTextField = "StudentName";
            cbl.DataValueField = "ID";
            cbl.DataBind();
        }
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in cbl.Items)
            {
                if (li.Selected)
                {
                    chbSelectAll.Checked = true;
                }
                else
                {
                    chbSelectAll.Checked = false;
                }
            }
        }

        protected void chbselectall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chbSelectAll.Checked == true)
                {
                    foreach (ListItem li in cbl.Items)
                    {
                        if (li.Enabled)
                            li.Selected = chbSelectAll.Checked;
                        else
                            li.Attributes["title"] = $"{li.Text} child is not linked to this picker.";
                    }
                }
                //else
                //{
                //    cbl.ClearSelection();
                //}
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int PID = Convert.ToInt32(Request.QueryString["ID"]);

            ISPickerAssignment obj = new ISPickerAssignment();
            string ChildNames = string.Empty;
            foreach (ListItem li in cbl.Items)
            {
                int SID = Convert.ToInt32(li.Value);
                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == SID && p.Deleted == true);

                var findMultipleStudentList = DB.ISStudents.Where(p => p.StudentName == _Students.StudentName && (p.ParantEmail1 == Authentication.LoginParentEmail || p.ParantEmail2 == Authentication.LoginParentEmail) && p.Deleted == true).ToList();

                if (li.Selected == true)
                {
                    foreach (var item in findMultipleStudentList)
                    {
                        List<ISPickerAssignment> objPicker = DB.ISPickerAssignments.Where(p => p.PickerId == PID && p.StudentID == item.ID && p.RemoveChildStatus == 0).ToList();
                        ///
                        ISPicker picker = DB.ISPickers.FirstOrDefault(r => r.ID == PID);

                        var findSameNamePicker = DB.ISPickers.Where(r => (r.FirstName + ' ' + r.LastName) == (picker.FirstName + ' ' + picker.LastName)).ToList();
                        var findSamePickerAssignment = new List<ISPickerAssignment>();
                        if (findSameNamePicker != null)
                        {
                            List<int> pickerIDs = findSameNamePicker.Select(r => r.ID).ToList();
                            findSamePickerAssignment = DB.ISPickerAssignments.Where(p => pickerIDs.Contains(p.PickerId.Value) && p.StudentID == item.ID).ToList();
                        }
                        ///
                        if (objPicker.Count > 0 && findSamePickerAssignment.Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            obj.PickerId = PID;
                            obj.StudentID = item.ID;
                            obj.RemoveChildStatus = 0;
                            obj.StudentAssignBy = Authentication.LoginParentName;
                            DB.ISPickerAssignments.Add(obj);
                            DB.SaveChanges();
                            Clear();
                        }

                    }
                    ChildNames += _Students.StudentName + ", ";
                    AlertMessageManagement.ServerMessage(Page, "Children Assignment Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
                else
                {
                    foreach (var item in findMultipleStudentList)
                    {
                        ISPickerAssignment objPicker = DB.ISPickerAssignments.FirstOrDefault(p => p.PickerId == PID && p.StudentID == item.ID && p.RemoveChildStatus == 0);
                        if (objPicker != null)
                        {
                            obj.StudentAssignBy = "";
                            objPicker.RemoveChildStatus = 1;
                            objPicker.RemoveChildLastUpdateDate = DateTime.Now;
                            objPicker.RemoveChildLastupdateParent = Authentication.LogginParent.ID.ToString();
                            DB.SaveChanges();
                        }
                    }
                }

            }
            ChildNames = ChildNames.Length > 2 ? ChildNames.Substring(ChildNames.Length - 2) : "";
            ISStudent _Student = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Deleted == true);
            ISPicker _Picker = DB.ISPickers.SingleOrDefault(p => p.ID == PID && p.Deleted == true);
            SendCreateEmailManage(_Student, _Picker, ChildNames);
            Response.Redirect("ManagePickers.aspx", false);
        }
        protected void Clear()
        {

        }
        public void SendCreateEmailManage(ISStudent _Student, ISPicker _Picker, string ChildNames)
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
                                    A new Picker has been created for your Child/Children. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       New Picker Created : " + _Picker.FirstName + " " + _Picker.LastName + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Created By : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Assigned To Children : " + ChildNames + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Picker picture should be added as soon as possible to make the pickup process easy for this Picker as codes will be required where picture does not exist.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please be aware that you have the sole responsibility as parents for assigning the Picker to collect your child/children. As an organisation, our responsibility does not go beyond verifying the Picker that you have assigned to the child/children.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       It is highly recommended to constantly review this right and remove/amend it where it is no longer be required for any child.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       We advice that you make use of code generation or other feature if you require additional security.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date & Time Created : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
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
                                    A new Picker has been created for your Child/Children. <br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       New Picker Created : " + _Picker.FirstName + " " + _Picker.LastName + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Created By : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Assigned To Children : " + ChildNames + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Picker picture should be added as soon as possible to make the pickup process easy for this Picker as codes will be required where picture does not exist.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Please be aware that you have the sole responsibility as parents for assigning the Picker to collect your child/children. As an organisation, our responsibility does not go beyond verifying the Picker that you have assigned to the child/children.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       It is highly recommended to constantly review this right and remove/amend it where it is no longer be required for any child.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       We advice that you make use of code generation or other feature if you require additional security.
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date & Time Created : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + @"
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

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Create Picker Notification", AdminBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Create Picker Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }
}