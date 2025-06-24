using System.Security.Claims;
using System.Threading.Tasks;
using itec420.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace itec420.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private IEmailService _emailService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(AccountCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                Fullname = model.Fullname
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Patient");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AccountLoginModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong Password");
                }
            }
            else
            {
                ModelState.AddModelError("", "Wrong Email");
            }
        }
        return View(model);
    }

    [Authorize]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<ActionResult> EditUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }
        return View(new AccountEditUserModel
        {
            Fullname = user.Fullname,
            Email = user.Email!
        });
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult> EditUser(AccountEditUserModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user != null)
            {
                user.Email = model.Email;
                user.Fullname = model.Fullname;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Your information has been updated";
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }


    [Authorize]
    public ActionResult ChangePassword()
    {
        return View();
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult> ChangePassword(AccountChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Password has been changed";
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }



    public ActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            TempData["Message"] = "Enter your email";
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            TempData["Message"] =  "Email address could not be found";
            return View();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var url = Url.Action("ResetPassword", "Account",new {userId = user.Id, token});
        var link = $"<a href='http://localhost:5183{url}'>Forgot Password</a> ";
        await _emailService.SendEmailAsync(user.Email!, "Reset Password", link);
        TempData["Message"] =  "Reset your password with the link that has been sent to your email";
        
        return RedirectToAction("Login");
    }



    public async Task<ActionResult> ResetPassword(string userId, string token)
    {
        if (userId == null || token == null)
        {
            return RedirectToAction("Login");
        }
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            TempData["Message"] =  "Email address could not be found";
            return RedirectToAction("Login");
        }

        var model = new AccountResetPasswordModel
        {
            Token = token,
            Email = user.Email!
        };
        return View(model);
    }


    [HttpPost]
    public async Task<ActionResult> ResetPassword(AccountResetPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                TempData["Message"] = "Your password has been updated";
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    
    public ActionResult AccessDenied()
    {
        return View();
    }
}