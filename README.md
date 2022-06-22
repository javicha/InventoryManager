#  InventoryManager

##  Intro

We start from a fictitious case to illustrate the operation of an inventory management system. Let's take as an example that we work for a genomic sequencing company, and we need to control the inventory of the consumable products used in the laboratory. We provide a REST API to query, add and remove products. 

##  Requirements

+  .NET 6
+  Docker installed (the application runs in containers)

##  How to run the application

+  Clone the repository
+  Open a terminal and navigate to *InventoryManager/src* path
+  Launch the application by running the following Docker command: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d** (this command build and run all the required images). *NOTE: RabbitMQ needs more time to get up. It is normal that in the first moments some connection error traces appear in the microservices that connect to RabbitMQ. It is not a problem. Connection retries are implemented.*
+  Open a browser and navigate to http://localhost:7000/swagger/index.html to interact with the inventory API
+  It is a secure API. The following jwt token must be used to authenticate and be able to consume the API: *eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHb2FsU3lzdGVtcyIsImlhdCI6MTY1NTMyMzM4NiwiZXhwIjoxNjg2ODU5Mzg2LCJhdWQiOiJpbnZlbnRvcnlfbWFuYWdlciIsInN1YiI6InVzZXIudGVzdCIsIlJvbGUiOiJBZG1pbiIsIkVudmlyb25tZW50IjoiU3RhZ2luZyJ9.Vgvf2WGnqAcfM72bxoMJWIKZbfmIGNz-RSFWOtbWRYE*

## How to stop the application

Located on *InventoryManager/src* path, run the following Docker command to stop all the containers: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down**


##  How to run test

Located on *InventoryManager/test/Inventory.Tests* path, execute the **dotnet test** command


##  Not implemented requirements

+  A frontend has not been implemented to consume the API. All interactions must be done through Swagger (or similar)


##  Solution architecture

A view of the global architecture of the application is shown:

