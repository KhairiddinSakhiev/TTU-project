using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.DepartmentImageServices
{
    public  class DepartmentImageService:IDepartmentImageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentImageService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Delete(DepartmentImageDto department)
        {
            var d = await _context.DepartmentImages.FindAsync(department.Id);
            if (d == null) return 0;
            _context.DepartmentImages.Remove(d);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var d = await _context.DepartmentImages.FindAsync(Id);
            if (d == null) return 0;
            _context.DepartmentImages.Remove(d);
            return await _context.SaveChangesAsync();
        }

        public async Task<DepartmentImageDto> GetDepartmentImageById(int id)
        {
            var di = await (from d in _context.DepartmentImages
                            select new DepartmentImageDto
                            {
                                Id=d.Id,
                                Title=d.Title,
                                DepartmentId=d.DepartmentId
                            }).FirstOrDefaultAsync();
            if (di == null) return new DepartmentImageDto();
            return di;
        }

        public async Task<List<DepartmentImage>> GetDepartmentImages()
        {
            return await _context.DepartmentImages.ToListAsync();
        }

        public async Task<int> Insert(DepartmentImageDto department)
        {
            var mapped = _mapper.Map<DepartmentImage>(department);
            await _context.DepartmentImages.AddAsync(mapped);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(DepartmentImageDto department)
        {
            var d = await _context.DepartmentImages.FindAsync(department.Id);
            if (d == null) return 0;
            d.Title = department.Title;
            d.DepartmentId = department.DepartmentId;
            return await _context.SaveChangesAsync();
        }
    }
}
