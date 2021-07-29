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
    public partial class Supportlog : System.Web.UI.Page
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
                if (Session["FROMDATES"] != null)
                {
                    DateTime myDt = Convert.ToDateTime(Session["FROMDATES"]);
                    txtDateFrom.Text = myDt.ToString("yyyy-MM-dd");
                }
                if (Session["TODATES"] != null)
                {
                    DateTime myDt = Convert.ToDateTime(Session["TODATES"]);
                    txtDateTo.Text = myDt.ToString("yyyy-MM-dd");
                }
                BindDropdown();
                BindList(txtDateFrom.Text, txtDateTo.Text, "0", "", "Date");
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageSupportFlag == true)
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
            drpStatus.DataSource = DB.ISSupportStatus.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpStatus.DataValueField = "ID";
            drpStatus.DataTextField = "Name";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "0" });
        }

        private void BindList(string FromDate, string ToDate, string Status, string OrderBy, string SortBy)
        {
            SupportManagement objSupportManagement = new SupportManagement();
            ListView1.DataSource = objSupportManagement.SupportList(Authentication.SchoolID, FromDate, ToDate, Status, OrderBy, SortBy);
            ListView1.DataBind();
        }
        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)ListView1.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(txtDateFrom.Text, txtDateTo.Text, drpStatus.SelectedValue, rbtnAscending.Checked == true ? "ASC" : "DESC", drpSortBy.SelectedValue);
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (txtDateFrom.Text != "")
            {
                Session["FROMDATES"] = txtDateFrom.Text;
            }
            if (txtDateTo.Text != "")
            {
                Session["TODATES"] = txtDateTo.Text;
            }
            BindList(txtDateFrom.Text, txtDateTo.Text, drpStatus.SelectedValue, rbtnAscending.Checked == true ? "ASC" : "DESC", drpSortBy.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            drpStatus.SelectedValue = "0";
            rbtnDescending.Checked = true;
            drpSortBy.SelectedValue = "Date";
            Session["FROMDATES"] = null;
            Session["TODATES"] = null;
            BindList("", "", "0", "", "Date");
        }
    }
}