using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingUtility;


using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DesignationDA  
    {
        public DesignationDA() { }

        #region CreateObjects
        private Designation  CreateObject(IDataReader oReader)
        {
            Designation objDesg = new Designation();
            NullManager reader = new NullManager(oReader);

            try
            {
                objDesg.DesignationID = reader.GetInt32("DesignationID");
                objDesg.DesignationName = reader.GetString("Name");
                objDesg.PayScaleID = reader.GetInt32("PayScaleID");

                objDesg.CompanyID = reader.GetInt32("CompanyID");
                objDesg.UserID = reader.GetInt32("UserID");
                objDesg.ModifiedDate = reader.GetDateTime("ModifiedDate");

            }
            catch (Exception Ex)
            {
                throw new Exception("Error while creating object" + Ex.Message);
            }
            return objDesg;
        }
        #endregion

        public ArrayList getDesignation(int id)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            //SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                //trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                //com.Transaction = trans;
                com.CommandText = "Select * FROM Designation WHERE (DesignationID = @DesgID OR (@DesgID=0 AND CompanyID = @CompanyID))  Order by name";
                com.Parameters.Add("@DesgID", SqlDbType.Int).Value = id;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;

                IDataReader oReader = com.ExecuteReader();
                while (oReader.Read())
                {
                    list.Add(CreateObject(oReader));
                }
                oReader.Close();
                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
               
                throw new Exception("Can not get designation " + Ex.Message);

            }
            return list;            
        }
        public ArrayList getDesignation(int numDesgID,double dSalary)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            //SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                //trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                //com.Transaction = trans;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "SpGetDesignationBySalary";
                com.Parameters.Add("@DesignationID", SqlDbType.Int).Value = numDesgID;
                com.Parameters.Add("@Salary", SqlDbType.Money).Value = dSalary;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;

                IDataReader oReader = com.ExecuteReader();
                while (oReader.Read())
                {
                    list.Add(CreateObject(oReader));
                }
                oReader.Close();
                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {

                throw new Exception("Can not get designation " + Ex.Message);

            }
            return list;
        }
        public DataTable getDesignationANDPayScale(int id)
        {

            DataTable dt = new DataTable();
            try
            {

                SqlConnection con = ConnectionHelper.getConnection();
                string qstr = "SELECT     Designation.DesignationID, Designation.Name AS Designation, PayScaleType.Name PayScale, PayScale.GradeOrScale"
                                + " FROM PayScaleType INNER JOIN  PayScale ON PayScaleType.PayScaleTypeID = PayScale.PayScaleTypeID RIGHT OUTER JOIN "
                                + " Designation ON PayScale.PayScaleID = Designation.PayScaleID WHERE     (Designation.DesignationID = @DesgID) OR "
                                + " (@DesgID = 0) AND (Designation.CompanyID = @CompanyID) ORDER BY Designation,  PayScaleType.Name, PayScale.GradeOrScale";
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                da.SelectCommand.Parameters.Add("@DesgID", SqlDbType.Int).Value = id;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
                ConnectionHelper.closeConnection(con);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;


        }
        public int SaveUpdateDesignation(Designation objDesg)
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

                if (objDesg.DesignationID == 0)
                {

                    objDesg.DesignationID = ConnectionHelper.GenerateID(con, com, "DesignationID", "Designation");

                    com.CommandText = "Insert Into Designation(CompanyID, UserID, ModifiedDate, DesignationID, Name, PayScaleID) "
                                      + " Values(@CompanyID, @UserID, @ModifiedDate, @DesgID, @Name, @PSTID)";

                }
                else
                {
                    com.CommandText = "Update Designation SET CompanyID = @CompanyID, UserID =@UserID, ModifiedDate = @ModifiedDate, Name = @Name,  PayScaleID= @PSTID WHERE DesignationID = @DesgID";

                }

                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = LogInInfo.ModifiedDate;
                com.Parameters.Add("@DesgID", SqlDbType.Int).Value = objDesg .DesignationID ;
                com.Parameters.Add("@Name", SqlDbType.VarChar, 200).Value = objDesg.DesignationName ;
                if(objDesg.PayScaleID <=0 )
                com.Parameters.Add("@PSTID", SqlDbType.Int ).Value = DBNull.Value;
                else
                 com.Parameters.Add("@PSTID", SqlDbType.Int ).Value =  objDesg.PayScaleID  ;
                


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
                throw new Exception("Can not save or update" + Ex.Message);

            }


            return objDesg.DesignationID ; 
        }

        public string getPayScaleTypeName(int id)
        {
            string strTypeName=null;
            SqlConnection con = null;
            SqlCommand  cmd = null;
            try
            {
                con = ConnectionHelper.getConnection();
                cmd = new SqlCommand("SELECT name FROM PayScaleType WHERE PayScaleID=@PSTID",con);
                cmd.Parameters.Add("@PSTID", SqlDbType.Int).Value = id;
                IDataReader objReader = cmd.ExecuteReader();

                while (objReader.Read())
                {
                    strTypeName = objReader.GetString(objReader.GetOrdinal("name"));
                }
                objReader.Close();

                ConnectionHelper.closeConnection(con);
            }
               
            catch (Exception ex)
            {
                strTypeName = null;
                throw new Exception("Error in retrieve payscaletype name " + ex.Message);
            }
            return strTypeName;
        }
        public void DeleteDesignation(int id)
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
                com.CommandText = "DELETE FROM Designation WHERE DesignationID = @DesgID";
                com.Parameters.Add("@DesgID", SqlDbType.Int).Value = id;
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
                throw new Exception("Can not delete designation " + Ex.Message);

            }
        }
    }
}
