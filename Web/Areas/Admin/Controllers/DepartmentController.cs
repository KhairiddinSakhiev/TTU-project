using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentServices;

namespace Web.Areas.Admin.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }
        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            var list =  await _departmentService.GetDepartments();
            return View(list);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View(new DepartmentDto());
        }

        // POST: DepartmentController/Create
        [HttpPost]
        public async Task<ActionResult?> Create(DepartmentDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.Insert(department);
                    return RedirectToAction(nameof(Index));
                }
                return View(department);
            }
            catch(Exception ex)
            {
             
                _logger.LogError(ex, "Something went wrong on the server, please wait");
                return null;
            }
        }

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var d = await _departmentService.GetDepartmentById(id);
            return View(d);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        public async Task<ActionResult?> Edit(DepartmentDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.Update(dto);
                    return RedirectToAction(nameof(Index));
                }
                return View(dto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Something went wrong on the server, please wait");
                return null;
            }
        }

        // GET: DepartmentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var d = await _departmentService.GetDepartmentById(id);
            return View(d);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        public async Task<ActionResult?> Delete(DepartmentDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.Delete(dto);
                    return RedirectToAction(nameof(Index));
                }
                return View(dto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Something went wrong on the server, please wait");
                return null;
            }
        }
    }
}
