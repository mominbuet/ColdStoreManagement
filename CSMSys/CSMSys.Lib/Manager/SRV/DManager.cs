using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Model;
using CSMSys.Lib.BusinessObjects;
namespace CSMSys.Lib.Manager.SRV
{
    public class DManager
    {
        #region Properties
        public RegistrationDAOLinq _RegistrationDAOLinq;
        public ItemDetailShadDAOLinq _itemDetailShadDaoLinq;
        #endregion

        #region Constructor
        public DManager()
        {
            _RegistrationDAOLinq = new RegistrationDAOLinq();
            _itemDetailShadDaoLinq = new ItemDetailShadDAOLinq();
        }
        #endregion
        public SRVRegistration getRegBySerialNo(int serialno)
        {
            return _RegistrationDAOLinq.SearchRegistrationByNo(serialno.ToString());

        }
        public IList<INVItemDetail> getItemDetailByRegID(long regid)
        {
            return _itemDetailShadDaoLinq.GetItemDetailByRegID(regid.ToString());

        }
        public float retValidDelivery(int serialid)
        {
            float res = 0;
            IList<SRVLoanDisburse> isrvdisburse = new LoanDAOLinq().getAllLoansLikeSerialID(serialid);
            IList<SRVLoanCollection> iSrvLoanCollections = new LoanCollectionDAOLinq().getAllLoansLikeSerialID(serialid);
            SRVLoanDisburse srvLoanDisburse = new StockSerialNo().retLoanDisburse(isrvdisburse, serialid);
            SRVLoanCollection srvLoanCollection = new StockSerialNo().retLoanDisburse(iSrvLoanCollections, serialid);
            INVStockSerial invStockSerial = new SerialManager().GetSerialByID(serialid);
            int bagcount = new StockSerialNo().getbagcount(invStockSerial.SerialNo);

            if (srvLoanDisburse != null)
            {
                res = (-1)*float.Parse(srvLoanDisburse.LoanAmount.ToString())*bagcount;
                if (srvLoanCollection != null)
                {
                    res += float.Parse(srvLoanCollection.TotalLoan.ToString());
                }
            }
            return res;
        }

    }
}
