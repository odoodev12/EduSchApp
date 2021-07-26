using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class ClubClass : System.Web.UI.Page
    {   
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindData();
                BindDropdown();
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageStudentFlag == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void BindDropdown()
        {
            //ClassManagement objClassManagement = new ClassManagement();
            //ddlClassTo.DataSource = objClassManagement.ClassList(Authentication.SchoolID, "", 0);
            //ddlClassTo.DataTextField = "Name";
            //ddlClassTo.DataValueField = "ID";
            //ddlClassTo.DataBind();
            //ddlClassTo.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });

            //ddlClass.DataSource = objClassManagement.ClassList(Authentication.LogginSchool.ID, "",0);
            //ddlClass.DataTextField = "Name";
            //ddlClass.DataValueField = "ID";
            //ddlClass.DataBind();
            //ddlClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
        }
        public void BindData()
        {
            ClassManagement objClassManagement = new ClassManagement();
            var Obj = objClassManagement.StudentListByOfficeClass(Authentication.SchoolID);
            lstClasses.DataSource = Obj.ToList();
            lstClasses.DataBind();
            ISClass objClass = DB.ISClasses.FirstOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.Club && p.SchoolID == Authentication.SchoolID);
            if (objClass != null)
            {
                litClassName.Text = objClass.Name;
                litTotalStudent.Text = objClassManagement.getClubStudentCount(objClass.SchoolID).ToString();
            }
            if (Obj.Count <= 0)
            {
                lbl.Text = "There are No Student in " + objClass.Name;
            }
        }
        protected void lstClasses_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstClasses.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindData();
        }
    }
}