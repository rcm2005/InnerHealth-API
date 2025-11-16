using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InnerHealth.Api.Dtos;
using InnerHealth.Api.Services;

namespace InnerHealth.Api.Controllers;

/// <summary>
/// Endpoints para gerenciar sessões de exposição ao sol.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/sunlight")]
public class SunlightController : ControllerBase
{
    private readonly ISunlightService _sunlightService;
    private readonly IMapper _mapper;
    public SunlightController(ISunlightService sunlightService, IMapper mapper)
    {
        _sunlightService = sunlightService;
        _mapper = mapper;
    }
    [HttpGet("today")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetToday()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var sessions = await _sunlightService.GetSessionsAsync(date);
        var total = await _sunlightService.GetDailyTotalAsync(date);
        var recommended = _sunlightService.GetRecommendedDailyMinutes();
        var dtoList = _mapper.Map<IEnumerable<SunlightSessionDto>>(sessions);
        return Ok(new { date, totalMinutes = total, recommendedMinutes = recommended, entries = dtoList });
    }
    [HttpGet("week")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetWeekly()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        int diff = ((int)today.DayOfWeek + 6) % 7;
        var monday = today.AddDays(-diff);
        var totals = await _sunlightService.GetWeeklyTotalsAsync(monday);
        return Ok(totals);
    }
    [HttpPost]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Post([FromBody] CreateSunlightSessionDto dto)
    {
        var session = await _sunlightService.AddSessionAsync(dto.Minutes);
        var resultDto = _mapper.Map<SunlightSessionDto>(session);
        return CreatedAtAction(nameof(GetToday), new { id = resultDto.Id }, resultDto);
    }
    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateSunlightSessionDto dto)
    {
        var updated = await _sunlightService.UpdateSessionAsync(id, dto.Minutes);
        if (updated == null) return NotFound();
        return Ok(_mapper.Map<SunlightSessionDto>(updated));
    }
    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _sunlightService.DeleteSessionAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}