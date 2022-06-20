# InventoryManager (Goal Systems - Code challenge Backend Engineer)

## Intro

We start from a fictitious case to illustrate the operation of an inventory management system. Let's take as an example that we work for a genomic sequencing company, and we need to control the inventory of the consumable products used in the laboratory. We provide a REST API to query, add and remove products. 

## Requirements
+ Docker installed (the application runs in containers)

## How to run the application
+ Clone the repository
+ Open a terminal and navigate to InventoryManager/src
+ Launch the application by running the following Docker command: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d** (this command build and run all the required images)
+ Open a browser and navigate to http://localhost:7000/swagger/index.html to interact with the inventory API
+ It is a secure API. The following jwt token must be used to authenticate and be able to consume the API: *eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJHb2FsU3lzdGVtcyIsImlhdCI6MTY1NTMyMzM4NiwiZXhwIjoxNjg2ODU5Mzg2LCJhdWQiOiJpbnZlbnRvcnlfbWFuYWdlciIsInN1YiI6InVzZXIudGVzdCIsIlJvbGUiOiJBZG1pbiIsIkVudmlyb25tZW50IjoiU3RhZ2luZyJ9.Vgvf2WGnqAcfM72bxoMJWIKZbfmIGNz-RSFWOtbWRYE*

## How to stop the application
+ Run the following Docker command to stop all the containers: ****

## Not implemented requirements
+ A frontend has not been implemented to consume the API. All interactions must be done through Swagger (or similar)

## Solution architecture

## API architecture

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
