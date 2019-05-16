using System;
using System.Collections.Generic;
using System.Text;

namespace RatesChecker.Helpers
{
    static class Config
    {
        public static string WebURL(string dateFrom, string dateTo)
        {
            return $"https://eservices.mas.gov.sg/api/action/datastore/search.json?resource_id=5f2b18a8-0883-4769-a635-879c63d3caac&limit=500&between[end_of_month]={dateFrom},{dateTo}";

        }

    }
}
