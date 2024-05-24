using CoursesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesAPI.Data
{
    public class CourseDbContext:DbContext
    {
        public CourseDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Course> Courses { get; set; }
    }
}
