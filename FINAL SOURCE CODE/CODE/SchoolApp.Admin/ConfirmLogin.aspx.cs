using DocumentFormat.OpenXml.Wordprocessing;
using SchoolApp.ClassLibrary;
using SchoolApp.Database;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolApp.Admin
{
    public partial class ConfirmLogin : System.Web.UI.Page
    {
        SchoolAppEntities DB = new SchoolAppEntities();
        string userType = "";
        string answer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUserType"] != null)
            {
                userType = Convert.ToString(Session["LoggedInUserType"]);
            }

            if (!IsPostBack)
            {
                BindComboBox();
            }
        }

        private void BindComboBox()
        {
            drpFirstAns.DataSource = GetAtoZWord();
            drpFirstAns.DataBind();


            drpSecondAns.DataSource = GetAtoZWord();
            drpSecondAns.DataBind();


            drpThirdAns.DataSource = GetAtoZWord();
            drpThirdAns.DataBind();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            SetQuestionLabel();
        }

        private void SetQuestionLabel()
        {
            if (userType == "Admin")
            {
                ISAdminLogin adminUser = Session["LoggedInUser"] as ISAdminLogin;

                if (adminUser != null)
                {
                    //lblMemorableQue.Text = adminUser.MemorableQuestionList.Question;
                    answer = EncryptionHelper.Decrypt(adminUser.MemorableQueAnswer);
                }
            }
            else
            {
                ISOrganisationUser orgUser = Session["LoggedInUser"] as ISOrganisationUser;

                if (orgUser != null)
                {
                    //lblMemorableQue.Text = orgUser.MemorableQuestionList.Question;
                    answer = EncryptionHelper.Decrypt(orgUser.MemorableQueAnswer);
                }
            }

            Random random = new Random();
            List<int> randomList = new List<int>();
            bool isValid = true;
            while (isValid)
            {
                int temp = random.Next(1, answer.Length);

                if (!randomList.Contains(temp))
                    randomList.Add(temp);

                if (randomList.Count >= 3)
                    isValid = false;
            }

            if (randomList.Count > 0)
            {
                lblFirstAns.Text = $"{(randomList[0])} char";
                lblSecondAns.Text = $"{(randomList[1])} char";
                lblThirdAns.Text = $"{(randomList[2])} char";

                Session["RandomList"] = randomList;
                Session["Answer"] = answer;
            }

        }

        private string GetString(int number)
        {
            string returnString = "";

            switch (number)
            {
                case 1:
                    returnString = "First";
                    break;
                case 2:
                    returnString = "Second";
                    break;
                case 3:
                    returnString = "Third";
                    break;
                case 4:
                    returnString = "Fourth";
                    break;
                case 5:
                    returnString = "Fifth";
                    break;
                case 6:
                    returnString = "Sixth";
                    break;
                case 7:
                    returnString = "Seventh";
                    break;
                case 8:
                    returnString = "Eighth";
                    break;
            }

            return returnString;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<int> randomList = new List<int>();
            try
            {
                if (Session["RandomList"] != null)
                    randomList = Session["RandomList"] as List<int>;

                if (Session["Answer"] != null)
                    answer = Convert.ToString(Session["Answer"]);

                if (drpFirstAns.SelectedValue.ToLower() == answer[--randomList[0]].ToString().ToLower() &&
                drpSecondAns.SelectedValue.ToLower() == answer[--randomList[1]].ToString().ToLower() &&
                drpThirdAns.SelectedValue.ToLower() == answer[--randomList[2]].ToString().ToLower())
                {
                    if (userType == "Admin")
                    {
                        ISAdminLogin adminUser = Session["LoggedInUser"] as ISAdminLogin;
                        Authentication.LoggedInUser = adminUser;
                    }
                    else
                    {
                        ISOrganisationUser orgUser = Session["LoggedInUser"] as ISOrganisationUser;
                        Authentication.LoggedInOrgUser = orgUser;
                    }

                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private List<string> GetAtoZWord()
        {
            List<string> wordLIst = new List<string>();

            for (int i = 97; i <= 122; i++)
            {
                char ff = (char)i;
                wordLIst.Add(ff.ToString());
            }

            for (int j = 0; j <= 9; j++)
                wordLIst.Add(j.ToString());

            return wordLIst;
        }
    }
}