using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Queries;

public class DeleteTaskQuery : IRequest<BaseResponse>
{
    public int TaskId { get; set; }
}