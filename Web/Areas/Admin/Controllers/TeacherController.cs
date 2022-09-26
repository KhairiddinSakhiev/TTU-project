using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.TeacherServices;

namespace Web.Areas.Admin.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService,ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }
        // GET: TeacherController
        public async Task<IActionResult> Index()
        {
            var list = await _teacherService.GetTeachers();
            return View(list);
        }

       

        // GET: TeacherController/Create
        public IActionResult Create()
        {
            return View(new TeacherDto());
        }

        // POST: TeacherController/Create
        [HttpPost]
        public async Task<IActionResult> Create(TeacherDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _teacherService.InsertTeacher(dto);
                    return RedirectToAction(nameof(Index));
                }
                if(dto.ImageName == null)
                {
                    await _teacherService.InsertTeacher(dto);
                    return RedirectToAction("Index");
                }
                else if(dto.Image == null && dto.ImageName == null)
                {
                    await _teacherService.InsertTeacher(dto);
                    return RedirectToAction("Index");
                }
                return View(dto);
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(dto);
            }
        }

        // GET: TeacherController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherService.GetTeacherById(id);
            return View(teacher);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeacherDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _teacherService.UpdateTeacher(dto);
                    return RedirectToAction(nameof(Index));

                }
                if (dto.Image == null)
                {
                    await _teacherService.UpdateTeacher(dto);
                    return RedirectToAction(nameof(Index));
                }
                else if (dto.Image == null && dto.ImageName == null)
                {
                    await _teacherService.UpdateTeacher(dto);
                    return RedirectToAction(nameof(Index));
                }
                return View(dto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(dto);
            }
        }

            // GET: TeacherController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {   var teacher= await _teacherService.GetTeacherById(id);
            return View(teacher);
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(TeacherDto dto)
        {
            if (dto != null)
            {
                await _teacherService.DeleteTeacher(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }
    }
}
