using System.ComponentModel.DataAnnotations;

namespace PcComponentsApi.DTOs;

public record CreatePcRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = null!;

    [Range(0.01, 999999.99)]
    public double Weight { get; init; }

    [Range(0, 1200)]
    public int Warranty { get; init; }

    public DateTime CreatedAt { get; init; }

    [Range(0, int.MaxValue)]
    public int Stock { get; init; }
}