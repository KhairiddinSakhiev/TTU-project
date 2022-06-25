using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public  class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentImage> DepartmentImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<News> Newses { get; set; }
    }
}
