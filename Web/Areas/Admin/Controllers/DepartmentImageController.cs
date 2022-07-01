using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentImageServices;

namespace Web.Areas.Admin.Controllers
{
    public class DepartmentImageController : BaseController
    {
        private readonly IDepartmentImageService _imageService;

        public DepartmentImageController(IDepartmentImageService imageService)
        {
            _imageService = imageService;
        }
        // GET: DepartmentImageController
        public ActionResult Index()
        {
            var list = _imageService.GetDepartmentImages();
            return View(list);
        }


        // GET: DepartmentImageController/Create
        public ActionResult Create()
        {
            return View(new DepartmentImageDto());
        }

        // POST: DepartmentImageController/Create
        [HttpPost]
        public async Task<ActionResult> Create(DepartmentImageDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _imageService.Insert(dto);
                    return RedirectToAction(nameof(Index));
                }
                return View(dto);
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: DepartmentImageController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var d = await _imageService.GetDepartmentImageById(id);
            return View(d);
        }

        // POST: DepartmentImageController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentImageDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _imageService.Update(dto);
                    return RedirectToAction(nameof(Index));
                }
                return View(dto);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: DepartmentImageController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var d = await _imageService.GetDepartmentImageById(id);
            return View(d);
        }

        // POST: DepartmentImageController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(DepartmentImageDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _imageService.Delete(dto);
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
