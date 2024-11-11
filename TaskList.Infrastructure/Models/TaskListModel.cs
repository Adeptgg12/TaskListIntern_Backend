using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskList.Infrastructure.Models;

[Table("TaskListTb")]

public class TaskListModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskListId { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Title { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(400)")]
    public string Detail { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string AsignTo { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Status { get; set; }
    
    [ForeignKey("Id")]
    [Required]
    public int Id { get; set; }
}