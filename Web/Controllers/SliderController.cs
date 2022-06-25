using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.SliderServices;

namespace Web.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        // GET: SliderController
        public async Task<ActionResult> Index()
        {
            var list = await _sliderService.GetSliders();
            return View(list);
        }

        // GET: SliderController/Create
        public async Task<ActionResult> Create()
        {
            return View(new SliderDto());
        }

        // POST: SliderController/Create
        [HttpPost]
        public async Task<ActionResult> Create(SliderDto slider)
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
            catch
            {
                return View("Error");
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
        public async Task<ActionResult> Edit(SliderDto slider)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sliderService.Update(slider);
                    return RedirectToAction(nameof(Index));
                }
                return View(slider);
            }
            catch
            {
                return View("Error");
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
        public async Task<ActionResult> Delete(SliderDto slider)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sliderService.Delete(slider);
                    return RedirectToAction(nameof(Index));
                }
                return View(slider);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
