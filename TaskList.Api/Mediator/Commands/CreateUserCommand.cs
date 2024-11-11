using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Commands;

public class CreateUserCommand : IRequest<BaseResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}