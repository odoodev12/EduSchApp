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
    public partial class InternalClass : System.Web.UI.Page
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
            }
        }
        public void BindData()
        {
            ClassManagement objClassManagement = new ClassManagement();
            var Obj = objClassManagement.StudentListByInternalClass(Authentication.SchoolID);
            lstClasses.DataSource = Obj.ToList();
            lstClasses.DataBind();
            ISClass objClass = DB.ISClasses.SingleOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == Authentication.SchoolID && p.Active == true && p.Deleted == true);
            if (objClass != null)
            {
                hlinkSchool.NavigateUrl = "ClassDetail.aspx?ID=" + objClass.ID;
                hlinkSchool.Text = objClass.Name;
                hlinkTeacher.NavigateUrl = "ClassDetail.aspx?ID=" + objClass.ID;
                hlinkTeacher.Text = objClass.Name;
                litClassName.Text = objClass.Name + " Students";
                litTotalStudent.Text = objClassManagement.getInternalStudentCount(objClass.SchoolID).ToString();
            }
            if(Obj.Count <= 0)
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
        public bool? ISTeachers()
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
        public bool ISInternal()
        {
            ISClass objClass = DB.ISClasses.OrderByDescending(x=>x.ID).FirstOrDefault(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.SchoolID == Authentication.SchoolID && p.Deleted == true);
            if (objClass.AfterSchoolType == "Internal")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}