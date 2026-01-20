using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public static class CurrencyToWord
{
    public static String toCurrency(this double number)
    {
        return ((decimal)number).toCurrency("", "Sen", "only", 2);
    }

    public static String toCurrency(this decimal number, String major, String minor, String endStr, int minorLength)
    {

        //if (String.IsNullOrWhiteSpace(major)) throw new Exception("Must specify major currency");
        if (String.IsNullOrWhiteSpace(minor)) throw new Exception("Must specify minor currency");

        String numb = number.ToString();
        String val = "", wholeNo = numb, points = "", andStr = major, pointStr = "";
        int decimalPlace = numb.IndexOf(".");
        if (decimalPlace > 0)
        {
            wholeNo = numb.Substring(0, decimalPlace);
            points = numb.Substring(decimalPlace + 1);
            if (points.Length > minorLength) throw new Exception("Incorrect format");
            if (Convert.ToInt32(points) > 0)
            {
                andStr = major + " and " + minor + " ";
                endStr = andStr;
                //pointStr = Int32.Parse(points.Length == 1 ? points + "0" : points).toWords();
                pointStr = Int64.Parse(points.Length == 1 ? points + "0" : points).toWords();
            }
        }
        if (String.IsNullOrWhiteSpace(pointStr))
        {
            if (endStr == minor + " " || endStr == null)
            {
                //val = String.Format("{0} {1}", Int32.Parse(wholeNo).toWords().Trim(), andStr.Trim());
                val = String.Format("{0} {1}", Int64.Parse(wholeNo).toWords().Trim(), andStr.Trim());
            }
            else
            {
                //val = String.Format("{0} {1} {2}", Int32.Parse(wholeNo).toWords().Trim(), andStr.Trim(), endStr.Trim());
                val = String.Format("{0} {1} {2}", Int64.Parse(wholeNo).toWords().Trim(), andStr.Trim(), endStr.Trim());
            }
        }
        else
        {
            //val = String.Format("{0} {1} {2} {3}", Int32.Parse(wholeNo).toWords().Trim(), andStr.Trim(), pointStr.Trim(), endStr.Trim());
            val = String.Format("{0} {1} {2} {3}", Int64.Parse(wholeNo).toWords().Trim(), andStr.Trim(), pointStr.Trim(), endStr.Trim());
        }
        return val;
    }

    public static string toWords(this Int64 number)
    {
        if (number == 0)
            return "Zero";

        if (number < 0)
            return "minus " + Math.Abs(number).toWords();

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += (number / 1000000).toWords().TrimEnd() + " Million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += (number / 1000).toWords().TrimEnd() + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += (number / 100).toWords().TrimEnd() + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            //if (words != "")
            //    words += "and ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words += unitsMap[Convert.ToInt32(number)];
            else
            {
                words += tensMap[Convert.ToInt32(number) / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[Convert.ToInt32(number) % 10];
                    //words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
}

