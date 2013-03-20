using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;
namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaLC
    {
       public DaLC() { }
       public DataTable GetBank(SqlConnection con)
       {
           SqlDataAdapter da = new SqlDataAdapter("select * from T_Ledgers where LedgerTypeID=4 AND CompanyID="+LogInInfo.CompanyID.ToString(), con);
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.Dispose();
           return dt;
       }
       public DataTable GetMasterLCNo(SqlConnection con)
       {
           SqlDataAdapter da = new SqlDataAdapter("select * from T_LC_Master WHERE CompanyID="+LogInInfo.CompanyID.ToString(), con);
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.Dispose();
           return dt;
       }

       public DataTable getPIsOfLC(SqlConnection con, int LC)
       {
           DataTable dt = new DataTable();

           try
           {

               string qstr = "SELECT LCDetailID,PIID FROM T_LC_Detail WHERE LCID=" + LC.ToString();

               SqlDataAdapter da = new SqlDataAdapter(qstr , con);

               da.Fill(dt);
               da.Dispose();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           return dt;

       }
       public DataTable GetLCNo(string Where, SqlConnection con)
       {
           DataTable dt = new DataTable();

           try
           {

               string qstr;
               qstr = "SELECT     0 AS LCDID, PM.PIMID, PM.PINO, OM.OrderNo, I.ItemName, C.CountName Count, S.SizesName Size, T.ColorsName [Prd.Type], OD.ColorCode, OD.Labdip, OD.OrderQty, "+
                      " U.UnitsName Unit, OD.UnitPrice, OD.OrderValue, OD.Remarks [C.Qty] FROM Order_Details AS OD INNER JOIN Order_Master AS OM ON OD.OrderMID = OM.OrderMID INNER JOIN "+
                      " T_PI_Master AS PM INNER JOIN T_PI_Details AS PD ON PM.PIMID = PD.PIMID ON OM.OrderMID = PD.OrderID INNER JOIN T_Item AS I ON OD.ItemID = I.ItemID INNER JOIN " +
                      " P_Units AS U ON I.UnitID = U.UnitsID LEFT OUTER JOIN P_Colors AS T ON OD.ColorID = T.ColorsID LEFT OUTER JOIN T_Count AS C ON OD.CountID = C.CountID LEFT OUTER JOIN P_Sizes AS S ON OD.SizeID = S.SizesID ";
               SqlDataAdapter da = new SqlDataAdapter(qstr + " " + Where + (Where != "" ? " AND " : " WHERE ") + " OM.CompanyID=" + LogInInfo.CompanyID.ToString() + " ORDER BY PM.PINO, OM.OrderNo, I.ItemName, Count, Size, [Prd.Type] ", con);

               da.Fill(dt);
               da.Dispose();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           return dt;

       }
       public DataTable GetLCNos(string Ignore, DateTime strd, DateTime endd, SqlConnection con)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("spLCInfo", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@std", SqlDbType.DateTime).Value = strd;
               da.SelectCommand.Parameters.Add("@endd", SqlDbType.DateTime).Value = endd;
               da.SelectCommand.Parameters.Add("@strPINo", SqlDbType.VarChar, 100).Value = Ignore;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable GetLCDTL(int LCMID, SqlConnection con)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("spLCDTLInfoINDGV", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@LCMID", SqlDbType.Int).Value = LCMID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }


       public DataTable GetLCDTLAmend(int LCMID, SqlConnection con)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("spLCDTLInfoAmendment", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@LCMID", SqlDbType.Int).Value = LCMID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }







       public DataTable GetLCNo(string stLC, DateTime stdate, DateTime enddate, SqlConnection conn)
       {
           throw new NotImplementedException();
       }

       public int SaveUpdateLCMaster(LC_Master objLCMaster, SqlConnection con)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           int ID = 0;
           try
           {
               if (con.State != ConnectionState.Open)
                   con.Open();
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "spInsertUpdateLCMaster";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@LCID", SqlDbType.Int).Value = objLCMaster.LCID;
               com.Parameters.Add("@LCNo", SqlDbType.VarChar, 100).Value = objLCMaster.LCNo;
               com.Parameters.Add("@LCType", SqlDbType.VarChar, 100).Value = objLCMaster.LCType;
               com.Parameters.Add("@MasterLCNo", SqlDbType.VarChar, 100).Value = objLCMaster.MasterLCNo;
               if (objLCMaster.MasterLCID <= 0)
                   com.Parameters.Add("@MasterLCID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@MasterLCID", SqlDbType.Int).Value = objLCMaster.MasterLCID;
               com.Parameters.Add("@LCDate", SqlDbType.DateTime).Value = objLCMaster.LCDate;
               com.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = objLCMaster.ShipmentDate;
               com.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = objLCMaster.ExpiredDate;
               com.Parameters.Add("@DocumentDate", SqlDbType.DateTime).Value = objLCMaster.DocumentDate == new DateTime(1900, 1, 1) ? DBNull.Value :(object) objLCMaster.DocumentDate;
               com.Parameters.Add("@NegotiationDate", SqlDbType.DateTime).Value = objLCMaster.NegotiationDate == new DateTime(1900, 1, 1) ? DBNull.Value :(object) objLCMaster.NegotiationDate;
               com.Parameters.Add("@UDDate", SqlDbType.DateTime).Value = objLCMaster.UDDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.UDDate;

               com.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = objLCMaster.AcceptDate == new DateTime(1900, 1, 1) ? DBNull.Value :(object) objLCMaster.AcceptDate;
               com.Parameters.Add("@FileNo", SqlDbType.VarChar, 100).Value = objLCMaster.FileNo;
               if (objLCMaster.IssuBankID <= 0)
                   com.Parameters.Add("@IssuBankID", SqlDbType.Int).Value = DBNull.Value;
               else
               com.Parameters.Add("@IssuBankID", SqlDbType.Int).Value = objLCMaster.IssuBankID;
               if (objLCMaster.NegotiationBankID <= 0)
                   com.Parameters.Add("@NegotiationBankID", SqlDbType.Int).Value = DBNull.Value;
               else
               com.Parameters.Add("@NegotiationBankID", SqlDbType.Int).Value = objLCMaster.NegotiationBankID;
               com.Parameters.Add("@AtSight", SqlDbType.Int).Value = objLCMaster.AtSight;
               com.Parameters.Add("@TotalQty", SqlDbType.Money).Value = objLCMaster.TotalQty;
               com.Parameters.Add("@TotalValue", SqlDbType.Money).Value = objLCMaster.TotalValue;
               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               if (objLCMaster.UnderLCDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@UnderLCDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@UnderLCDate", SqlDbType.DateTime).Value = objLCMaster.UnderLCDate;
               if (objLCMaster.ActualShipmentDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ActualShipmentDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ActualShipmentDate", SqlDbType.DateTime).Value = objLCMaster.ActualShipmentDate;
               com.Parameters.Add("@PayDate", SqlDbType.DateTime).Value = objLCMaster.PaymentDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.PaymentDate;
               com.Parameters.Add("@ActualPayDate", SqlDbType.DateTime).Value = objLCMaster.ActualPaymentDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.ActualPaymentDate;
               com.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = objLCMaster.CustomerSupplierID;
               com.ExecuteNonQuery();
               trans.Commit();

               if (objLCMaster.LCID == 0)
               {
                   ID = ConnectionHelper.GetID(con, "LCID"," T_LC_Master");
               }
               else 
               {
                   ID = objLCMaster.LCID;
               }
           }
           catch (Exception ex)
           {
               trans.Rollback();
               throw ex;
           }
           return ID;

       }
       public int SaveUpdateLCMaster(LC_Master objLCMaster, SqlConnection con,SqlTransaction trans)
       {
           SqlCommand com = null;
         
           int ID = 0;
           try
           {
              
               com = new SqlCommand();
              
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "spInsertUpdateLCMaster";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@LCID", SqlDbType.Int).Value = objLCMaster.LCID;
               com.Parameters.Add("@LCNo", SqlDbType.VarChar, 100).Value = objLCMaster.LCNo;
               com.Parameters.Add("@LCType", SqlDbType.VarChar, 100).Value = objLCMaster.LCType;
               com.Parameters.Add("@MasterLCNo", SqlDbType.VarChar, 100).Value = objLCMaster.MasterLCNo;
               if (objLCMaster.MasterLCID <= 0)
                   com.Parameters.Add("@MasterLCID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@MasterLCID", SqlDbType.Int).Value = objLCMaster.MasterLCID;
               com.Parameters.Add("@LCDate", SqlDbType.DateTime).Value = objLCMaster.LCDate;
               com.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = objLCMaster.ShipmentDate;
               com.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = objLCMaster.ExpiredDate;
               com.Parameters.Add("@DocumentDate", SqlDbType.DateTime).Value = objLCMaster.DocumentDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.DocumentDate;
               com.Parameters.Add("@NegotiationDate", SqlDbType.DateTime).Value = objLCMaster.NegotiationDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.NegotiationDate;
               com.Parameters.Add("@UDDate", SqlDbType.DateTime).Value = objLCMaster.UDDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.UDDate;

               com.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = objLCMaster.AcceptDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.AcceptDate;
               com.Parameters.Add("@FileNo", SqlDbType.VarChar, 100).Value = objLCMaster.FileNo;
               if (objLCMaster.IssuBankID <= 0)
                   com.Parameters.Add("@IssuBankID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@IssuBankID", SqlDbType.Int).Value = objLCMaster.IssuBankID;
               if (objLCMaster.NegotiationBankID <= 0)
                   com.Parameters.Add("@NegotiationBankID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@NegotiationBankID", SqlDbType.Int).Value = objLCMaster.NegotiationBankID;
               com.Parameters.Add("@AtSight", SqlDbType.VarChar,50).Value = objLCMaster.AtSight;
               com.Parameters.Add("@TotalQty", SqlDbType.Money).Value = objLCMaster.TotalQty;
               com.Parameters.Add("@TotalValue", SqlDbType.Money).Value = objLCMaster.TotalValue;
               if (objLCMaster.CurrencyID <= 0)
                   com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objLCMaster.CurrencyID;
               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               if (objLCMaster.UnderLCDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@UnderLCDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@UnderLCDate", SqlDbType.DateTime).Value = objLCMaster.UnderLCDate;
               if (objLCMaster.ActualShipmentDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ActualShipmentDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ActualShipmentDate", SqlDbType.DateTime).Value = objLCMaster.ActualShipmentDate;
               com.Parameters.Add("@PayDate", SqlDbType.DateTime).Value = objLCMaster.PaymentDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.PaymentDate;
               com.Parameters.Add("@ActualPayDate", SqlDbType.DateTime).Value = objLCMaster.ActualPaymentDate == new DateTime(1900, 1, 1) ? DBNull.Value : (object)objLCMaster.ActualPaymentDate;
               com.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = objLCMaster.CustomerSupplierID;
               com.Parameters.Add("@Rate", SqlDbType.Money).Value = objLCMaster.Rate;
               com.Parameters.Add("@LcUnit", SqlDbType.VarChar, 100).Value = objLCMaster.LcUnit;
               com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objLCMaster.LCDescription;
               com.ExecuteNonQuery();
             

               if (objLCMaster.LCID == 0)
               {
                   ID = ConnectionHelper.GetIDForInsert(con,trans, "LCID", " T_LC_Master");
               }
               else
               {
                   ID = objLCMaster.LCID;
               }
           }
           catch (Exception ex)
           {
              
               throw ex;
           }
           return ID;

       }

       public int SaveUpdateLCDetail(LC_Detail objLCDTL, SqlConnection con)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               if (con.State != ConnectionState.Open)
                   con.Open();
               trans = con.BeginTransaction();

               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "spInsertUpdateLCDetail";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@LCDetailID", SqlDbType.Int).Value = objLCDTL.LCDetailID;
               com.Parameters.Add("@LCID", SqlDbType.Int).Value = objLCDTL.LCID;
               com.Parameters.Add("@PIID", SqlDbType.Int).Value = objLCDTL.PIID;
               com.ExecuteNonQuery();
               trans.Commit();

           }
           catch (Exception ex)
           {
               throw ex;
           }
           return objLCDTL.LCDetailID;
       }

       public int SaveUpdateLCDetail(LC_Detail objLCDTL, SqlConnection con,SqlTransaction trans)
       {
           SqlCommand com = null;
          
           try
           {
               com = new SqlCommand();

               
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "spInsertUpdateLCDetail";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@LCDetailID", SqlDbType.Int).Value = objLCDTL.LCDetailID;
               com.Parameters.Add("@LCID", SqlDbType.Int).Value = objLCDTL.LCID;
               com.Parameters.Add("@PIID", SqlDbType.Int).Value = objLCDTL.PIID;
               com.ExecuteNonQuery();
          

           }
           catch (Exception ex)
           {
               throw ex;
           }
           return objLCDTL.LCDetailID;
       }


       public DataTable GetLCNO(SqlConnection con)
       {
           SqlDataAdapter da = new SqlDataAdapter("select * from T_LC_Master WHERE CompanyID="+ LogInInfo.CompanyID.ToString(), con);
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.Dispose();
           return dt;
       }


       public LC_Master GetLC_Master(int LC_MasterID, SqlConnection con)
       {
           LC_Master objLCm = new LC_Master();
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_LC_Master where LCID=@LCMasterID", con);

               da.SelectCommand.Parameters.Add("@LCMasterID", SqlDbType.Int).Value = LC_MasterID;

               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0) return null;
               objLCm.LCID = dt.Rows[0].Field<int>("LCID");

               objLCm.LCType = dt.Rows[0].Field<string>("LCType");
               objLCm.MasterLCID = dt.Rows[0].Field<object>("MasterLCID")==DBNull.Value || dt.Rows[0].Field<object>("MasterLCID") == null ? -1 : dt.Rows[0].Field<int>("MasterLCID");
               objLCm.MasterLCNo = dt.Rows[0].Field<string>("MasterLCNo");
               objLCm.LCNo = dt.Rows[0].Field<string>("LCNo");
               objLCm.LCDate = dt.Rows[0].Field<DateTime>("LCDate");
               objLCm.AcceptDate = dt.Rows[0].Field<object>("AcceptDate") == DBNull.Value || dt.Rows[0].Field<object>("AcceptDate") == null ?new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("AcceptDate");
              objLCm.AtSight= dt.Rows[0].Field<string>("Asights");
               //objLCm.AtSight = dt.Rows[0].Field<object>("ASights")==DBNull.Value||dt.Rows[0].Field<object>("Asights")==null?0:dt.Rows[0].Field<string>("Asights");
               objLCm.DocumentDate = dt.Rows[0].Field<object>("DocumentDate") == DBNull.Value || dt.Rows[0].Field<object>("DocumentDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("DocumentDate");
               objLCm.ExpiredDate = dt.Rows[0].Field<object>("ExpireDate") == DBNull.Value || dt.Rows[0].Field<object>("ExpireDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("ExpireDate");
               objLCm.FileNo = dt.Rows[0].Field<string>("FileNo");
               objLCm.IssuBankID = dt.Rows[0].Field<object>("IssueBank") == DBNull.Value || dt.Rows[0].Field<object>("IssueBank") == null ? -1 : dt.Rows[0].Field<int>("IssueBank");
               objLCm.NegotiationBankID = dt.Rows[0].Field<object>("NegotiateBank") == DBNull.Value || dt.Rows[0].Field<object>("NegotiateBank") == null ? -1 : dt.Rows[0].Field<int>("NegotiateBank");
               objLCm.NegotiationDate = dt.Rows[0].Field<object>("NegotiationDate") == DBNull.Value || dt.Rows[0].Field<object>("NegotiationDate") == null ? new DateTime(1900,1,1) : dt.Rows[0].Field<DateTime>("NegotiationDate");
               objLCm.ShipmentDate = dt.Rows[0].Field<object>("ShipmentDate") == DBNull.Value || dt.Rows[0].Field<object>("ShipmentDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("ShipmentDate");
               objLCm.UDDate = dt.Rows[0].Field<object>("UDDate") == DBNull.Value || dt.Rows[0].Field<object>("UDDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("UDDate");
               objLCm.TotalQty = dt.Rows[0].Field<object>("TotalQty") == DBNull.Value || dt.Rows[0].Field<object>("TotalQty") == null ? 0 : Convert.ToDouble(dt.Rows[0].Field<object>("TotalQty"));
               objLCm.TotalValue =dt.Rows[0].Field<object>("TotalValue")==DBNull.Value||dt.Rows[0].Field<object>("TotalValue")==null?0.0:Convert.ToDouble( dt.Rows[0].Field<object>("TotalValue"));
               objLCm.ActualShipmentDate = dt.Rows[0].Field<object>("ActualShipmentDate") == DBNull.Value || dt.Rows[0].Field<object>("ActualShipmentDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("ActualShipmentDate");
               objLCm.UnderLCDate = dt.Rows[0].Field<object>("UnderLCDate") == DBNull.Value || dt.Rows[0].Field<object>("UnderLCDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("UnderLCDate");
               objLCm.PaymentDate = dt.Rows[0].Field<object>("PayDate") == DBNull.Value || dt.Rows[0].Field<object>("PayDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("PayDate");
               objLCm.ActualPaymentDate = dt.Rows[0].Field<object>("PayDate") == DBNull.Value || dt.Rows[0].Field<object>("ActualPayDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("ActualPayDate");
               objLCm.CustomerSupplierID = dt.Rows[0].Field<object>("CustSuppID") == DBNull.Value || dt.Rows[0].Field<object>("CustSuppID") == null ? 0 : dt.Rows[0].Field<int>("CustSuppID");
               objLCm.CurrencyID = dt.Rows[0].Field<object>("CurrencyID") == DBNull.Value || dt.Rows[0].Field<object>("CurrencyID") == null ? 0 : dt.Rows[0].Field<int>("CurrencyID");
               objLCm.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));
               objLCm.LcUnit = GlobalFunctions.isNull(dt.Rows[0].Field<object>("lcUnit"), "");
               objLCm.LCDescription = GlobalFunctions.isNull(dt.Rows[0].Field<object>("Remarks"),"");
               
               return objLCm;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }


       public void DeleteLC(int dd, SqlConnection con)
       {
               SqlTransaction trans = null;
           try
           {
               trans = con.BeginTransaction();
               SqlCommand com = new SqlCommand("spDeleteLC", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@deleteID", SqlDbType.Int).Value = dd;
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



       public void CreateAmendment(SqlConnection con, SqlTransaction trans, int LCID, DateTime AmendDate, double TotalAmendQty, double TotalAmendValue, string AmendComment)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("spDoneLCAmendment", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@LCID", SqlDbType.Int).Value = LCID;
               cmd.Parameters.Add("@AmendDate", SqlDbType.DateTime).Value = AmendDate;
               cmd.Parameters.Add("@TotalAmendQty", SqlDbType.Money).Value = TotalAmendQty;
               cmd.Parameters.Add("@TotalAmendValue", SqlDbType.Money).Value = TotalAmendValue;
               cmd.Parameters.Add("@AmendComment", SqlDbType.VarChar, 500).Value = AmendComment;
               //cmd.Parameters.Add("@AmendUnitPrice", SqlDbType.Money).Value = AmendUnitPrice;

               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }




       //public void UpdateLCDetail(SqlConnection con, SqlTransaction trans, int LCID, int LCDetailID, double TotalQty, double uPrice, double TotalValue)
       //{
       //    try
       //    {
       //        string qstr = "UPDATE Order_Details SET OrderQty = @OrderQty, UnitPrice = @UnitPrice, OrderValue = @OrderValue "
       //                    + " WHERE  (OrderDID = @OrderDID) AND (OrderMID = @OrderMID)";
       //        SqlCommand cmd = new SqlCommand(qstr, con, trans);
       //        cmd.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMID;
       //        cmd.Parameters.Add("@OrderDID", SqlDbType.Int).Value = OrderDID;
       //        cmd.Parameters.Add("@OrderQty", SqlDbType.Money).Value = OrderQty;
       //        cmd.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = uPrice;
       //        cmd.Parameters.Add("@OrderValue", SqlDbType.Money).Value = OrderValue;
       //        cmd.ExecuteNonQuery();

       //    }
       //    catch (Exception ex)
       //    {
       //        throw ex;
       //    }
       //}

       public DataTable getAmendments(SqlConnection con, int LCid)
       {
           DataTable dt = new DataTable();
           try
           {
               string qstr = "SELECT  LCID, AmendDate,TotalQty, TotalValue,  AmendQty, AmendUnitPrice, AmendValue, AmendComment " +
                            " FROM  T_LC_Master_A WHERE LCID = "+ LCid.ToString() +" ORDER BY AmendDate";
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
               SqlDataAdapter da = new SqlDataAdapter("select * from Currency", con);
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

       public DataTable GetImportLC(SqlConnection con, DateTime sDate, DateTime eDate)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_LC_Master WHERE ((LCType LIKE 'Import LC') OR (LCType LIKE 'Direct Import LC')) AND LCDate between @std and @endd AND CompanyID=@CompanyID", con);
               da.SelectCommand.Parameters.Add("@std", SqlDbType.DateTime).Value = sDate;
               da.SelectCommand.Parameters.Add("@endd", SqlDbType.DateTime).Value = eDate;
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
       public DataTable loadLCAcceptance(SqlConnection con, int LCid)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_LC_Acceptance WHERE LCID = @LCId", con);
               da.SelectCommand.Parameters.Add("@LCId", SqlDbType.Int).Value = LCid;
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return dt;
       }
       public DataTable GetBank(int BankId, SqlConnection con)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_Ledgers where LedgerID = @BankId AND CompanyID = " + LogInInfo.CompanyID.ToString(), con);
               da.SelectCommand.Parameters.Add("@BankId", SqlDbType.Int).Value = BankId;
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return dt;
       }

       public void SaveUpdateLCAcceptance(LCAcceptance obLCAcc, SqlConnection con)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spSaveUpdateLCAcceptance";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@SlNo", SqlDbType.Int).Value = obLCAcc.SlNo;
               com.Parameters.Add("@LCID", SqlDbType.Int).Value = obLCAcc.LCID;
               if (obLCAcc.acceptDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@acceptDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@acceptDate", SqlDbType.DateTime).Value = obLCAcc.acceptDate;
               com.Parameters.Add("@acceptQty", SqlDbType.Money).Value = obLCAcc.acceptQty;
               com.Parameters.Add("@acceptValue", SqlDbType.Money).Value = obLCAcc.acceptValue;

               if (obLCAcc.ActualShipmentDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ActualShipmentDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ActualShipmentDate", SqlDbType.DateTime).Value = obLCAcc.ActualShipmentDate;
               if (obLCAcc.MaturityDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = obLCAcc.MaturityDate;
               if (obLCAcc.PaidDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@PaidDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@PaidDate", SqlDbType.DateTime).Value = obLCAcc.PaidDate;
               com.Parameters.Add("@remarks", SqlDbType.VarChar, 100).Value = obLCAcc.remarks;
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

       public void deleteLcAcceptance(SqlConnection con, int SLno)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "Delete from T_LC_Acceptance Where SLNo = @SLNo";
               com.Parameters.Add("@SLNo", SqlDbType.Int).Value = SLno;
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
       public double getAcceptValue(SqlConnection con, int LcId)
       {
           double value = 0;
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT SUM(acceptValue) AS Value FROM T_LC_Acceptance WHERE (LCID = @LCID)", con);
               da.SelectCommand.Parameters.Add("@LCID", SqlDbType.Int).Value = LcId;
               da.Fill(dt);
               da.Dispose();
               value = GlobalFunctions.isNull(dt.Rows[0].Field<object>("Value"), 0.0);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return value;
       }
       public DataTable getReceivedQty(SqlConnection con, int LcId)
       {
           DataTable dt = new DataTable();
           //double ReceivedQty = 0;
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("Select dbo.fnGetReceivedQty(@LcId) AS ReceivedQty, dbo.fnTotalBillQTY(@LcId) AS TotalBilQty, dbo.fnTotalBillAmount(@LcId) AS TotalBilAmt", con);
               da.SelectCommand.Parameters.Add("@LcId", SqlDbType.Int).Value = LcId;
               da.Fill(dt);
               da.Dispose();
               }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return dt;
       }
       public DataTable getUnderLc(SqlConnection con, int LcId)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_UnderLC WHERE LCID = @SlNo", con);
               da.SelectCommand.Parameters.Add("@SlNo", SqlDbType.Int).Value = LcId;
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return dt;
       }
       public DataTable getLc(SqlConnection con, int Lcid)
       { 
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_LC_Master where LCID = @LCMasterID", con);

               da.SelectCommand.Parameters.Add("@LCMasterID", SqlDbType.Int).Value = Lcid;

               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0)
                   return null;
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dt;
       }
       public void saveUpdateUnderLC(SqlConnection con, UnderLC obUnderlc)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spSaveUpdateUnderLc";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@SLNo", SqlDbType.Int).Value = obUnderlc.SLNo;
               com.Parameters.Add("@LCID", SqlDbType.Int).Value = obUnderlc.LCID;
               if (obUnderlc.UnderLCID == 0)
                   com.Parameters.Add("@UnderLCID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@UnderLCID", SqlDbType.Int).Value = obUnderlc.UnderLCID;
               com.Parameters.Add("@UnderLCNo", SqlDbType.VarChar,100).Value = obUnderlc.UnderLCNo;
               if (obUnderlc.UnderLCDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@UnderLCDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@UnderLCDate", SqlDbType.DateTime).Value = obUnderlc.UnderLCDate;

               if (obUnderlc.ShipmentDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = obUnderlc.ShipmentDate;
               if (obUnderlc.ExpDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ExpDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ExpDate", SqlDbType.DateTime).Value = obUnderlc.ExpDate;
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
       public void deleteUnderLc(SqlConnection con, int slno)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "Delete from T_UnderLC Where SLNo = @SLNo";
               com.Parameters.Add("@SLNo", SqlDbType.Int).Value = slno;
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               if (trans != null)
                   trans.Commit();
               throw new Exception(ex.Message);
           }
       }

    }
}
