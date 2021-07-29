using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class Settings : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Authentication.ISParentLogin())
                {
                    Response.Redirect(Authentication.ParentAuthorizePage());
                }
                if (!IsPostBack)
                {
                    BindData();

                    //var isschoolList = BindDropDownSchoolNameList();
                    //drpSchoolName.DataSource = isschoolList;
                    //drpSchoolName.DataTextField = "Name";
                    //drpSchoolName.DataValueField = "ID";
                    //drpSchoolName.DataBind();
                    //drpSchoolName.Items.Insert(0, new ListItem { Text = "ALL", Value = "0" });
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        private List<ISSchool> BindDropDownSchoolNameList()
        {
            string loginParentEmail = Authentication.LoginParentEmail.ToLower();

            var schoolIdList = DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == loginParentEmail || p.ParantEmail2.ToLower() == loginParentEmail) && p.Active == true && p.Deleted == true).Select(r=>r.SchoolID).Distinct().ToList();

            return DB.ISSchools.Where(r => schoolIdList.Contains(r.ID) && r.Active == true).ToList();
        }

        private void BindData()
        {
            try
            {
                ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Active == true && p.Deleted == true);
                if (objStudent != null)
                {
                    if (objStudent.EmailAfterConfirmPickUp == true)
                    {
                        email_confirmation_radio_1.Checked = true;
                        email_confirmation_radio_2.Checked = false;
                    }
                    else if (objStudent.EmailAfterConfirmPickUp == false)
                    {
                        email_confirmation_radio_1.Checked = false;
                        email_confirmation_radio_2.Checked = true;
                    }
                    if (objStudent.EmailAfterConfirmAttendence == true)
                    {
                        email_Attendence_marked_1.Checked = true;
                        email_Attendence_marked_2.Checked = false;
                    }
                    else if (objStudent.EmailAfterConfirmAttendence == false)
                    {
                        email_Attendence_marked_1.Checked = false;
                        email_Attendence_marked_2.Checked = true;
                    }
                    litStudent.Text = Authentication.LoginParentName;
                    img1.Src = String.Format("{0}{1}", "../", objStudent.Photo);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(Authentication.LogginParent.ID);
                string loginParentEmail = Authentication.LoginParentEmail.ToLower();

                var _StudentList = DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == loginParentEmail || p.ParantEmail2.ToLower() == loginParentEmail) && p.Active == true && p.Deleted == true).ToList();
                foreach(var _Student in _StudentList)
                {
                    _Student.EmailAfterConfirmPickUp = email_confirmation_radio_1.Checked == true ? true : false;
                    _Student.EmailAfterConfirmAttendence = email_Attendence_marked_1.Checked == true ? true : false;
                    DB.SaveChanges();
                }

                AlertMessageManagement.ServerMessage(Page, "Settings Saved Successfully.", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
    }
}