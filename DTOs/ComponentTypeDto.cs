namespace PcComponentsApi.DTOs;

public record ComponentTypeDto
{
    public int Id { get; init; }

    public string Abbreviation { get; init; } = null!;

    public string Name { get; init; } = null!;
}