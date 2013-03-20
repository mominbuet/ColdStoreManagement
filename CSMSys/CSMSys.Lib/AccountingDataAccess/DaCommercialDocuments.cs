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
    public class DaCommercialDocuments
    {
        public DaCommercialDocuments() { }
        public int SaveUpdateCommercialDocuments(CommercialDocuments obCommDoc, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateCommercialDocuments", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ComInvoiceID", SqlDbType.Int).Value = obCommDoc.ComInvoiceID;
                com.Parameters.Add("@ComInvoiceNo", SqlDbType.VarChar, 100).Value = obCommDoc.ComInvoiceNo;
                com.Parameters.Add("@ComInvoiceDate", SqlDbType.DateTime).Value = obCommDoc.ComInvoiceDate;
                com.Parameters.Add("@NotifyParty", SqlDbType.VarChar, 1000).Value = obCommDoc.NotifyParty;
                com.Parameters.Add("@FromLocation", SqlDbType.VarChar, 500).Value = obCommDoc.FromLocation;
                com.Parameters.Add("@ToLocation", SqlDbType.VarChar, 500).Value = obCommDoc.ToLocation;
                com.Parameters.Add("@ExpNo", SqlDbType.VarChar, 100).Value = obCommDoc.ExpNo;
                com.Parameters.Add("@ExpDate", SqlDbType.DateTime).Value = obCommDoc.ExpDate;
                com.Parameters.Add("@Carrier", SqlDbType.VarChar, 500).Value = obCommDoc.Carrier;
                com.Parameters.Add("@CarrierNo", SqlDbType.VarChar, 500).Value = obCommDoc.CarrierNo;
                com.Parameters.Add("@LCID", SqlDbType.Int).Value = obCommDoc.LCID;
                com.Parameters.Add("@OriginCountry", SqlDbType.VarChar, 100).Value = obCommDoc.OriginCountry;
                com.Parameters.Add("@TotalQty", SqlDbType.Money).Value = obCommDoc.TotalQty;
                com.Parameters.Add("@TotalValue", SqlDbType.Money).Value = obCommDoc.TotalValue;
                com.Parameters.Add("@PackingListNo", SqlDbType.VarChar, 100).Value = obCommDoc.PackingListNo;
                com.Parameters.Add("@PackingDate", SqlDbType.DateTime).Value = obCommDoc.PackingDate;
                com.Parameters.Add("@CONo", SqlDbType.VarChar, 100).Value = obCommDoc.CONo;
                com.Parameters.Add("@CODate", SqlDbType.DateTime).Value = obCommDoc.CODate;
                com.Parameters.Add("@ChalanNo", SqlDbType.VarChar, 100).Value = obCommDoc.ChalanNo;
                com.Parameters.Add("@ChalanDate", SqlDbType.DateTime).Value = obCommDoc.ChalanDate;
                com.Parameters.Add("@VATerIDNo", SqlDbType.VarChar, 500).Value = obCommDoc.VATerIDNo;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obCommDoc.Remarks;
                if (obCommDoc.SubmitDate == new DateTime(1900,1,1))
                    com.Parameters.Add("@SubmitDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                com.Parameters.Add("@SubmitDate", SqlDbType.DateTime).Value = obCommDoc.SubmitDate;

                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@DraftNo", SqlDbType.VarChar, 500).Value = obCommDoc.DraftNo;
                com.Parameters.Add("@BankRefNo", SqlDbType.VarChar, 500).Value = obCommDoc.BankRefNo;
                com.Parameters.Add("@MasterLCTitle", SqlDbType.VarChar, 500).Value = obCommDoc.MasterLCTitle;
                com.Parameters.Add("@FactoryAddress", SqlDbType.VarChar, 500).Value = obCommDoc.FactoryAddress;
                com.Parameters.Add("@IsEPZ", SqlDbType.Int).Value = obCommDoc.IsEPZ;
                com.Parameters.Add("@Authority", SqlDbType.VarChar, 500).Value = obCommDoc.Authority;
                com.Parameters.Add("@LCTermsCondition", SqlDbType.VarChar, 1000).Value = obCommDoc.LCTermsCondition;
                com.Parameters.Add("@PackingListStatus", SqlDbType.VarChar, 500).Value = obCommDoc.PackingListStatus;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return 0;
        }

        public string GetLCNO(int lcid, SqlConnection con)
        {
            SqlCommand com = new SqlCommand();

            try
            {

                com.Connection = con;

                com.CommandText = "select LCNo from T_LC_Master where LCID=" + lcid.ToString();
               
                object str = com.ExecuteScalar();

                return str == null || str ==DBNull.Value ? "" : str.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetComDoc(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spLoadComDoc", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
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

        public DataTable SearchComDoc(DateTime stdate, DateTime enddate, string ComDocNo, SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSearchComDoc", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@stDate", SqlDbType.DateTime).Value = stdate;
                da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = enddate;
                da.SelectCommand.Parameters.Add("@ComDocNo", SqlDbType.VarChar, 50).Value = ComDocNo;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetComDocBYComDocID(int dd,SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spComDocBYComInvID", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@ComInvoiceID", SqlDbType.Int).Value = dd;
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
        public CommercialDocuments GetCommDocs(SqlConnection con, int CommDocID)
        {
            CommercialDocuments obCommercialDocuments = new CommercialDocuments();
            DataTable dt = new DataTable();
            try
            {

                dt = GetComDocBYComDocID(CommDocID, con);
                if (dt.Rows.Count == 0) return null;

                obCommercialDocuments.ComInvoiceID = dt.Rows[0].Field<int>("ComInvoiceID");
                obCommercialDocuments.ComInvoiceNo = dt.Rows[0].Field<string>("ComInvoiceNo");
                obCommercialDocuments.ComInvoiceDate = dt.Rows[0].Field<DateTime>("ComInvoiceDate");
                obCommercialDocuments.NotifyParty = dt.Rows[0].Field<string>("NotifyParty");
                obCommercialDocuments.FromLocation = dt.Rows[0].Field<string>("FromLocation");
                obCommercialDocuments.ToLocation = dt.Rows[0].Field<string>("ToLocation");
                obCommercialDocuments.ExpNo = dt.Rows[0].Field<string>("ExpNo");
                obCommercialDocuments.ExpDate = dt.Rows[0].Field<DateTime>("ExpDate");
                obCommercialDocuments.Carrier = dt.Rows[0].Field<string>("Carrier");
                obCommercialDocuments.CarrierNo = dt.Rows[0].Field<string>("CarrierNo");
                obCommercialDocuments.LCID = dt.Rows[0].Field<int>("LCID");
                obCommercialDocuments.OriginCountry = dt.Rows[0].Field<string>("OriginCountry");
                obCommercialDocuments.TotalQty = Convert.ToDouble(dt.Rows[0].Field<object>("TotalQty"));
                obCommercialDocuments.TotalValue = Convert.ToDouble(dt.Rows[0].Field<object>("TotalValue"));
                obCommercialDocuments.PackingListNo = dt.Rows[0].Field<string>("PackingListNo");
                obCommercialDocuments.PackingDate = dt.Rows[0].Field<DateTime>("PackingDate");
                obCommercialDocuments.CONo = dt.Rows[0].Field<string>("CONo");
                obCommercialDocuments.CODate = dt.Rows[0].Field<DateTime>("CODate");
                obCommercialDocuments.VATerIDNo = dt.Rows[0].Field<string>("VATerIDNo");
                obCommercialDocuments.Remarks = dt.Rows[0].Field<string>("Remarks");
                obCommercialDocuments.ChalanNo = dt.Rows[0].Field<string>("ChalanNo");

                obCommercialDocuments.SubmitDate = dt.Rows[0].Field<object>("SubmitDate") == DBNull.Value || dt.Rows[0].Field<object>("SubmitDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("SubmitDate");
                obCommercialDocuments.DraftNo = dt.Rows[0].Field<string>("DraftNo");
                obCommercialDocuments.BankRefNo = dt.Rows[0].Field<string>("BankRefNo");
                obCommercialDocuments.MasterLCTitle = dt.Rows[0].Field<string>("MasterLCTitle");
                obCommercialDocuments.FactoryAddress = dt.Rows[0].Field<string>("FactoryAddress");
                obCommercialDocuments.IsEPZ = dt.Rows[0].Field<object>("IsEPZ") == DBNull.Value || dt.Rows[0].Field<object>("IsEPZ") == null?-1 : dt.Rows[0].Field<int>("IsEPZ");
                obCommercialDocuments.Authority = dt.Rows[0].Field<string>("Authority");
                obCommercialDocuments.LCTermsCondition = dt.Rows[0].Field<string>("IsLCTermsCondition");
                obCommercialDocuments.PackingListStatus = dt.Rows[0].Field<string>("PackingListStatus");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obCommercialDocuments;
        }
        public void DeleteComDoc(int dd,SqlConnection con)
       {
           SqlCommand com = new SqlCommand();
           SqlTransaction trans = null;
           try
           {
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "Delete From T_Commercial_Documents where ComInvoiceID=" + dd.ToString();
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               trans.Rollback();
               throw ex;
           }
 
        }


      
    }
}
