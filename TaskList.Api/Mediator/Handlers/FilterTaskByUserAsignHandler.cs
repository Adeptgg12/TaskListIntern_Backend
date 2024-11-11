using MediatR;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class FilterTaskByUserAsign : IRequestHandler<FilterTaskByUserAsignCommand, TaskListModelResponse>
{
    private readonly IUserRepository _userRepository;

    public FilterTaskByUserAsign(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<TaskListModelResponse> Handle(FilterTaskByUserAsignCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = _userRepository.FilterTaskByUserAsign(command);
        
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