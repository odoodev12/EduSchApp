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
    public partial class Message : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindList();
                //BindDropdownParent();
                //BindDropdownTeacher();
            }
        }

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }
        //private void BindDropdownParent()
        //{
        //    int SID = Authentication.LogginSchool.ID;
        //    drpReceiver.DataSource = DB.ISStudents.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SID).ToList();
        //    drpReceiver.DataTextField = "ParantName1";
        //    drpReceiver.DataValueField = "ID";
        //    drpReceiver.DataBind();
        //    drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
        //}
        //private void BindDropdownTeacher()
        //{
        //    int SID = Authentication.LogginSchool.ID;
        //    drpReceiver.DataSource = DB.ISTeachers.Where(p => p.Deleted == true && p.Active == true && p.SchoolID == SID).ToList();
        //    drpReceiver.DataTextField = "Name";
        //    drpReceiver.DataValueField = "ID";
        //    drpReceiver.DataBind();
        //    drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
        //}

        private void BindList()
        {

            int SID = Authentication.LogginSchool.ID;

            MessageManagement objNewMessage = new MessageManagement();
            lstSent.DataSource = objNewMessage.SentMessageList(Authentication.LogginSchool.ID, (int)EnumsManagement.MESSAGEUSERTYPE.School);
            lstSent.DataBind();
            lstReceived.DataSource = objNewMessage.ReceivedMessageList(Authentication.LogginSchool.ID, (int)EnumsManagement.MESSAGEUSERTYPE.School);
            lstReceived.DataBind();

        }
        protected void lstSent_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //DataPager dp = (DataPager)lstSent.FindControl("DataPager1");
            //dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            //BindList(drpReceiver.SelectedValue);
        }
        //protected void drpReceiver_SelectedIndexChanged(object sender, PagePropertiesChangingEventArgs e)
        //{
        //    BindList(drpReceiver.SelectedValue);
        //}
        //protected void btnSend_Click(object sender, EventArgs e)
        //{
        //    int SID = Authentication.LogginSchool.ID;
        //    ISMessage objMsg = new ISMessage();

        //    objMsg.Title = txtSubject.Text;
        //    if (fpUploadAttachment.HasFile == true)
        //    {
        //        string filename = System.IO.Path.GetFileName(fpUploadAttachment.PostedFile.FileName);
        //        fpUploadAttachment.SaveAs(Server.MapPath("../Upload/" + filename));
        //        objMsg.Attechment = "../Upload/" + filename;
        //    }
        //    objMsg.Desc = txtDescription.Text;
        //    objMsg.SendID = Authentication.LogginSchool.ID;
        //    objMsg.SenderType = (int)EnumsManagement.MESSAGEUSERTYPE.School;
        //    objMsg.ReceiveID = Convert.ToInt32(drpReceiver.SelectedValue);
        //    objMsg.ReceiverType = Convert.ToInt32(drpReceiverGroup.SelectedValue);

        //    objMsg.SchoolID = Authentication.LogginSchool.ID;
        //    objMsg.Time = DateTime.Now;
        //    objMsg.Active = true;
        //    objMsg.Deleted = true;
        //    objMsg.CreatedBy = Authentication.LogginSchool.ID;
        //    objMsg.CreatedDateTime = DateTime.Now;
        //    DB.ISMessages.Add(objMsg);
        //    DB.SaveChanges();
        //    BindList(drpReceiver.SelectedValue);
        //    Clear();
        //}
        //private void Clear()
        //{
        //    txtDescription.Text = "";
        //    txtSubject.Text = "";
        //    drpReceiver.SelectedValue = "0";
        //    drpReceiverGroup.SelectedValue = "0";
        //}
        protected void lstSent_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lbtnReply")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("NewMessage.aspx?OP=Reply&ID=" + ID);
                //ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                //if (objMessage != null)
                //{
                //    drpReceiverGroup.SelectedValue = objMessage.ReceiverType.ToString();
                //    BindReceiverGroup(Convert.ToInt32(drpReceiverGroup.SelectedValue));
                //    drpReceiver.SelectedValue = objMessage.ReceiveID.ToString();
                //    txtDescription.Text = "";
                //    txtSubject.Text = "";
                //}
            }
            if (e.CommandName == "lbtnForward")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("NewMessage.aspx?OP=Forward&FID=" + ID);
                //ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                //if (objMessage != null)
                //{
                //    drpReceiver.SelectedValue = "0";
                //    drpReceiverGroup.SelectedValue = "0";
                //    txtDescription.Text = objMessage.Desc;
                //    txtSubject.Text = objMessage.Title;
                //}
            }
            if (e.CommandName == "lbtnDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                if (objMessage != null)
                {
                    objMessage.Active = false;
                    objMessage.Deleted = false;
                    objMessage.DeletedBy = Authentication.LogginSchool.ID;
                    objMessage.DeletedDateTime = DateTime.Now;
                    DB.SaveChanges();
                }
                BindList();
                LogManagement.AddLog("Message Deleted Successfully" + "Name : " + objMessage.Title + " Document Category : Message", "Message");
                AlertMessageManagement.ServerMessage(Page, "Message Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
        }

        protected void lstReceived_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "lbtnRReply")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("NewMessage.aspx?OP=RReply&RID=" + ID);
                //ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                //if (objMessage != null)
                //{
                //    drpReceiverGroup.SelectedValue = objMessage.ReceiverType.ToString();
                //    BindReceiverGroup(Convert.ToInt32(drpReceiverGroup.SelectedValue));
                //    drpReceiver.SelectedValue = objMessage.ReceiveID.ToString();
                //    txtDescription.Text = "";
                //    txtSubject.Text = "";
                //}
            }
            if (e.CommandName == "lbtnRForward")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("NewMessage.aspx?OP=RForward&RFID=" + ID);
                //ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                //if (objMessage != null)
                //{
                //    drpReceiver.SelectedValue = "0";
                //    drpReceiverGroup.SelectedValue = "0";
                //    txtDescription.Text = objMessage.Desc;
                //    txtSubject.Text = objMessage.Title;
                //}
            }
            if (e.CommandName == "lbtnRDelete")
            {
                int ID = Convert.ToInt32(e.CommandArgument);
                ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
                if (objMessage != null)
                {
                    objMessage.Active = false;
                    objMessage.Deleted = false;
                    objMessage.DeletedBy = Authentication.LogginSchool.ID;
                    objMessage.DeletedDateTime = DateTime.Now;
                    DB.SaveChanges();
                }
                BindList();
                LogManagement.AddLog("Message Deleted Successfully" + "Name : " + objMessage.Title + " Document Category : Message", "Message");
                AlertMessageManagement.ServerMessage(Page, "Message Deleted Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
            }
        }

        //protected void lstReceived_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "lbtnReply")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        Response.Redirect("NewMessage.aspx?OP=Reply&ID=" + ID);
        //        //ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
        //        //if (objMessage != null)
        //        //{
        //        //    drpReceiverGroup.SelectedValue = objMessage.ReceiverType.ToString();
        //        //    BindReceiverGroup(Convert.ToInt32(drpReceiverGroup.SelectedValue));
        //        //    drpReceiver.SelectedValue = objMessage.ReceiveID.ToString();
        //        //    txtDescription.Text = "";
        //        //    txtSubject.Text = "";
        //        //}
        //    }
        //    if (e.CommandName == "lbtnForward")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        Response.Redirect("NewMessage.aspx?OP=Forward&FID=" + ID);
        //        //ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
        //        //if (objMessage != null)
        //        //{
        //        //    drpReceiver.SelectedValue = "0";
        //        //    drpReceiverGroup.SelectedValue = "0";
        //        //    txtDescription.Text = objMessage.Desc;
        //        //    txtSubject.Text = objMessage.Title;
        //        //}
        //    }
        //    if (e.CommandName == "lbtnDelete")
        //    {
        //        int ID = Convert.ToInt32(e.CommandArgument);
        //        ISMessage objMessage = DB.ISMessages.SingleOrDefault(p => p.ID == ID);
        //        if (objMessage != null)
        //        {
        //            objMessage.Active = false;
        //            objMessage.Deleted = false;
        //            objMessage.DeletedBy = Authentication.LogginSchool.ID;
        //            objMessage.DeletedDateTime = DateTime.Now;
        //            DB.SaveChanges();
        //        }
        //        BindList();
        //    }
        //}

        //protected void drpReceiverGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindReceiverGroup(Convert.ToInt32(drpReceiverGroup.SelectedValue));
        //}
        //public void BindReceiverGroup(int GroupID)
        //{
        //    if (GroupID == (int)EnumsManagement.MESSAGEUSERTYPE.Teacher)
        //    {
        //        TeacherManagement objTeacherManagement = new TeacherManagement();
        //        drpReceiver.DataSource = objTeacherManagement.TeacherList(Authentication.SchoolID, "", 0, "", "", "");
        //        drpReceiver.DataValueField = "ID";
        //        drpReceiver.DataTextField = "Name";
        //        drpReceiver.DataBind();
        //        drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
        //    }
        //    if (GroupID == (int)EnumsManagement.MESSAGEUSERTYPE.Parent)
        //    {
        //        StudentManagement objStudentManagement = new StudentManagement();

        //        ParentManagement objParentManagement = new ParentManagement();
        //        drpReceiver.DataSource = objParentManagement.ParentList(Authentication.SchoolID);
        //        drpReceiver.DataValueField = "ID";
        //        drpReceiver.DataTextField = "ParantName1";
        //        drpReceiver.DataBind();
        //        drpReceiver.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
        //    }
        //}

    }
}