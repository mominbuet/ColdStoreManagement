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
    public class DivisionManager
    {
        #region Properties
        IDivisionDAO _DivisionDAOLinq;
        #endregion

        #region Constructor
        public DivisionManager(bool isLoadWith)
        {
            _DivisionDAOLinq = new DivisionDAOLinq(isLoadWith);
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Division from Division table 
        /// </summary>
        /// <returns></returns>
        public IList<ADMDivision> GetAllDivision()
        {
            try
            {
                return _DivisionDAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get all Country from Country table 
        /// </summary>
        /// <returns></returns>
        public IList<ADMDivision> GetAllDivisionByCountry(int countryID)
        {
            try
            {
                if (countryID > 0)
                {
                    return _DivisionDAOLinq.All().Where(d => d.CountryID.Equals(countryID)).ToList();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get country object by OID
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public ADMDivision GetDivisionByID(int id)
        {
            return _DivisionDAOLinq.PickByID(id);
        }

        /// <summary>
        /// Method to save (add/edit) country object
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool SaveDivision(ADMDivision division)
        {
            try
            {
                division.ModifiedDate = System.DateTime.Now;

                if (division.DivisionID == 0)
                {
                    return _DivisionDAOLinq.Add(division);
                }
                else
                {
                    return _DivisionDAOLinq.Edit(division, true);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IList<ADMDivision> SearchDivision(string divisionCode, string divisionName)
        {
            return _DivisionDAOLinq.SearchDivision(divisionCode, divisionName);
        }
        #endregion
    }
}
