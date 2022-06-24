using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Slider
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Введите загаловок для учреждения ")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Встафте  фото для учреждения  ")]
        public string? Image { get; set; }
        public bool Enabled { get; set; }



    }
}
