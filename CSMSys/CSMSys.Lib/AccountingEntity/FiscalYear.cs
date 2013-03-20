using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace CSMSys.Lib.AccountingEntity
{
    public class FiscalYear : BaseObject
    {
        public FiscalYear() : base() { }

        private int _numFiscalYearID;
        private string _strTitile;
        private DateTime _dtstartdate;
        private DateTime _dtenddate;


        public int FiscalYearID
        {
            get { return _numFiscalYearID; }
            set { _numFiscalYearID = value; }
        }
        public string Titile
        {
            get { return _strTitile; }
            set { _strTitile = value; }
        }
        public DateTime StartDate
        {
            get { return _dtstartdate; }
            set { _dtstartdate = value; }
        }
        public DateTime EndDate
        {
            get { return _dtenddate; }
            set { _dtenddate = value; }
        }


        

    }
}
