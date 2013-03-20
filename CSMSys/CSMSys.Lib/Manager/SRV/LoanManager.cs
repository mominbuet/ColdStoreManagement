using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.SRV
{
    public class LoanManager
    {
        #region Properties
        LoanDAOLinq _loanDaoLinq;
        LoanCollectionDAOLinq _loanDaoCollectionLinq;
        #endregion
        #region Constructor
        public LoanManager()
        {
            _loanDaoLinq = new LoanDAOLinq();
            _loanDaoCollectionLinq = new LoanCollectionDAOLinq();

        }
        #endregion
        public bool updateSRVRegistration(string serialno,int party,float loan,int bags,int caseID)
        {
            try
            {
                SRVRegistration temp  = new SRVRegistration();
                SRVRegistration srv = new RegistrationDAOLinq().SearchSRVBySerialParty(serialno, party);
                temp.Requisitioned = "Loan Approved";
                temp.LoanDisbursed = loan;
                temp.RegistrationID = srv.RegistrationID;
                temp.SerialID = srv.SerialID;
                temp.SerialNo = srv.SerialNo;
                temp.PartyID = srv.PartyID;
                temp.BagLoan = srv.BagLoan;
                temp.CarryingLoan = srv.CarryingLoan;
                temp.Remarks = srv.Remarks;
                temp.CreatedBy = srv.CreatedBy;
                temp.CreatedDate = srv.CreatedDate;
                temp.ModifiedBy = srv.ModifiedBy;
                temp.ModifiedDate = DateTime.Now;
                temp.Bags = bags;
                temp.CaseID = caseID;
                return new RegistrationManager().SaveRegistration(temp);
               
            }
            catch (Exception ex)
            {
                return false;
            }    
        }
        
        public bool updateSRVRegistration(string serialno, int party,int caseid)
        {
            try
            {
                SRVRegistration temp = new SRVRegistration();
                SRVRegistration srv = new RegistrationDAOLinq().SearchSRVBySerialParty(serialno, party);
                temp.Requisitioned = "Loan Disbursed";
                temp.LoanDisbursed = srv.LoanDisbursed;
                temp.RegistrationID = srv.RegistrationID;
                temp.SerialID = srv.SerialID;
                temp.SerialNo = srv.SerialNo;
                temp.PartyID = srv.PartyID;
                temp.BagLoan = srv.BagLoan;
                temp.CarryingLoan = srv.CarryingLoan;
                temp.Remarks = srv.Remarks;
                temp.CreatedBy = srv.CreatedBy;
                temp.CreatedDate = srv.CreatedDate;
                temp.ModifiedBy = srv.ModifiedBy;
                temp.ModifiedDate = DateTime.Now;
                temp.Bags = srv.Bags;
                temp.CaseID = caseid;
                return new RegistrationManager().SaveRegistration(temp);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SaveOrEditLoanDisburse(SRVLoanDisburse loc)
        {
            try
            {
                if (loc.LoanID == 0)
                {
                    return _loanDaoLinq.Add(loc);
                }
                else
                {
                    return _loanDaoLinq.Edit(loc);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool SaveOrEditLoanCollection(SRVLoanCollection loc)
        {
            try
            {
                if (loc.LCollectionID == 0)
                {
                    return _loanDaoCollectionLinq.Add(loc);
                }
                else
                {
                    return _loanDaoCollectionLinq.Edit(loc);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public SRVLoanDisburse GetLoanDByID(int id)
        {
            return _loanDaoLinq.PickByID(id);
        }
        public bool DeleteLoan(SRVLoanDisburse serial)
        {
            try
            {
                if (serial.LoanID > 0)
                {
                    return _loanDaoLinq.Delete(serial, true);
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
        public IList<SRVRegistration> GetAllSerialByPartyForRequisition(int pid,string req)
        {
            return _loanDaoLinq.getAllRequisitionByParty(pid, req);
        }
        public IList<INVStockSerial> GetAllSerialDorDisburse(int pid)
        {
            List<long> serids = new List<long>();
            IList<INVStockSerial> res = new List<INVStockSerial>();
            IList<INVStockSerial> result = new List<INVStockSerial>();
            IList<INVStockSerial> allserialByParty = new SerialManager().GetAllSerialByParty(pid);
            IList<SRVLoanDisburse> allloansfrompid = _loanDaoLinq.getAllLoansByParty(pid);
            foreach (SRVLoanDisburse srvLoanDisburse in allloansfrompid)
            {
                string[] serials = srvLoanDisburse.serialIDs.Split(',');
                foreach (string serial in serials)
                {
                    if (serial != "")
                    {
                        INVStockSerial iss = new SerialDAOLinq().PickByID(int.Parse(serial));
                        res.Add(iss);
                        serids.Add(iss.SerialID);
                    }
                }

            }
            //result = allserialByParty;
            foreach (INVStockSerial re in res)
            {
                //INVStockSerial[] arrss = allserialByParty.ToArray();
                //int i = 0;
                foreach (INVStockSerial ss in allserialByParty)
                {
                    if (ss.SerialID != re.SerialID && !serids.Contains( ss.SerialID))
                    {

                         //allserialByParty.RemoveAt(i);
                        result.Add(ss);
                    }
                    //i++;
                }
                //allserialByParty.Remove(re);
            }
            //result = allserialByParty.Except(res).ToList();
            return (res.Count > 0) ? result.Distinct().ToList() : allserialByParty;
        }
        public IList<INVStockSerial> GetAllSerialForCollection(int pid,IList<INVStockSerial> invss )
        {
            List<long> serids = new List<long>();
            IList<INVStockSerial> res = new List<INVStockSerial>();
            IList<INVStockSerial> result = new List<INVStockSerial>();
            IList<INVStockSerial> allserialByParty = invss;
            IList<SRVLoanCollection> allloansfrompid = _loanDaoCollectionLinq.getAllLoansByParty(pid);
            foreach (SRVLoanCollection srvLoancollection in allloansfrompid)
            {
                string[] serials = srvLoancollection.SerialIDs.Split(',');
                foreach (string serial in serials)
                {
                    if (serial != "")
                    {
                        INVStockSerial iss = new SerialDAOLinq().PickByID(int.Parse(serial));
                        res.Add(iss);
                        serids.Add(iss.SerialID);
                    }
                }

            }
            //result = allserialByParty;
            foreach (INVStockSerial re in res)
            {
                //INVStockSerial[] arrss = allserialByParty.ToArray();
                //int i = 0;
                foreach (INVStockSerial ss in allserialByParty)
                {
                    if (ss.SerialID != re.SerialID && !serids.Contains(ss.SerialID))
                    {

                        //allserialByParty.RemoveAt(i);
                        result.Add(ss);
                    }
                    //i++;
                }
                //allserialByParty.Remove(re);
            }
            //result = allserialByParty.Except(res).ToList();
            return (res.Count > 0) ? result.Distinct().ToList() : allserialByParty;
        }
        public Int32 GetNextCaseNo()
        {
            try
            {
                return Convert.ToInt32(_loanDaoLinq.All().ToList().Last().LoanID);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IList<SRVLoanDisburse> GetAllLoanDisbursed()
        {
            try
            {
                return _loanDaoLinq.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<SRVLoanDisburse> GetAllLoanDtoParty(long pid)
        {
            try
            {
                return _loanDaoLinq.All().Where(u => u.PartyID.Equals(pid)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        
    }
}
