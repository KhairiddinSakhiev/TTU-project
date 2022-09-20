using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsService(DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<NewsDto>> GetNewses()
        {
            var news = await (from n in _context.Newses
                              select new NewsDto
                              {
                                  Id = n.Id,
                                  Title = n.Title,
                                  ImageName = n.Image,
                                  Description = n.Description,
                                  CreatedAt = n.CreatedAt,
                                  Enabled = n.Enabled
                              }).ToListAsync();
            return news;
        }

        public async Task<NewsDto> GetNewsById(int Id)
        {
            var news = await (from n in _context.Newses
                                    where n.Id == Id
                                    select new NewsDto
                                    {
                                        Id = n.Id,
                                        Title = n.Title,
                                        ImageName=n.Image,
                                        Description = n.Description,
                                        CreatedAt = n.CreatedAt,
                                        Enabled = n.Enabled
                                    }).FirstOrDefaultAsync();
            if (news == null) return new NewsDto();
            return news;
        }

        public async Task<int> Insert(NewsDto news)
        {
            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(news.Image.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await news.Image.CopyToAsync(stream);
            }

            var mapped = _mapper.Map<News>(news);
            mapped.Image = news.Image.FileName;
            await _context.Newses.AddAsync(mapped);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> Update(NewsDto news)
        {
            if (news.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(news.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await news.Image.CopyToAsync(stream);
                }
                var n = await _context.Newses.FindAsync(news.Id);
                if (n == null) return 0;
                n.Title = news.Title;
                n.Description = news.Description;
                n.Image = fileName;
                n.CreatedAt = news.CreatedAt;
                n.Enabled = news.Enabled;
                return await _context.SaveChangesAsync();
            }


            else
            {
                var n = await _context.Newses.FindAsync(news.Id);
                if (n == null) return 0;
                n.Title = news.Title;
                n.Description = news.Description;
                n.Image = news.ImageName;
                n.CreatedAt = news.CreatedAt;
                n.Enabled = news.Enabled;
                return await _context.SaveChangesAsync();
            }


        }
        public async Task<int> Delete(NewsDto news)
        {
            //var n = await _context.Departments.FindAsync(news.Id);
            //if (n == null) return 0;
            //_context.Newses.Remove(n);
            //return await _context.SaveChangesAsync();

            var news1 = await _context.Newses.FindAsync(news.Id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", news1.Image);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            _context.Newses.Remove(news1);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int Id)
        {
            var n = await _context.Newses.FindAsync(Id);
            if (n == null) return 0;
            _context.Newses.Remove(n);
            return await _context.SaveChangesAsync();
        }

        public Task<NewsDto> Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
