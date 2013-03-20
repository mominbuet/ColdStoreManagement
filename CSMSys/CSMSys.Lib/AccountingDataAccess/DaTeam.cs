using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingEntity;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingUtility;
using System.Data;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaTeam
    {
        public DaTeam()
        { 
        }
      
        public int SaveOrUpdateMaster(TeamMaster objTeamMaster, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            int LastID = 0;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction("l1");
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spInsertUpdateTeamMaster";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@TeamID", SqlDbType.Int).Value = objTeamMaster.intTeamID;
                com.Parameters.Add("@TeamNo", SqlDbType.VarChar, 100).Value = objTeamMaster.strTeamNo;
                com.Parameters.Add("@TeamName", SqlDbType.VarChar, 100).Value = objTeamMaster.strTeamName;
                com.Parameters.Add("@BranchID", SqlDbType.Int).Value = objTeamMaster.intBranchID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                
                com.ExecuteNonQuery();
                trans.Commit();
                if (objTeamMaster.intTeamID == 0)
                    LastID = ConnectionHelper.GetID(con, "TeamID", "T_Team_Master");
                else
                    LastID = objTeamMaster.intTeamID;
            }
            catch (Exception ex)
            {
                trans.Rollback("l1");
                throw new Exception("Unable to Save or Update" + ex.Message);
            }
            return LastID;
        }
        public int SaveOrUpdateMaster(TeamMaster objTeamMaster, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
            int LastID = 0;
            try
            {
                com = new SqlCommand("spInsertUpdateTeamMaster", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@TeamID", SqlDbType.Int).Value = objTeamMaster.intTeamID;
                com.Parameters.Add("@TeamNo", SqlDbType.VarChar, 100).Value = objTeamMaster.strTeamNo;
                com.Parameters.Add("@TeamName", SqlDbType.VarChar, 100).Value = objTeamMaster.strTeamName;
                com.Parameters.Add("@BranchID", SqlDbType.Int).Value = objTeamMaster.intBranchID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.ExecuteNonQuery();
                if (objTeamMaster.intTeamID == 0)
                {
                    SqlCommand cmd = new SqlCommand("Select ISNULL(MAX(TeamID),0) FROM T_Team_Master WHERE CompanyID = " + LogInInfo.CompanyID, con, trans);
                    LastID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    LastID = objTeamMaster.intTeamID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return LastID;
        }

        public int SaveOrUpdateDetail(TeamDetail objTeamDetail, SqlConnection con)
        {
          
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spInsertUpdateTeamDetail";
                com.Parameters.Add("@memberID", SqlDbType.Int).Value = objTeamDetail.intMemberID;
                com.Parameters.Add("@TeamID", SqlDbType.Int).Value = objTeamDetail.intTeamID;

                com.Parameters.Add("@MemberName", SqlDbType.VarChar, 100).Value = objTeamDetail.strMemberName;
                com.Parameters.Add("@DesignationID", SqlDbType.Int).Value = objTeamDetail.intDesignationID;
                com.Parameters.Add("@DeptID", SqlDbType.Int).Value = objTeamDetail.intDeptID;
                com.Parameters.Add("@ContactNo", SqlDbType.VarChar, 100).Value = objTeamDetail.strContactNo;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objTeamDetail.strRemarks;

                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to Save or Update " + ex.Message);
            }
            return 0;
        }
        public int SaveOrUpdateDetail(TeamDetail objTeamDetail, SqlConnection con,SqlTransaction trans)
        {

            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spInsertUpdateTeamDetail", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@memberID", SqlDbType.Int).Value = objTeamDetail.intMemberID;
                com.Parameters.Add("@TeamID", SqlDbType.Int).Value = objTeamDetail.intTeamID;
                com.Parameters.Add("@MemberName", SqlDbType.VarChar, 100).Value = objTeamDetail.strMemberName;
                com.Parameters.Add("@DesignationID", SqlDbType.Int).Value = objTeamDetail.intDesignationID;
                com.Parameters.Add("@DeptID", SqlDbType.Int).Value = objTeamDetail.intDeptID;
                com.Parameters.Add("@ContactNo", SqlDbType.VarChar, 100).Value = objTeamDetail.strContactNo;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objTeamDetail.strRemarks;

                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return 0;
        }
        /*
        public void deleteMember(TeamDetail obTeamDetail, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spDeleteMember";
                com.Parameters.Add("@memberID", SqlDbType.Int).Value = obTeamDetail.intMemberID;
                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete " + ex.Message);
            }
        }
         * */
        public void DeleteMember(SqlConnection con, int MemberID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "Delete From T_Team_Detail Where MemberID = @MemID";
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@MemID", SqlDbType.Int).Value = MemberID;
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
        
        //---------------Delete Team--------------------//
        public void deleteTeam(int TeamID, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spDeleteTeam";
                com.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamID;
                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //----------------End of Team Delete----------------------

        public DataTable loadMaster(SqlConnection con, string teamNo)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from T_Team_Master where TeamNo=@TeamNo AND CompanyID=" + LogInInfo.CompanyID.ToString() + " Order By TeamName", con);
            da.SelectCommand.Parameters.Add("@TeamNo", SqlDbType.VarChar, 100).Value = teamNo;
            DataTable dt=new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        
        //-----------For Viewing all Team---------------------
        public DataTable loadMaster(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from T_Team_Master WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " Order By TeamName", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public DataTable loadDetail(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from T_Team_Detail ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public DataTable loadDetail(SqlConnection con, int TeamID)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from T_Team_Detail WHERE TeamID = @TeamID", con);
            da.SelectCommand.Parameters.Add("@TeamID", SqlDbType.Int).Value = TeamID;
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public DataTable loadDepartment(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from T_Department WHERE CompanyID = " + LogInInfo.CompanyID, con);
            DataTable dtt = new DataTable();
            da.Fill(dtt);
            da.Dispose();
            return dtt;
        }

        public DataTable loadDesignation(SqlConnection con)
        {
            DataTable dtDes = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select DesignationID," +
                "Name,CompanyID from Designation WHERE CompanyID = " + LogInInfo.CompanyID, con);
                da.Fill(dtDes);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtDes;
        }

        public DataTable loadBranch(SqlConnection con)
        {
            DataTable dtMas = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from T_Branch WHERE CompanyID=" + LogInInfo.CompanyID.ToString(), con);
                da.Fill(dtMas);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtMas;
        }
        public TeamDetail getTeamMember(SqlConnection con, int memberID)
        {
            DataTable dt = new DataTable();
            TeamDetail objTeamMember = null;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_Team_Detail WHERE memberID=" + memberID.ToString(), con);
               
                da.Fill(dt);
                da.Dispose();

                if (dt.Rows.Count == 0) return null;
                objTeamMember = new TeamDetail();
                objTeamMember.intMemberID = dt.Rows[0].Field<int>("MemberID");
                objTeamMember.intTeamID = dt.Rows[0].Field<int>("TeamID");
                objTeamMember.strMemberName = dt.Rows[0].Field<string>("MemberName");
                objTeamMember.strRemarks = dt.Rows[0].Field<string>("Remarks");
                objTeamMember.strContactNo = dt.Rows[0].Field<string>("ContactNo");
                objTeamMember.intDesignationID = dt.Rows[0].Field<int>("DesignationID");
                objTeamMember.intDeptID = dt.Rows[0].Field<int>("DeptID");
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objTeamMember;
        }

        public DataTable loadMembers(SqlConnection con, string TeamName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(" Select * from T_Team_Detail Where TeamID = '" + TeamName.ToString()+"'", con);
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable loadSelectedCustomer(SqlConnection con, string strQuerry )
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(strQuerry, con);
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
    }
}
