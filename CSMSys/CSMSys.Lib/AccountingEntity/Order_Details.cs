using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Order_Details
    {
        public Order_Details() { }

        #region Fields
        private int intOrderDID = 0;
        private int intOrderMID = 0;
        private double intOrderQty = 0;
        private int intItemID = 0;
        private int intUnitID = 0;
        private double dblUnitPrice = 0.0;
        private double dblOrderValue = 0.0;
        private int intPriceID = 0;
        private string strColorCode = "";
        private string strLabdip = "";
        private string strRemarks = "";
        private int numCountID;
        private int numSizeID;
        private int numColorID;
        #endregion

        #region Properties
        public int OrderDID
        {
            get { return intOrderDID; }
            set { intOrderDID = value; }
        }

        public int OrderMID
        {
            get { return intOrderMID; }
            set { intOrderMID = value; }
        }

        public double OrderQty
        {
            get { return intOrderQty; }
            set { intOrderQty = value; }
        }

        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }

        public int UnitID
        {
            get { return intUnitID; }
            set { intUnitID = value; }
        }

        public int PriceID
        {
            get { return intPriceID; }
            set { intPriceID = value; }
        }

        public double UnitPrice
        {
            get { return dblUnitPrice; }
            set { dblUnitPrice = value; }
        }

        public double OrderValue
        {
            get { return dblOrderValue; }
            set { dblOrderValue = value; }
        }
        public string ColorCode
        {
            get { return strColorCode; }
            set { strColorCode = value; }
        }
        public string Labdip
        {
            get { return strLabdip; }
            set { strLabdip = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public int CountID
        {
            get { return numCountID; }
            set { numCountID = value; }
        }
        public int ColorID
        {
            get { return numColorID; }
            set { numColorID = value; }
        }
        public int SizeID
        {
            get { return numSizeID; }
            set { numSizeID = value; }
        }
        #endregion
    }
}
