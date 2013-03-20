using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingEntity;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaCompany
    {

        public DaCompany() { }

        public int SaveOrUpdate(Company objCompany)
        {

            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                if (objCompany.CompanyID == 0)
                {

                    objCompany.CompanyID = ConnectionHelper.GetID(con, "CompanyID", "Company");

                   com.CommandText = "Insert Into Company(CompanyID, CompanyName, AddressLine1, AddressLine2, Phone, Fax, WebSite, Email, BusinessSubTypeID, TradeLicense, TINno, IRCNo, ERCNo, MembershipNo1, MembershipNo2, ContactPerson, ContactPersonPhone, CompanyLogo, CurrencyID ) "
                                   + " Values(@CompanyID, @CompanyName, @AddressLine1, @AddressLine2, @Phone, @Fax, @WebSite, @Email, @BusinessSubTypeID, @TradeLicense, @TINno, @IRCNo, @ERCNo, @MembershipNo1, @MembershipNo2, @ContactPerson, @ContactPersonPhone, @CompanyLogo, @CurrencyID)";

                    //com.CommandText = "Insert Into Company(CompanyID, CompanyName, AddressLine1, AddressLine2, Phone, Fax, WebSite, Email, BusinessSubTypeID, TradeLicense, TINno, IRCNo, ERCNo, MembershipNo1, MembershipNo2, ContactPerson, ContactPersonPhone, CompanyLogo,CurrencyID ) "
                      //                + " Values(@CompanyID, @CompanyName, @AddressLine1, @AddressLine2, @Phone, @Fax, @WebSite, @Email, @BusinessSubTypeID, @TradeLicense, @TINno, @IRCNo, @ERCNo, @MembershipNo1, @MembershipNo2, @ContactPerson, @ContactPersonPhone,@CompanyLogo, @CurrencyID)";

                }
                else
                {
                   com.CommandText = "Update Company SET CompanyName = @CompanyName, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, Phone = @Phone, Fax = @Fax, WebSite = @WebSite, Email = @Email, BusinessSubTypeID = @BusinessSubTypeID, TradeLicense = @TradeLicense, TINno = @TINno, IRCNo = @IRCNo, ERCNo = @ERCNo, MembershipNo1 = @MembershipNo1, MembershipNo2 = @MembershipNo2, ContactPerson = @ContactPerson, ContactPersonPhone = @ContactPersonPhone, "
                        +" CompanyLogo = @CompanyLogo, CurrencyID = @CurrencyID WHERE CompanyID = @CompanyID";
                   // com.CommandText = "Update Company SET CompanyName = @CompanyName, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, Phone = @Phone, Fax = @Fax, WebSite = @WebSite, Email = @Email, BusinessSubTypeID = @BusinessSubTypeID, TradeLicense = @TradeLicense, TINno = @TINno, IRCNo = @IRCNo, ERCNo = @ERCNo, MembershipNo1 = @MembershipNo1, MembershipNo2 = @MembershipNo2, ContactPerson = @ContactPerson, ContactPersonPhone = @ContactPersonPhone, CurrencyID = @CurrencyID WHERE CompanyID = @CompanyID";

                }
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = objCompany.CompanyID;
                com.Parameters.Add("@CompanyName", SqlDbType.VarChar, 300).Value = objCompany.CompanyName;
                com.Parameters.Add("@AddressLine1", SqlDbType.VarChar, 500).Value = objCompany.AddressLine1;
                com.Parameters.Add("@AddressLine2", SqlDbType.VarChar, 500).Value = objCompany.AddressLine2;
                com.Parameters.Add("@Phone", SqlDbType.VarChar, 500).Value = objCompany.Phone;
                com.Parameters.Add("@Fax", SqlDbType.VarChar, 200).Value = objCompany.Fax;
                com.Parameters.Add("@WebSite", SqlDbType.VarChar, 100).Value = objCompany.WebSite;
                com.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = objCompany.Email;
                com.Parameters.Add("@BusinessSubTypeID", SqlDbType.Int).Value = objCompany.BusinessSubTypeID;
                com.Parameters.Add("@TradeLicense", SqlDbType.VarChar, 100).Value = objCompany.TradeLicense;
                com.Parameters.Add("@TINno", SqlDbType.VarChar, 100).Value = objCompany.TINno;
                com.Parameters.Add("@IRCNo", SqlDbType.VarChar, 100).Value = objCompany.IRCNo;
                com.Parameters.Add("@ERCNo", SqlDbType.VarChar, 100).Value = objCompany.ERCNo;
                com.Parameters.Add("@MembershipNo1", SqlDbType.VarChar, 100).Value = objCompany.MembershipNo1;
                com.Parameters.Add("@MembershipNo2", SqlDbType.VarChar, 100).Value = objCompany.MembershipNo2;
                com.Parameters.Add("@ContactPerson", SqlDbType.VarChar, 100).Value = objCompany.ContactPerson;
                com.Parameters.Add("@ContactPersonPhone", SqlDbType.VarChar, 100).Value = objCompany.ContactPersonPhone;
                if (objCompany.CompanyLogo == null)
                    com.Parameters.Add("@CompanyLogo", SqlDbType.Image).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CompanyLogo", SqlDbType.Image).Value= objCompany.CompanyLogo;
                com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objCompany.CurrencyID;
               
                

                com.ExecuteNonQuery();
                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    ConnectionHelper.closeConnection(con);
                }
                throw new Exception("Can not save or update" + Ex.Message);

            }


            return objCompany.CompanyID;

        }

        public ArrayList getCompany(int numCompanyID)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                if (numCompanyID == 0)
                {
                    com.CommandText = "Select * FROM Company";
                }
                else
                {
                    com.CommandText = "Select * FROM Company WHERE CompanyID = @CompanyID";
                    com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = numCompanyID;
                }


                IDataReader objReader = com.ExecuteReader();
                while (objReader.Read())
                {
                    list.Add(CreateObject(objReader));

                }
                objReader.Close();

                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get Company" + Ex.Message);

            }
            return list;

        }

        public void Delete(int numCompanyID)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "DELETE FROM Company WHERE CompanyID = @CompanyID";
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = numCompanyID;
                com.ExecuteNonQuery();
                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get Company" + Ex.Message);

            }

        }


        #region CreateObjects
        private Company CreateObject(IDataReader objReader)
        {
            
            Company objCompany = new Company();
            NullManager oNullManager = new NullManager(objReader);

            try
            {
                objCompany.CompanyID = oNullManager.GetInt32("CompanyID");
                objCompany.CompanyName = oNullManager.GetString("CompanyName");
                objCompany.AddressLine1 = oNullManager.GetString("AddressLine1");
                objCompany.AddressLine2 = oNullManager.GetString("AddressLine2");
                objCompany.Phone = oNullManager.GetString("Phone");
                objCompany.Fax = oNullManager.GetString("Fax");
                objCompany.WebSite = oNullManager.GetString("WebSite");
                objCompany.Email = oNullManager.GetString("Email");
                objCompany.BusinessSubTypeID = oNullManager.GetInt32("BusinessSubTypeID");
                objCompany.TradeLicense = oNullManager.GetString("TradeLicense");
                objCompany.TINno = oNullManager.GetString("TINno");
                objCompany.IRCNo = oNullManager.GetString("IRCNo");
                objCompany.ERCNo = oNullManager.GetString("ERCNo");
                objCompany.MembershipNo1 = oNullManager.GetString("MembershipNo1");
                objCompany.MembershipNo2 = oNullManager.GetString("MembershipNo2");
                objCompany.ContactPerson = oNullManager.GetString("ContactPerson");
                objCompany.ContactPersonPhone = oNullManager.GetString("ContactPersonPhone");
                objCompany.CompanyLogo = oNullManager.GetBytes("CompanyLogo");
                objCompany.CurrencyID = oNullManager.GetInt32("CurrencyID");

            }
            catch (Exception Ex)
            {
                throw new Exception("Error while creating object" + Ex.Message);
            }
            return objCompany;
        }
        #endregion
    }
}
