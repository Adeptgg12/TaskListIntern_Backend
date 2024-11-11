using Microsoft.EntityFrameworkCore;
using TaskList.Api.Mediator.Commands;
using TaskList.Api.Mediator.Queries;
using TaskList.Api.Repositories.Interfaces;
using TaskList.Api.Response;
using TaskList.Infrastructure;
using TaskList.Infrastructure.Models;
using TaskListModel = TaskList.Infrastructure.Models.TaskListModel;

namespace TaskList.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TaskListDbContext _context;

    public UserRepository(TaskListDbContext context)
    {
        _context = context;
    }

    public int CreateUser(CreateUserCommand user)
    {
        var userCreate = new UserModel()
        {
            Username = user.Username,
            Password = user.Password
        };
        _context.UserTb.Add(userCreate);
        return _context.SaveChanges();
    }

    public bool ExistUserById(string username)
    {
        var exitUserByIdObject = _context.UserTb
            .Any(includingObject => includingObject.Username == username);
        return exitUserByIdObject;
    }

    public bool UsernamePassword(LoginCommand command)
    {
        var userlogin = _context.UserTb.Any(x => x.Username == command.Username && x.Password == command.Password);
        return userlogin;
    }

    public dynamic CreateTask(CreateTaskCommand command)
    {
        var id = _context.UserTb.Where(x => x.Username == command.Username).Select(x => x.Id).FirstOrDefault();
        var userTask = new TaskListModel
        {
            Title = command.Title,
            Detail = command.Detail,
            AsignTo = command.AsignTo,
            DueDate = command.DueDate,
            Status = command.Status,
            Id = id
        };
        _context.TaskListTb.Add(userTask);
        return _context.SaveChanges();
        ;
    }

    public TaskListModelResponse TaskList()
    {
        var tasks = _context.TaskListTb
            .Select(t => new ListModel
            {
                TaskId = t.TaskListId,
                Title = t.Title,
                Detail = t.Detail,
                AsignTo = t.AsignTo,
                DueDate = t.DueDate,
                Status = t.Status
            })
            .ToList();

        var taskListModel = new TaskListModelResponse
        {
            List = tasks
        };

        return taskListModel;
    }

    public dynamic DeleteTask(DeleteTaskQuery query)
    {
        var deleteObject = _context.TaskListTb.FirstOrDefault(t => t.TaskListId == query.TaskId);
        if (deleteObject != null)
        {
            _context.TaskListTb.Remove(deleteObject);
            _context.SaveChanges();
            return "Success";
        }
        else
        {
            return "Failed";
        }
    }

    public ListModel GetTaskById(GetTaskByIdQuery query)
    {
        ListModel result = new ListModel();
        var selector = _context.TaskListTb.Where(x => x.TaskListId == query.TaskId).FirstOrDefault();
        if (selector != null)
        {
            result = new ListModel()
            {
                TaskId = selector.TaskListId,
                Title = selector.Title,
                AsignTo = selector.AsignTo,
                Detail = selector.Detail,
                DueDate = selector.DueDate,
                Status = selector.Status
            };
        }

        return result;
    }

    public dynamic GetUser()
    {
        var result = _context.UserTb.Select(x => x.Username).ToList();
        return result;
    }

    public dynamic UpdateTask(UpdateTaskCommand command)
    {
        var task = _context.TaskListTb.Where(x => x.TaskListId == command.TaskId).FirstOrDefault();
        if (task == null)
        {
            return "Failed";
        }

        task.Title = command.Title;
        task.Detail = command.Detail;
        task.AsignTo = command.AsignTo;
        task.DueDate = command.DueDate;
        task.Status = command.Status;
        _context.SaveChanges();
        return "Success";
    }

    public GetUserByUsernameResponse GetUserByUsername(GetUserByUsernameQuery query)
    {
        var result = _context.UserTb.Where(x => x.Username == query.Username).FirstOrDefault();
        GetUserByUsernameResponse user = new GetUserByUsernameResponse()
        {
            Username = result.Username,
            Password = result.Password,
        };

        return user;
    }

    public int UpdateUsernameAndPassword(UpdateUsernameAndPasswordCommand command)
    {
        var result = _context.UserTb.Where(x => x.Username == command.Username).FirstOrDefault();
        result.Username = command.Username;
        result.Password = command.Password;
        return _context.SaveChanges();
    }

    public TaskListModelResponse FilterTaskByUserAsign(FilterTaskByUserAsignCommand command)
    {
        try
        {
            var tasks = _context.TaskListTb.Where(x => x.AsignTo == command.Username)
                .Select(t => new ListModel
                {
                    TaskId = t.TaskListId,
                    Title = t.Title,
                    Detail = t.Detail,
                    AsignTo = t.AsignTo,
                    DueDate = t.DueDate,
                    Status = t.Status
                })
                .ToList();
            var taskListModel = new TaskListModelResponse
            {
                List = tasks,
            };
            return taskListModel;
        }
        catch (Exception a)
        {
            Console.WriteLine(a);
            throw;
        }
    }

    public TaskListModelResponse FilterTaskByUserCreate(FilterTaskByCreateByCommand command)
    {
        try
        {
            var id = _context.UserTb.Where(x => x.Username == command.Username).Select(id => id.Id).FirstOrDefault();
            var tasks = _context.TaskListTb.Where(x => x.Id == id)
                .Select(t => new ListModel
                {
                    TaskId = t.TaskListId,
                    Title = t.Title,
                    Detail = t.Detail,
                    AsignTo = t.AsignTo,
                    DueDate = t.DueDate,
                    Status = t.Status
                })
                .ToList();
            var taskListModel = new TaskListModelResponse
            {
                List = tasks,
            };
            return taskListModel;
        }
        catch (Exception a)
        {
            Console.WriteLine(a);
            throw;
        }
    }
}