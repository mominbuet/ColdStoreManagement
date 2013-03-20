using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class TransactionDetail :BaseObject
    {
        public TransactionDetail() : base() { }

        #region Fields
        private int numTransDID;
        private int numTransMID;
        private int numAccountID;
        private double dblDebitAmt;
        private double dblCreditAmt;
        private string strComments;
        #endregion

        #region Properties
        public int TransactionDetailID
        {
            get { return numTransDID; }
            set { numTransDID = value; }
        }
        public int TransactionMasterID
        {
            get { return numTransMID; }
            set { numTransMID = value; }
        }
        public int TransactionAccountID
        {
            get { return numAccountID; }
            set { numAccountID = value; }
        }
        public double DebitAmount
        {
            get { return dblDebitAmt; }
            set { dblDebitAmt = value; }
        }
        public double CreditAmount
        {
            get { return dblCreditAmt; }
            set { dblCreditAmt = value; }
        }
        public string Comments
        {
            get { return strComments; }
            set { strComments = value; }
        }
        #endregion
    }
}
