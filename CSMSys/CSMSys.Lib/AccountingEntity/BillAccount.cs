using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class BillAccount
    {
        public BillAccount() { }
        #region Fields
        private int numSlNo;
        private int numBillID;
        private int nAccountID;
      
        private string strParticulars;
        private double dblDebitAmount;
        private double dblCreditAmount;
        private int numPosted;
        private string strRef="";
        private string strVoucherType;
        private int numVSlNo;
        #endregion

        #region Properties
        public int SlNo
        {
            get { return numSlNo; }
            set { numSlNo = value; }
        }
        public int BillID
        {
            get { return numBillID; }
            set { numBillID = value; }
        }
        public int AccountID
        {
            get { return nAccountID; }
            set { nAccountID = value; }
        }
        
        public string Particulars
        {
            get { return strParticulars; }
            set { strParticulars = value; }
        }
        public double DebitAmount
        {
            get { return dblDebitAmount; }
            set { dblDebitAmount = value; }
        }
        public double CreditAmount
        {
            get { return dblCreditAmount; }
            set { dblCreditAmount = value; }
        }
        public int Posted
        {
            get { return numPosted; }
            set { numPosted = value; }
        }

        public string Reference
        {
            get { return strRef; }
            set { strRef = value; }
        }

        public string VoucherType
        {
            get { return strVoucherType; }
            set { strVoucherType = value; }
        }
        public int VoucherSlNo
        {
            get { return numVSlNo; }
            set { numVSlNo = value; }
        }
        #endregion
    }
}
