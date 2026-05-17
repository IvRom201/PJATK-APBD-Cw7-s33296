namespace PcComponentsApi.DTOs;

public record PcComponentDto
{
    public int Amount { get; init; }

    public ComponentDto Component { get; init; } = null!;
}