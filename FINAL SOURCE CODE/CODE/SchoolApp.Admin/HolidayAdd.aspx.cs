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
    public partial class HolidayAdd : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    //Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    //ltrtitle.Text = "School APP : OrganisationAdd";
                    //BindDropdown();
                    if (Request.QueryString["ID"] != null)
                    {
                        OperationManagement.ID = Convert.ToInt32(Request.QueryString["ID"]);
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.Edit;
                        ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == OperationManagement.ID);
                        if (obj != null)
                        {
                            txtHolidayName.Text = obj.Name;
                            if (obj.DateFrom != null)
                            {
                                DateTime dt = obj.DateFrom.Value.Date;
                                string dates = dt.ToString("dd/MM/yyyy");
                                if (dates.Contains("-"))
                                {
                                    string[] arrDate = dates.Split('-');
                                    txtFromDate.Text = arrDate[0].ToString() + "/" + arrDate[1].ToString() + "/" + arrDate[2].ToString();
                                }
                                else
                                {
                                    txtFromDate.Text = dates;
                                }
                            }
                            if (obj.DateTo != null)
                            {
                                DateTime dt = obj.DateTo.Value.Date;
                                string dates = dt.ToString("dd/MM/yyyy");
                                if (dates.Contains("-"))
                                {
                                    string[] arrDate = dates.Split('-');
                                    txtToDate.Text = arrDate[0].ToString() + "/" + arrDate[1].ToString() + "/" + arrDate[2].ToString();
                                }
                                else
                                {
                                    txtToDate.Text = dates;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {     if (Request.QueryString["ID"] != null)
                  {
                    ISHoliday obj = DB.ISHolidays.SingleOrDefault(p => p.ID == OperationManagement.ID && p.Deleted == true);
                    if (obj != null)
                    {
                        SetField(ref obj);
                        obj.ModifyBy = Authentication.LoggedInUser.ID;
                        obj.ModifyDateTime = DateTime.Now;
                        DB.SaveChanges();
                        Clear();
                        AlertMessageManagement.ServerMessage(Page, "Holiday Edited Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        OperationManagement.ID = 0;
                        OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                        Response.Redirect("HolidayList.aspx", false);

                    }                   
                }
                else
                {
                    ISHoliday obj = new ISHoliday();
                    SetField(ref obj);
                    obj.CreatedBy = Authentication.LoggedInUser.ID;
                    obj.CreatedDateTime = DateTime.Now;                    
                    DB.ISHolidays.Add(obj);
                    DB.SaveChanges();
                    Clear();
                    AlertMessageManagement.ServerMessage(Page, "Holiday Added Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    Response.Redirect("HolidayList.aspx", false);
                }
            }                              

            
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void SetField(ref ISHoliday obj)
        {
            try
            {
                obj.Name = txtHolidayName.Text;
                // obj.HolidayDate =Convert.ToDateTime(txtHolidayDate.Text);
                string dates1 = txtFromDate.Text;
                DateTime dt2 = DateTime.Now;
                if (dates1.Contains("/"))
                {
                    string[] arrDate = dates1.Split('/');
                    dt2 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                }
                else
                {
                    dt2 = Convert.ToDateTime(dates1);
                }
                
                obj.DateFrom = dt2;

                string dates2 = txtToDate.Text;
                DateTime dt3 = DateTime.Now;
                if (dates2.Contains("/"))
                {
                    string[] arrDate = dates2.Split('/');
                    dt3 = new DateTime(Convert.ToInt32(arrDate[2]), Convert.ToInt32(arrDate[1]), Convert.ToInt32(arrDate[0]));
                }
                else
                {
                    dt3 = Convert.ToDateTime(dates2);
                }

                obj.DateTo = dt3;
                obj.Active = true;
                obj.Deleted = true;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void Clear()
        {
            try
            {
                txtHolidayName.Text = "";
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                OperationManagement.ID = 0;
                OperationManagement.Opration = (int)OperationManagement.OPERRATION.None;
                Response.Redirect("HolidayList.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }
    }
}