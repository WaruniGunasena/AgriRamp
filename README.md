# AgriRamp - AgriState MiniTracker

AgriRamp (AgriState MiniTracker) is a full-stack web application designed for **agricultural state and inventory tracking**. The system follows a modern decoupled architecture with a **.NET 8 Web API backend**, **React/Vite frontend**, **Microsoft SQL Server**, and **containerized deployment using Docker**.

The project also includes a **GitHub Actions CI/CD pipeline** for automated Docker image building and deployment.

---

## Architecture & Project Structure

The repository is organized as a **monorepo**, containing the backend, frontend, Docker configuration, and CI/CD workflow in a single repository.

```text
AgriRamp/
│
├── AgriState_MiniTracker_backend/
│   ├── AgriState_MiniTracker/
│   │   └── Application source code & .csproj
│   ├── Dockerfile
│   └── AgriState_MiniTracker.sln
│
├── AgriState_MiniTracker_frontend/
│   ├── src/
│   ├── package.json
│   └── Dockerfile
│
├── .github/
│   └── workflows/
│       └── deploy.yml
│
├── docker-compose.yml
│
└── README.md
```

### Project Components

| Component                        | Description                                            |
| -------------------------------- | ------------------------------------------------------ |
| `AgriState_MiniTracker_backend`  | .NET 8 Web API backend                                 |
| `AgriState_MiniTracker_frontend` | React/Vite frontend application                        |
| `docker-compose.yml`             | Orchestrates SQL Server, backend, and frontend         |
| `Dockerfile`                     | Container build configuration for backend and frontend |
| `.github/workflows/deploy.yml`   | GitHub Actions CI/CD pipeline                          |

---

## Tech Stack

### Backend

* **.NET 8**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **JWT Authentication**
* C#

### Frontend

* **React**
* **Vite**
* JavaScript 
* HTML5 & CSS3

### Database

* **Microsoft SQL Server 2022**

### DevOps & Deployment

* **Docker**
* **Docker Compose**
* **GitHub Actions**
* **Docker Hub**

---

# Getting Started

## Prerequisites

Make sure the following tools are installed on your machine:

