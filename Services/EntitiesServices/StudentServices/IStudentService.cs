using Domain.Entities;
using Domain.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.StudentServices
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudents();
        Task<StudentDto> GetStudentById(int Id);
        Task<int> Insert(StudentDto student);
        Task<int> Update(StudentDto student);
        Task<int> Delete(StudentDto student);
        Task<int> Delete(int Id);
    }
}
