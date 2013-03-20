using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Drawing.Drawing2D;
using System.Management;
using System.Drawing;
//using System.Windows.Forms;
//using CrystalDecisions.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CSMSys.Lib.AccountingUtility
{
    public class GlobalFunctions
    {
        public static string CypherText = "!!~~~!~~~0)&!!^^~~!!**$$$$$!~@~#";//For Encode and decode
        public GlobalFunctions() { }
        private static int AutoNoDigitLength = 6;
        public static string generateNo(SqlConnection con, string tableName, string ColName, string Prefix)
        {
            int PL = Prefix.Length + 1;

            string NO = string.Empty;
            string qstr = "SELECT ISNULL((SELECT SUBSTRING(MAX(" + ColName + ")," + PL.ToString() + ", 50) + 1 FROM " + tableName + " WHERE CompanyID=@CompanyID AND (" + ColName + " LIKE @PreFix )),1)";
            try
            {
                SqlCommand cmd = new SqlCommand(qstr, con);
                cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                cmd.Parameters.Add("@PreFix", SqlDbType.VarChar, 50).Value = Prefix + "%";
                NO = cmd.ExecuteScalar().ToString();

                NO = Prefix.ToUpper() + NO.PadLeft(AutoNoDigitLength, '0');
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NO;
        }
        public static string generateNo(SqlConnection con,SqlTransaction trans, string tableName, string ColName, string Prefix)
        {
            int PL = Prefix.Length + 1;
           
            string NO = string.Empty;
            string qstr = "SELECT ISNULL((SELECT SUBSTRING(MAX(" + ColName + ")," + PL.ToString() + ", 50) + 1 FROM " + tableName + " WHERE CompanyID=@CompanyID AND (" + ColName + " LIKE @PreFix )),1)";
            try
            {
                SqlCommand cmd = new SqlCommand(qstr, con, trans);
                cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                cmd.Parameters.Add("@PreFix", SqlDbType.VarChar, 50).Value = Prefix+"%";
                NO = cmd.ExecuteScalar().ToString();
                NO = Prefix.ToUpper() + NO.PadLeft(AutoNoDigitLength, '0');
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NO;
        }

        #region Encode Decode
        public static string Encode(string strValue, string Cypher)
        {
            string result = "~";
            for (int i = 0; i < strValue.Length; i++)
            {
                int sValue = i < strValue.Length ? (int)strValue[i] : 0;
                int CypherValue = i < Cypher.Length ? (int)Cypher[i] : 0;
                int a = sValue + CypherValue;
                result = result + (char)a;

            }
            return result;

        }
        public static string Decode(string strValue, string Cypher)
        {
            string result = "";
            for (int i = 1; i < strValue.Length; i++)
            {
                int sValue = (int)strValue[i];
                int CypherValue = i < Cypher.Length + 1 ? (int)Cypher[i - 1] : 0;
                int a = sValue - CypherValue;
                result = result + (char)a;

            }
            return result;

        }

        private static string separator = "x";
        private static string[] Symbol = new string[] { "~", "^", ".", ";", "?", "#", ":", "_" };

        public static string EC(string dSTR)
        {
            string eSTR = "";

            eSTR += E0_9(int.Parse(dSTR[0].ToString()), Symbol[0]);
            eSTR += E0_9(int.Parse(dSTR[1].ToString()), Symbol[1]);
            eSTR += separator;
            eSTR += E0_9(int.Parse(dSTR[3].ToString()), Symbol[2]);
            eSTR += E0_9(int.Parse(dSTR[4].ToString()), Symbol[3]);
            eSTR += separator;
            eSTR += E0_9(int.Parse(dSTR[6].ToString()), Symbol[4]);
            eSTR += E0_9(int.Parse(dSTR[7].ToString()), Symbol[5]);
            eSTR += E0_9(int.Parse(dSTR[8].ToString()), Symbol[6]);
            eSTR += E0_9(int.Parse(dSTR[9].ToString()), Symbol[7]);

            return eSTR;
        }

        public static string DC(string eSTR)
        {

            try
            {
                int d, m, y;

                d = countDigit(eSTR, Symbol[0]) * 10 + countDigit(eSTR, Symbol[1]);
                m = countDigit(eSTR, Symbol[2]) * 10 + countDigit(eSTR, Symbol[3]);

                y = countDigit(eSTR, Symbol[4]) * 1000 + countDigit(eSTR, Symbol[5]) * 100 + countDigit(eSTR, Symbol[6]) * 10 + countDigit(eSTR, Symbol[7]);

                DateTime dt = new DateTime(y, m, d);

                return dt.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                return "NULL";
                //throw ex;
            }
        }

        public static DateTime Todate(string str) ///dd/MM/yyyy
        {
            try
            {
                int d, m, y;
                d = int.Parse(str.Substring(0, 2));
                m = int.Parse(str.Substring(3, 2));
                y = int.Parse(str.Substring(6, 4));

                return new DateTime(y, m, d);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string E0_9(int n, string d)
        {
            string str = "";
            for (int i = 1; i <= n + 1; i++) str += d;

            return str;
        }

        private static int countDigit(string str, string c)
        {
            int i, N, C = 0;
            N = str.Length;
            for (i = 0; i < N; i++)
            {
                if (str[i].ToString() == c) C += 1;
            }

            return C - 1;
        }


        public static string getComments()
        {
            string str = "NULL";
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT  top 1 sysComment FROM SysComment", ConnectionHelper.getConnection());
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0)
                    str = "NULL";
                else
                    str = dt.Rows[0].ItemArray.GetValue(0).ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        #endregion

        #region Binary to Image
        public static byte[] toBinary(Image img, int width, int hieght)
        {
            byte[] b = null;
            try
            {

                MemoryStream ms = new MemoryStream();
                Bitmap bm = new Bitmap(img, width, hieght);
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                b = ms.GetBuffer();
                ms.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }


        public static byte[] toBinary(Image img)
        {

            byte[] b = null;
            try
            {

                MemoryStream ms = new MemoryStream();
                Bitmap bm = new Bitmap(img);

                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                b = ms.GetBuffer();
                ms.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }

        public static object GetNullValue(object obj)
        {
            if (obj == null)
            {
                return DBNull.Value;
            }
            else
            {
                return obj;
            }

        }

        public static object GetNullValue(int nI)
        {
            if (nI == 0)
            {
                return DBNull.Value;
            }
            else
            {
                return (object)nI;
            }

        }


        public static Image toImage(byte[] b)
        {
            Image img = null;
            if (b != null)
            {
                if (b.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(b);
                    img = new Bitmap(ms);
                    ms.Close();
                }
            }

            return img;
        }

        #endregion
        #region IsOverlap2DateRange
        public static bool IsOverlap2DateRange(DateTime Start1, DateTime End1, DateTime Start2, DateTime End2)
        {

            if ((End2.Date < End1.Date && End2.Date < Start1.Date) || (End1.Date < End2.Date && End1.Date < Start2.Date)) return false;//Not Overlap
            else return true;
        }
        #endregion
        public static int isNull(object obj,int value)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString().Trim() == string.Empty) return value;
                else return Convert.ToInt32(obj);
            }
            catch (Exception ex) { throw ex; }
        }
        public static double isNull(object obj, double value)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString().Trim()==string.Empty) return value;
                else return Convert.ToDouble(obj);
            }
            catch (Exception ex) { throw ex; }
        }
        public static DateTime  isNull(object obj, DateTime value)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString().Trim() == string.Empty) return value;
                else return Convert.ToDateTime(obj);
            }
            catch (Exception ex) { throw ex; }
        }
        public static string isNull(object obj, string value)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString().Trim() == string.Empty) return value;
                else return Convert.ToString(obj);
            }
            catch (Exception ex) { throw ex; }
        }

    }

    public class LogInInfo
    {
        public LogInInfo() { }


        #region Fields
        private static int _nUserID;
        private static int _nCompanyID;
        private static DateTime _dModifiedDate;


        #endregion

        #region Properties
        public static int UserID
        {
            get { return _nUserID; }
            set { _nUserID = value; }
        }

        public static int CompanyID
        {
            get { return _nCompanyID; }
            set { _nCompanyID = value; }
        }

        public static DateTime ModifiedDate
        {
            get { return _dModifiedDate; }
            set { _dModifiedDate = value; }
        }
        #endregion
    }


    public class FormColorClass
    {
        public FormColorClass() { }


        #region Fields
        public static Color VertexDefaultButtonColor = SystemColors.Control;//SystemColors.Control;
        public static Color VertexDefaultGradiantColor1 = Color.SteelBlue;//SystemColors.Control;
        public static Color VertexDefaultGradiantColor2 = Color.White;//SystemColors.Control;
        public static Single VertexDefaultAngle = 270f;//0f;
        private static Color _cButtonColor = VertexDefaultButtonColor;
        private static Color _cGradiantColor1 = VertexDefaultGradiantColor1;
        private static Color _cGradiantColor2 = VertexDefaultGradiantColor2;
        private static Single _sAngle = VertexDefaultAngle;
        #endregion

        #region Properties
        public static Color ButtonColor
        {
            get { return _cButtonColor; }
            set { _cButtonColor = value; }
        }
        public static Color GradiantColor1
        {
            get { return _cGradiantColor1; }
            set { _cGradiantColor1 = value; }
        }
        public static Color GradiantColor2
        {
            get { return _cGradiantColor2; }
            set { _cGradiantColor2 = value; }
        }
        public static Single Angle
        {
            get { return _sAngle; }
            set { _sAngle = value; }
        }
        //public static void ColorControl(Form f)
        //{


        //    for (int i = 0; i < f.Controls.Count; i++)
        //    {
        //        if (f.Controls[i].GetType() == typeof(Button)) { f.Controls[i].BackColor = FormColorClass.ButtonColor; continue; }
        //        else if (f.Controls[i].GetType() == typeof(TextBox) || f.Controls[i].GetType() == typeof(ComboBox) || f.Controls[i].GetType() == typeof(ListView) || f.Controls[i].GetType() == typeof(DateTimePicker) || f.Controls[i].GetType() == typeof(DataGridView) || f.Controls[i].GetType() == typeof(ProgressBar) || f.Controls[i].GetType() == typeof(CrystalReportViewer) || f.Controls[i].GetType().FullName == "Accounting.Controls.ctlDaraGridView") { continue; }
        //        else
        //        {
        //            f.Controls[i].BackColor = Color.Transparent;
        //            for (int j = 0; j < f.Controls[i].Controls.Count; j++)
        //            {
        //                if (f.Controls[i].Controls[j].GetType() == typeof(Button)) { f.Controls[i].Controls[j].BackColor = FormColorClass.ButtonColor; continue; }
        //            }
        //        }

        //    }
        //}
        //public static void ColorForm(Form f, PaintEventArgs e)
        //{

        //    Graphics gr = e.Graphics;
        //    Brush br;
        //    Rectangle rect = new Rectangle(0, 0, f.Width, f.Height);
        //    br = new LinearGradientBrush(rect, FormColorClass.GradiantColor1, FormColorClass.GradiantColor2, FormColorClass.Angle);
        //    gr.FillRegion(br, new Region(rect));
        //}

        //public static void ColorTabControl(TabPage f, PaintEventArgs e)
        //{


        //    Graphics gr = e.Graphics;
        //    Brush br;
        //    Rectangle rect = new Rectangle(0, 0, f.Width, f.Height);
        //    br = new LinearGradientBrush(rect, FormColorClass.GradiantColor1, FormColorClass.GradiantColor2, FormColorClass.Angle);
        //    gr.FillRegion(br, new Region(rect));
            
        //}
     
        #endregion
    }
    public class ResistationClass
    {
        public ResistationClass() { }


        public static string GenerateAuthorizationCode(string param)
        {
            string ans = "";

            string MBID = Encode(param.Split('-')[0]);
            string PID = Encode(param.Split('-')[1]);
            for (int i = 0; i < 12; i++)
            {
                if (i < MBID.Length) ans = ans + MBID[i];
                else ans = ans + "5";

                if (i < PID.Length) ans = ans + PID[i];
                else ans = ans + "6";
                if (i % 2 == 1 && i != 11) ans = ans + "-";

            }
            return ans;


        }
        public static string Encode(string s)
        {
            string ans = "";
            for (int i = 0; i < s.Length; i++)
            {
                char b;
                if (s[i] == 'z') b = 'a';
                else if (s[i] == 'Z') b = 'A';
                else if (s[i] == '9') b = '0';
                else b = (char)((int)s[i] + 1);
                ans = ans + b;
            }
            return ans;
        }
        //public static string GenerateProcessorID()
        //{
        //    ManagementObjectCollection mbsList = null;
        //    ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
        //    mbsList = mbs.Get();
        //    foreach (ManagementObject mo in mbsList)
        //    {
        //        return mo["ProcessorID"].ToString();
        //    }
        //    return "";

        //}
        //public static string GenerateMotherBoardSirialNo()
        //{

        //    ManagementObjectCollection mbsList = null;
        //    ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
        //    mbsList = mbs.Get();
        //    foreach (ManagementObject mo in mbsList)
        //    {
        //        return mo["SerialNumber"].ToString();
        //    }
        //    return "";

        //}

    }
}
 