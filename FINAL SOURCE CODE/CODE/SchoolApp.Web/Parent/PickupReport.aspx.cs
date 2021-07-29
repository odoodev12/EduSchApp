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
    public partial class PickupReport : System.Web.UI.Page
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
                BindListview();
            }
        }
        public void BindListview()
        {
            int ID = Authentication.LogginParent.ID;
            PickupManagement objPickupManagement = new PickupManagement();
            //ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == Authentication.LogginParent.ID && p.Active == true && p.Deleted == true);
           // if (Obj != null)
            {
                List<MISStudent> ObjSt = (from item in DB.ISStudents.Where(p => (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower())
                                          /*&& p.ISSchool.TypeID == 2*/ && p.StartDate == null && p.Active == true && p.Deleted == true).ToList()
                                          select new MISStudent
                                          {
                                              ID = item.ID,
                                              StudentName = item.StudentName,
                                              StudentPic = item.Photo,
                                              SchoolID = item.SchoolID,
                                              SchoolName = item.ISSchool.Name,
                                              TypeID = item.ISSchool.TypeID.Value,
                                              Deleted = item.Deleted,
                                              Active = item.Active
                                          }).OrderByDescending(r=>r.TypeID).ToList();

                var result = new List<MISStudent>();

                foreach (var item in ObjSt.OrderByDescending(r => r.TypeID))
                {
                    if (result.FirstOrDefault(r => r.StudentName.ToLower() == item.StudentName.ToLower() && r.Active == true && r.Deleted == true) == null)
                    {
                        result.Add(item);
                    }
                }

                lstParentPickup.DataSource = result.OrderBy(r=>r.StudentName).ToList();
                lstParentPickup.DataBind();
            }
            //else
            //{
            //    lstParentPickup.DataSource = null;
            //    lstParentPickup.DataBind();
            //}
        }

        protected void lstParentPickup_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var HID = e.Item.FindControl("HID") as HiddenField;
                int ID = Convert.ToInt32(HID.Value);
                ISStudent Obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
                if (Obj != null)
                {
                    var shids = DB.ISStudents.Where(p => p.StudentName == Obj.StudentName && p.Deleted == true && p.Active == true &&
                    (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower()))
                    .Select(q => q.SchoolID).Distinct().ToList();

                    List<ISSchool> ObjStudent = DB.ISSchools.Where(p => shids.Contains(p.ID) && p.Deleted == true).ToList();
                    DropDownList SchoolDropdown = e.Item.FindControl("drpSchool") as DropDownList;
                    SchoolDropdown.DataSource = ObjStudent.OrderByDescending(r=>r.TypeID);
                    SchoolDropdown.DataTextField = "Name";
                    SchoolDropdown.DataValueField = "ID";
                    SchoolDropdown.DataBind();
                }
            }
        }
    }
}