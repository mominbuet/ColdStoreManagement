using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.SRV
{
    public class BagManager
    {
        
        #region Properties
        IBagDAO _IBagDAOLinq;
        #endregion

        #region Constructor
        public BagManager(bool isLoadWith)
        {
            _IBagDAOLinq = new BagDAOLinq(isLoadWith);
        }

        //public BagManager()
        //{
        //    _IBagDAOLinq = new BagDAOLinq();
        //}
        #endregion

        #region Public Methods
        /// <summary>
        /// Get all District from District table 
        /// </summary>
        /// <returns></returns>
        public IList<INVBagFair> GetAllBagWeight()
        {
            try
            {
                return _IBagDAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all Division from District table 
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Method to get district object by OID
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        /// 
        public IList<INVBagFair> GetByFairID(int id)
        {
            try
            {
                if (id > 0)
                {
                    return _IBagDAOLinq.All().Where(d => d.FairID.Equals(id)).ToList();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public INVBagFair GetFairByID(int id)
        {
            return _IBagDAOLinq.PickByID(id);
        }
         #endregion
    }
}
