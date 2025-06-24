using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace itec420.Models;

public class Appointment
{
    public int AppointmentId { get; set; }

    [Required]
    public int AppUserId { get; set; }

    [ValidateNever]
    public AppUser AppUser { get; set; } = null!;

    [Required]
    public int DoctorId { get; set; }

    [ValidateNever]
    public Doctor Doctor { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan Time { get; set; }
}
