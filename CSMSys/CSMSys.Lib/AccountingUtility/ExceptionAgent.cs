using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace CSMSys.Lib.AccountingUtility
{
   public class ExceptionAgent
    {
        public ExceptionAgent() { }
        
        #region Fields
        private static Exception _execption;
        private static string _strFilepath = "Errors.xml";
        #endregion
        #region Properties
        public Exception Execption
        {
            get { return _execption; }
            set { _execption = value; }
        }

        #endregion
        #region methods
        public static void SaveError(Exception exception)
        {
            _execption = exception;
            DataSet dsXml = new DataSet();

            try
            {
                dsXml.ReadXml(_strFilepath);
            }

            catch (Exception ex)
            {
                if (ex == null) return;
                #region if file not exists or blank
                if (File.Exists(_strFilepath) == false)
                {

                    FileStream fs = File.Create(_strFilepath);
                    fs.Close();
                }

                string strStructure = "<Errors>";
                strStructure += "<Error>";
                strStructure += "<Time>" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "</Time>";
                strStructure += "<Source>" + _execption.Source + "</Source>";
                strStructure += "<StackTrace>" + _execption.StackTrace + "</StackTrace>";
                strStructure += "<Message>" + _execption.Message + "</Message>";
                strStructure += "</Error>";
                strStructure += "</Errors>";
                File.AppendAllText(_strFilepath, strStructure);
                return;
                #endregion
            }
            dsXml.Tables[0].Rows.Add(new object[] { DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), _execption.Source, _execption.StackTrace, _execption.Message });
            dsXml.WriteXml(_strFilepath);

        }
        #endregion
    }
}
