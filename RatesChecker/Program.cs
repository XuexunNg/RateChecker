using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using RatesChecker.Data.Interface;
using RatesChecker.Data;
using RatesChecker.View;
using RatesChecker.Helpers;
using RatesChecker.Services;
using System;

namespace RatesChecker
{
    class Program
    {
        
        [Verb("period", HelpText = "Get data within the period")]
        public class Period
        {
            [Option('f', "from", Required = true, HelpText = "From Date in YYYYY-MM")]
            public string fromDate { get; set; }

            [Option('t', "to", Required = true, HelpText = "To Date in YYYYY-MM")]
            public string toDate { get; set; }
        }

        [Verb("compare", HelpText = "")]
        public class Compare
        {
            [Option('f', "from", Required = true, HelpText = "From Date in YYYYY-MM")]
            public string fromDate { get; set; }

            [Option('t', "to", Required = true, HelpText = "To Date in YYYYY-MM")]
            public string toDate { get; set; }
        }

        [Verb("average", HelpText = "")]
        public class Average
        {
            [Option('f', "from", Required = true, HelpText = "From Date in YYYYY-MM")]
            public string fromDate { get; set; }

            [Option('t', "to", Required = true, HelpText = "To Date in YYYYY-MM")]
            public string toDate { get; set; }
        }

        [Verb("trend", HelpText = "")]
        public class Trend
        {
            [Option('f', "from", Required = true, HelpText = "From Date in YYYYY-MM")]
            public string fromDate { get; set; }

            [Option('t', "to", Required = true, HelpText = "To Date in YYYYY-MM")]
            public string toDate { get; set; }
        }



        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
           .AddTransient<IRepo, Repo>()
           .BuildServiceProvider();

            //Set up DI
            var repo = serviceProvider.GetService<IRepo>();

            RateServices rateSvc = new RateServices(repo);

            Parser.Default.ParseArguments<Period, Compare, Average, Trend>(args).MapResult(
                (Period opts) => {

                    var fromDate = Helper.convertDate(opts.fromDate);
                    var toDate = Helper.convertDate(opts.toDate);
                   
                    var rateList = rateSvc.GetRates(fromDate, toDate).Result;
                    RatesView view = new RatesView();
                    view.DisplayRates(rateList);

                    return 1;
                },
                (Compare opts) => {
                    
                    var fromDate = Helper.convertDate(opts.fromDate);
                    var toDate = Helper.convertDate(opts.toDate);

                    var rateList = rateSvc.GetHighestRateMonth(fromDate, toDate).Result;
                    RatesView view = new RatesView();
                    view.DisplayRates(rateList);
                    
                    return 1;
                    
                },
                (Average opts) => {

                    var fromDate = Helper.convertDate(opts.fromDate);
                    var toDate = Helper.convertDate(opts.toDate);

                    var averageRates = rateSvc.GetAverageRates(fromDate, toDate).Result;

                    Console.WriteLine($"Average FC Rates:{averageRates.average_fc_rate}%");
                    Console.WriteLine($"Average Bank Rates:{averageRates.average_bank_rate}%");

                    return 1;

                },
                (Trend opts) => {

                    var fromDate = Helper.convertDate(opts.fromDate);
                    var toDate = Helper.convertDate(opts.toDate);

                    var trend = rateSvc.GetTrend(fromDate, toDate).Result;

                    Console.WriteLine($"The trend for the bank rates within the selected period is {trend}");

                    return 1;

                },
                errs => 1
           );


        }

     
    }
}
