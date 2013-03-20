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
    public class DaStockInOut
    {
        public DaStockInOut() { }

        int VoucherNoDigitLength = 6;
        public int SaveUpdateStockInOutMaster(Stock_InOut_Master obInOutMaster, SqlConnection con, SqlTransaction trans)
        {
            int StockMID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateStockInOutMaster", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@StockMID", SqlDbType.Int).Value = obInOutMaster.StockMID;
                com.Parameters.Add("@TransType", SqlDbType.VarChar, 100).Value = obInOutMaster.TransType;
                com.Parameters.Add("@TransDate", SqlDbType.DateTime).Value = obInOutMaster.TransDate;
                com.Parameters.Add("@VoucherNo", SqlDbType.VarChar, 100).Value = obInOutMaster.VoucherNo;
                if (obInOutMaster.ChalanNo == "")
                    com.Parameters.Add("@ChalanNo", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                    com.Parameters.Add("@ChalanNo", SqlDbType.VarChar, 100).Value = obInOutMaster.ChalanNo;
                if (obInOutMaster.ChalanDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@ChalanDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@ChalanDate", SqlDbType.DateTime).Value = obInOutMaster.ChalanDate;
                if (obInOutMaster.CustSupplID <= 0)
                    com.Parameters.Add("@CustSupplID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CustSupplID", SqlDbType.Int).Value = obInOutMaster.CustSupplID;
                if (obInOutMaster.RefID <= 0)
                    com.Parameters.Add("@RefID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@RefID", SqlDbType.Int).Value = obInOutMaster.RefID;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obInOutMaster.Remarks;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@Module", SqlDbType.VarChar, 500).Value = obInOutMaster.Module;
                com.ExecuteNonQuery();
                if (obInOutMaster.StockMID <= 0)
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(StockMID),0) FROM T_Stock_InOut_Master", con, trans);
                    StockMID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    StockMID = obInOutMaster.StockMID;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return StockMID;
        }
        public void SaveUpdateStockInOutDetail(Stock_InOut_Detail obInOutDetail, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateStockInOutDetail", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@StockDID", SqlDbType.Int).Value = obInOutDetail.StockDID;
                com.Parameters.Add("@StockMID", SqlDbType.Int).Value = obInOutDetail.StockMID;
                com.Parameters.Add("@TransNature", SqlDbType.VarChar, 100).Value = obInOutDetail.TransNature;

                com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obInOutDetail.ItemID;
                com.Parameters.Add("@InQty", SqlDbType.Money).Value = obInOutDetail.InQty;
                com.Parameters.Add("@OutQty", SqlDbType.Money).Value = obInOutDetail.OutQty;
                com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obInOutDetail.UnitPrice;
                com.Parameters.Add("@InAmount", SqlDbType.Money).Value = obInOutDetail.InAmount;
                com.Parameters.Add("@OutAmount", SqlDbType.Money).Value = obInOutDetail.OutAmount;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obInOutDetail.Remarks;
                com.Parameters.Add("@ShortQty", SqlDbType.Int).Value = obInOutDetail.ShortQty;
                com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 500).Value = obInOutDetail.ColorCode;
                com.Parameters.Add("@Labdip", SqlDbType.VarChar, 500).Value = obInOutDetail.Labdip;

                if (obInOutDetail.CountID <= 0)
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = obInOutDetail.CountID;
                if (obInOutDetail.SizeID <= 0)
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = obInOutDetail.SizeID;
                if (obInOutDetail.ColorID <= 0)
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = obInOutDetail.ColorID;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string getVoucherNo(SqlConnection con, string preFix)
        {
            string Vno = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL((SELECT SUBSTRING(MAX(VoucherNo), 4, 50) + 1 FROM T_Stock_InOut_Master WHERE (VoucherNo LIKE @prefix + '%')),1)", con);
                cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 10).Value = preFix;
                Vno = cmd.ExecuteScalar().ToString();
                Vno = preFix.ToUpper() + "-" + Vno.PadLeft(VoucherNoDigitLength, '0');

            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
            return Vno;
        }

        public string getVoucherNo(SqlConnection con, SqlTransaction trans, string preFix)
        {
            string Vno = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL((SELECT SUBSTRING(MAX(VoucherNo), 3, 50) + 1 FROM T_Stock_InOut_Master WHERE (VoucherNo LIKE @prefix + '%')),1)", con, trans);
                cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 10).Value = preFix;
                Vno = cmd.ExecuteScalar().ToString();
                Vno = preFix.ToUpper() + "-" + Vno.PadLeft(VoucherNoDigitLength, '0');

            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
            return Vno;
        }

        public string getChalanNo(SqlConnection con, string preFix)
        {
            string Vno = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL((SELECT SUBSTRING(MAX(ChalanNo), 3, 50) + 1 FROM T_Stock_InOut_Master WHERE (ChalanNo LIKE @prefix + '%')),1)", con);
                cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 10).Value = preFix;
                Vno = cmd.ExecuteScalar().ToString();
                Vno = preFix.ToUpper() + "-" + Vno.PadLeft(VoucherNoDigitLength, '0');

            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
            return Vno;
        }

        public string getChalanNo(SqlConnection con, SqlTransaction trans, string preFix)
        {
            string Vno = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL((SELECT SUBSTRING(MAX(ChalanNo), 3, 50) + 1 FROM T_Stock_InOut_Master WHERE (ChalanNo LIKE @prefix + '%')),1)", con, trans);
                cmd.Parameters.Add("@prefix", SqlDbType.VarChar, 10).Value = preFix;
                Vno = cmd.ExecuteScalar().ToString();
                Vno = preFix.ToUpper() + "-" + Vno.PadLeft(VoucherNoDigitLength, '0');

            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
            return Vno;
        }

        public Stock_InOut_Master getStockInOutMaster(SqlConnection con, int StockMID)
        {
            Stock_InOut_Master objSM = null;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Stock_InOut_Master WHERE StockMID=" + StockMID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0) return null;
                objSM = new Stock_InOut_Master();
                objSM.StockMID = Convert.ToInt32(dt.Rows[0].Field<object>("StockMID"));
                objSM.TransType = dt.Rows[0].Field<string>("TransType");
                objSM.TransDate = dt.Rows[0].Field<DateTime>("TransDate");
                objSM.VoucherNo = dt.Rows[0].Field<string>("VoucherNo");
                objSM.ChalanNo = dt.Rows[0].Field<string>("ChalanNo");
                objSM.ChalanDate = dt.Rows[0].Field<object>("ChalanDate") == DBNull.Value || dt.Rows[0].Field<object>("ChalanDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("ChalanDate");
                objSM.CustSupplID = dt.Rows[0].Field<object>("CustSupplID") == DBNull.Value || dt.Rows[0].Field<object>("CustSupplID") == null ? -1 : dt.Rows[0].Field<int>("CustSupplID");
                objSM.RefID = dt.Rows[0].Field<object>("RefID") == DBNull.Value || dt.Rows[0].Field<object>("RefID") == null ? -1 : dt.Rows[0].Field<int>("RefID");
                objSM.Remarks = dt.Rows[0].Field<object>("Remarks") == DBNull.Value || dt.Rows[0].Field<object>("Remarks") == null ? string.Empty : dt.Rows[0].Field<string>("Remarks");
                objSM.Module = dt.Rows[0].Field<object>("Module") == DBNull.Value || dt.Rows[0].Field<object>("Module") == null ? string.Empty : dt.Rows[0].Field<string>("Module");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objSM;
        }
        public DataTable getStockItems(SqlConnection con, int StockMID)
        {
            DataTable dt = null;
            try
            {
                string qstr = "SELECT  SD.StockDID,SD.StockMID,SD.TransNature,SD.ItemID,I.ItemName,I.CountName,I.SizesName,I.ColorsName,SD.ColorCode,SD.Labdip,I.ShadeNo,I.UnitsName,I.GroupName,SD.InQty, SD.OutQty, SD.UnitPrice,SD.InAmount, SD.OutAmount, SD.Remarks , SD.ShortQty "
                               + "FROM  T_Stock_InOut_Detail AS SD INNER JOIN  VW_Items AS I ON SD.ItemID = I.ItemID " + "WHERE StockMID=" + StockMID.ToString();
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public int getIODID(SqlConnection con, SqlTransaction trans, int IOMID, int ItemID)
        {
            int IODID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("Select dbo.fnGetInOutDID(@StockMID,@ItemID)", con, trans);
                com.Parameters.Add("@StockMID", SqlDbType.Int).Value = IOMID;
                com.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID;
                IODID = (int)com.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return IODID;
        }
        public DataTable searchStockInOut(SqlConnection con, string VoucherNo, DateTime sDate, DateTime eDate)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSearchStockInOut", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@VoucherNo", SqlDbType.VarChar, 100).Value = VoucherNo;
                da.SelectCommand.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sDate;
                da.SelectCommand.Parameters.Add("@eDate", SqlDbType.DateTime).Value = eDate;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public void DeleteStockInOut(SqlConnection con, int StockMID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteStockInOut";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@StockMID", SqlDbType.Int).Value = StockMID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public void DeleteStockInOut(SqlConnection con, SqlTransaction trans,int StockMID)
        {
            SqlCommand com = null;
           
            try
            {
                com = new SqlCommand();
               
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteStockInOut";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@StockMID", SqlDbType.Int).Value = StockMID;
                com.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        //public Stock_InOut_Master getInOutMaster(SqlConnection con, int StockMID)
        //{
        //    Stock_InOut_Master obIOM = new Stock_InOut_Master();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter("Select * from T_Stock", con);
        //        da.Fill(dt);
        //        da.Dispose();
        //        if (dt.Rows.Count == 0) return null;
        //        obIOM.StockMID = Convert.ToInt32(dt.Rows[0].Field<object>("StockMID"));
        //        obIOM.TransType = dt.Rows[0].Field<object>("TransType").ToString();
        //        obIOM.TransDate = dt.Rows[0].Field<DateTime>("TransDate");
        //        obIOM.VoucherNo = dt.Rows[0].Field<object>("VoucherNo").ToString();
        //        obIOM.ChalanNo = dt.Rows[0].Field<object>("ChalanNo").ToString();
        //        obIOM.ChalanDate = dt.Rows[0].Field<DateTime>("ChalanDate");
        //        obIOM.CustSupplID = Convert.ToInt32(dt.Rows[0].Field<object>("CustSupplID"));
        //        obIOM.RefID = Convert.ToInt32(dt.Rows[0].Field<object>("RefID"));
        //        obIOM.Remarks = dt.Rows[0].Field<object>("Remarks").ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return obIOM;
        //}

        public DataTable getOrderOrStockItems(SqlConnection con, int StockMID, int OrderMasterID, string Nature)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSelectItemOfOrderOrStock", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@StockMID", SqlDbType.Int).Value = StockMID;
                da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMasterID;
                da.SelectCommand.Parameters.Add("@TransNature", SqlDbType.VarChar, 50).Value = Nature;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            return dt;
        }
        public DataTable getOrderOrStockItems4CustomerIn(SqlConnection con, int StockMID, int OrderMasterID, string Nature)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSelectItemOfOrderOrStock4CustomerIn", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@StockMID", SqlDbType.Int).Value = StockMID;
                da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMasterID;
                da.SelectCommand.Parameters.Add("@TransNature", SqlDbType.VarChar, 50).Value = Nature;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            return dt;
        }
        public DataTable getOrderOrStockItems4CustomerOut(SqlConnection con, int StockMID, int OrderMasterID, string Nature)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSelectItemOfOrderOrStock4CustomerOut", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@StockMID", SqlDbType.Int).Value = StockMID;
                da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMasterID;
                da.SelectCommand.Parameters.Add("@TransNature", SqlDbType.VarChar, 50).Value = Nature;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            return dt;
        }
       
       
        public void DeleteStockInOutDetail(SqlConnection con, SqlTransaction trans, int StockMID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM T_Stock_InOut_Detail WHERE  (StockMID = @StockMID)", con, trans);
                cmd.Parameters.Add("@StockMID", SqlDbType.Int).Value = StockMID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRawMaterialAndFinishGoodsAccountBalance(SqlConnection con,SqlTransaction trans)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("T_spUpdateRawOrFinishBalance", con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
