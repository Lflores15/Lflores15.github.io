# Signin/Login Features Test in ASP.NET

This program is designed to test the signin/login features within an ASP.NET Core environment.

## Requirements

## Development
duplicate the appsettingsDevelopment.json changing the name to appsettings.json

### Dotnet Packages

To install the required .NET packages, run the following commands in the terminal:

```bash
dotnet add package Microsoft.AspNetCore.Identity
dotnet add package Microsoft.EntityFrameworkCore 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package System.ComponentModel.Annotations
dotnet add package NETStandard.Library
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
