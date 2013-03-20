using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Stock_InOut_Detail
    {
        public Stock_InOut_Detail() { }
        #region Fields
        private int numStockDID = 0;
        private int numStockMID = 0;
        private string strTransNature = "";
        private int numItemID = 0;
        private double numInQty = 0;
        private double numOutQty = 0;
        private double dblUnitPrice = 0.0;
        private double dblInAmount = 0.0;
        private double dblOutAmount = 0.0;
        private string strRemarks = "";
        private int numShortQty = 0;
        private string strColorCode = "";
        private string strLabdip = "";
        private int nCountID;
        private int nSizeID;
        private int nColorID;
        #endregion
        #region Properties
        public int StockDID
        {
            get { return numStockDID; }
            set { numStockDID = value; }
        }
        public int StockMID
        {
            get { return numStockMID; }
            set { numStockMID = value; }
        }
        public string TransNature
        {
            get { return strTransNature; }
            set { strTransNature = value; }
        }
        public int ItemID
        {
            get { return numItemID; }
            set { numItemID = value; }
        }
        public double InQty
        {
            get { return numInQty; }
            set { numInQty = value; }
        }
        public double OutQty
        {
            get { return numOutQty; }
            set { numOutQty = value; }
        }
        public double UnitPrice
        {
            get { return dblUnitPrice; }
            set { dblUnitPrice = value; }
        }
        public double InAmount
        {
            get { return dblInAmount; }
            set { dblInAmount = value; }
        }
        public double OutAmount
        {
            get { return dblOutAmount; }
            set { dblOutAmount = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public int ShortQty
        {
            get { return numShortQty; }
            set { numShortQty = value; }
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
        public int CountID
        {
            get { return nCountID; }
            set { nCountID = value; }
        }
        public int SizeID
        {
            get { return nSizeID; }
            set { nSizeID = value; }
        }
        public int ColorID
        {
            get { return nColorID; }
            set { nColorID = value; }
        }
        #endregion
    }
}
