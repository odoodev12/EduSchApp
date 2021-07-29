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
    public partial class Student : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        ClassManagement objClassManagement = new ClassManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin() && !Authentication.ISTeacherLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());
            }
            if (!IsPostBack)
            {
                BindDropdown();
                BindClass();
                BindData();
            }
        }

        private void BindClass()
        {
            ddlClass.DataSource = objClassManagement.ClassList(Authentication.SchoolID, ddlYear.SelectedValue, 0).Where(p => p.TypeID == (int)EnumsManagement.CLASSTYPE.Standard && p.Active == true).ToList();
            ddlClass.DataValueField = "ID";
            ddlClass.DataTextField = "Name";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem { Text = "Select Class", Value = "0" });
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
        private void BindDropdown()
        {
            ddlYear.DataSource = CommonOperation.GetYear();
            ddlYear.DataValueField = "ID";
            ddlYear.DataTextField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem { Text = "Select Year", Value = "" });

        }
        public void BindData()
        {
            if (Authentication.ISTeacherLogin())
            {
                ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Deleted == true);
                lstClasses.DataSource = objClassManagement.ClassList(objTeacher.SchoolID, "", 0).Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Office && p.TypeID != (int)EnumsManagement.CLASSTYPE.Club && p.TypeID != (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).OrderBy(r=>r.Name).ToList();
                lstClasses.DataBind();
            }
            else
            {
                ISSchool ObjSchool = DB.ISSchools.SingleOrDefault(p => p.ID == Authentication.SchoolID);
                if (ObjSchool.TypeID == (int)EnumsManagement.SCHOOLTYPE.Standard)
                {
                    lstClasses.DataSource = objClassManagement.ClassList(Authentication.LogginSchool.ID, "", 0).Where(p => p.TypeID != (int)EnumsManagement.CLASSTYPE.Office && p.TypeID != (int)EnumsManagement.CLASSTYPE.Club && p.TypeID != (int)EnumsManagement.CLASSTYPE.AfterSchool && p.Active == true).OrderBy(r => r.Name).ToList();
                    lstClasses.DataBind();
                }
                else
                {
                    List<MISClass> objList = (from item in DB.ISClasses.Where(p => p.SchoolID == Authentication.SchoolID && p.Deleted == true).ToList()
                                              select new MISClass
                                              {
                                                  ID = item.ID,
                                                  SchoolID = item.SchoolID,
                                                  Name = item.Name,
                                                  YearName = !String.IsNullOrEmpty(item.Year) ? objClassManagement.getyeardata(item.Year) : "N/A",
                                                  Year = item.Year,
                                                  TypeID = item.TypeID,
                                                  AfterSchoolType = item.AfterSchoolType,
                                                  ExternalOrganisation = item.ExternalOrganisation,
                                                  EndDate = item.EndDate,
                                                  EndDateString = item.EndDate != null ? item.EndDate.Value.ToString("dd/MM/yyyy") : "N/A",
                                                  PickupComplete = item.PickupComplete,
                                                  Active = item.Active,
                                                  Deleted = item.Deleted,
                                                  CreatedBy = item.CreatedBy,
                                                  CreatedDateTime = item.CreatedDateTime,
                                                  ModifyBy = item.ModifyBy,
                                                  ModifyDateTime = item.ModifyDateTime,
                                                  DeletedBy = item.DeletedBy,
                                                  DeletedDateTime = item.DeletedDateTime,
                                                  ClassType = item.ISClassType.Name,
                                                  StudentCount = item.Name.Contains("Outside") ? objClassManagement.getExternalStudentCount(item.SchoolID) : objClassManagement.getStudentCount(item.ID, item.SchoolID),
                                              }).ToList().OrderBy(r => r.Name).ToList();
                    lstClasses.DataSource = objList;
                    lstClasses.DataBind();
                }
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue != "")
            {
                if (Authentication.ISTeacherLogin())
                {
                    ISTeacher objTeacher = DB.ISTeachers.SingleOrDefault(p => p.ID == Authentication.LogginTeacher.ID && p.Role == (int)EnumsManagement.ROLETYPE.TEACHING && p.Deleted == true);
                    lstClasses.DataSource = objClassManagement.ClassList(objTeacher.SchoolID, "", 0).Where(p => p.Active == true).ToList();
                    lstClasses.DataBind();
                    BindClass();
                }
                else
                {
                    lstClasses.DataSource = objClassManagement.ClassList(Authentication.LogginSchool.ID, ddlYear.SelectedValue, 0).Where(p => p.Active == true).ToList();
                    lstClasses.DataBind();
                    BindClass();
                }
            }
            else
            {
                BindData();
                BindClass();
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue != "0")
            {
                int ID = Convert.ToInt32(ddlClass.SelectedValue);
                lstClasses.DataSource = objClassManagement.ClassList(Authentication.SchoolID, ddlYear.SelectedValue, 0).Where(p => p.ID == ID).ToList();
                lstClasses.DataBind();
            }
            else
            {
                BindData();
            }
        }
    }
}