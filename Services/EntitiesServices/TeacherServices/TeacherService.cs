using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.TeacherServices;

public class TeacherService : ITeacherService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public TeacherService(DataContext context, IMapper mapper,IWebHostEnvironment webHostEnvironment)
    {
        _context = context; 
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async Task<List<Teacher>> GetTeachers()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<int> InsertTeacher(TeacherDto teacher)
    {
        if (teacher.Image != null)
        {
            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(teacher.Image.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await teacher.Image.CopyToAsync(stream);
            }

            var mapped = _mapper.Map<Teacher>(teacher);
            mapped.Image = fileName;
            await _context.Teachers.AddAsync(mapped);
            return await _context.SaveChangesAsync();

        }
        else
        {
            var mapped = _mapper.Map<Teacher>(teacher);
            await _context.Teachers.AddAsync(mapped);
            return await _context.SaveChangesAsync();
        }
    }

    public async Task<int> UpdateTeacher(TeacherDto teacher)
    {
        if (teacher.Image != null)
        {
           
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(teacher.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await teacher.Image.CopyToAsync(stream);
                }
            var finded = await _context.Teachers.FindAsync(teacher.Id);

            if (finded == null) return 0;
            finded.FirstName = teacher.FirstName;
            finded.LastName = teacher.LastName;
            finded.MiddleName = teacher.MiddleName;
            finded.Address = teacher.Address;
            finded.Description = teacher.Description;
            finded.Email = teacher.Email;
            finded.Image = fileName;
            finded.BirthDate = teacher.BirthDate;
            finded.PhoneNumber = teacher.PhoneNumber;
            finded.Gender = teacher.Gender;
            finded.Status = teacher.Status;
            finded.Degree = teacher.Degree;
            return await _context.SaveChangesAsync();
        }
        else
        {
            var finded = await _context.Teachers.FindAsync(teacher.Id);

            if (finded == null) return 0;
            finded.FirstName = teacher.FirstName;
            finded.LastName = teacher.LastName;
            finded.MiddleName = teacher.MiddleName;
            finded.Address = teacher.Address;
            finded.Description = teacher.Description;
            finded.Email = teacher.Email;
            finded.BirthDate = teacher.BirthDate;
            finded.PhoneNumber = teacher.PhoneNumber;
            finded.Gender = teacher.Gender;
            finded.Status = teacher.Status;
            finded.Degree = teacher.Degree;
            return await _context.SaveChangesAsync();
        }
    }

    public async Task<TeacherDto> GetTeacherById(int id)
    {

        var finded = await (from t in _context.Teachers
                            where t.Id == id
                            select new TeacherDto
                            {
                              Id=t.Id,
                              FirstName=t.FirstName,
                              LastName=t.LastName,
                              MiddleName=t.MiddleName,
                              Description=t.Description,
                              Address=t.Address,
                              Email=t.Email,
                              ImageName=t.Image,
                              PhoneNumber=t.PhoneNumber,
                              Gender = t.Gender,
                              Status = t.Status,
                              Degree = t.Degree
                             }).FirstOrDefaultAsync();
        if (finded == null) return new TeacherDto();
        return finded;

    }

    public async Task<int> DeleteTeacher(TeacherDto dto)
    {   
        var finded = await _context.Teachers.FindAsync(dto.Id);
        if (finded == null) return 0;
        _context.Teachers.Remove(finded);
        return await _context.SaveChangesAsync();
    }
}