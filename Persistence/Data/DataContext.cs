using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public  class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherPosition>().HasKey(tp => new { tp.PositionId, tp.TeacherId,});
        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentImage> DepartmentImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherPosition> TeacherPositions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<News> Newses { get; set; }
    }
}
