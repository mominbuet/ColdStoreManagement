using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class Loan
    {
       public Loan() { }
       #region Fields
       private int numLoanID;
       private string strLoanNo;
       private int numRefAccID;
       private int numLoanAccID;
       private double dbLoanAmount;
       private DateTime dtApplyDate;
       private DateTime dtSanktionDate;
       private DateTime dtExpireDate;
       private double dbInterestRate;
       private double dbDueAmount;
       private string strRemarks;
       private string strInterestPeriod;
       private int numLCID;
       private int numTransRefID;
       private double dblRate;
       private double dblAcceptedPercent;
       #endregion

       #region Properties
       public int LoanID
       {
           get { return numLoanID; }
           set { numLoanID = value; }
       }
       public string LoanNo
       {
           get { return strLoanNo; }
           set { strLoanNo = value; }
       }
       public int RefAccID
       {
           get { return numRefAccID; }
           set { numRefAccID = value; }
       }
       public double LoanAmount
       {
           get { return dbLoanAmount; }
           set { dbLoanAmount = value; }
       }
       public DateTime ApplyDate
       {
           get { return dtApplyDate; }
           set { dtApplyDate = value; }
       }
       public DateTime SanktionDate
       {
           get { return dtSanktionDate; }
           set { dtSanktionDate = value; }
       }
       public DateTime ExpireDate
       {
           get { return dtExpireDate; }
           set { dtExpireDate = value; }
       }
       public double InterestRate
       {
           get { return dbInterestRate; }
           set { dbInterestRate = value; }
       }
       public double DueAmount
       {
           get { return dbDueAmount; }
           set { dbDueAmount = value; }
       }
       public string Remarks
       {
           get { return strRemarks; }
           set { strRemarks = value; }
       }
       public string InterestPeriod
       {
           get { return strInterestPeriod; }
           set { strInterestPeriod = value; }
       }
       public int LoanAccID
       {
           get { return numLoanAccID; }
           set { numLoanAccID = value; }
       }
       public int LCID
       {
           get { return numLCID; }
           set { numLCID = value; }
       }
       public int TransRefID
       {
           get { return numTransRefID; }
           set { numTransRefID = value; }
       }
       public double Rate
       {
           get { return dblRate; }
           set { dblRate = value; }
       }
       public double AcceptedPercent
       {
           get { return dblAcceptedPercent; }
           set { dblAcceptedPercent = value; }
       }
        #endregion
    }
}
