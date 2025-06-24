using System.ComponentModel.DataAnnotations;

namespace itec420.Models;
public class RoleCreateModel{
    [Required]
    [StringLength(50)]
    [Display(Name = "Role Name")]
    public string RoleName { get; set; } = null!;
}