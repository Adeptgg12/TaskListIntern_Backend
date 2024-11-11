using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Commands;

public class UpdateUsernameAndPasswordCommand : IRequest<BaseResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}