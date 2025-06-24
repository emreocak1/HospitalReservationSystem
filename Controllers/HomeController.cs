using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using itec420.Models;

namespace itec420.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    public HomeController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index(){
        return View();
    }
    public ActionResult Doctor(){
        var dcs = _context.Doctors.ToList();
        return View(dcs);
    }
    public ActionResult Department(){
        var deps = _context.Department.ToList();
        return View(deps);
    }
    public ActionResult Contact(){
        return View();
    }
}
