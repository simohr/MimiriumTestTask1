# Installation

**There are three ways for you to use this API.**

The first two will require you to have Visual Studio and MongoDB installed. You need ASP.NET Core package in 
Visual Studio. For MnogoDB you need to set up a local database by the name of “CompaniesDb” with 
two collections - “Companies” and “ComapniesCounter”. In “CompaniesCounter” collection you need to insert one 
document by the name of “counter” with value 0. Once inside the MongoDb console you can set it up by using the 
following commands:

    use CompaniesDb
    db.createCollection(“Companies”)
    db.CompaniesCounter.insertOne({“counter”:0})

You now should be able to run the API locally through Visual Studio or build it and run the Mimirium.exe in 
the bin folder of the solution. If you want to access the API with devices in the same network you will need to 
open the ports - 

    netsh http add urlacl url=http://*:5000/ user=Everyone listen=yes
    netsh advfirewall firewall add rule name=”Http Port 5000” dir=in action=allow protocol=TCP localport=5000

For the third way to use this API you will have to have Docker installed. 
Head to the root directory of the MimiriumTest solution and run the following commands:

    docker build -t mimirium .
    docker-compose up --build
