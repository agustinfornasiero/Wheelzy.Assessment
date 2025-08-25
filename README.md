# Wheelzy.Assessment
This project implements the technical challenge for Wheelzy.  
It was developed using:
 **.NET 8 Minimal API**, applying **CQRS + Handlers**, **Entity Framework Core**, **SQL Server**, and **caching (Memory + Redis)**.

 - **.NET 8 Minimal API** → lightweight and modern way to expose endpoints.  
- **Entity Framework Core 8** → ORM for database access.  
- **SQL Server** → relational database with indexes, constraints, and triggers.  
- **CQRS (Command Query Responsibility Segregation)** → separates read and write responsibilities.  
- **Custom Handlers** for commands/queries organization.  
- **Redis (StackExchange.Redis)** → distributed cache for shared catalogs.  
- **MemoryCache** → for small, frequently used lookups.  
- **xUnit + Moq** → unit testing.  

### 1. Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Docker with SQL Server
- [Redis](https://redis.io/) (local or Docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### 2. Clone Repository
```bash 
git clone https://github.com/agustinfornasiero/Wheelzy.Assessment.git


