using Domain.EntitiesDto;
using Microsoft.AspNetCore.Mvc;
using Services.EntitiesServices.NewsServices;

namespace Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;
        public NewsController(INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }
        public async Task<ActionResult> Index()
        {
            var list = await _newsService.GetNewses();
            return View(list);
        }
        public ActionResult Create()
        {
            return View(new NewsDto());
        }

        [HttpPost]
        public async Task<ActionResult?> Create(NewsDto news)
        {
            try
            {
                if (ModelState.IsValid)
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

        public async Task<ActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsById(id);
            return View(news);
        }

        [HttpPost]
        public async Task<ActionResult?> Edit(NewsDto stu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsService.Update(stu);
                    return RedirectToAction(nameof(Index));
                }
                return View(stu);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(stu);
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            var news = await _newsService.GetNewsById(id);
            return View(news);
        }

        [HttpPost]
        public async Task<ActionResult?> Delete(NewsDto stu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _newsService.Delete(stu);
                    return RedirectToAction(nameof(Index));
                }
                return View(stu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, "Some generic error occurred. Try again.");
                return View(stu);
            }
        }

    }
}
