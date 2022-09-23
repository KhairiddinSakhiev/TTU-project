using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
      
        [MaxLength(30)]
        public string? Address { get; set; }
        public bool Enabled { get; set; }
        [Required]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
    }
}
