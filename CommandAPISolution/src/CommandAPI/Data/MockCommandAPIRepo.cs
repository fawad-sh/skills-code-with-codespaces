using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data;

public class MockCommandAPIRepo : ICommandAPIRepo
{
    public bool SaveChanges()
    {
        return false;
    }
    
    public void CreateCommand(Command cmd)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateCommand(Command cmd)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteCommand(Command  cmd)
    {
        throw new System.NotImplementedException();
    }

    public Command GetCommandById(int id)
    {
        return new Command {
            Id=0, HowTo="How to generate a migration",
            Line="dotnet ef migrations add <Name of Migration>",
            Platform=".Net Core EF"
        };
    }

    public IEnumerable<Command> GetAllCommands()
    {
        var commands = new List<Command>
        {
            new Command{
            Id=0, HowTo="How to generate a migration",
            Line="dotnet ef migrations add <Name of Migration>",
            Platform=".Net Core EF"},
            new Command{
            Id=1, HowTo="Run Migrations",
            Line="dotnet ef database update",
            Platform=".Net Core EF"},
            new Command{
            Id=2, HowTo="List active migrations",
            Line="dotnet ef migrations list",
            Platform=".Net Core EF"}  
        };
        return commands;
    }
}