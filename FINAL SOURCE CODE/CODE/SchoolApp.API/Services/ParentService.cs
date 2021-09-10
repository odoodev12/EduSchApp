using SchoolApp.API.Models;
using SchoolApp.API.Models.ViewModels;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolApp.API.Services
{
    public class ParentService
    {
        public SchoolAppEntities entity;

        public ParentService()
        {
            entity = new SchoolAppEntities();
            entity.Configuration.ProxyCreationEnabled = false;
        }

        #region Child Picker
        public ReturnResponce GetPickerList()
        {
            try
            {
                var response = entity.ISPickers.ToList();
                return new ReturnResponce(response, EntityJsonIgnore.PickerIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce AddUpdatePicker(ISPicker iSPicker)
        {
            try
            {
                ISPicker objPicker = new ISPicker();
                if (String.IsNullOrEmpty(iSPicker.Email))
                {
                    return new ReturnResponce("Email must be entered to contact this account in the event of pickup emergency.");
                }
                else
                {
                    if (iSPicker.ID == 0)
                    {
                        List<ISPicker> _Picker = entity.ISPickers.Where(p => p.StudentID == iSPicker.StudentID && p.Deleted == true).ToList();

                        bool isFLNameExist = _Picker.Any(r => (string.Compare(r.FirstName, iSPicker.FirstName, true) == 0 && string.Compare(r.LastName, iSPicker.LastName, true) == 0));
                        bool isEmailExist = _Picker.Any(r => string.Compare(r.Email, iSPicker.Email, true) == 0);
                        bool isBlankEmailIdFound = _Picker.Any(r => r.Email.Length == 0);

                        if (isFLNameExist && iSPicker.Email.Length == 0 && isBlankEmailIdFound)
                        {
                            return new ReturnResponce("Picker with the Same name already exist please enter another name");
                        }
                        else if (iSPicker.Email != "" && isEmailExist)
                        {
                            return new ReturnResponce("Picker with the Same Email already exist please enter another email");
                        }
                        else
                        {
                            ISStudent _Student = entity.ISStudents.SingleOrDefault(p => p.ID == iSPicker.StudentID && p.Deleted == true);

                            objPicker.PickerType = iSPicker.PickerType;
                            objPicker.SchoolID = iSPicker.SchoolID;
                            objPicker.ParentID = iSPicker.ParentID;
                            objPicker.StudentID = iSPicker.StudentID;
                            objPicker.Title = iSPicker.Title == "1" ? iSPicker.Title != "0" ? iSPicker.Title : "" : "";
                            objPicker.FirstName = iSPicker.PickerType == 1 ? iSPicker.FirstName : "";
                            objPicker.LastName = iSPicker.LastName;
                            if (iSPicker.PickerType == 1)
                            {
                                ///Image Upload code heare
                            }
                            else
                            {
                                ///Image Upload code heare
                            }
                            objPicker.Email = iSPicker.Email;
                            objPicker.EmergencyContact = !String.IsNullOrEmpty(iSPicker.Email) ? iSPicker.EmergencyContact == true ? true : false : false;
                            objPicker.Phone = iSPicker.Phone;
                            objPicker.OneOffPickerFlag = iSPicker.OneOffPickerFlag == true ? true : false;
                            objPicker.ActiveStatus = "Active";
                            objPicker.Active = true;
                            objPicker.Deleted = true;
                            objPicker.CreatedBy = iSPicker.ParentID;
                            objPicker.CreatedByEmail = iSPicker.CreatedByEmail;
                            objPicker.CreatedDateTime = DateTime.Now;
                            objPicker.CreatedByName = iSPicker.CreatedByName;
                            entity.ISPickers.Add(objPicker);
                            entity.SaveChanges();

                            if (iSPicker.OneOffPickerFlag == true)
                            {
                                ISPickerAssignment obj = new ISPickerAssignment();
                                obj.PickerId = objPicker.ID;
                                obj.StudentID = iSPicker.StudentID;
                                obj.StudentPickAssignFlag = 1;
                                obj.RemoveChildStatus = 0;
                                obj.RemoveChildLastUpdateDate = DateTime.Now;
                                obj.PickCodayExDate = DateTime.Now;
                                obj.StudentAssignBy = iSPicker.CreatedByName;
                                entity.ISPickerAssignments.Add(obj);
                                entity.SaveChanges();
                            }

                        }
                    }
                    else
                    {
                         objPicker = entity.ISPickers.SingleOrDefault(p => p.ID == iSPicker.ID && p.Deleted == true);
                        objPicker.PickerType = iSPicker.PickerType;
                        objPicker.SchoolID = iSPicker.SchoolID;
                        objPicker.ParentID = iSPicker.ParentID;
                        objPicker.StudentID = iSPicker.StudentID;
                        objPicker.Title = iSPicker.Title == "1" ? iSPicker.Title != "0" ? iSPicker.Title : "" : "";
                        objPicker.FirstName = iSPicker.PickerType == 1 ? iSPicker.FirstName : "";
                        objPicker.LastName = iSPicker.LastName;
                        if (iSPicker.PickerType == 1)
                        {
                            ///Image Upload code heare
                        }
                        else
                        {
                            ///Image Upload code heare
                        }
                        objPicker.Email = iSPicker.Email;
                        objPicker.EmergencyContact = !String.IsNullOrEmpty(iSPicker.Email) ? iSPicker.EmergencyContact == true ? true : false : false;
                        objPicker.Phone = iSPicker.Phone;
                        objPicker.OneOffPickerFlag = iSPicker.OneOffPickerFlag == true ? true : false;
                        objPicker.ActiveStatus = "Active";
                        objPicker.Active = true;
                        objPicker.Deleted = true;
                        objPicker.ModifyBy = iSPicker.ModifyBy;
                        objPicker.ModifyDateTime = DateTime.Now;                    
                        entity.SaveChanges();                        
                    }
                   
                    return new ReturnResponce(objPicker, EntityJsonIgnore.PickerIgnore);
                }
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        public ReturnResponce DeletePicker(int PickerId , int LoginId , string LoginName)
        {
            try
            {
               var  objPicker = entity.ISPickers.SingleOrDefault(p => p.ID == PickerId && p.Deleted == true);

                objPicker.Deleted = false;
                objPicker.Deleted = true;
                objPicker.DeletedBy = LoginId;
                objPicker.CreatedDateTime = DateTime.Now;
                objPicker.DeletedByName = LoginName;
                entity.SaveChanges();
                return new ReturnResponce(objPicker , EntityJsonIgnore.PickerIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public ReturnResponce UpdateAssign(int PickerAssignId , int LoginId , string LoginName,ISPickerAssignment iSPickerAssignment)
        {
            try
            {
                var obj = entity.ISPickerAssignments.SingleOrDefault(p => p.ID == PickerAssignId);
                obj.PickerId = iSPickerAssignment.ID;
                obj.StudentID = LoginId;
                obj.StudentPickAssignFlag = 1;
                obj.RemoveChildStatus = 0;
                obj.RemoveChildLastUpdateDate = DateTime.Now;
                obj.PickCodayExDate = DateTime.Now;
                obj.StudentAssignBy = LoginName;               
                entity.SaveChanges();
                return new ReturnResponce(obj, EntityJsonIgnore.PickerIgnore);
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }


        #endregion

        #region  Daily Pickup Status
        public ReturnResponce GetPckupDailyStatus(DailyPickupReportRequest dailyPickupReportRequest , int LoginId)
        {
            try
            {
                int ID = LoginId;
                int StudentIDs = Convert.ToInt32(dailyPickupReportRequest.StudentID);                
                var response = DailyPickUpStatus(LoginId , dailyPickupReportRequest.Date, StudentIDs, ID, dailyPickupReportRequest.PickerID, dailyPickupReportRequest.PickupStatus, dailyPickupReportRequest.Orderby, dailyPickupReportRequest.SortByStudentID);

                return new ReturnResponce(response, new[] { "" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }
        public List<MViewStudentPickUp> DailyPickUpStatus(int LoginId,string date, int StudentID, int StudID, string PickerID, string Status, string OrderBy, string SortBy)
        {
            DateTime dt = DateTime.Now;
            if (date != null && date != "")
            {
                string dates = date;
                string Format = "";
                if (dates.Contains("/"))
                {
                    string[] arrDate = dates.Split('/');
                    Format = arrDate[1].ToString() + "/" + arrDate[0].ToString() + "/" + arrDate[2].ToString();
                }
                else
                {
                    Format = dates;
                }
                dt = Convert.ToDateTime(Format);
            }
            int ID = StudID;
            ISStudent objSt = entity.ISStudents.OrderByDescending(p => p.ID).FirstOrDefault(p => p.ID == ID && p.Active == true && p.Deleted == true);
            List<MViewStudentPickUp> objList = new List<MViewStudentPickUp>();
            List<MViewStudentPickUp> objList2 = new List<MViewStudentPickUp>();
            if (objSt != null)
            {
                List<ISHoliday> objHoliday = entity.ISHolidays.Where(p => p.SchoolID == objSt.SchoolID && p.Active == true).ToList();
                objList = (from item in entity.getPickUpData(dt).Where(p => p.Deleted == true && (p.StartDate == (DateTime?)null || p.StartDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")))/*ViewStudentPickUps.Where(p => p.Deleted == true && (p.ParantEmail1 == objStudent.ParantEmail1 || p.ParantEmail2 == objStudent.ParantEmail2)).ToList()*/
                           select new MViewStudentPickUp
                           {
                               ID = item.ID == null ? 0 : item.ID,
                               StudentID = item.StudID,
                               StudentName = item.StudentName,
                               StudentPic = item.Photo,
                               PickStatus = String.IsNullOrEmpty(item.PickStatus) ? (objHoliday.Where(p => p.DateFrom.Value.Date <= dt.Date && p.DateTo.Value.Date >= dt.Date).Count() > 0) ? "School Closed" : OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                               //PickStatus = item.PickStatus == null ? "Not Marked" : item.PickStatus,
                               Pick_Time = item.PickTime == null ? "" : item.PickTime.Value.ToString("HH:mm tt"),
                               Pick_Date = item.PickDate == null ? null : item.PickDate.Value.ToString("dd/MM/yyyy"),
                               PickerID = item.PickerID == null ? 0 : item.PickerID,
                               PickerName = item.PickerID == null ? "" : entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).FirstName + " " + entity.ISPickers.SingleOrDefault(p => p.ID == item.PickerID).LastName,
                               Status = item.PickStatus == null ? OperationManagement.GetDefaultPickupStatus() : item.PickStatus,
                               ClassID = item.ClassID,
                               ParantEmail1 = item.ParantEmail1,
                               ParantEmail2 = item.ParantEmail2,
                               SchoolName = item.SchoolID != null ? entity.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "",
                               SchoolType = item.SchoolID != null ? entity.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).TypeID.Value : 2,
                               StartDates = item.StartDate != (DateTime?)null ? item.StartDate.Value.ToString("dd/MM/yyyy") : "",
                               AfterSchoolName = item.StartDate != (DateTime?)null ? entity.ISSchools.SingleOrDefault(p => p.ID == item.SchoolID).Name : "",
                           }).ToList();
                objList = objList.Where(p => String.IsNullOrEmpty(p.StartDates) || p.StartDates == DateTime.Now.ToString("dd/MM/yyyy")).ToList();

                if (StudID != 0)
                {
                    ISStudent objStudent = entity.ISStudents.SingleOrDefault(p => p.ID == ID);
                    
                    //objList = objList.Where(p => p.ParantEmail1 == (entity.) || p.ParantEmail2 == Authentication.LoginParentEmail).GroupBy(r => r.StudentID).Select(r => r.First()).ToList();
                }
            }
            if (StudentID != 0)
            {
                objList = objList.Where(p => p.StudentID == StudentID).ToList();
            }
            if (PickerID != "")
            {
                objList = objList.Where(p => p.PickerName == PickerID).ToList();
            }
            if (Status != "")
            {
                objList = objList.Where(p => Status == "Send to After School" ? p.PickStatus.Contains("After-School") : p.PickStatus == Status).ToList();

            }
            objList2 = objList.ToList();
            foreach (var items in objList)
            {

                if (items.SchoolType == (int)EnumsManagement.SCHOOLTYPE.AfterSchool)
                {
                    ISAttendance _Attendence = entity.ISAttendances.SingleOrDefault(p => p.StudentID == items.StudentID && DbFunctions.TruncateTime(p.Date.Value) == DbFunctions.TruncateTime(DateTime.Now) && p.Active == true && p.Status.Contains("Present"));
                    if (_Attendence == null)
                    {
                        objList2.Remove(items);
                    }
                }
            }
            objList = objList2.ToList();
            if (OrderBy == "ASC")
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderBy(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderBy(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderBy(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderBy(p => p.PickDate).ToList();
                }
            }
            else
            {
                if (SortBy == "ChildName")
                {
                    objList = objList.OrderByDescending(p => p.StudentName).ToList();
                }
                else if (SortBy == "Status")
                {
                    objList = objList.OrderByDescending(p => p.Status).ToList();
                }
                else if (SortBy == "Picker")
                {
                    objList = objList.OrderByDescending(p => p.PickerName).ToList();
                }
                else
                {
                    objList = objList.OrderByDescending(p => p.PickDate).ToList();
                }
            }
            return objList;
        }
        #endregion

        #region PickupReport
        public ReturnResponce GetPickupReport()
        {
            try
            {
                int ID = Authentication.LogginParent.ID;
                    List<MISStudent> response = (from item in entity.ISStudents.Where(p => (p.ParantEmail1.ToLower() == Authentication.LoginParentEmail.ToLower() || p.ParantEmail2.ToLower() == Authentication.LoginParentEmail.ToLower())
                                              && p.StartDate == null && p.Active == true && p.Deleted == true).ToList()
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
                                              }).OrderByDescending(r => r.TypeID).ToList();
                    return new ReturnResponce(response, new[] { "" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
            }
        }

        #endregion

        #region Notification
        public ReturnResponce GetNotification()
        {
            try
            {
                var responce = entity.Notifactions.ToList();
                return new ReturnResponce(responce, new[] { "" });
            }
            catch (Exception ex)
            {
                return new ReturnResponce(ex.Message);
                throw;
            }
        }
        #endregion
    }
}