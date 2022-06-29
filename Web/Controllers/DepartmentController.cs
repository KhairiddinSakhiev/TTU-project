using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentServices;

namespace Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService){
            _departmentService = departmentService;
        }

        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            var listOfDepartments = await _departmentService.GetDepartments();
            return View(listOfDepartments);
        }

        
        // GET: DepartmentController/Create
        public async Task<ActionResult> Create()
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
            catch
            {
                return View("Error");
            }
        }

        // GET: DepartmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            return View(department);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                return View(department);
            }
            catch
            {
                return View("Error");
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
        public async Task<ActionResult> Delete(DepartmentDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                return View(department);
            }
            catch
            {
                return View("Error");
            }
        }
       
    }
}
