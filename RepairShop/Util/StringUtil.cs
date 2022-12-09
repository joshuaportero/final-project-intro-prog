using System.Globalization;
using System.Text.RegularExpressions;

namespace RepairShop.Util
{
    public static class StringUtil
    {
        public static string DoubleToCurrency(double value)
        {
            return value.ToString("C", new CultureInfo("en-US"));
        }

        public static string FixFromNamingConvention(string value)
        {
            var regex = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            return regex.Replace(value, " ");
        }
    }
}