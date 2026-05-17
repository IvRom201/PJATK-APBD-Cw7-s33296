namespace PcComponentsApi.DTOs;

public record ComponentManufacturerDto
{
    public int Id { get; init; }

    public string Abbreviation { get; init; } = null!;

    public string FullName { get; init; } = null!;

    public DateOnly FoundationDate { get; init; }
}