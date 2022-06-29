using Domain.Entities;
using Domain.EntitiesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EntitiesServices.DepartmentImageServices
{
    public  interface IDepartmentImageService
    {
        Task<List<DepartmentImage>> GetDepartmentImages();
        Task<DepartmentImageDto> GetDepartmentImageById(int id);
        Task<int> Insert(DepartmentImageDto department);
        Task<int> Update(DepartmentImageDto department);
        Task<int> Delete(DepartmentImageDto department);
        Task<int> Delete(int Id);

    }
}
