using Net.FreeORM.DataSetConversion.Constant;
using System;

namespace Net.FreeORM.DataSetConversion.Extensions
{
    public static class NumericExtesions
    {
        public static string DecimalToHex(this decimal dcc)
        {
            string s = string.Empty;
            decimal dd = dcc;
            decimal d1;

            do
            {
                d1 = dd % 16;
                s = string.Format("{0}{1}", AppConstants.HexNumCharList[(int)d1], s);
                dd /= 16;
                dd -= (dd % decimal.One);
            } while (dd >= 16);
            s = string.Format("{0}{1}", AppConstants.HexNumCharList[(int)dd], s);

            return s;
        }

        public static object GetValue<T>(this Nullable<T> t) where T : struct
        {
            object val = null;

            if (t.HasValue)
            {
                val = t.Value;
            }

            return val;
        }
    }
}