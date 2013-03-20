using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;
namespace CSMSys.Lib.Manager.INV
{
    public class LoadManager
    {
        #region Properties
        ILoadingDAO _LoadDAOLinq;    
        #endregion
         #region Constructor
        public LoadManager()
        {
            _LoadDAOLinq = new LoadingDAOLinq();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get all Party from Party table 
        /// </summary>
        /// <returns></returns>
        public IList<INVStockLoading> GetAllLoading()
        {
            try
            {
                return _LoadDAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get party object by OID
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
         public INVStockLoading checkloc(Int32 chamber,int floor,int pocket)
        {
            return _LoadDAOLinq.checklocation(chamber,floor,pocket);
        }
        public INVStockLoading GetLoadByID(int id)
        {
            return _LoadDAOLinq.PickByID(id);
        }
        public INVStockLoading GetLoadBySerial(string id)
        {
            return _LoadDAOLinq.GetLoadBySerial(id.ToString());
        }
        public INVStockSerial GetSSBySerial(int id)
        {
            SerialManager sm = new SerialManager();
            return sm.GetSerialByID(id);
        }
        /// <summary>
        /// Method to save (add/edit) party object
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool SaveOrEditLocation(INVStockLoading loc)
        {
            try
            {
                if (loc.LoadingID == 0)
                {
                    return _LoadDAOLinq.Add(loc);
                }
                else
                {
                    return _LoadDAOLinq.Edit(loc);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteParty(INVStockLoading loc)
        {
            try
            {
                if (loc.LoadingID > 0)
                {
                    return _LoadDAOLinq.Delete(loc, true);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
