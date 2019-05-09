using Microsoft.Extensions.DependencyInjection;
using RatesChecker.Data;
using RatesChecker.Data.Interface;
using RatesChecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RatesChecker.Services
{
    public class RateServices
    {
        private readonly IRepo _repo;
        
        public RateServices(IRepo repo)
        {            
            _repo = repo;            
        }

        public async Task<List<RateViewModel>> GetRates(string fromDate, string toDate)
        {
            return await _repo.HttpRequestRates(fromDate, toDate);
        }

        public async Task<List<RateViewModel>> GetHighestRateMonth(string fromDate, string toDate)
        {
            var rateList = await _repo.HttpRequestRates(fromDate, toDate);
            foreach (var rate in rateList)
            {
                rate.fc_rate_higher = rate.fc_savings_deposits > rate.banks_savings_deposits ? true : false;
            }

            return rateList;
        }

        public async Task<AverageRateViewModel> GetAverageRates(string fromDate, string toDate)
        {
            var rateList = await _repo.HttpRequestRates(fromDate, toDate);

            double totalFcRates = 0;
            double totalBankRates = 0;

            foreach (var rate in rateList)
            {
                totalFcRates += rate.fc_savings_deposits;
                totalBankRates += rate.banks_savings_deposits;
            }

            AverageRateViewModel vm = new AverageRateViewModel();
            vm.average_fc_rate = totalFcRates / rateList.Count;
            vm.average_bank_rate = totalBankRates / rateList.Count;
           
            return vm;
        }

        public async Task<string> GetTrend(string fromDate, string toDate)
        {
            var rateList = await _repo.HttpRequestRates(fromDate, toDate);

            //calculate the slope using Linear Regression
            double counter = 1;
            double add2 = 0;
            double add3 = 0;
            double add4 = 0;
            double add5 = 0;
            foreach (var rate in rateList)
            {
                add2 += counter * rate.banks_savings_deposits;
                add3 += rate.banks_savings_deposits;
                add4 += counter * counter;
                add5 += counter;
                counter++;
            }

            var b = (rateList.Count * add2 - add5 * add3) / (rateList.Count * add4 - add5 * add5);

            if (b > 0)
            {
                return "uptrend";

            } else if (b < 0)
            {
                return "downtrend";

            } else
            {
                return "stable";
            }

        }

    }
}
