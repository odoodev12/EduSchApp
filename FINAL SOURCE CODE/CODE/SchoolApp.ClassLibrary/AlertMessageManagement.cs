using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SchoolApp.ClassLibrary
{
    public static class AlertMessageManagement
    {
        public static void ServerMessage(Page page, string Message, int OP)
        {
            String Operation = "";

            if (OP == 1) { Operation = "success"; }
            if (OP == 2) { Operation = "error"; }
            if (OP == 3) { Operation = "info"; }
            if (OP == 4) { Operation = "warning"; }
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "myscript", "setTimeout(function() {toastr.options = {closeButton: true,positionClass: 'toast-bottom-right', progressBar: true,showMethod: 'slideDown',timeOut: 4000};toastr." + Operation + "('" + Message + "', '" + Operation + "');}, 500);", true);
        }
        public static void ExcuteJScript(Page page, string Scirpt)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "JsScript", Scirpt, true);
        }
        public enum MESSAGETYPE
        {
            Success = 1,
            Error = 2,
            info = 3,
            warning = 4
        }
    }
}
