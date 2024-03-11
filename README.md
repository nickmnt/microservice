# Platform Command Microservices Application

## Overview
This repository contains the source code for a microservices-based application, consisting of two main services: `Platform Service` and `Command Service`. Both services are built using .NET (C#) and are designed to communicate with each other using both synchronous (HTTP, gRPC) and asynchronous (RabbitMQ) protocols.

## Solution Architecture

  <img src="https://github.com/nickmnt/microservice/blob/main/Images/KubernetesArchitecture.png" alt="Screenshot"></img>

The architecture above illustrates the service interactions and the communication flow between the `Platform Service` and the `Command Service`.

### Platform Service
The `Platform Service` is responsible for hosting and managing platform data. It provides functionality to add new platforms and manage existing ones. It communicates with the SQL Server database for data persistence and publishes messages to the RabbitMQ message bus when new platforms are added.

### Command Service
The `Command Service` hosts the commands for platforms and handles the business logic associated with these commands. It subscribes to the RabbitMQ message bus to listen for new platforms and uses an In-Memory database for caching and quick data retrieval.

## Communication

- **HTTP**: Utilized for REST API communication between services.
- **gRPC**: Employed for high-performance RPC calls, especially for internal service communication.
- **RabbitMQ Message Bus**: Used for asynchronous communication and to ensure loose coupling between services. The `Platform Service` publishes messages to the message bus, which are then consumed by the `Command Service`.

## Getting Started

To get started with this project, follow these steps:

1. Ensure you have .NET, Docker, Kubernetes installed on your machine.
2. Clone this repository to your local machine.
3. Like any other .NET, Kubernetes project, except, make sure to do these as well:

Add to hosts file:  
```shell
127.0.0.1 acme.com
```

Start up ingress:  
```shell
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.0/deploy/static/provider/cloud/deploy.yaml
```

Set up the password for production db:  
```shell
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
```

Start up the cluster:  
```shell
kubectl apply -f .
```  
  
Note: For PlatformService, since we are using in-memory database for development and SQL Server for production, we temporarily commented out the code for in-memory database and switched to a SQL Server localhost database to apply migrations using EF Core to generate the migration code and switched back again to in-memory database after the migration code (needed for the production db seed code) was generated!
