# Infonetica Task Workflow Engine

## Overview
This project is a simple workflow engine implemented in C# using ASP.NET Core. It allows you to define workflows, create instances, and execute actions that transition between states. The engine is designed for extensibility and clarity, making it suitable for learning, prototyping, or basic workflow automation.

## Features
- Define workflows with states and actions
- Enforce single initial state and unique state IDs
- Create workflow instances
- Execute actions to transition between states
- Track instance history (action and timestamp)
- RESTful API endpoints for all operations

## Project Structure
- `Program.cs`: Main entry point. Sets up the ASP.NET Core web API and maps endpoints for definitions, instances, and actions.
- `Services/EngineServices.cs`: Core logic for managing definitions, instances, and executing actions. Validates workflow rules and updates instance state/history.
- `Models/Definition.cs`: Represents a workflow definition, including states and actions.
- `Models/Instance.cs`: Represents a workflow instance, including current state and history.
- `Models/State.cs`: Represents a state in a workflow, with properties like `Id`, `IsInitial`, `IsFinal`, and `Enabled`.
- `Models/Action.cs`: Represents an action that can transition an instance from one state to another.

## How It Works
1. **Add a Definition**: POST `/definitions` with a workflow definition JSON.
2. **Get a Definition**: GET `/definitions/{id}` to retrieve a definition.
3. **Start an Instance**: POST `/instances/{defId}` to create a new instance of a workflow.
4. **Get an Instance**: GET `/instances/{id}` to retrieve an instance and its state/history.
5. **Execute an Action**: POST `/instances/{id}/actions/{actionId}` to perform an action and transition the instance state.

## API Endpoints
- `POST /definitions` — Add a new workflow definition
- `GET /definitions/{id}` — Get a workflow definition by ID
- `POST /instances/{defId}` — Start a new instance of a workflow
- `GET /instances/{id}` — Get an instance by ID
- `POST /instances/{id}/actions/{actionId}` — Execute an action on an instance

## Getting Started
1. **Build and Run**
   ```bash
   dotnet build
   dotnet run
   ```
2. **Test the API**
   Use tools like curl, Postman, or any HTTP client to interact with the endpoints.

## Example Definition JSON
```json
{
  "Id": "order",
  "States": [
    { "Id": "created", "IsInitial": true, "IsFinal": false, "Enabled": true },
    { "Id": "shipped", "IsInitial": false, "IsFinal": false, "Enabled": true },
    { "Id": "delivered", "IsInitial": false, "IsFinal": true, "Enabled": true }
  ],
  "Actions": [
    { "Id": "ship", "FromStates": ["created"], "ToState": "shipped", "Enabled": true },
    { "Id": "deliver", "FromStates": ["shipped"], "ToState": "delivered", "Enabled": true }
  ]
}
```

## Requirements
- .NET 8.0 SDK
- ASP.NET Core

## Notes
- All data is stored in-memory; restarting the app will clear all definitions and instances.
- Designed for demonstration and prototyping purposes.

