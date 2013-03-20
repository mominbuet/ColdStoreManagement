using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class PI_Master : BaseObject
    {
        public PI_Master() : base() { }

        #region Fields
        private int intPIMID;
        private string strPINO;
        private DateTime dtPIDate = DateTime.Now;
        private string strAttention;
        private int intFactoryID;
        private string strPIType;
        private int numCustSuppID;
        private int numCurrencyID = 0;
        private double dbRate;
        private string termsCond="";
        #endregion

        #region Properties
        public int PIMID
        {
            get { return intPIMID; }
            set { intPIMID = value; }
        }

        public string PINO
        {
            get { return strPINO; }
            set { strPINO = value; }
        }

        public DateTime PIDate
        {
            get { return dtPIDate; }
            set { dtPIDate = value; }
        }



        public int FactoryID
        {
            get { return intFactoryID; }
            set { intFactoryID = value; }
        }
        public string Attention
        {
            get { return strAttention; }
            set { strAttention = value; }
        }




        public string PIType
        {
            get { return strPIType; }
            set { strPIType = value; }
        }
        public int CustomerOrSupplierID
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
        public string TermsCondition
        {
            get { return termsCond; }
            set { termsCond = value; }
        }
        #endregion
    }
}
