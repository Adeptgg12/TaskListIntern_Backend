using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Mediator.Queries;
using TaskList.Api.Repositories.Interfaces;

namespace TaskList.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class UserController : ControllerBase
{
    //Dependency Injection
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    //รับค่าเข้ามาและกำหนดให้กับฟิลด์  _mediator 
    public UserController(IMediator mediator, IMapper mapper, IUserRepository userRepository)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    
    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet("GetUser")]
    public async Task<ActionResult> GetUser()
    {
        try
        {
            var result = _userRepository.GetUser();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    
    [HttpGet("GetUserByUsername")]
    public async Task<ActionResult> GetUserByUsername([FromQuery] GetUserByUsernameQuery query)
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
    
    [HttpPatch("UpdateUsernameAndPassword")]
    public async Task<ActionResult> UpdateUsernameAndPassword([FromBody] UpdateUsernameAndPasswordCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}