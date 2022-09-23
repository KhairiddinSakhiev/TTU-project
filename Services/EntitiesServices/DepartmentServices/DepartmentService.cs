using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.DepartmentServices
{
    public  class DepartmentService:IDepartmentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(DataContext context,IMapper mapper  )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Delete(DepartmentDto department)
        {
            var d = await  _context.Departments.FindAsync(department.Id);
            if (d == null) return 0;
            _context.Departments.Remove(d);
            return await  _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var d = await _context.Departments.FindAsync(Id);
            if (d == null) return 0;
            _context.Departments.Remove(d);
            return await _context.SaveChangesAsync();
        }

        public async Task<DepartmentDto> GetDepartmentById(int Id)
        {
            var department = await (from d in _context.Departments
                              where d.Id == Id
                              select new DepartmentDto
                              {
                                  Id=d.Id,
                                  Title=d.Title,
                                  Description=d.Description,
                                  Enabled=(bool)d.Enabled
                              }).FirstOrDefaultAsync();
            if (department == null) return new DepartmentDto();
            return department;
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<int> Insert(DepartmentDto department)
        {
            var mapped = _mapper.Map<Department>(department);
            await _context.Departments.AddAsync(mapped);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(DepartmentDto department)
        {
            var d = await _context.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);
            if (d == null) return 0;
            d.Title = department.Title;
            d.Description = department.Description;
            d.Enabled = department.Enabled;
            return await _context.SaveChangesAsync();
        }
    }
}
