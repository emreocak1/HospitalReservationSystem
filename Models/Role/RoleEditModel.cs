using System.ComponentModel.DataAnnotations;

namespace itec420.Models;
public class RoleEditModel{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; } = null!;
}