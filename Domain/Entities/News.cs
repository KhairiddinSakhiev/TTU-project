using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class News
    {
        public int Id { get; set; }
       
        [MaxLength(150)]
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt {get; set;}
        public bool Enabled { get; set; }

        
    }
}
