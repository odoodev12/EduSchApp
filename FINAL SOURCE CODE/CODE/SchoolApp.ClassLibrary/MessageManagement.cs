using SchoolApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SchoolApp.ClassLibrary
{
    public  class MessageManagement
    {
        public List<MISMessage> ReceivedMessageList(int ID, int type)
        {
            SchoolAppEntities DB = new SchoolAppEntities();            
            List<MISMessage> objList = (from item in DB.ISMessages.Where(p => p.ReceiveID==ID && p.ReceiverType==type && p.Active == true && p.Deleted == true).ToList()
                                        select new MISMessage
                                        {
                                            ID = item.ID,
                                            SchoolID = item.SchoolID,
                                            STime = item.Time.ToString(),
                                            Subject = item.Title,
                                            Sender = GetUserName(item.SendID.Value, item.SenderType.Value),
                                            Description = item.Desc,
                                            Receiver= GetUserName(item.ReceiveID.Value, item.ReceiverType.Value),
                                            ReceiverType=item.ReceiverType,
                                            SenderType=item.SenderType,
                                            ReceiveID=item.ReceiveID,
                                            SendID=item.SendID
                                            
                                        }).ToList();


            return objList;
        }
        public List<MISMessage> SentMessageList(int ID, int type)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            List<MISMessage> objList = (from item in DB.ISMessages.Where(p => p.SendID == ID && p.SenderType== type && p.Active == true && p.Deleted == true).ToList()
                                        select new MISMessage
                                        {
                                            ID = item.ID,
                                            SchoolID = item.SchoolID,
                                            STime = item.Time.ToString(),
                                            Subject = item.Title,
                                            Sender = GetUserName(item.SendID.Value, item.SenderType.Value),
                                            Description = item.Desc,
                                            Receiver = GetUserName(item.ReceiveID.Value, item.ReceiverType.Value),
                                            ReceiverType = item.ReceiverType,
                                            SenderType = item.SenderType,
                                            ReceiveID = item.ReceiveID,
                                            SendID = item.SendID
                                        }).ToList();


            return objList;
        }
        public string GetUserName(int ID,int Type)
        {
            SchoolAppEntities DB = new SchoolAppEntities();
            if ((int)EnumsManagement.MESSAGEUSERTYPE.School == Type)
            {
                ISSchool obj = DB.ISSchools.SingleOrDefault(p => p.ID == ID);
                if(obj!=null)
                {
                    return string.Format("{0} {1}", obj.AdminFirstName, obj.AdminLastName);
                }
            }
            else if((int)EnumsManagement.MESSAGEUSERTYPE.Teacher== Type)
            {
                ISTeacher obj = DB.ISTeachers.SingleOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    return string.Format("{0}", obj.Name);
                }
            }
            else if ((int)EnumsManagement.MESSAGEUSERTYPE.Parent == Type)
            {
                ISStudent obj = DB.ISStudents.SingleOrDefault(p => p.ID == ID);
                if (obj != null)
                {
                    return string.Format("{0}", obj.ParantName1);
                }
            }
            else
            {
                return "";
            }
            return "";
        }
    }
}
