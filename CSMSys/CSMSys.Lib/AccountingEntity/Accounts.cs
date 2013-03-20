using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Accounts : BaseObject
    {
        public Accounts() : base(){ }
        #region Feilds
      private int numAccountID;
      private string strAccountNo;
      private string strAccountTitle;
      private int numNature;
      private double monOpening;
      private double monCurrent;
      private string  numAccOrGroup;
      private DateTime dtCreateDate;
      private int numDepth;
      private int numIsInventory;
      private int numLedgerID;
      private int numLedgerTypeID;

      private Guid createdBy;
      private DateTime createdDate;
      private Guid modifiedBy;
      private DateTime modifiedDate;
     
      private int numParentID;
      private string strAccStatus;
#endregion
        #region Properties
      public int AccountID
      {
          get {return numAccountID; }
          set { numAccountID = value; }
      }
      public string AccountNo
      {
          get { return strAccountNo; }
          set { strAccountNo = value; }
      }
      public string AccountTitle
      {
          get { return strAccountTitle; }
          set { strAccountTitle = value; }
      }
      public DateTime AccountCreateDate
      {
          get { return dtCreateDate; }
          set { dtCreateDate = value; }
      }
      public int AccountNature
      {
          get { return numNature; }
          set { numNature = value; }
      }
      public double OpeningBalance
      {
          get { return monOpening; }
          set { monOpening = value; }
      }
      public double CurrentBalance
      {
          get { return monCurrent; }
          set { monCurrent = value; }
      }

      public string AccountOrGroup
      {
          get { return numAccOrGroup; }
          set { numAccOrGroup = value; }
      }
      public int AccountDepth
      {
          get { return numDepth; }
          set { numDepth = value; }
      }
      public int IsInventoryRelated
      {
          get { return numIsInventory; }
          set { numIsInventory = value; }
      }
      public int LedgerID
      {
          get { return numLedgerID; }
          set { numLedgerID = value; }
      }
      
      public int ParentID
      {
          get { return numParentID; }
          set { numParentID = value; }
      }
      public string AccountStatus
      {
          get { return strAccStatus; }
          set { strAccStatus = value; }
      }

      public Guid CreatedBy
      {
          get { return createdBy; }
          set { createdBy = value; }
      }
      public DateTime CreatedDate
      {
          get { return createdDate; }
          set { createdDate = value; }
      }
      public Guid ModifiedBy
      {
          get { return modifiedBy; }
          set { modifiedBy = value; }
      }
      public DateTime ModifiedDate
      {
          get { return modifiedDate; }
          set { modifiedDate = value; }
      }

      public int LedgerTypeID
      {
          get { return numLedgerTypeID; }
          set { numLedgerTypeID = value; }
      }
        #endregion
    }
}
