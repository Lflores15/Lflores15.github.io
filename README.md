# Signin/Login Features Test in ASP.NET

This program is designed to test the signin/login features within an ASP.NET Core environment.

## Requirements

### Dotnet Packages

To install the required .NET packages, run the following commands in the terminal:

```bash
dotnet add package Microsoft.AspNetCore.Identity --version 2.2.0
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0
```

Running the App
Initialize Database
To set up the database, run the following commands:

Copy code
```bash 
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Booting the Website
To build and run the app, use one of the following commands:

To build the app:

Copy code
```bash
dotnet build
```
To run with live updates (watch for changes):

Copy code
```bash
dotnet watch run
```
To run without updates:

Copy code
```bash
dotnet run
```
