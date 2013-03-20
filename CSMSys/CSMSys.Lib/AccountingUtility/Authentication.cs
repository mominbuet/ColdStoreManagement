using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace CSMSys.Lib.AccountingUtility
{
    public class Authentication
    {
        public Authentication() { }
        public Authentication(string strModulesName)
        {
            Initialization();
            AuthenticateModule(strModulesName);
        }
        public Authentication(ArrayList lRbName, bool bIsReport)
        {
            Initialization();
            _bIsReport = bIsReport;
            AuthenticateReport(lRbName);
        }
        private int _numUserID;
        private int _numCompanyID;
        #region Fields
        private bool _bCanView;
        private bool _bCanEdit;
        private bool _bCanAdd;
        private bool _bCanDelete;
        private bool _bIsReport;
        #endregion
        private ArrayList _lCanView = new ArrayList();
        private void Initialization()
        {
            _numCompanyID = LogInInfo.CompanyID;
            _numUserID = LogInInfo.UserID;
            _bCanView = false;
            _bCanEdit = false;
            _bCanAdd = false;
            _bCanDelete = false;
            _bIsReport = false;
        }
        #region Properties
        public bool CanEdit
        {
            get { return _bCanEdit; }
            set { _bCanEdit = value; }
        }
        public bool CanDelete
        {
            get { return _bCanDelete; }
            set { _bCanDelete = value; }
        }
        public bool CanAdd
        {
            get { return _bCanAdd; }
            set { _bCanAdd = value; }
        }
        public bool CanView
        {
            get { return _bCanView; }
            set { _bCanView = value; }
        }
        #endregion

        public ArrayList ListCanView
        {
            get { return _lCanView; }
            set { _lCanView = value; }
        }
        private void AuthenticateModule(string strModulesName)
        {
            SqlConnection con = null;
            DataTable dt = new DataTable();
            try
            {
                con = ConnectionHelper.getConnection();
                SqlDataAdapter da = new SqlDataAdapter("spGetPrivilegesAllForAccounting", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = _numUserID;
                da.SelectCommand.Parameters.Add("@ParentMenuName", SqlDbType.VarChar, 100).Value = "all";
                da.SelectCommand.Parameters.Add("@ModulesName", SqlDbType.VarChar, 100).Value = strModulesName;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = _numCompanyID;

                da.Fill(dt);
                da.Dispose();
                CreateObject(dt);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);

            }
        }
        private ArrayList AuthenticateReport(ArrayList lRbName)
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;
            //int[] numAuthentication = new int[4];
            try
            {
                con = ConnectionHelper.getConnection();
                SqlDataAdapter da = null;
                //com.CommandText = "SELECT CanView FROM UserReportPrivilege WHERE UserID=@UserID AND RbName=@RbName AND CompanyID=@CompanyID";
                foreach (string strRbName in lRbName)
                {
                    da = new SqlDataAdapter("SELECT CanView FROM UserReportPrivilege WHERE UserID=@UserID AND RbName=@RbName AND CompanyID=@CompanyID", con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.SelectCommand.Parameters.Clear();
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = _numUserID;
                    da.SelectCommand.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = strRbName;
                    da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = _numCompanyID;
                    da.Fill(dt);
                    da.Dispose();
                    //IDataReader objReader = com.ExecuteReader();

                        CreateObject(dt);
                    _lCanView.Add(_bCanView);
                }

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);

            }
            return _lCanView;
        }
        #region CreateObjects
        private void CreateObject(DataTable dt)
        {
            if (dt.Rows.Count == 0) return;
            try
            {
                int rowId = dt.Rows.Count;
                for (int i = 0; i < rowId; i++)
                {
                    if (!_bIsReport)
                    {
                        _bCanEdit = Convert.ToInt32(dt.Rows[i].Field<object>("CanEdit")) == 1 ? true : false;
                        _bCanDelete = Convert.ToInt32(dt.Rows[i].Field<object>("CanDelete")) == 1 ? true : false;
                        _bCanAdd = Convert.ToInt32(dt.Rows[i].Field<object>("CanAdd")) == 1 ? true : false;
                    }
                    _bCanView = Convert.ToInt32(dt.Rows[i].Field<object>("CanView")) == 1 ? true : false;

                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        #endregion
    }
}
