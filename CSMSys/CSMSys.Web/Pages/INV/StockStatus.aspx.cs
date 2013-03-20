using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.DataAccessLayer.Implementations;
namespace CSMSys.Web.Pages.INV
{
    public partial class StockStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                string sql = @"SELECT SUM(bagcount) as smbags from INVParty";
                
                lblFailure.Text = "All party has " + new SerialDAOLinq().GetSumByParty(sql) + " bags on stock.";
            }
        }
        protected void checkSum(object sender, EventArgs e)
        {
            string sql = "SELECT SUM(Bagcount) as smbags from invparty where PartyCode  =N'"+txtpartyCode.Text+"';";
            string sql1 = "SELECT SUM(BagNumber) as smbags from SRVBagLoan as a,INVParty as b where a.PartyID=b.PartyID and PartyCode  =N'"+txtpartyCode.Text+"';";
            lblFailure.Text = "এই পার্টির ব‌ত‍মানে " + new SerialDAOLinq().GetSumByParty(sql) + " বস্তা স্টক এ আছে। পার্টির " + new SerialDAOLinq().GetSumByParty(sql1)+" খালি বস্তা due আছে।";

        }
        protected void ddltop_changed(object sender, EventArgs e)
        {
            dsStockSerial.SelectCommand = (ddltop.SelectedItem.Text != "All") ? @"select top " + ddltop.SelectedItem.Text + @" partycode,Areavillagename,fathername,PartyName,sum(bagcount) as smbags
from INVParty
GROUP BY PartyCode,PartyName,Areavillagename,fathername
ORDER BY sum(bagcount) desc" : @"select  partycode,PartyName,sum(bagcount) as smbags,Areavillagename,fathername
from INVParty
GROUP BY PartyCode,PartyName,Areavillagename,fathername
ORDER BY sum(bagcount) desc";

            grvStockSerial.DataBind();

        }
        protected void grvStockSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvStockSerial.PageIndex = e.NewPageIndex;
            grvStockSerial.DataBind();
        }
        
    }
}