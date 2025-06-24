using System.Threading.Tasks;
using itec420.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace itec420.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public ActionResult Index()
    {
        return View(_roleManager.Roles);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(RoleCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _roleManager.CreateAsync(new AppRole { Name = model.RoleName });

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

    public async Task<ActionResult> Edit(string id)
    {
        var entity = await _roleManager.FindByIdAsync(id);
        if (entity != null)
        {
            return View(new RoleEditModel { Id = entity.Id, RoleName = entity.Name! });
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string id, RoleEditModel model)
    {
        if (ModelState.IsValid)
        {
            var entity = await _roleManager.FindByIdAsync(id);
            if (entity != null)
            {
                entity.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(entity);
                if (result.Succeeded)
                {
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


    public async Task<ActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }
        var entity = await _roleManager.FindByIdAsync(id);

        if (entity != null)
        {
            ViewBag.Users = await _userManager.GetUsersInRoleAsync(entity.Name!);
            return View(entity);
        }

        return RedirectToAction("Index");
    }


    public async Task<ActionResult> DeleteRoleConfirm(string id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }
        var entity = await _roleManager.FindByIdAsync(id);

        if (entity != null)
        {
            await _roleManager.DeleteAsync(entity);
            TempData["Message"] = $"{entity.Name} has been deleted.";
        }
        return RedirectToAction("Index");
    }
    
}