# [sample-lab-management][repo]

sample dotnet rest api

## Environment

- [Fedora 41][fedora]
- [,Net Core 9.0][dotnet]

## Initial setup

Install dotnet sdk on fedora:

```bash
sudo dnf install dotnet-sdk-9.0
```

Use [dotnet cli][cli] to scaffold a minimal web project:

```bash
dotnet new web -o sample-lab-management
```

Add dotnet tools in the path:

```bash
echo 'export PATH=$HOME/.dotnet/tools:$PATH' >> ~/.bashrc
source ~/.bashrc
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

-

[repo]: https://github.com/sombriks/sample-lab-management
[fedora]: https://fedoraproject.org/
[dotnet]: https://dotnet.microsoft.com/en-us/download
[cli]: https://learn.microsoft.com/pt-br/dotnet/core/tools/dotnet-new#synopsis
