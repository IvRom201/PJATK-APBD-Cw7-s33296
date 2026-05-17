namespace PcComponentsApi.DTOs;

public record ComponentDto
{
    public string Code { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public ComponentManufacturerDto Manufacturer { get; init; } = null!;

    public ComponentTypeDto Type { get; init; } = null!;
}