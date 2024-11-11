using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Commands;


public class FilterTaskByUserAsignCommand : IRequest<TaskListModelResponse>
{
    public string Username { get; set; }
}