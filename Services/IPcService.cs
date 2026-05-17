using PcComponentsApi.DTOs;

namespace PcComponentsApi.Services;

public interface IPcService
{
    Task<List<PcSummaryResponseDto>> GetAllAsync();

    Task<PcDetailsResponseDto?> GetByIdWithComponentsAsync(int id);

    Task<PcResponseDto> CreateAsync(CreatePcRequestDto request);

    Task<PcResponseDto?> UpdateAsync(int id, UpdatePcRequestDto request);

    Task<bool> DeleteAsync(int id);
}