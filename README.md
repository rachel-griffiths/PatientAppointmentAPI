# PANDA – Patient Appointment Network Data Application (Backend)

## Overview

This project provides a .NET HTTP API for managing patient demographic data and associated appointments as part of the PANDA MVP. The focus is on delivering a working, secure, and extensible backend while keeping the implementation lightweight and easy to evolve.

---

## Running the application

- The **API project** is the single startup project
- Run locally using Visual Studio with the API project selected
- The application starts using a SQL Server database as configured in `appsettings.json`

In production, the API could be hosted on a physical server or as a cloud-hosted web application.

---

## Architecture

The solution is split into the following projects:

- **API** – Controllers, middleware, localisation, startup configuration  
- **Core** – Services, DTOs, validation, repository interfaces, error codes  
- **Domain** – Business entities and rules  
- **Infrastructure** – EF Core DbContext and repository implementations  

Dependencies flow inward only, keeping persistence and hosting concerns isolated from business logic.

---

## Persistence and database choice

Entity Framework Core is used for persistence.

- The current configuration uses **SQL Server**
- EF Core and the use of Repository interfaces allows the database provider to be swapped with minimal change
- This satisfies the persistence requirement while keeping the system database-agnostic

---

## API behaviour

The API supports:

- CRUD operations for patients
- CRUD and search operations for appointments
- Appointment cancellation and status management
- JSON-based communication over HTTP

All timestamps use `DateTimeOffset` and are handled in UTC to ensure timezone awareness.

---

## Validation and business rules

- NHS numbers are checksum validated
- UK postcodes are normalised and validated
- Cancelled appointments cannot be modified
- Appointments are marked as missed when overdue and not attended

Validation is enforced server-side; the frontend is expected to surface API errors to users.

---

## Error handling and localisation

- Errors are handled centrally using middleware and returned as `ProblemDetails`
- Stable error codes are used to support tracing and analytics
- The design supports localisation via resource files and request culture, but full localisation is deferred for MVP

---

## Security

Authentication is intentionally not implemented, in line with the brief’s assumption of a trusted environment.  
In a real deployment, application-level authentication and authorisation would be strongly recommended.

---

## Trade-offs and future work

To meet the MVP scope and timebox, the following were intentionally deferred:

- Pagination on search endpoints
- Background processing for appointment status updates
- Clinician and organisation modelling
- Full localisation implementation
- Authentication and role-based access control
- Demographic patient data

Each can be added later without architectural changes.

---
