using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InnerHealth.Api.Dtos;
using InnerHealth.Api.Services;

namespace InnerHealth.Api.Controllers;

/// <summary>
/// Endpoints para gerenciar sessões de meditação.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/meditation")]
public class MeditationController : ControllerBase
{
    private readonly IMeditationService _meditationService;
    private readonly IMapper _mapper;
    public MeditationController(IMeditationService meditationService, IMapper mapper)
    {
        _meditationService = meditationService;
        _mapper = mapper;
    }
    [HttpGet("today")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetToday()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var sessions = await _meditationService.GetSessionsAsync(date);
        var total = await _meditationService.GetDailyTotalAsync(date);
        var recommended = _meditationService.GetRecommendedDailyMinutes();
        var dtoList = _mapper.Map<IEnumerable<MeditationSessionDto>>(sessions);
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
        var totals = await _meditationService.GetWeeklyTotalsAsync(monday);
        return Ok(totals);
    }
    [HttpPost]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Post([FromBody] CreateMeditationSessionDto dto)
    {
        var session = await _meditationService.AddSessionAsync(dto.Minutes);
        var resultDto = _mapper.Map<MeditationSessionDto>(session);
        return CreatedAtAction(nameof(GetToday), new { id = resultDto.Id }, resultDto);
    }
    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateMeditationSessionDto dto)
    {
        var updated = await _meditationService.UpdateSessionAsync(id, dto.Minutes);
        if (updated == null) return NotFound();
        return Ok(_mapper.Map<MeditationSessionDto>(updated));
    }
    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _meditationService.DeleteSessionAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}