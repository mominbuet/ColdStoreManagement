using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public  class PurchaseReturnDetails
    {
       public PurchaseReturnDetails() { }
       #region Fields
       private int numReturnDID;
       private int numReturnMID;
       private int numItemID;
       private double numReturnQty;
       private int numUnitID;
       private double dbUnitPrice;
       private double dbReturnAmount;
       private string strRemarks;
       private string strColorCode;
       private string strLabdip;
       #endregion
        #region Properties
       public int ReturnDID
       {
           get { return numReturnDID; }
           set { numReturnDID = value; }
       }
       public int ReturnMID
       {
           get { return numReturnMID; }
           set { numReturnMID = value; }
       }
       public int ItemID
       {
           get { return numItemID; }
           set { numItemID = value; }
       }
       public double ReturnQty
       {
           get { return numReturnQty; }
           set { numReturnQty = value; }
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
       public double ReturnAmount
       {
           get { return dbReturnAmount; }
           set { dbReturnAmount = value; }
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
        #endregion

    }
}
