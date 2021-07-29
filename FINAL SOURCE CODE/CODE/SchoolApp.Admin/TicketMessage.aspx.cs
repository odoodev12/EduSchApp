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
    public partial class TicketMessage : System.Web.UI.Page
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
                        if (Authentication.LoggedInOrgUser.RoleID == 1 || Authentication.LoggedInOrgUser.RoleID == 2)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry ! You are not Able to access this Page');window.location ='Login.aspx';", true);
                        }
                    }
                }
                if (!IsPostBack)
                {
                    Literal ltrtitle = this.Master.FindControl("littitle") as Literal;
                    ltrtitle.Text = "School APP : Ticket Message";
                    if (Request.QueryString["ID"] != null)
                    {
                        int ID = Convert.ToInt32(Request.QueryString["ID"]);
                        BindDropdown();
                        BindData(ID);
                    }
                    if (Authentication.ISOrgUserLogin())
                    {
                        PanelAdmin.Visible = false;
                        Requiredfieldvalidator3.Enabled = false;
                        Requiredfieldvalidator1.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }

        }

        private void BindData(int ID)
        {
            SupportManagement objSupportManagement = new SupportManagement();
            MISSupport objMSupport = objSupportManagement.GetSupport(ID);
            //List<MISSupport> obj = (from item in DB.ISSupports.Where(p => p.ID == ID).ToList()
            //                        select new MISSupport
            //                        {
            //                            ID = item.ID,
            //                            TicketNo = item.TicketNo,
            //                            Created = DB.ISSchools.SingleOrDefault(p => p.ID == item.CreatedBy).Name,
            //                            Request = item.Request,
            //                            SDate = item.CreatedDateTime.Value.ToString("dd/MM/yyyy hh:mm tt"),
            //                            Status = item.ISSupportStatu.Name,
            //                        }).ToList();
            if (objMSupport != null)
            {
                litCreatedBy.Text = objMSupport.CreatedByName;
                litRequest.Text = "<div class='chat-discussion'>";
                foreach (ISTicketMessage item in DB.ISTicketMessages.Where(p => p.SupportID == objMSupport.ID).OrderByDescending(p => p.CreatedDatetime))
                {
                    ISSchool objSchool = new ISSchool();
                    ISTeacher ObjTeacher = new ISTeacher();
                    if (item.UserTypeID == 1)
                    {
                        objSchool = DB.ISSchools.SingleOrDefault(p => p.ID == item.SenderID);
                    }
                    if(item.UserTypeID == 2)
                    {
                        ObjTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == item.SenderID);
                    }
                    if (objSchool != null || ObjTeacher != null)
                    {
                        litRequest.Text += "<div class='chat-message left'>" +
                            "<img class='message-avatar' src='Upload/user.jpg' alt='' >" +
                            "<div class='message'>" +
                                "<a class='message-author' href='#'>" + (item.UserTypeID == 1 ? objSchool.AdminFirstName + " " + objSchool.AdminLastName + "(" + objSchool.Name + ")" : item.UserTypeID == 2 ? ObjTeacher.Name + "(" + ObjTeacher.ISSchool.Name + ")" : "Support") + "</a>" +
                                "<span class='message-date'>" + (item.CreatedDatetime != null ? item.CreatedDatetime.Value.ToString("dd/MM/yyyy hh:mm tt") : "") + "</span>" +
                                    "<span class='message-content'>" + item.Message + "</span>" +
                                "<a href='" + (item.SelectFile != null ? item.SelectFile : "#") + "' download=''>Attachment</a>" +
                                "</div>" +
                            "</div>";
                    }
                    else
                    {
                        litRequest.Text += "<div class='chat-message right'>" +
                        "<img class='message-avatar' src='Upload/logo.png' alt='' >" +
                        "<div class='message'>" +
                            "<a class='message-author' href='#'>" + "Support" + "</a>" +
                            "<span class='message-date'>" + (item.CreatedDatetime != null ? item.CreatedDatetime.Value.ToString("dd/MM/yyyy hh:mm tt") : "") + "</span>" +
                                "<span class='message-content'>" + item.Message + "</span>" +
                                 "<a href='" + (item.SelectFile != null ? item.SelectFile : "#") + "' download=''>Attachment</a>" +
                            "</div>" +
                        "</div>";
                    }
                }
                litRequest.Text += "</div>";

                litTicketNo.Text = objMSupport.TicketNo;
                litCreated.Text = objMSupport.CreatedByName;
                litDate.Text = objMSupport.SDate;
                litAllRequests.Text = objMSupport.Request;
                litStatus.Text = objMSupport.Status;
                litPriority.Text = objMSupport.Priority == 1 ? "Critical" : objMSupport.Priority == 2 ? "High" : objMSupport.Priority == 3 ? "Medium" : objMSupport.Priority == 4 ? "Low" : "";
                drpStatus.SelectedValue = objMSupport.StatusID != null ? objMSupport.StatusID.ToString() : "0";
                drpPriority.SelectedValue = objMSupport.Priority != null ? objMSupport.Priority.ToString() : "0";
                drpSupportOfficer.SelectedValue = objMSupport.SupportOfficerID != null ? objMSupport.SupportOfficerID.ToString() : "0";

                //lstMessage.DataSource = objMSupport;
                //lstMessage.DataBind();
                //lstTicket.DataSource = objMSupport;
                //lstTicket.DataBind();
            }
        }

        private void BindDropdown()
        {
            drpSupportOfficer.DataSource = DB.ISOrganisationUsers.Where(p => p.RoleID == (int)EnumsManagement.USERROLE.SUPPORT && p.Deleted == true && p.Active == true).ToList();
            drpSupportOfficer.DataTextField = "FirstName";
            drpSupportOfficer.DataValueField = "ID";
            drpSupportOfficer.DataBind();
            drpSupportOfficer.Items.Insert(0, new ListItem { Text = "Select Support Officer", Value = "0" });

            drpStatus.DataSource = DB.ISSupportStatus.Where(p => p.Deleted == true && p.Active == true).ToList();
            drpStatus.DataTextField = "Name";
            drpStatus.DataValueField = "ID";
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem { Text = "Select Status", Value = "0" });
        }

        protected void btnPostReply_Click(object sender, EventArgs e)
        {
            try
            {
                ISTicketMessage obj = new ISTicketMessage();
                SetField(ref obj);
                //obj.Active = true;
                //obj.Deleted = true;
                //obj.CreatedBy = 1;//Authentication.LoggedInUser.ID;
                //obj.CreatedDateTime = DateTime.Now;
                DB.ISTicketMessages.Add(obj);
                DB.SaveChanges();
                Clear();
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                BindData(ID);
                AlertMessageManagement.ServerMessage(Page, "Reply Added Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }
        private void Clear()
        {
            try
            {
                txtReply.Text = "";
            }
            catch (Exception ex)
            {
                //ErrorLogManagement.AddLog(Page, ex);
            }
        }

        private void SetField(ref ISTicketMessage obj)
        {
            try
            {
                obj.SupportID = Convert.ToInt32(Request.QueryString["ID"]);
                obj.SenderID = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1; //Convert.ToInt32(Request.QueryString["ID"]);
                obj.Message = txtReply.Text;
                if (UploadFile.HasFile == true)
                {
                    string filename = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName);
                    UploadFile.SaveAs(Server.MapPath("Upload/" + filename));
                    obj.SelectFile = "Upload/" + filename;
                }
                obj.CreatedDatetime = DateTime.Now;
                obj.UserTypeID = 3;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(Page, ex);
            }
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            ISSupport obj = DB.ISSupports.SingleOrDefault(p => p.ID == ID);
            if (obj != null)
            {
                obj.Priority = Convert.ToInt32(drpPriority.SelectedValue);
                int SID = Convert.ToInt32(drpSupportOfficer.SelectedValue);
                if (obj.SupportOfficerID != SID)
                {
                    obj.AssignBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                    obj.AssignDate = DateTime.Now;
                }
                obj.SupportOfficerID = SID;
                obj.StatusID = Convert.ToInt32(drpStatus.SelectedValue);
                DB.SaveChanges();
            }
            BindData(ID);
            AlertMessageManagement.ServerMessage(Page, "Support Updated and Assigned Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
        }
    }
}