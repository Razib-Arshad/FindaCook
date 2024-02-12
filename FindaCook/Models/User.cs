using System.Text.Json.Serialization;

namespace FindaCook.Maui.Models;

public class User
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPassword { get; set; }

    // public string? UserPassword { get; set; }    
}
