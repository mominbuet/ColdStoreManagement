using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class Purchases_Invoice_DTL
    {
       public Purchases_Invoice_DTL() { }
        #region Fields
       private int numSLNO;
       private int numInvoiceID;
       private int numOrderID;
       private int numItemID;
       private double numInvQty;
       private int numUnitID;
       private double dbUnitPrice;
       private double dbPriceAmmount;
       private string strRemarks;
       private string strColorCode;
       private string strLabdip;
       private int nCountID;
       private int nSizeID;
       private int nColorID;
        #endregion

        #region Properties
       public int InvoiceID
       {
           get { return numInvoiceID; }
           set { numInvoiceID = value; }

       }
       public int SLNO
       {
           get { return numSLNO; }
           set { numSLNO = value; }
       }
       public int OrderID
       {
           get { return numOrderID; }
           set { numOrderID = value; }
       }
       public int ItemID
       {
           get { return numItemID; }
           set { numItemID = value; }
       }
       public double InvQty
       {
           get { return numInvQty; }
           set { numInvQty = value; }
       }
       public int UnitID
       {
           get { return numUnitID; }
           set { numUnitID = value; }
       }
       public double UnitPrice
       {
           get { return dbUnitPrice; }
           set { dbUnitPrice = value; }
       }
       public double PriceAmmount
       {
           get { return dbPriceAmmount; }
           set { dbPriceAmmount = value; }
       }
       public string Remarks
       {
           get { return strRemarks; }
           set { strRemarks = value; }
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
