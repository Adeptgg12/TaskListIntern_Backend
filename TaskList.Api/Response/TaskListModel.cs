namespace TaskList.Api.Response;

public class TaskListModelResponse : BaseResponse
{
    public List<ListModel> List { get; set; }
}

public class ListModel 
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string AsignTo { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
}
