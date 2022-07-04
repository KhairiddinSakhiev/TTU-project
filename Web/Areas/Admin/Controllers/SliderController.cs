using Domain.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.SliderServices;

namespace Web.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;
        private readonly ILogger<SliderController> _logger;

        public SliderController(ISliderService sliderService, ILogger<SliderController> logger)
        {
            _sliderService = sliderService;
            _logger = logger;
        }
        // GET: SliderController
        public async Task<ActionResult> Index()
        {
            var list = await _sliderService.GetSliders();
            return View(list);
        }



        // GET: SliderController/Create
        public ActionResult Create()
        {
            return View(new SliderDto());
        }

        // POST: SliderController/Create
        [HttpPost]
        public async Task<ActionResult?> Create(SliderDto slider)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sliderService.Insert(slider);
                    return RedirectToAction(nameof(Index));
                }
                return View(slider);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(slider);
            }
        }

        // GET: SliderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetSliderById(id);
            return View(slider);
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        public async Task<ActionResult?> Edit(SliderDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sliderService.Update(dto);
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

        // GET: SliderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var slider = await _sliderService.GetSliderById(id);
            return View(slider);
        }

        // POST: SliderController/Delete/5
        [HttpPost]
        public async Task<ActionResult?> Delete(SliderDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sliderService.Delete(dto);
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
