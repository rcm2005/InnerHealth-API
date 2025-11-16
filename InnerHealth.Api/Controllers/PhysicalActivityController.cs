using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InnerHealth.Api.Dtos;
using InnerHealth.Api.Services;
using InnerHealth.Api.Models;

namespace InnerHealth.Api.Controllers;

/// <summary>
/// Endpoints para gerenciar atividades físicas.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/physical-activity")]
public class PhysicalActivityController : ControllerBase
{
    private readonly IPhysicalActivityService _activityService;
    private readonly IMapper _mapper;
    public PhysicalActivityController(IPhysicalActivityService activityService, IMapper mapper)
    {
        _activityService = activityService;
        _mapper = mapper;
    }
    [HttpGet("today")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetToday()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var activities = await _activityService.GetActivitiesAsync(date);
        var dtoList = _mapper.Map<IEnumerable<PhysicalActivityDto>>(activities);
        return Ok(new { date, entries = dtoList });
    }
    [HttpGet("week")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetWeekly()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        int diff = ((int)today.DayOfWeek + 6) % 7;
        var monday = today.AddDays(-diff);
        var activities = await _activityService.GetWeeklyActivitiesAsync(monday);
        var mapped = activities.ToDictionary(kvp => kvp.Key, kvp => _mapper.Map<IEnumerable<PhysicalActivityDto>>(kvp.Value));
        return Ok(mapped);
    }
    [HttpPost]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Post([FromBody] CreatePhysicalActivityDto dto)
    {
        var activity = await _activityService.AddActivityAsync(dto.Modality, dto.DurationMinutes);
        var resultDto = _mapper.Map<PhysicalActivityDto>(activity);
        return CreatedAtAction(nameof(GetToday), new { id = resultDto.Id }, resultDto);
    }
    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdatePhysicalActivityDto dto)
    {
        var updated = await _activityService.UpdateActivityAsync(id, dto.Modality, dto.DurationMinutes);
        if (updated == null) return NotFound();
        return Ok(_mapper.Map<PhysicalActivityDto>(updated));
    }
    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _activityService.DeleteActivityAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}