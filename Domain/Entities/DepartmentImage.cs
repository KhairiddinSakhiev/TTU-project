using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class DepartmentImage
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Введите загаловок для учреждения ")]
        public string? Title { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

    }
}
