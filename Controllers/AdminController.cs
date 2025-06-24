using itec420.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace itec420.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public AdminController(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            DoctorCount = await _context.Doctors.CountAsync(),
            UserCount = await _userManager.Users.CountAsync(),
            DepartmentCount = await _context.Department.CountAsync(),
            AppointmentCount = await _context.Appointments.CountAsync()
        };

        return View(model);
    }

}