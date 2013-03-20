using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
   public class TransactionMaster : BaseObject
    {
       public TransactionMaster() : base() { }


       #region Fields
           private int numTransMID;
           private DateTime dtTransDate;
           private string strVoucherNo;
           private string strVoucherPayee;
           private int numVoucherType;
           private int numTransMethodID;
           private int numMethodRefID;
           private string strMethodRefNo;
           private string strTransDesc;
           private string strApprovedBy;
           private DateTime dtApprovedDate;

           private Guid createdBy;
           private DateTime createdDate;
           private Guid modifiedBy;
           private DateTime modifiedDate;

           private string strModule="";
       #endregion

       #region Properties

           public int TransactionMasterID
           {
               get { return numTransMID; }
               set { numTransMID = value; }
           }
           public DateTime TransactionDate
           {
               get { return dtTransDate; }
               set { dtTransDate = value; }
           }
           public string VoucherNo
           {
               get { return  strVoucherNo; }
               set { strVoucherNo = value; }
           }
           public string VoucherPayee
           {
               get { return strVoucherPayee; }
               set { strVoucherPayee = value; }
           }
           public int VoucherType
           {
               get { return numVoucherType; }
               set { numVoucherType = value; }
           }
           public int TransactionMethodID
           {
               get { return numTransMethodID; }
               set { numTransMethodID = value; }
           }
           public int MethodRefID
           {
               get { return numMethodRefID; }
               set { numMethodRefID = value; }
           }
           public string MethodRefNo
           {
               get { return strMethodRefNo; }
               set { strMethodRefNo = value; }
           }
           public string TransactionDescription
           {
               get { return strTransDesc; }
               set { strTransDesc = value; }
           }
           public string ApprovedBy
           {
               get { return strApprovedBy; }
               set { strApprovedBy = value; }
           }
           public DateTime ApprovedDate
           {
               get { return dtApprovedDate; }
               set { dtApprovedDate = value; }
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

           public string Module
           {
               get { return strModule; }
               set { strModule = value; }
           }
        #endregion
    }
}
