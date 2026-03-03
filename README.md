# KiwiTracker Backend API

> A goal tracking REST API built with ASP.NET Core and PostgreSQL.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?logo=postgresql)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

---

## Live Demo

**API Base URL:** `https://kiwitracker-production.up.railway.app/index.html`  

---

## Features

- ✅ **User Authentication** — JWT-based auth with registration and login
- ✅ **Goal Management** — Create, read, update, delete personal goals
- ✅ **RESTful API** — Clean endpoints following REST principles
- ✅ **Secure** — Password hashing, JWT tokens, CORS configured
- ✅ **Database Migrations** — EF Core with PostgreSQL

---

## Tech Stack

**Backend:**
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- PostgreSQL 16
- JWT Authentication

**Tools:**
- Visual Studio 2022
- Postman (API testing)
- Git/GitHub

**Deployment:**
- Railway (backend + PostgreSQL)

---

## Screenshots

### Swagger API Documentation
![Swagger UI](screenshots/swagger-ui.png)

### Example Response
```json
{
  "id": 1,
  "title": "Learn React",
  "description": "Build 3 projects with React",
  "isCompleted": false,
  "createdAt": "2026-02-01T10:00:00Z"
}
🚦 Getting Started
Prerequisites
.NET 8.0 SDK

PostgreSQL 16

Git

Local Setup
Clone the repository

bash
git clone https://github.com/awsedrig/KiwiTracker.git
cd KiwiTracker
Configure database

Update appsettings.Development.json:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=kiwitracker;Username=postgres;Password=YOUR_PASSWORD"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_MINIMUM_32_CHARACTERS",
    "Issuer": "KiwiTrackerAPI",
    "Audience": "KiwiTrackerClient"
  }
}
Run migrations

bash
dotnet ef database update
Start the server

bash
dotnet run
Open Swagger UI

text
https://localhost:5001/swagger
📡 API Endpoints
Authentication
Method	Endpoint	Description
POST	/api/auth/register	Register new user
POST	/api/auth/login	Login and get JWT token
Goals
Method	Endpoint	Description	Auth Required
GET	/api/goals	Get all user goals	✅
GET	/api/goals/{id}	Get goal by ID	✅
POST	/api/goals	Create new goal	✅
PUT	/api/goals/{id}	Update goal	✅
DELETE	/api/goals/{id}	Delete goal	✅
Full API documentation: Available in Swagger UI after running the app

 Roadmap
 Add frontend (React)

 Implement goal categories

 Add deadline reminders

 User profile management

 Export goals to PDF

 Docker containerization

 What I Learned
Building this project taught me:

RESTful API design principles

JWT authentication implementation

Entity Framework Core migrations

PostgreSQL database management

Deployment to cloud platforms (Render.com)

CORS configuration for frontend integration

- Connect With Me
  Email: semagaev99@mail.ru
  GitHub: @awsedrig

 License
This project is open source and available under the MIT License.

 If you like this project, please give it a star!
