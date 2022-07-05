using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.TeacherServices;

public class TeacherService : ITeacherService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TeacherService(DataContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }
    
    public async Task<List<Teacher>> GetTeachers()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<int> InsertTeacher(TeacherDto teacher)
    {
        var mapped = _mapper.Map<Teacher>(teacher);
        await _context.Teachers.AddAsync(mapped);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateTeacher(TeacherDto teacher)
    {
        var finded = await _context.Teachers.FindAsync(teacher.Id);
        if (finded == null) return 0;
        finded.FirstName = teacher.FirstName;
        finded.LastName = teacher.LastName;
        finded.MiddleName = teacher.MiddleName;
        finded.Address = teacher.Address;
        finded.Description = teacher.Description;
        finded.Email = teacher.Email;
        finded.Image = teacher.Image;
        finded.BirthDate = teacher.BirthDate;
        finded.PhoneNumber = teacher.PhoneNumber;
        finded.Gender = teacher.Gender;
        finded.Status = teacher.Status;
        finded.Degree = teacher.Degree;
        return await _context.SaveChangesAsync();
    }

    public async Task<TeacherDto> GetTeacherById(int id)
    {
        
        var finded = await _context.Teachers.FindAsync(id);
        var conv = _mapper.Map<TeacherDto>(finded);
        return conv;
        // var finded = await (from t in _context.Teachers
        //     where t.Id == id
        //     select new TeacherDto
        //     {
        //         Id = t.Id,
        //         FirstName = t.FirstName,
        //         LastName = t.LastName,
        //         MiddleName = t.MiddleName,
        //         Address = t.Address,
        //         Description = t.Description,
        //         Email = t.Email,
        //         Image = t.Image,
        //         BirthDate = t.BirthDate,
        //         PhoneNumber = t.PhoneNumber,
        //         Gender = t.Gender,
        //         Status = t.Status,
        //         Degree = t.Degree
        //     }).FirstOrDefaultAsync();
        // return finded;
    }

    public async Task<int> DeleteTeacher(int id)
    {
        var finded = await _context.Teachers.FindAsync(id);
        if (finded == null) return 0;
        _context.Teachers.Remove(finded);
        return await _context.SaveChangesAsync();
    }
}