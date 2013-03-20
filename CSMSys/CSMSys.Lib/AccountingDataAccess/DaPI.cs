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
    public class DaPI
    {
        public DaPI() { }
        //-------------------Save or Update PI_Print_Setting---------------------------------********
        public int UpdateT_PI_Print_Setting(string TermAndCondition, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "UPDATE T_PI_Print_setting SET TermAndCondition=@TermAndCondition";


                com.Parameters.Add("TermAndCondition", SqlDbType.VarChar, 4000).Value = TermAndCondition;


                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new Exception("Unable to Save or UpDate. " + ex.Message);
            }
            return 0;
        }
        /************************** End of PI_Print_Setting ********************************

        *************************** Save or Update PI_Master ***************************/

        public int SaveUpdatePI_Master(PI_Master obPI_Master, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans=null;
            int ID = 0;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdatePI_Master";
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@PIMID", SqlDbType.Int).Value = obPI_Master.PIMID;
                com.Parameters.Add("@PINO", SqlDbType.VarChar, 50).Value = obPI_Master.PINO;
                com.Parameters.Add("@PIDate", SqlDbType.DateTime).Value = obPI_Master.PIDate;
                //com.Parameters.Add("OrderID", SqlDbType.Int).Value = obPI_Master.OrderID;
                if (obPI_Master.FactoryID <= 0)
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = DBNull.Value;
                else
                com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = obPI_Master.FactoryID;
                //com.Parameters.Add("TotalOrderQty", SqlDbType.Int).Value = obPI_Master.TotalOrderQty;
                //com.Parameters.Add("UnitID", SqlDbType.Int).Value = obPI_Master.UnitID;
                //com.Parameters.Add("OrderValue", SqlDbType.Money).Value = obPI_Master.OrderValue;
                //com.Parameters.Add("CurrencyID", SqlDbType.Int).Value = obPI_Master.CurrencyID;
                com.Parameters.Add("@PIType", SqlDbType.VarChar, 50).Value = obPI_Master.PIType;
                com.Parameters.Add("@Attention", SqlDbType.VarChar, 500).Value = obPI_Master.Attention;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = obPI_Master.CustomerOrSupplierID; 
                com.ExecuteNonQuery();
               
                if (obPI_Master.PIMID == 0)
                    ID = ConnectionHelper.GetIDForInsert(con,trans, "PIMID", "T_PI_Master");
                else
                    ID = obPI_Master.PIMID;
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return ID;
        }
        public int SaveUpdatePI_Master(PI_Master obPI_Master, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
            
            int ID = 0;
            try
            {
                com = new SqlCommand();
              
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdatePI_Master";
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@PIMID", SqlDbType.Int).Value = obPI_Master.PIMID;
                com.Parameters.Add("@PINO", SqlDbType.VarChar, 50).Value = obPI_Master.PINO;
                com.Parameters.Add("@PIDate", SqlDbType.DateTime).Value = obPI_Master.PIDate;
                //com.Parameters.Add("OrderID", SqlDbType.Int).Value = obPI_Master.OrderID;
                if (obPI_Master.FactoryID <= 0)
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = obPI_Master.FactoryID;
                com.Parameters.Add("@PIType", SqlDbType.VarChar, 50).Value = obPI_Master.PIType;
                if (obPI_Master.CurrencyID <= 0)
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obPI_Master.CurrencyID;
                com.Parameters.Add("@Attention", SqlDbType.VarChar, 500).Value = obPI_Master.Attention;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = obPI_Master.CustomerOrSupplierID;
                com.Parameters.Add("@Rate", SqlDbType.Money).Value = obPI_Master.Rate;
                com.Parameters.Add("@TermsCond", SqlDbType.VarChar, 5000).Value = obPI_Master.TermsCondition;
                com.ExecuteNonQuery();
                if (obPI_Master.PIMID == 0)
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(PIMID),0) FROM T_PI_Master", con, trans);
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    ID = obPI_Master.PIMID;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return ID;
        }
        //----------------------End of PI_Master-----------------*********************

        //---------------------Save or Update PI_Details-------------****************
        public int SaveUpdatePI_Details(PI_Details obPI_Details, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdatePI_Details";
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@PIDID", SqlDbType.Int).Value = obPI_Details.PIDID;
                com.Parameters.Add("@PIMID", SqlDbType.Int).Value = obPI_Details.PIMID;
                //com.Parameters.Add("CountID", SqlDbType.Int).Value = obPI_Details.CountID;
                //com.Parameters.Add("CountTypeID", SqlDbType.Int).Value = obPI_Details.CountTypeID;
                com.Parameters.Add("@OrderID", SqlDbType.Int).Value = obPI_Details.OrderID;
                com.Parameters.Add("@OrderQty", SqlDbType.Money).Value = obPI_Details.OrderQty;
                //com.Parameters.Add("UnitID", SqlDbType.Int).Value = obPI_Details.UnitID;
                com.Parameters.Add("@OrderValue", SqlDbType.Money).Value = obPI_Details.OrderValue;
                
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

        public int SaveUpdatePI_Details(PI_Details obPI_Details, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
         
            try
            {
                com = new SqlCommand();
             
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdatePI_Details";
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@PIDID", SqlDbType.Int).Value = obPI_Details.PIDID;
                com.Parameters.Add("@PIMID", SqlDbType.Int).Value = obPI_Details.PIMID;
                //com.Parameters.Add("CountID", SqlDbType.Int).Value = obPI_Details.CountID;
                //com.Parameters.Add("CountTypeID", SqlDbType.Int).Value = obPI_Details.CountTypeID;
                com.Parameters.Add("@OrderID", SqlDbType.Int).Value = obPI_Details.OrderID;
                com.Parameters.Add("@OrderQty", SqlDbType.Money).Value = obPI_Details.OrderQty;
                //com.Parameters.Add("UnitID", SqlDbType.Int).Value = obPI_Details.UnitID;
                com.Parameters.Add("@OrderValue", SqlDbType.Money).Value = obPI_Details.OrderValue;

                com.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {
                
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return 0;
        }

        public string GetMaxID(SqlConnection con)
        {
            SqlCommand com = null;
           
            string str = null;
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                com = new SqlCommand();
                
                com.Connection = con;
                com.CommandText = "select  ltrim(isnull(count(PINO),0)+1)as NN from T_PI_Master";
                str=com.ExecuteScalar().ToString();
            
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return str;
        }

        public DataTable  LoadTeam(SqlConnection con)
        {
           
            
            try
            {
                SqlDataAdapter da=new SqlDataAdapter("select  * from T_Team_Master WHERE CompanyID=" + LogInInfo.CompanyID.ToString(),con);
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
        public DataTable LoadMember(int dd, SqlConnection con)
        {


            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select  * from T_Team_Detail where TeamID=" + dd.ToString(), con);
                DataTable dta = new DataTable();
                da.Fill(dta);
                da.Dispose();
                return dta;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable LoadDGVCustomer(int dd, SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSearchCustomerDGV", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@MemberID",SqlDbType.Int).Value=dd;
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

        public DataTable LoadDGVSearchByCustomer(string strpara, SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT  LedgerName,LedgerID,LedgerTypeID FROM T_Ledgers WHERE  CompanyID="+ LogInInfo.CompanyID.ToString() +" AND LedgerName like '" + strpara.ToString() + "%'", con);
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



        public DataTable LoadCustomerInDGV( SqlConnection con)
        {
           
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT  LedgerName,LedgerID, LedgerTypeID FROM T_Ledgers WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " AND LedgerTypeID =2", con);
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


        public DataTable LoadFactoryIncmb(int dd,SqlConnection con)
        {

            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT  * from Factory where CustomerID="+dd.ToString(), con);
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

        public DataTable GetOrderNo(int dd,DateTime stdt,DateTime endt, SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select Order_Master.*,T_Ledgers.LedgerName,(CASE WHEN ISNULL(T_PI_Details.PIDID,0) <=0 THEN NULL ELSE 'PI Opened' END) AS Status  from Order_Master INNER JOIN T_Ledgers ON Order_Master.customerID=T_Ledgers.LedgerID LEFT OUTER JOIN T_PI_Details ON Order_Master.OrderMID = T_PI_Details.OrderID where Order_Master.CompanyID=" + LogInInfo.CompanyID.ToString() + " AND OrderDate between @SatrtDate and @EndDate and CustomerID=" + dd.ToString() + " ORDER BY Status, Order_Master.OrderNo", con);
                da.SelectCommand.Parameters.Add("@SatrtDate", SqlDbType.DateTime).Value = stdt;
                da.SelectCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endt;
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

        public DataTable GetOrderNos(int dd, DateTime stdt, DateTime endt,string str, SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select Order_Master.*,(CASE WHEN ISNULL(T_PI_Details.PIDID,0) <=0 THEN NULL ELSE 'PI Opened' END) AS Status from Order_Master LEFT OUTER JOIN T_PI_Details ON Order_Master.OrderMID = T_PI_Details.OrderID where CompanyID=" + LogInInfo.CompanyID.ToString() + " AND OrderDate between @SatrtDate and @EndDate and OrderNo=@str  and CustomerID=" + dd.ToString() + " ORDER BY Status, Order_Master.OrderNo", con);
                da.SelectCommand.Parameters.Add("@SatrtDate", SqlDbType.DateTime).Value = stdt;
                da.SelectCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endt;
                da.SelectCommand.Parameters.Add("@str", SqlDbType.VarChar, 50).Value = str;
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


        public DataTable GetOrderNosa(int dd, string str, SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select Order_Master.*,(CASE WHEN ISNULL(T_PI_Details.PIDID,0) <=0 THEN NULL ELSE 'PI Opened' END) AS Status from Order_Master LEFT OUTER JOIN T_PI_Details ON Order_Master.OrderMID = T_PI_Details.OrderID where CompanyID=" + LogInInfo.CompanyID.ToString() + " AND OrderNo=@str and CustomerID=" + dd.ToString() + " ORDER BY Status, Order_Master.OrderNo", con);
                
                da.SelectCommand.Parameters.Add("@str", SqlDbType.VarChar, 50).Value = str;
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

        public DataTable GetOrderRefNo(string Where, SqlConnection con)
        {
            DataTable dt = new DataTable();

            try
            {

                string qstr;//= "SELECT   0 as PIDID , Order_Master.OrderMID , Order_Master.OrderNo, Order_Details.ItemID, VW_Items.ItemName, VW_Items.CountName AS Count, VW_Items.ItemCode, VW_Items.SizesName AS Size, VW_Items.ColorsName AS Color, Order_Details.ColorCode, Order_Details.Labdip, VW_Items.ShadeNo, VW_Items.UnitsName AS Unit, VW_Items.GroupName, Order_Details.OrderQty, Order_Details.OrderValue, Order_Details.Remarks  FROM Order_Details INNER JOIN Order_Master ON Order_Details.OrderMID = Order_Master.OrderMID INNER JOIN VW_Items ON Order_Details.ItemID = VW_Items.ItemID";
                qstr = "SELECT 0 AS PIDID,OM.OrderMID, OM.OrderNo, OD.ItemID, I.ItemName, C.CountName Count, S.SizesName Size, T.ColorsName [Prd.Type], OD.ColorCode, OD.Labdip, OD.OrderQty, " +
                        " U.UnitsName Unit, OD.UnitPrice, OD.OrderValue, OD.Remarks [C.Qty] FROM Order_Details AS OD INNER JOIN Order_Master AS OM ON OD.OrderMID = OM.OrderMID " +
                        " INNER JOIN T_Item AS I ON OD.ItemID = I.ItemID INNER JOIN P_Units AS U ON I.UnitID = U.UnitsID LEFT OUTER JOIN P_Colors AS T ON OD.ColorID = T.ColorsID " +
                        " LEFT OUTER JOIN P_Sizes AS S ON OD.SizeID = S.SizesID LEFT OUTER JOIN T_Count AS C ON OD.CountID = C.CountID ";

                SqlDataAdapter da = new SqlDataAdapter(qstr + " " + Where + " Order By OM.OrderNo,I.ItemName,C.CountName,S.SizesName, T.ColorsName", con);

                da.Fill(dt);
                da.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;

        }
        public DataTable GetPINo(string Ignore,DateTime strd, DateTime endd,string PIType,int CSid, SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spPIInfo",con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@std", SqlDbType.DateTime).Value = strd;
                da.SelectCommand.Parameters.Add("@endd", SqlDbType.DateTime).Value = endd;
                da.SelectCommand.Parameters.Add("@strPINo", SqlDbType.VarChar, 100).Value = Ignore;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                //da.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CutomerID;

                da.SelectCommand.Parameters.Add("@PItype", SqlDbType.VarChar, 100).Value = PIType;
                da.SelectCommand.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = CSid;
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPINoNew(string Ignore, DateTime strd, DateTime endd, string PIType, int CSid, SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spPIInfoNew", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@std", SqlDbType.DateTime).Value = strd;
                da.SelectCommand.Parameters.Add("@endd", SqlDbType.DateTime).Value = endd;
                da.SelectCommand.Parameters.Add("@strPINo", SqlDbType.VarChar, 100).Value = Ignore;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                //da.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CutomerID;

                da.SelectCommand.Parameters.Add("@PItype", SqlDbType.VarChar, 100).Value = PIType;
                da.SelectCommand.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = CSid;
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPINo(string Ignore, DateTime strd, DateTime endd,int CSid, SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spPIInfo", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@std", SqlDbType.DateTime).Value = strd;
                da.SelectCommand.Parameters.Add("@endd", SqlDbType.DateTime).Value = endd;
                da.SelectCommand.Parameters.Add("@strPINo", SqlDbType.VarChar, 100).Value = Ignore;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.SelectCommand.Parameters.Add("@PItype", SqlDbType.VarChar, 100).Value = "%";
                da.SelectCommand.Parameters.Add("@CustSuppID", SqlDbType.Int).Value = CSid; 
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPINoatTrans(int PI_MasterID, SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spTransferPIDID", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@PIMasterID", SqlDbType.Int).Value = PI_MasterID;
                
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PI_Master GetPI_Master(int PI_MasterID, SqlConnection con)
        {
            PI_Master objPIM = new PI_Master();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_PI_Master where PIMID=@PIMasterID", con);

                da.SelectCommand.Parameters.Add("@PIMasterID", SqlDbType.Int).Value = PI_MasterID;

                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0) return null;
                objPIM.PIMID = dt.Rows[0].Field<int>("PIMID");
                objPIM.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
                objPIM.PIType = dt.Rows[0].Field<string>("PIType");
                objPIM.PINO = dt.Rows[0].Field<string>("PINO");
                objPIM.PIDate = dt.Rows[0].Field<DateTime>("PIDate");
                objPIM.FactoryID = (dt.Rows[0].Field<object>("FactoryID")==DBNull.Value||dt.Rows[0].Field<object>("FactoryID")==null)?0:dt.Rows[0].Field<int>("FactoryID");
                objPIM.Attention = dt.Rows[0].Field<string>("Attention");
                objPIM.CustomerOrSupplierID = dt.Rows[0].Field<int>("CustSuppID");
                objPIM.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));
                objPIM.TermsCondition =GlobalFunctions.isNull( dt.Rows[0].Field<object>("TermsCond"),"");
                return objPIM;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePI(int dd, SqlConnection con)
        {
            try
            {
                SqlCommand com = new SqlCommand("spdeletepi", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@deleteID", SqlDbType.Int).Value = dd;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        public PI_Print_Settings GetPrittingItems(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_PI_Print_Setting", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                PI_Print_Settings obj = new PI_Print_Settings();
                obj.ItemID = dt.Rows[0].Field<int>("SettingID");
                obj.Terms = dt.Rows[0].Field<string>("TermAndCondition");
                obj.Condition = dt.Rows[0].Field<string>("Items");
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetPrittingItemsTable(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_PI_Print_Setting", con);
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






        public DataTable getPIDetail(SqlConnection con, string where,int PIMID)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                string qstr = "SELECT ISNULL(PI.PIDID,0) PIDID ,PI.PIMID,OrderM.OrderMID OrderID,OrderM.TotalOrderQty AS OrderQty, OrderM.OrderValue FROM (SELECT * FROM T_PI_Details  WHERE PIMID = @PIMID) AS PI RIGHT OUTER JOIN  Order_Master AS OrderM ON PI.OrderID = OrderM.OrderMID ";
                qstr +=  where;
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                da.SelectCommand.Parameters.Add("@PIMID", SqlDbType.Int).Value = PIMID;
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

 
        



        //--------------------End of PI_Details--------------------------------\\
    }

}
