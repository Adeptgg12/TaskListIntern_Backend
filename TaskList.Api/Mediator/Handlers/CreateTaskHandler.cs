using MediatR;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository;

    public CreateTaskHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = _userRepository.CreateTask(command);

            if (result > 0)
            {
                return new BaseResponse
                {
                    Status = "Success"
                };
            }
            return new BaseResponse
            {
                Status = "Failed"
            };

        }
        catch (Exception a)
        {
            Console.WriteLine(a);
            throw;
        }
    }
}

