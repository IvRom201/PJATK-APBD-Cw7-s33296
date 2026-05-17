namespace PcComponentsApi.DTOs;

public record PcResponseDto
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public double Weight { get; init; }

    public int Warranty { get; init; }

    public DateTime CreatedAt { get; init; }

    public int Stock { get; init; }
}