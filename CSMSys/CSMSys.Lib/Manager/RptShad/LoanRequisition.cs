using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Implementations;
using System.Data;
using CSMSys.Lib.BusinessObjects;
namespace CSMSys.Lib.Manager.RptShad
{
    public class LoanRequisition
    {

        
        public Int32 getBalance(int pid,int bags)
        {
            Int32 totbags = 0;
            IList<SRVLoanDisburse> ld =new LoanDAOLinq().getAllLoansByParty(pid);
            foreach (SRVLoanDisburse l in ld)
            {
                if (l.Bags == bags)
                {
                    string[] serials = l.serialIDs.Split(',');
                    foreach (string serial in serials)
                    {
                        if (serial != "")
                        {
                            INVStockSerial iss = new SerialDAOLinq().PickByID(int.Parse(serial));
                            totbags += Convert.ToInt32( iss.Bags);
                        }
                    }
                }
            }
            return (Convert.ToInt32(totbags) - bags);
        }
        public DataTable getAppliedBags(int pid, DateTime dtFrom, DateTime dtTo)
        {
            DataTable res = new DataTable("Applied");
            res.Columns.Add("applied", typeof(string));
            res.Columns.Add("mdate", typeof(string));
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            //command.CommandText = @"select serialids, convert(varchar(10),Createddate,103) as cdate from srvLoanDisburse where partyID=" +
            //            pid +
            //            "order by Createddate asc;";

            command.CommandText = @"select SerialID AS serialids, convert(varchar(10),Createddate,103) as cdate from SRVRegistration where PartyID = " + pid + "  order by Createddate asc;";
            connection.Open();
            Reader = command.ExecuteReader();
            string cdate = string.Empty;
            int bagcount = 0;
            while (Reader.Read())
            {
                if (Reader["serialids"] != DBNull.Value)
                {
                    cdate = Reader["cdate"].ToString();

                    //IList<INVStockSerial> ss = new StockSerialNo().getSerialFromSerialIDS((Reader["serialids"]).ToString());
                    //foreach (INVStockSerial s in ss)
                    //{
                    //    //bagcount = bagcount + Convert.ToInt32(new StockSerialNo().getbagcount(s.SerialNo));
                    //    bagcount = Convert.ToInt32(new StockSerialNo().getbagcount(s.SerialNo));
                    //}
                    //res.Rows.Add(new string[2] { bagcount.ToString(), Reader["cdate"].ToString() });
                }
            }

            Reader.Close();
            connection.Close();
            res.Rows.Add(new string[2] { getSumAppliedBags(pid).ToString(),cdate });
            return res;
        }
        public DataTable getPrevloan(int pid)
        {
            DataTable res = new DataTable("Prev");
            res.Columns.Add("Bags", typeof(string));
            res.Columns.Add("amount", typeof(string));
            res.Columns.Add("mult", typeof(string));
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            command.CommandText = @"select Bags,Loanamount as amount from srvLoanDisburse where partyID=" +
                        pid +
                        "order by Createddate asc;";
            connection.Open();
            Reader = command.ExecuteReader();
            res.Rows.Add(new string[3] { 0.ToString(), 0.ToString(),0.ToString() });

            while (Reader.Read())
            {
                if (Reader["Bags"] != DBNull.Value)
                {
                    res.Rows.Add(new string[3] { (Reader["Bags"]).ToString(), (Reader["amount"]).ToString() ,
                        (double.Parse((Reader["Bags"]).ToString())*double.Parse((Reader["amount"]).ToString())).ToString()});
                }
            }
            Reader.Close();
            connection.Close();
            return res;
        }
        public IList<Int32> getApprovedBags(int pid)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            command.CommandText = @"select Bags from srvLoanDisburse where partyID=" +
                        pid +
                        "order by Createddate asc;";
            connection.Open();
            Reader = command.ExecuteReader();
            IList<Int32> log = new List<Int32>();
            while (Reader.Read())
            {
                if (Reader["Bags"] != DBNull.Value)
                    log.Add(Convert.ToInt32((Reader["Bags"]).ToString()));
                

            }
            Reader.Close();
            connection.Close();

            //string strSQL = "";
            return log;
        }
        static string mdate = string.Empty;
        public Int32 getSumAppliedBags(int pid)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            command.CommandText = @"SELECT sum(invss.Bags) as smbags /*,convert( varchar(10),srvreg.ModifiedDate,103) as mdate*/
from INVStockSerial as invss,SRVRegistration as srvreg
where srvreg.PartyID=" +pid +
@" and srvreg.Requisitioned='Applied for Loan' and srvreg.SerialID=invss.SerialID
/*group by srvreg.ModifiedDate*/";
            connection.Open();
            Reader = command.ExecuteReader();
            Int32  log = 0;
            while (Reader.Read())
            {
                if (Reader["smbags"] != DBNull.Value)
                {
                    log = Convert.ToInt32((Reader["smbags"]).ToString());
                    //mdate = Reader["mdate"].ToString();
                }
                break;

            }
            Reader.Close();
            connection.Close();

            //string strSQL = "";
            return log;
        }
    }
}
