using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.Parent
{
    public partial class ManagePickers : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISParentLogin())
            {
                Response.Redirect(Authentication.ParentAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindList();
            }
            foreach (var item in lstPicker.Items)
            {
                FileUpload fp = (FileUpload)item.FindControl("fpUpload");
                if (IsPostBack && fp.PostedFile != null && fp != null)
                {
                    HiddenField HID = (HiddenField)item.FindControl("HID");
                    if (fp.PostedFile.FileName.Length > 0)
                    {
                        int ID = Convert.ToInt32(HID.Value);
                        ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Deleted == true);
                        if (objStudent != null)
                        {
                            string filename = System.IO.Path.GetFileName(fp.PostedFile.FileName);
                            fp.SaveAs(Server.MapPath("~/Upload/" + filename));
                            objStudent.Photo = "Upload/" + filename;
                            DB.SaveChanges();
                            SendStudentImageEmailManage(objStudent);
                            AlertMessageManagement.ServerMessage(Page, "Photo Uploaded Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                        }
                        BindList();
                    }
                }
            }
        }
        private void BindList()
        {
            int StudentID = Authentication.LogginParent.ID;
            string Email = Authentication.LoginParentEmail;
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Active == true && p.Deleted == true);
            if (objStudent != null)
            {
                List<MISStudent> obj = (from item in DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == Email.ToLower() || p.ParantEmail2.ToLower() == Email.ToLower()) && p.StartDate == null && p.Active == true && p.Deleted == true).ToList()
                                        select new MISStudent
                                        {
                                            ID = item.ID,
                                            StudentName = item.StudentName,
                                            Photo = item.Photo,
                                            SchoolName = item.ISSchool.Name,
                                            SchoolNumber = item.ISSchool.PhoneNumber,
                                            SchoolID = item.ISSchool.ID,
                                            TypeID = item.ISSchool.TypeID.Value,
                                            Deleted = item.Deleted,
                                            Active = item.Active
                                        }).ToList();

                var result = new List<MISStudent>();

                foreach (var item in obj.OrderByDescending(r => r.TypeID))
                {
                    if (result.FirstOrDefault(r => r.StudentName.ToLower() == item.StudentName.ToLower() && r.Active == true && r.Deleted == true) == null)
                    {
                        result.Add(item);
                    }
                }

                lstPicker.DataSource = result.ToList().OrderBy(r => r.StudentName).ToList();
                lstPicker.DataBind();
            }
        }

        protected void lstPicker_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = (DataPager)lstPicker.FindControl("DataPager1");
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindList();
        }

        protected void lstPicker_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {



                var HID = e.Item.FindControl("HID") as HiddenField;
                int ID = Convert.ToInt32(HID.Value);
                ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
                {
                    var shids = DB.ISStudents.Where(p => p.StudentName == Obj.StudentName && p.Deleted == true && p.Active == true && 
                    (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()))
                    .Select(q => q.SchoolID).Distinct().ToList();

                    List<ISSchool> ObjStudent = DB.ISSchools.Where(p => shids.Contains(p.ID) && p.Deleted == true).OrderByDescending(r => r.TypeID).ToList();
                    DropDownList SchoolDropdown = e.Item.FindControl("drpSchool") as DropDownList;

                    ObjStudent.ForEach(r => r.PhoneNumber = $"{r.ID}##{r.PhoneNumber}");

                    SchoolDropdown.DataSource = ObjStudent.ToList();


                    SchoolDropdown.DataTextField = "Name";
                    SchoolDropdown.DataValueField = "PhoneNumber";
                    SchoolDropdown.DataBind();
                }
            }
        }


        public void SendStudentImageEmailManage(ISStudent _Student)
        {
            try
            {
                ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
                string LoggedinParent = Authentication.LoginParentName;
                string SuperwisorBody = string.Empty;
                string tblSupbody = string.Empty;
                tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear Parent/Guardian's of " + _Student.StudentName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td>
                                    Student , " + _Student.StudentName + @", image updated by " + LoggedinParent + @"
                                </td> 
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Amendment by : " + LoggedinParent + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Date Generated : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                   </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                       Time Generated : " + DateTime.Now.ToString("hh:mm tt") + @"
                                   </td>
                                </tr></table>";
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
                {
                    SuperwisorBody += reader.ReadToEnd();
                }
                SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

                _EmailManagement.SendEmail(_Student.ParantEmail1, "Browse Picker Picture Notification", SuperwisorBody);
                if (!String.IsNullOrEmpty(_Student.ParantEmail2))
                {
                    _EmailManagement.SendEmail(_Student.ParantEmail2, "Browse Picker Picture Notification", SuperwisorBody);
                }
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
            }
        }

        protected void btnCreatePicker_Click(object sender, EventArgs e)
        {
            int ID = Authentication.LogginParent.ID;
            Response.Redirect("CreatePicker.aspx?ID=" + ID);
        }

        protected void drpSchool_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lstPicker_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "btnViewPicker")
            {
                string navigationURL = "";
                var dropDownList = (e.Item.FindControl("drpSchool") as DropDownList);
                if (dropDownList != null)
                {
                    string schoolName = (e.Item.FindControl("drpSchool") as DropDownList).SelectedItem.Text.ToLower();
                    int schoolId = DB.ISSchools.FirstOrDefault(r => r.Name.ToLower() == schoolName).ID;
                    int studentId = Convert.ToInt32((e.Item.FindControl("HID") as HiddenField).Value);
                    navigationURL = $"StudentPicker.aspx?ID={studentId}&SchoolId={schoolId}";
                    Response.Redirect(navigationURL);
                }
                //< a href = "StudentPicker.aspx?ID=<%# Eval("ID") %>&StudentName=<%# Eval("StudentName")%>&SchoolID=<%# Convert.ToInt32(Eval("SchoolId"))%>" class="btn btn-primary">View Child Pickers</a>
            }
        }

        protected void lstPicker_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {

        }
    }
}