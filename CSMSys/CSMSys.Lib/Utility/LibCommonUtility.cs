using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CSMSys.Lib.Utility
{
    public class LibCommonUtility
    {
        public static string GetShortDateString_EN(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
            //return dateTime.ToShortDateString();
        }

        public static string GetShortDateString_BN(DateTime dateTime)
        {
            return ConvertDateToBengali(dateTime);
        }

        private static string ConvertDateToBengali(DateTime dateTime)
        {
            string separator = "-";
            string result = dateTime.ToString("dd-MM-yyyy");
            string[] valueList = result.Split(new char[] { '-', '/' });

            int counter = 1;

            result = string.Empty;

            foreach (String value in valueList)
            {
                //var intList = value.Select(digit => digit.ToString()); 

                char[] val = value.ToCharArray();

                foreach (char v in val)
                {
                    if (GetBengaliNumberDictionary().ContainsKey(v.ToString()))
                    {
                        result += GetBengaliNumberDictionary()[v.ToString()];
                    }
                }

                if (counter < valueList.Length)
                {
                    result += separator;
                }

                counter++;
            }

            return result;
        }
        
        private static Dictionary<string, string> GetBengaliNumberDictionary()
        {
            Dictionary<string, string> numDictionary = new Dictionary<string, string>();

            numDictionary.Add("1", "১");
            numDictionary.Add("2", "২");
            numDictionary.Add("3", "৩");
            numDictionary.Add("4", "৪");
            numDictionary.Add("5", "৫");
            numDictionary.Add("6", "৬");
            numDictionary.Add("7", "৭");
            numDictionary.Add("8", "৮");
            numDictionary.Add("9", "৯");
            numDictionary.Add("0", "০");

            return numDictionary;
        }

        public static IList<Object> GetBloodGroupList()
        {
            return new List<object>
            {
                new {
                    ID="A+",
                    DropDownDisplayField = "A+",
                    DropDownDisplayField_BN = "A+"
                },

                new {
                    ID="A-",
                    DropDownDisplayField = "A-",
                    DropDownDisplayField_BN = "A-"
                },

                new {
                    ID="B+",
                    DropDownDisplayField = "B+",
                    DropDownDisplayField_BN = "B+"
                },

                new {
                    ID="B-",
                    DropDownDisplayField = "B-",
                    DropDownDisplayField_BN = "B-"
                },

                new {
                    ID="AB+",
                    DropDownDisplayField = "AB+",
                    DropDownDisplayField_BN = "AB+"
                },

                new {
                    ID="AB-",
                    DropDownDisplayField = "AB-",
                    DropDownDisplayField_BN = "AB-"
                },

                new {
                    ID="O+",
                    DropDownDisplayField = "O+",
                    DropDownDisplayField_BN = "O+"
                },

                new {
                    ID="O-",
                    DropDownDisplayField = "O-",
                    DropDownDisplayField_BN = "O-S"
                },

            };
        }

        public static IList<Object> GetMonthList()
        {
            return new List<object>
            {
                new {
                    ID="1",
                    DropDownDisplayField = "Jan",
                    DropDownDisplayField_BN = "Jan"
                },

                new {
                    ID="2",
                    DropDownDisplayField = "Feb",
                    DropDownDisplayField_BN = "Feb"
                },

                new {
                    ID="3",
                    DropDownDisplayField = "Mar",
                    DropDownDisplayField_BN = "Mar"
                },

                new {
                    ID="4",
                    DropDownDisplayField = "Apr",
                    DropDownDisplayField_BN = "Apr"
                },

                new {
                    ID="5",
                    DropDownDisplayField = "May",
                    DropDownDisplayField_BN = "May"
                },

                new {
                    ID="6",
                    DropDownDisplayField = "June",
                    DropDownDisplayField_BN = "June"
                },

                new {
                    ID="7",
                    DropDownDisplayField = "July",
                    DropDownDisplayField_BN = "July"
                },

                new {
                    ID="8",
                    DropDownDisplayField = "Aug",
                    DropDownDisplayField_BN = "Aug"
                },

                new {
                    ID="9",
                    DropDownDisplayField = "Sept",
                    DropDownDisplayField_BN = "Sept"
                },

                new {
                    ID="10",
                    DropDownDisplayField = "Oct",
                    DropDownDisplayField_BN = "Oct"
                },

                new {
                    ID="11",
                    DropDownDisplayField = "Nov",
                    DropDownDisplayField_BN = "Nov"
                },

                new {
                    ID="12",
                    DropDownDisplayField = "Dec",
                    DropDownDisplayField_BN = "Dec"
                },

            };
        }

    }
}
