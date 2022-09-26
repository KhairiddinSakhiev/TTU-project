using Domain.EntitiesDto;
using Domain.Entities;

namespace Services.EntitiesServices.TeacherServices;

public interface ITeacherService
{
    Task<List<Teacher>> GetTeachers();
    Task<int> InsertTeacher(TeacherDto teacher);
    Task<int> UpdateTeacher(TeacherDto teacher);
    Task<TeacherDto> GetTeacherById(int id);
    Task<int> DeleteTeacher(TeacherDto dto);
}