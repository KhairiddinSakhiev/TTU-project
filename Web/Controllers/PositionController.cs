using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.DepartmentServices;
using Services.EntitiesServices.PositionServices;

namespace Web.Controllers
{
    public class PositionController :Controller
    {
        private readonly IPositionService _positionService;
        private readonly ILogger<PositionController> _logger;
        private readonly IDepartmentService _departmentService;

        public PositionController(
            IPositionService positionService,
            ILogger<PositionController> logger,
            IDepartmentService departmentService
            )
        {
            _positionService = positionService;
            _logger = logger;
            _departmentService = departmentService;
        }
        // GET: PositionController
        public async Task<IActionResult> Index()
        {
            var list = await _positionService.GetPositions();
            return View(list);
        }       

        // GET: PositionController/Create
        public async Task<IActionResult> Create()
        {
           ViewBag.Departments=await _departmentService.GetDepartments();
            return View(new PositionDto());
        }

        // POST: PositionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PositionDto dto)
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            try
            {
                if (ModelState.IsValid)
                {
                    await _positionService.InsertPosition(dto);
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

        // GET: PositionController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            var position = await _positionService.GetPositionById(id);
            return View(position);
        }

        // POST: PositionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PositionDto dto)
        {
            ViewBag.Departments = await _departmentService.GetDepartments();
            try
            {
                if (ModelState.IsValid)
                {
                    await _positionService.UpdatePosition(dto);
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

        // GET: PositionController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var position = await _positionService.GetPositionById(id);
            return View(position);
        }

        // POST: PositionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PositionDto dto)
        {
            try
            {
                if (dto.Name!=null)
                {
                    await _positionService.DeletePosition(dto);
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
