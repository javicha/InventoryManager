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
+ Run the following Docker command to stop all the containers: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down**

## Not implemented requirements
+ A frontend has not been implemented to consume the API. All interactions must be done through Swagger (or similar)

## Solution architecture

## API architecture

## Design patterns and best practices

## Third-party Nuget packages

## Possible improvements
