using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.Administration.Application
{
    public class AreaPOManager
    {
        #region Properties
        IAreaPODAO _AreaPODAOLinq;
        #endregion
        #region Constructor
        public AreaPOManager(bool isLoadWith)
        {
            _AreaPODAOLinq = new AreaPODAOLinq(isLoadWith);
        }
        #endregion
        /// <summary>
        /// Get all AreaPO from AreaPO table 
        /// </summary>
        /// <returns></returns>
        public IList<ADMAreaPO> GetAllAreaPO()
        {
            try
            {
                return _AreaPODAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ADMAreaPO GetAreaPOByID(int id)
        {
            return _AreaPODAOLinq.PickByID(id);
        }


        public bool SaveAreaPO(ADMAreaPO areaPO)
        {
            try
            {
                areaPO.ModifiedDate = System.DateTime.Now;

                if (areaPO.AreaPOID > 0)
                {
                    areaPO.CreatedDate = System.DateTime.Now;
                    return _AreaPODAOLinq.Add(areaPO);
                }
                else
                {
                    return _AreaPODAOLinq.Edit(areaPO);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="upazilaPSOID"></param>
        /// <returns></returns>
        public IList<ADMAreaPO> GetAllAreaPOByUpazilaPS(int upazilaPSID)
        {
            try
            {
                if (upazilaPSID > 0)
                {
                    return _AreaPODAOLinq.All().Where(u => u.UpazilaPSID.Equals(upazilaPSID)).ToList();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<ADMAreaPO> SearchAreaPO(int upazilaPSID, string areaPOCode, string areaPOName)
        {
            return _AreaPODAOLinq.SearchAreaPO(upazilaPSID, areaPOCode, areaPOName);
        }
    }
}
