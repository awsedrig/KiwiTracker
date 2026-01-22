# KiwiTracker Backend API

REST API for personal goal and habit tracking system. Built with ASP.NET Core 8.0, PostgreSQL, and JWT authentication.

## Features
- User registration and authentication (JWT tokens)
- Create, read, update, delete goals
- Goal status tracking (NotStarted, InProgress, Completed, Abandoned)
- User-specific data isolation (users only see their own goals)

## Tech Stack
- **Framework:** ASP.NET Core 8.0
- **Database:** PostgreSQL 16
- **ORM:** Entity Framework Core
- **Authentication:** JWT Bearer tokens
- **Documentation:** Swagger/OpenAPI

## API Endpoints

### Authentication
```http
POST /api/auth/register - Register new user
POST /api/auth/login    - Login and receive JWT token
Goals (requires authentication)
text
GET    /api/goals      - Get all user's goals
GET    /api/goals/{id} - Get specific goal by ID
POST   /api/goals      - Create new goal
PUT    /api/goals/{id} - Update existing goal
DELETE /api/goals/{id} - Delete goal
Setup Instructions
Prerequisites
.NET 8 SDK

PostgreSQL 16+

Configuration
Update appsettings.json with your PostgreSQL connection string:

json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=kiwitracker;Username=postgres;Password=yourpassword"
  }
}
Database Migration
bash
dotnet ef database update
Run Application
bash
dotnet run
Access Swagger UI
Navigate to: https://localhost:5038/swagger

Authentication Flow
Register new user via POST /api/auth/register

Login via POST /api/auth/login → receive JWT token

Click 🔒 Authorize button in Swagger UI

Enter: Bearer <your_token>

Test authenticated endpoints

Database Schema
Users Table
Id (Primary Key, UUID)

Username (unique, required)

Email (unique, required)

PasswordHash (required)

CreatedAt (timestamp)

Goals Table
Id (Primary Key, UUID)

Title (required)

Description (optional)

Status (enum: NotStarted, InProgress, Completed, Abandoned)

CreatedAt (timestamp)

CompletedAt (nullable timestamp)

UserId (Foreign Key → Users)

Project Structure
text
KiwiTracker/
├── Controllers/     # API endpoints (AuthController, GoalsController)
├── Services/        # Business logic (AuthService, GoalService)
├── Models/          # Database entities (User, Goal)
├── DTOs/            # Data transfer objects (request/response models)
├── Data/            # DbContext and migrations
└── Program.cs       # Application configuration
Development Status
✅ User authentication (registration, login, JWT)

✅ Goals CRUD operations

✅ Swagger documentation with JWT support

⏳ Frontend (planned)

⏳ Habits tracking feature (planned)

Author
Built by Danil as part of ASP.NET Core learning journey.

Portfolio project demonstrating REST API design, authentication, and database integration.

License
MIT