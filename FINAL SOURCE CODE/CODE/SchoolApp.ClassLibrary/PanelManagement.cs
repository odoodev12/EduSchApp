using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace SchoolApp.ClassLibrary
{
    public static class PanelManagement
    {
        public static void OpenPanel(ref HtmlGenericControl collapseTab)
        {
            collapseTab.Attributes.Add("class", "panel-collapse collapse in");
        }
        public static void ClosePanel(ref HtmlGenericControl collapseTab)
        {
            collapseTab.Attributes.Add("class", "panel-collapse collapse");
        }
    }
}