* [Git](https://git-scm.com/)
* [.NET 8 SDK](https://dotnet.microsoft.com/)
* [Node.js v20+](https://nodejs.org/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

---

# Local Development Setup

## 1. Clone the Repository

Clone the repository and navigate into the project directory:

```bash
git clone https://github.com/YOUR_GITHUB_USERNAME/AgriRamp.git
cd AgriRamp
```

> Replace `YOUR_GITHUB_USERNAME` with your GitHub username.

---

## 2. Run with Docker Compose

The recommended way to run the complete application locally is using Docker Compose.

Run:

```bash
docker compose up -d --build
```

This will start the complete application stack, including:

* SQL Server
* .NET 8 Backend API
* React/Vite Frontend

### Application URLs

| Service                      | URL                             |
| ---------------------------- | ------------------------------- |
|  Frontend                  | `http://localhost:3000`         |
|  Backend API               | `http://localhost:5208`         |
|  Swagger API Documentation | `http://localhost:5208/swagger` |

To stop the containers:

```bash
docker compose down
```

To stop the containers and remove associated volumes:

```bash
docker compose down -v
```

---

#  3. Running Manually Without Docker

If you prefer to run the backend and frontend directly on your development machine, follow the steps below.

## Database Setup

Make sure **Microsoft SQL Server** is running locally or through Docker.

Make sure the database connection string in the backend configuration matches your SQL Server environment.

---

## Backend Setup

Navigate to the backend project:

```bash
cd AgriState_MiniTracker_backend/AgriState_MiniTracker/AgriState_MiniTracker
```

Restore the required .NET packages:

```bash
dotnet restore
```

Apply Entity Framework Core migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

The backend API will be available at:

```text
http://localhost:5208
```

Swagger documentation:

```text
http://localhost:5208/swagger
```

---

## Frontend Setup

Open a new terminal and navigate to the frontend directory:

```bash
cd AgriState_MiniTracker_frontend
```

Install dependencies:

```bash
npm install
```

Start the Vite development server:

```bash
npm run dev
```

The frontend will then be available through the URL displayed by Vite.

---

# CI/CD Pipeline

AgriRamp uses **GitHub Actions** to automate the Continuous Integration and Continuous Deployment process.

The workflow is located at:

```text
.github/workflows/deploy.yml
```

## Workflow Process

Whenever code is pushed to the `main` branch, the CI/CD pipeline automatically performs the following operations.

### Backend Job

1. Checks out the repository.
2. Sets up the .NET environment.
3. Restores .NET dependencies.
4. Builds the backend application.
5. Builds the backend Docker image.
6. Authenticates with Docker Hub.
7. Pushes the Docker image to Docker Hub.

### Frontend Job

1. Checks out the repository.
2. Sets up Node.js.
3. Installs frontend dependencies.
4. Builds the Vite application.
5. Builds the Nginx Docker image.
6. Authenticates with Docker Hub.
7. Pushes the Docker image to Docker Hub.

---

# GitHub Actions Secrets

To allow GitHub Actions to authenticate with Docker Hub, configure the following repository secrets.

Navigate to:

```text
GitHub Repository
→ Settings
→ Secrets and variables
→ Actions
```

Add the following secrets:

| Secret               | Description                                                  |
| -------------------- | ------------------------------------------------------------ |
| `DOCKERHUB_USERNAME` | Your Docker Hub username                                     |
| `DOCKERHUB_TOKEN`    | Docker Hub Personal Access Token with Read/Write permissions |

### Example

```text
DOCKERHUB_USERNAME = your-dockerhub-username
DOCKERHUB_TOKEN    = your-dockerhub-access-token
```

> **Important:** Never commit Docker Hub credentials, access tokens, passwords, or other sensitive information directly to the repository.

---

# Docker Architecture

The application is containerized into separate services.

```text
                  ┌─────────────────────┐
                  │       Browser       │
                  └──────────┬──────────┘
                             │
                             ▼
                  ┌─────────────────────┐
                  │     React           │
                  │    Frontend         │
                  │   Port: 3000        │
                  └──────────┬──────────┘
                             │
                             ▼
                  ┌─────────────────────┐
                  │    .NET 8 Web API   │
                  │     Backend         │
                  │   Port: 5208       │
                  └──────────┬──────────┘
                             │
                             ▼
                  ┌─────────────────────┐
                  │   SQL Server 2022   │
                  │     Database        │
                  │     Port: 1433      │
                  └─────────────────────┘
```

Docker Compose is responsible for orchestrating the services and establishing communication between the containers.

---

# API Documentation

The backend provides interactive API documentation through **Swagger/OpenAPI**.

Once the backend is running, access:

```text
http://localhost:5208/swagger
```

Swagger can be used to:

* Explore available API endpoints
* View request and response models
* Test API endpoints
* Review authentication requirements

---

# Authentication

The application uses **JWT (JSON Web Token) authentication** to secure protected API endpoints.

The general authentication flow is:

```text
User
  │
  ▼
Login
  │
  ▼
.NET Web API
  │
  ▼
JWT Token
  │
  ▼
Frontend
  │
  ▼
Authenticated API Requests
```

---

# Development

Before pushing changes to the repository, it is recommended to verify both applications locally.

### Backend

```bash
dotnet restore
dotnet build
dotnet run
```

### Frontend

```bash
npm install
npm run dev
```

### Docker

```bash
docker compose up -d --build
```

---

# Repository Structure

```text
AgriRamp
│
├── 📁 AgriState_MiniTracker_backend
│   ├── 📁 AgriState_MiniTracker
│   ├── 🐳 Dockerfile
│   └── 📄 AgriState_MiniTracker.sln
│
├── 📁 AgriState_MiniTracker_frontend
│   ├── 📁 src
│   ├── 📄 package.json
│   └── 🐳 Dockerfile
│
├── 📁 .github
│   └── 📁 workflows
│       └── ⚙️ deploy.yml
│
├── 🐳 docker-compose.yml
│
└── 📄 README.md
```

---

# Deployment

The application is designed to support containerized deployment.

The recommended deployment workflow is:

```text
Developer
    │
    │ git push
    ▼
GitHub Repository
    │
    ▼
GitHub Actions
    │
    ├───────────────┐
    ▼               ▼
Backend Build   Frontend Build
    │               │
    ▼               ▼
Docker Image    Docker Image
    │               │
    └───────┬───────┘
            ▼
        Docker Hub
```

This approach provides a repeatable and automated deployment process for both the backend and frontend applications.

---

Full-stack agricultural state and inventory tracking application built with **.NET 8, React, SQL Server, Docker, and GitHub Actions**.
