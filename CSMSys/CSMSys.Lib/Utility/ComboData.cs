using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.Utility
{
    public class ComboData
    {
        IList<ComboItem> _GenderList;
        IList<ComboItem> _ReportList;
        IList<ComboItem> _ReligionList;
        IList<ComboItem> _BloodGroup;
        IList<ComboItem> _EmployeeCategory;
        IList<ComboItem> _EmployeeType;
        IList<ComboItem> _PartyType;
        IList<ComboItem> _CurrencyCode;
        IList<ComboItem> _MethodOfPayment;

        public ComboData()
        {
            _GenderList = new List<ComboItem>();
            _ReportList = new List<ComboItem>();
            _ReligionList = new List<ComboItem>();
            _BloodGroup = new List<ComboItem>();
            _EmployeeCategory = new List<ComboItem>();
            _EmployeeType = new List<ComboItem>();
            _PartyType = new List<ComboItem>();
            _CurrencyCode = new List<ComboItem>();
            _MethodOfPayment = new List<ComboItem>();
        }

        //############### Employee ##################
        public IList<ComboItem> GenderList()
        {
            _GenderList.Clear();
            _GenderList.Add(new ComboItem() { Description = "", Value = " " });
            _GenderList.Add(new ComboItem() { Description = "Male", Value = "Male" });
            _GenderList.Add(new ComboItem() { Description = "Female", Value = "Female" });
            _GenderList.Add(new ComboItem() { Description = "Other", Value = "Other" });

            return _GenderList;
        }

        public IList<ComboItem> ReportList()
        {
            _ReportList.Clear();
          //  _ReportList.Add(new ComboItem() { Description = "", Value = " " });
            _ReportList.Add(new ComboItem() { Description = "Agreement Report", Value = "Agreement" });
            _ReportList.Add(new ComboItem() { Description = "Relocation Report", Value = "Relocation" });
            _ReportList.Add(new ComboItem() { Description = "Collection Report", Value = "Collection" });
            _ReportList.Add(new ComboItem() { Description = "Loan Disbursement Report", Value = "Disbursement" });
            _ReportList.Add(new ComboItem() { Description = "Delivery Report", Value = "Delivery" });

            return _ReportList;
        }

        public IList<ComboItem> ReligionList()
        {
            _ReligionList.Clear();
            _ReligionList.Add(new ComboItem() { Description = "", Value = " " });
            _ReligionList.Add(new ComboItem() { Description = "Islam", Value = "Islam" });
            _ReligionList.Add(new ComboItem() { Description = "Hindu", Value = "Hindu" });
            _ReligionList.Add(new ComboItem() { Description = "Cristian", Value = "Cristian" });
            _ReligionList.Add(new ComboItem() { Description = "Buddhist", Value = "Buddhist" });
            _ReligionList.Add(new ComboItem() { Description = "Other", Value = "Other" });

            return _ReligionList;
        }

        public IList<ComboItem> BloodGroup()
        {
            _BloodGroup.Clear();
            _BloodGroup.Add(new ComboItem() { Description = "", Value = " " });
            _BloodGroup.Add(new ComboItem() { Description = "A+", Value = "A+" });
            _BloodGroup.Add(new ComboItem() { Description = "A-", Value = "A-" });
            _BloodGroup.Add(new ComboItem() { Description = "B+", Value = "B+" });
            _BloodGroup.Add(new ComboItem() { Description = "B-", Value = "B-" });
            _BloodGroup.Add(new ComboItem() { Description = "AB+", Value = "AB+" });
            _BloodGroup.Add(new ComboItem() { Description = "AB-", Value = "AB-" });
            _BloodGroup.Add(new ComboItem() { Description = "O+", Value = "O+" });
            _BloodGroup.Add(new ComboItem() { Description = "O-", Value = "O-" });

            return _BloodGroup;
        }

        public IList<ComboItem> EmployeeCategory()
        {
            _EmployeeCategory.Clear();
            _EmployeeCategory.Add(new ComboItem() { Description = "", Value = " " });
            _EmployeeCategory.Add(new ComboItem() { Description = "Part Time", Value = "Part Time" });
            _EmployeeCategory.Add(new ComboItem() { Description = "Full Time", Value = "Full Time" });
            _EmployeeCategory.Add(new ComboItem() { Description = "Other", Value = "Other" });

            return _EmployeeCategory;
        }

        public IList<ComboItem> EmployeeType()
        {
            _EmployeeType.Clear();
            _EmployeeType.Add(new ComboItem() { Description = "", Value = " " });
            _EmployeeType.Add(new ComboItem() { Description = "Teacher", Value = "Teacher" });
            _EmployeeType.Add(new ComboItem() { Description = "Staff", Value = "Staff" });
            _EmployeeType.Add(new ComboItem() { Description = "Other", Value = "Other" });

            return _EmployeeType;
        }

        //############## Party ################
        public IList<ComboItem> PartyType()
        {
            _PartyType.Clear();
            _PartyType.Add(new ComboItem() { Description = "", Value = " " });
            _PartyType.Add(new ComboItem() { Description = "Company (কোম্পানী)", Value = "Company (কোম্পানী)" });
            _PartyType.Add(new ComboItem() { Description = "Party (পার্টি)", Value = "Party (পার্টি)" });
            _PartyType.Add(new ComboItem() { Description = "Agent (এজেন্ট)", Value = "Agent (এজেন্ট)" });
            _PartyType.Add(new ComboItem() { Description = "Farmer (কৃষক)", Value = "Farmer (কৃষক)" });

            return _PartyType;
        }

        public IList<ComboItem> CurrencyCodeList()
        {
            _CurrencyCode.Clear();

            _CurrencyCode.Add(new ComboItem() { Description = "", Value = " " });
            _CurrencyCode.Add(new ComboItem() { Description = "United Arab Emirates dirham", Value = "AED" });
            _CurrencyCode.Add(new ComboItem() { Description = "Afghani", Value = "AFN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lek", Value = "ALL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Armenian dram", Value = "AMD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Netherlands Antillean guilder", Value = "ANG" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kwanza", Value = "AOA" });
            _CurrencyCode.Add(new ComboItem() { Description = "Argentine peso", Value = "ARS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Australian dollar", Value = "AUD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Aruban guilder", Value = "AWG" });
            _CurrencyCode.Add(new ComboItem() { Description = "Azerbaijanian manat", Value = "AZN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Convertible marks", Value = "BAM" });
            _CurrencyCode.Add(new ComboItem() { Description = "Barbados dollar", Value = "BBD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Bangladeshi taka", Value = "BDT" });
            _CurrencyCode.Add(new ComboItem() { Description = "Bulgarian lev", Value = "BGN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Bahraini dinar", Value = "BHD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Burundian franc", Value = "BIF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Bermudian dollar (customarily known as Bermuda dollar)", Value = "BMD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Brunei dollar", Value = "BND" });
            _CurrencyCode.Add(new ComboItem() { Description = "Boliviano", Value = "BOB" });
            _CurrencyCode.Add(new ComboItem() { Description = "Bolivian Mvdol (funds code)", Value = "BOV" });
            _CurrencyCode.Add(new ComboItem() { Description = "Brazilian real", Value = "BRL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Bahamian dollar", Value = "BSD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Ngultrum", Value = "BTN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Pula", Value = "BWP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Belarusian ruble", Value = "BYR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Belize dollar", Value = "BZD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Canadian dollar", Value = "CAD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Franc Congolais", Value = "CDF" });
            _CurrencyCode.Add(new ComboItem() { Description = "WIR euro (complementary currency)", Value = "CHE" });
            _CurrencyCode.Add(new ComboItem() { Description = "Swiss franc", Value = "CHF" });
            _CurrencyCode.Add(new ComboItem() { Description = "WIR franc (complementary currency)", Value = "CHW" });
            _CurrencyCode.Add(new ComboItem() { Description = "Unidad de Fomento (funds code)", Value = "CLF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Chilean peso", Value = "CLP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Chinese Yuan", Value = "CNY" });
            _CurrencyCode.Add(new ComboItem() { Description = "Colombian peso", Value = "COP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Unidad de Valor Real", Value = "COU" });
            _CurrencyCode.Add(new ComboItem() { Description = "Costa Rican colon", Value = "CRC" });
            _CurrencyCode.Add(new ComboItem() { Description = "Cuban convertible peso", Value = "CUC" });
            _CurrencyCode.Add(new ComboItem() { Description = "Cuban peso", Value = "CUP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Cape Verde escudo", Value = "CVE" });
            _CurrencyCode.Add(new ComboItem() { Description = "Czech Koruna", Value = "CZK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Djibouti franc", Value = "DJF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Danish krone", Value = "DKK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Dominican peso", Value = "DOP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Algerian dinar", Value = "DZD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kroon", Value = "EEK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Egyptian pound", Value = "EGP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Nakfa", Value = "ERN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Ethiopian birr", Value = "ETB" });
            _CurrencyCode.Add(new ComboItem() { Description = "euro", Value = "EUR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Fiji dollar", Value = "FJD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Falkland Islands pound", Value = "FKP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Pound sterling", Value = "GBP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lari", Value = "GEL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Cedi", Value = "GHS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Gibraltar pound", Value = "GIP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Dalasi", Value = "GMD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Guinea franc", Value = "GNF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Quetzal", Value = "GTQ" });
            _CurrencyCode.Add(new ComboItem() { Description = "Guyana dollar", Value = "GYD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Hong Kong dollar", Value = "HKD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lempira", Value = "HNL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Croatian kuna", Value = "HRK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Haiti gourde", Value = "HTG" });
            _CurrencyCode.Add(new ComboItem() { Description = "Forint", Value = "HUF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Rupiah", Value = "IDR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Israeli new sheqel", Value = "ILS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Indian rupee", Value = "INR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Iraqi dinar", Value = "IQD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Iranian rial", Value = "IRR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Iceland krona", Value = "ISK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Jamaican dollar", Value = "JMD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Jordanian dinar", Value = "JOD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Japanese yen", Value = "JPY" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kenyan shilling", Value = "KES" });
            _CurrencyCode.Add(new ComboItem() { Description = "Som", Value = "KGS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Riel", Value = "KHR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Comoro franc", Value = "KMF" });
            _CurrencyCode.Add(new ComboItem() { Description = "North Korean won", Value = "KPW" });
            _CurrencyCode.Add(new ComboItem() { Description = "South Korean won", Value = "KRW" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kuwaiti dinar", Value = "KWD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Cayman Islands dollar", Value = "KYD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Tenge", Value = "KZT" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kip", Value = "LAK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lebanese pound", Value = "LBP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Sri Lanka rupee", Value = "LKR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Liberian dollar", Value = "LRD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lesotho loti", Value = "LSL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lithuanian litas", Value = "LTL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Latvian lats", Value = "LVL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Libyan dinar", Value = "LYD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Moroccan dirham", Value = "MAD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Moldovan leu", Value = "MDL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Malagasy ariary", Value = "MGA" });
            _CurrencyCode.Add(new ComboItem() { Description = "Denar", Value = "MKD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kyat", Value = "MMK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Tugrik", Value = "MNT" });
            _CurrencyCode.Add(new ComboItem() { Description = "Pataca", Value = "MOP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Ouguiya", Value = "MRO" });
            _CurrencyCode.Add(new ComboItem() { Description = "Mauritius rupee", Value = "MUR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Rufiyaa", Value = "MVR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kwacha", Value = "MWK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Mexican peso", Value = "MXN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Mexican Unidad de Inversion (UDI) (funds code)", Value = "MXV" });
            _CurrencyCode.Add(new ComboItem() { Description = "Malaysian ringgit", Value = "MYR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Metical", Value = "MZN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Namibian dollar", Value = "NAD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Naira", Value = "NGN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Cordoba oro", Value = "NIO" });
            _CurrencyCode.Add(new ComboItem() { Description = "Norwegian krone", Value = "NOK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Nepalese rupee", Value = "NPR" });
            _CurrencyCode.Add(new ComboItem() { Description = "New Zealand dollar", Value = "NZD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Rial Omani", Value = "OMR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Balboa", Value = "PAB" });
            _CurrencyCode.Add(new ComboItem() { Description = "Nuevo sol", Value = "PEN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kina", Value = "PGK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Philippine peso", Value = "PHP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Pakistan rupee", Value = "PKR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Zloty", Value = "PLN" });
            _CurrencyCode.Add(new ComboItem() { Description = "Guarani", Value = "PYG" });
            _CurrencyCode.Add(new ComboItem() { Description = "Qatari rial", Value = "QAR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Romanian new leu", Value = "RON" });
            _CurrencyCode.Add(new ComboItem() { Description = "Serbian dinar", Value = "RSD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Russian rouble", Value = "RUB" });
            _CurrencyCode.Add(new ComboItem() { Description = "Rwanda franc", Value = "RWF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Saudi riyal", Value = "SAR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Solomon Islands dollar", Value = "SBD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Seychelles rupee", Value = "SCR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Sudanese pound", Value = "SDG" });
            _CurrencyCode.Add(new ComboItem() { Description = "Swedish krona/kronor", Value = "SEK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Singapore dollar", Value = "SGD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Saint Helena pound", Value = "SHP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Leone", Value = "SLL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Somali shilling", Value = "SOS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Surinam dollar", Value = "SRD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Dobra", Value = "STD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Syrian pound", Value = "SYP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Lilangeni", Value = "SZL" });
            _CurrencyCode.Add(new ComboItem() { Description = "Baht", Value = "THB" });
            _CurrencyCode.Add(new ComboItem() { Description = "Somoni", Value = "TJS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Manat", Value = "TMT" });
            _CurrencyCode.Add(new ComboItem() { Description = "Tunisian dinar", Value = "TND" });
            _CurrencyCode.Add(new ComboItem() { Description = "Pa'anga", Value = "TOP" });
            _CurrencyCode.Add(new ComboItem() { Description = "Turkish lira", Value = "TRY" });
            _CurrencyCode.Add(new ComboItem() { Description = "Trinidad and Tobago dollar", Value = "TTD" });
            _CurrencyCode.Add(new ComboItem() { Description = "New Taiwan dollar", Value = "TWD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Tanzanian shilling", Value = "TZS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Hryvnia", Value = "UAH" });
            _CurrencyCode.Add(new ComboItem() { Description = "Uganda shilling", Value = "UGX" });
            _CurrencyCode.Add(new ComboItem() { Description = "US dollar", Value = "USD" });
            _CurrencyCode.Add(new ComboItem() { Description = "United States dollar (next day) (funds code)", Value = "USN" });
            _CurrencyCode.Add(new ComboItem() { Description = "United States dollar (same day) (funds code)", Value = "USS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Peso Uruguayo", Value = "UYU" });
            _CurrencyCode.Add(new ComboItem() { Description = "Uzbekistan som", Value = "UZS" });
            _CurrencyCode.Add(new ComboItem() { Description = "Venezuelan bolívar fuerte", Value = "VEF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Vietnamese d?ng", Value = "VND" });
            _CurrencyCode.Add(new ComboItem() { Description = "Vatu", Value = "VUV" });
            _CurrencyCode.Add(new ComboItem() { Description = "Samoan tala", Value = "WST" });
            _CurrencyCode.Add(new ComboItem() { Description = "CFA franc BEAC", Value = "XAF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Silver (one troy ounce)", Value = "XAG" });
            _CurrencyCode.Add(new ComboItem() { Description = "Gold (one troy ounce)", Value = "XAU" });
            _CurrencyCode.Add(new ComboItem() { Description = "European Composite Unit (EURCO) (bond market unit)", Value = "XBA" });
            _CurrencyCode.Add(new ComboItem() { Description = "European Monetary Unit (E.M.U.-6) (bond market unit)", Value = "XBB" });
            _CurrencyCode.Add(new ComboItem() { Description = "European Unit of Account 9 (E.U.A.-9) (bond market unit)", Value = "XBC" });
            _CurrencyCode.Add(new ComboItem() { Description = "European Unit of Account 17 (E.U.A.-17) (bond market unit)", Value = "XBD" });
            _CurrencyCode.Add(new ComboItem() { Description = "East Caribbean dollar", Value = "XCD" });
            _CurrencyCode.Add(new ComboItem() { Description = "Special Drawing Rights", Value = "XDR" });
            _CurrencyCode.Add(new ComboItem() { Description = "UIC franc (special settlement currency)", Value = "XFU" });
            _CurrencyCode.Add(new ComboItem() { Description = "CFA Franc BCEAO", Value = "XOF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Palladium (one troy ounce)", Value = "XPD" });
            _CurrencyCode.Add(new ComboItem() { Description = "CFP franc", Value = "XPF" });
            _CurrencyCode.Add(new ComboItem() { Description = "Platinum (one troy ounce)", Value = "XPT" });
            _CurrencyCode.Add(new ComboItem() { Description = "Code reserved for testing purposes", Value = "XTS" });
            _CurrencyCode.Add(new ComboItem() { Description = "No currency", Value = "XXX" });
            _CurrencyCode.Add(new ComboItem() { Description = "Yemeni rial", Value = "YER" });
            _CurrencyCode.Add(new ComboItem() { Description = "South African rand", Value = "ZAR" });
            _CurrencyCode.Add(new ComboItem() { Description = "Kwacha", Value = "ZMK" });
            _CurrencyCode.Add(new ComboItem() { Description = "Zimbabwe dollar", Value = "ZWL" });


            return _CurrencyCode;
        }

        public IList<ComboItem> MethodOfPaymentList()
        {
            _MethodOfPayment.Clear();
            _MethodOfPayment.Add(new ComboItem() { Description = "", Value = " " });
            _MethodOfPayment.Add(new ComboItem() { Description = "Current Account Debit", Value = "CAD" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Promissory Note", Value = "BOE" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Bank draft; Automated bank draft", Value = "BAR" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Direct transfer; postal payment slip", Value = "DIR" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Authorization to Direct Current Account Debit", Value = "ADD" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Credit card payment", Value = "CCR" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Cheque", Value = "CHQ" });
            _MethodOfPayment.Add(new ComboItem() { Description = "Other", Value = "OTH" });

            return _MethodOfPayment;
        }
    }
}
