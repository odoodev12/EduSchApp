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
    public partial class FAQ : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISUserLogin())
            {
                if (!Authentication.ISOrgUserLogin())
                {
                    Response.Redirect(Authentication.AuthorizePage());
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ISFAQ obj = new ISFAQ();
                obj.Question = txtQuestions.Text;
                obj.Description = txtDescriptions.Text;
                string FileName = "";
                if (UploadAttachments.HasFile == true)
                {
                    string filename = System.IO.Path.GetFileName(UploadAttachments.PostedFile.FileName);
                    UploadAttachments.SaveAs(Server.MapPath("~/Upload/" + filename));
                    FileName = Server.MapPath("../Upload/" + System.IO.Path.GetFileName(UploadAttachments.PostedFile.FileName));
                    obj.Attachment = "Upload/" + filename;
                }
                obj.Active = true;
                obj.Deleted = true;
                obj.CreatedBy = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.ID : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.ID : 1;
                obj.CreatedByName = Authentication.LoggedInUser != null ? Authentication.LoggedInUser.FullName : Authentication.LoggedInOrgUser != null ? Authentication.LoggedInOrgUser.FirstName + " " + Authentication.LoggedInOrgUser.LastName : "";
                obj.CreatedDateTime = DateTime.Now;
                DB.ISFAQs.Add(obj);
                DB.SaveChanges();
                Clear();
                AlertMessageManagement.ServerMessage(Page, "FAQ Sent Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                Response.Redirect("FAQ.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        private void Clear()
        {
            txtQuestions.Text = "";
            txtDescriptions.Text = "";
            UploadAttachments.Attributes.Clear();
        }
    }
}