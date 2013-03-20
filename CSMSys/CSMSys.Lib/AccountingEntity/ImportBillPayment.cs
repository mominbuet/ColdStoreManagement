using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class ImportBillPayment
    {
       public ImportBillPayment() { }

       #region Fields
       private int numSlNo;
       private int numBillID;
       private string strIFBCNo;
       private DateTime dtPayDate;
       private int numAccountID;
       private double dblDrAmt;
       private double dblCrAmt;
       private string strPrtclrs;
       #endregion

       #region Properties
       public int SerialNo
       {
           get {return  numSlNo; }
           set { numSlNo=value; }
       }
       public int BillID
       {
           get { return numBillID; }
           set { numBillID=value; }
       }
       public string IFBCNo  // PAD / APB No.
       {
           get { return strIFBCNo; }
           set { strIFBCNo = value; }
       }
       public DateTime PaymentDate
       {
           get { return dtPayDate; }
           set { dtPayDate=value; }
       }
       public int AccountID
       {
           get { return numAccountID; }
           set { numAccountID=value; }
       }
       public double DebitAmount
       {
           get { return dblDrAmt; }
           set { dblDrAmt=value; }
       }
       public double CreditAmount
       {
           get { return dblCrAmt; }
           set { dblCrAmt=value; }
       }
       public string Particulars
       {
           get { return strPrtclrs; }
           set { strPrtclrs=value; }
       }
       #endregion
    }
}
