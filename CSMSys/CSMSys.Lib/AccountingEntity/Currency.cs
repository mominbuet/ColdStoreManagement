using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class Currency
    {
       public Currency() { }


       #region Fields
       private int nCurrencyID;
       private string sCode;
       private string sName;
       private string sSymbol;
       //private int nCompanyID;
       //private int numUserID;
       //private DateTime dtModifiedDate;
       #endregion

      

       public int CurrencyID
       {
           get { return nCurrencyID; }
           set { nCurrencyID = value; }
       }
       public string Code
       {
           get { return sCode; }
           set { sCode = value; }
       }
       public string Name
       {
           get { return sName; }
           set { sName = value; }
       }
       public string Symbol
       {
           get { return sSymbol; }
           set { sSymbol = value; }
       }
       /*
       public int CompanyID
       {
           get { return nCompanyID; }
           set { nCompanyID = value; }
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
        * */
    }
}
