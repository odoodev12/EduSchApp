using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.ClassLibrary
{
    public class PickerManagement
    {
        public List<MISPICKER> AllPickerList(int ParentID, int StudentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISPICKER> ObjList = (from item in DB.ISPickers.Where(p => p.StudentID == ParentID && p.Deleted == true).ToList()
                                       select new MISPICKER
                                       {
                                           ID = item.ID,
                                           ParentID = item.ParentID,
                                           StudentID = item.StudentID,
                                           Photo = item.Photo,
                                           Title = item.Title,
                                           PickerName = item.FirstName + " " + item.LastName,
                                           Email = item.Email,
                                           Phone = item.Phone,
                                           ActiveStatus = item.Active == true ? "Active" : "Inactive",
                                           ParentName = DB.ISStudents.SingleOrDefault(p => p.ID == item.StudentID).ParantName1,
                                           Active = item.Active,
                                       }).ToList();
            List<MISPickerAssignment> objList2 = GetPickerAssignment(StudentID);
            //List<int> objList1 = (from item in DB.ISPickers.Where(p => p.StudentID == ParentID && p.Deleted == true).ToList()
            //                      join item1 in objList2.ToList() on item.ID equals item1.PickerId
            //                      select ParentID = item.ID).ToList();
            List<string> objList1 = (from item in DB.ISPickers.Where(p => p.Deleted == true).ToList()
                                     join item1 in objList2.ToList() on item.ID equals item1.PickerId
                                     select item.FirstName + " " + item.LastName).Distinct().ToList();
            //List<MISPICKER> objList = objPickerManagement.AllPickerList(ID, 0);

            //List<MISPICKER> objLists = ObjList.Where(p => !objList1.Contains(p.ID)).ToList();
            List<MISPICKER> objLists = ObjList.Where(p => !objList1.Contains(p.PickerName)).ToList();
            //if (ParentID != 0)
            //{
            //    objLists = objLists.Where(p => p.ParentID == ParentID).ToList();
            //}

            //if (StudentID != 0)
            //{
            //    ObjList = ObjList.Where(p => p.StudentID == StudentID).ToList();
            //}

            return objLists;
        }
        public List<MISPickerAssignment> GeneratePickerCode(int PickerID, int ParentID, int StudentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerID && p.StudentID == StudentID);
            if (objPicker != null)
            {
                objPicker.PickerCode = CommonOperation.GenerateNewRandom();
                objPicker.PickCodeExDate = DateTime.Now;
                DB.SaveChanges();
                List<MISPickerAssignment> objList = (from item in DB.ISPickerAssignments.Where(p => p.ID == PickerID && p.StudentID == StudentID).ToList()
                                                     select new MISPickerAssignment
                                                     {
                                                         ID = item.ID,
                                                         StudentID = item.StudentID,
                                                         PickerCode = item.PickerCode,
                                                         PickCodeExDate = item.PickCodeExDate
                                                     }).ToList();
                return objList;
            }
            else
            {
                return null;
            }




        }
        public List<MISPickerAssignment> GeneratePickerCode2ndCondition(int PickerID, int ParentID, int StudentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISPickerAssignment objPicker = DB.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerID && p.StudentID == StudentID);
            if (objPicker != null)
            {
                objPicker.PickerCode = CommonOperation.GenerateNewRandom();
                objPicker.PickCodeExDate = DateTime.Now;
                DB.SaveChanges();
                List<MISPickerAssignment> objList = (from item in DB.ISPickerAssignments.Where(p => p.ID == PickerID && p.StudentID == StudentID).ToList()
                                                     select new MISPickerAssignment
                                                     {
                                                         ID = item.ID,
                                                         StudentID = item.StudentID,
                                                         PickerCode = item.PickerCode,
                                                         PickCodeExDate = item.PickCodeExDate
                                                     }).ToList();
                return objList;
            }
            else
            {
                return null;
            }




        }
        public bool DeletePicker(int PickerID, int ParentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISPicker objPicker = DB.ISPickers.SingleOrDefault(p => p.ID == PickerID && p.ParentID == ParentID);
            if (objPicker != null)
            {
                objPicker.Active = false;
                objPicker.Deleted = false;
                objPicker.DeletedBy = 1;
                objPicker.DeletedDateTime = DateTime.Now;
                DB.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public MISPICKER GetPicker(int PickerID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISPicker Obj = DB.ISPickers.SingleOrDefault(p => p.Deleted == true && p.ID == PickerID);
            if (Obj != null)
            {
                return new MISPICKER
                {
                    ID = Obj.ID,
                    ParentID = Obj.ParentID,
                    StudentID = Obj.StudentID,
                    Photo = Obj.Photo,
                    Title = Obj.Title,
                    FirstName = Obj.FirstName,
                    LastName = Obj.LastName != null ? Obj.LastName : "",
                    PickerName = Obj.FirstName + " " + (Obj.LastName != null ? Obj.LastName : ""),
                    Email = !String.IsNullOrEmpty(Obj.Email) ? Obj.Email : " Enter Email",
                    Phone = Obj.Phone,
                    StrPickerType = Obj.PickerType == 1 ? "Individual Picker" : "Organisation Picker",
                };
            }
            else
            {
                return null;
            }


        }
        public ISPicker AddUpdatePicker(int PickerID, int StudentID, int SchoolID, int ParentID, int PickerType, string Title, string FirstName, string LastName, string PhotoURL, string Email, string PhoneNo, bool OneOffPicker)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISPicker objPick = new ISPicker();
            if (PickerID != 0)
            {
                objPick = DB.ISPickers.SingleOrDefault(p => p.ID == PickerID && p.Deleted == true);
                if (objPick != null)
                {
                    objPick.SchoolID = SchoolID;
                    objPick.StudentID = StudentID;
                    objPick.ParentID = ParentID;
                    objPick.PickerType = PickerType;
                    objPick.FirstName = FirstName;
                    if (PickerType == (int)EnumsManagement.PICKERTYPE.Individual)
                    {
                        objPick.Title = Title;
                        objPick.LastName = LastName;
                        objPick.Photo = PhotoURL;
                    }

                    objPick.Email = Email;
                    objPick.Phone = PhoneNo;
                    objPick.ModifyBy = 1;
                    objPick.ModifyDateTime = DateTime.Now;
                    DB.SaveChanges();
                }

            }
            else
            {
                objPick = new ISPicker();
                objPick.SchoolID = SchoolID;
                objPick.StudentID = StudentID;
                objPick.ParentID = ParentID;
                objPick.PickerType = PickerType;
                objPick.FirstName = FirstName;
                if (PickerType == (int)EnumsManagement.PICKERTYPE.Individual)
                {
                    objPick.Title = Title;
                    objPick.LastName = LastName;
                    objPick.Photo = PhotoURL != "" ? PhotoURL : "Upload/user.jpg";
                }
                else
                {
                    objPick.Photo = "Upload/DefaultOrg.png";
                }
                objPick.Email = Email;
                objPick.Phone = PhoneNo;
                objPick.OneOffPickerFlag = OneOffPicker;
                objPick.ActiveStatus = "Active";
                objPick.Active = true;
                objPick.Deleted = true;
                objPick.CreatedBy = 1;
                objPick.CreatedDateTime = DateTime.Now;
                DB.ISPickers.Add(objPick);
                DB.SaveChanges();
                if (OneOffPicker == true)
                {
                    ISPickerAssignment obj = new ISPickerAssignment();
                    obj.PickerId = objPick.ID;
                    obj.StudentID = StudentID;
                    obj.StudentPickAssignFlag = 1;
                    obj.StudentPickAssignDate = DateTime.Now;
                    obj.RemoveChildStatus = 0;
                    obj.RemoveChildLastUpdateDate = DateTime.Now;
                    obj.PickCodayExDate = DateTime.Now;
                    DB.ISPickerAssignments.Add(obj);
                    DB.SaveChanges();
                }
            }
            return objPick;


        }

        public List<MISPickerAssignment> GetPickerAssignment(int StudentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISStudent ObjStudnt = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
            List<MISPickerAssignment> objList = new List<MISPickerAssignment>();
            if (ObjStudnt != null)
            {
                objList = (from item in DB.ISPickerAssignments.Where(p => p.StudentID == StudentID && p.RemoveChildStatus == 0 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))).ToList()
                           join item2 in DB.ISPickers.Where(p => p.Active == true && p.Deleted == true).ToList() on item.PickerId equals item2.ID
                           select new MISPickerAssignment
                           {
                               ID = item.ID,
                               StudentID = item.StudentID,
                               PickerId = item2.ID,
                               PickerName = item2.FirstName + " " + item2.LastName,
                               PickerEmail = !String.IsNullOrEmpty(item2.Email) ? item2.Email : " Enter Email",
                               PickerPhone = !String.IsNullOrEmpty(item2.Phone) ? item2.Phone : " Enter Phone Number",
                               //PickerPhone = item2.Phone,
                               PickerType = item2.PickerType,
                               AssignBy = item2.FirstName.Contains("(") ? ObjStudnt.ISSchool.Name : item.StudentAssignBy,
                               Status = item2.Active == true ? "Active" : "Inactive",
                               Photo = item2.Photo,
                               StrDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                               PickerCode = item.PickCodeExDate != null ? item.PickCodeExDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ? item.PickerCode != null ? item.PickerCode : "" : "" : "",
                               StrStudentPickAssignDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                               CurrentDate = DateTime.Now.ToString("dd/MM/yyyy"),
                               Count = false,
                               CreatedByEmail = item2.CreatedByEmail,
                               CreatedByName = item2.FirstName.Contains("(") ? ObjStudnt.ISSchool.Name : item2.CreatedByName,
                           }).OrderByDescending(p => p.PickerName.Contains("(")).ToList();
                if (StudentID != 0)
                {
                    string Dt = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    objList = objList.Where(p => p.StrDate == "").ToList();
                }
            }
            return objList;
        }
        public List<MISPickerAssignment> GetPickerAssignmentToday(int StudentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            DateTime dt = DateTime.Now.Date;
            ISStudent ObjStudnt = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Deleted == true);
            List<MISPickerAssignment> objList = new List<MISPickerAssignment>();
            if (ObjStudnt != null)
            {
                objList = (from item in DB.ISPickerAssignments.Where(p => p.StudentID == StudentID && p.RemoveChildStatus == 0 && (p.PickCodayExDate == null || DbFunctions.TruncateTime(p.PickCodayExDate) == DbFunctions.TruncateTime(DateTime.Now))).ToList()
                           join item2 in DB.ISPickers.Where(p => p.Active == true && p.Deleted == true /*&& p.SchoolID == ObjStudnt.SchoolID*/).ToList() on item.PickerId equals item2.ID
                           select new MISPickerAssignment
                           {
                               ID = item.ID,
                               PickerId = item2.ID,
                               PickerName = item2.FirstName + " " + item2.LastName,
                               PickerEmail = !String.IsNullOrEmpty(item2.Email) ? item2.Email : " Enter Email",
                               PickerPhone = !String.IsNullOrEmpty(item2.Phone) ? item2.Phone : " Enter Phone Number",
                               PickerType = item2.PickerType,
                               AssignBy = item2.FirstName.Contains("(") ? ObjStudnt.ISSchool.Name : item.StudentAssignBy,
                               Status = item2.Active == true ? "Active" : "Inactive",
                               Photo = item2.Photo,
                               StrDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                               PickerCode = item.PickerCode,
                               //PickerCode = item.PickCodeExDate != null ? item.PickCodeExDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ? 
                               // DB.ISPickups.SingleOrDefault(p => DbFunctions.TruncateTime(p.PickDate) == DbFunctions.TruncateTime(dt) 
                               // && p.PickStatus == "Picked" && p.StudentID == item.StudentID) != null ? item.PickerCode != null ? item.PickerCode : "" : "" : "" : "",
                               StrStudentPickAssignDate = item.StudentPickAssignDate != null ? item.StudentPickAssignDate.Value.ToString("dd/MM/yyyy") : "",
                               CurrentDate = DateTime.Now.ToString("dd/MM/yyyy"),
                               Count = true,
                               CreatedByName = item2.FirstName.Contains("(") ? ObjStudnt.ISSchool.Name : item2.CreatedByName
                           }).ToList();
                if (StudentID != 0)
                {
                    string Dt = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    objList = objList.Where(p => p.StrDate == Dt).ToList();
                }
            }
            return objList;
        }
        public List<MISPICKER> AllPickerByStudent(int StudentID)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            ISStudent objStudent = DB.ISStudents.SingleOrDefault(p => p.ID == StudentID && p.Active == true && p.Deleted == true);
            List<MISPICKER> ObjList = new List<MISPICKER>();
            if (objStudent != null)
            {
                ObjList = (from item in DB.ISPickers.Where(p => p.StudentID == StudentID && p.Deleted == true).ToList()/*(p.ISStudent.ParantEmail1 == objStudent.ParantEmail1 || p.ISStudent.ParantEmail2 == objStudent.ParantEmail2)*/
                           select new MISPICKER
                           {
                               ID = item.ID,
                               ParentID = item.ParentID,
                               StudentID = item.StudentID,
                               Photo = item.Photo,
                               Title = item.Title,
                               PickerName = item.FirstName + " " + item.LastName,
                               Email = item.Email,
                               Phone = item.Phone,
                               PickerType = item.PickerType,
                               ActiveStatus = item.ActiveStatus,
                               ParentName = DB.ISStudents.SingleOrDefault(p => p.ID == item.StudentID).ParantName1,
                               Active = item.Active,
                           }).ToList();
            }
            return ObjList;
        }



    }
}

