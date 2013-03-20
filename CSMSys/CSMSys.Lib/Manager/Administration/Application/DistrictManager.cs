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
    public class DistrictManager
    {
        #region Properties
        IDistrictDAO _DistrictDAOLinq;
        #endregion

        #region Constructor
        public DistrictManager(bool isLoadWith)
        {
            _DistrictDAOLinq = new DistrictDAOLinq(isLoadWith);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get all District from District table 
        /// </summary>
        /// <returns></returns>
        public IList<ADMDistrict> GetAllDistrict()
        {
            try
            {
                return _DistrictDAOLinq.All().ToList();
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
        public IList<ADMDistrict> GetAllDistrictByDivision(int divisionID)
        {
            try
            {
                if (divisionID > 0)
                {
                    return _DistrictDAOLinq.All().Where(d => d.DivisionID.Equals(divisionID)).ToList();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get district object by OID
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ADMDistrict GetDistrictByID(int id)
        {
            return _DistrictDAOLinq.PickByID(id);
        }

        /// <summary>
        /// Method to save (add/edit) district object
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public bool SaveDistrict(ADMDistrict district)
        {
            try
            {
                district.ModifiedDate = System.DateTime.Now;

                if (district.DistrictID == 0)
                {
                    return _DistrictDAOLinq.Add(district);
                }
                else
                {
                    return _DistrictDAOLinq.Edit(district, true);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IList<ADMDistrict> SearchDistrict(string districtCode, string districtName)
        {
            return _DistrictDAOLinq.SearchDistrict(districtCode, districtName);
        }
        #endregion
    }
}
