using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itec420.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace itec420.Controllers;

[Authorize(Roles = "Admin")]
public class DoctorController : Controller
{
    private readonly DataContext _context;
    public DoctorController(DataContext context)
    {
        _context = context;
    }
    public ActionResult Index()
    {
        var dcs = _context.Doctors.ToList();
        return View(dcs);
    }
    public ActionResult CreateDoctor()
    {
        ViewBag.Departments = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");
        return View();
    }

    [HttpPost]
    public ActionResult CreateDoctor(DoctorCreateModel model)
    {

        if (ModelState.IsValid)
        {
            var entity = new Doctor
            {
                DoctorName = model.DoctorName,
                DoctorDepartment = model.DoctorDepartment,
                PicOfDoc = model.PicOfDoc
            };

            _context.Doctors.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public ActionResult EditDoctor(int id)
    {

        ViewBag.Departments = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");

        var entity = _context.Doctors.Select(i => new DoctorEditModel
        {
            Id = i.Id,
            DoctorName = i.DoctorName,
            DoctorDepartment = i.DoctorDepartment,
            PicOfDoc = i.PicOfDoc
        }).FirstOrDefault(i => i.Id == id);

        return View(entity);
    }

    [HttpPost]
    public ActionResult EditDoctor(int id, DoctorEditModel model)
    {

        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var entity = _context.Doctors.FirstOrDefault(i => i.Id == model.Id);

            if (entity != null)
            {
                entity.DoctorName = model.DoctorName;
                entity.DoctorDepartment = model.DoctorDepartment;
                entity.PicOfDoc = model.PicOfDoc;

                _context.SaveChanges();

                TempData["Message"] = $"{entity.DoctorName} has been updated.";

                return RedirectToAction("Index");
            }
        }

        return View(model);

    }

    public ActionResult DeleteDoctor(int? id)
    {

        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Doctors.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            return View(entity);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteDoctorConfirm(int? id)
    {

        if (id == null)
        {
            return RedirectToAction("Index");
        }

        var entity = _context.Doctors.FirstOrDefault(i => i.Id == id);

        if (entity != null)
        {
            _context.Doctors.Remove(entity);
            _context.SaveChanges();

            TempData["Message"] = $"{entity.DoctorName} has been deleted.";

        }
        return RedirectToAction("Index");
    }
}