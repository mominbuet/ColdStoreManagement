using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaSection
    {
        public DaSection() { }
        public void deleteSection(SqlConnection con, int SectionId)
        {
            SqlTransaction trans = null;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "Delete from Section Where SectionID = @SectionID";
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@SectionID", SqlDbType.Int).Value = SectionId;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public int SaveUpdateSection(Section obSection, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateSection";
                com.CommandType = CommandType.StoredProcedure;
                if (obSection.SectionID == -1)
                    com.Parameters.Add("@SectionID", SqlDbType.Int).Value = 0;
                else
                    com.Parameters.Add("@SectionID", SqlDbType.Int).Value = obSection.SectionID;
                com.Parameters.Add("@Name", SqlDbType.VarChar, 200).Value = obSection.Name;
                com.Parameters.Add("@Description", SqlDbType.VarChar, 200).Value = obSection.Description;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
            return 0;
        }
    }
}
