using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class PickupMessage : System.Web.UI.Page
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
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Convert.ToInt32(Request.QueryString["ID"]);

                }

                int SchoolID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                lblSchoolNamewithColor.Text = " @ " + DB.ISSchools.FirstOrDefault(r => r.ID == SchoolID).Name;
            }
        }

        

        protected void lbtnSendToTeacher_Click(object sender, EventArgs e)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            if (Request.QueryString["Name"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["SchoolID"]);
                string studentname = Request.QueryString["Name"];

                ISStudent CID = DB.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.SchoolID == ID && p.StudentName == studentname && p.Active == true);
                var tlist = DB.ISTeacherClassAssignments.Where(p => p.ISTeacher.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.ClassID == CID.ClassID && p.Active == true).ToList();
                if (rbl.SelectedIndex == -1)
                {
                    AlertMessageManagement.ServerMessage(Page, "Please select one Message Option to Complete this Action", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else if(rbl.SelectedIndex == 3 && txtOther.Text.Length == 0)
                {
                    AlertMessageManagement.ServerMessage(Page, "Please enter other pickup message.", (int)AlertMessageManagement.MESSAGETYPE.warning);
                }
                else
                {
                    foreach (var TID in tlist)
                    {
                        string Desc = rbl.SelectedValue + txtOther.Text;
                        ISStudent ObjStudent = DB.ISStudents.SingleOrDefault(p => p.SchoolID == ID && p.StudentName == studentname && p.Active == true);
                        string Email = DB.ISTeachers.SingleOrDefault(p => p.ID == TID.TeacherID).Email;
                        EmailManagement objEmailManagement = new EmailManagement();
                        try
                        {
                            string Message = string.Format("<center><font size='5' color='blue'>School APP</font></center><br /><br />Dear {0},<br><br> Subject &nbsp;: {1}<br><br>Description &nbsp;: {2}.<br /><br/>Thanks, <br/> StudentName : {3}<br>Parent Name : {4}<br>Class : {5}<br>", TID.ISTeacher.Name, "PickUp Message", Desc, ObjStudent.StudentName, ObjStudent.ParantName1, ObjStudent.ISClass.Name);
                            objEmailManagement.SendEmail(Email, "Pickup Message", Message);
                        }
                        catch (Exception ex)
                        {
                            ErrorLogManagement.AddLog(ex);
                        }
                        ISPickUpMessage ObjMessage = new ISPickUpMessage();
                        ObjMessage.SchoolID = Authentication.SchoolID;
                        ObjMessage.Message = Desc;
                        ObjMessage.ReceiverID = TID.TeacherID;
                        ObjMessage.ClassID = ObjStudent.ClassID;
                        ObjMessage.SendID = ObjStudent.ID;
                        ObjMessage.SenderName = ObjStudent.ParantName1;
                        ObjMessage.Viewed = false;
                        ObjMessage.Active = true;
                        ObjMessage.Deleted = true;
                        ObjMessage.CreatedBy = Authentication.LogginParent.ID;
                        ObjMessage.CreatedDateTime = DateTime.Now;
                        DB.ISPickUpMessages.Add(ObjMessage);
                        DB.SaveChanges();
                    }
                    Clear();
                    AlertMessageManagement.ServerMessage(Page, "Message send Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                    Response.Redirect("Message.aspx", false);
                }
            }
        }
        protected void Clear()
        {
            rbl.ClearSelection();
            txtOther.Text = "";
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl.SelectedValue == " Other")
            {
                PanelOther.Visible = true;
            }
            else
            {
                PanelOther.Visible = false;
                txtOther.Text = "";
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Message.aspx", true);
        }
    }
}