using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class PriceList
    {
       public PriceList() { }

       private int numPriceID;
       private int objCustomerID;
       private int numItemID;
       private int numUnitID;
       private double dblPrice;
       private double dblVAT;
       private DateTime dtSetupDate;
       private string strRemarks;
       private int numCountID;
       private int numSizeID;
       private int numColorID;

       public int PriceID
       {
           get { return numPriceID; }
           set { numPriceID = value; }
       }
       public int CustomerID
       {
           get { return objCustomerID; }
           set { objCustomerID = value; }
       }
       public int ItemID
       {
           get { return numItemID; }
           set { numItemID = value; }
       }
       public int UnitID
       {
           get { return numUnitID; }
           set { numUnitID = value; }
       }
       public double Price
       {
           get { return dblPrice; }
           set { dblPrice = value; }
       }
       public double VAT
       {
           get { return dblVAT; }
           set { dblVAT = value; }
       }
       public DateTime SetupDate
       {
           get { return dtSetupDate; }
           set { dtSetupDate = value; }
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
       public int SizeID
       {
           get { return numSizeID; }
           set { numSizeID = value; }
       }
       public int ColorID
       {
           get { return numColorID; }
           set { numColorID = value; }
       }
    }
}
