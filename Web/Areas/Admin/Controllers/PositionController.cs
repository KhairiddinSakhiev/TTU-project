using Domain.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.PositionServices;

namespace Web.Areas.Admin.Controllers;

public class PositionController : Controller
{
    private readonly IPositionService _positionService;
    private readonly ILogger _logger;

    public PositionController(IPositionService positionService, ILogger logger)
    {
        _positionService = positionService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _positionService.GetPositions();
        return View(list);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new PositionDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(PositionDto positionDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _positionService.InsertPosition(positionDto);
                return RedirectToAction(nameof(Index));
            }

            return View(positionDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ModelState.AddModelError(string.Empty,"Some generic error occurred. Try again.");
            return View(positionDto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var finded = await _positionService.GetPositionById(id);
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