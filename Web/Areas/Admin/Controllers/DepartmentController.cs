using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentServices;

namespace Web.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService )
        {
            _departmentService = departmentService;
        }
        // GET: DepartmentController
        public ActionResult Index()
        {
            var list =  _departmentService.GetDepartments();
            return View(list);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View(new DepartmentDto());
        }

        // POST: DepartmentController/Create
        [HttpPost]
        public async Task<ActionResult> Create(DepartmentDto department)
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
                return View(ex.Message);
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
        public async Task<ActionResult> Edit(DepartmentDto dto)
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
                return View(ex.Message);
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
        public async Task<ActionResult> Delete(DepartmentDto dto)
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
                return View(ex.Message);
            }
        }
    }
}
