using Moq;
using RatesChecker.Data.Interface;
using RatesChecker.Services;
using RatesChecker.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest
{
    public class UnitTest
    {

        /// <summary>
        /// This unit test focus on mocking the repo to test various methods in the service layer
        /// </summary>

        [Fact]
        public async void ShouldReturnRecords_GetRates()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month = "2018-01", banks_savings_deposits = 0.16, fc_savings_deposits = 0.17 });
            vm.Add(new RateViewModel { end_of_month = "2018-02", banks_savings_deposits = 0.16, fc_savings_deposits = 0.17});
            vm.Add(new RateViewModel { end_of_month = "2018-03", banks_savings_deposits = 0.16, fc_savings_deposits = 0.17 });
            vm.Add(new RateViewModel { end_of_month = "2018-04", banks_savings_deposits = 0.16, fc_savings_deposits = 0.17 });
            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetHighestRateMonth(mock.Object, It.IsAny<string>(), It.IsAny<string>()); 
            var result = results.Count;

            //assert
            Assert.Equal(4, result);

        }


        [Fact]
        public async void ShouldReturnTrue_IfFcRateisHigher()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month="2019-04", banks_savings_deposits = 0.1, fc_savings_deposits = 0.2});           
            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetHighestRateMonth(mock.Object, It.IsAny<string>(), It.IsAny<string>()); //Pass in the mock repo into the method
            var result = results.Find(c => c.end_of_month == "2019-04");

            //assert
            Assert.True(result.fc_rate_higher);

        }

        [Fact]
        public async void ShouldReturnFalse_IfFcRateIsLower()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month = "2019-04", banks_savings_deposits = 0.23, fc_savings_deposits = 0.12 });
            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetHighestRateMonth(mock.Object, It.IsAny<string>(), It.IsAny<string>()); 
            var result = results.Find(c => c.end_of_month == "2019-04");

            //assert
            Assert.False(result.fc_rate_higher);

        }

        [Fact]
        public async void ShouldReturnTrue_GetAverageRate()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month = "2018-01", banks_savings_deposits = 0.12, fc_savings_deposits = 0.16 });
            vm.Add(new RateViewModel { end_of_month = "2018-02", banks_savings_deposits = 0.13, fc_savings_deposits = 0.17 });
            vm.Add(new RateViewModel { end_of_month = "2018-03", banks_savings_deposits = 0.14, fc_savings_deposits = 0.18 });
            vm.Add(new RateViewModel { end_of_month = "2018-04", banks_savings_deposits = 0.15, fc_savings_deposits = 0.19 });

            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetAverageRates(mock.Object, It.IsAny<string>(), It.IsAny<string>());
            var averageBankRate = results.average_bank_rate;
            var averageFcRate = results.average_fc_rate;

            //assert
            Assert.Equal(0.135, averageBankRate);
            Assert.Equal(0.175, averageFcRate);

        }


        [Fact]
        public async void ShouldReturnDownTrend_IfBankRatesAreDown()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month = "2018-01", banks_savings_deposits = 0.20 });
            vm.Add(new RateViewModel { end_of_month = "2018-02", banks_savings_deposits = 0.19 });
            vm.Add(new RateViewModel { end_of_month = "2018-03", banks_savings_deposits = 0.16 });
            vm.Add(new RateViewModel { end_of_month = "2018-04", banks_savings_deposits = 0.14});

            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetTrend(mock.Object, It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.Equal("downtrend", results);
 
        }


        [Fact]
        public async void ShouldReturnUpTrend_IfBankRatesAreUp()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month = "2018-01", banks_savings_deposits = 0.10 });
            vm.Add(new RateViewModel { end_of_month = "2018-02", banks_savings_deposits = 0.11 });
            vm.Add(new RateViewModel { end_of_month = "2018-03", banks_savings_deposits = 0.14 });
            vm.Add(new RateViewModel { end_of_month = "2018-04", banks_savings_deposits = 0.18 });

            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetTrend(mock.Object, It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.Equal("uptrend", results);

        }


        [Fact]
        public async void ShouldReturnStableTrend_IfBankRatesAreStable()
        {
            //arrange
            RateServices svc = new RateServices();
            var mock = new Mock<IRepo>();
            List<RateViewModel> vm = new List<RateViewModel>();
            vm.Add(new RateViewModel { end_of_month = "2018-01", banks_savings_deposits = 0.14 });
            vm.Add(new RateViewModel { end_of_month = "2018-02", banks_savings_deposits = 0.14 });
            vm.Add(new RateViewModel { end_of_month = "2018-03", banks_savings_deposits = 0.14 });
            vm.Add(new RateViewModel { end_of_month = "2018-04", banks_savings_deposits = 0.14 });

            mock.Setup(foo => foo.HttpRequestRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(vm);

            //act
            var results = await svc.GetTrend(mock.Object, It.IsAny<string>(), It.IsAny<string>());

            //assert
            Assert.Equal("stable", results);

        }       
    }
}
