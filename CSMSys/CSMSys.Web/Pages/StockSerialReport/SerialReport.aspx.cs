using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Implementations;
namespace CSMSys.Web.Pages.StockSerialReport
{
    public partial class SerialReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int serialid = (Request.QueryString["serialid"].ToString() != "") ? int.Parse(Request.QueryString["serialid"].ToString()) : 0;
                if (serialid != 0)
                {
                    INVStockSerial invss = new SerialDAOLinq().PickByID(serialid);
                    int partyid =Convert.ToInt32( new SerialDAOLinq().GetSumByParty(@"select PartyID as smbags
from INVStockSerial where serialno='"+invss.SerialNo+"';"));
                    INVParty invp = new PartyDAOLinq().PickByID(partyid);
                    lblpartyname.Text = invp.PartyName;
                    lblpartyname.Text = invp.FatherName;
                    lblvillage.Text = invp.AreaVillageName;
                    lblcode.Text = invss.PartyCode;
                    lblbagcount.Text = invss.Bags.ToString();
                    lbldate.Text = invss.CreatedDate.ToString();
                    lblsr.Text = invss.SerialNo;
                }
            }
        }
    }
}