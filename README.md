# Report fixer for SQL Server Reporting Services

Reads an SQL Server Reporting Services report file (RDL) and makes automated changes in a command line based environment. The project currently converts all cm units (the default in SSRS) to mm. All decimal values are rounded to integers for recommended practice in structure and layout of reports.

## Producing a working build

The project is in a very early stage at writing. To build clone the repository.

```bash
$ git clone https://github.com/dkmann/rdl-report-fixer.git
```

Move into the new project folder.

```bash
$ cd rdl-report-fixer
```

Run .NET `restore` to install package dependencies.

```bash
$ dotnet restore
```

Finally, to run the application enter the command `run` to build and execute the project.

```bash
$ dotnet run
```
