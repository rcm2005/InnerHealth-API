using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InnerHealth.Api.Dtos;
using InnerHealth.Api.Services;

namespace InnerHealth.Api.Controllers;

/// <summary>
/// Endpoints para gerenciar tarefas.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;
    public TaskController(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper = mapper;
    }
    [HttpGet("today")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetToday()
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var tasks = await _taskService.GetTasksAsync(date);
        var dtoList = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        return Ok(dtoList);
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        var dtoList = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        return Ok(dtoList);
    }
    [HttpPost]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Post([FromBody] CreateTaskItemDto dto)
    {
        var task = await _taskService.AddTaskAsync(dto.Title!, dto.Description, dto.Date, dto.Priority);
        var resultDto = _mapper.Map<TaskItemDto>(task);
        return CreatedAtAction(nameof(GetAll), new { id = resultDto.Id }, resultDto);
    }
    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskItemDto dto)
    {
        var updated = await _taskService.UpdateTaskAsync(id, dto.Title!, dto.Description, dto.Date, dto.IsComplete, dto.Priority);
        if (updated == null) return NotFound();
        return Ok(_mapper.Map<TaskItemDto>(updated));
    }
    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}