using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingEntity;
using System.Data;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
  public  class DaLedger
    {
      public DaLedger() { }

      public int InsertUpdateLedger(Ledgers objLdgr, SqlConnection con)
      {
          int ID = 0;
          SqlCommand com = new SqlCommand();

          SqlTransaction trans = null;
          trans = con.BeginTransaction();
          com.Connection = con;
          com.Transaction = trans;
          com.CommandText = "spInsertUpdateLedgers";
          com.CommandType = CommandType.StoredProcedure;
          com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = objLdgr.LedgerID;
          com.Parameters.Add("@LedgerName", SqlDbType.VarChar, 100).Value = objLdgr.LedgerName;
          com.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = objLdgr.LedgerTypeID;
          com.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = objLdgr.Address;

          com.Parameters.Add("@CountryID", SqlDbType.Int).Value = objLdgr.CountryID;
          com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objLdgr.CurrencyID;

          com.Parameters.Add("@ContactPerson", SqlDbType.VarChar,500).Value = objLdgr.ContactPerson;
          if (objLdgr.BankAccountType == "NULL")
              com.Parameters.Add("@bankAccType", SqlDbType.VarChar, 500).Value = DBNull.Value;
          else
              com.Parameters.Add("@bankAccType", SqlDbType.VarChar, 500).Value = objLdgr.BankAccountType;
          com.Parameters.Add("@BusinessType", SqlDbType.VarChar, 500).Value = objLdgr.BusinessType;
          com.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = objLdgr.Phone;
          com.Parameters.Add("@Fax", SqlDbType.VarChar, 100).Value = objLdgr.Fax;
          com.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = objLdgr.Email;
          if (objLdgr.TeamMemberID == -1)
              com.Parameters.Add("@TeamID", SqlDbType.Int).Value = DBNull.Value;
          else
              com.Parameters.Add("@TeamID", SqlDbType.Int).Value = objLdgr.TeamMemberID;
          com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objLdgr.Remarks;
          if(objLdgr.AccountID==-1)
              com.Parameters.Add("@AccountID", SqlDbType.Int).Value = DBNull.Value;
          else
              com.Parameters.Add("@AccountID", SqlDbType.Int).Value = objLdgr.AccountID;
          com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
          com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;

          com.ExecuteNonQuery();
          trans.Commit();
          if (objLdgr.LedgerID == 0)
              ID = ConnectionHelper.GetID(con, "LedgerID", "T_Ledgers");
          else
              ID = objLdgr.LedgerID;
          return ID;
      }

      public int InsertUpdateLedgers(Ledgers objLdgr, SqlConnection con)
      {
          int ID = 0;
          try
          {
              SqlCommand com = new SqlCommand();


              com.Connection = con;
              com.CommandText = "spInsertUpdateLedgers";
              com.CommandType = CommandType.StoredProcedure;
              com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = objLdgr.LedgerID;
              com.Parameters.Add("@LedgerName", SqlDbType.VarChar, 100).Value = objLdgr.LedgerName;
              com.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = objLdgr.LedgerTypeID;
              com.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = objLdgr.Address;

              com.Parameters.Add("@CountryID", SqlDbType.Int).Value = objLdgr.CountryID;
              com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objLdgr.CurrencyID;

              com.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 500).Value = objLdgr.ContactPerson;
              if (objLdgr.BankAccountType == "NULL")
                  com.Parameters.Add("@bankAccType", SqlDbType.VarChar, 500).Value = DBNull.Value;
              else
                  com.Parameters.Add("@bankAccType", SqlDbType.VarChar, 500).Value = objLdgr.BankAccountType;
              com.Parameters.Add("@BusinessType", SqlDbType.VarChar, 500).Value = objLdgr.BusinessType;
              com.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = objLdgr.Phone;
              com.Parameters.Add("@Fax", SqlDbType.VarChar, 100).Value = objLdgr.Fax;
              com.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = objLdgr.Email;
              if (objLdgr.TeamMemberID == -1)
                  com.Parameters.Add("@TeamID", SqlDbType.Int).Value = DBNull.Value;
              else
                  com.Parameters.Add("@TeamID", SqlDbType.Int).Value = objLdgr.TeamMemberID;
              com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objLdgr.Remarks;
              if (objLdgr.AccountID == -1)
                  com.Parameters.Add("@AccountID", SqlDbType.Int).Value = DBNull.Value;
              else
                  com.Parameters.Add("@AccountID", SqlDbType.Int).Value = objLdgr.AccountID;

              if (objLdgr.LedgerID <= 0)
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
                  ID = objLdgr.LedgerID;
                  // ID = Convert.ToInt32(com.Parameters["@TransMID"].Value);
              }

              //com.ExecuteNonQuery();

              //if (objLdgr.LedgerID == 0)
              //{
              //    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(LedgerID),0) FROM T_Ledgers", con, trans);
              //    ID = Convert.ToInt32(cmd.ExecuteScalar());
              //}
              //else
              //    ID = objLdgr.LedgerID;
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return ID;
      }
      public void UpdateAccountID(SqlConnection con, int LdgrID, int AccID)
      {
          try
          {
              SqlCommand cmd = new SqlCommand("UPDATE T_Ledgers SET AccountID=@accID WHERE LedgerID=@ldgrID", con);
              if (AccID <= 0)
                  cmd.Parameters.Add("@accID", SqlDbType.Int).Value = DBNull.Value;
              else
                  cmd.Parameters.Add("@accID", SqlDbType.Int).Value = AccID;
              cmd.Parameters.Add("@ldgrID", SqlDbType.Int).Value = LdgrID;
              cmd.ExecuteNonQuery();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
      public void DeleteLedger(int LdgrID, SqlConnection con)
      {
          SqlCommand com = null;
          //SqlTransaction trans = null;
          try
          {
              com = new SqlCommand();
              //trans = con.BeginTransaction();
              com.Connection = con;
              //com.Transaction = trans;
              com.CommandText = "DELETE FROM T_Ledgers WHERE LedgerID=@LedgerID";
              com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = LdgrID;
              com.ExecuteNonQuery();
              //trans.Commit();
          }
          catch (Exception ex)
          {

              //if (trans != null) trans.Rollback();
              throw ex;
          }
      }
      public void DeleteLedger(int LdgrID, SqlConnection con,SqlTransaction trans)
      {
          SqlCommand com = null;
         
          try
          {
              com = new SqlCommand();
            
              com.Connection = con;
              com.Transaction = trans;
              com.CommandText = "DELETE FROM T_Ledgers WHERE LedgerID=@LedgerID";
              com.Parameters.Add("@LedgerID", SqlDbType.Int).Value = LdgrID;
              com.ExecuteNonQuery();
            
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }
      public DataTable GetLadgers(SqlConnection con,int LdgrType)
      {
          DataTable ds = new DataTable();
          try
          {
              SqlDataAdapter da = new SqlDataAdapter("select * from T_Ledgers WHERE CompanyID=@CompanyID AND  LedgerTypeID=@LedgerTypeID ORDER BY LedgerName", con);
              da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
              da.SelectCommand.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = LdgrType;
              da.Fill(ds);
              da.Dispose();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return ds;


      }
      public DataTable GetLadgers(SqlConnection con, int LdgrTypeID,string LdgrName)
      {
          DataTable ds = new DataTable();
          try
          {
              SqlDataAdapter da = new SqlDataAdapter("select * from T_Ledgers WHERE CompanyID=@CompanyID AND LedgerTypeID=@LedgerTypeID AND LedgerName LIKE @Lname ORDER BY LedgerName", con);
              da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
              da.SelectCommand.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = LdgrTypeID;
              da.SelectCommand.Parameters.Add("@Lname", SqlDbType.VarChar, 100).Value = LdgrName+ "%";
              da.Fill(ds);
              da.Dispose();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return ds;


      }

      public DataTable GetLadgers(SqlConnection con, int LdgrTypeID, int team)
      {
          DataTable ds = new DataTable();
          try
          {
              string qstr = "select * from T_Ledgers WHERE CompanyID=@CompanyID AND LedgerTypeID=@LedgerTypeID AND " + (team <= 0 ? "TeamID IS NULL" : "TeamID =@TeamID ")+ " ORDER BY LedgerName";
              SqlDataAdapter da = new SqlDataAdapter(qstr, con);
              da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
              da.SelectCommand.Parameters.Add("@LedgerTypeID", SqlDbType.Int).Value = LdgrTypeID;
              da.SelectCommand.Parameters.Add("@TeamID", SqlDbType.Int).Value = team;
              da.Fill(ds);
              da.Dispose();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return ds;


      }
      public Ledgers GetLedger(SqlConnection con, int LedgerID)
      {
          DataTable ds = new DataTable();
          Ledgers objLdgr = new Ledgers();
          try
          {
              SqlDataAdapter da = new SqlDataAdapter("select * from T_Ledgers WHERE LedgerID=@LedgerID ", con);
              da.SelectCommand.Parameters.Add("@LedgerID", SqlDbType.Int).Value = LedgerID;
              da.Fill(ds);
              da.Dispose();
              if (ds.Rows.Count == 0) return null;
              objLdgr.LedgerID = ds.Rows[0].Field<int>("LedgerID");
              objLdgr.LedgerName = ds.Rows[0].Field<string>("LedgerName");
              objLdgr.LedgerTypeID = ds.Rows[0].Field<int>("LedgerTypeID");
              objLdgr.Address = ds.Rows[0].Field<string>("Address");
              objLdgr.BankAccountType = ds.Rows[0].Field<string>("bankAccType");
              objLdgr.BusinessType = ds.Rows[0].Field<string>("BusinessType");
              objLdgr.ContactPerson = ds.Rows[0].Field<string>("ContactPerson");
              objLdgr.CountryID = ds.Rows[0].Field<int>("CountryID");
              objLdgr.CurrencyID = ds.Rows[0].Field<int>("CurrencyID");
              objLdgr.Email = ds.Rows[0].Field<string>("Email");
              objLdgr.Fax = ds.Rows[0].Field<string>("Fax");
              objLdgr.Phone = ds.Rows[0].Field<string>("Phone");
              objLdgr.Remarks = ds.Rows[0].Field<string>("Remarks");
              objLdgr.TeamMemberID = ds.Rows[0].Field<object>("TeamID")==null?-1:ds.Rows[0].Field<int>("TeamID");
              objLdgr.AccountID = ds.Rows[0].Field<object>("AccountID") == null ? -1 : ds.Rows[0].Field<int>("AccountID"); ;
          }
          catch (Exception ex)
          {
              throw ex;
          }
          return objLdgr;
      }
    }
}
