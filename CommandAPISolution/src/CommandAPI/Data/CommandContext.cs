using Microsoft.EntityFrameworkCore;
using CommandAPI.Models;

namespace CommandAPI.Data;

public class CommandContext : DbContext
{
    public CommandContext(DbContextOptions<CommandContext> options)
    {
        
    }

    public DbSet<Command> CommandItems()
    {
        get; set;
    }
}