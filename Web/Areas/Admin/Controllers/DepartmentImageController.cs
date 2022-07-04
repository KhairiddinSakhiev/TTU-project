using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentImageServices;

namespace Web.Areas.Admin.Controllers
{
    public class DepartmentImageController : BaseController
    {
        private readonly IDepartmentImageService _imageService;
        private readonly ILogger<DepartmentImageController> _logger;

        public DepartmentImageController(IDepartmentImageService imageService,ILogger<DepartmentImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }
        // GET: DepartmentImageController
        public async Task<ActionResult> Index()
        {
            var list =await _imageService.GetDepartmentImages();
            return View(list);
        }


        // GET: DepartmentImageController/Create
        public ActionResult Create()
        {
            return View(new DepartmentImageDto());
        }

        // POST: DepartmentImageController/Create
        [HttpPost]
        public async Task<ActionResult?> Create(DepartmentImageDto dto)
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
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(dto);
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
        public async Task<ActionResult?> Edit(DepartmentImageDto dto)
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

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(dto);
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
        public async Task<ActionResult?> Delete(DepartmentImageDto dto)
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

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(dto);
            }
        }
    }
}
