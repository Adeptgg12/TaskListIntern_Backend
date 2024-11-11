using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Queries;

public class GetUserByUsernameQuery : IRequest<GetUserByUsernameResponse>
{
    public string Username { get; set; }
}