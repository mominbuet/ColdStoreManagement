using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
 public class Ledgers : BaseObject
    {
     public Ledgers() : base(){ }
        #region Feilds
     private int numLedgerID;
     private int numLedgerTypeID;
     private string strLedgerName;
     private string strAddress;
     private int numCountryID;
     private int numCurrencyID;
     private string strContactPerson;
     private string strBankAccType;
     private string strBusinessType;
     private string strPhone;
     private string strFax;
     private string strEmail;
  
     private int numTeamMemberID;
     private string strRemarks;
     private int numAccountID;
        #endregion
        #region
     public int LedgerID
     {
         get { return numLedgerID; }
         set { numLedgerID = value; }
     }
     public string LedgerName
     {
         get { return strLedgerName; }
         set { strLedgerName = value; }
     }
     public int LedgerTypeID
     {
         get { return numLedgerTypeID; }
         set { numLedgerTypeID = value; }
     }
     public string Address
     {
         get { return strAddress; }
         set { strAddress = value; }
     }
     public int CountryID
     {
         get { return numCountryID; }
         set { numCountryID = value; }
     }
     public int CurrencyID
     {
         get { return numCurrencyID; }
         set { numCurrencyID = value; }
     }
     public string ContactPerson
     {
         get { return strContactPerson; }
         set { strContactPerson = value; }
     }
     public string BankAccountType
     {
         get { return strBankAccType; }
         set { strBankAccType = value; }
     }
     public string BusinessType
     {
         get { return strBusinessType; }
         set { strBusinessType = value; }
     }
     public string Phone
     {
         get { return strPhone; }
         set { strPhone = value; }
     }
     public string Fax
     {
         get { return strFax; }
         set { strFax = value; }
     }
     public string Email
     {
         get { return strEmail; }
         set { strEmail = value; }
     }
     
     public int TeamMemberID
     {
         get { return numTeamMemberID; }
         set { numTeamMemberID = value; }
     }
     public string Remarks
     {
         get { return strRemarks; }
         set { strRemarks = value; }
     }

     public int AccountID
     {
         get { return numAccountID; }
         set { numAccountID = value; }
     }
        #endregion

    }
}
