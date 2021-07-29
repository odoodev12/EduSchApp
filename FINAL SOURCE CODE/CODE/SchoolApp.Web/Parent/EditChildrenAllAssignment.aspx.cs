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
    public partial class EditChildrenAllAssignment : System.Web.UI.Page
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

                var findMultipleStudentList = DB.ISStudents.Where(p => p.StudentName.ToLower() == _Student.StudentName.ToLower() && 
                (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()) && p.Deleted == true).ToList();

                bool isEnable = false;
                bool isSelected = false;

                foreach (var tempStud in findMultipleStudentList)
                {
                    if (_Picker.CreatedByEmail.ToLower() == tempStud.ParantEmail1.ToLower() ||
                        (_Picker.CreatedByEmail.ToLower() == tempStud.ParantEmail2.ToLower()))
                    {
                        isEnable = true;
                    }

                    if (objList.Where(p => p.StudentID == tempStud.ID).ToList().Count > 0)                    
                        isSelected = true;
                }

                lis.Enabled = isEnable;
                lis.Selected = isSelected;
                if (!isEnable)
                    lis.Attributes["title"] = $"{lis.Text} child is not linked to this picker.";

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
            //cbl.DataSource = DB.ISStudents.Where(p => p.Active == true && p.StartDate == null && p.EndDate == null && p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).OrderBy(r=>r.StudentName).ToList();


            //////
            //var iSStudent = DB.ISStudents.Where(r => (r.ParantEmail1 == Authentication.LoginParentEmail || r.ParantEmail2 == Authentication.LoginParentEmail) && r.Active == true && r.Deleted == true).ToList();

            //var allEmailId = new List<string>();
            //allEmailId.AddRange(iSStudent.Where(r => !string.IsNullOrEmpty(r.ParantEmail1)).Select(r => r.ParantEmail1).ToList());
            //allEmailId.AddRange(iSStudent.Where(r => !string.IsNullOrEmpty(r.ParantEmail2)).Select(r => r.ParantEmail2).ToList());
            //allEmailId = allEmailId.Distinct().ToList();
            ///////

            //var result = DB.ISStudents.Where(p => p.Active == true && p.StartDate == null && p.EndDate == null && p.Deleted == true && 
            //((allEmailId.Contains(p.ParantEmail1.ToLower()) || allEmailId.Contains(p.ParantEmail2.ToLower())))).ToList();

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
            foreach (ListItem li in cbl.Items)
            {
                int SID = Convert.ToInt32(li.Value);
                ISStudent _Students = DB.ISStudents.SingleOrDefault(p => p.ID == SID && p.Deleted == true);
                var findMultipleStudentList = DB.ISStudents.Where(p => p.StudentName == _Students.StudentName && 
                (p.ParantEmail1 == Authentication.LoginParentEmail || p.ParantEmail2 == Authentication.LoginParentEmail) && p.Deleted == true).ToList();

                if (li.Selected == true)
                {
                    PickerManagement pObj = new PickerManagement();
                    var currentPicker = pObj.GetPicker(PID);

                    foreach (var item in findMultipleStudentList)
                    {
                        
                        var assignedPickerList = pObj.GetPickerAssignment(item.ID);                        

                        if (assignedPickerList != null && currentPicker != null)
                        {
                            bool isAny = assignedPickerList.Any(r => r.PickerName == currentPicker.PickerName && r.PickerEmail == currentPicker.Email);
                            if (isAny)
                                continue;
                        }

                        List<ISPickerAssignment> objPicker = DB.ISPickerAssignments.Where(p => p.PickerId == PID && p.StudentID == item.ID && p.RemoveChildStatus == 0).ToList();
                        if (objPicker.Count > 0)
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
            Response.Redirect("StudentAllPicker.aspx", false);
        }
        protected void Clear()
        {

        }
    }
}