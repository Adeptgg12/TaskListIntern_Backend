using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Infrastructure.Models;

[Table("UserTb")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Username { get; set; }
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Password { get; set; }
    
}
