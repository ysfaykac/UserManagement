# UserManagement 

In this project Postgresql is used which is managed by EntityFramework ORM tool with CodeFirst approach.
Clean Architecture and DDD are tried to be used correctly in this project.

## Technologies & Tools

* ASP.Net 6
* ASP.NET Core Web API
* Entity Framework Core 6.*
* PostgreSQL
* Docker - Docker Compose
* RabbitMq
* UnitTests

| Concept | Description |
| --- | --- |
| Docker | It was implemented to help us make a faster and reliable deployment. |

## Prerequities

You will need the following tools:

* [Visual Studio Code or 2022](https://www.visualstudio.com/downloads/) 
* [.Net 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
* [Docker](https://www.docker.com/)
* [Docker Compose](https://docs.docker.com/compose/)


# How to Use
 After opening Command Promdt, go to the directory where the project is located and run the code below.
```
 CMD>docker-compose up -d
```
**Application Run**

For it to run in Visual Studio, make sure the .net 5 sdk is installed and the postgresql container in docker-compose is running. There are 3 main projects that need to be run after opening the project. These are UserManagement.Api, UserService.Api and UserManagement.Grpc. 
Select these projects as multiple startup projects and run the application.

**Enpoints**

There are 3 endpoints and one hosted service application. Endpoints are listed below.

```
  http://localhost:55611/ UserManagement.Api
  http://localhost:54555 UserService.Api
  http://localhost:5289/ UserManagement.Grpc
  http://localhost:5050/  PgAdmin
  http://localhost:5432/  Postgresql
  http://localhost:15672/ RabbitMq
```


# Test Project
You can see some usage in test folder.