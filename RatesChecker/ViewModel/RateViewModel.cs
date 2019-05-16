using System;
using System.Collections.Generic;
using System.Text;

namespace RatesChecker.ViewModel
{
    public class RateViewModel
    {
        public string end_of_month;

        public double? prime_lending_rate;

        public double? banks_fixed_deposits_3m;

        public double? banks_fixed_deposits_6m;

        public double? banks_fixed_deposits_12m;

        public double? banks_savings_deposits;

        public double? fc_hire_purchase_motor_3y;

        public double? fc_housing_loans_15y;

        public double? fc_fixed_deposits_3m;

        public double? fc_fixed_deposits_6m;

        public double? fc_fixed_deposits_12m;

        public double? fc_savings_deposits;

        public bool? fc_rate_higher { get; set; }
    }
}
