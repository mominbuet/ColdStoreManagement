using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CSMSys.Lib.AccountingUtility
{
    public class NullManager
    {
        public NullManager() { }
        public NullManager(IDataReader objReader)
        {
            _objReader = objReader;
        }
        #region Fields
        private IDataReader _objReader;
        #endregion

        #region methods
        public bool GetBoolean(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? false : _objReader.GetBoolean(i);

        }
        public byte GetByte(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? (byte)0 : _objReader.GetByte(i);

        }
        public Int16 GetInt16(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? (Int16)0 : _objReader.GetInt16(i);

        }
        public Int32 GetInt32(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return (_objReader.IsDBNull(i)) ? 0 : _objReader.GetInt32(i);

        }
        public Int64 GetInt64(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? 0 : _objReader.GetInt64(i);

        }
        public decimal GetDecimal(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? 0 : _objReader.GetDecimal(i);

        }
        public float GetFloat(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? 0 : _objReader.GetFloat(i);

        }
        public double GetDouble(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? 0 : _objReader.GetDouble(i);

        }

        public double GetMoney(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? 0 : Convert.ToDouble(_objReader.GetValue(i));

        }
        public DateTime GetDateTime(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? DateTime.Now : _objReader.GetDateTime(i);

        }
        public char GetChar(string sColumnName)
        {
            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? Convert.ToChar(string.Empty) : _objReader.GetChar(i);

        }
        public string GetString(string sColumnName)
        {
            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? "" : _objReader.GetString(i);

        }

        public byte[] GetBytes(string sColumnName)
        {

            int i = _objReader.GetOrdinal(sColumnName);
            return _objReader.IsDBNull(i) ? null : (byte[])_objReader.GetValue(i);

        }
        #endregion
    }
}
