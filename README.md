# [sample-lab-management][repo]

Sample dotnet rest api.

## Environment

- [Fedora 41][fedora]
- [,Net Core 8.0][dotnet]

As preferred IDE, either use [visual studio code][code] or
[jetbrains rider][rider].

## Initial setup

Install dotnet sdks on fedora:

```bash
sudo dnf install dotnet-sdk-9.0
sudo dnf install dotnet-sdk-8.0
```

Install [code generation tools][codegen]:

```bash
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool install -g dotnet-ef
```

Add dotnet tools in the path:

```bash
echo 'export PATH=$HOME/.dotnet/tools:$PATH' >> ~/.bashrc
source ~/.bashrc
```

### How this project was scaffolded

Use [dotnet cli][cli] to scaffold a minimal web project:

```bash
dotnet new web -o sample-lab-management

cd sample-lab-management

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

mkdir -p App/{Controllers,Models}

touch App/Main.cs
touch App/Models/Student.cs
touch App/Models/Laboratory.cs
touch App/Models/Projectz.cs
touch App/Models/ModelsContext.cs
```

### Controller scaffolding

In order to create a controller class, use thd sample bellow:

```bash
dotnet aspnet-codegenerator controller \
 -name ProjectController -async -api \
 -m Project -dc ModelsContext \
 -outDir App/Controllers
```

### Migration scaffolding

To create database migrations:

```bash
 dotnet ef migrations add InitialSchema
```

Where `InitialSchema` is the migration name.

Those migrations are generated based on models.

## How to build

```bash
dotnet build
```

## How to run

```bash
dotnet run
```

## How to test

```bash
# under construction
```

There is also a [/swagger][swagger] endpoint to offer an easy way to try the
endpoints.

## Noteworthy

- By the time of this writing, controller generation fails on sdk 9.
- There is a simpler "minimal web api" besides the controller based one, but for
  the sake of the exercise we went with controller style.

## Database schema

```mermaaid
classDiagram
direction BT
class Laboratories {
   text Name
   integer Id
}
class LaboratoryStudent {
   integer LaboratoriesId
   integer StudentsId
}
class ProjectStudent {
   integer ProjectsId
   integer StudentsId
}
class Projects {
   integer LaboratoryId
   text Name
   integer Id
}
class Students {
   text Name
   integer Id
}

LaboratoryStudent  -->  Laboratories : LaboratoriesId:Id
LaboratoryStudent  -->  Students : StudentsId:Id
ProjectStudent  -->  Projects : ProjectsId:Id
ProjectStudent  -->  Students : StudentsId:Id
Projects  -->  Laboratories : LaboratoryId:Id

```

[repo]: https://github.com/sombriks/sample-lab-management
[fedora]: https://fedoraproject.org/
[dotnet]: https://dotnet.microsoft.com/en-us/download
[cli]: https://learn.microsoft.com/pt-br/dotnet/core/tools/dotnet-new#synopsis
[codegen]: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio-code#scaffold-a-controller
[swagger]: https://learn.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-8.0&tabs=net-cli#add-and-configure-swagger-middleware
[code]: https://code.visualstudio.com/
[rider]: https://www.jetbrains.com/rider/
