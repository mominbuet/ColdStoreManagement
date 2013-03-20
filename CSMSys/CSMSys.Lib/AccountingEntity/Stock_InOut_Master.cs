using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Stock_InOut_Master : BaseObject
    {
        public Stock_InOut_Master() : base() { }
        #region Fields
        private int numStockMID = 0;
        private string strTransType = "";
        private DateTime dtTransDate = DateTime.Now;
        private string strVoucherNo = "";
        private string strChalanNo = "";
        private DateTime dtChalanDate = DateTime.Now;
        private int nCustSupplID = 0;
        private int numRefID = 0;
        private string strRemarks = "";
        private string strModule = "";
        #endregion
        #region Properties
        public int StockMID
        {
            get { return numStockMID; }
            set { numStockMID = value; }
        }
        public string TransType
        {
            get { return strTransType; }
            set { strTransType = value; }
        }
        public DateTime TransDate
        {
            get { return dtTransDate; }
            set { dtTransDate = value; }
        }
        public string VoucherNo
        {
            get { return strVoucherNo; }
            set { strVoucherNo = value; }
        }
        public string ChalanNo
        {
            get { return strChalanNo; }
            set { strChalanNo = value; }
        }
        public DateTime ChalanDate
        {
            get { return dtChalanDate; }
            set { dtChalanDate = value; }
        }
        public int CustSupplID
        {
            get { return nCustSupplID; }
            set { nCustSupplID = value; }
        }
        public int RefID
        {
            get { return numRefID; }
            set { numRefID = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        public string Module
        {
            get { return strModule; }
            set { strModule = value; }
        }
        #endregion
    }
}