![inventory_manager_arch](https://user-images.githubusercontent.com/3404380/174652645-ce286a7f-635c-4ac8-a5da-178280a87ac1.png)


We have 4 microservices, with asynchronous communication mechanism through RabbitMQ. We manage events through asynchronous communication between microservices, using the MassTransit library (it has native support for Rabbit). The events generate a publish action to RabbitMQ, and another service consumes it. We will create a common class library, to handle the events. In this way, each microservice that needs events will add a reference to this library. Brief description of the services:

+  **Inventory.API**: Main microservice. Implement inventory management functionalities. Accessible at the url http://localhost:7000/swagger/index.html. It includes:
    +  ASP.NET Core Web API application
    +  REST API principles, CRUD operations (except update for simplicity)
    +  Implementing DDD, CQRS, and Clean Architecture using Best Practices applying SOLID principles. Developing CQRS implementation on commands and queries using MediatR, FluentValidation and AutoMapper packages.
    +  InMemory database connection
    +  Using Entity Framework Core ORM and database initialization with test Product entities when application startup
    +  Publishing RabbitMQ ProductRemovedEvent event queue using MassTransit-RabbitMQ Configuration
+  **Inventory.Synchro**: Microservice for illustrative purposes. It executes a daily scheduled task (using the Coravel package), which is responsible of searching for the products that expire on the day. In case it finds products, it publishes an ProductExpiredEvent event in RabbitMQfor each of them. *NOTE: Once the application starts, it waits 1 minute to check the expired products and trigger the event*
+  **Laboratory.API**: Microservice for illustrative purposes (no swagger). The only functionality it implements is subscribing to a Rabbit queue to consume the event "ProductExpiredEvent". Inventory.Synchro publishes the event in the corresponding Rabbit queue, and this microservice consumes it and logs a message of the style *ProductExpiredConsumer - ProductExpiredEvent consumed - {event}*. We can see the message event in the logs, executing the command **docker logs laboratory.api** from the command line. A possible real use case would be to update the laboratory database, to avoid using expired products.
+  **Accounting.API**: Microservice for illustrative purposes (no swagger). The only functionality it implements is subscribing to a Rabbit queue to consume the event "ProductRemovedEvent". When we remove a product from the inventory, Inventory.API publishes the event in the corresponding Rabbit queue, and this microservice consumes it and logs a message of the style *ProductRemovedConsumer - ProductRemovedEvent consumed - {event}*. We can see the message event in the logs, executing the command **docker logs accounting.api** from the command line.
+  **RabbitMQ**: We can access the Rabbit dashboard at the url http://localhost:7004/, using the default username and password (guest/guest). In this way we can also monitor the queues to see the published events.

![rabbit_queues](https://user-images.githubusercontent.com/3404380/174673944-7dc99542-f33b-4803-82d3-bca77dcb5c8f.png)


Regarding the folder structure, we have a root folder with the code (src) and another with the tests (test), to separate the deployments from the test projects. Then inside the src folder, we start from the identification of the contexts of our system, and we will create these conceptual divisions within /src/bounded_context packages (for example, src/Inventory, src/Laboratory, src/Accounting). If within each context, we identify several modules, we can in turn create subdivisions by modules (for example Inventory/Product).

![folder_structure](https://user-images.githubusercontent.com/3404380/174668806-e1d28fb9-a0b3-4dd0-b9d1-c7f2301f5015.png)


##  Inventory.API architecture

We design our service following the principles of Clean Architecture DDD. Then, we structure the code in 4 layers:

![clean_architecture](https://user-images.githubusercontent.com/3404380/174666084-23ac18ef-88a5-49e5-abc1-eb64da12fedd.png)

**Inventory.Domain** layer and **Inventory.Application** layer will be the core layers. And we have **Inventory.API** layer, which is the presentation layer, and **Inventory.Infrastructure** layer (we also call Periphery layers). The main idea behind Clean Architecture approach, is to separate the domain code from the application and infrastructure code, so that the core (business logic) of our software can evolve independently of the rest of the components. Regarding the DDD (Domain Drive Design) approach, it proposes a modeling based on business reality according to its use cases. The important thing is to organize the code so that it is aligned with the business problems and uses the same business terms (ubiquitous language). 

We explain the layers in more detail:

+  **Inventory.Domain**: It must contain the domain entities and encapsulate their business logic. And should **not have dependencies** on other application layers.
+  **Inventory.Application**: This layer covers all business use cases, therefore it is responsible for aspects such as business use cases, business validations, business flows, etc. **Work only with abstractions**, delegating implementations to the infrastructure layer. Depends on Domain layer in order to use business entities and logic. We structure this layer in 3 main folders:
    -  Contracts: represent business requirements. Includes the interfaces and contracts for the application. This folder should cover application capabilities. This should include interfaces for abstracting use cases implementations. We separate contracts into subfolders based on functionality.
    -  Features: represents the business use cases. Includes the application use cases and features. This folder will apply CQRS design pattern for handling business use cases. It will contain a subfolder for each use case. Is the heart of this layer
    -  Behaviours: represents the business validations. Includes the business validations, logging and other crosscutting concerns that apply when performing the use case implementations. 
+  **Inventory.Infrastructure**: we perform database operations, email send operations, and all those related to external systems. This layer will include the implementations of the abstractions defined in the Application layer. Depends on Application layer in order to use core layers.
+  **Inventory.API**: this layer expose API to external microservices. Depends on Application (to perform operations in controlers) and Infrastructure


##  Inventory.API endpoints

![inventory_api](https://user-images.githubusercontent.com/3404380/174658196-cfe1cfc4-e4a8-4e71-b462-fa795742e53a.png)

+  **/api/v1/Inventory/GetProducts**: (GET) Obtains all the products of the inventory, paginated. It is also possible to filter by name.
+  **/api/v1/Inventory/AddProduct**: (POST) Adds a product to inventory (if not exists). Name, Reference and Type are required. NumUnits must be greater than 0.
+  **/api/v1/Inventory/RemoveProduct**: (DELETE) Remove a product from the inventory by name. Fire the event ProductRemovedEvent


##  Design patterns and best practices

+  CQRS
+  Dependency Inversion
+  Dependency Injection
+  Logging
+  Validation
+  Exception handling
+  Authentication
+  Repository
+  Unit of work
+  Testing
+  Implement with SOLID principles in mind


##  Third-party Nuget packages

+  **AutoMapper**: A convention-based object-object mapper. We use it to mapping operations between objects.
+  **AutoMapper.Extensions.Microsoft.DependencyInjection**: AutoMapper extensions for ASP.NET Core necessary to register AutoMapper in .NET Core dependency injection tool.
+  **Coravel**: Near-zero config .NET Core library that makes Task Scheduling, Caching, Queuing, Mailing, Event Broadcasting... We use it for the scheduled execution of tasks.
+  **FluentValidation**: A validation library for .NET that uses a fluent interface to construct strongly-typed validation rules. We ise it to perform validations when applying CQRS, before execute commands.
+  **FluentValidation.DependencyInjectionExtensions**: Dependency injection extensions for FluentValidation. We use it to being able to register FluentValidation in the NET Core dependency injection tool
+  **MassTransit**: MassTransit is a message-based distributed application framework for .NET. We use it in order to create queues in RabbitMQ, and thus, support asynchronous communication between microservices.
+  **MassTransit.RabbitMQ**: MassTransit RabbitMQ transport support. We use in order to connect to Rabbit MQ message broker.
+  **MediatR.Extensions.Microsoft.DependencyInjection**: MediatR extensions for ASP.NET Core. We use it in order to implement CQRS with Mediator pattern
+  **Moq**: Moq is the most popular and friendly mocking framework for .NET.
+  **Moq.AutoMock**: An auto-mocking container that generates mocks using Moq
+  **Swashbuckle.AspNetCore**: Swagger tools for documenting APIs built on ASP.NET Core
+  **xunit**: xUnit.net is a developer testing framework, built to support Test Driven Development, with a design goal of extreme simplicity and alignment with framework features.


##  Assumptions

+  We use an in-memory database for simplicity
+  We only implement unit tests for the use case of adding product to inventory, to illustrate examples in the 3 layers of Clean Architecture


##  Possible improvements

+  Implement resilience mechanisms, such as retries, using for example the Polly library, in those use cases that require it
+  Apply CQRS to handle domain events, just as we are doing to handle use cases
+  Create more tests to achieve full code coverage
+  Implement sorting in GET query
+  Improving performance in event publishing using Parallel.ForEach
+  Implement concurrency control mechanisms in EF Core
