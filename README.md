# InventoryManager (Goal Systems - Code challenge Backend Engineer)

## Intro

We start from a fictitious case to illustrate the operation of an inventory management system. Let's take as an example that we work for a genomic sequencing company, and we need to control the inventory of the consumable products used in the laboratory.

## Requirements
+ Docker installed (the application runs in containers)

## How to run the application
+ Clone the repository
+ Open a terminal and navigate to InventoryManager/src
+ Launch the application by running the following Docker command: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d** (this command build and run all the required images)
+ Open a browser and navigate to http://localhost:7000/swagger/index.html to interact with the inventory API

## How to stop the application
+ Run the following Docker command to stop all the containers: **docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down**
