using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class SalesReturnDetail
    {
       public SalesReturnDetail() { }
        #region Fields
       private int nReturnDID;
       private int nReturnMID;
       private int nItemID;
       private double nReturnQty;
       private int nUnitID;
       private double dblUnitPrice;
       private double dblReturnAmount;
       private string strRemarks;
       private string strColorCode;
       private string strLabdip;
       private int nCountID;
       private int nSizeID;
       private int nColorID;
        #endregion

        #region Properties
       public int ReturnDID
       {
           get { return nReturnDID; }
           set { nReturnDID = value; }
       }
       public int ReturnMID
       {
           get { return nReturnMID; }
           set { nReturnMID = value; }
       }
       public int ItemID
       {
           get { return nItemID; }
           set { nItemID = value; }
       }
       public double ReturnQty
       {
           get { return nReturnQty; }
           set { nReturnQty = value; }
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
       public double ReturnAmount
       {
           get { return dblReturnAmount; }
           set { dblReturnAmount = value; }
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
