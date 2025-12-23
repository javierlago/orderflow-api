\# OrderFlow API



Backend API for B2B order and invoicing management.



This project is part of a professional portfolio focused on real-world backend development using .NET.



---



\## Features (current)

\- Health endpoint (`GET /health`)

\- Swagger documentation (Development)



---



\## Tech stack

\- .NET 8

\- ASP.NET Core Web API

\- SQL Server (planned)

\- Entity Framework Core (planned)

\- xUnit (planned)



---



\## Architecture



Clean Architecture (simplified):



\- \*\*OrderFlow.Api\*\*  

&nbsp; HTTP endpoints, middleware, dependency injection



\- \*\*OrderFlow.Application\*\*  

&nbsp; Use cases, DTOs, validation logic



\- \*\*OrderFlow.Domain\*\*  

&nbsp; Domain entities and business rules



\- \*\*OrderFlow.Infrastructure\*\*  

&nbsp; Data access and external integrations



---



\## Run locally



```bash

dotnet restore

dotnet run --project src/OrderFlow.Api



