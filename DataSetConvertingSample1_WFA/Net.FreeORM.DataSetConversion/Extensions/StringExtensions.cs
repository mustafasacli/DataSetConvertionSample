using Net.FreeORM.DataSetConversion.Constant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Net.FreeORM.DataSetConversion.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertTurkishCharactersInWord(this string theWord)
        {
            try
            {
                theWord = theWord.Replace("ö", "o");
                theWord = theWord.Replace("ü", "u");
                theWord = theWord.Replace("ı", "i");
                theWord = theWord.Replace("ş", "s");
                theWord = theWord.Replace("ğ", "g");
                theWord = theWord.Replace("ç", "c");

                theWord = theWord.Replace("Ö", "O");
                theWord = theWord.Replace("Ü", "U");
                theWord = theWord.Replace("İ", "I");
                theWord = theWord.Replace("Ş", "S");
                theWord = theWord.Replace("Ğ", "G");
                theWord = theWord.Replace("Ç", "C");

                return theWord;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Str2Int(this string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        public static bool IsNullOrSpace(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Trim().Length == 0;
            }
        }

        public static string Reverse(this string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return Str;

            try
            {
                int len = Str.Length;
                switch (len)
                {
                    case 0:
                    case 1:
                        return Str;

                    case 2:
                        return string.Format("{0}{1}", Str[1], Str[0]);

                    default:
                        return string.Format("{0}{1}{2}", Str[Str.Length - 1], Reverse(Str.Substring(1, Str.Length - 2)), Str[0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool isNumber(this string willBeExamined)
        {
            try
            {
                if (willBeExamined.Length < 1)
                    return false;

                bool willBeReturned = true;
                foreach (char ch in willBeExamined.ToCharArray())
                {
                    willBeReturned = char.IsNumber(ch);
                    if (willBeReturned == false)
                        break;
                }
                return willBeReturned;
            }
            catch (Exception)
            {
                throw;
            }
        } // end isNumber

        public static decimal HexToDecimal(this string strHex)
        {
            if (string.IsNullOrWhiteSpace(strHex))
            {
                throw new ArgumentException("Input string can not be null, empty or white-space string.");
            }

            char[] hexx = AppConstants.HexNumCharList.ToCharArray();

            char[] ToHexArr = strHex.Reverse().ToArray(); //strHex.ToCharArray();
            int count = ToHexArr.AsQueryable().Where(c => hexx.Contains(c) == false).Count();

            if (count > 0)
            {
                throw new ArgumentException("Illegal characters are given. Characters: \"0123456789ABCDEF\".");
            }

            List<char> lstChars = hexx.ToList();
            decimal dd = decimal.Zero;
            int indx;
            for (int counter = 0; counter < ToHexArr.Length; counter++)
            {
                indx = lstChars.IndexOf(ToHexArr[ToHexArr.Length - counter - 1]);
                if (indx < 0)
                {
                    throw new ArgumentException("Illegal characters are given. Characters: \"0123456789ABCDEF\".");
                }

                dd += (decimal)(indx * Math.Pow(16, ToHexArr.Length - counter - 1));
            }

            return dd;
        }
    }
}