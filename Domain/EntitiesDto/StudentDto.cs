using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesDto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public bool Enabled { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
    }
}
