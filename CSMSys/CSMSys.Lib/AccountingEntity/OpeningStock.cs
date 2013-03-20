using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class OpeningStock : BaseObject
    {
       public OpeningStock() : base() { }

       private int numOpID;
       private int numFiscalYearID;
       private int numCustomerID;
       private int numItemID;
       private int numUnitID;
       private double numOpQty;
       private double dblUnitPrice;
       private double dblOpAmt;
       private DateTime dtOpDate;
       private double dblDRate;
       private string strColorCode = "";
       private string strLabdip = "";
       private string strRemarks = "";
       private int nCountID;
       private int nSizeID;
       private int nColorID;
       private int nCurrencyID;

       public int OpeningID
       {
           get { return numOpID; }
           set { numOpID=value; }
       }
       public int FiscalYearID
       {
           get { return numFiscalYearID; }
           set { numFiscalYearID=value; }
       }
       public int CustomerID
       {
           get { return numCustomerID; }
           set { numCustomerID=value; }
       }
       public int ItemID
       {
           get { return numItemID; }
           set { numItemID=value; }
       }
       public int UnitID
       {
           get { return numUnitID; }
           set { numUnitID=value; }
       }
       public double OpeningQuantity
       {
           get { return numOpQty; }
           set { numOpQty=value; }
       }
       public double UnitPrice
       {
           get { return dblUnitPrice; }
           set { dblUnitPrice=value; }
       }
       public double OpeningAmount
       {
           get { return dblOpAmt; }
           set { dblOpAmt=value; }
       }
       public DateTime OpeningDate
       {
           get { return dtOpDate; }
           set { dtOpDate=value; }
       }
       public double DRate
       {
           get { return dblDRate; }
           set { dblDRate = value; }
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
       public int CurrencyID
       {
           get { return nCurrencyID; }
           set { nCurrencyID = value; }
       }
    }
}
