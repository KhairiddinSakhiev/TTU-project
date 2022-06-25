using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentImageServices;

namespace Web.Controllers
{
    public class DepartmentImageController : Controller
    {
        private readonly IDepartmentImageService _departmentImageService;

        public DepartmentImageController(IDepartmentImageService departmentImageService )
        {
            _departmentImageService = departmentImageService;
        }
        // GET: DepartmentImageController
        public async Task<ActionResult> Index()
        {
            var list = await _departmentImageService.GetDepartmentImages();
            return View(list);
        }

        // GET: DepartmentImageController/Create
        public async Task<ActionResult> Create()
        {
            return View(new DepartmentImageDto());
        }

        // POST: DepartmentImageController/Create
        [HttpPost]
        public async Task<ActionResult> Create(DepartmentImageDto departmentImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentImageService.Insert(departmentImage);
                    return RedirectToAction(nameof(Index));
                }
                return View(departmentImage);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: DepartmentImageController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var d = await _departmentImageService.GetDepartmentImageById(id);
            return View(d);
        }

        // POST: DepartmentImageController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentImageDto departmentImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentImageService.Update(departmentImage);
                    return RedirectToAction(nameof(Index));
                }
                return View(departmentImage);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: DepartmentImageController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var department = await _departmentImageService.GetDepartmentImageById(id);
            return View(department);
        }

        // POST: DepartmentImageController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(DepartmentImageDto departmentImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentImageService.Delete(departmentImage);
                    return RedirectToAction(nameof(Index));
                }
                return View(departmentImage);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
