# TodoList Application

A full-stack Todo application built with .NET 8 and Angular 19, implementing Clean Architecture principles and JWT authentication.

## Overview

This is my first Angular project, coming from a React background. While both frameworks share similar concepts (component-based architecture, Single Page Application), this project helped me explore Angular's unique features and ecosystem.

## Architecture

### Backend (.NET 8)

The backend follows Clean Architecture principles. While this might be considered over-engineered for a simple Todo app, it served as an excellent learning opportunity to understand proper separation of concerns and domain-driven design.

Structure:
```
TodoList/
├── TodoList.API         # API Controllers and Configuration
├── TodoList.Application # Use Cases and Business Logic
├── TodoList.Domain      # Core Business Entities
└── TodoList.Infrastructure # External Concerns (Database)
```

Key Features:
- Clean Architecture implementation
- JWT-based authentication
- User-specific TodoList CRUD operations
- Proper separation of concerns
- Domain-driven design principles

### Frontend (Angular 19)

The Angular frontend is organized using a feature-based file structure, making it easy to navigate and maintain as the application grows.

Structure:
```
src/
├── app/
│   ├── auth/     # Authentication related components
│   └── todo/     # Todo feature components
```

Key Features:
- Feature-based organization
- Local session storage for auth persistence
- Global state management
- Angular Material UI components
- RxJS for reactive state management
- Route Guards for protected routes

## Technical Stack

Backend:
- .NET 8
- Entity Framework Core
- JWT Authentication
- Clean Architecture

Frontend:
- Angular 19
- Angular Material
- RxJS
- TypeScript
- Local Session Storage

## Development Setup

1. Clone the repository
2. Configure the backend:
   ```bash
   cd TodoList.API
   dotnet restore
   dotnet run
   ```

3. Configure the frontend:
   ```bash
   cd TodoList.Client
   npm install
   ng serve
   ```

## Environment Configuration

Environment files are located in:
- Backend: `.env` file
- Frontend: `environment.ts`


## Learning Notes

This project represents my first venture into Angular development, coming from a React background. Some key learnings:

- While React uses JSX, Angular uses its own template syntax
- Angular's dependency injection system differs from React's context
- RxJS provides powerful reactive programming capabilities
- Angular Material offers a comprehensive UI component library
- TypeScript is more deeply integrated into Angular compared to React

## Areas for Improvement

- Implement more comprehensive error handling
- Add tests
- Add more Validation to the user both frontend/backend
- Consider implementing real-time updates using SignalR
- Enhance the UI/UX design
- Add pagination for todo lists
- Implement caching strategies
