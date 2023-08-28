# Clean Architecture - Car Store - REST API 

This application was created to manage a car store, It's an application using .NET 6, Docker, and Docker-Composer

## Install

To run the application first is necessary to run the docker-compose command using a prompt command.

```bash
docker compose up -d
```

Each time the application is started, a standard user and some cars are created so that it is possible to run integration tests.

## Run the app

You can run the application through Visual Studio and the URL created by Docker

Docker application endpoint:

`http://localhost:8090/swagger/`

After execution it will be possible to use the application using `Swagger`

## Run the tests

The project contains `unit tests` to test all use cases, data access layer and `integrated tests` to test routes

```bash
dotnet test
```

# Architecture

This application is built with clean architecture and .NET 6.

The app was built using the car domain as a base. There are a few use cases for managing each flow of CRUD operations.

The application has a database resiliency system using Polly and uses SQL Server and Dapper to communicate with the database.

It is possible to run and test the application both through Visual Studio, and Docker and through integrated tests.

The application was created in a short time, so there are some improvements that can be made such as improving the domain, creating a UI using React, and improving the authentication system.

# REST API

Using application routes

## Login
`POST /api/login`

Route responsible for creating the access token

`curl -X 'POST' \
  'http://localhost:8090/api/Login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "userName": "admin",
  "password": "123456"
}'`

## Create New Login
`POST /api/login/create`

Route responsible for creating a new login

`curl -X 'POST' \
  'http://localhost:8090/api/Login/Create' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "userName": "renato",
  "password": "123456",
  "roles": "create"
}'`

## Manage Login Permissions
`PATCH /api/login/{id}`

Route responsible for manage login permissions

`curl -X 'PATCH' \
  'http://localhost:8090/api/Login/2' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "roles": "create;delete"
}'`

## Add New Car
`POST /api/car`

Add a new car


`curl -X 'POST' \
  'http://localhost:8090/api/Car' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer xxx' \
  -H 'Content-Type: application/json' \
  -d '{
  "brand": "Fiat",
  "model": "Toro",
  "year": 2022
}'`

## Search Car
`GET /api/car/{term}`

Searches for a car

`curl -X 'GET' \
  'http://localhost:8090/api/Car/civic' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer xxx'`
  
## Delete Car
`DELETE /api/car/{id}`

Delete a car by the id


`curl -X 'DELETE' \
  'http://localhost:8090/api/Car/3' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer xxx'`
  
  
## Update Car Data
`PATCH /api/car/{id}`

Update car data. Update only populated properties

`curl -X 'PATCH' \
  'http://localhost:8090/api/Car/5' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer xxx' \
  -d '{
  "year": 2020
}'`
