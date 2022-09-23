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

        public async Task<List<News>> GetNewses()
        {
            var news = await _context.Newses.ToListAsync();
            return news;
        }

        public async Task<NewsDto> GetNewsById(int Id)
        {
            var news = await (from s in _context.Newses
                              where s.Id==Id
                              select new NewsDto
                              {
                                  Id = s.Id,
                                  Title=s.Title,
                                  ImageName=s.Image,
                                  Description=s.Description,
                                  CreatedAt=s.CreatedAt,
                                  Enabled=s.Enabled
                              }
                              ).FirstOrDefaultAsync();
            return news;
        }

        public async Task<int> Insert(NewsDto news)
        {
            if (news.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(news.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await news.Image.CopyToAsync(stream);
                }

                var mapped = _mapper.Map<News>(news);
                mapped.Image = fileName;
                await _context.Newses.AddAsync(mapped);
                return await _context.SaveChangesAsync();
            }
            else
            {
                var mapped = _mapper.Map<News>(news);
                await _context.Newses.AddAsync(mapped);
                return await _context.SaveChangesAsync();
            }
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
                n.CreatedAt = news.CreatedAt;
                n.Enabled = news.Enabled;
                return await _context.SaveChangesAsync();
            }


        }
        public async Task<int> Delete(NewsDto news)
        {
            var n = await _context.Newses.FindAsync(news.Id);
            if (n == null) return 0;
            _context.Newses.Remove(n);
            return await _context.SaveChangesAsync();



        }

    }
}
