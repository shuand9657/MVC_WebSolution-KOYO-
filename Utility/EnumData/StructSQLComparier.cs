using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityModelClass.EnumData
{
    public class StructSQLComparier
    {
        public static string LessEqualThan = "=<";
        public static string GreaterEqualThan = ">=";
        public static string LessThan = "<";
        public static string GreaterThan = ">";
        public static string EqualTo = "==";
        public static string NotEqualTo = "!=";
        public static string StrEquals = "Equals";
        public static string StrStartsWith = "StartsWith";
        public static string StrEndsWith = "EndsWith";
        public static string StrContains = "Contains";
        public static string ParenthesesStart = "(";
        public static string ParenthesesEnd = ")";

        public enum SQLComparierType
        {
            LessEqualThan,
            GreaterEqualThan,
            LessThan,
            GreaterThan,
            EqualTo,
            StrEquals,
            StrStartsWith,
            StrEndsWith,
            StrContains,
            ParenthesesStart,
            ParenthesesEnd,
        }

        public static string GetSQLComparier(SQLComparierType Comparier)
        {
            string comparier = string.Empty;
            switch (Comparier)
            {
                case SQLComparierType.EqualTo:
                    comparier = EqualTo;
                    break;
                case SQLComparierType.GreaterEqualThan:
                    comparier = GreaterEqualThan;
                    break;
                case SQLComparierType.GreaterThan:
                    comparier = GreaterThan;
                    break;
                case SQLComparierType.StrContains:
                    comparier = StrContains;
                    break;

            }
            return comparier;

        }
    }
}
