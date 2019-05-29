# .NET core template

This is a template created for .net core, it contains an API, Configuration, Models, Services & Tests. The Api is setup with DI and basic auth, it also has unhandled exception middleware to catch any errors and return a 500 status code

## How to get it

The package can be installed with:
```
dotnet new -i jertje260.Core.Project.Template
```
If you have multiple nuget sources, use: `--nuget-source https://api.nuget.org/v3/index.json` behind it.


and then the template can be created with:
```
dotnet new dotnetProjectTemplate --name MyProject --output MyProject
```

It also has a `--framework` parameter to override the default framework.
Check `dotnet new dotnetProjectTemplate --help` for more info.

## Find more templates

More templates from nuget can be found [here](https://dotnetnew.azurewebsites.net/).