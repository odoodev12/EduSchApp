using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;

namespace SchoolApp.Web.School
{
    public partial class Dashboard1 : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.ISSchoolLogin())
            {
                Response.Redirect(Authentication.SchoolAuthorizePage());

            }
            if (!IsPostBack)
            {

            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetChartData()
        {
            List<ISPickup> data = new List<ISPickup>();
            //Here MyDatabaseEntities  is our dbContext
            //using (MyDatabaseEntities dc = new MyDatabaseEntities())
            //{
            //    data = dc.GoogleChartDatas.ToList();
            //}
            SchoolAppEntities DB = new SchoolAppEntities();
            var result = DB.GetDataForChartControl(1, "justPieChart", 2020).ToList();

            var chartData = new object[result.Count() + 1];
            chartData[0] = new object[]{
                "PickStatus",
                "Count"
            };

            int j = 0;
            foreach (var i in result)
            {
                j++;
                chartData[j] = new object[] { i.PickStatus.ToString(), i.Count };
            }
            return chartData;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetChartData1()
        {
            List<ISPickup> data = new List<ISPickup>();
            //Here MyDatabaseEntities  is our dbContext
            //using (MyDatabaseEntities dc = new MyDatabaseEntities())
            //{
            //    data = dc.GoogleChartDatas.ToList();
            //}
            SchoolAppEntities DB = new SchoolAppEntities();
            var result = DB.GetDataForChartControl(1, "multiColumnChart", 2020).ToList();

            var chartData = new object[result.Count() + 1];
            chartData[0] = new object[]{
                "Month",
                "Picked",
                "Picked(Late)",
                "Picked(Reportable)",
                "Picked(Chargeable)"

            };

            int j = 0;
            foreach (var i in result)
            {
                j++;
                chartData[j] = new object[] { i.PickStatus.ToString(), i.Count };
            }
            return chartData;
        }
    }
}