using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaPriceList
    {
       public DaPriceList() { }


       public int SaveUpdate(PriceList objPL, SqlConnection con)
       {
           int ID = 0;
           SqlTransaction trans = null;
           try
           {

               SqlCommand com = new SqlCommand();

              
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "spInsertUpdatePriceList";
               com.CommandType = CommandType.StoredProcedure;
  
               com.Parameters.Add("@PriceID", SqlDbType.Int).Value = objPL.PriceID;
               if (objPL.CustomerID <= 0)
                   com.Parameters.Add("@customerID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@customerID", SqlDbType.Int).Value = objPL.CustomerID;
               com.Parameters.Add("@itemID", SqlDbType.Int).Value = objPL.ItemID;
               com.Parameters.Add("@unitID", SqlDbType.Int).Value = objPL.UnitID;
               com.Parameters.Add("@price", SqlDbType.Money).Value = objPL.Price;
               com.Parameters.Add("@vat", SqlDbType.Money).Value = objPL.VAT;
               com.Parameters.Add("@setupDate", SqlDbType.DateTime).Value = objPL.SetupDate;
               com.Parameters.Add("@remarks", SqlDbType.VarChar,500).Value = objPL.Remarks;

               if (objPL.CountID <=0)
                   com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@CountID", SqlDbType.Int).Value = objPL.CountID;
               if (objPL.SizeID <=0)
                   com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@SizeID", SqlDbType.Int).Value = objPL.SizeID;
               if (objPL.ColorID <=0)
                   com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ColorID", SqlDbType.Int).Value = objPL.ColorID;

               com.ExecuteNonQuery();
               trans.Commit();
               if (objPL.PriceID == 0)
                   ID = ConnectionHelper.GetID(con, "PriceID", "T_Price_List");
               else
                   ID = objPL.PriceID;
           }
           catch (Exception ex)
           {
               if (trans != null)
                   trans.Rollback();
               throw ex;
           }
           return ID;
       }

       public void DeletePriceList(SqlConnection con, int PriceID)
       {
           SqlTransaction trans = null;
           try
           {

               SqlCommand com = new SqlCommand();

               
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "DELETE FROM T_Price_List WHERE PriceID=@PriceID";
               com.Parameters.Add("@PriceID", SqlDbType.Int).Value = PriceID;
               com.ExecuteNonQuery();
               trans.Commit();
              
           }
           catch (Exception ex)
           {
               if (trans != null)
                   trans.Rollback();
               throw ex;
           }
          
       }
       public DataTable getPriceList(SqlConnection con,string PriceListOf, DateTime sDate,DateTime eDate,int Cid)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("spSelectPriceList", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@PriceListOf", SqlDbType.NVarChar, 100).Value = PriceListOf;
               da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = sDate;
               da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = eDate;
               da.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Cid;
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dt;
       }

       public DataTable getItems(SqlConnection con, string Cols, string items)
       {
           try
           {
               DataTable dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SELECT " + Cols + " FROM T_Item WHERE ItemID IN " + items, con);
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

    }
}
