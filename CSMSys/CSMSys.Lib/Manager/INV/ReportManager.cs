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
    public class ReportManager
    {
        #region Properties
        IReportDAO _ReportDAOLinq;
        #endregion

        #region Constructor
        public ReportManager()
        {
            _ReportDAOLinq = new ReportDAOLinq();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Party from Party table 
        /// </summary>
        /// <returns></returns>
        public IList<INVParty> GetAllParty()
        {
            try
            {
                return _ReportDAOLinq.All().ToList();
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
        public INVParty GetPartyByID(int id)
        {
            return _ReportDAOLinq.PickByID(id);
        }


        /// <summary>
        /// Method to save (add/edit) party object
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool SaveReport(SRVRegistration party)
        {
            try
            {
                if (party.RegistrationID == 0)
                {
                    return new RegistrationDAOLinq().Add(party);
                }
                else
                {
                    return new RegistrationDAOLinq().Edit(party);
                }
               // return new RegistrationDAOLinq().Add(party);
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public ADMDistrict getdist(Int32 id)
        {
            return new DistrictDAOLinq(false).PickByID(id);
        }
        public ADMUpazilaPS getupzilla(Int32 id)
        {
            return new UpazilaPSDAOLinq(false).PickByID(id);
        }
        public IList<INVParty> SearchPartyByCode(string partyCode)
        {
            return _ReportDAOLinq.SearchPartyByCode(partyCode);
        }

        public IList<INVParty> SearchParty(string partyCode, string partyName)
        {
            return _ReportDAOLinq.SearchParty(partyCode, partyName);
        }
        #endregion
    }
}
