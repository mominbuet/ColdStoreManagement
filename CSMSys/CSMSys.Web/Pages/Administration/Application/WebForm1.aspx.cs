using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CSMSys.Web.Pages.Administration.Application
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected Label m_TimeLabel;

        protected void Page_Load(object sender, EventArgs e)
        {
            // create an update panel
            UpdatePanel updatepanel = new UpdatePanel();

            // add the update panel to the placeholder
            LocalPlaceHolder.Controls.Add(updatepanel);

            // create a label to show time
            m_TimeLabel = new Label();
            m_TimeLabel.Text = DateTime.Now.ToString();

            // add the label to the update panel
            updatepanel.ContentTemplateContainer.Controls.Add(m_TimeLabel);

            // create a drop down list
            DropDownList dropdown = new DropDownList();
            dropdown.ID = "Dropdown1";
            dropdown.AutoPostBack = true; // this is absolutely required
            dropdown.Items.Add("Item 1");
            dropdown.Items.Add("Item 2");
            dropdown.Items.Add("Item 3");
            dropdown.Items.Add("Item 4");

            // add the drop down list to the update panel
            updatepanel.ContentTemplateContainer.Controls.Add(dropdown);

            // create a trigger
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();

            // associate the trigger with the drop down
            trigger.ControlID = "Dropdown1";
            trigger.EventName = "SelectedIndexChanged";

            // add the trigger to the update panel
            updatepanel.Triggers.Add(trigger);
        }

        protected void Dropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // event to handle drop down change
            m_TimeLabel.Text = DateTime.Now.ToString();
        }
    }
}