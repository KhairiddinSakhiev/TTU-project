using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public  class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            
            builder.Entity<TeacherPosition>().HasKey(tp => new { tp.PositionId, tp.TeacherId, });
            this.SeedUsers(builder);
            this.SeedUserRoles(builder);
            this.SeedRoles(builder);

        }
        private void SeedUsers(ModelBuilder builder)
        {
            var user = new IdentityUser()
            {
                Id = "z44ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };


            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            passwordHasher.HashPassword(user, "Admin*123");

            builder.Entity<IdentityUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "dab4fac1-c546-41de-aebc-a14da6895731", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" }
                //new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "dab4fac1-c546-41de-aebc-a14da6895731", UserId = "z44ddd14-6340-4840-95c2-db12554843e5" }
                );
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
