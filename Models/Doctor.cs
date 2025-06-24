using System.ComponentModel.DataAnnotations.Schema;
namespace itec420.Models;

public class Doctor
{
    public int Id { get; set; }
    public string DoctorName { get; set; } = null!;
    public string DoctorDepartment { get; set; } = null!;
    public string PicOfDoc { get; set; } = null!;
    public int? AppUserId { get; set; }
    [ForeignKey("AppUserId")]
    public AppUser? AppUser { get; set; }


}