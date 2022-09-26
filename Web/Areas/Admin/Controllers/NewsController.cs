using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.NewsServices;

namespace Web.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;
        public NewsController(INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _newsService.GetNewses();
            return View(list);
        }
        public IActionResult Create()
        {
            return View(new NewsDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsDto news)
        {
            try
            {
                if (news.Title!=null)
                {
                    await _newsService.Insert(news);
                    return RedirectToAction(nameof(Index));
                }
                return View(news);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(news);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsById(id);
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsDto dto)
        {
            try
            {
                if (dto.Title != null || dto.Image == null && dto.ImageName == null)
                {
                    await _newsService.Update(dto);
                    return RedirectToAction(nameof(Index));
                }
                return View(dto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(dto);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsById(id);
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(NewsDto dto)
        {
            try
            {
                if (dto.Title != null || dto.Image == null && dto.ImageName == null)
                {
                    await _newsService.Delete(dto);
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
