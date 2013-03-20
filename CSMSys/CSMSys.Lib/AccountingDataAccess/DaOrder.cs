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
    public class DaOrder
    {
        public DaOrder() { }

        public int saveUpdateOrderMaster(Order_Master obOrder_Master, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            int lastID = 0;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction("11");
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdateOrder_Master";
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@OrderMID", SqlDbType.Int).Value = obOrder_Master.OrderMID;
                com.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100).Value = obOrder_Master.OrderNo;
                com.Parameters.Add("@OrderType", SqlDbType.VarChar, 50).Value = obOrder_Master.OrderType;
                com.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = obOrder_Master.OrderDate;
                com.Parameters.Add("@CustomerID", SqlDbType.Int).Value = obOrder_Master.CustomerID;
                if (obOrder_Master.DeliveryDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = obOrder_Master.DeliveryDate;

                if (obOrder_Master.FactoryID <= 0)
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = obOrder_Master.FactoryID;

                com.Parameters.Add("@LedgerNo", SqlDbType.VarChar, 100).Value = obOrder_Master.LedgerNo;
                com.Parameters.Add("@TotalOrderQty", SqlDbType.Money).Value = obOrder_Master.TotalOrderQty;

                if (obOrder_Master.UnitID <= 0)
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obOrder_Master.UnitID;

                com.Parameters.Add("@OrderValue", SqlDbType.Money).Value = obOrder_Master.OrderValue;
                if (obOrder_Master.CurrencyID <= 0)
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obOrder_Master.CurrencyID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.ExecuteNonQuery();
                trans.Commit();
                if (obOrder_Master.OrderMID == 0)
                    lastID = ConnectionHelper.GetID(con, "OrderMID", "Order_Master");
                else
                    lastID = obOrder_Master.OrderMID;
                //com.Parameters["@OrderMID"].Value;
            }
            catch (Exception ex)
            {
                trans.Rollback("11");
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return lastID;
        }
        public int saveUpdateOrder_Details(Order_Details obOrder_Details, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdateOrder_Details";
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@OrderDID", SqlDbType.Int).Value = obOrder_Details.OrderDID;
                com.Parameters.Add("@OrderMID", SqlDbType.Int).Value = obOrder_Details.OrderMID;
                com.Parameters.Add("@OrderQty", SqlDbType.Money).Value = obOrder_Details.OrderQty;
                com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obOrder_Details.ItemID;
                if (obOrder_Details.UnitID <= 0)
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obOrder_Details.UnitID;
                if (obOrder_Details.PriceID <= 0)
                    com.Parameters.Add("@PriceID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@PriceID", SqlDbType.Int).Value = obOrder_Details.PriceID;
                com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obOrder_Details.UnitPrice;
                com.Parameters.Add("@OrderValue", SqlDbType.Money).Value = obOrder_Details.OrderValue;
                
                com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 100).Value = obOrder_Details.ColorCode;
                com.Parameters.Add("@Labdip", SqlDbType.VarChar, 100).Value = obOrder_Details.Labdip;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obOrder_Details.Remarks;

                com.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return 0;
        }
        public int saveUpdateOrderMaster(Order_Master obOrder_Master, SqlTransaction trans, SqlConnection con)
        {
            SqlCommand com = null;

            int lastID = 0;
            try
            {
                com = new SqlCommand("spSaveUpdateOrder_Master", con, trans);

                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@OrderMID", SqlDbType.Int).Value = obOrder_Master.OrderMID;
                com.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100).Value = obOrder_Master.OrderNo;
                com.Parameters.Add("@OrderType", SqlDbType.VarChar, 50).Value = obOrder_Master.OrderType;
                com.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = obOrder_Master.OrderDate;
                com.Parameters.Add("@CustomerID", SqlDbType.Int).Value = obOrder_Master.CustomerID;
                if (obOrder_Master.DeliveryDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = obOrder_Master.DeliveryDate;

                if (obOrder_Master.FactoryID <= 0)
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = obOrder_Master.FactoryID;

                com.Parameters.Add("@LedgerNo", SqlDbType.VarChar, 100).Value = obOrder_Master.LedgerNo;
                com.Parameters.Add("@TotalOrderQty", SqlDbType.Money).Value = obOrder_Master.TotalOrderQty;

                if (obOrder_Master.UnitID <= 0)
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obOrder_Master.UnitID;

                com.Parameters.Add("@OrderValue", SqlDbType.Money).Value = obOrder_Master.OrderValue;
                if (obOrder_Master.CurrencyID <= 0)
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obOrder_Master.CurrencyID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@Rate", SqlDbType.Money).Value = obOrder_Master.Rate;
                com.Parameters.Add("@Buyer_ref", SqlDbType.VarChar, 500).Value = obOrder_Master.Buyer_ref;
                com.ExecuteNonQuery();
                //trans.Commit();
                if (obOrder_Master.OrderMID == 0)
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(OrderMID),0) FROM Order_Master", con, trans);
                    lastID = Convert.ToInt32(cmd.ExecuteScalar());

                }
                else
                    lastID = obOrder_Master.OrderMID;
            }
            catch (Exception ex)
            {
                //trans.Rollback("11");
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return lastID;
        }
        public int saveUpdateOrder_Details(Order_Details obOrder_Details, SqlTransaction trans, SqlConnection con)
        {
          
           
            try
            {
                SqlCommand com = new SqlCommand("spSaveUpdateOrder_Details", con, trans);
               
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@OrderDID", SqlDbType.Int).Value = obOrder_Details.OrderDID;
                com.Parameters.Add("@OrderMID", SqlDbType.Int).Value = obOrder_Details.OrderMID;
                com.Parameters.Add("@OrderQty", SqlDbType.Money).Value = obOrder_Details.OrderQty;
                com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obOrder_Details.ItemID;
                if (obOrder_Details.UnitID <= 0)
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obOrder_Details.UnitID;
                if (obOrder_Details.PriceID <= 0)
                    com.Parameters.Add("@PriceID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@PriceID", SqlDbType.Int).Value = obOrder_Details.PriceID;
                com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obOrder_Details.UnitPrice;
                com.Parameters.Add("@OrderValue", SqlDbType.Money).Value = obOrder_Details.OrderValue;

                com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 100).Value = obOrder_Details.ColorCode;
                com.Parameters.Add("@Labdip", SqlDbType.VarChar, 100).Value = obOrder_Details.Labdip;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obOrder_Details.Remarks;

                if (obOrder_Details.CountID <= 0)
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = obOrder_Details.CountID;
                if (obOrder_Details.SizeID <= 0)
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = obOrder_Details.SizeID;
                if (obOrder_Details.ColorID <= 0)
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = obOrder_Details.ColorID;
                com.ExecuteNonQuery();
               

            }
            catch (Exception ex)
            {
               
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return 0;
        }
        public DataTable loadFactory(int customerID, SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Factory WHERE CustomerID=" + customerID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable loadDetail(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Order_Details", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public DataTable loadItems(SqlConnection con, string sItemName)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spLoadItems", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar, 100).Value = sItemName;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable loadSelectedItems(SqlConnection con, string ItemList)
        {
            SqlDataAdapter da = new SqlDataAdapter(" SELECT  ItemID, ItemName, ColorsName, SizesName, CountName, ShadeNo FROM Vw_Items" +
            " WHERE  CompanyID=@CompanyID AND   ( ItemID IN  " + ItemList + " ) ", con);
            da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;

        }
        public DataTable loadForm(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("spLoadOrderDetail", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrderDID", SqlDbType.Int).Value = -5;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        public DataTable loadOrderNo(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("select OrderMID,OrderNo from Order_Master WHERE CompanyID=@CompanyID ", con);
            da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        //------------------------  Find Sales Order -----------------------------
        public DataTable searchSelectedCustomer(SqlConnection con, DateTime sDate, DateTime eDate, string OrderNo)
        {
            try
            {
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter("spSearchOrder", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100).Value = OrderNo;
                da.SelectCommand.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sDate;
                da.SelectCommand.Parameters.Add("@eDate", SqlDbType.DateTime).Value = eDate;
                da.SelectCommand.Parameters.Add("@OrderType", SqlDbType.VarChar, 100).Value = "Sales Order";
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable Find(SqlConnection con, string OrderNo)
        {
            try
            {

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter("spFindOrder", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100).Value = OrderNo;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable loadUnit(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select UnitsID,UnitsName from P_Units", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteItems(SqlConnection con, int OrderDID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "Delete from Order_Details Where OrderDID = @OrderDID";
                com.Parameters.Add("@OrderDID", SqlDbType.Int).Value = OrderDID;
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
        public void Delete(SqlConnection con, int OrderMasterID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteOrder";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMasterID;
                com.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception("Unable to Delete Order" + ex.Message);
            }
        }
        public double loadPrice(SqlConnection con, int CustomerID, int ItemID,int CountID,int SizeID,int ColorID,DateTime PriceDate)
        {
            try
            {
                double UnitPrice = 0.0;
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand("Select dbo.fnGetPriceOfTime(@CustomerID, @ItemID,@CountID,@SizeID,@ColorID,@PriceDate) AS price", con);
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID;
                cmd.Parameters.Add("@CountID", SqlDbType.Int).Value = CountID;
                cmd.Parameters.Add("@SizeID", SqlDbType.Int).Value = SizeID ;
                cmd.Parameters.Add("@ColorID", SqlDbType.Int).Value = ColorID;
                cmd.Parameters.Add("@PriceDate", SqlDbType.DateTime).Value = PriceDate;

                UnitPrice = Convert.ToDouble(cmd.ExecuteScalar());

                return UnitPrice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Order_Master GetOrder_Master(SqlConnection con, string OrderNo)
        {
            DataTable dt = new DataTable();
            Order_Master obOrderMaster = new Order_Master();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Order_Master WHERE OrderNo=@OrderNo AND CompanyID=@CompanyID", con);
                da.SelectCommand.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100).Value = OrderNo;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0) return null;
                obOrderMaster.OrderMID = dt.Rows[0].Field<int>("OrderMID");
                obOrderMaster.OrderNo = dt.Rows[0].Field<string>("OrderNo");
                obOrderMaster.LedgerNo = dt.Rows[0].Field<object>("LedgerNo") == DBNull.Value || dt.Rows[0].Field<object>("LedgerNo") == null ? "" : dt.Rows[0].Field<string>("LedgerNo");
                obOrderMaster.OrderDate = dt.Rows[0].Field<DateTime>("OrderDate");
                obOrderMaster.OrderValue = Convert.ToDouble(dt.Rows[0].Field<object>("OrderValue"));
                obOrderMaster.TotalOrderQty = dt.Rows[0].Field<object>("TotalOrderQty") == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0].Field<object>("TotalOrderQty"));
                obOrderMaster.UnitID = dt.Rows[0].Field<object>("UnitID") == DBNull.Value || dt.Rows[0].Field<object>("UnitID") == null ? 0 : dt.Rows[0].Field<int>("UnitID");
                obOrderMaster.FactoryID = dt.Rows[0].Field<object>("FactoryID") == DBNull.Value || dt.Rows[0].Field<object>("FactoryID") == null ? -1 : dt.Rows[0].Field<int>("FactoryID");
                obOrderMaster.DeliveryDate = dt.Rows[0].Field<object>("DeliveryDate") == DBNull.Value || dt.Rows[0].Field<object>("DeliveryDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("DeliveryDate");
                obOrderMaster.CustomerID = dt.Rows[0].Field<int>("CustomerID");
                obOrderMaster.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
                obOrderMaster.CurrencyID = dt.Rows[0].Field<object>("CurrencyID") == DBNull.Value || dt.Rows[0].Field<object>("CurrencyID") == null ? -1 : dt.Rows[0].Field<int>("CurrencyID");
                obOrderMaster.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));
                obOrderMaster.Buyer_ref = dt.Rows[0].Field<string>("Buyer_ref");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obOrderMaster;
        }
        public Order_Master GetOrder_Master(SqlConnection con, int OrderMasterID)
        {
            DataTable dt = new DataTable();
            Order_Master obOrderMaster = new Order_Master();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Order_Master WHERE OrderMID=@OrderMID ", con);
                da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMasterID;
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0) return null;
                obOrderMaster.OrderMID = dt.Rows[0].Field<int>("OrderMID");
                obOrderMaster.OrderNo = dt.Rows[0].Field<string>("OrderNo");
                obOrderMaster.LedgerNo = dt.Rows[0].Field<object>("LedgerNo") == DBNull.Value || dt.Rows[0].Field<object>("LedgerNo") == null ? "" : dt.Rows[0].Field<string>("LedgerNo");
                obOrderMaster.OrderDate = dt.Rows[0].Field<DateTime>("OrderDate");
                obOrderMaster.OrderValue = Convert.ToDouble(dt.Rows[0].Field<object>("OrderValue"));
                obOrderMaster.TotalOrderQty = dt.Rows[0].Field<object>("TotalOrderQty") == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0].Field<object>("TotalOrderQty"));
                obOrderMaster.UnitID = dt.Rows[0].Field<object>("UnitID") == DBNull.Value || dt.Rows[0].Field<object>("UnitID") == null ? 0 : dt.Rows[0].Field<int>("UnitID");
                obOrderMaster.FactoryID = dt.Rows[0].Field<object>("FactoryID") == DBNull.Value || dt.Rows[0].Field<object>("FactoryID") == null ? -1 : dt.Rows[0].Field<int>("FactoryID");
                obOrderMaster.DeliveryDate = dt.Rows[0].Field<object>("DeliveryDate") == DBNull.Value || dt.Rows[0].Field<object>("DeliveryDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("DeliveryDate");
                obOrderMaster.CustomerID = dt.Rows[0].Field<int>("CustomerID");
                obOrderMaster.CurrencyID = dt.Rows[0].Field<object>("CurrencyID") == DBNull.Value || dt.Rows[0].Field<object>("CurrencyID") == null ? -1 : dt.Rows[0].Field<int>("CurrencyID");
                obOrderMaster.Buyer_ref = dt.Rows[0].Field<string>("Buyer_ref");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obOrderMaster;
        }
        public DataTable getOrders(SqlConnection con, string Cols, string Where, DateTime st, DateTime ed)
        {
            DataTable dt = null;
            try
            {
                string qstr = "SELECT " + Cols + ",(CASE WHEN ISNULL(T_PI_Details.PIDID,0) <=0 THEN NULL ELSE 'PI Opened' END) AS Status FROM Order_Master LEFT OUTER JOIN T_PI_Details ON Order_Master.OrderMID = T_PI_Details.OrderID " + Where + " Order By OrderNo";

                SqlDataAdapter da = new SqlDataAdapter(qstr, con);

                if (st != new DateTime(1900, 1, 1) && ed != new DateTime(1900, 1, 1))
                {
                    da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = st;
                    da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = ed;
                }

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
        public DataTable getOrderItems(SqlConnection con, int OrderMasterID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSelectItemsOfanOrder", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMasterID;
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

        public DataTable SelectOrNewAmendment(SqlConnection con, int OrderID, int AmendID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSelectOrNewAmendment", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderID;
                da.SelectCommand.Parameters.Add("@AmendID", SqlDbType.Int).Value = AmendID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public void UpdateOrderItemAmendment(SqlConnection con, SqlTransaction trans, int OrderMID, int OrderDID, int ItemID, double AmendQty, double AmendValue)
        {
            try
            {
                string qstr;


                //qstr = "UPDATE  Order_Details_A SET  AmendQty = @AmendQty, AmendValue = @AmendValue "
                //                 + " WHERE AmendID=(SELECT MAX(AmendID) FROM Order_Master_A) AND (OrderDID = @OrderDID) AND (OrderMID = @OrderMID)";

                qstr = "spInsertUpdateAmendmentItems";
                SqlCommand cmd = new SqlCommand(qstr, con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMID;
                cmd.Parameters.Add("@OrderDID", SqlDbType.Int).Value = OrderDID;
                cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID;
                cmd.Parameters.Add("@AmendQty", SqlDbType.Money).Value = AmendQty;
                cmd.Parameters.Add("@AmendValue", SqlDbType.Money).Value = AmendValue;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateOrderDetail(SqlConnection con, SqlTransaction trans, int OrderMID, int OrderDID, double OrderQty, double uPrice, double OrderValue)
        {
            try
            {
                string qstr = "UPDATE Order_Details SET OrderQty = @OrderQty, UnitPrice = @UnitPrice, OrderValue = @OrderValue "
                            + " WHERE  (OrderDID = @OrderDID) AND (OrderMID = @OrderMID)";
                SqlCommand cmd = new SqlCommand(qstr, con, trans);
                cmd.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMID;
                cmd.Parameters.Add("@OrderDID", SqlDbType.Int).Value = OrderDID;
                cmd.Parameters.Add("@OrderQty", SqlDbType.Money).Value = OrderQty;
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = uPrice;
                cmd.Parameters.Add("@OrderValue", SqlDbType.Money).Value = OrderValue;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateAmendment(SqlConnection con, SqlTransaction trans, int Stage, int OrderMID, DateTime AmendDate, string AmendComment, double TtlAQty, double TtlAValue)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spDoneOrderAmendment", con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@stage", SqlDbType.Int).Value = Stage;
                cmd.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMID;
                cmd.Parameters.Add("@AmendDate", SqlDbType.DateTime).Value = AmendDate;
                cmd.Parameters.Add("@AmendComment", SqlDbType.VarChar, 500).Value = AmendComment;
                cmd.Parameters.Add("@TotalAmendQty", SqlDbType.Money).Value = TtlAQty;
                cmd.Parameters.Add("@TotalAmendValue", SqlDbType.Money).Value = TtlAValue;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getAmendments(SqlConnection con, int OrderMID)
        {
            DataTable dt = new DataTable();
            try
            {
                string qstr = "SELECT AmendID,OrderMID,OrderNo,AmendDate,AmendComment FROM Order_Master_A WHERE OrderMID = " + OrderMID.ToString();
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable LoadCurrency(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Currency where CompanyID=" + LogInInfo.CompanyID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable loadItem(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select ItemID,ItemName from T_Item where CompanyID="+LogInInfo.CompanyID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable loadCount(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select CountID,CountName from T_Count where CompanyID=" + LogInInfo.CompanyID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable loadColor(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select ColorsID,ColorsName from P_Colors where CompanyID=" + LogInInfo.CompanyID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable loadSize(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select SizesID,SizesName from P_Sizes where CompanyID=" + LogInInfo.CompanyID.ToString(), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public int SuppCusCurrency(SqlConnection con, int SupCusID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                int ab;
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "select CurrencyID from T_Ledgers where LedgerID=@SupCusID";
                com.Parameters.Add("@SupCusID", SqlDbType.Int).Value = SupCusID;
               ab=Convert.ToInt32( com.ExecuteScalar());
                trans.Commit();
                return ab;
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
 
        }

    }
}
