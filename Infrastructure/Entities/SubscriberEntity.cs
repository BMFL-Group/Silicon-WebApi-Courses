using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string Email { get; set; } = null!;
}
