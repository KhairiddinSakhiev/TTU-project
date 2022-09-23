using Domain.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.StudentServices;

namespace Web.Controllers
{
    public class StudentController :Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _studentService.GetStudents();
            return View(list);
        }
        public IActionResult Create()
        {
            return View(new StudentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDto student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentService.Insert(student);
                    return RedirectToAction(nameof(Index));
                }
                return View(student);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(student);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentById(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentDto stu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _studentService.Update(stu);
                    return RedirectToAction(nameof(Index));
                }
                else if(stu.Image==null && stu.ImageName == null)
                {
                    await _studentService.Update(stu);
                    return RedirectToAction(nameof(Index));
                }
                return View(stu);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(stu);
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentById(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentDto stu)
        {
            try
            {
                if (stu!=null)
                {
                    await _studentService.Delete(stu);
                    return RedirectToAction(nameof(Index));
                }
                
                    return View(stu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(stu);
            }
        }

    }
}
