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
    public partial class Report : System.Web.UI.Page
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
                ddlReport.Items.Clear();
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ReportID = Convert.ToInt32(ddlReportType.SelectedValue);
            if(ddlReportType.SelectedValue != "0")
            {
                ddlReport.DataSource = DB.ISReports.Where(p => p.Deleted == true && p.Active == true && p.ReportTypeID == ReportID).ToList();
                ddlReport.DataTextField = "Name";
                ddlReport.DataValueField = "URL";
                ddlReport.DataBind();
                ddlReport.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
            }
        }

        //protected void lnkSubmit_Click(object sender, EventArgs e)
        //{
        //    if (ddlReportType.SelectedValue != "0" && ddlReport.SelectedValue != "0")
        //    {
        //        Response.Redirect(ddlReport.SelectedValue);
        //    }
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue != "0" && ddlReport.SelectedValue != "0")
            {
                Response.Redirect(ddlReport.SelectedValue);
            }
        }
    }
}