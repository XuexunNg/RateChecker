using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RatesChecker.Helpers
{
    static class Helper
    {
        public static string convertDate(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "MMM-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM");
            }
            catch
            {
                Console.WriteLine("Invalid Date Format. The date format should be MMM-yyyy (i.e Jan-2017)");
                return "";
            }

        }
    }
}
