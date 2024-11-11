using TaskList.Api.Mediator.Queries;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

using MediatR;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, ListModel>
{
    private readonly IUserRepository _userRepository;

    public GetTaskByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ListModel> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            ListModel result = _userRepository.GetTaskById(query);
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