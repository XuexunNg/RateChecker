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
                if (String.IsNullOrEmpty(date)) { date = DateTime.Now.AddMonths(-1).ToString("MMM-yyyy"); };

                var convertedDate = DateTime.ParseExact(date, "MMM-yyyy", CultureInfo.InvariantCulture);               

                if (convertedDate > DateTime.Now)
                {
                    throw new ArgumentException("Date cannot be later than today date");
                }

                return convertedDate.ToString("yyyy-MM");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            catch(SystemException ex)
            {
                Console.WriteLine("Invalid Date Format. The date format should be MMM-yyyy (i.e Jan-2017)");
                return "";
            }
        }
    }
}
