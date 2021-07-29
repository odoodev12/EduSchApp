using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Authentication.ISUserLogin())
                {
                    if (!Authentication.ISOrgUserLogin())
                    {
                        Response.Redirect(Authentication.AuthorizePage());
                    }
                }
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void BindData()
        {
            lblSchool.Text = DB.ISSchools.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblOrgUsers.Text = DB.ISOrganisationUsers.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblClass.Text = DB.ISClasses.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblTeachers.Text = DB.ISTeachers.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblStudents.Text = DB.ISStudents.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblPickers.Text = DB.ISPickers.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblSupport.Text = DB.ISSupports.Where(p => p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblInvoice.Text = DB.ISSchoolInvoices.Where(p => p.StatusID == 1 && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblAfterSchool.Text = DB.ISSchools.Where(p => p.TypeID == (int?)EnumsManagement.SCHOOLTYPE.AfterSchool && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            lblUnResolvedSupport.Text = DB.ISSupports.Where(p => (p.StatusID == (int?)EnumsManagement.SUPPORTSTATUS.UnResolved || p.StatusID == (int?)EnumsManagement.SUPPORTSTATUS.InProgress) && p.Active == true && p.Deleted == true).ToList().Count.ToString();
        }
    }
}