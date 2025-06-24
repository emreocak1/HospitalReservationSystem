using System.ComponentModel.DataAnnotations;

namespace itec420.Models;
public class DoctorCreateModel{
    [Required]
    [StringLength(50)]
    [Display(Name = "Name of Doctor")]
    public string DoctorName { get; set; } = null!;
    
    [Required]
    [StringLength(50)]
    [Display(Name = "Department of Doctor")]
    public string DoctorDepartment { get; set; } = null!;
    
    [Required]
    [Display(Name = "Picture URL")]
    public string PicOfDoc { get; set; } = null!;
}