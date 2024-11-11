using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Mediator.Queries;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;
using TaskList.Api.Services;

namespace TaskList.Api.Controllers;


[ApiController]
[Route("Api/[controller]")]
public class TaskController : ControllerBase
{
    //Dependency Injection
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;
    private readonly LineNotifyService _lineNotifyService;


    //รับค่าเข้ามาและกำหนดให้กับฟิลด์  _mediator 
    public TaskController(IMediator mediator,IUserRepository userRepository, LineNotifyService lineNotifyService)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _lineNotifyService = lineNotifyService;
    }
    
    
    [HttpPost("CreateTask")]
    public async Task<ActionResult> CreateTask([FromBody]CreateTaskCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            // เตรียมข้อความแจ้งเตือน
            //string message = $"\nNew Task Created: {command.Title}\nAssigned to: {command.AsignTo}\nDue Date: {command.DueDate.ToString("yyyy-MM-dd HH:mm")}\nStatus: {command.Status}";
            string message = $"\n📋 New Task Created! \n"
                             + $"📝 Title: {command.Title}\n"
                             + $"👤 Assigned to: {command.AsignTo}\n"
                             + $"📅 Due Date: {command.DueDate.ToString("yyyy-MM-dd HH:mm")}\n"
                             + $"📊 Status: {command.Status}";
            // เรียกใช้งาน LINE Notify เพื่อแจ้งเตือน
            await _lineNotifyService.SendLineNotificationAsync(message);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet("GetAllTask")]
    public async Task<ActionResult> GetAllTask()
    {
        try
        {
            TaskListModelResponse result = _userRepository.TaskList();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpDelete("DeleteTask")]
    public async Task<BaseResponse> DeleteTask([FromQuery] DeleteTaskQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
            return result;
        }
        catch (Exception a)
        {
            Console.WriteLine(a);
            throw;
        }
    }
    
    [HttpGet("GetTaskById")]
    public async Task<ActionResult> GetTaskById([FromQuery] GetTaskByIdQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpPatch("UpdateTask")]
    public async Task<ActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            //string message = $"\nUpdate Task: {command.Title}\nAssigned to: {command.AsignTo}\nDue Date: {command.DueDate.ToString("yyyy-MM-dd HH:mm")}\nStatus: {command.Status}";
            string message = $"\n📋 Update Task! \n"
                             + $"📝 Title: {command.Title}\n"
                             + $"👤 Assigned to: {command.AsignTo}\n"
                             + $"📅 Due Date: {command.DueDate.ToString("yyyy-MM-dd HH:mm")}\n"
                             + $"📊 Status: {command.Status}";
            // เรียกใช้งาน LINE Notify เพื่อแจ้งเตือน
            await _lineNotifyService.SendLineNotificationAsync(message);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet("FilterTaskByAssignTo")]
    public async Task<ActionResult> FilterTaskByUserAsign([FromQuery] FilterTaskByUserAsignCommand command)
    {
        if (string.IsNullOrEmpty(command.Username))
        {
            return BadRequest(new { Status = "Failed", Message = "Username is required." });
        }

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Status = "Failed", Message = "Internal server error.", Error = e.Message });
        }
    }
    [HttpGet("FilterTaskByCreateBy")]
    public async Task<ActionResult> FilterTaskByUserAsign([FromQuery] FilterTaskByCreateByCommand command)
    {
        if (string.IsNullOrEmpty(command.Username))
        {
            return BadRequest(new { Status = "Failed", Message = "Username is required." });
        }

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Status = "Failed", Message = "Internal server error.", Error = e.Message });
        }
    }

}