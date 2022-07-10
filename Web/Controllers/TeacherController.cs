using Domain.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.EntitiesServices.TeacherServices;

namespace Web.Controllers;

public class TeacherController : Controller
{
    private readonly ITeacherService _teacher;
    private readonly ILogger _logger;

    public TeacherController(ITeacherService teacher, ILogger logger)
    {
        _teacher = teacher;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _teacher.GetTeachers();
        return View(list);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new TeacherDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeacherDto teacherDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _teacher.InsertTeacher(teacherDto);
                return RedirectToAction(nameof(Index));
            }

            return View(teacherDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ModelState.AddModelError(string.Empty,"Some generic error occurred. Try again.");
            return View(teacherDto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var finded = await _teacher.GetTeacherById(id);
        return View(finded);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TeacherDto teacherDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _teacher.UpdateTeacher(teacherDto);
                return RedirectToAction(nameof(Index));
            }

            return View(teacherDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
            return View(teacherDto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var finded = await _teacher.GetTeacherById(id);
        return View(finded);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(TeacherDto teacherDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _teacher.DeleteTeacher(teacherDto);
                return RedirectToAction(nameof(Index));
            }

            return View(teacherDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
            return View(teacherDto);
        }
    }
}