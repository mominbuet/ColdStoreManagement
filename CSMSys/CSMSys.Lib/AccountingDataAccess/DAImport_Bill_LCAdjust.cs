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
   public class DAImport_Bill_LCAdjust
    {
       public DAImport_Bill_LCAdjust() { }


       public int SaveUpdateImport_Bill(SqlConnection con, SqlTransaction trans, Import_Bill_LCAdjust objImportBill)
       {
           int ID = 0;
           try
           {
               SqlCommand cmd = new SqlCommand("spSaveUpdateImport_BillLCAdjust", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@AdjustID", SqlDbType.Int).Value = objImportBill.AdjustID;
               cmd.Parameters.Add("@BillPayID", SqlDbType.VarChar, 100).Value = objImportBill.BillPayID;
               cmd.Parameters.Add("@AdjustDate", SqlDbType.Int).Value = objImportBill.AdjustDate;
               cmd.Parameters.Add("@AdjustAmount", SqlDbType.Money).Value = objImportBill.AdjustAmount;
               cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objImportBill.CurrencyID;
               cmd.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = objImportBill.CurrencyRate;
               cmd.Parameters.Add("@PayFromAccID", SqlDbType.Int).Value = objImportBill.PayFromAccID;
               cmd.Parameters.Add("@AdjustFromAccID", SqlDbType.Int).Value = objImportBill.AdjustFromAccID;
               cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objImportBill.TransRefID;
               cmd.Parameters.Add("@LCID", SqlDbType.Int).Value = objImportBill.LCID;
               cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objImportBill.Remarks;
              
               cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
              // cmd.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objImportBill.ModifiedDate;


               cmd.ExecuteNonQuery();


               if (objImportBill.AdjustID ==0)
               {
                   SqlCommand com = new SqlCommand("SELECT ISNULL(MAX(AdjustID),0) FROM T_Import_BillLCAdjust", con, trans);
                   ID = Convert.ToInt32(com.ExecuteScalar());
               }
               else
                   ID = objImportBill.AdjustID;
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return ID;
       }
    }
}
