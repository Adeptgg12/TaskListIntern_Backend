using MediatR;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Commands;


public class UpdateTaskCommand : IRequest<BaseResponse>
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string AsignTo { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
}