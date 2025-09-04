[Back to README](../README.md)

Execute project with visual studio is optional, just if you want to see and/or debug the code.

### 1: Run Docker Compose
The dockerfile is configured to up project and its dependencies running the docker-compose.
```bash
docker-compose up --build
```
The API will be available at:
```bash
http://localhost:8080
http://localhost:8080/swagger
```

### 2: Run with Visual Studio
For dev enviroment system have migrations to create the database in the first execute.

1. Open the solution Ambev.DeveloperEvaluation.sln in Visual Studio.
2. Ensure Ambev.DeveloperEvaluation.WebApi is set as the Startup Project.
3. Run the project using F5 (Debug) or Ctrl + F5 (Without Debugging).

### 3: Tests
The solution includes unit, integration, and functional tests.
To run all tests via CLI:
```
dotnet test
```
Or use the Test Explorer in Visual Studio.