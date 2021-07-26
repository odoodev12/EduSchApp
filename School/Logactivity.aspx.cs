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
    public partial class Logactivity : System.Web.UI.Page
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
                BindDropdown();
                BindData();
            }
        }

        private void BindDropdown()
        {
            var objList = from item in DB.ISUserActivities.Where(p => p.Deleted == true && p.SchoolID == Authentication.SchoolID).ToList()
                          group item by new { item.CreatedBy, item.UserName } into g
                          select new { CreatedBy = g.Key.CreatedBy, UserName = g.Key.UserName };
            ddlUpdatedBy.DataSource = objList.OrderBy(r=>r.UserName);
            ddlUpdatedBy.DataValueField = "CreatedBy";
            ddlUpdatedBy.DataTextField = "UserName";
            ddlUpdatedBy.DataBind();
            ddlUpdatedBy.Items.Insert(0, new ListItem { Text = "Select Updated By", Value = "0" });
        }

        private void BindData()
        {
            LogManagement objLogManagement = new LogManagement();
            lstLog.DataSource = objLogManagement.UserActivity(Authentication.SchoolID, "", "","0","0","", "Date").OrderByDescending(p => p.CreatedDateTime).ToList();
            lstLog.DataBind();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            LogManagement objLogManagement = new LogManagement();
            lstLog.DataSource = objLogManagement.UserActivity(Authentication.SchoolID, txtFromDate.Text, txtToDate.Text, drpLogName.SelectedValue, ddlUpdatedBy.SelectedValue, rbtnAsending.Checked == true ? "ASC" : "DESC", ddlSelect.SelectedValue);
            lstLog.DataBind();
        }
        protected void lstLog_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstLog.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            LogManagement objLogManagement = new LogManagement();
            lstLog.DataSource = objLogManagement.UserActivity(Authentication.SchoolID, txtFromDate.Text, txtToDate.Text, drpLogName.SelectedValue, ddlUpdatedBy.SelectedValue, rbtnAsending.Checked == true ? "ASC" : "DESC", ddlSelect.SelectedValue);
            lstLog.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            //txtLogName.Text = "";
            drpLogName.SelectedValue = "0";
            //txtDocCategory.Text = "";
            rbtnDescending.Checked = true;
            ddlSelect.SelectedValue = "Date";
            ddlUpdatedBy.SelectedValue = "0";
            //txtUpdatedBy.Text = "";
            BindData();
        }

        protected void hlink_Click(object sender, EventArgs e)
        {
            //List<ISUserActivity> ObjList = DB.ISUserActivities.Where(p => p.SchoolID == Authentication.SchoolID).ToList();
            //DB.ISUserActivities.RemoveRange(ObjList);
            //DB.SaveChanges();
            //BindData();
            Response.Redirect("SupportlogCreate.aspx");
        }
        
    }
}