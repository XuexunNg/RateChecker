[![Build Status](https://travis-ci.org/XuexunNg/RatesChecker.svg?branch=master)](https://travis-ci.org/XuexunNg/RatesChecker)

# RateChecker
RateChecker is a command line program that compare and search interests rate for banks and financial center within a given period of time. This project is written in C# 7.3 using the .net core 2.1 framework. 

## Design Pattern 

### Repository Pattern
https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff649690(v=pandp.10)

The repository pattern separates the data access logic away from the business logic layer. The business logic layer will not be concern where the data is from. It can be from persistent datastore or an API from MAS. This will allows flexibility for furture changes (the data source can be changed to a SQL database without affecting the entire business logic). This pattern also provides a "single source of truth" and one single point of entry to the data source which makes maintainability easier. 

But most importantly, this pattern allows me to "inject" a mock data source to my service layer to create accurate and comprehensive unit tests. You can see this pattern in place under the Data folder. There is a IRepo interface class which I can use to inject into the service layer.

### Service Layer Pattern
https://en.wikipedia.org/wiki/Service_layer_pattern

The service layer for this project serves as the business logic layer. The service layer pattern is loosely coupled with the repo layer. This will allows us to create unit tests without accessing any data source and provide accurate test for the business logic. 

### Dependency Injection
https://en.wikipedia.org/wiki/Dependency_injection

In conjunction with the repository pattern, the dependency injection design pattern is applied to inject the repo interface class to the service layer. I use method dependency injection which can be seen in Program.cs. Using dependency injection, I can mock my repo interface in the unit tests so that I can create tests for different set of data.

### MVVM
https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93viewmodel

The entire project is created based on MVVM design pattern. I created the ViewModel layer to serve as a DTO between the layers. Also by having a separation between the different parts of an app's, it brings a level of structure and uniformity to the code. It's easy to see where things should go or where they're likely to be. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for testing purposes. 

### Prerequisites

Currently, RateChecker is only supported on Windows platform. Although, .net core allows me to publish a cross platform app. The app has not been tested on other platforms other than Windows.

Requires internet connection to run 


## Building RateChecker
* Download and install .net core 2.1 or above SDK https://dotnet.microsoft.com/download/dotnet-core/2.2

* Download repo to computer

* Open powershell and navigate to the folder containing RatesChecker.sln

* Enter command "dotnet restore" (https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-restore?tabs=netcore2x)

* Enter command "dotnet build" (https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build?tabs=netcore2x)


## Running RateChecker

* Download the release build from here https://github.com/XuexunNg/RatesChecker/releases

* Unzip folder

* Open powershell and navigate to the RateChecker folder.

### Get rates between a certain period
Enter the following command. The date format is in MMM-yyyy (i.e Jan-2018). -f is the from Date and -t is the to Date
Example
```
.\RatesChecker.exe period -f Jan-2018 -t Dec-2018
```
This will return rates between Jan 2018 and Dec 2018

### Get rates between a certain period and indicate if bank rates are lower then financial companies rates
 Example
```
 .\RatesChecker.exe compare -f Jan-2017 -t Dec-2018
```
This will return bank interest rates and the financial center interest rate between Jan 2017 and Dec 2018. The results will include a attribute called "FC Higher Rate" which display true if the financial companies interest rate is higher than bank rates or false if the financial companies interest rate is lower than bank rates

### Get average rates of bank rates and financial companies rates between a certain period
 Example
```
.\RatesChecker.exe average -f Jan-2018 -t Dec-2018
```
This command will return two values. The average FC rates and the average Bank rates within the given period

### Get trend of interest rate within a certain period
 Example
```
.\RatesChecker.exe trend -f Jan-2014 -t Dec-2018
```
I use linear regression to calculate the slope of the trend. This command will return one of the following values:
- upward
- downward
- stable
 

## Run Unit Test
* Download and install .net core 2.1 or above SDK https://dotnet.microsoft.com/download/dotnet-core/2.2

* Download the repo

* Navigate to XUnitText Folder

* Type in the command 
```
dotnet test
```
Note: Travis CI server will also run the unit tests on every commit to master branch

## Built With

* .Net Core 2.1
* MSBuild


## Authors

* **Eric Ng** 



