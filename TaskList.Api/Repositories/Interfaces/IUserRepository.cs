using TaskList.Api.Mediator.Commands;
using TaskList.Api.Mediator.Queries;
using TaskList.Api.Response;

namespace TaskList.Api.Repositories.Interfaces;

public interface IUserRepository
{
    int CreateUser(CreateUserCommand user);
    bool ExistUserById(string username);

    bool UsernamePassword(LoginCommand command);
    
    
    //task

    dynamic CreateTask(CreateTaskCommand command);
    
    TaskListModelResponse TaskList();

    dynamic DeleteTask(DeleteTaskQuery query);

    ListModel GetTaskById(GetTaskByIdQuery query);

    dynamic GetUser();

    dynamic UpdateTask(UpdateTaskCommand command);

    GetUserByUsernameResponse GetUserByUsername(GetUserByUsernameQuery query);

    int UpdateUsernameAndPassword(UpdateUsernameAndPasswordCommand command);

    TaskListModelResponse FilterTaskByUserAsign(FilterTaskByUserAsignCommand command);
    
    TaskListModelResponse FilterTaskByUserCreate(FilterTaskByCreateByCommand command);

}