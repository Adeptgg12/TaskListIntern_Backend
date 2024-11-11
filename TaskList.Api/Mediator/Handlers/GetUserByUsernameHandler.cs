using TaskList.Api.Mediator.Queries;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

using MediatR;

public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameQuery, GetUserByUsernameResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByUsernameResponse> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = _userRepository.GetUserByUsername(query);
            if (result != null)
            {
                return result;
            }
        
            return result;

        }
        catch (Exception a)
        {
            Console.WriteLine(a);
            throw;
        }
    }
}