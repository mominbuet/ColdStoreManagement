using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
  public  class Import_Bill_LCAdjust
    {
      public Import_Bill_LCAdjust() { }
      #region Fields
      private int numAdjustID;
      private int numBillPayID;
      private DateTime dtAdjustDate;
      private double dbAdjustAmount;
      private int numCurrencyID;
      private double dbCurrencyRate;
      private int numPayFromAccID;
      private int numAdjustFromAccID;
      private int numTransRefID;
      private string strRemarks;
      private int numLCID;
      private int numCompanyID;
      private int numUserID;
      private DateTime dtModifiedDate;
        #endregion
        #region Properties
      public int AdjustID
      {
          get { return numAdjustID; }
          set { numAdjustID = value; }
      }
      public int BillPayID
      {
          get { return numBillPayID; }
          set { numBillPayID = value; }
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
      public int CurrencyID
      {
          get { return numCurrencyID; }
          set { numCurrencyID = value; }
      }
      public double CurrencyRate
      {
          get { return dbCurrencyRate; }
          set { dbCurrencyRate = value; }
      }
      public int PayFromAccID
      {
          get { return numPayFromAccID; }
          set { numPayFromAccID = value; }
      }
      public int AdjustFromAccID
      {
          get { return numAdjustFromAccID; }
          set { numAdjustFromAccID = value; }
      }
      public int TransRefID
      {
          get { return numTransRefID; }
          set { numTransRefID = value; }
      }
      public string Remarks
      {
          get { return strRemarks; }
          set { strRemarks = value; }
      }
      public int LCID
      {
          get { return numLCID; }
          set { numLCID = value; }
      }
      public int CompanyID
      {
          get { return numCompanyID; }
          set { numCompanyID = value; }
      }

      public int UserID
      {
          get { return numUserID; }
          set { numUserID = value; }
      }
      public DateTime ModifiedDate
      {
          get { return dtModifiedDate; }
          set { dtModifiedDate = value; }
      }
#endregion Properties
    }
}
