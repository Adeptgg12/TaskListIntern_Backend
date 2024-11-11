using MediatR;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existuser = _userRepository.ExistUserById(command.Username);
            if (!existuser)
            {
                var result = _userRepository.CreateUser(command);
                if (result > 0)
                {
                    return new BaseResponse
                    {
                        Status = "Success"
                    };
                }
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