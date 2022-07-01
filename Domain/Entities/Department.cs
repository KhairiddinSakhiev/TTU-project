using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Department
    {

        
        public int Id { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Введите загаловок для учреждения ")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? Enabled { get; set; }
        public virtual List<DepartmentImage>? DepartmentImages { get; set; }
        public virtual  List<Position>? Positions { get; set; }

    }
}
