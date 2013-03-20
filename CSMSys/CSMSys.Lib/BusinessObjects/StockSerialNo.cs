using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.BusinessObjects
{
    public class StockSerialNo
    {
        public int getbagcount(string serialno)
        {
            string[] serial = serialno.Split('/');

            return (serial[1]!=null)?Convert.ToInt32( serial[1]):0;
        }
        public int getSerialID(string serialno)
        {
            string[] serial = serialno.Split('/');

            return (serial[0] != null) ? Convert.ToInt32(serial[0]) : 0;
        }

        public SRVLoanDisburse retLoanDisburse(IList<SRVLoanDisburse> isrvld, int serid)
        {
            foreach (SRVLoanDisburse srvLoanDisburse in isrvld)
            {
                string[] words = srvLoanDisburse.serialIDs.Split(',');
                foreach (string word in words)
                {
                    if (word != "")
                    {
                        if (serid==Convert.ToInt32(word))  
                            return srvLoanDisburse;

                    }    
                }
                
            }
            return null;
        }
        public SRVLoanCollection retLoanDisburse(IList<SRVLoanCollection> isrvld, int serid)
        {
            foreach (SRVLoanCollection srvLoanDisburse in isrvld)
            {
                string[] words = srvLoanDisburse.SerialIDs.Split(',');
                foreach (string word in words)
                {
                    if (word != "")
                    {
                        if (serid == Convert.ToInt32(word)) return srvLoanDisburse;

                    }
                }

            }
            return null;
        }
        public IList<INVStockSerial> getSerialFromSerialIDS(string serialids)
        {
             IList<INVStockSerial> invss = new List<INVStockSerial>();
            SerialManager sm = new SerialManager();
            string[] serids = serialids.Split(',');
            foreach (string serid in serids)
            {
                if (serid != "")
                {
                    invss.Add(sm.GetSerialByID(Convert.ToInt32(serid)));     
                }
            }
            return invss;
        }
    }
}
