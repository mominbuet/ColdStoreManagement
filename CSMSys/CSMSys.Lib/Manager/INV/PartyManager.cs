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
    public class PartyManager
    {
        #region Properties
        IPartyDAO _PartyDAOLinq;
        #endregion

        #region Constructor
        public PartyManager()
        {
            _PartyDAOLinq = new PartyDAOLinq();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Party from Party table 
        /// </summary>
        /// <returns></returns>
        public long getNextPartyID()
        {
            return _PartyDAOLinq.getnextPartyID()+100;
        }
        public IList<INVParty> GetAllParty()
        {
            try
            {
                return _PartyDAOLinq.All().ToList();
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
            return _PartyDAOLinq.PickByID(id);
        }

        /// <summary>
        /// Method to save (add/edit) party object
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool SaveParty(INVParty party)
        {
            try
            {
                if (party.PartyID == 0)
                {
                    return _PartyDAOLinq.Add(party);
                }
                else
                {
                    return _PartyDAOLinq.Edit(party);      //bool deleted
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteParty(INVParty party)
        {
            try
            {
                if (party.PartyID > 0)
                {
                    return _PartyDAOLinq.Delete(party);    //bool deleted
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

        public IList<INVParty> SearchPartyByCode(string partyCode)
        {
            return _PartyDAOLinq.SearchPartyByCode(partyCode);
        }

        public IList<INVParty> SearchParty(string partyCode, string partyName)
        {
            return _PartyDAOLinq.SearchParty(partyCode, partyName);
        }
        #endregion
    }
}
