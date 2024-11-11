using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Commands;


public class FilterTaskByCreateByCommand : IRequest<TaskListModelResponse>
{
    public string Username { get; set; }
}