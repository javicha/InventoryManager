# InventoryManager (Goal Systems - Code challenge Backend Engineer)

## Intro

We start from a fictitious case to illustrate the operation of an inventory management system. Let's take as an example that we work for a genomic sequencing company, and we need to control the inventory of the consumable products used in the laboratory. We provide a REST API to query, add and remove products. 

## Requirements

+ .NET 6
+ Docker installed (the application runs in containers)

## How to run the application

+ Clone the repository
+ Open a terminal and navigate to *InventoryManager/src* path
+ Launch the application by running the following Docker command: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d** (this command build and run all the required images). *NOTE: RabbitMQ needs more time to get up. It is normal that in the first moments some connection error traces appear in the microservices that connect to RabbitMQ. It is not a problem. Connection retries are implemented.*
+ Open a browser and navigate to http://localhost:7000/swagger/index.html to interact with the inventory API
+ It is a secure API. The following jwt token must be used to authenticate and be able to consume the API: *eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHb2FsU3lzdGVtcyIsImlhdCI6MTY1NTMyMzM4NiwiZXhwIjoxNjg2ODU5Mzg2LCJhdWQiOiJpbnZlbnRvcnlfbWFuYWdlciIsInN1YiI6InVzZXIudGVzdCIsIlJvbGUiOiJBZG1pbiIsIkVudmlyb25tZW50IjoiU3RhZ2luZyJ9.Vgvf2WGnqAcfM72bxoMJWIKZbfmIGNz-RSFWOtbWRYE*

## How to stop the application

Located on *InventoryManager/src* path, run the following Docker command to stop all the containers: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down**


## How to run test

Located on *InventoryManager/test/Inventory.Tests* path, execute the **dotnet test** command


## Not implemented requirements

+ A frontend has not been implemented to consume the API. All interactions must be done through Swagger (or similar)


## Solution architecture

A view of the global architecture of the application is shown:

![inventory_manager_arch](https://user-images.githubusercontent.com/3404380/174652645-ce286a7f-635c-4ac8-a5da-178280a87ac1.png)


We have 4 microservices, with asynchronous communication mechanism through RabbitMQ:
+ **Inventory.API**: Main microservice. Implement inventory management functionalities. Accessible at the url http://localhost:7000/swagger/index.html. It includes:
    + ASP.NET Core Web API application
    + REST API principles, CRUD operations (except update for simplicity)
    + Implementing DDD, CQRS, and Clean Architecture with using Best Practices applying SOLID principles. Developing CQRS implementation on commands and queries using MediatR, FluentValidation and AutoMapper packages.
    + InMemory database connection
    + Using Entity Framework Core ORM and database initialization with test Product entities when application startup
    + Publishing RabbitMQ ProductRemovedEvent event queue using MassTransit-RabbitMQ Configuration
+ **Inventory.Synchro**: Microservice for illustrative purposes. It executes a daily scheduled task (using the Coravel package), which is responsible of searching for the products that expire on the day. In case it finds products, it publishes an ProductExpiredEvent event for each of them.
+ **Laboratory.API**: Microservice for illustrative purposes (no swagger). The only functionality it implements is subscribing to a Rabbit queue to consume the event "ProductExpiredEvent". Inventory.Synchro publishes the event in the corresponding Rabbit queue, and this microservice consumes it and logs a message of the style *ProductExpiredConsumer - ProductExpiredEvent consumed - {event}*. We can see the message event in the logs, executing the command **docker logs laboratory.api** from the command line. A possible real use case would be to update the laboratory database, to avoid using expired products.
+ **Accounting.API**: Microservice for illustrative purposes (no swagger). The only functionality it implements is subscribing to a Rabbit queue to consume the event "ProductRemovedEvent". When we remove a product from the inventory, Inventory.API publishes the event in the corresponding Rabbit queue, and this microservice consumes it and logs a message of the style *ProductRemovedConsumer - ProductRemovedEvent consumed - {event}*. We can see the message event in the logs, executing the command **docker logs accounting.api** from the command line.


## Inventory.API architecture


## Design patterns and best practices


## Third-party Nuget packages

+ **AutoMapper**: A convention-based object-object mapper. We use it to mapping operations between objects.
+ **AutoMapper.Extensions.Microsoft.DependencyInjection**: AutoMapper extensions for ASP.NET Core necessary to register AutoMapper in .NET Core dependency injection tool.
+ **Coravel**: Near-zero config .NET Core library that makes Task Scheduling, Caching, Queuing, Mailing, Event Broadcasting... We use it for the scheduled execution of tasks.
+ **FluentValidation**: A validation library for .NET that uses a fluent interface to construct strongly-typed validation rules. We ise it to perform validations when applying CQRS, before execute commands.
+ **FluentValidation.DependencyInjectionExtensions**: Dependency injection extensions for FluentValidation. We use it to being able to register FluentValidation in the NET Core dependency injection tool
+ **MassTransit**: MassTransit is a message-based distributed application framework for .NET. We use it in order to create queues in RabbitMQ, and thus, support asynchronous communication between microservices.
+ **MassTransit.RabbitMQ**: MassTransit RabbitMQ transport support. We use in order to connect to Rabbit MQ message broker.
+ **MediatR.Extensions.Microsoft.DependencyInjection**: MediatR extensions for ASP.NET Core. We use it in order to implement CQRS with Mediator pattern
+ **Moq**: Moq is the most popular and friendly mocking framework for .NET.
+ **Moq.AutoMock**: An auto-mocking container that generates mocks using Moq
+ **Swashbuckle.AspNetCore**: Swagger tools for documenting APIs built on ASP.NET Core
+ **xunit**: xUnit.net is a developer testing framework, built to support Test Driven Development, with a design goal of extreme simplicity and alignment with framework features.


## Assumptions

+ We use an in-memory database for simplicity
+ We only implement unit tests for the use case of adding product to inventory, to illustrate examples in the 3 layers of Clean Architecture


## Possible improvements

+ Implement resilience mechanisms, such as retries, using for example the Polly library, in those use cases that require it
+ Apply CQRS to handle domain events, just as we are doing to handle use cases
+ Create more tests to achieve full code coverage
+ Implement sorting in GET query
+ Improving performance in event publishing using Parallel.ForEach
