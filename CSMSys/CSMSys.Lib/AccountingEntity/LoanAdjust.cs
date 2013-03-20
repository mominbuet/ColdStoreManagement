using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class LoanAdjust
    {
       public LoanAdjust() { }
        #region Fields
       private int numLoanAdjustID;
       private int numLoanID;
       private DateTime dtAdjustDate;
       private double dbAdjustAmount;
       private int numAdjustMethodID;
       private string strAdjustRefNo;
       private string strRemarks;
       private int nPayFrom;
       private int nTransRefID;
        #endregion
        #region Properties
       public int LoanAdjustID
       {
           get { return numLoanAdjustID; }
           set { numLoanAdjustID = value; }
       }
       public int LoanID
       {
           get { return numLoanID; }
           set { numLoanID = value; }
       }
       public DateTime AdjustDate
       {
           get { return dtAdjustDate; }
           set { dtAdjustDate = value; }
       }
       public double AdjustAmount
       {
           get { return dbAdjustAmount; }
           set { dbAdjustAmount = value; }
       }
       public int AdjustMethodID
       {
           get { return numAdjustMethodID; }
           set { numAdjustMethodID = value; }
       }
       public string AdjustRefNo
       {
           get { return strAdjustRefNo; }
           set { strAdjustRefNo = value; }
       }
       public string Remarks
       {
           get { return strRemarks; }
           set { strRemarks = value; }
       }
       public int PayFrom
       {
           get { return nPayFrom; }
           set { nPayFrom = value; }
       }
       public int TransRefID
       {
           get { return nTransRefID; }
           set { nTransRefID = value; }
       }
        #endregion 
    }
}
