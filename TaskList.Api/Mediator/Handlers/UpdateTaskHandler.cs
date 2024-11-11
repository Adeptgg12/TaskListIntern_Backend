using MediatR;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository;

    public UpdateTaskHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = _userRepository.UpdateTask(command);

            if (result == "Success")
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