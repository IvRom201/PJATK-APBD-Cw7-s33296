using Microsoft.AspNetCore.Mvc;
using PcComponentsApi.DTOs;
using PcComponentsApi.Services;

namespace PcComponentsApi.Controllers;


[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PcSummaryResponseDto>>> GetAll()
    {
        var pcs = await _pcService.GetAllAsync();

        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PcDetailsResponseDto>> GetByIdWithComponents(int id)
    {
        var pc = await _pcService.GetByIdWithComponentsAsync(id);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<ActionResult<PcResponseDto>> Create(CreatePcRequestDto request)
    {
        var createdPc = await _pcService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetByIdWithComponents),
            new { id = createdPc.Id },
            createdPc
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PcResponseDto>> Update(int id, UpdatePcRequestDto request)
    {
        var updatedPc = await _pcService.UpdateAsync(id, request);

        if (updatedPc is null)
        {
            return NotFound();
        }

        return Ok(updatedPc);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}