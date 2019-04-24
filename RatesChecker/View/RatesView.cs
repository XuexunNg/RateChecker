using RatesChecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatesChecker.View
{
    class RatesView
    {
        public void DisplayRates(List<RateViewModel> rateList)
        {

            foreach (var rate in rateList)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Month:{rate.end_of_month}");
                Console.WriteLine($"Prime Lending Rate:{rate.prime_lending_rate}%");
                Console.WriteLine($"Bank Savings Rate:{rate.banks_savings_deposits}%");
                Console.WriteLine($"FC Saving Rate:{rate.fc_savings_deposits}%");
                if (rate.fc_rate_higher != null) Console.WriteLine($"FC Higher Rate:{rate.fc_rate_higher}");
                Console.WriteLine("-------------------------------------------------");
            }

        }
    }
}
