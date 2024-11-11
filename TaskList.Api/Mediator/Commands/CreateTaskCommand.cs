using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Commands;

public class CreateTaskCommand : IRequest<BaseResponse>
{
    public string Title { get; set; }
    public string Detail { get; set; }
    public string AsignTo { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
    public string Username { get; set; }
}