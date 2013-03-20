using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingUtility;
using System.Data;
using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaCompanySettings
    {
        public DaCompanySettings() { }

        private static int PI_SL_NO_DIGIT = 3;
        private static string getPrefix(string PrefixCode,int CompanyID)
        {

            try
            {
                SqlConnection con = ConnectionHelper.getConnection();
                SqlCommand cmd = new SqlCommand("SELECT SettingValue FROM T_RefNoSettings WHERE (SettingCode LIKE @code) AND (CompanyID = @CompanyID) ", con);
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = PrefixCode;
                cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
                object obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return string.Empty;
                else
                    return obj.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static bool IsInventoryWithAccounting()
        {

            try
            {
                SqlConnection con = ConnectionHelper.getConnection();
                SqlCommand cmd = new SqlCommand("SELECT SettingValue FROM T_RefNoSettings WHERE (SettingCode LIKE @code) AND (CompanyID = @CompanyID) ", con);
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = "INV_ACC";
                cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                object obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return false;
                else
                    return (obj.ToString()=="YES");
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public   string getSettingValue(string PrefixCode, int CompanyID)
        {

            try
            {
                SqlConnection con = ConnectionHelper.getConnection();
                SqlCommand cmd = new SqlCommand("SELECT SettingValue FROM T_RefNoSettings WHERE (SettingCode LIKE @code) AND (CompanyID = @CompanyID) ", con);
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 50).Value = PrefixCode;
                cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
                object obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    return string.Empty;
                else
                    return obj.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataTable getSettings(SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_RefNoSettings WHERE CompanyID=" + LogInInfo.CompanyID.ToString(), con);
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void SaveUpdateSettings(SqlConnection con, SqlTransaction trans, CompanySettings s)
        {
            try
            {
                string qstr;

                if (s.SlNo == 0)
                {
                    qstr = "INSERT INTO T_RefNoSettings (SettingCode,SettingTitle,SettingValue,CompanyID) VALUES ('" + s.SettingCode + "','" + s.SettingTitle + "','" + s.SettingValue + "'," + LogInInfo.CompanyID.ToString() + ")";
                }
                else
                    qstr = "UPDATE T_RefNoSettings SET SettingCode='" + s.SettingCode + "',SettingTitle='" + s.SettingTitle + "',SettingValue='" + s.SettingValue + "' WHERE SLNO=" + s.SlNo.ToString();

                SqlCommand cmd = new SqlCommand(qstr, con, trans);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteSettings(SqlConnection con, int slNo)
        {
            try
            {
                string qstr;


                qstr = "DELETE FROM T_RefNoSettings WHERE SlNo=" + slNo.ToString();
                
                SqlCommand cmd = new SqlCommand(qstr, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteSettings(SqlConnection con,SqlTransaction trans)
        {
            try
            {
                string qstr;


                qstr = "DELETE FROM T_RefNoSettings WHERE CompanyID=" + LogInInfo.CompanyID.ToString();

                SqlCommand cmd = new SqlCommand(qstr, con, trans);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region PI No.
        public static string GeneratePINO()
        {
            string ss;
            try
            {
                string Prefix = getPrefix("PI", LogInInfo.CompanyID);

                string ad = getPISerial(Prefix);

                

                ss = Prefix + ad;
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ss;
        }
        private static string getPISerial(string prefix)
        {
            SqlCommand com = null;

            string str = null;
            try
            {
                SqlConnection con = ConnectionHelper.getConnection();
                com = new SqlCommand();

                com.Connection = con;
                //com.CommandText = "select  ltrim(isnull(count(PINO),0)+1)as NN from T_PI_Master WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " AND PINO LIKE '" + prefix+"'";
                com.CommandText = "SELECT COUNT(PINO) FROM T_PI_Master WHERE (CompanyID = @CompanyID) AND (MONTH(PIDate) = MONTH(@MyDate)) AND (YEAR(PIDate) = YEAR(@MyDate))";
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@MyDate", SqlDbType.DateTime).Value = DateTime.Now.Date;
                object obj = com.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    str = "1";
                else
                    str = (Convert.ToInt32(obj) + 1).ToString();
                str = DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString().Substring(2,2) + str.PadLeft(PI_SL_NO_DIGIT, '0');
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return str;
        }
        #endregion

        #region Chalan No.
        public static string GenerateChalanNO()
        {
            string ss;
            try
            {
                string Prefix = getPrefix("CN-Sales", LogInInfo.CompanyID);

                string ad = getChallanSerial(Prefix);



                ss = Prefix + ad;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ss;
        }
        private static string getChallanSerial(string prefix)
        {
            SqlCommand com = null;

            string str = null;
            try
            {
                SqlConnection con = ConnectionHelper.getConnection();
                com = new SqlCommand();

                com.Connection = con;
                
                com.CommandText = "SELECT COUNT(ChalanNo) FROM T_Sales_Invoice WHERE (CompanyID = @CompanyID) AND (MONTH(InvoiceDate) = MONTH(@MyDate)) AND (YEAR(InvoiceDate) = YEAR(@MyDate))";
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@MyDate", SqlDbType.DateTime).Value = DateTime.Now.Date;
                object obj = com.ExecuteScalar();
                if (obj == null || obj == DBNull.Value)
                    str = "1";
                else
                    str = (Convert.ToInt32(obj) + 1).ToString();
                str = DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString().Substring(2, 2) + str.PadLeft(PI_SL_NO_DIGIT, '0');
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return str;
        }
        #endregion
        
    }
}
