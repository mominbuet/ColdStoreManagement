using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingEntity;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaAccount
    {

       public int InsertUpdateAccount(Accounts objAcc, SqlConnection con)
       {
           int ID = 0;
           try
           {

               SqlCommand com = new SqlCommand();

               SqlTransaction trans = null;
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "spInsertOrUpdateAccount";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@AccountID", SqlDbType.Int).Value = objAcc.AccountID;
               com.Parameters.Add("@AccountNo", SqlDbType.VarChar, 100).Value = objAcc.AccountNo;
               com.Parameters.Add("@AccountTitle", SqlDbType.VarChar, 500).Value = objAcc.AccountTitle;
               com.Parameters.Add("@AccOrGroup", SqlDbType.VarChar, 50).Value = objAcc.AccountOrGroup;
               com.Parameters.Add("@AccDepth", SqlDbType.Int).Value = objAcc.AccountDepth;
               com.Parameters.Add("@Nature", SqlDbType.Int).Value = objAcc.AccountNature;
               com.Parameters.Add("@AccountStatus", SqlDbType.VarChar, 100).Value = objAcc.AccountStatus;
               com.Parameters.Add("@AccCreateDate", SqlDbType.DateTime).Value = objAcc.AccountCreateDate;
               com.Parameters.Add("@ParentID", SqlDbType.Int).Value = objAcc.ParentID;
               com.Parameters.Add("@OpeningBalance", SqlDbType.Money).Value = objAcc.OpeningBalance;

               com.Parameters.Add("@IsInventoryRelated", SqlDbType.Int).Value = objAcc.IsInventoryRelated;

               com.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = objAcc.LedgerTypeID;
               if (objAcc.LedgerID == -1)
                   com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = objAcc.LedgerID;

               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;

               com.ExecuteNonQuery();
               trans.Commit();
               if (objAcc.AccountID == 0)
                   ID = ConnectionHelper.GetID(con, "AccountID", "T_Account");
               else
                   ID = objAcc.AccountID;
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ID;
       }

       public int InsertUpdateAccounts(Accounts objAcc, SqlConnection con)
       {
           int ID = 0;
           SqlCommand com = new SqlCommand("spInsertOrUpdateAccount", con);
           try
           {
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@AccountID", SqlDbType.Int).Value = objAcc.AccountID;
               com.Parameters.Add("@AccountNo", SqlDbType.VarChar, 100).Value = objAcc.AccountNo;
               com.Parameters.Add("@AccountTitle", SqlDbType.VarChar, 500).Value = objAcc.AccountTitle;
               com.Parameters.Add("@AccOrGroup", SqlDbType.VarChar, 50).Value = objAcc.AccountOrGroup;
               com.Parameters.Add("@AccDepth", SqlDbType.Int).Value = objAcc.AccountDepth;
               com.Parameters.Add("@Nature", SqlDbType.Int).Value = objAcc.AccountNature;
               com.Parameters.Add("@AccountStatus", SqlDbType.VarChar, 100).Value = objAcc.AccountStatus;
               com.Parameters.Add("@AccCreateDate", SqlDbType.DateTime).Value = objAcc.AccountCreateDate;
               com.Parameters.Add("@ParentID", SqlDbType.Int).Value = objAcc.ParentID;
               com.Parameters.Add("@OpeningBalance", SqlDbType.Money).Value = objAcc.OpeningBalance;

               com.Parameters.Add("@IsInventoryRelated", SqlDbType.Int).Value = objAcc.IsInventoryRelated;

               com.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = objAcc.LedgerTypeID;
               if (objAcc.LedgerID == -1)
                   com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = objAcc.LedgerID;

               //if (objAcc.AccountID <= 0)
               //{
               //    com.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = objAcc.CreatedBy;
               //    com.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = objAcc.CreatedDate;
               //    com.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = objAcc.ModifiedBy;
               //    com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objAcc.ModifiedDate;
               //}
               //else
               //{
               //    com.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
               //    com.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DBNull.Value;
               //    com.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = objAcc.ModifiedBy;
               //    com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objAcc.ModifiedDate;
               //}

               if (objAcc.AccountID <= 0)
               {
                   SqlParameter retValParam = com.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                   retValParam.Direction = System.Data.ParameterDirection.ReturnValue;

                   com.ExecuteNonQuery();

                   ID = Convert.ToInt32(retValParam.Value);
                   //SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TransMID),0) FROM T_Transaction_Master", con, trans);
                   //ID = Convert.ToInt32(cmd.ExecuteScalar());
               }
               else
               {
                   com.ExecuteNonQuery();
                   ID = objAcc.AccountID;
                   // ID = Convert.ToInt32(com.Parameters["@TransMID"].Value);
               }

               //com.ExecuteNonQuery();

               //if (objAcc.AccountID == 0)
               //{
               //    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(AccountID),0) FROM T_Account", con, trans);
               //    ID = Convert.ToInt32(cmd.ExecuteScalar());
               //}
               //else
               //    ID = objAcc.AccountID;
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ID;
       }

       public void DeleteAccount(int accID, SqlConnection con)
       {
           SqlCommand com = null;
           //SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               //trans = con.BeginTransaction();
               com.Connection = con;
               //com.Transaction = trans;
               com.CommandText = "DELETE FROM T_Account WHERE AccountID=@AccountID";
               com.Parameters.Add("@AccountID", SqlDbType.Int).Value = accID;
               com.ExecuteNonQuery();
               //trans.Commit();
           }
           catch (Exception ex)
           {

               //if (trans != null) trans.Rollback();
               throw ex;
           }

       }
       public void DeleteAccount(int accID, SqlConnection con,SqlTransaction trans)
       {
           SqlCommand com = null;
          
           try
           {
               com = new SqlCommand();
             
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "DELETE FROM T_Account WHERE AccountID=@AccountID";
               com.Parameters.Add("@AccountID", SqlDbType.Int).Value = accID;
               com.ExecuteNonQuery();
           
           }
           catch (Exception ex)
           {

             
               throw ex;
           }

       }
       public void UpdateLedgerID(SqlConnection con, int AccID, int LdgrID)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("UPDATE T_Account SET LedgerID=@ldgrID WHERE AccountID=@accID", con);
               if (LdgrID <= 0)
                   cmd.Parameters.Add("@ldgrID", SqlDbType.Int).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@ldgrID", SqlDbType.Int).Value = LdgrID;
               cmd.Parameters.Add("@accID", SqlDbType.Int).Value = AccID;
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteAccountAndLedger(int accID,int ldgrID, SqlConnection con)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "UPDATE T_Ledgers SET AccountID=NULL WHERE LedgerID=@LedgerID; UPDATE T_Account SET LedgerID=NULL WHERE AccountID=@AccountID;DELETE FROM T_Ledgers WHERE LedgerID=@LedgerID;DELETE FROM T_Account WHERE AccountID=@AccountID";
               com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = ldgrID;
               com.Parameters.Add("@AccountID", SqlDbType.Int).Value = accID;
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {

               if (trans != null) trans.Rollback();
               throw ex;
           }

       }
       //public int SaveOrUpdateA_DTL(Ledgers objAccDTL,string mode, SqlConnection con)
       //{
       //    SqlCommand comm = new SqlCommand();
       //    SqlTransaction trans = null;
       //    try
       //    {
       //        trans = con.BeginTransaction();
       //        comm.Connection = con;
       //        comm.Transaction = trans;
       //        comm.CommandText = "Acc_DTL";
       //        comm.CommandType = CommandType.StoredProcedure;
       //        comm.Parameters.Add("@addedit", SqlDbType.VarChar,50).Value = mode;
       //        comm.Parameters.Add("@AccountID", SqlDbType.Int).Value = objAccDTL.AccountID;
       //        comm.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = objAccDTL.LedgerTypeID;
       //        comm.Parameters.Add("@Address", SqlDbType.VarChar, 100).Value = objAccDTL.Address;
       //        comm.Parameters.Add("@CountryID", SqlDbType.Int).Value = objAccDTL.CountryID;
       //        comm.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objAccDTL.CurrencyID;
       //        comm.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = objAccDTL.ContactPerson;
       //        comm.Parameters.Add("@ContactNo", SqlDbType.VarChar, 100).Value = objAccDTL.BankAccountType;
       //        comm.Parameters.Add("@BussinessType", SqlDbType.VarChar, 100).Value = objAccDTL.BusinessType;
       //        comm.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = objAccDTL.Phone;
       //        comm.Parameters.Add("@Fax", SqlDbType.VarChar, 100).Value = objAccDTL.Fax;
       //        comm.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = objAccDTL.Email;
       //        comm.Parameters.Add("@Web", SqlDbType.VarChar, 100).Value = objAccDTL.Web;
       //        comm.Parameters.Add("@TeamID", SqlDbType.Int).Value = objAccDTL.TeamMemberID;
       //        comm.Parameters.Add("@Remarks", SqlDbType.VarChar, 100).Value = objAccDTL.Remarks;
       //        comm.ExecuteNonQuery();
       //        trans.Commit();
       //    }
       //    catch (Exception ex)
       //    {
       //        trans.Rollback();
       //        throw ex;
       //    }
       //    return objAccDTL.AccountID;


       //}
       public DataTable GetChildsOfParents(int numParentID,SqlConnection con)
       {
           DataTable ds = new DataTable();
           try
           {
           SqlDataAdapter da=new SqlDataAdapter("select * from T_Account where ParentID="+numParentID.ToString(),con);
          
           da.Fill(ds);
           da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ds;
              
              
        }
       public DataTable GetAccountsOfDepth(int numdepth,SqlConnection con)
       {
           SqlDataAdapter da = new SqlDataAdapter("select * from T_Account where CompanyID=@CompanyID AND AccDepth=" + numdepth.ToString(), con);
           da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
           DataTable ds = new DataTable();
           da.Fill(ds);
           da.Dispose();
           return ds;

       }

       public DataTable GetAllAccounts( SqlConnection con)
       {
           DataTable ds = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_Account WHERE CompanyID=@CompanyID  ORDER BY AccountNo", con);
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(ds);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ds;


       }
       public Accounts GetAccount(SqlConnection con, int AccountID)
       {
           Accounts objAcc = new Accounts();
           DataTable ds = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_Account WHERE AccountID="+AccountID.ToString(), con);

               da.Fill(ds);
               da.Dispose();
               if (ds.Rows.Count==0) return null;
               objAcc.AccountID = ds.Rows[0].Field<int>("AccountID");
               objAcc.AccountTitle  = ds.Rows[0].Field<string>("AccountTitle");
               objAcc.AccountOrGroup  = ds.Rows[0].Field<string>("AccOrGroup");
               objAcc.AccountNo = ds.Rows[0].Field<string>("AccountNo");
               objAcc.AccountNature = ds.Rows[0].Field<int>("Nature");
               objAcc.AccountStatus = ds.Rows[0].Field<string>("AccountStatus");
               objAcc.AccountCreateDate = ds.Rows[0].Field<object>("AccCreateDate") == null || ds.Rows[0].Field<object>("AccCreateDate") ==DBNull.Value? DateTime.Now : ds.Rows[0].Field<DateTime>("AccCreateDate");
               objAcc.AccountDepth = ds.Rows[0].Field<int>("AccDepth");
               objAcc.OpeningBalance = Convert.ToDouble( ds.Rows[0].Field<object>("OpeningBalance"));
               objAcc.CurrentBalance =Convert.ToDouble( ds.Rows[0].Field<object>("CurrentBalance"));
               objAcc.IsInventoryRelated = ds.Rows[0].Field<int>("IsInventoryRelated");
               objAcc.LedgerTypeID =ds.Rows[0].Field<object>("LedgerTypeID")==DBNull.Value||ds.Rows[0].Field<object>("LedgerTypeID")==null?0:ds.Rows[0].Field<int>("LedgerTypeID");
               objAcc.LedgerID = ds.Rows[0].Field<object>("LedgerID") == null || ds.Rows[0].Field<object>("LedgerID") == DBNull.Value ? -1 : ds.Rows[0].Field<int>("LedgerID");
               objAcc.ParentID = ds.Rows[0].Field<object>("parentID") == null || ds.Rows[0].Field<object>("parentID") == DBNull.Value ? -1 : ds.Rows[0].Field<int>("parentID");
               objAcc.UserID = ds.Rows[0].Field<object>("UserID") == DBNull.Value || ds.Rows[0].Field<object>("UserID") == null ? 0 : ds.Rows[0].Field<int>("UserID");
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return objAcc;

       }
       public DataTable GetAccounts(SqlConnection con)
       {
           DataTable ds = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_Account WHERE accOrGroup like 'Account' ORDER BY AccountNo", con);
               //da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(ds);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ds;


       }
       public DataTable GetAccounts(SqlConnection con,string fields,string OrderByFields)
       {
           DataTable ds = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select " + fields + " from VW_Account WHERE CompanyID=@CompanyID AND accOrGroup like 'Account' ORDER BY " + OrderByFields, con);
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(ds);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ds;


       }
       public DataTable GetAccounts(SqlConnection con, string fields,string FilterExpr, string OrderByFields)
       {
           DataTable ds = new DataTable();
           try
           {
               if (FilterExpr == string.Empty) FilterExpr = "1=1";
               string qstr = "select " + fields + " from VW_Account WHERE accOrGroup like 'Account' AND CompanyID=@CompanyID AND " + FilterExpr + " ORDER BY " + OrderByFields;
               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(ds);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ds;


       }
       public DataTable getLedgerType(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_LedgerType", con);
                DataTable ds = new DataTable();
                da.Fill(ds);
                da.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public DataTable getAccountHeads(SqlConnection con)
       {
           try
           {
               DataTable dtAccHeads = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Account WHERE AccOrGroup like 'Group' ORDER BY AccountTitle", con);
               //da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(dtAccHeads);
               da.Dispose();
               return dtAccHeads;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public string GenerateAccountNo(SqlConnection con, int parentID)
       {
           string strAccNo = string.Empty;
           try
           {

               SqlCommand cmd = new SqlCommand("SELECT dbo.fnGenerateAccountNo(@parentID)", con);
               cmd.Parameters.Add("@parentID", SqlDbType.Int).Value = parentID;
               //cmd.CommandType = CommandType.StoredProcedure;
               strAccNo = cmd.ExecuteScalar().ToString();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return strAccNo;
       }

       public DataTable getAccountsLedgers(SqlConnection con,string searchBy,int LedgerTypeID,string Title)
       {
           try
           {
               DataTable dtAccLdgr = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("spSearchAccountLedgers", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@SearchBy", SqlDbType.VarChar, 100).Value = searchBy;
               da.SelectCommand.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = LedgerTypeID;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.SelectCommand.Parameters.Add("@TitleLike", SqlDbType.VarChar,500).Value = Title;
               da.Fill(dtAccLdgr);
               da.Dispose();
               return dtAccLdgr;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable loadCustomerAccount(SqlConnection con, string LedgerTypeID)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("Select AccountID,AccountTitle,CurrentBalance from T_Account " + @LedgerTypeID.ToString(), con);
              da.SelectCommand.Parameters.Add("@LedgerTypeID", SqlDbType.VarChar, 100).Value = LedgerTypeID;

               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception Ex)
           {
               throw Ex;
           }
       }
       public DataTable searchAccount(SqlConnection con, string strQuerry, string LedgerTypeID)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("Select AccountID,AccountTitle,CurrentBalance from T_Account WHERE CompanyID=@CompanyID AND AccountTitle like '" + @strQuerry.ToString() + "%'" + @LedgerTypeID.ToString(), con);
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.SelectCommand.Parameters.Add("@strQuerry", SqlDbType.VarChar, 500).Value = strQuerry;
               da.SelectCommand.Parameters.Add("@LedgerTypeID", SqlDbType.VarChar, 100).Value = LedgerTypeID;
             
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public Accounts getSalesAccount(SqlConnection con, int AccountID)
       {
           Accounts obSalesAccount = new Accounts();
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Account WHERE AccountID = @AccountID", con);
               da.SelectCommand.Parameters.Add("@AccountID", SqlDbType.Int).Value = AccountID;
               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0) return null;
               obSalesAccount.AccountID = dt.Rows[0].Field<int>("AccountID");
               obSalesAccount.AccountNo = dt.Rows[0].Field<string>("AccountNo");
               obSalesAccount.AccountTitle = dt.Rows[0].Field<string>("AccountTitle");
               obSalesAccount.AccountStatus = dt.Rows[0].Field<string>("AccountStatus");
               //obSalesAccount.ParentID = dt.Rows[0].Field<int>("AccountStatus");
               obSalesAccount.OpeningBalance = Convert.ToDouble(dt.Rows[0].Field<object>("OpeningBalance"));
               obSalesAccount.CurrentBalance = Convert.ToDouble(dt.Rows[0].Field<object>("CurrentBalance"));
               obSalesAccount.IsInventoryRelated = dt.Rows[0].Field<int>("IsInventoryRelated");
               obSalesAccount.LedgerTypeID = dt.Rows[0].Field<int>("LedgerTypeID");
               if (dt.Rows[0].Field<object>("LedgerID") == null)
                   obSalesAccount.LedgerID = 0;
               else
               obSalesAccount.LedgerID = dt.Rows[0].Field<int>("LedgerID");
               //obSalesAccount.CompanyID = dt.Rows[0].Field<int>("CompanyID");
               //obSalesAccount.UserID = dt.Rows[0].Field<int>("UserID");
               if (dt.Rows[0].Field<object>("ModifiedDate") == null)
                   obSalesAccount.ModifiedDate = DateTime.Now;
               else
               obSalesAccount.ModifiedDate = dt.Rows[0].Field<DateTime>("ModifiedDate");
               return obSalesAccount;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }
       public Accounts getPurchaseAccount(SqlConnection con, int AccountID)
       {
           Accounts obPurchaseAccount = new Accounts();
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Account WHERE AccountID = @AccountID", con);
               da.SelectCommand.Parameters.Add("@AccountID", SqlDbType.Int).Value = AccountID;
               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0) return null;
               obPurchaseAccount.AccountID = dt.Rows[0].Field<int>("AccountID");
               obPurchaseAccount.AccountNo = dt.Rows[0].Field<string>("AccountNo");
               obPurchaseAccount.AccountTitle = dt.Rows[0].Field<string>("AccountTitle");
               obPurchaseAccount.AccountStatus = dt.Rows[0].Field<string>("AccountStatus");
               //obSalesAccount.ParentID = dt.Rows[0].Field<int>("AccountStatus");
               obPurchaseAccount.OpeningBalance = Convert.ToDouble(dt.Rows[0].Field<object>("OpeningBalance"));
               obPurchaseAccount.CurrentBalance = Convert.ToDouble(dt.Rows[0].Field<object>("CurrentBalance"));
               obPurchaseAccount.IsInventoryRelated = dt.Rows[0].Field<int>("IsInventoryRelated");
               obPurchaseAccount.LedgerTypeID = dt.Rows[0].Field<int>("LedgerTypeID");
               if (dt.Rows[0].Field<object>("LedgerID") == null)
                   obPurchaseAccount.LedgerID = 0;
               else
                   obPurchaseAccount.LedgerID = dt.Rows[0].Field<int>("LedgerID");
               //obSalesAccount.CompanyID = dt.Rows[0].Field<int>("CompanyID");
               //obSalesAccount.UserID = dt.Rows[0].Field<int>("UserID");
               if (dt.Rows[0].Field<object>("ModifiedDate") == null)
                   obPurchaseAccount.ModifiedDate = DateTime.Now;
               else
                   obPurchaseAccount.ModifiedDate = dt.Rows[0].Field<DateTime>("ModifiedDate");
               return obPurchaseAccount;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public int GetAccountIdOfTitle(SqlConnection con, string AccountTitle)
       {
           int AccountId = 0;
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Account WHERE AccountTitle = @AccountTitle AND CompanyID = @CompanyID", con);
               da.SelectCommand.Parameters.Add("@AccountTitle", SqlDbType.VarChar, 500).Value = AccountTitle;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(dt);
               da.Dispose();

               if (dt.Rows.Count == 0) return 0;
               AccountId =GlobalFunctions.isNull( dt.Rows[0].Field<object>("AccountID"),0);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return AccountId;
       }

       public int GetDepreciationAccountID(SqlConnection con)
       {
           try
           {
               string qstr = "SELECT AccountID FROM  VW_AccountWithClassification WHERE (AccountType LIKE 'Expense') AND (AccountTitle  LIKE 'Depreciation') AND (CompanyID = @CompanyID)";
               SqlCommand cmd = new SqlCommand(qstr, con);
               cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;

               return GlobalFunctions.isNull(cmd.ExecuteScalar(), 0);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
