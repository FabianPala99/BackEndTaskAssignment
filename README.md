
# BackEnd Task Assignment Management System

A full-stack **Task Assignment Management** web application built using **.NET 8** for the backend and **React.js** for the frontend. The backend system supports user authentication and authorization, with roles such as Administrator, Supervisor, and Employee.

## You can check the live system here:
- **Live URL**: [Task Assignment System](https://lively-sea-0c48f3010.5.azurestaticapps.net/login)

### Test Users:
- **Administrator**:
  - Email: `admin@gmail.com`
  - Password: `admin`

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Setup Instructions](#setup-instructions)
4. [Deployment on Azure](#deployment-on-azure)
5. [Postman Collection](#postman-collection)

## Features

- **Authentication**: JWT-based authentication and authorization.
- **User Roles**:
  - **Administrator**: Manage users, assign roles, create, edit, delete tasks.
  - **Supervisor**: Assign tasks to employees, track task progress.
  - **Employee**: View and update assigned tasks.
- **Task Management**: Create, read, update, and delete tasks, assign tasks to users, and track progress.
- **Role-Based Access**: Secured access with restricted routes based on user roles.
- **Responsive API**: Fast and reliable API responses.
- **Error Handling**: User-friendly error messages for various CRUD operations.

## Technologies Used

- **Backend**:
  - .NET 8.0
  - Entity Framework Core
  - SQL Server
  - JWT Authentication
  - REST API using ASP.NET Core
- **Frontend**: [Frontend repository](https://github.com/FabianPala99/FrontEndTaskAssignment)
- **Miscellaneous**:
  - Azure (for hosting and deployment)
  - Postman Collection for API testing

## Setup Instructions

### Prerequisites

Ensure you have the following tools installed on your machine:
- **.NET 8.0 SDK**
- **SQL Server** (or any SQL database)

### Step-by-step guide

1. **Clone the repository:**
   ```bash
   git clone https://github.com/FabianPala99/BackEndTaskAssignment.git
   ```

2. **Navigate to the project folder:**
   ```bash
   cd BackEndTaskAssignment
   ```

3. **Configure the database:**
   Update the `appsettings.json` file with your SQL Server connection string.

4. **Run migrations to create the database schema:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

6. **API Endpoints and Documentation:**
   The Postman collection for testing the API is available in the `collection` folder.

## Deployment on Azure

This project has been deployed on **Azure** for scalability and reliability. The backend is hosted using **Azure App Service** and uses **Azure SQL Database**.

### Steps to deploy on Azure

1. **Azure App Service**: Use the `dotnet publish` command to create a release build and deploy it to an Azure App Service.

2. **Azure SQL Database**: The SQL database schema and data can be migrated to Azure SQL using the `ScriptDB.sql` file found in the `DB` folder.

3. **Continuous Deployment**: Set up GitHub Actions or Azure Pipelines for automated deployment.

## Database

The `DB` folder contains the following:

- **Model**: The database schema model.
- **ScriptDB.sql**: SQL script for creating tables and relationships.

## Postman Collection

In the `collection` folder, you will find a **Postman collection** (`TaskAssignmentCollection.json`) that includes all the necessary endpoints for testing the API.

1. Import the collection into Postman to quickly start testing the API.
2. The collection includes endpoints for:
   - User login
   - CRUD operations for users and tasks
   - Role-based access management
3. Ensure that you set the proper authorization token in the Postman environment after logging in.

