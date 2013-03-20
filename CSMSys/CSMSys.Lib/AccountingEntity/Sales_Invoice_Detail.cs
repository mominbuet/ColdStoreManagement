using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Sales_Invoice_Detail
    {
        public Sales_Invoice_Detail() { }
        #region Fields
        private int numSLNo = 0;
        private int nInvoiceID = 0;
        private int nOrderID = 0;
        private int nItemID = 0;
        private double nInvQty = 0;
        private int nUnitID = 0;
        private double dblUnitPrice = 0.0;
        private double dblPriceAmount = 0.0;
        private string strRemarks = "";
        private string strColorcode;
        private string strLabdip;
        private int nCountID;
        private int nSizeID;
        private int nColorID;

        #endregion

        #region Properties
        public int SLNo
        {
            get { return numSLNo; }
            set { numSLNo = value; }
        }
        public int InvoiceID
        {
            get { return nInvoiceID; }
            set { nInvoiceID = value; }
        }
        public int OrderID
        {
            get { return nOrderID; }
            set { nOrderID = value; }
        }
        public int ItemID
        {
            get { return nItemID; }
            set { nItemID = value; }
        }
        public double InvQty
        {
            get { return nInvQty; }
            set { nInvQty = value; }
        }
        public int UnitID
        {
            get { return nUnitID; }
            set { nUnitID = value; }
        }
        public double UnitPrice
        {
            get { return dblUnitPrice; }
            set { dblUnitPrice = value; }
        }
        public double PriceAmount
        {
            get { return dblPriceAmount; }
            set { dblPriceAmount = value; }
        }
        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }
        public string ColorCode
        {

            get { return strColorcode; }
            set { strColorcode = value; }
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
