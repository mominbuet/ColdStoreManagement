using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingEntity;
using System.Data.SqlClient;
using System.Data;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaBillAccount
    {
        public DaBillAccount() { }
        public int SaveUpdateBillAccount(BillAccount obBillAcc, SqlConnection con, SqlTransaction trans)
        {
            int ID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@SlNo", SqlDbType.Int).Value = obBillAcc.SlNo;
                com.Parameters.Add("@BillID", SqlDbType.Int).Value = obBillAcc.BillID;
                com.Parameters.Add("@AccountID", SqlDbType.Int).Value = obBillAcc.AccountID;
                //com.Parameters.Add("@DrCr", SqlDbType.VarChar, 50).Value = obBillAcc.DrCr;
                com.Parameters.Add("@Particulars", SqlDbType.VarChar, 500).Value = obBillAcc.Particulars;
                //com.Parameters.Add("@Amount", SqlDbType.Money).Value = obBillAcc.Amount;
                com.ExecuteNonQuery();
                if (obBillAcc.SlNo == 0)
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(SINo),0) FROM T_Export_Bill", con, trans);
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    ID = obBillAcc.SlNo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }
    }
}
