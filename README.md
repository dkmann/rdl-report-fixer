# Report fixer for SQL Server Reporting Services

Reads an SQL Server Reporting Services report file (RDL) and makes automated changes in a command line based environment.

## Producing a working build

The project is in a very early stage at writing. To build clone the repository.

```bash
$ git clone https://github.com/dkmann/rdl-report-fixer.git
```

Run .NET `restore` to install package dependencies.

```bash
$ cd rdl-report-fixer
$ dotnet restore
```

Finally, to run the application enter the command `run` to build and execute the project.

```bash
$ dotnet run
```
