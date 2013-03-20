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
    public class UpazilaPSManager
    {
        #region Properties
        IUpazilaPSDAO _UpazilaPSDAOLinq;
        #endregion
        #region Constructor
        public UpazilaPSManager(bool isLoadWith)
        {
            _UpazilaPSDAOLinq = new UpazilaPSDAOLinq(isLoadWith);
        }
        #endregion
        /// <summary>
        /// Get all UpazilaPS from UpazilaPS table 
        /// </summary>
        /// <returns></returns>
        public IList<ADMUpazilaPS> GetAllUpazilaPS()
        {
            try
            {
                return _UpazilaPSDAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ADMUpazilaPS GetUpazilaPSByID(int id)
        {
            return _UpazilaPSDAOLinq.PickByID(id);
        }


        public bool SaveUpazilaPS(ADMUpazilaPS upazilla)
        {
            try
            {
                upazilla.ModifiedDate = System.DateTime.Now;

                if (upazilla.UpazilaPSID <= 0)
                {
                    upazilla.CreatedDate = System.DateTime.Now;
                    return _UpazilaPSDAOLinq.Add(upazilla);
                }
                else
                {
                    return _UpazilaPSDAOLinq.Edit(upazilla);
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
        /// <param name="districtOID"></param>
        /// <returns></returns>
        public IList<ADMUpazilaPS> GetAllUpazilaPSByDistrict(int districtID)
        {
            try
            {
                if (districtID > 0)
                {
                    return _UpazilaPSDAOLinq.All().Where(u => u.DistrictID.Equals(districtID)).ToList();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<ADMUpazilaPS> SearchUpazilaPS(int districtID, string upazillaCode, string upazillaName)
        {
            return _UpazilaPSDAOLinq.SearchUpazilaPS(districtID, upazillaCode, upazillaName);
        }
    }
}
