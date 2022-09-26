using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.SliderServices
{
    public  class SliderService:ISliderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public SliderService(DataContext context,IMapper mapper, IWebHostEnvironment  webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<int> Delete(SliderDto slider)
        {
            var s = await _context.Sliders.FindAsync(slider.Id);
            if (s == null) return 0;
            _context.Sliders.Remove(s);
            var slid= await _context.SaveChangesAsync();
            return slid;
        }

       

        public async Task<SliderDto> GetSliderById(int Id)
        {
            var slider = await (from s in _context.Sliders
                                where s.Id==Id
                                select new SliderDto
                                {
                                    Id=s.Id,
                                    Title=s.Title,
                                    ImageName=s.Image,
                                    Enabled=s.Enabled

                                }).FirstOrDefaultAsync();
            if (slider == null) return new SliderDto();
             return slider;
        }

        public async Task<List<Slider>> GetSliders()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<int> Insert(SliderDto slider)
        {
            if (slider.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(slider.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await slider.Image.CopyToAsync(stream);
                }
                var mapped = _mapper.Map<Slider>(slider);
                mapped.Image = fileName;
                await _context.Sliders.AddAsync(mapped);
                return await _context.SaveChangesAsync();
            }
            else
            {
                var mapped = _mapper.Map<Slider>(slider);
                await _context.Sliders.AddAsync(mapped);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<int> Update(SliderDto slider)
        {
            if (slider.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(slider.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await slider.Image.CopyToAsync(stream);
                }
                var s = await _context.Sliders.FindAsync(slider.Id);
                if (s == null) return 0;
                s.Title = slider.Title;
                s.Image = fileName;
                s.Enabled = slider.Enabled;
                return await _context.SaveChangesAsync();
            }
            else
            {
                var s = await _context.Sliders.FindAsync(slider.Id);
                if (s == null) return 0;
                s.Title = slider.Title;
                s.Enabled = slider.Enabled;
                return await _context.SaveChangesAsync();
            }
        }
    }
}
