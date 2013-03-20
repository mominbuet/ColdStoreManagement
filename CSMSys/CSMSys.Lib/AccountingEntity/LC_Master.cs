using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class LC_Master : BaseObject
    {
        public LC_Master() : base() { }
        #region Feilds
        private int numLCID;
        private string strLCNo;
        private string strLCType;
        private string strMasterLCNo;
        private int numMasterLCID;
        private DateTime dtLCDate;
        private DateTime dtShipmentDate;
        private DateTime dtExpaireDate;
        private DateTime dtDocumentDate;
        private DateTime dtNegotiationDate;
        private DateTime dtUDDate;
        private DateTime dtAcceptDate;
        private string strAtSight;
        private string strFileNo;
        private int numIssuBankID;
        private int numNegotiationBankID;
        private double numTotalQty;
        private double dbTotalValue;
        private DateTime dtUnderLCdate;
        private DateTime dtActualShipmentDate;
        private DateTime dtPayDate;
        private DateTime dtActualPayDate;
        private int numCustSuppID;
        private int numCurrencyID = 0;
        private double dbRate;
        private string strLcUnit="Cone";
        private string strRemarks="";
        #endregion
        #region Properties
        public int LCID
        {
            get { return numLCID; }
            set { numLCID = value; }
        }
        public string LCNo
        {
            get { return strLCNo; }
            set { strLCNo = value; }

        }
        public string LCType
        {
            get { return strLCType; }
            set { strLCType = value; }
        }
        public string MasterLCNo
        {
            get { return strMasterLCNo; }
            set { strMasterLCNo = value; }
        }
        public int MasterLCID
        {
            get { return numMasterLCID; }
            set { numMasterLCID = value; }
        }
        public DateTime LCDate
        {
            get { return dtLCDate; }
            set { dtLCDate = value; }
        }
        public DateTime ShipmentDate
        {
            get { return dtShipmentDate; }
            set { dtShipmentDate = value; }
        }
        public DateTime ExpiredDate
        {
            get { return dtExpaireDate; }
            set { dtExpaireDate = value; }
        }
        public DateTime DocumentDate
        {
            get { return dtDocumentDate; }
            set { dtDocumentDate = value; }
        }
        public DateTime NegotiationDate
        {
            get { return dtNegotiationDate; }
            set { dtNegotiationDate = value; }
        }
        public DateTime UDDate
        {
            get { return dtUDDate; }
            set { dtUDDate = value; }
        }
        public DateTime AcceptDate
        {
            get { return dtAcceptDate; }
            set { dtAcceptDate = value; }
        }
        public string AtSight
        {
            get { return strAtSight; }
            set { strAtSight = value; }
        }
        public int IssuBankID
        {
            get { return numIssuBankID; }
            set { numIssuBankID = value; }
        }
        public string FileNo
        {
            get { return strFileNo; }
            set { strFileNo = value; }
        }
        public int NegotiationBankID
        {
            get { return numNegotiationBankID; }
            set { numNegotiationBankID = value; }
        }

        public double TotalQty
        {
            get { return numTotalQty; }
            set { numTotalQty = value; }
        }
        public double TotalValue
        {
            get { return dbTotalValue; }
            set { dbTotalValue = value; }
        }
        public DateTime UnderLCDate
        {
            get { return dtUnderLCdate; }
            set { dtUnderLCdate = value; }
        }
        public DateTime ActualShipmentDate
        {
            get { return dtActualShipmentDate; }
            set { dtActualShipmentDate = value; }
        }

        public DateTime PaymentDate
        {
            get { return dtPayDate; }
            set { dtPayDate = value; }
        }
        public DateTime ActualPaymentDate
        {
            get { return dtActualPayDate; }
            set { dtActualPayDate = value; }
        }
        public int CustomerSupplierID
        {
            get { return numCustSuppID; }
            set { numCustSuppID = value; }
        }

        public int CurrencyID
        {
            get { return numCurrencyID; }
            set { numCurrencyID = value; }
        }

        public double Rate
        {
            get { return dbRate; }
            set { dbRate = value; }
        }

        public string LcUnit
        {
            get { return strLcUnit; }
            set { strLcUnit = value; }
        }
        public string LCDescription
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        #endregion

    }
}
