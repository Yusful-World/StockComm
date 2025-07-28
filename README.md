#  StockComm API

**StockComm** is a comment-enabled stock tracking API that allows users to monitor various stocks, comment on them, and manage personalized portfolios. It features role-based authentication and data protection to ensure a secure and personalized user experience

## Features
- **User Authentication** (Sign-In & Login)
- **CRUD Operations for Stocks**
- **Comment System** for Stocks
- **User Portfolio** with one-to-many stock mapping
- **Role-Based Access Control**
- **Data Protection & Validation**
- **Repository Pattern** used for clean architecture

## Technologies Used
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server (MSSQL)**
- **Redis (for caching or future use)**

## Project Structure
🗁️ Controllers│   
├ AccountController.cs       # Handles Sign-In & Login
│   ├ StockController.cs         # Handles Stock CRUD
│   ├ CommentsController.cs      # Handles Comment CRUD
│   └ UserPortfolioController.cs # Manages User Portfolios
📁️ Repositories                   # Implements Repository Pattern
📁️ DTOs                           # Data Transfer Objects
📁️ Models                         # Database Models
📁️ Helpers                        
...

## Getting Started

Follow these steps to run the project locally.

### 1. Clone the Repository

```bash
git clone https://github.com/Yusful-World/StockComm.git
cd StockComm
```

### 2. Install Dependencies

Make sure you have the [.NET SDK](https://dotnet.microsoft.com/en-us/download) and [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) installed.

```bash
dotnet restore
```

### 3. Set Up the Database

Update your `appsettings.json` with your **SQL Server connection string**:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=StockCommDb;Trusted_Connection=True;"
}
```

Apply migrations (if applicable):

```bash
dotnet ef database update
```

### 4. Run the App

```bash
dotnet run
```

The API will be available at `https://localhost:7298` or `http://localhost:5098`.

## API Endpoints Overview

| Endpoint                      | Method | Description                      |
|------------------------------|--------|----------------------------------|
| `/api/account`               | POST   | User Sign-In & Login             |
| `/api/stocks`                | GET/POST/PUT/DELETE | Stock CRUD         |
| `/api/comments`              | GET/POST/PUT/DELETE | Comment CRUD       |
| `/api/userportfolio`         | GET/POST/DELETE      | Manage user portfolio |

## Tools & Utilities

- Swagger UI is available at `/swagger` when the app is running.
- Uses `Repository Pattern` for clean separation of concerns.
- Built with scalability and maintainability in mind.
