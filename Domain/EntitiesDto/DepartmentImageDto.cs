using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesDto
{
    public  class DepartmentImageDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int DepartmentId { get; set; }
    }
}
