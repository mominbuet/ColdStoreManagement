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
    public class SerialManager
    {
        #region Properties
        ISerialDAO _SerialDAOLinq;
        #endregion

        #region Constructor
        public SerialManager()
        {
            _SerialDAOLinq = new SerialDAOLinq();
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Get all Serial from Serial table 
        /// </summary>
        /// <returns></returns>
        public long getnextserial()
        {
            return _SerialDAOLinq.getNextSerialNo();
        }
        public IList<INVStockSerial> GetAllSerial()
        {
            try
            {
                return _SerialDAOLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<INVStockSerial> GetAllSerialByParty(int pid)
        {
            try
            {
                return _SerialDAOLinq.All().Where(u=>u.PartyID.Equals(pid)).ToList();
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
        public INVStockSerial GetSerialByID(int id)
        {
            return _SerialDAOLinq.PickByID(id);
        }

        /// <summary>
        /// Method to save (add/edit) party object
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool SaveSerial(INVStockSerial serial)
        {
            try
            {
                if (serial.SerialID == 0)
                {
                    return _SerialDAOLinq.Add(serial);
                }
                else
                {
                    return _SerialDAOLinq.Edit(serial);      //bool deleted
                }

                //if (_SerialDAOLinq.Exists(serial))
                //{
                //    return _SerialDAOLinq.Edit(serial, true);                    
                //}
                //else
                //{
                //    return _SerialDAOLinq.Add(serial);
                //}
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteSerial(INVStockSerial serial)
        {
            try
            {
                if (serial.SerialID > 0)
                {
                    return _SerialDAOLinq.Delete(serial, true);
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

        public IList<INVStockSerial> SearchSerial(string serialNo, float bag)
        {
            return _SerialDAOLinq.SearchSerial(serialNo, bag);
        }
        #endregion
    }
}
