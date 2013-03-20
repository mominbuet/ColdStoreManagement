using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class Country
    {
       public Country() { }

       private int numCountryID;
       private string strCountryName;
       //private int numCompanyID;
       //private int numUserID;
       //private DateTime dtModifiedDate;

       public int CountryID
       {
           get { return numCountryID; }
           set { numCountryID=value; }
       }
       public string CountryName
       {
           get { return strCountryName; }
           set { strCountryName=value; }
       }
       /*
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
         */
    }
}
