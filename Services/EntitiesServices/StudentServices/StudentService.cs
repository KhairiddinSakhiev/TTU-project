using AutoMapper;
using Domain.Entities;
using Domain.EntitiesDto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Services.EntitiesServices.StudentServices
{
    public class StudentService:IStudentService
    {
        private readonly DataContext _cont;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentService(DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _cont = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _cont.Students.ToListAsync();
        }

        public async Task<StudentDto> GetStudentById(int Id)
        {
            var student = await (from s in _cont.Students
                                    where s.Id == Id
                                    select new StudentDto
                                    {
                                        Id = s.Id,
                                        FirstName = s.FirstName,
                                        LastName = s.LastName,
                                        ImageName = s.Image,
                                        Description = s.Description,
                                        Title = s.Title,
                                        Address = s.Address,
                                        Enabled = s.Enabled,
                                        PhoneNumber = s.PhoneNumber,
                                        Age = s.Age,

                                    }).FirstOrDefaultAsync();
            if (student == null) return new StudentDto();
            return student;
        }

        public async Task<int> Insert(StudentDto student)
        {
            if (student.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(student.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await student.Image.CopyToAsync(stream);
                }
                var mapped = _mapper.Map<Student>(student);
                mapped.Image = fileName;
                await _cont.Students.AddAsync(mapped);
                return await _cont.SaveChangesAsync();
            }
            else
            {
                var mapped = _mapper.Map<Student>(student);
                await _cont.Students.AddAsync(mapped);
                return await _cont.SaveChangesAsync();
            }
        }
        public async Task<int> Update(StudentDto student)
        {
            if (student.Image != null)
            {
                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(student.Image.FileName);
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await student.Image.CopyToAsync(stream);
                }
                var s = await _cont.Students.FindAsync(student.Id);
                if (s == null) return 0;
                s.FirstName = student.FirstName;
                s.LastName = student.LastName;
                s.Image = fileName;
                s.Description = student.Description;
                s.Title = student.Title;
                s.Address = student.Address;
                s.Enabled = student.Enabled;
                s.PhoneNumber = student.PhoneNumber;
                s.Age = student.Age;
                return await _cont.SaveChangesAsync();
            }
            else
            {
                var s = await _cont.Students.FindAsync(student.Id);
                if (s == null) return 0;
                s.FirstName = student.FirstName;
                s.LastName = student.LastName;
                s.Description = student.Description;
                s.Title = student.Title;
                s.Address = student.Address;
                s.Enabled = student.Enabled;
                s.PhoneNumber = student.PhoneNumber;
                s.Age = student.Age;
                return await _cont.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(StudentDto student)
        {
            var s = await _cont.Students.FindAsync(student.Id);
            if (s == null) return 0;
            _cont.Students.Remove(s);
            return await _cont.SaveChangesAsync();
        }

       
    }
}
