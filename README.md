# CQRS & Mediatr Design Pattern Example
### The Tech Stack this project contains are:

 - Asp.Net Core 6 Web API
 - PostgreSQL
 - Redis
 - Entity Framework Core
 - Docker & Docker Compose
 - Elastic Stack (Elasticsearch & Kibana)
 - Serilog

If you want to work this project, follow this steps.

 1. Clone this repository in your local machine. Run this git code on your terminal. `git clone https://github.com/Filiphasan/MediatrExample.git`
 2. If you use VS Code, follow this steps
    - Go project directory in your terminal
    - Run `dotnet restore` for build your solution
    - Run `docker compose build` Build All Dependency (Redis, ELK etc.) with Docker.
    - Run `docker compose up -d` Run All Dependency
    - Run `dotnet run` for Run Dotnet Application
 3. If you use Visual Studio Just Right Click on Solution + Rebuild Solution and  Select docker-compose and Run it.
