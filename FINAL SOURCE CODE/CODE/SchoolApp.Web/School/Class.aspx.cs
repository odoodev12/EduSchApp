using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Web.School
{
    public partial class Class : System.Web.UI.Page
    {

        SchoolAppEntities DB = new SchoolAppEntities();
        ClassManagement objClassManagement = new ClassManagement();
        EmailManagement _EmailManagement = new EmailManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!Page.IsPostBack)
            {
                Binddropdown();
                bindData();
                //txtEndDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                if (Authentication.SchoolTypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    ////lbtn.Visible = false;
                }
                else
                {
                    lbtn.Visible = true;
                }
            }
        }
        public bool? ISTeacher()
        {
            if (Authentication.LogginTeacher != null)
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Deleted == true);
                if (objTeacher.ISUserRole.ManageClassFlag == true)
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

        public bool IsStandardSchool()
        {
            return Authentication.SchoolTypeID == 2;
        }
        public void Binddropdown()
        {
            ddlSelectClassYear.DataSource = CommonOperation.GetYear();
            ddlSelectClassYear.DataValueField = "ID";
            ddlSelectClassYear.DataTextField = "Year";
            ddlSelectClassYear.DataBind();
            ddlSelectClassYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });


            ddlClassYear.DataSource = CommonOperation.GetYear();
            ddlClassYear.DataValueField = "ID";
            ddlClassYear.DataTextField = "Year";
            ddlClassYear.DataBind();
            ddlClassYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });



            ddlClassType.DataSource = objClassManagement.ClassTypeListSchoolWise(Authentication.SchoolTypeID);
            ddlClassType.DataValueField = "ID";
            ddlClassType.DataTextField = "Name";
            ddlClassType.DataBind();
            ddlClassType.Items.Insert(0, new ListItem { Text = "Select Class Type", Value = "0" });

            ddlSelectClassType.DataSource = objClassManagement.ClassTypeListSchoolWise(Authentication.SchoolTypeID);
            ddlSelectClassType.DataValueField = "ID";
            ddlSelectClassType.DataTextField = "Name";
            ddlSelectClassType.DataBind();
            ddlSelectClassType.Items.Insert(0, new ListItem { Text = "Select Class Type", Value = "0" });
        }
        public void bindData()
        {
            lstClasses.DataSource = objClassManagement.ClassListByFilter(Authentication.SchoolID, "", 0, "1").OrderByDescending(r => (!r.Name.Contains("(Office)") && !r.Name.Contains("(After School)") && !r.Name.Contains("(Club)"))).ThenBy(r => r.Name).ToList();
            lstClasses.DataBind();
        }

        protected void ddlSelectClassYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectClassYear.SelectedValue != "")
            {
                lstClasses.DataSource = objClassManagement.ClassListByFilter(Authentication.SchoolID, ddlSelectClassYear.SelectedValue, Convert.ToInt32(ddlSelectClassType.SelectedValue), drpClassStatus.SelectedValue);
                lstClasses.DataBind();
            }
            else
            {
                bindData();
            }
        }
        public void Clear()
        {
            txtClassName.Text = string.Empty;
            ddlClassYear.SelectedValue = "";
            ddlClassType.SelectedValue = "0";
            ddlAfterSchoolType.SelectedValue = "Internal";
            txtOrganization.Text = "";
            //txtEndDate.Text = "";
        }
        protected void btnCreateClass_Click(object sender, EventArgs e)
        {

            SchoolAppEntities DB = new SchoolAppEntities();
            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);

            List<ISClass> ObjClasses = DB.ISClasses.Where(p => p.Name == txtClassName.Text && p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList();
            if (ObjClasses.Count > 0)
            {
                AlertMessageManagement.ServerMessage(Page, "Class Name already Exist. Please try different one.", (int)AlertMessageManagement.MESSAGETYPE.warning);
            }
            else
            {

                ISClass obj = new ISClass();
                if (ddlClassType.SelectedValue == Convert.ToString((int)EnumsManagement.CLASSTYPE.AfterSchool))
                {
                    List<ISClass> ObjClass = DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.TypeID == (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).ToList();
                    if (ObjClass.Count <= 0)
                    {
                        if (ddlAfterSchoolType.SelectedValue == "Internal")
                        {
                            obj.Name = txtClassName.Text + "(After School)";
                            obj.TypeID = Convert.ToInt32(ddlClassType.SelectedValue);
                            obj.AfterSchoolType = ddlAfterSchoolType.SelectedValue;
                            obj.ExternalOrganisation = "";
                            obj.EndDate = new DateTime(2050, 01, 01);
                            obj.SchoolID = Authentication.SchoolID;
                            obj.ISNonListed = false;
                            obj.Active = true;
                            obj.Deleted = true;
                            obj.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.SchoolID;
                            obj.CreatedByType = Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                            obj.CreatedDateTime = DateTime.Now;
                            DB.ISClasses.Add(obj);
                            DB.SaveChanges();
                            Clear();
                            EmailManage(obj);
                            LogManagement.AddLog("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", "Class");
                            AlertMessageManagement.ServerMessage(Page, "Class Created Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                        }
                        else
                        {
                            if (DB.ISSchools.Where(p => p.Name == txtOrganization.Text).Count() <= 0)
                            {
                                Clear();
                                AlertMessageManagement.ServerMessage(Page, "Input the name of the After-School in the text box below. Please select the non-listed checkbox to continue if the name is not auto-selected from the list on our database", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            }
                            //else if(DB.ISSchools.Where(p => p.Name == txtOrganization.Text).Count() > 0 && rdNonListed.Checked == true)
                            //{
                            //    Clear();
                            //    AlertMessageManagement.ServerMessage(Page, "After School name is already setup on our database. Please select Listed and pick the name of the After School from the search field", (int)AlertMessageManagement.MESSAGETYPE.warning);
                            //}
                            else
                            {
                                obj.Name = txtClassName.Text + "(After School Ex)";
                                obj.TypeID = Convert.ToInt32(ddlClassType.SelectedValue);
                                obj.AfterSchoolType = ddlAfterSchoolType.SelectedValue;
                                obj.ExternalOrganisation = txtOrganization.Text;
                                obj.EndDate = new DateTime(2050, 01, 01);
                                obj.SchoolID = Authentication.SchoolID;
                                obj.ISNonListed = false;
                                obj.Active = true;
                                obj.Deleted = true;
                                obj.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.SchoolID;
                                obj.CreatedByType = Authentication.ISTeacherLogin() == true ? (int)EnumsManagement.CREATEBYTYPE.Teacher : (int)EnumsManagement.CREATEBYTYPE.School;
                                obj.CreatedDateTime = DateTime.Now;
                                DB.ISClasses.Add(obj);
                                DB.SaveChanges();
                                Clear();
                                EmailManage(obj);
                                LogManagement.AddLog("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", "Class");
                                AlertMessageManagement.ServerMessage(Page, "Class Created Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);

                            }
                        }
                    }
                    else
                    {
                        Clear();
                        AlertMessageManagement.ServerMessage(Page, "Only One After School is Allowed for a Standard School", (int)AlertMessageManagement.MESSAGETYPE.warning);
                    }
                }
                else
                {
                    obj.Name = txtClassName.Text;
                    if (ddlClassType.SelectedValue == Convert.ToString((int)EnumsManagement.CLASSTYPE.Standard))
                    {
                        obj.Year = IsStandardSchool() ? ddlClassYear.SelectedValue : "1";
                    }
                    obj.TypeID = Convert.ToInt32(ddlClassType.SelectedValue);
                    obj.EndDate = new DateTime(2050, 01, 01);
                    obj.SchoolID = Authentication.SchoolID;
                    obj.ISNonListed = false;
                    obj.Active = true;
                    obj.Deleted = true;
                    obj.CreatedBy = Authentication.ISTeacherLogin() == true ? Authentication.LogginTeacher.ID : Authentication.SchoolID;
                    obj.CreatedDateTime = DateTime.Now;
                    DB.ISClasses.Add(obj);
                    DB.SaveChanges();
                    Clear();
                    EmailManage(obj);
                    LogManagement.AddLog("Class Created Successfully " + "Name : " + obj.Name + " Document Category : Class", "Class");
                    AlertMessageManagement.ServerMessage(Page, "Class Created Successfully", (int)AlertMessageManagement.MESSAGETYPE.Success);
                }
            }
            bindData();
        }

        public string GetInitailValueOfClassType
        {
            get
            {
                return IsStandardSchool() ? "" : "1";
            }
        }

        public void EmailManage(ISClass _Class)
        {
            string AdminBody = String.Empty;
            string SuperwisorBody = String.Empty;

            ISSchool _School = DB.ISSchools.SingleOrDefault(x => x.ID == Authentication.SchoolID);
            string LoggedINName = Authentication.LogginSchool != null ? Authentication.LogginSchool.AdminFirstName + " " + Authentication.LogginSchool.AdminLastName : Authentication.LogginTeacher.Name;
            string tblSupbody = string.Empty;
            string tblAdminBody = string.Empty;
            tblSupbody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.SupervisorFirstname + ' ' + _School.SupervisorLastname + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    New Class / Classess  Added By  : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> " + LoggedINName + @"<br/> " + _School.Name + @"
                                    </td>
                                </tr></table>";
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                SuperwisorBody += reader.ReadToEnd();
            }
            SuperwisorBody = SuperwisorBody.Replace("{Body}", tblSupbody);

            tblAdminBody = @"<table>
                            <tr style='float:left;'>
                                <td>
                                 Dear " + _School.AdminFirstName + ' ' + _School.AdminLastName + @" ,<br/><br/>
                                </td><br/>
                            </tr>
                            <tr><br/>
                                <td style = 'text-align:left;'>
                                    New Class / Classess  Added By  : " + LoggedINName + @"<br/>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        Creation Date : " + DateTime.Now.ToString("dd/MM/yyyy") + @"
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        Please review the relevant reports or contact<b>" + LoggedINName + @"</b> should you require more information.
                                   </td>
                                </tr>
                                <tr style = 'float:right;'>
                                    <td>
                                        <br/>
                                        Regards, <br/> " + LoggedINName + @"<br/> " + _School.Name + @"
                                    </td>
                                </tr></table>";

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.html")))
            {
                AdminBody += reader.ReadToEnd();
            }
            AdminBody = AdminBody.Replace("{Body}", tblAdminBody);

            _EmailManagement.SendEmail(_School.AdminEmail, "New Class Created ", AdminBody);
            _EmailManagement.SendEmail(_School.SupervisorEmail, "New Class Created ", SuperwisorBody);
        }
        protected void ddlSelectClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectClassType.SelectedValue != "0")
            {
                if (ddlSelectClassType.SelectedValue == "2" || ddlSelectClassType.SelectedValue == "3" || ddlSelectClassType.SelectedValue == "5" || ddlSelectClassType.SelectedValue == "6")
                {
                    ddlSelectClassYear.SelectedValue = "";
                    ddlSelectClassYear.Enabled = false;
                    ddlSelectClassYear.Style.Add("color", "#cccccc !important");
                    ddlSelectClassYear.CssClass = "form-control";
                }
                else
                {
                    ddlSelectClassYear.Enabled = true;
                    ddlSelectClassYear.Style.Add("color", "#555 !important");
                }
                lstClasses.DataSource = objClassManagement.ClassListByFilter(Authentication.SchoolID, ddlSelectClassYear.SelectedValue, Convert.ToInt32(ddlSelectClassType.SelectedValue), drpClassStatus.SelectedValue);
                lstClasses.DataBind();

            }
            else
            {
                bindData();
                ddlSelectClassYear.Enabled = true;
                ddlSelectClassYear.Style.Add("color", "#555 !important");
            }
        }

        protected void drpClassStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectClassType.SelectedValue != "0")
            {
                lstClasses.DataSource = objClassManagement.ClassListByFilter(Authentication.SchoolID, ddlSelectClassYear.SelectedValue, Convert.ToInt32(ddlSelectClassType.SelectedValue), drpClassStatus.SelectedValue);
                lstClasses.DataBind();
            }
            else
            {
                lstClasses.DataSource = objClassManagement.ClassListByFilter(Authentication.SchoolID, ddlSelectClassYear.SelectedValue, Convert.ToInt32(ddlSelectClassType.SelectedValue), drpClassStatus.SelectedValue);
                lstClasses.DataBind();
            }
        }

        protected void valDateRange_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime minDate = DateTime.Now;
            DateTime maxDate = DateTime.Parse("9999/12/28");
            DateTime dt;

            args.IsValid = (DateTime.TryParse(args.Value, out dt)
                            && dt <= maxDate
                            && dt >= minDate);
            AlertMessageManagement.ServerMessage(Page, args.IsValid.ToString(), (int)AlertMessageManagement.MESSAGETYPE.Success);
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetOrganisations(string prefixText)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            TeacherManagement objTeacherManagement = new TeacherManagement();
            List<ISSchool> ObjList = DB.ISSchools.Where(p => p.Deleted == true && p.Active == true && p.Name.ToLower().Contains(prefixText.ToLower()) && p.TypeID == (int)EnumsManagement.SCHOOLTYPE.AfterSchool).ToList();

            List<string> AfterSchools = new List<string>();
            for (int i = 0; i < ObjList.Count; i++)
            {
                AfterSchools.Add(ObjList[i].Name);
            }
            //if (AfterSchools.Count <= 0)
            //{
            //    AfterSchools.Add("Record not Found. Please Select NonListed Radio Button and Write your School Name.");
            //}
            return AfterSchools;
        }

        protected void rdNonListed_CheckedChanged(object sender, EventArgs e)
        {
            //if(rdNonListed.Checked == true)
            //{
            //    AutoCompleteTeachers.Enabled = false;
            //}
            //else
            //{
            //    AutoCompleteTeachers.Enabled = true;
            //}
        }

        protected void rdListed_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdListed.Checked == true)
            //{
            //    AutoCompleteTeachers.Enabled = true;
            //}
            //else
            //{
            //    AutoCompleteTeachers.Enabled = false;
            //}
        }
    }
}