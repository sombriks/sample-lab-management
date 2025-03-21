# [sample-lab-management][repo]

sample dotnet rest api

## Environment

- [Fedora 41][fedora]
- [,Net Core 8.0][dotnet]

## Initial setup

Install dotnet sdks on fedora:

```bash
sudo dnf install dotnet-sdk-9.0
sudo dnf install dotnet-sdk-8.0
```

Use [dotnet cli][cli] to scaffold a minimal web project:

```bash
dotnet new web -o sample-lab-management

cd sample-lab-management

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

mkdir -p App/{Controllers,Models}

touch App/Main.cs
touch App/Models/Student.cs
touch App/Models/StudentContext.cs
```

Install [code generation tools][codegen]:

```bash
dotnet tool uninstall -g dotnet-aspnet-codegenerator
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator
```

Add dotnet tools in the path:

```bash
echo 'export PATH=$HOME/.dotnet/tools:$PATH' >> ~/.bashrc
source ~/.bashrc
```

### Controller scaffolding

In order to create a controller class, use thd sample bellow:

```bash
dotnet aspnet-codegenerator controller \
 -name StudentController -async -api \
 -m Student -dc StudentContext \
 -outDir App/Controllers
```

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

```

## Noteworthy

- By the time of this writing, controller generation fails on sdk 9.

[repo]: https://github.com/sombriks/sample-lab-management
[fedora]: https://fedoraproject.org/
[dotnet]: https://dotnet.microsoft.com/en-us/download
[cli]: https://learn.microsoft.com/pt-br/dotnet/core/tools/dotnet-new#synopsis
[codegen]: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio-code#scaffold-a-controller
