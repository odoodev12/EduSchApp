using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Admin
{
    public partial class HolidayList : System.Web.UI.Page
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
                    else
                    {
                        if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 2 || Authentication.LoggedInOrgUser.RoleID == 3)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
                if (!Page.IsPostBack)
                {                   
                    BindData("");
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void BindData(string Name)
        {
            try
            {
                List<MISHoliday> objList = new List<MISHoliday>();
                objList = (from Obj in DB.ISHolidays.Where(p => p.Deleted == true).ToList()
                           select new MISHoliday
                           {
                               ID = Obj.ID,
                               Name = Obj.Name,
                               FromDate = Obj.DateFrom.Value.ToString("dd/MM/yyyy"),
                               ToDate = Obj.DateTo.Value.ToString("dd/MM/yyyy"),
                           }).ToList();
                if (Name != "")
                {
                    objList = objList.Where(p => p.Name == Name).ToList();
                }
                GvHolidayList.DataSource = objList.ToList();
                GvHolidayList.DataBind();
                Session["Objectlist"] = objList.ToList();
                //GvHolidayList.DataSource = DB.ISHolidays.Where(p=>p.Active==true && p.Deleted==true).ToList();
                //GvHolidayList.DataBind();
            }
            catch(Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvHolidayList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "btnEdit")
                {
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("HolidayAdd.aspx?ID=" + ID);
                }
                if (e.CommandName == "btnDelete")
                {
                    OperationManagement.ID = Convert.ToInt32(e.CommandArgument);
                    ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                    if (obj != null)
                    {
                        obj.Active = false;
                        obj.Deleted = false;
                        obj.DeletedBy = Authentication.LoggedInUser.ID;
                        obj.DeletedDateTime = DateTime.Now;
                        DB.SaveChanges();
                        BindData("");
                        AlertMessageManagement.ServerMessage(Page, "Holiday Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void GvHolidayList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvHolidayList.PageIndex = e.NewPageIndex;
                if (txtHolidayName.Text != "")
                {
                    BindData(txtHolidayName.Text);
                }
                else
                {
                    BindData("");
                }

            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("HolidayAdd.aspx");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                BindData(txtHolidayName.Text);
                PanelManagement.OpenPanel(ref collapseOne1);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                PanelManagement.ClosePanel(ref collapseOne1);
                BindData("");
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }
        private void Clear()
        {
            try
            {
                txtHolidayName.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

    }
}