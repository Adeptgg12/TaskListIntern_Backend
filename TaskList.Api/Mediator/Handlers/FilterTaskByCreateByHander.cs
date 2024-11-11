using MediatR;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class FilterTaskByCreateByHander : IRequestHandler<FilterTaskByCreateByCommand, TaskListModelResponse>
{
    private readonly IUserRepository _userRepository;

    public FilterTaskByCreateByHander(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<TaskListModelResponse> Handle(FilterTaskByCreateByCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = _userRepository.FilterTaskByUserCreate(command);
        
            if (result == null || !result.List.Any())
            {
                return new TaskListModelResponse()
                {
                    Status = "Failed",
                };
            }

            result.Status = "Success";
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("An error occurred while fetching tasks", e);
        }
    }
}