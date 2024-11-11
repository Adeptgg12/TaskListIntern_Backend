using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Queries;

public class GetTaskByIdQuery : IRequest<ListModel>
{
    public int TaskId { get; set; }
}