using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Order_Master : BaseObject
    {
        public Order_Master() : base() { }
        #region Fields
        private int intOrderMID = 0;
        private string strOrderNo = "";
        private string strOrderType = "";
        private DateTime dtOrderDate = DateTime.Now;
        private int intCustomerID = 0;
        private DateTime dtDeliveryDate = DateTime.Now;
        private int intFactoryID = 0;
        private string strLedgerNo = "";
        private double intTotalOrderQty = 0;
        private int intUnitID = 0;
        private double dblOrderValue = 0.0;
        private int intCurrencyID = 0;
        private double dbRate;
        private string strBuyer_ref = "";
        #endregion

        #region Properties
        public int OrderMID
        {
            get { return intOrderMID; }
            set { intOrderMID = value; }
        }

        public string OrderNo
        {
            get { return strOrderNo; }
            set { strOrderNo = value; }
        }

        public string OrderType
        {
            get { return strOrderType; }
            set { strOrderType = value; }
        }

        public DateTime OrderDate
        {
            get { return dtOrderDate; }
            set { dtOrderDate = value; }
        }

        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }

        public DateTime DeliveryDate
        {
            get { return dtDeliveryDate; }
            set { dtDeliveryDate = value; }
        }

        public int FactoryID
        {
            get { return intFactoryID; }
            set { intFactoryID = value; }
        }

        public string LedgerNo
        {
            get { return strLedgerNo; }
            set { strLedgerNo = value; }
        }

        public double TotalOrderQty
        {
            get { return intTotalOrderQty; }
            set { intTotalOrderQty = value; }
        }

        public int UnitID
        {
            get { return intUnitID; }
            set { intUnitID = value; }
        }

        public double OrderValue
        {
            get { return dblOrderValue; }
            set { dblOrderValue = value; }
        }

        public int CurrencyID
        {
            get { return intCurrencyID; }
            set { intCurrencyID = value; }
        }
        public double Rate
        {
            get { return dbRate; }
            set { dbRate = value; }
        }
        public string Buyer_ref
        {
            get { return strBuyer_ref; }
            set { strBuyer_ref = value; }
        }
        #endregion
    }
}
