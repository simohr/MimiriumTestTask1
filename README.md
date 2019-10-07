# Mimirium Test

**Summary**
This is a simple RESTful API done with ASP.NET Core. The goals of this exercise were to implement basic HTTP methods 
(GET, POST, PUT, DELETE) using a .NET Web Application.

**Implementation**

  * Classes
    * Program <br />
This file is the default entry point for our application and starts our web host with the specified configuration.
    * Startup
       * ConfigureServices - configures services and singleton instances.
       * Configure - configures endpoints.
    * Company - container model for company information.
    * CompaniesCounter - container model for the database Id indexer.
    * CompaniesDatabaseSettings - container for loading the database settings.
    * CompanyService - this class establishes a connection with the database and holds the implementation of the queries.
    * CompaniesController - this class is a controller that defines the HTTP methods. It uses the CompanyService class to connect to the database.
  * Settings files
    * appsettings - holds the information for the database connection.
    * launchSettings - holds the configuration information(IP addresses, ports) for each platform of the application.


# Installation

**There are three ways for you to use this API.**

The first two will require you to have Visual Studio and MongoDB installed. You need ASP.NET Core package in 
Visual Studio. For MnogoDB you need to set up a local database by the name of “CompaniesDb” with 
two collections - “Companies” and “ComapniesCounter”. In “CompaniesCounter” collection you need to insert one 
document by the name of “counter” with value 0. Once inside the MongoDb console you can set it up by using the 
following commands:
```
    use CompaniesDb
    db.createCollection("Companies")
    db.CompaniesCounter.insertOne({"counter":0})
```
You now should be able to run the API locally through Visual Studio or build it and run the Mimirium.exe in 
the bin folder of the solution. If you want to access the API with devices in the same network you will need to 
open the ports - 
```bash
    netsh http add urlacl url=http://*:5000/ user=Everyone listen=yes
    netsh advfirewall firewall add rule name=”Http Port 5000” dir=in action=allow protocol=TCP localport=5000
```
For the third way to use this API you will have to have Docker installed. 
Head to the root directory of the MimiriumTest solution and run the following commands:
```bash
    docker build -t mimirium .
    docker-compose up --build
```

# MobileApp

**Summary**
This is a mobile app developed to consume the MimiriumTest Web API.

**Implementation**
  * Classes
    * AddCompany - activity with fields for adding a new company.
    * EditActivity - activity with fields for editing a company.
    * MainActivity - the activity that loads when the application starts. It has a list view for displaying the companies and a search view for searching companies by name.
    * CompanyAdapter - adapter for the MainActivity that populates the listView with given list of companies.
    * Company - container for company information.
    * Constants - static class that holds definitions(URLs, etc.).
  * Layouts
    * activity_main- layout for the MainActivity.
    * company_edit - layout for the EditActivity and AddCompany activities.
    * company_list_item - layout for the CompanyAdapter list item.

**Setup**
You need to have Visual Studio with android support(Xamarin Android). You need to modify the Constants script SERVER_HOST 
variable with the correct IP address and port depending on where you chose to run the MimiriumTest API.


