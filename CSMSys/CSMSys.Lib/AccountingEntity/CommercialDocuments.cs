using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class CommercialDocuments : BaseObject
    {
        public CommercialDocuments(): base() { }
        #region Fields
        private int nComInvoiceID = 0;
        private string strComInvoiceNo = "";
        private DateTime dtComInvoiceDate = DateTime.Now;
        private string strNotifyParty = "";
        private string strFromLocation = "";
        private string strToLocation = "";
        private string strExpNo = "";
        private DateTime dtExpDate = DateTime.Now;
        private string strCarrier = "";
        private string strCarrierNo = "";
        private int numLCID = 0;
        private string strOriginCountry = "";
        private double numTotalQty = 0;
        private double dblTotalValue = 0.0;
        private string strPackingListNo = "";
        private DateTime dtPackingDate = DateTime.Now;
        private string strCONo = "";
        private DateTime dtCODate = DateTime.Now;
        private string strChalanNo = "";
        private DateTime dtChalanDate = DateTime.Now;
        private string strVATerIDNo = "";
        private string strRemarks = "";
        private DateTime dtSubmitDate = new DateTime(1900, 1, 1);
        private string strDraftNo;
        private string strBankRefNo;
        private string strMasterLCTitle;
        private string strFactoryAddress;
        private string strAuthority;
        private string strTermsCondition;
        private string strPackingListStatus;
        private int numIsEPZ;

        #endregion
        #region Properties
        public int ComInvoiceID
        {
            get { return nComInvoiceID; }
            set { nComInvoiceID = value; }
        }
        public string ComInvoiceNo
        {
            get { return strComInvoiceNo; }
            set { strComInvoiceNo = value; }
        }
        public DateTime ComInvoiceDate
        {
            get { return dtComInvoiceDate; }
            set { dtComInvoiceDate = value; }
        }
        public string NotifyParty
        {
            get { return strNotifyParty; }
            set { strNotifyParty = value; }
        }
        public string FromLocation
        {
            get { return strFromLocation; }
            set { strFromLocation = value; }
        }
        public string ToLocation
        {
            get { return strToLocation; }
            set { strToLocation = value; }
        }
        public string ExpNo
        {
            get { return strExpNo; }
            set { strExpNo = value; }
        }
        public DateTime ExpDate
        {
            get { return dtExpDate; }
            set { dtExpDate = value; }
        }
        public string Carrier
        { get { return strCarrier; }
            set { strCarrier = value; }
        }
        public string CarrierNo
        {
            get { return strCarrierNo; }
            set { strCarrierNo = value; }
        }
        public int LCID
        {
            get { return numLCID; }
            set { numLCID = value; }
        }
        public string OriginCountry
        {
            get { return strOriginCountry; }
            set { strOriginCountry = value; }
        }
        public double TotalQty
        {
            get { return numTotalQty; }
            set { numTotalQty = value; }
        }
        public double TotalValue
        {
            get { return dblTotalValue; }
            set { dblTotalValue = value; }
        }
        public string PackingListNo
        {
            get { return strPackingListNo; }
            set { strPackingListNo = value; }
        }
        public DateTime PackingDate
        {
            get { return dtPackingDate; }
            set { dtPackingDate = value; }
        }
        public string CONo
        {
            get { return strCONo; }
            set { strCONo = value; }
        }
        public DateTime CODate
        {
            get { return dtCODate; }
            set { dtCODate = value; }
        }
        public string ChalanNo
        { get { return strChalanNo; }
            set { strChalanNo = value; }
        }
        public DateTime ChalanDate
        {
            get { return dtChalanDate; }
            set { dtChalanDate = value; }
        }
        public string VATerIDNo
        {
            get { return strVATerIDNo; }
            set { strVATerIDNo = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public DateTime SubmitDate
        {
            get { return dtSubmitDate; }
            set { dtSubmitDate = value; }
        }
        public string DraftNo
        {
            get { return strDraftNo; }
            set { strDraftNo = value; }
        }
        public string BankRefNo
        {
            get { return strBankRefNo; }
            set { strBankRefNo = value; }
        }
        public string MasterLCTitle
        {
            get { return strMasterLCTitle; }
            set { strMasterLCTitle = value; }
        }
        public string FactoryAddress
        {
            get { return strFactoryAddress; }
            set { strFactoryAddress = value; }
        }
        public string Authority
        {
            get { return strAuthority; }
            set { strAuthority = value; }
        }
        public string LCTermsCondition
        {
            get { return strTermsCondition; }
            set { strTermsCondition = value; }
        }
        public string PackingListStatus
        {
            get { return strPackingListStatus; }
            set { strPackingListStatus = value; }
        }
        public int IsEPZ
        {
            get { return numIsEPZ; }
            set { numIsEPZ = value; }
        }
        #endregion
    }
}
