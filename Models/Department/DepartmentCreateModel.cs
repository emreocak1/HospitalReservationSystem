using System.ComponentModel.DataAnnotations;

namespace itec420.Models;
public class DepartmentCreateModel{
    [Required]
    [StringLength(50)]
    [Display(Name = "Name of Department")]
    public string DepartmentName { get; set; } = null!;

    [Required]
    [Display(Name = "Department Description")]
    public string DepartmentDescription { get; set; } = null!;
}