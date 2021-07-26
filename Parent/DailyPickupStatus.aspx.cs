using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class DailyPickupStatus : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindList(null, "0", "", "", "0");
                BindDropdown();
            }
        }
        private void BindDropdown()
        {
            int SchoolID = Authentication.SchoolID;
            //int SchoolID = DB.ISTeachers.SingleOrDefault(p => p.ID == ID).SchoolID;  
            var obj = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Active == true && p.Deleted == true);
            if (obj != null)
            {
                List<ISStudent> ObjLists = DB.ISStudents.Where(p => p.SchoolID == SchoolID && (p.ParantEmail1 == obj.ParantEmail1 || p.ParantEmail2 == obj.ParantEmail2) && p.StartDate == null && p.Deleted == true && p.Active == true).ToList();
                drpStudent.DataSource = ObjLists.OrderBy(r => r.StudentName);
                drpStudent.DataTextField = "StudentName";
                drpStudent.DataValueField = "ID";
                drpStudent.DataBind();
                drpStudent.Items.Insert(0, new ListItem { Text = "Select Student", Value = "0" });

                PickerManagement objPickerManagement = new PickerManagement();
                List<MISPickerAssignment> objPickerList = new List<MISPickerAssignment>();
                foreach (var item in ObjLists)
                {
                    List<MISPickerAssignment> objList = objPickerManagement.GetPickerAssignmentToday(item.ID);
                    List<MISPickerAssignment> objList2 = objPickerManagement.GetPickerAssignment(item.ID);

                    foreach (MISPickerAssignment items in objList)
                    {
                        objPickerList.Add(items);
                    }
                    foreach (MISPickerAssignment items in objList2)
                    {
                        objPickerList.Add(items);
                    }
                }
                var list = objPickerList.Select(p => new { PickerName = p.PickerName }).Distinct().ToList().OrderBy(r => r.PickerName).ToList();
                drpPicker.DataSource = list;//DB.ISPickers.Where(p => p.SchoolID == SchoolID && p.Deleted == true && p.Active == true && p.StudentID == Authentication.LogginParent.ID).ToList();
                drpPicker.DataTextField = "PickerName";
                drpPicker.DataValueField = "PickerName";
                drpPicker.DataBind();
                drpPicker.Items.Insert(0, new ListItem { Text = "Select Picker", Value = "" });
            }
            drpStatus.DataSource = DB.ISPickUpStatus.Where(p => p.Active == true).ToList().OrderBy(r => r.Status).ToList();
            drpStatus.DataTextField = "Status";
            drpStatus.DataValueField = "Status";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "" });
        }

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }

        private void BindList(string Date, string StudentID, string PickerID, string PickupStatus, string SortByStudentID)
        {

            int ID = Authentication.LogginParent.ID;
            int StudentIDs = Convert.ToInt32(StudentID);
            PickupManagement objPikupManagement = new PickupManagement();
            List<MViewStudentPickUp> List = objPikupManagement.DailyPickUpStatus(Date, StudentIDs, ID, PickerID, PickupStatus, rbAsc.Checked == true ? "ASC" : "DESC", SortByStudentID);

            DateTime dt = DateTime.Now;
            if (Date != null && Date != "")
            {
                string dates = Date;
                string Format = "";
                if (dates.Contains("/"))
                {
                    string[] arrDate = dates.Split('/');
                    Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                }
                else
                {
                    Format = dates;
                }
                dt = Convert.ToDateTime(Format);
            }

            foreach (var temp in List)
            {
                if (string.IsNullOrEmpty(temp.AfterSchoolName))
                    temp.AfterSchoolName = temp.SchoolName;

                if (OperationManagement.DefaultPickupStatuslist.Contains(temp.PickStatus))
                {
                    temp.Pick_Time = "";
                    temp.PickerName = "";
                }
                //else if (string.Compare(temp.PickStatus, OperationManagement.DefaultPickupStatuslist[0], true) == 0)
                //    temp.PickStatus = "Weekend (School Closed)";


                //if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                //{
                //    temp.PickStatus = "Weekend (School Closed)";
                //    temp.Pick_Time = "";
                //    temp.PickerName = "";
                //}

            }
            ListView1.DataSource = List.ToList().OrderBy(e => e.StudentName).ToList();
            ListView1.DataBind();

        }
        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)ListView1.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList(txtDate.Text, drpStudent.SelectedValue, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (txtDate.Text != "")
            {
                BindList(txtDate.Text, drpStudent.SelectedValue, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            BindList(txtDate.Text, drpStudent.SelectedValue, drpPicker.SelectedValue, drpStatus.SelectedValue, drpSort.SelectedValue);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDate.Text = "";
            drpStudent.SelectedValue = "0";
            drpPicker.SelectedValue = "";
            drpStatus.SelectedValue = "";
            drpSort.SelectedValue = "Date";
            rbDesc.Checked = true;
            BindList(null, "0", "", "", "0");
        }
    }
}