# RateChecker
To compare and search interests rate for banks and financial center within a given period of time. This project is written with C# 7.3 using the .net core 2.1 framework. 

## Design Pattern 

### Repository Pattern
https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff649690(v=pandp.10)
The repository pattern separates the data access logic away from the business logic layer. The business logic layer will not be concern where the data is from. It can be from persistent datastore or an API from MAS. This will allows flexibility for furture changes (the data source can be changed to a SQL database wthout affecting the entire business logic). This pattern also provides a "single source of truth" and one single point of entry to the data source which makes maintainability easier. 

But most importantly, this pattern allows me to "inject" a mock data source to my service layer to create accurate and comprehensive unit tests. You can see this pattern in place under the Data folder. There is a IRepo interface class which I can use to inject into the service layer.

###Dependency Injection
https://en.wikipedia.org/wiki/Dependency_injection

In conjunction with the repository pattern, the dependency injection design pattern is applied to inject the repo interface class to the service layer. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
Give examples
```

### Installing

A step by step series of examples that tell you how to get a development env running

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## Deployment

Add additional notes about how to deploy this on a live system

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Billie Thompson** - *Initial work* - [PurpleBooth](https://github.com/PurpleBooth)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
