using System.Collections.Generic;
using System.Linq;

using CommandAPI.Models;

namespace CommandAPI.Data;

public class SqlCommandAPIRepo : ICommandAPIRepo
{
    private readonly CommandContext _context;
    public SqlCommandAPIRepo(CommandContext context)
    {
        _context = context;
    }
    
    public bool SaveChanges()
    {
        return (_context.SaveChanges() >= 0);
    }

    public void CreateCommand(Command cmd)
    {
        if (cmd == null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }
        _context.CommandItems.Add(cmd);
    }

    public void UpdateCommand(Command cmd)
    {
        // nothing required, data update is handled in controller by entoty framework
    }

    public void DeleteCommand(Command  cmd)
    {
        if (cmd == null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }
        _context.CommandItems.Remove(cmd);
        
    }

    public Command GetCommandById(int id)
    {
        return _context.CommandItems.FirstOrDefault( p => p.Id == id);
    }

    public IEnumerable<Command> GetAllCommands()
    {
        var commands = _context.CommandItems.ToList();
        return commands;
    }
}