using RatesChecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RatesChecker.Data.Interface
{
    public interface IRepo
    {
        Task<List<RateViewModel>> HttpRequestRates(string fromDate, string toDate);
       
    }
}
