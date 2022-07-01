using Domain.EntitiesDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.SliderServices;

namespace Web.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        // GET: SliderController
        public ActionResult Index()
        {
            var list = _sliderService.GetSliders();
            return View(list);
        }

       

        // GET: SliderController/Create
        public ActionResult Create()
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
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: SliderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        { var slider = await _sliderService.GetSliderById(id);
            return View(slider);
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(SliderDto dto)
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
                return View(ex.Message);
            }
        }

        // GET: SliderController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {var slider = await _sliderService.GetSliderById(id);
            return View(slider);
        }

        // POST: SliderController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(SliderDto dto)
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
                return View(ex.Message);
            }
        }
    }
}
