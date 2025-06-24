using System.Threading.Tasks;
using itec420.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace itec420.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private UserManager<AppUser> _userManager;
    private RoleManager<AppRole> _roleManager;
    private readonly DataContext _context;

    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DataContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<ActionResult> Index(string role)
    {
        ViewBag.Roles = new SelectList(_roleManager.Roles, "Name", "Name", role);

        if (!string.IsNullOrEmpty(role))
        {
            return View(await _userManager.GetUsersInRoleAsync(role));
        }
        return View(_userManager.Users);
    }

    public ActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser { UserName = model.Email, Email = model.Email, Fullname = model.Fullname };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View(model);
    }

    public async Task<ActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

        return View(
            new UserEditModel
            {
                Fullname = user.Fullname,
                Email = user.Email!,
                SelectedRoles = await _userManager.GetRolesAsync(user)
            }
        );
    }


    [HttpPost]
    public async Task<ActionResult> EditUser(string id, UserEditModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = model.Email;
                user.Fullname = model.Fullname;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.Password);
                }
                if (result.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    if (model.SelectedRoles != null)
                    {
                        await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                        var isDoctorSelected = model.SelectedRoles.Contains("Doctor");
                        var doctorEntry = await _context.Doctors.FirstOrDefaultAsync(d => d.AppUserId == user.Id);

                        if (isDoctorSelected)
                        {
                            if (doctorEntry == null)
                            {
                                var newDoctor = new Doctor
                                {
                                    AppUserId = user.Id,
                                    DoctorName = user.Fullname,
                                    DoctorDepartment = "General",
                                    PicOfDoc = "1.png"
                                };
                                _context.Doctors.Add(newDoctor);
                            }
                        }
                        else
                        {
                            if (doctorEntry != null)
                            {
                                _context.Doctors.Remove(doctorEntry);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }


    public async Task<ActionResult> DeleteUser(string id)
    {

        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = await _userManager.FindByIdAsync(id);

        if (entity != null)
        {
            return View(entity);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> DeleteUserConfirm(string id)
    {

        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = await _userManager.FindByIdAsync(id);

        if (entity != null)
        {
            var result = await _userManager.DeleteAsync(entity);
            if (result.Succeeded)
            {
                TempData["Message"] = $"{entity.Fullname} has been deleted.";
            }

        }
        return RedirectToAction("Index");
    }

}