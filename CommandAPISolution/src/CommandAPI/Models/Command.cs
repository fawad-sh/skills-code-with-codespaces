using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Models;

public class Command 
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string HowTo { get; set; } = string.Empty;

    [Required]
    public string Line { get; set; } = string.Empty;
    
    [Required]
    public string Platform { get; set; } = string.Empty;
}