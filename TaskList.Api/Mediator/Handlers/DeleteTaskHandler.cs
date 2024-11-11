using MediatR;
using TaskList.Api.Mediator.Queries;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;

namespace TaskList.Api.Mediator.Handlers;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskQuery, BaseResponse>
{
    private readonly IUserRepository _userRepository;

    public DeleteTaskHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(DeleteTaskQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = _userRepository.DeleteTask(query);

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