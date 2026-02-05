# KiwiTracker Backend API

> A goal tracking REST API built with ASP.NET Core and PostgreSQL. Part of my journey to New Zealand 🇳🇿

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?logo=postgresql)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

---

## Live Demo

**API Base URL:** `https://kiwitracker-backend.onrender.com`  
**Swagger UI:** `https://kiwitracker-backend.onrender.com/swagger`

>  **Note:** Free tier may take 30-60 seconds for first request (server wakes up)

---

## Features

- ✅ **User Authentication** — JWT-based auth with registration and login
- ✅ **Goal Management** — Create, read, update, delete personal goals
- ✅ **Favorite Cities** — Track cities you want to visit
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
- Render.com (backend + database)

---

## Screenshots

### Swagger API Documentation
![Swagger UI]([[[https://github.com/awsedrig/KiwiTracker/issues/1](https://github.com/awsedrig/KiwiTracker/issues/1#issue-3900451005)](https://private-user-images.githubusercontent.com/226268062/545466371-0eac7db7-5a91-4728-8684-690c907ab99c.png?jwt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NzAyODI5NjAsIm5iZiI6MTc3MDI4MjY2MCwicGF0aCI6Ii8yMjYyNjgwNjIvNTQ1NDY2MzcxLTBlYWM3ZGI3LTVhOTEtNDcyOC04Njg0LTY5MGM5MDdhYjk5Yy5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjYwMjA1JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI2MDIwNVQwOTExMDBaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT0yZWU1NGQzMjhmYzJhZmZhYjJlYWRmM2Y2ODMyM2VlNjc1MTdhODRlMGE1YTFiOTBlMTliYzdlZTI4MjdkNjQ4JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.7zxlBiEFO6hSFMf9imMNz05-dhQ5P8wzIsoD0GYtSjM)](https://private-user-images.githubusercontent.com/226268062/545466371-0eac7db7-5a91-4728-8684-690c907ab99c.png?jwt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NzAyODI5NDMsIm5iZiI6MTc3MDI4MjY0MywicGF0aCI6Ii8yMjYyNjgwNjIvNTQ1NDY2MzcxLTBlYWM3ZGI3LTVhOTEtNDcyOC04Njg0LTY5MGM5MDdhYjk5Yy5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjYwMjA1JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI2MDIwNVQwOTEwNDNaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT05ZDIyYjg1NDc4YzIzZDE3YzVkYzliMDc3MTlmYzdhYjg3YmUwZWUyMzhiMTdhZTJhZDM4NTZhZWZlZDUwYThjJlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.ZwaUW6JYb0MqDKBfRxFOek1RovLVthdjcsu1UCMU5ks))

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
git clone https://github.com/awsedrig/KiwiTracker
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
Favorite Cities
Method	Endpoint	Description	Auth Required
GET	/api/favoritecities	Get all favorites	✅
POST	/api/favoritecities	Add city	✅
DELETE	/api/favoritecities/{id}	Remove city	✅
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
