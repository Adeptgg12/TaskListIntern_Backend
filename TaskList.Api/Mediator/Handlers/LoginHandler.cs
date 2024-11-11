using MediatR;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse>
{
    private readonly IUserRepository _userRepository;

    public LoginHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var existuser = _userRepository.ExistUserById(command.Username);
            
            if (existuser)
            {
                var result = _userRepository.UsernamePassword(command);
                if (result)
                {
                    return new BaseResponse
                    {
                        Status = "Success"
                    };
                }
                else
                {
                    return new BaseResponse
                    {
                        Status = "Username or Password Incorrect"
                    };
                }
            }
            return new BaseResponse
            {
                Status = "Username Not found"
            };
        }
        catch (Exception a)
        {
            Console.WriteLine(a);
            throw;
        }
    }
}