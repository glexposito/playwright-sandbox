using System.ComponentModel.DataAnnotations;

namespace PlaywrightSandbox.Web.Models;

public class Game
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Genre { get; set; } = string.Empty;

    [Range(1970, 2030)]
    public int ReleaseYear { get; set; }

    [Range(1, 10)]
    public double Rating { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }
}
