using Domain.Entities;
using Domain.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.DepartmentServices
{
    public  interface IDepartmentService
    {
        Task<List<Department>> GetDepartments();
        Task<DepartmentDto> GetDepartmentById(int Id);
        Task<int> Insert(DepartmentDto department);
        Task<int> Update(DepartmentDto department);
        Task<int> Delete (DepartmentDto department);
        Task<int> Delete(int Id);

    }
}
