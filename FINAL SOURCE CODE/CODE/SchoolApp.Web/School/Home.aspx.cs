using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web.School
{
    public partial class Home : System.Web.UI.Page
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
                //SetSchoolData();
            }
        }

        //public void SetSchoolData()
        //{
        //    lblOustanding.Text=DB.ISSupports.Where(p=>p.StatusID==(int?)EnumsManagement.SUPPORTSTATUS.Resolved && p.Active == true && p.Deleted == true).ToList().Count.ToString();
        //    lblUnpaidInvoice.Text=DB.ISSchoolInvoices.Where(p => p.StatusID == 1 && p.Active == true && p.Deleted == true).ToList().Count.ToString();
        //    lblClass.Text = DB.ISClasses.Where(p => p.SchoolID== Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true).ToList().Count.ToString();
        //    lblTeachers.Text = DB.ISTeachers.Where(p => p.SchoolID== Authentication.LogginSchool.ID &&  p.Active == true && p.Deleted == true).ToList().Count.ToString();
        //    lblStudent.Text = DB.ISStudents.Where(p => p.SchoolID== Authentication.LogginSchool.ID && p.Active == true && p.Deleted == true).ToList().Count.ToString();
            
        //}


        public bool? GetSchoolType()
        {
            if (Authentication.LogginSchool != null)
            {
                ISSchool objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.LogginSchool.ID && p.Deleted == true);
                if (objSchool.TypeID != (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
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



    }
}