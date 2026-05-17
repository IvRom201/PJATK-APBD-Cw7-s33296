using Microsoft.EntityFrameworkCore;
using PcComponentsApi.Data;
using PcComponentsApi.DTOs;
using PcComponentsApi.Models;

namespace PcComponentsApi.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcSummaryResponseDto>> GetAllAsync()
    {
        return await _context.Pcs
            .AsNoTracking()
            .OrderBy(pc => pc.Id)
            .Select(pc => new PcSummaryResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<PcDetailsResponseDto?> GetByIdWithComponentsAsync(int id)
    {
        return await _context.Pcs
            .AsNoTracking()
            .Where(pc => pc.Id == id)
            .Select(pc => new PcDetailsResponseDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock,
                Components = pc.PcComponents
                    .OrderBy(pcComponent => pcComponent.Component.Code)
                    .Select(pcComponent => new PcComponentDto
                    {
                        Amount = pcComponent.Amount,
                        Component = new ComponentDto
                        {
                            Code = pcComponent.Component.Code,
                            Name = pcComponent.Component.Name,
                            Description = pcComponent.Component.Description,
                            Manufacturer = new ComponentManufacturerDto
                            {
                                Id = pcComponent.Component.ComponentManufacturer.Id,
                                Abbreviation = pcComponent.Component.ComponentManufacturer.Abbreviation,
                                FullName = pcComponent.Component.ComponentManufacturer.FullName,
                                FoundationDate = pcComponent.Component.ComponentManufacturer.FoundationDate
                            },
                            Type = new ComponentTypeDto
                            {
                                Id = pcComponent.Component.ComponentType.Id,
                                Abbreviation = pcComponent.Component.ComponentType.Abbreviation,
                                Name = pcComponent.Component.ComponentType.Name
                            }
                        }
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PcResponseDto> CreateAsync(CreatePcRequestDto request)
    {
        var pc = new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return ToPcResponseDto(pc);
    }

    public async Task<PcResponseDto?> UpdateAsync(int id, UpdatePcRequestDto request)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc is null)
        {
            return null;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return ToPcResponseDto(pc);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }

    private static PcResponseDto ToPcResponseDto(Pc pc)
    {
        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }
}