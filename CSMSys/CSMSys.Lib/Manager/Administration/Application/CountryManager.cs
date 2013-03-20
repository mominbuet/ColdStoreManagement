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
    public class CountryManager
    {
        #region Properties
        ICountryDAO _CountryDAOLinq;
        #endregion

        #region Constructor
        public CountryManager()
        {
            _CountryDAOLinq = new CountryDAOLinq();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Country from Country table 
        /// </summary>
        /// <returns></returns>
        public IList<ADMCountry> GetAllCountry()
        {
            try
            {
                return _CountryDAOLinq.All().ToList();
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
        public ADMCountry GetCountryByID(int id)
        {
            return _CountryDAOLinq.PickByID(id);
        }

        /// <summary>
        /// Method to save (add/edit) country object
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool SaveCountry(ADMCountry country)
        {
            try
            {
                country.ModifiedDate = System.DateTime.Now;

                if (country.CountryID == 0)
                {
                    return _CountryDAOLinq.Add(country);
                }
                else
                {
                    return _CountryDAOLinq.Edit(country, true);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IList<ADMCountry> SearchCountry(string countryCode, string countryName)
        {
            return _CountryDAOLinq.SearchCountry(countryCode, countryName);
        }
        #endregion
    }
}
