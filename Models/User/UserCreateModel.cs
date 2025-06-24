using System.ComponentModel.DataAnnotations;

namespace itec420.Models;

public class UserCreateModel
{
    [Required]
    [Display(Name = "Fullname")]
    public string Fullname { get; set; } = null!;

    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = null!;
}