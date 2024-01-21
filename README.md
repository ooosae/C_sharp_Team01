# Team 01 – .NET Boot camp
### Under the weather

# Contents
1. [Chapter I](#chapter-i) \
  [General Rules](#general-rules)
2. [Chapter II](#chapter-ii) \
  [Rules of the Day](#rules-of-the-day)
3. [Chapter III](#chapter-iii) \
  [Intro](#intro)
4. [Chapter IV](#chapter-iv) \
  [Exercise 00 – Basics](#exercise-00-basics)
5. [Chapter V](#chapter-v) \
  [Exercise 01 – Environment](#exercise-01-environment) 
6. [Chapter VI](#chapter-vi) \
  [Exercise 02 – GET weather by coordinates](#exercise-02-get-weather-by-coordinates) 
7. [Chapter VII](#chapter-vii) \
  [Exercise 03 – GET weather by city](#exercise-03-get-weather-by-city)
8. [Chapter VIII](#chapter-viii) \
  [Exercise 04 – Code refactoring](#exercise-04-code-refactoring)
9. [Chapter IX](#chapter-ix) \
  [Exercise 05 – POST/PUT/DELETE, repeat](#exercise-05-postputdelete-repeat)

# Chapter I 

### General Rules
- Make sure you have [the .NET 5 SDK](<https://dotnet.microsoft.com/download>) installed on your computer and use it.
- Remember, your code will be read! Pay special attention to the design of your code and the naming of variables. Adhere to commonly accepted [C# Coding Conventions](<https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/inside-a-program/coding-conventions>).
- Choose your own IDE that is convenient for you.
- The program must be able to run from the dotnet command line.
- Each of the exercise contains examples of input and output. The solution should use them as the correct format.
- If you find the problem difficult to solve, ask questions to other piscine participants, the Internet, Google or go to StackOverflow.
- You may see the main features of C# language in [official specification](<https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/language-specification/introduction>).
- Avoid **hard coding** and **"magic numbers"**.
- You demonstrate the complete solution, the correct result of the program is just one of the ways to check its correct operation. Therefore, when it is necessary to obtain a certain output as a result of the work of your programs, it is forbidden to show a pre-calculated result.
- Pay special attention to the terms highlighted in **bold** font: their study will be useful to you both in performing the current task, and in your future career of a .NET developer.
- Have fun :)


# Chapter II
###  Rules of the Day

- Use **var**.
- The name of the solution (and its separate catalog) is d {xx}, where xx are the digits of the current day. The names of the projects are specified in the exercise.
- To format the output data, use the en-GB **culture**: N2 for the output of monetary amounts, d for dates.


# Chapter III
## Intro

If you managed to participate in the previous days of the piscine, you may have noticed the frequent simplicity of output in the exercises. The exception was Rush 00, where you could try yourself in the development of desktop applications. Today we will look at another way to exchange information with the user: **web applications** and in particular the **Web API**.

Today's topic will be the weather. The subject of any conversation,  a cause of admiration and complaints. And forecasts. Not always accurate, but always extremely useful.

Let's develop a web application that will provide an API for getting the weather forecast in the selected location.

To develop our API, we will use the **REST** architectural approach. Unlike **SOAP**-services, **REST** is not a standard, but rather a set of practices and approaches, a general concept for implementing web-services with an access interface. Let's try it in practice.

# Chapter IV
## Exercise 00 – Basics

[To create a web application in .NET](<https://docs.microsoft.com/en-us/learn/modules/build-web-api-aspnet-core/>) use the standard ASP.NET Core Web Application template and select the Web API type for it. You should have a web project containing configuration files, configuration classes, and **a controller** with the WeatherForecast model.

Your project is already working. Run and test the method written out of the box:
/WeatherForecast should return 5 randomly generated forecasts. This data is static and set directly in the code, but we will change this later. Now we will consider the project itself.

Take a look at the Program.cs file and see what's going on in it. This file and its Main method are the entry point when launching the application and are used in all .NET web projects.

The application is configured in the **Startup.cs** file, which is an important part of any web project on .NET. Learn on your own what the **Configure** and **ConfigureServices** methods are for, what **IApplicationBuilder** is.

The current configuration will be enough to continue working, but we will add documentation to our API. In Startup, there are calls to the **Swagger** service - a functionality for automatically generating documentation. At the address /swagger/index.html you can see the documentation for the methods that you currently have. Test them.

Add a description for the Get() method in the controller: a summary of what it does, and two types of response - 200 if everything is fine, and 400 if an error occurred. **ProducesResponseType** is perfect for indicating different [response statuses](<https://developer.mozilla.org/en-US/docs/Web/HTTP/Status>) of the method.

Open the Swagger page of your application and check if the description of the method and the possible statuses of its responses are correctly displayed in the generated documentation.

Don't forget to check that the documentation is generated in an XML file.


# Chapter V
## Exercise 01 – Environment

When it comes to web development, it is assumed that the **backend** application can be launched in different **environments**: it will be debugged locally on the developer's machine, run on the dev server for testing by the team, and then released to production.

However, some application settings may (and should) differ for different environments. This is solved easily. Firstly, the appsettings.json configuration files have a postfix with the name of the environment where this particular configuration is applied.

Secondly, in the *Startup* file, you can find a property of the **IConfiguration** type and a parameter of the **IWebHostEnvironment** type. Here you can, for example, find that the swagger API documentation is enabled only for the dev configuration. This is done in order not to give access to a detailed description of your interface in production versions.

The application learns about [what kind of environment it is](<https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-5.0>) when it starts. To do this, it reads the value of the **ASPNETCORE_ENVIRONMENT** argument.

Run the application in the Production environment. Make sure that the swagger-documentation is not available in this configuration.


# Chapter VI
## Exercise 02 – GET weather by coordinates

Let's make the weather forecast request relevant. Use the public [OpenWeather API](<https://openweathermap.org/current>): register, get an API key and change the Get() method of your controller to accept geographic coordinates (latitude and longitude) and return weather information using an OpenWeather call.

Output information about temperature, pressure, humidity, wind speed, the name of a geographical object and a description of the weather.

Add a description to the method and model. Make sure it is displayed in the swagger-documentation.

### Example of a call

```
{
    wind: 
    {
            speed: 4.47
    },
    weather: 
    [
            {
                      description: "overcast clouds"
            }
    ],
    main: 
    {
            temp: 29.27,
            pressure: 1007,
            humidity: 57
    },
    name: "Kizicheskaya"
}
```

# Chapter VII
## Exercise 03 – GET weather by city

Add another **HttpGet** method to the controller that will accept the name of the city and return the weather forecast for this city. Use the appropriate method in the [OpenWeather API](<https://openweathermap.org/current>).

Use the output format from Exercise 02. Pay attention to **routing**, it can be implemented [using attributes](<https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2>). Make the method available by the link
/WeatherForecast/{cityName}.

Add a description to the method and model. Make sure it is displayed in the swagger-documentation.

### Example of a call

```
{
    wind: 
    {
            speed: 4.49
    },
    weather: 
    [
            {
                      description: "overcast clouds"
            }
    ],
    main: 
    {
            temp: 29.34,
            pressure: 1007,
            humidity: 57
    },
    name: "Kazan’"
}
```

# Chapter VIII
## Exercise 04 – Code refactoring

### Structuring

Let's clean up. To be ready to expand the functionality and take advantage of the principles of **SOLID**, separate all code related to the OpenWeather API into a separate *WeatherClient* class.
You should have a class with two methods for getting a weather forecast: by coordinates and by the name of the city. Both methods should access the OpenWeather API, so this functionality will be **encapsulated**. The only thing left in the controller is to call these methods with the required arguments.

Move the weather forecast model and the service for working with the OpenWeather API into a separate project *rush01.WeatherClient*. To test yourself, ask yourself the question: what happens if I decide to use a different method of input and output of information, and instead of a web application, it will be a desktop application or a console application?
With this separation, you can easily reuse the project that implements the main logic, and simply connect another application to it with a new entry point.

### API KEY

Note that the OpenWeather API has different tariffication options and, accordingly, different restrictions. This means that a test account may well be suitable for development, but in a production environment you may have to purchase a full license (hypothetically). So, the key that we use to connect can be different and depend on the environment.

Put the API KEY value in the *appsettings files.json* and *appsettings.Development.json* and make it so that it is passed to the *WeatherClient* class and used as a parameter.

### DI

Think about how the *WeatherClient* object is created in the controller that uses it:
- We can create a new instance every time there is a need for it. 
- We can make it a property of the controller and create a new instance in the controller constructor once, after using the filled property. 
- We can fill the property with a value that is a parameter of the constructor and the ready-made one is passed to it.

All three methods are legitimate, their use depends on the requirements for the creation rules, the lifetime, and the number of instances of the object. Most often, it is convenient to use the third option - it is much more convenient to write unit tests for it.

Where will the controller's constructor be called from to pass the required parameter to it? This is where **Dependency Injection** comes to our help. This is a form of **Inversion of control** (IoC), which allows you to implement dependencies from the outside. In fact, the object configuration style, in which the object fields are set by an external entity. In our case, a [service container that is built into the .NET tool](<https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0>).

Edit the WeatherForecastController so that it contains

```
private readonly WeatherClient _weatherClient;
```
and that the value of this property is passed to the controller constructor and set there. Note that the property is **private** and **readonly**, why is this necessary?

Edit Startup.cs so that the *WeatherClient* dependency is registered in the *ConfigureServices* method when the application is launched. The service will embed the object in the controller constructor where it is used. The platform takes care of creating an instance of the dependency and deleting it when it is no longer needed.

Launch the application and check that everything is working correctly.

### Options

Using Dependency Injection, you can [register an options object ](<https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-5.0>) that will allow you to embed configuration elements where they are needed. Remember, we took out the API key value in appsettings?

Create the *ServiceSettings* class with the *ApiKey* field and edit the *WeatherClient* constructor:
```
public WeatherClient (IOptions<ServiceSettings> options)
```
Make the object filled with the value from the appsettings file. This dependency must also be registered in the *ConfigureServices Startup.cs* method.


# Chapter IX
## Exercise 05 – POST/PUT/DELETE, repeat

In addition to the HTTP **GET** method, [others](<https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods>) may be useful to you, each of them has its own scope of applicability. For example, look into the difference between using the **PUT**, **POST**, and **PATCH** methods. Read what **idempotence** is.

Let's make it so that you can set the default city if you want. If it is set, the Get method from Exercise 03 will return:
- if the name of the city is passed to the method: weather for this city;
- if the name of the city is not passed to the method and the default city is set: weather for the default city;
- if the city name is not passed to the method and the default city is not set: [404](<https://developer.mozilla.org/ru/docs/Web/HTTP/Status>).

Add the Post() method to the controller, which accepts the name of the city. Don't forget to add a swagger description to the method! If everything is fine, the method should return HTTP status OK.

Since one of the features of the REST principle is **serverless**, and it is wasteful to start an entire database to store a single row, let's use [caching](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-5.0).

Change the application code so that the Post() method stores the city passed to it in the cache, and the Get method from Exercise 03 uses it if necessary. Check that everything is working correctly:
1. Call the Get method without specifying the city. The method should return the status 404.
2. Call the Get method specifying the city of Barnaul. The method should return the weather for the city of Barnaul.
3. Call the Post method specifying the city of Albuquerque. The method should return the status 200.
4. Call the Get method specifying the city of Barnaul. The method should return the weather for the city of Barnaul.
5. Call the Get method without specifying the city. The method should return the weather for the city of Albuquerque.
