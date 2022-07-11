using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _webHostEnvironment;

        public NewsService(DataContext context, IMapper mapper, IHostingEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<News>> GetNewses()
        {
            return await _context.Newses.ToListAsync();
        }
        public async Task<NewsDto> GetNewsById(int Id)
        {
            var news = await (from n in _context.Newses
                                    where n.Id == Id
                                    select new NewsDto
                                    {
                                        Id = n.Id,
                                        Title = n.Title,
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
            mapped.Image = fileName;
            await _context.Newses.AddAsync(mapped);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(NewsDto news)
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
        public async Task<int> Delete(NewsDto news)
        {
            var n = await _context.Departments.FindAsync(news.Id);
            if (n == null) return 0;
            _context.Departments.Remove(n);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var n = await _context.Newses.FindAsync(Id);
            if (n == null) return 0;
            _context.Newses.Remove(n);
            return await _context.SaveChangesAsync();
        }
    }
}
