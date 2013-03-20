using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.Utility
{
    public class NumberToBangla
    {
        public String changeNumericToWords(double numb)
        {
            String num = numb.ToString();
            return changeToWords(num, false);
        }

        public String changeCurrencyToWords(String numb)
        {
            return changeToWords(numb, true);
        }

        public String changeNumericToWords(String numb)
        {
            return changeToWords(numb, false);
        }

        public String changeCurrencyToWords(double numb)
        {
            return changeToWords(numb.ToString(), true);
        }

        private String changeToWords(String numb, bool isCurrency)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = (isCurrency) ? ("মাত্র") : ("");
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = (isCurrency) ? ("এবং") : ("দশমিক");// just to separate whole numbers from points/cents
                        endStr = (isCurrency) ? ("পয়সা " + endStr) : ("");
                        pointStr = translateCents(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            catch { ;}
            return val;
        }

        private String translateWholeNumber(String number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX
                bool isDone = false;//test if already translated
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric
                    beginsZero = number.StartsWith("0");
                    int numDigits = number.Length;
                    int pos = 0;//store digit grouping
                    String place = "";//digit grouping name:hundres,thousand,etc...
                    switch (numDigits)
                    {
                        case 1://ones' range
                            word = ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range
                            word = tens(number);
                            isDone = true;
                            break;
                        case 3://hundreds' range
                            pos = (numDigits % 3) + 1;
                            place = " শত ";
                            break;
                        case 4://thousands' range
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " হাজার ";
                            break;
                        case 7://millions' range
                        case 8:
                            pos = (numDigits % 6) + 1;
                            place = " লক্ষ ";
                            break;
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " মিলিয়ন ";
                            break;
                        case 10://Billions's range
                            pos = (numDigits % 10) + 1;
                            place = " বিলিয়ন ";
                            break;
                        //add extra case options for anything above Billion...
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)
                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                        //check for trailing zeros
                        if (beginsZero) word = " এবং " + word.Trim();
                    }
                    //ignore digit grouping names
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { ;}
            return word.Trim();
        }

        private String tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = "দশ";
                    break;
                case 11:
                    name = "এগারো";
                    break;
                case 12:
                    name = "বারো";
                    break;
                case 13:
                    name = "তেরো";
                    break;
                case 14:
                    name = "চৌদ্দ";
                    break;
                case 15:
                    name = "পনর";
                    break;
                case 16:
                    name = "ষোল";
                    break;
                case 17:
                    name = "সতের";
                    break;
                case 18:
                    name = "আঠারো";
                    break;
                case 19:
                    name = "উনিশ";
                    break;
                case 20:
                    name = "বিশ";
                    break;
                case 30:
                    name = "ত্রিশ";
                    break;
                case 40:
                    name = "চল্লিশ";
                    break;
                case 50:
                    name = "পঞ্চাশ";
                    break;
                case 60:
                    name = "ষাট";
                    break;
                case 70:
                    name = "সত্তর";
                    break;
                case 80:
                    name = "আশি";
                    break;
                case 90:
                    name = "নব্বই";
                    break;
                default:
                    if (digt > 0)
                    {
                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private String ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    name = "এক";
                    break;
                case 2:
                    name = "দুই";
                    break;
                case 3:
                    name = "তিন";
                    break;
                case 4:
                    name = "চার";
                    break;
                case 5:
                    name = "পাচ";
                    break;
                case 6:
                    name = "ছয়";
                    break;
                case 7:
                    name = "সাত";
                    break;
                case 8:
                    name = "আট";
                    break;
                case 9:
                    name = "নয়";
                    break;
            }
            return name;
        }

        private String translateCents(String cents)
        {
            String cts = "", digit = "", engOne = "";
            for (int i = 0; i < cents.Length; i++)
            {
                digit = cents[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cts += " " + engOne;
            }
            return cts;
        }
    }
}
