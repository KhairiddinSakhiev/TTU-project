using Domain.EntitiesDto;

namespace Services.EntitiesServices.TeacherServices;

public interface ITeacherService
{
    Task<List<Domain.Entities.Teacher>> GetTeachers();
    Task<int> InsertTeacher(TeacherDto teacher);
    Task<int> UpdateTeacher(TeacherDto teacher);
    Task<TeacherDto> GetTeacherById(int id);
    Task<int> DeleteTeacher(int id);
}