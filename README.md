# Machine Order Tracking System

This project is a machine order tracking system developed using .NET 6. It allows admins to manage orders and users, and regular users to track machines related to their operations.

## Technologies Used

- .NET 6
- Entity Framework Core
- AutoMapper
- ASP.NET Identity
- JWT Bearer Authentication
- PostgreSQL

## Installation

1. Clone the project:

    ```bash
    git clone <repo-url>
    ```

2. Set up the PostgreSQL database and update the connection string in the `appsettings.json` file.

3. Navigate to the project directory and install dependencies:

    ```bash
    dotnet restore
    ```

4. Apply migrations to create the database:

    ```bash
    dotnet ef database update
    ```

5. Run the project:

    ```bash
    dotnet run
    ```

## Usage

- Users with admin role:
  - View, create, update, and delete orders.
  - Manage users (create, update, delete).
  
- Users with regular role:
  - View machines related to their operations.
  - Mark machines as completed.


---

⚠️ This project is a work in progress and is still under development.
