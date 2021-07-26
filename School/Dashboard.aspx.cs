using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());

            }
            if (!IsPostBack)
            {
                SetSchoolData();
            }
        }
        public void SetSchoolData()
        {
            lblOustanding.Text = DB.ISSupports.Where(p => p.StatusID != (int?)EnumsManagement.SUPPORTSTATUS.Resolved && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblUnpaidInvoice.Text = DB.ISSchoolInvoices.Where(p => p.StatusID == 1 && p.SchoolID == Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblClass.Text = DB.ISClasses.Where(p => p.SchoolID == Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblTeachers.Text = DB.ISTeachers.Where(p => p.SchoolID == Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblStudent.Text = DB.ISStudents.Where(p => p.SchoolID == Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true).ToList().Count.ToString();

        }

        
    }
}